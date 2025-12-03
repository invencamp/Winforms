using System.Windows.Forms;

namespace UartWinFormsExample
{
    partial class Form1
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>

        private void InitializeComponent()
        {
            System.Windows.Forms.DataVisualization.Charting.ChartArea chartArea3 = new System.Windows.Forms.DataVisualization.Charting.ChartArea();
            System.Windows.Forms.DataVisualization.Charting.Legend legend3 = new System.Windows.Forms.DataVisualization.Charting.Legend();
            System.Windows.Forms.DataVisualization.Charting.Series series4 = new System.Windows.Forms.DataVisualization.Charting.Series();
            System.Windows.Forms.DataVisualization.Charting.Series series5 = new System.Windows.Forms.DataVisualization.Charting.Series();
            System.Windows.Forms.DataVisualization.Charting.ChartArea chartArea4 = new System.Windows.Forms.DataVisualization.Charting.ChartArea();
            System.Windows.Forms.DataVisualization.Charting.Legend legend4 = new System.Windows.Forms.DataVisualization.Charting.Legend();
            System.Windows.Forms.DataVisualization.Charting.Series series6 = new System.Windows.Forms.DataVisualization.Charting.Series();
            this.comboBoxPorts = new System.Windows.Forms.ComboBox();
            this.comboBoxBaud = new System.Windows.Forms.ComboBox();
            this.btnOpen = new System.Windows.Forms.Button();
            this.btnSend = new System.Windows.Forms.Button();
            this.txtSend = new System.Windows.Forms.TextBox();
            this.txtReceived = new System.Windows.Forms.TextBox();
            this.btnRefresh = new System.Windows.Forms.Button();
            this.checkBoxAppendNewline = new System.Windows.Forms.CheckBox();
            this.radioHex = new System.Windows.Forms.RadioButton();
            this.radioHexDisplay = new System.Windows.Forms.RadioButton();
            this.radioTextDisplay = new System.Windows.Forms.RadioButton();
            this.chart1 = new System.Windows.Forms.DataVisualization.Charting.Chart();
            this.chart2 = new System.Windows.Forms.DataVisualization.Charting.Chart();
            this.btnMode = new System.Windows.Forms.Button();
            this.lbl_DateAndTime = new System.Windows.Forms.Label();
            this.txtSet = new System.Windows.Forms.TextBox();
            this.label11 = new System.Windows.Forms.Label();
            this.btnSet = new System.Windows.Forms.Button();
            ((System.ComponentModel.ISupportInitialize)(this.chart1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.chart2)).BeginInit();
            this.SuspendLayout();
            // 
            // comboBoxPorts
            // 
            this.comboBoxPorts.Location = new System.Drawing.Point(12, 12);
            this.comboBoxPorts.Name = "comboBoxPorts";
            this.comboBoxPorts.Size = new System.Drawing.Size(120, 21);
            this.comboBoxPorts.TabIndex = 0;
            // 
            // comboBoxBaud
            // 
            this.comboBoxBaud.Location = new System.Drawing.Point(138, 12);
            this.comboBoxBaud.Name = "comboBoxBaud";
            this.comboBoxBaud.Size = new System.Drawing.Size(80, 21);
            this.comboBoxBaud.TabIndex = 1;
            // 
            // btnOpen
            // 
            this.btnOpen.Location = new System.Drawing.Point(290, 10);
            this.btnOpen.Name = "btnOpen";
            this.btnOpen.Size = new System.Drawing.Size(60, 23);
            this.btnOpen.TabIndex = 2;
            this.btnOpen.Text = "Open";
            this.btnOpen.Click += new System.EventHandler(this.btnOpen_Click);
            // 
            // btnSend
            // 
            this.btnSend.Location = new System.Drawing.Point(380, 248);
            this.btnSend.Name = "btnSend";
            this.btnSend.Size = new System.Drawing.Size(92, 23);
            this.btnSend.TabIndex = 3;
            this.btnSend.Text = "Send";
            this.btnSend.Click += new System.EventHandler(this.btnSend_Click);
            // 
            // txtSend
            // 
            this.txtSend.Location = new System.Drawing.Point(12, 250);
            this.txtSend.Name = "txtSend";
            this.txtSend.Size = new System.Drawing.Size(360, 20);
            this.txtSend.TabIndex = 4;
            this.txtSend.TextChanged += new System.EventHandler(this.txtSend_TextChanged);
            // 
            // txtReceived
            // 
            this.txtReceived.Location = new System.Drawing.Point(12, 40);
            this.txtReceived.Multiline = true;
            this.txtReceived.Name = "txtReceived";
            this.txtReceived.ScrollBars = System.Windows.Forms.ScrollBars.Vertical;
            this.txtReceived.Size = new System.Drawing.Size(460, 200);
            this.txtReceived.TabIndex = 5;
            // 
            // btnRefresh
            // 
            this.btnRefresh.Location = new System.Drawing.Point(224, 10);
            this.btnRefresh.Name = "btnRefresh";
            this.btnRefresh.Size = new System.Drawing.Size(60, 23);
            this.btnRefresh.TabIndex = 6;
            this.btnRefresh.Text = "Refresh";
            this.btnRefresh.Click += new System.EventHandler(this.btnRefresh_Click);
            // 
            // checkBoxAppendNewline
            // 
            this.checkBoxAppendNewline.Location = new System.Drawing.Point(12, 280);
            this.checkBoxAppendNewline.Name = "checkBoxAppendNewline";
            this.checkBoxAppendNewline.Size = new System.Drawing.Size(104, 24);
            this.checkBoxAppendNewline.TabIndex = 7;
            this.checkBoxAppendNewline.Text = "Append CRLF";
            // 
            // radioHex
            // 
            this.radioHex.Location = new System.Drawing.Point(260, 280);
            this.radioHex.Name = "radioHex";
            this.radioHex.Size = new System.Drawing.Size(104, 24);
            this.radioHex.TabIndex = 8;
            this.radioHex.Text = "Send as Hex";
            // 
            // radioHexDisplay
            // 
            this.radioHexDisplay.Location = new System.Drawing.Point(180, 280);
            this.radioHexDisplay.Name = "radioHexDisplay";
            this.radioHexDisplay.Size = new System.Drawing.Size(104, 24);
            this.radioHexDisplay.TabIndex = 9;
            this.radioHexDisplay.Text = "Hex display";
            // 
            // radioTextDisplay
            // 
            this.radioTextDisplay.Checked = true;
            this.radioTextDisplay.Location = new System.Drawing.Point(120, 280);
            this.radioTextDisplay.Name = "radioTextDisplay";
            this.radioTextDisplay.Size = new System.Drawing.Size(104, 24);
            this.radioTextDisplay.TabIndex = 10;
            this.radioTextDisplay.TabStop = true;
            this.radioTextDisplay.Text = "Text";
            // 
            // chart1
            // 
            chartArea3.Name = "ChartArea1";
            this.chart1.ChartAreas.Add(chartArea3);
            legend3.Name = "Legend1";
            this.chart1.Legends.Add(legend3);
            this.chart1.Location = new System.Drawing.Point(478, 4);
            this.chart1.Name = "chart1";
            series4.ChartArea = "ChartArea1";
            series4.ChartType = System.Windows.Forms.DataVisualization.Charting.SeriesChartType.Spline;
            series4.Legend = "Legend1";
            series4.Name = "Nhiệt độ";
            series5.ChartArea = "ChartArea1";
            series5.ChartType = System.Windows.Forms.DataVisualization.Charting.SeriesChartType.Spline;
            series5.Legend = "Legend1";
            series5.Name = "Độ ẩm";
            this.chart1.Series.Add(series4);
            this.chart1.Series.Add(series5);
            this.chart1.Size = new System.Drawing.Size(328, 267);
            this.chart1.TabIndex = 11;
            this.chart1.Text = "chart1";
            // 
            // chart2
            // 
            chartArea4.Name = "ChartArea1";
            this.chart2.ChartAreas.Add(chartArea4);
            legend4.Name = "Legend1";
            this.chart2.Legends.Add(legend4);
            this.chart2.Location = new System.Drawing.Point(478, 277);
            this.chart2.Name = "chart2";
            series6.ChartArea = "ChartArea1";
            series6.ChartType = System.Windows.Forms.DataVisualization.Charting.SeriesChartType.Spline;
            series6.Legend = "Legend1";
            series6.Name = "ADC";
            this.chart2.Series.Add(series6);
            this.chart2.Size = new System.Drawing.Size(328, 254);
            this.chart2.TabIndex = 12;
            this.chart2.Text = "chart2";
            // 
            // btnMode
            // 
            this.btnMode.Location = new System.Drawing.Point(365, 10);
            this.btnMode.Name = "btnMode";
            this.btnMode.Size = new System.Drawing.Size(75, 23);
            this.btnMode.TabIndex = 13;
            this.btnMode.Text = "Tự động";
            this.btnMode.UseVisualStyleBackColor = true;
            this.btnMode.Click += new System.EventHandler(this.btnMode_Click);
            // 
            // lbl_DateAndTime
            // 
            this.lbl_DateAndTime.AutoSize = true;
            this.lbl_DateAndTime.Location = new System.Drawing.Point(40, 376);
            this.lbl_DateAndTime.Name = "lbl_DateAndTime";
            this.lbl_DateAndTime.Size = new System.Drawing.Size(59, 13);
            this.lbl_DateAndTime.TabIndex = 14;
            this.lbl_DateAndTime.Text = "Date & Time";
            // 
            // txtSet
            // 
            this.txtSet.Location = new System.Drawing.Point(258, 332);
            this.txtSet.Name = "txtSet";
            this.txtSet.Size = new System.Drawing.Size(100, 20);
            this.txtSet.TabIndex = 15;
            // 
            // label11
            // 
            this.label11.AutoSize = true;
            this.label11.Location = new System.Drawing.Point(203, 335);
            this.label11.Name = "label11";
            this.label11.Size = new System.Drawing.Size(49, 13);
            this.label11.TabIndex = 16;
            this.label11.Text = "Setpoint:";
            // 
            // btnSet
            // 
            this.btnSet.Location = new System.Drawing.Point(380, 330);
            this.btnSet.Name = "btnSet";
            this.btnSet.Size = new System.Drawing.Size(75, 23);
            this.btnSet.TabIndex = 17;
            this.btnSet.Text = "Set";
            this.btnSet.UseVisualStyleBackColor = true;
            this.btnSet.Click += new System.EventHandler(this.btnSet_Click);
            // 
            // Form1
            // 
            this.ClientSize = new System.Drawing.Size(808, 529);
            this.Controls.Add(this.btnSet);
            this.Controls.Add(this.label11);
            this.Controls.Add(this.txtSet);
            this.Controls.Add(this.lbl_DateAndTime);
            this.Controls.Add(this.btnMode);
            this.Controls.Add(this.chart2);
            this.Controls.Add(this.chart1);
            this.Controls.Add(this.comboBoxPorts);
            this.Controls.Add(this.comboBoxBaud);
            this.Controls.Add(this.btnOpen);
            this.Controls.Add(this.btnSend);
            this.Controls.Add(this.txtSend);
            this.Controls.Add(this.txtReceived);
            this.Controls.Add(this.btnRefresh);
            this.Controls.Add(this.checkBoxAppendNewline);
            this.Controls.Add(this.radioHex);
            this.Controls.Add(this.radioHexDisplay);
            this.Controls.Add(this.radioTextDisplay);
            this.Name = "Form1";
            this.Text = "UART WinForms";
            this.Load += new System.EventHandler(this.Form1_Load);
            ((System.ComponentModel.ISupportInitialize)(this.chart1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.chart2)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button button1;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.ComboBox comboBox1;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.ComboBox comboBox2;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.TextBox textBox1;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.Button button2;
        private System.Windows.Forms.Label label7;
        private System.Windows.Forms.Button button3;
        private System.Windows.Forms.Label label8;
        private System.Windows.Forms.Label label9;
        private System.Windows.Forms.Label label10;

        private System.Windows.Forms.ComboBox comboBoxPorts;
        private System.Windows.Forms.ComboBox comboBoxBaud;
        private System.Windows.Forms.Button btnOpen;
        private System.Windows.Forms.Button btnSend;
        private System.Windows.Forms.TextBox txtSend;
        private System.Windows.Forms.TextBox txtReceived;
        private System.Windows.Forms.Button btnRefresh;
        private System.Windows.Forms.CheckBox checkBoxAppendNewline;
        private System.Windows.Forms.RadioButton radioHex;
        private System.Windows.Forms.RadioButton radioHexDisplay;
        private System.Windows.Forms.RadioButton radioTextDisplay;
        private System.Windows.Forms.DataVisualization.Charting.Chart chart1;
        private System.Windows.Forms.DataVisualization.Charting.Chart chart2;
        private Button btnMode;
        private Label lbl_DateAndTime;
        private TextBox txtSet;
        private Label label11;
        private Button btnSet;
    }
}

