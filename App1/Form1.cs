using Firebase.Database;
using Firebase.Database.Query;
using System;
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
        private FirebaseClient firebase;
        private CancellationTokenSource pushFirebaseCts;

        // Biến lưu giá trị hiện tại để push lên Firebase
        private double temp = 0;
        private double humi = 0;
        private int adc = 0;
        public Form1()
        {
            InitializeComponent();
            InitSerial();
            LoadComPorts();
            InitCharts(); // Thêm dòng này
            try // connect to the firebase
            {
                firebase = new FirebaseClient("https://dht22-fed51-default-rtdb.firebaseio.com/");
            }
            catch (Exception ex) // connect failed
            {
                MessageBox.Show("Không thể khởi tạo Firebase client: " + ex.Message,
                                "Lỗi kết nối", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
           
        }
        // Hàm push dữ liệu lên Firebase
        private async void PushDataToFirebase()
        {
            pushFirebaseCts = new CancellationTokenSource();

            while (!pushFirebaseCts.Token.IsCancellationRequested)
            {
                try
                {
                    if (firebase != null)
                    {
                        // Lấy giá trị PWM từ txtSend
                        string pwmValue = "";
                        Invoke(new Action(() =>
                        {
                            pwmValue = txtSend.Text;
                        }));

                        // Push dữ liệu lên Firebase
                        await firebase
                            .Child("Data")
                            .PutAsync(new
                            {
                                Temp = temp,
                                Humi = humi,
                                ADC = adc,
                                PWM = pwmValue,
                                Timestamp = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss")
                            });

                        // Cập nhật status (optional)
                        BeginInvoke(new Action(() =>
                        {
                            // Có thể thêm label status nếu muốn
                            // lblStatus.Text = $"✓ Đã push lên Firebase lúc {DateTime.Now:HH:mm:ss}";
                        }));
                    }
                }
                catch (Exception ex)
                {
                    BeginInvoke(new Action(() =>
                    {
                        txtReceived.AppendText($"⚠️ Lỗi push Firebase: {ex.Message}\n");
                    }));
                }

                // Chờ 3 giây trước khi push lần tiếp theo
                await Task.Delay(3000);
            }
        }
        private void InitCharts()
        {
            // Đảm bảo chart đã được khởi tạo đúng
            if (chart1.Series.Count < 2)
            {
                chart1.Series.Clear();
                chart1.Series.Add("Temperature");
                chart1.Series.Add("Humidity");
            }

            if (chart2.Series.Count < 1)
            {
                chart2.Series.Clear();
                chart2.Series.Add("ADC");
            }

            chart1.Series[0].ChartType = SeriesChartType.Line;
            chart1.Series[1].ChartType = SeriesChartType.Line;
            chart2.Series[0].ChartType = SeriesChartType.Line;
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
                    pushFirebaseCts?.Cancel(); return; }

                if (comboBoxPorts.SelectedItem == null) { MessageBox.Show("Chọn COM port"); return; }
                _serial.PortName = comboBoxPorts.SelectedItem.ToString();
                _serial.BaudRate = int.Parse(comboBoxBaud.SelectedItem.ToString());
                _serial.Open();
                btnOpen.Text = "Close";
                // Bắt đầu push Firebase khi mở COM
                Task.Run(() => PushDataToFirebase());
            }
            catch (Exception ex) { MessageBox.Show("Không mở được COM: " + ex.Message); }
        }

        private void btnSend_Click(object sender, EventArgs e)
        {
            if (!_serial.IsOpen) { MessageBox.Show("COM chưa mở"); return; }
            try
            {
                string s = txtSend.Text;
                if (checkBoxAppendNewline.Checked) s += "\r\n";
                if (radioHex.Checked)
                {
                    // gửi hex (ví dụ: "0A 01 FF")
                    byte[] data = HexStringToBytes(s);
                    _serial.Write(data, 0, data.Length);
                }
                else
                {
                    _serial.Write(s);
                }
            }
            catch (Exception ex) { MessageBox.Show("Gửi lỗi: " + ex.Message); }
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
                            @"TEMP\s*=\s*([\d\.]+)\s*;\s*HUMI\s*=\s*([\d\.]+)\s*;\s*ADC\s*=\s*(\d+)",
                            RegexOptions.IgnoreCase | RegexOptions.Multiline
                        );

                        if (match.Success)
                        {
                            temp = double.Parse(match.Groups[1].Value);
                            humi = double.Parse(match.Groups[2].Value);
                            adc = int.Parse(match.Groups[3].Value);

                            // Vẽ chart
                            chart1.Series[0].Points.AddY(temp);
                            chart1.Series[1].Points.AddY(humi);
                            chart2.Series[0].Points.AddY(adc);

                            // Giới hạn số điểm hiển thị (tránh chart quá dày)
                            if (chart1.Series[0].Points.Count > 100)
                            {
                                chart1.Series[0].Points.RemoveAt(0);
                                chart1.Series[1].Points.RemoveAt(0);
                            }
                            if (chart2.Series[0].Points.Count > 100)
                            {
                                chart2.Series[0].Points.RemoveAt(0);
                            }
                        }
                        else
                        {
                            // Debug: hiển thị dữ liệu không match
                            txtReceived.AppendText($"[DEBUG] No match: {completeLine.Trim()}\r\n");
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
            if (radioHexDisplay.Checked)
            {
                txtReceived.AppendText(BitConverter.ToString(data).Replace("-", " ") + Environment.NewLine);
            }
            else
            {
                string s = _serial.Encoding.GetString(data);
                txtReceived.AppendText(s);
            }
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

        private void btnMode_Click(object sender, EventArgs e)
        {
            if (btnMode.Text == "Tự động")
            {
                _serial.Write("m");
                btnMode.Text = "Thủ công";
            }
            else
            {
                _serial.Write("a");
                btnMode.Text = "Tự động";
            }
               
        }
    }
}
