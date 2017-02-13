namespace InsertWsnData
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
            this.button1 = new System.Windows.Forms.Button();
            this.MIND = new System.Windows.Forms.TextBox();
            this.MAXD = new System.Windows.Forms.TextBox();
            this.FI = new System.Windows.Forms.TextBox();
            this.MIN = new System.Windows.Forms.TextBox();
            this.MAX = new System.Windows.Forms.TextBox();
            this.IV = new System.Windows.Forms.TextBox();
            this.textBox1 = new System.Windows.Forms.TextBox();
            this.dateTimePicker1 = new System.Windows.Forms.DateTimePicker();
            this.label1 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.dateTimePicker2 = new System.Windows.Forms.DateTimePicker();
            this.label3 = new System.Windows.Forms.Label();
            this.label4 = new System.Windows.Forms.Label();
            this.label5 = new System.Windows.Forms.Label();
            this.SuspendLayout();
            // 
            // button1
            // 
            this.button1.Location = new System.Drawing.Point(50, 78);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(75, 23);
            this.button1.TabIndex = 0;
            this.button1.Text = "插入数据";
            this.button1.UseVisualStyleBackColor = true;
            this.button1.Click += new System.EventHandler(this.button1_Click);
            // 
            // MIND
            // 
            this.MIND.Enabled = false;
            this.MIND.Location = new System.Drawing.Point(578, 56);
            this.MIND.Name = "MIND";
            this.MIND.Size = new System.Drawing.Size(100, 21);
            this.MIND.TabIndex = 1;
            this.MIND.Text = "开始日期";
            // 
            // MAXD
            // 
            this.MAXD.Enabled = false;
            this.MAXD.Location = new System.Drawing.Point(578, 29);
            this.MAXD.Name = "MAXD";
            this.MAXD.Size = new System.Drawing.Size(100, 21);
            this.MAXD.TabIndex = 2;
            this.MAXD.Text = "结束日期";
            // 
            // FI
            // 
            this.FI.Location = new System.Drawing.Point(346, 134);
            this.FI.Name = "FI";
            this.FI.Size = new System.Drawing.Size(100, 21);
            this.FI.TabIndex = 3;
            // 
            // MIN
            // 
            this.MIN.Enabled = false;
            this.MIN.Location = new System.Drawing.Point(578, 84);
            this.MIN.Name = "MIN";
            this.MIN.Size = new System.Drawing.Size(100, 21);
            this.MIN.TabIndex = 4;
            this.MIN.Text = "最小值";
            // 
            // MAX
            // 
            this.MAX.Enabled = false;
            this.MAX.Location = new System.Drawing.Point(578, 110);
            this.MAX.Name = "MAX";
            this.MAX.Size = new System.Drawing.Size(100, 21);
            this.MAX.TabIndex = 5;
            this.MAX.Text = "最大值";
            // 
            // IV
            // 
            this.IV.Location = new System.Drawing.Point(346, 209);
            this.IV.Name = "IV";
            this.IV.Size = new System.Drawing.Size(100, 21);
            this.IV.TabIndex = 6;
            // 
            // textBox1
            // 
            this.textBox1.Location = new System.Drawing.Point(346, 171);
            this.textBox1.Name = "textBox1";
            this.textBox1.Size = new System.Drawing.Size(100, 21);
            this.textBox1.TabIndex = 7;
            // 
            // dateTimePicker1
            // 
            this.dateTimePicker1.CustomFormat = "yyyy/MM/dd HH:mm:ss";
            this.dateTimePicker1.Format = System.Windows.Forms.DateTimePickerFormat.Custom;
            this.dateTimePicker1.Location = new System.Drawing.Point(246, 53);
            this.dateTimePicker1.Name = "dateTimePicker1";
            this.dateTimePicker1.Size = new System.Drawing.Size(200, 21);
            this.dateTimePicker1.TabIndex = 8;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(244, 38);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(53, 12);
            this.label1.TabIndex = 9;
            this.label1.Text = "开始日期";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(244, 89);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(53, 12);
            this.label2.TabIndex = 11;
            this.label2.Text = "结束日期";
            // 
            // dateTimePicker2
            // 
            this.dateTimePicker2.CustomFormat = "yyyy/MM/dd HH:mm:ss";
            this.dateTimePicker2.Format = System.Windows.Forms.DateTimePickerFormat.Custom;
            this.dateTimePicker2.Location = new System.Drawing.Point(246, 104);
            this.dateTimePicker2.Name = "dateTimePicker2";
            this.dateTimePicker2.Size = new System.Drawing.Size(200, 21);
            this.dateTimePicker2.TabIndex = 10;
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(261, 137);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(47, 12);
            this.label3.TabIndex = 12;
            this.label3.Text = "feed_id";
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(261, 174);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(41, 12);
            this.label4.TabIndex = 13;
            this.label4.Text = "数据值";
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(231, 212);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(77, 12);
            this.label5.TabIndex = 14;
            this.label5.Text = "时间间隔(分)";
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(708, 434);
            this.Controls.Add(this.label5);
            this.Controls.Add(this.label4);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.dateTimePicker2);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.dateTimePicker1);
            this.Controls.Add(this.textBox1);
            this.Controls.Add(this.IV);
            this.Controls.Add(this.MAX);
            this.Controls.Add(this.MIN);
            this.Controls.Add(this.FI);
            this.Controls.Add(this.MAXD);
            this.Controls.Add(this.MIND);
            this.Controls.Add(this.button1);
            this.Name = "Form1";
            this.Text = "InsertData";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button button1;
        private System.Windows.Forms.TextBox MIND;
        private System.Windows.Forms.TextBox MAXD;
        private System.Windows.Forms.TextBox FI;
        private System.Windows.Forms.TextBox MIN;
        private System.Windows.Forms.TextBox MAX;
        private System.Windows.Forms.TextBox IV;
        private System.Windows.Forms.TextBox textBox1;
        private System.Windows.Forms.DateTimePicker dateTimePicker1;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.DateTimePicker dateTimePicker2;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Label label5;
    }
}

