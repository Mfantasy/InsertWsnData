namespace 气象站数据补充
{
    partial class Form1
    {
        /// <summary>
        /// 必需的设计器变量。
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// 清理所有正在使用的资源。
        /// </summary>
        /// <param name="disposing">如果应释放托管资源，为 true；否则为 false。</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows 窗体设计器生成的代码

        /// <summary>
        /// 设计器支持所需的方法 - 不要修改
        /// 使用代码编辑器修改此方法的内容。
        /// </summary>
        private void InitializeComponent()
        {
            this.label5 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.dateTimePicker2 = new System.Windows.Forms.DateTimePicker();
            this.label1 = new System.Windows.Forms.Label();
            this.dateTimePicker1 = new System.Windows.Forms.DateTimePicker();
            this.timeInterval = new System.Windows.Forms.TextBox();
            this.label3 = new System.Windows.Forms.Label();
            this.textBoxRain = new System.Windows.Forms.TextBox();
            this.label4 = new System.Windows.Forms.Label();
            this.textBoxWindSpeed = new System.Windows.Forms.TextBox();
            this.label6 = new System.Windows.Forms.Label();
            this.textBoxWindDirection = new System.Windows.Forms.TextBox();
            this.label7 = new System.Windows.Forms.Label();
            this.textBoxAirTemp = new System.Windows.Forms.TextBox();
            this.label8 = new System.Windows.Forms.Label();
            this.textBoxAirHumid = new System.Windows.Forms.TextBox();
            this.button1 = new System.Windows.Forms.Button();
            this.label9 = new System.Windows.Forms.Label();
            this.fAirHumid = new System.Windows.Forms.TextBox();
            this.fAirTemp = new System.Windows.Forms.TextBox();
            this.fWindDirection = new System.Windows.Forms.TextBox();
            this.fWindSpeed = new System.Windows.Forms.TextBox();
            this.fRain = new System.Windows.Forms.TextBox();
            this.label10 = new System.Windows.Forms.Label();
            this.label11 = new System.Windows.Forms.Label();
            this.label12 = new System.Windows.Forms.Label();
            this.SuspendLayout();
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(12, 112);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(77, 12);
            this.label5.TabIndex = 20;
            this.label5.Text = "时间间隔(秒)";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(12, 60);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(53, 12);
            this.label2.TabIndex = 19;
            this.label2.Text = "结束日期";
            // 
            // dateTimePicker2
            // 
            this.dateTimePicker2.CustomFormat = "yyyy/MM/dd HH:mm:ss";
            this.dateTimePicker2.Format = System.Windows.Forms.DateTimePickerFormat.Custom;
            this.dateTimePicker2.Location = new System.Drawing.Point(14, 75);
            this.dateTimePicker2.Name = "dateTimePicker2";
            this.dateTimePicker2.Size = new System.Drawing.Size(200, 21);
            this.dateTimePicker2.TabIndex = 18;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(12, 9);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(53, 12);
            this.label1.TabIndex = 17;
            this.label1.Text = "开始日期";
            // 
            // dateTimePicker1
            // 
            this.dateTimePicker1.CustomFormat = "yyyy/MM/dd HH:mm:ss";
            this.dateTimePicker1.Format = System.Windows.Forms.DateTimePickerFormat.Custom;
            this.dateTimePicker1.Location = new System.Drawing.Point(14, 24);
            this.dateTimePicker1.Name = "dateTimePicker1";
            this.dateTimePicker1.Size = new System.Drawing.Size(200, 21);
            this.dateTimePicker1.TabIndex = 16;
            // 
            // timeInterval
            // 
            this.timeInterval.Location = new System.Drawing.Point(114, 109);
            this.timeInterval.Name = "timeInterval";
            this.timeInterval.Size = new System.Drawing.Size(100, 21);
            this.timeInterval.TabIndex = 15;
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(297, 30);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(29, 12);
            this.label3.TabIndex = 22;
            this.label3.Text = "雨量";
            // 
            // textBoxRain
            // 
            this.textBoxRain.Location = new System.Drawing.Point(521, 27);
            this.textBoxRain.Name = "textBoxRain";
            this.textBoxRain.Size = new System.Drawing.Size(100, 21);
            this.textBoxRain.TabIndex = 21;
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(297, 57);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(29, 12);
            this.label4.TabIndex = 24;
            this.label4.Text = "风速";
            // 
            // textBoxWindSpeed
            // 
            this.textBoxWindSpeed.Location = new System.Drawing.Point(521, 54);
            this.textBoxWindSpeed.Name = "textBoxWindSpeed";
            this.textBoxWindSpeed.Size = new System.Drawing.Size(100, 21);
            this.textBoxWindSpeed.TabIndex = 23;
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Location = new System.Drawing.Point(297, 84);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(29, 12);
            this.label6.TabIndex = 26;
            this.label6.Text = "风向";
            // 
            // textBoxWindDirection
            // 
            this.textBoxWindDirection.Location = new System.Drawing.Point(521, 81);
            this.textBoxWindDirection.Name = "textBoxWindDirection";
            this.textBoxWindDirection.Size = new System.Drawing.Size(100, 21);
            this.textBoxWindDirection.TabIndex = 25;
            // 
            // label7
            // 
            this.label7.AutoSize = true;
            this.label7.Location = new System.Drawing.Point(297, 111);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(53, 12);
            this.label7.TabIndex = 28;
            this.label7.Text = "空气温度";
            // 
            // textBoxAirTemp
            // 
            this.textBoxAirTemp.Location = new System.Drawing.Point(521, 108);
            this.textBoxAirTemp.Name = "textBoxAirTemp";
            this.textBoxAirTemp.Size = new System.Drawing.Size(100, 21);
            this.textBoxAirTemp.TabIndex = 27;
            // 
            // label8
            // 
            this.label8.AutoSize = true;
            this.label8.Location = new System.Drawing.Point(297, 138);
            this.label8.Name = "label8";
            this.label8.Size = new System.Drawing.Size(53, 12);
            this.label8.TabIndex = 30;
            this.label8.Text = "空气湿度";
            // 
            // textBoxAirHumid
            // 
            this.textBoxAirHumid.Location = new System.Drawing.Point(521, 135);
            this.textBoxAirHumid.Name = "textBoxAirHumid";
            this.textBoxAirHumid.Size = new System.Drawing.Size(100, 21);
            this.textBoxAirHumid.TabIndex = 29;
            // 
            // button1
            // 
            this.button1.Location = new System.Drawing.Point(14, 163);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(75, 23);
            this.button1.TabIndex = 31;
            this.button1.Text = "插入数据";
            this.button1.UseVisualStyleBackColor = true;
            this.button1.Click += new System.EventHandler(this.button1_Click);
            // 
            // label9
            // 
            this.label9.AutoSize = true;
            this.label9.Location = new System.Drawing.Point(112, 168);
            this.label9.Name = "label9";
            this.label9.Size = new System.Drawing.Size(59, 12);
            this.label9.TabIndex = 32;
            this.label9.Text = "执行状态:";
            // 
            // fAirHumid
            // 
            this.fAirHumid.Location = new System.Drawing.Point(400, 135);
            this.fAirHumid.Name = "fAirHumid";
            this.fAirHumid.Size = new System.Drawing.Size(100, 21);
            this.fAirHumid.TabIndex = 37;
            // 
            // fAirTemp
            // 
            this.fAirTemp.Location = new System.Drawing.Point(400, 108);
            this.fAirTemp.Name = "fAirTemp";
            this.fAirTemp.Size = new System.Drawing.Size(100, 21);
            this.fAirTemp.TabIndex = 36;
            // 
            // fWindDirection
            // 
            this.fWindDirection.Location = new System.Drawing.Point(400, 81);
            this.fWindDirection.Name = "fWindDirection";
            this.fWindDirection.Size = new System.Drawing.Size(100, 21);
            this.fWindDirection.TabIndex = 35;
            // 
            // fWindSpeed
            // 
            this.fWindSpeed.Location = new System.Drawing.Point(400, 54);
            this.fWindSpeed.Name = "fWindSpeed";
            this.fWindSpeed.Size = new System.Drawing.Size(100, 21);
            this.fWindSpeed.TabIndex = 34;
            // 
            // fRain
            // 
            this.fRain.Location = new System.Drawing.Point(400, 27);
            this.fRain.Name = "fRain";
            this.fRain.Size = new System.Drawing.Size(100, 21);
            this.fRain.TabIndex = 33;
            // 
            // label10
            // 
            this.label10.AutoSize = true;
            this.label10.Location = new System.Drawing.Point(424, 9);
            this.label10.Name = "label10";
            this.label10.Size = new System.Drawing.Size(47, 12);
            this.label10.TabIndex = 38;
            this.label10.Text = "feed_id";
            // 
            // label11
            // 
            this.label11.AutoSize = true;
            this.label11.Location = new System.Drawing.Point(551, 9);
            this.label11.Name = "label11";
            this.label11.Size = new System.Drawing.Size(35, 12);
            this.label11.TabIndex = 39;
            this.label11.Text = "value";
            // 
            // label12
            // 
            this.label12.AutoSize = true;
            this.label12.Location = new System.Drawing.Point(398, 194);
            this.label12.Name = "label12";
            this.label12.Size = new System.Drawing.Size(251, 12);
            this.label12.TabIndex = 40;
            this.label12.Text = "风向风速为整数,雨量空气温湿度保留一位小数";
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(668, 227);
            this.Controls.Add(this.label12);
            this.Controls.Add(this.label11);
            this.Controls.Add(this.label10);
            this.Controls.Add(this.fAirHumid);
            this.Controls.Add(this.fAirTemp);
            this.Controls.Add(this.fWindDirection);
            this.Controls.Add(this.fWindSpeed);
            this.Controls.Add(this.fRain);
            this.Controls.Add(this.label9);
            this.Controls.Add(this.button1);
            this.Controls.Add(this.label8);
            this.Controls.Add(this.textBoxAirHumid);
            this.Controls.Add(this.label7);
            this.Controls.Add(this.textBoxAirTemp);
            this.Controls.Add(this.label6);
            this.Controls.Add(this.textBoxWindDirection);
            this.Controls.Add(this.label4);
            this.Controls.Add(this.textBoxWindSpeed);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.textBoxRain);
            this.Controls.Add(this.label5);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.dateTimePicker2);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.dateTimePicker1);
            this.Controls.Add(this.timeInterval);
            this.Name = "Form1";
            this.Text = "Form1";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.DateTimePicker dateTimePicker2;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.DateTimePicker dateTimePicker1;
        private System.Windows.Forms.TextBox timeInterval;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.TextBox textBoxRain;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.TextBox textBoxWindSpeed;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.TextBox textBoxWindDirection;
        private System.Windows.Forms.Label label7;
        private System.Windows.Forms.TextBox textBoxAirTemp;
        private System.Windows.Forms.Label label8;
        private System.Windows.Forms.TextBox textBoxAirHumid;
        private System.Windows.Forms.Button button1;
        private System.Windows.Forms.Label label9;
        private System.Windows.Forms.TextBox fAirHumid;
        private System.Windows.Forms.TextBox fAirTemp;
        private System.Windows.Forms.TextBox fWindDirection;
        private System.Windows.Forms.TextBox fWindSpeed;
        private System.Windows.Forms.TextBox fRain;
        private System.Windows.Forms.Label label10;
        private System.Windows.Forms.Label label11;
        private System.Windows.Forms.Label label12;
    }
}

