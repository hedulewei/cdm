namespace CdmLedC
{
    partial class FormLedC
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
        /// 设计器支持所需的方法 - 不要
        /// 使用代码编辑器修改此方法的内容。
        /// </summary>
        private void InitializeComponent()
        {
            this.richTextBoxLog = new System.Windows.Forms.RichTextBox();
            this.groupBoxvoiceconfig = new System.Windows.Forms.GroupBox();
            this.label5 = new System.Windows.Forms.Label();
            this.textBoxcounty = new System.Windows.Forms.TextBox();
            this.label4 = new System.Windows.Forms.Label();
            this.textBoxdone = new System.Windows.Forms.TextBox();
            this.buttonrejecttest = new System.Windows.Forms.Button();
            this.buttondonetest = new System.Windows.Forms.Button();
            this.label3 = new System.Windows.Forms.Label();
            this.textBoxreject = new System.Windows.Forms.TextBox();
            this.label2 = new System.Windows.Forms.Label();
            this.textBoxprocessing = new System.Windows.Forms.TextBox();
            this.label1 = new System.Windows.Forms.Label();
            this.textBoxserver = new System.Windows.Forms.TextBox();
            this.buttonlogin = new System.Windows.Forms.Button();
            this.buttonsometest = new System.Windows.Forms.Button();
            this.textBoxpass = new System.Windows.Forms.TextBox();
            this.groupBoxvoiceconfig.SuspendLayout();
            this.SuspendLayout();
            // 
            // richTextBoxLog
            // 
            this.richTextBoxLog.Location = new System.Drawing.Point(12, 283);
            this.richTextBoxLog.Name = "richTextBoxLog";
            this.richTextBoxLog.Size = new System.Drawing.Size(706, 178);
            this.richTextBoxLog.TabIndex = 0;
            this.richTextBoxLog.Text = "";
            // 
            // groupBoxvoiceconfig
            // 
            this.groupBoxvoiceconfig.Controls.Add(this.label5);
            this.groupBoxvoiceconfig.Controls.Add(this.textBoxcounty);
            this.groupBoxvoiceconfig.Controls.Add(this.label4);
            this.groupBoxvoiceconfig.Controls.Add(this.textBoxdone);
            this.groupBoxvoiceconfig.Controls.Add(this.buttonrejecttest);
            this.groupBoxvoiceconfig.Controls.Add(this.buttondonetest);
            this.groupBoxvoiceconfig.Controls.Add(this.label3);
            this.groupBoxvoiceconfig.Controls.Add(this.textBoxreject);
            this.groupBoxvoiceconfig.Controls.Add(this.label2);
            this.groupBoxvoiceconfig.Controls.Add(this.textBoxprocessing);
            this.groupBoxvoiceconfig.Location = new System.Drawing.Point(15, 51);
            this.groupBoxvoiceconfig.Name = "groupBoxvoiceconfig";
            this.groupBoxvoiceconfig.Size = new System.Drawing.Size(703, 187);
            this.groupBoxvoiceconfig.TabIndex = 8;
            this.groupBoxvoiceconfig.TabStop = false;
            this.groupBoxvoiceconfig.Text = "voice config";
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(23, 113);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(41, 12);
            this.label5.TabIndex = 10;
            this.label5.Text = "county";
            // 
            // textBoxcounty
            // 
            this.textBoxcounty.Location = new System.Drawing.Point(88, 110);
            this.textBoxcounty.Name = "textBoxcounty";
            this.textBoxcounty.Size = new System.Drawing.Size(238, 21);
            this.textBoxcounty.TabIndex = 9;
            this.textBoxcounty.TextChanged += new System.EventHandler(this.textBoxcounty_TextChanged);
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(23, 86);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(29, 12);
            this.label4.TabIndex = 10;
            this.label4.Text = "done";
            // 
            // textBoxdone
            // 
            this.textBoxdone.Location = new System.Drawing.Point(88, 83);
            this.textBoxdone.Name = "textBoxdone";
            this.textBoxdone.Size = new System.Drawing.Size(591, 21);
            this.textBoxdone.TabIndex = 9;
            this.textBoxdone.TextChanged += new System.EventHandler(this.textBoxdone_TextChanged);
            // 
            // buttonrejecttest
            // 
            this.buttonrejecttest.Location = new System.Drawing.Point(559, 149);
            this.buttonrejecttest.Name = "buttonrejecttest";
            this.buttonrejecttest.Size = new System.Drawing.Size(120, 23);
            this.buttonrejecttest.TabIndex = 8;
            this.buttonrejecttest.Text = "reject test";
            this.buttonrejecttest.UseVisualStyleBackColor = true;
            this.buttonrejecttest.Click += new System.EventHandler(this.buttonrejecttest_Click);
            // 
            // buttondonetest
            // 
            this.buttondonetest.Location = new System.Drawing.Point(414, 149);
            this.buttondonetest.Name = "buttondonetest";
            this.buttondonetest.Size = new System.Drawing.Size(120, 23);
            this.buttondonetest.TabIndex = 5;
            this.buttondonetest.Text = "done test";
            this.buttondonetest.UseVisualStyleBackColor = true;
            this.buttondonetest.Click += new System.EventHandler(this.buttondonetest_Click);
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(23, 59);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(41, 12);
            this.label3.TabIndex = 7;
            this.label3.Text = "reject";
            // 
            // textBoxreject
            // 
            this.textBoxreject.Location = new System.Drawing.Point(88, 56);
            this.textBoxreject.Name = "textBoxreject";
            this.textBoxreject.Size = new System.Drawing.Size(591, 21);
            this.textBoxreject.TabIndex = 6;
            this.textBoxreject.TextChanged += new System.EventHandler(this.textBoxreject_TextChanged);
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(23, 32);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(65, 12);
            this.label2.TabIndex = 5;
            this.label2.Text = "processing";
            // 
            // textBoxprocessing
            // 
            this.textBoxprocessing.Location = new System.Drawing.Point(88, 29);
            this.textBoxprocessing.Name = "textBoxprocessing";
            this.textBoxprocessing.Size = new System.Drawing.Size(591, 21);
            this.textBoxprocessing.TabIndex = 4;
            this.textBoxprocessing.TextChanged += new System.EventHandler(this.textBoxprocessing_TextChanged);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(38, 9);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(41, 12);
            this.label1.TabIndex = 7;
            this.label1.Text = "server";
            // 
            // textBoxserver
            // 
            this.textBoxserver.Location = new System.Drawing.Point(103, 6);
            this.textBoxserver.Name = "textBoxserver";
            this.textBoxserver.Size = new System.Drawing.Size(169, 21);
            this.textBoxserver.TabIndex = 6;
            this.textBoxserver.TextChanged += new System.EventHandler(this.textBoxserver_TextChanged);
            // 
            // buttonlogin
            // 
            this.buttonlogin.Location = new System.Drawing.Point(429, 4);
            this.buttonlogin.Name = "buttonlogin";
            this.buttonlogin.Size = new System.Drawing.Size(120, 23);
            this.buttonlogin.TabIndex = 5;
            this.buttonlogin.Text = "login server";
            this.buttonlogin.UseVisualStyleBackColor = true;
            this.buttonlogin.Click += new System.EventHandler(this.buttonlogin_Click);
            // 
            // buttonsometest
            // 
            this.buttonsometest.Location = new System.Drawing.Point(429, 244);
            this.buttonsometest.Name = "buttonsometest";
            this.buttonsometest.Size = new System.Drawing.Size(120, 23);
            this.buttonsometest.TabIndex = 11;
            this.buttonsometest.Text = "test";
            this.buttonsometest.UseVisualStyleBackColor = true;
            this.buttonsometest.Click += new System.EventHandler(this.buttonsometest_Click);
            // 
            // textBoxpass
            // 
            this.textBoxpass.Location = new System.Drawing.Point(103, 244);
            this.textBoxpass.Name = "textBoxpass";
            this.textBoxpass.Size = new System.Drawing.Size(238, 21);
            this.textBoxpass.TabIndex = 11;
            // 
            // FormLedC
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(731, 473);
            this.Controls.Add(this.textBoxpass);
            this.Controls.Add(this.buttonsometest);
            this.Controls.Add(this.groupBoxvoiceconfig);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.textBoxserver);
            this.Controls.Add(this.buttonlogin);
            this.Controls.Add(this.richTextBoxLog);
            this.Name = "FormLedC";
            this.Text = "Form Led Message";
            this.Load += new System.EventHandler(this.Form1_Load);
            this.groupBoxvoiceconfig.ResumeLayout(false);
            this.groupBoxvoiceconfig.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.RichTextBox richTextBoxLog;
        private System.Windows.Forms.GroupBox groupBoxvoiceconfig;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.TextBox textBoxdone;
        private System.Windows.Forms.Button buttonrejecttest;
        private System.Windows.Forms.Button buttondonetest;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.TextBox textBoxreject;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.TextBox textBoxprocessing;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.TextBox textBoxserver;
        private System.Windows.Forms.Button buttonlogin;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.TextBox textBoxcounty;
        private System.Windows.Forms.Button buttonsometest;
        private System.Windows.Forms.TextBox textBoxpass;
    }
}

