using System;
using System.Drawing;
using System.IO.Ports;
using System.Text;
using System.Text.RegularExpressions;
using System.Windows.Forms;
using System.Windows.Forms.DataVisualization.Charting;

namespace UartWinFormsExample
{
    public partial class Form1 : Form
    {
        private SerialPort _serial;

        public Form1()
        {
            InitializeComponent();
            InitSerial();
            LoadComPorts();
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
                if (_serial.IsOpen) { _serial.Close(); btnOpen.Text = "Open"; return; }

                if (comboBoxPorts.SelectedItem == null) { MessageBox.Show("Chọn COM port"); return; }
                _serial.PortName = comboBoxPorts.SelectedItem.ToString();
                _serial.BaudRate = int.Parse(comboBoxBaud.SelectedItem.ToString());
                _serial.Open();
                btnOpen.Text = "Close";
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

        // xử lý dữ liệu đến (chạy trong thread worker của SerialPort)
        private void Serial_DataReceived(object sender, SerialDataReceivedEventArgs e)
        {
            try
            {
                int n = _serial.BytesToRead;
                byte[] buffer = new byte[n];
                _serial.Read(buffer, 0, n);
                // chuyển về text để hiển thị (UTF8) hoặc hiển thị hex
                if (InvokeRequired)
                {
                    BeginInvoke(new Action(() => AppendReceived(buffer)));

                }
                else AppendReceived(buffer);
                Match match = Regex.Match(data, @"TEMP=([\d\.]+); HUMI=([\d\.]+)");
                double temp = double.Parse(match.Groups[1].Value);
                double humi = double.Parse(match.Groups[2].Value);
                int ADC = int.Parse(match.Groups[3].Value);
                chart1.Series[0].Points.AddY(temp);
                chart1.Series[1].Points.AddY(humi);
                chart2.Series[0].Points.AddY(ADC);
            }
            catch { /* ignore */ }
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

        }
    }
}
