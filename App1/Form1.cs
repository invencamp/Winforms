using System;
using System.Data.SqlClient;
using System.Drawing;
using System.IO.Ports;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Windows.Forms.DataVisualization.Charting;

namespace UartWinFormsExample
{
    public partial class Form1 : Form
    {
        private SerialPort _serial;

        private bool isTrackingStep = false;
        private double maxVelocityReached = 0;
        private int stepPointsCount = 0; // Đếm số điểm (mỗi điểm = 0.5s)
        private double currentSetpoint = 0;
        private bool wasTuning = false;
        private double startVelocity = 0; // Lưu tốc độ lúc vừa ấn Set
        private double minVelocityReached = 9999; // Dùng cho giảm tốc
        double overshoot = 0;
        private int stablePointsCount = 0; // Đếm số điểm "nằm ngoan" liên tiếp

        private double velocity = 0;
        private string setpoint = "80";
        private double currentSpeed = 0;
        public Form1()
        {
            InitializeComponent();
            InitSerial();
            LoadComPorts();
            InitCharts(); // Thêm dòng này           
        }
        private void InitCharts()
        {
            if (chart2.Series.Count < 2)
            {
                chart2.Series.Clear();
                chart2.Series.Add("Velocity");
                chart2.Series.Add("Setpoint");
            }
            chart2.Series[0].ChartType = SeriesChartType.Line;
            chart2.Series[1].ChartType = SeriesChartType.Line;

            // Cố định trục Y cho chart2 (ADC)
            chart2.ChartAreas[0].AxisY.Minimum = double.NaN;
            chart2.ChartAreas[0].AxisY.Maximum = 200;  // Với ADC 12-bit
        }
       
        private void InitSerial()
        {
            _serial = new SerialPort();
            _serial.BaudRate = 9600;
            _serial.DataBits = 8;
            _serial.Parity = Parity.None;
            _serial.StopBits = StopBits.One;
            _serial.Encoding = Encoding.UTF8;
            _serial.DataReceived += Serial_DataReceived;
        }

        private void LoadComPorts()
        {
            comboBoxPorts.Items.Clear();
            foreach (var p in SerialPort.GetPortNames())
                comboBoxPorts.Items.Add(p);
            if (comboBoxPorts.Items.Count > 0)
                comboBoxPorts.SelectedIndex = 0;
            comboBoxBaud.Items.Clear();
            comboBoxBaud.Items.AddRange(new object[] { "9600", "19200", "38400", "57600", "115200" });
            comboBoxBaud.SelectedItem = "9600";
        }

        private void btnOpen_Click(object sender, EventArgs e)
        {
            try
            {
                if (_serial.IsOpen) { _serial.Close(); btnOpen.Text = "Open";// Dừng push Firebase khi đóng COM
                     return; }

                if (comboBoxPorts.SelectedItem == null) { MessageBox.Show("Chọn COM port"); return; }
                _serial.PortName = comboBoxPorts.SelectedItem.ToString();
                _serial.BaudRate = int.Parse(comboBoxBaud.SelectedItem.ToString());
                _serial.Open();
                btnOpen.Text = "Close";

            }
            catch (Exception ex) { MessageBox.Show("Không mở được COM: " + ex.Message); }
        }

        

        // Thêm biến để lưu buffer tích lũy
        private StringBuilder _receiveBuffer = new StringBuilder();

        private void Serial_DataReceived(object sender, SerialDataReceivedEventArgs e)
        {
            try
            {
                int n = _serial.BytesToRead;
                if (n <= 0) return;

                byte[] buffer = new byte[n];
                _serial.Read(buffer, 0, n);
                string data = Encoding.UTF8.GetString(buffer);

                BeginInvoke(new Action(() =>
                {
                    AppendReceived(buffer);

                    // Tích lũy dữ liệu vào buffer
                    _receiveBuffer.Append(data);
                    string bufferContent = _receiveBuffer.ToString();

                    // Tìm dòng hoàn chỉnh (kết thúc bằng \n)
                    int newlineIndex = bufferContent.LastIndexOf('\n');
                    if (newlineIndex >= 0)
                    {
                        // Lấy phần dữ liệu đầy đủ
                        string completeLine = bufferContent.Substring(0, newlineIndex + 1);

                        // Giữ lại phần chưa đầy đủ
                        _receiveBuffer.Clear();
                        if (newlineIndex + 1 < bufferContent.Length)
                        {
                            _receiveBuffer.Append(bufferContent.Substring(newlineIndex + 1));
                        }

                        // Parse dữ liệu
                        var match = Regex.Match(
                            completeLine,
                            @"enc=([\d\.]+);\s*pwm=([-\d\.]+)",
                            RegexOptions.IgnoreCase
                        );

                        if (match.Success)
                        {
                            // 2. Lấy giá trị Speed (tương ứng với enc trong chuỗi gửi)
                            if (double.TryParse(match.Groups[1].Value, out currentSpeed))
                            {
                                velocity = currentSpeed; // Cập nhật biến velocity toàn cục

                                // 3. Cập nhật lên Chart
                                chart2.Series["Velocity"].Points.AddY(velocity);

                                // Chuyển setpoint từ string sang double để vẽ
                                if (double.TryParse(setpoint, out double spValue))
                                {
                                    chart2.Series["Setpoint"].Points.AddY(spValue);
                                }

                                // 4. Giới hạn số lượng điểm hiển thị (tránh lag)
                                if (chart2.Series[0].Points.Count > 100)
                                {
                                    chart2.Series["Velocity"].Points.RemoveAt(0);
                                    chart2.Series["Setpoint"].Points.RemoveAt(0);
                                }                                
                            }
                        }
                        if (completeLine.Contains("TUNING"))
                        {
                            wasTuning = true;
                            isTrackingStep = false; // Tạm dừng tính toán thông số
                            lblOvershoot.Text = "Đang đo đặc tính...";
                            lblSettlingTime.Text = "Hệ thống đang dao động...";
                        }
                        else if (completeLine.Contains("PID RUN") && wasTuning == true)
                        {
                            // Khoảnh khắc vàng: Vừa Tune xong và bắt đầu chạy PID!
                            stablePointsCount = 0;
                            wasTuning = false;       // Reset cờ
                            isTrackingStep = true;   // BẮT ĐẦU TÍNH TOÁN!
                            stepPointsCount = 0;
                            startVelocity = velocity;
                            maxVelocityReached = velocity;
                            minVelocityReached = velocity;
                            currentSetpoint = double.Parse(setpoint); // Lấy Setpoint hiện tại

                            lblOvershoot.Text = "Đang tính vọt lố...";
                        }

                        if (isTrackingStep)
                        {
                            stepPointsCount++;


                            // Liên tục tìm đỉnh và đáy
                            if (currentSpeed > maxVelocityReached) maxVelocityReached = currentSpeed;
                            if (currentSpeed < minVelocityReached) minVelocityReached = currentSpeed;

                            // Tính sai số hiện tại so với Setpoint
                            double error = Math.Abs(currentSetpoint - currentSpeed);
                            double errorPercent = (error / currentSetpoint) * 100.0;

                            // 2. Cập nhật Sai số liên tục
                            lblError.Text = $"Sai số tĩnh: {errorPercent:F1}%";

                            // KIỂM TRA ĐANG TĂNG TỐC HAY GIẢM TỐC
                            if (currentSetpoint >= startVelocity)
                            {
                                // Tăng tốc: Tìm độ vọt lố CẠNH TRÊN
                                if (maxVelocityReached > currentSetpoint)
                                    overshoot = ((maxVelocityReached - currentSetpoint) / currentSetpoint) * 100.0;
                            }
                            else
                            {
                                // Giảm tốc: Tìm độ vọt lố CẠNH DƯỚI (Undershoot)
                                if (minVelocityReached < currentSetpoint)
                                    overshoot = ((currentSetpoint - minVelocityReached) / currentSetpoint) * 100.0;
                            }
                            lblOvershoot.Text = $"Vọt lố: {overshoot:F1}%";

                            // 2. LOGIC KIỂM TRA ĐỘ ỔN ĐỊNH THỰC SỰ
                            if (errorPercent <= 5.0)
                            {
                                stablePointsCount++; // Ngoan ngoãn ở trong dải 5% -> Cộng dồn điểm
                            }
                            else
                            {
                                stablePointsCount = 0; // BỊ VĂNG RA NGOÀI -> Hủy bỏ, đếm lại sự ổn định từ đầu!
                            }

                            // 3. Kiểm tra Thời gian đáp ứng (Settling Time)
                            // Nếu tốc độ đã lọt vào dải sai số +-5% và ở đó (có thể thêm logic đếm liên tiếp)
                            if (stablePointsCount >= 15)
                            {



                                double settlingTime = (stepPointsCount - stablePointsCount) * 0.5;
                                lblSettlingTime.Text = $"T/gian đáp ứng: {settlingTime:F1} s";

                                // Hoàn thành 1 lần đo
                                isTrackingStep = false;
                            }
                        }
                    }
                }));
            }
            catch (Exception ex)
            {
                BeginInvoke(new Action(() =>
                    txtReceived.AppendText($"⚠️ Lỗi đọc dữ liệu: {ex.Message}\n")));
            }
        }

        private void AppendReceived(byte[] data)
        {
                string s = _serial.Encoding.GetString(data);
                txtReceived.AppendText(s);
        }

        private void btnRefresh_Click(object sender, EventArgs e) => LoadComPorts();

        private static byte[] HexStringToBytes(string hex)
        {
            var cleaned = hex.Replace(" ", "").Replace("\r", "").Replace("\n", "");
            if (cleaned.Length % 2 != 0) cleaned = "0" + cleaned;
            byte[] bytes = new byte[cleaned.Length / 2];
            for (int i = 0; i < bytes.Length; i++)
                bytes[i] = Convert.ToByte(cleaned.Substring(i * 2, 2), 16);
            return bytes;
        }

        // Dispose serial khi Form đóng
        protected override void OnFormClosing(FormClosingEventArgs e)
        {
            if (_serial != null)
            {
                try { if (_serial.IsOpen) _serial.Close(); _serial.Dispose(); } catch { }
            }
            base.OnFormClosing(e);
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            lbl_DateAndTime.Text = "Date: " + DateTime.Now.ToString("yyyy-MM-dd");
        }


        private void btnSet_Click(object sender, EventArgs e)
        {
            if (!_serial.IsOpen) { MessageBox.Show("COM chưa mở"); return; }
            try
            {
                setpoint = txtSet.Text;
                _serial.Write(setpoint + "\n");
                currentSetpoint = double.Parse(txtSet.Text);
                isTrackingStep = true;
                stablePointsCount = 0;
                stepPointsCount = 0;
                startVelocity = velocity; // CHỐT TỐC ĐỘ BAN ĐẦU
                maxVelocityReached = velocity;
                minVelocityReached = velocity;
                lblOvershoot.Text = "Overshoot: Tính toán...";
                lblSettlingTime.Text = "Thời gian: Tính toán...";
            }
            catch (Exception ex) { MessageBox.Show("Gửi lỗi: " + ex.Message); }
        }

        private void btn_Tune_Click(object sender, EventArgs e)
        {
            if (!_serial.IsOpen) { MessageBox.Show("COM chưa mở"); return; }
            _serial.Write("t");
        }
    }
}
