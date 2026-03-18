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
            System.Windows.Forms.DataVisualization.Charting.ChartArea chartArea1 = new System.Windows.Forms.DataVisualization.Charting.ChartArea();
            System.Windows.Forms.DataVisualization.Charting.Legend legend1 = new System.Windows.Forms.DataVisualization.Charting.Legend();
            System.Windows.Forms.DataVisualization.Charting.Series series1 = new System.Windows.Forms.DataVisualization.Charting.Series();
            this.comboBoxPorts = new System.Windows.Forms.ComboBox();
            this.comboBoxBaud = new System.Windows.Forms.ComboBox();
            this.btnOpen = new System.Windows.Forms.Button();
            this.txtReceived = new System.Windows.Forms.TextBox();
            this.btnRefresh = new System.Windows.Forms.Button();
            this.chart2 = new System.Windows.Forms.DataVisualization.Charting.Chart();
            this.lbl_DateAndTime = new System.Windows.Forms.Label();
            this.txtSet = new System.Windows.Forms.TextBox();
            this.label11 = new System.Windows.Forms.Label();
            this.btnSet = new System.Windows.Forms.Button();
            this.btn_Tune = new System.Windows.Forms.Button();
            this.lblOvershoot = new System.Windows.Forms.Label();
            this.lblSettlingTime = new System.Windows.Forms.Label();
            this.lblError = new System.Windows.Forms.Label();
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
            // chart2
            // 
            chartArea1.Name = "ChartArea1";
            this.chart2.ChartAreas.Add(chartArea1);
            legend1.Name = "Legend1";
            this.chart2.Legends.Add(legend1);
            this.chart2.Location = new System.Drawing.Point(0, 277);
            this.chart2.Name = "chart2";
            series1.ChartArea = "ChartArea1";
            series1.ChartType = System.Windows.Forms.DataVisualization.Charting.SeriesChartType.Spline;
            series1.Legend = "Legend1";
            series1.Name = "ADC";
            this.chart2.Series.Add(series1);
            this.chart2.Size = new System.Drawing.Size(806, 254);
            this.chart2.TabIndex = 12;
            this.chart2.Text = "chart2";
            // 
            // lbl_DateAndTime
            // 
            this.lbl_DateAndTime.AutoSize = true;
            this.lbl_DateAndTime.Location = new System.Drawing.Point(543, 20);
            this.lbl_DateAndTime.Name = "lbl_DateAndTime";
            this.lbl_DateAndTime.Size = new System.Drawing.Size(59, 13);
            this.lbl_DateAndTime.TabIndex = 14;
            this.lbl_DateAndTime.Text = "Date & Time";
            // 
            // txtSet
            // 
            this.txtSet.Location = new System.Drawing.Point(559, 92);
            this.txtSet.Name = "txtSet";
            this.txtSet.Size = new System.Drawing.Size(100, 20);
            this.txtSet.TabIndex = 15;
            // 
            // label11
            // 
            this.label11.AutoSize = true;
            this.label11.Location = new System.Drawing.Point(504, 95);
            this.label11.Name = "label11";
            this.label11.Size = new System.Drawing.Size(49, 13);
            this.label11.TabIndex = 16;
            this.label11.Text = "Setpoint:";
            // 
            // btnSet
            // 
            this.btnSet.Location = new System.Drawing.Point(677, 89);
            this.btnSet.Name = "btnSet";
            this.btnSet.Size = new System.Drawing.Size(75, 23);
            this.btnSet.TabIndex = 17;
            this.btnSet.Text = "Set";
            this.btnSet.UseVisualStyleBackColor = true;
            this.btnSet.Click += new System.EventHandler(this.btnSet_Click);
            // 
            // btn_Tune
            // 
            this.btn_Tune.Location = new System.Drawing.Point(591, 167);
            this.btn_Tune.Name = "btn_Tune";
            this.btn_Tune.Size = new System.Drawing.Size(75, 23);
            this.btn_Tune.TabIndex = 18;
            this.btn_Tune.Text = "Auto-Tune";
            this.btn_Tune.UseVisualStyleBackColor = true;
            this.btn_Tune.Click += new System.EventHandler(this.btn_Tune_Click);
            // 
            // lblOvershoot
            // 
            this.lblOvershoot.AutoSize = true;
            this.lblOvershoot.Location = new System.Drawing.Point(511, 207);
            this.lblOvershoot.Name = "lblOvershoot";
            this.lblOvershoot.Size = new System.Drawing.Size(56, 13);
            this.lblOvershoot.TabIndex = 19;
            this.lblOvershoot.Text = "Overshoot";
            // 
            // lblSettlingTime
            // 
            this.lblSettlingTime.AutoSize = true;
            this.lblSettlingTime.Location = new System.Drawing.Point(514, 235);
            this.lblSettlingTime.Name = "lblSettlingTime";
            this.lblSettlingTime.Size = new System.Drawing.Size(43, 13);
            this.lblSettlingTime.TabIndex = 20;
            this.lblSettlingTime.Text = "Xác lập";
            // 
            // lblError
            // 
            this.lblError.AutoSize = true;
            this.lblError.Location = new System.Drawing.Point(656, 217);
            this.lblError.Name = "lblError";
            this.lblError.Size = new System.Drawing.Size(36, 13);
            this.lblError.TabIndex = 21;
            this.lblError.Text = "Sai số";
            // 
            // Form1
            // 
            this.ClientSize = new System.Drawing.Size(808, 529);
            this.Controls.Add(this.lblError);
            this.Controls.Add(this.lblSettlingTime);
            this.Controls.Add(this.lblOvershoot);
            this.Controls.Add(this.btn_Tune);
            this.Controls.Add(this.btnSet);
            this.Controls.Add(this.label11);
            this.Controls.Add(this.txtSet);
            this.Controls.Add(this.lbl_DateAndTime);
            this.Controls.Add(this.chart2);
            this.Controls.Add(this.comboBoxPorts);
            this.Controls.Add(this.comboBoxBaud);
            this.Controls.Add(this.btnOpen);
            this.Controls.Add(this.txtReceived);
            this.Controls.Add(this.btnRefresh);
            this.Name = "Form1";
            this.Text = "UART WinForms";
            this.Load += new System.EventHandler(this.Form1_Load);
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
        private System.Windows.Forms.TextBox txtReceived;
        private System.Windows.Forms.Button btnRefresh;
        private System.Windows.Forms.DataVisualization.Charting.Chart chart2;
        private Label lbl_DateAndTime;
        private TextBox txtSet;
        private Label label11;
        private Button btnSet;
        private Button btn_Tune;
        private Label lblOvershoot;
        private Label lblSettlingTime;
        private Label lblError;
    }
}

