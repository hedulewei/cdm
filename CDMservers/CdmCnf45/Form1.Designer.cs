namespace CdmCnf45
{
    partial class FormVoice
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
            this.richTextBox1 = new System.Windows.Forms.RichTextBox();
            this.buttonlogin = new System.Windows.Forms.Button();
            this.textBoxserver = new System.Windows.Forms.TextBox();
            this.label1 = new System.Windows.Forms.Label();
            this.groupBoxvoiceconfig = new System.Windows.Forms.GroupBox();
            this.label4 = new System.Windows.Forms.Label();
            this.textBoxcounty = new System.Windows.Forms.TextBox();
            this.buttonrejecttest = new System.Windows.Forms.Button();
            this.buttonfeetest = new System.Windows.Forms.Button();
            this.label3 = new System.Windows.Forms.Label();
            this.textBoxreject = new System.Windows.Forms.TextBox();
            this.label2 = new System.Windows.Forms.Label();
            this.textBoxfeevoice = new System.Windows.Forms.TextBox();
            this.groupBoxvoiceconfig.SuspendLayout();
            this.SuspendLayout();
            // 
            // richTextBox1
            // 
            this.richTextBox1.Location = new System.Drawing.Point(12, 271);
            this.richTextBox1.Name = "richTextBox1";
            this.richTextBox1.Size = new System.Drawing.Size(816, 214);
            this.richTextBox1.TabIndex = 0;
            this.richTextBox1.Text = "";
            // 
            // buttonlogin
            // 
            this.buttonlogin.Location = new System.Drawing.Point(429, 10);
            this.buttonlogin.Name = "buttonlogin";
            this.buttonlogin.Size = new System.Drawing.Size(120, 23);
            this.buttonlogin.TabIndex = 1;
            this.buttonlogin.Text = "some";
            this.buttonlogin.UseVisualStyleBackColor = true;
            this.buttonlogin.Click += new System.EventHandler(this.button1_Click);
            // 
            // textBoxserver
            // 
            this.textBoxserver.Location = new System.Drawing.Point(103, 12);
            this.textBoxserver.Name = "textBoxserver";
            this.textBoxserver.Size = new System.Drawing.Size(169, 21);
            this.textBoxserver.TabIndex = 2;
            this.textBoxserver.TextChanged += new System.EventHandler(this.textBoxserver_TextChanged);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(38, 15);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(41, 12);
            this.label1.TabIndex = 3;
            this.label1.Text = "server";
            // 
            // groupBoxvoiceconfig
            // 
            this.groupBoxvoiceconfig.Controls.Add(this.label4);
            this.groupBoxvoiceconfig.Controls.Add(this.textBoxcounty);
            this.groupBoxvoiceconfig.Controls.Add(this.buttonrejecttest);
            this.groupBoxvoiceconfig.Controls.Add(this.buttonfeetest);
            this.groupBoxvoiceconfig.Controls.Add(this.label3);
            this.groupBoxvoiceconfig.Controls.Add(this.textBoxreject);
            this.groupBoxvoiceconfig.Controls.Add(this.label2);
            this.groupBoxvoiceconfig.Controls.Add(this.textBoxfeevoice);
            this.groupBoxvoiceconfig.Location = new System.Drawing.Point(15, 57);
            this.groupBoxvoiceconfig.Name = "groupBoxvoiceconfig";
            this.groupBoxvoiceconfig.Size = new System.Drawing.Size(813, 151);
            this.groupBoxvoiceconfig.TabIndex = 4;
            this.groupBoxvoiceconfig.TabStop = false;
            this.groupBoxvoiceconfig.Text = "voice config";
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(23, 86);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(41, 12);
            this.label4.TabIndex = 10;
            this.label4.Text = "county";
            // 
            // textBoxcounty
            // 
            this.textBoxcounty.Location = new System.Drawing.Point(88, 83);
            this.textBoxcounty.Name = "textBoxcounty";
            this.textBoxcounty.Size = new System.Drawing.Size(238, 21);
            this.textBoxcounty.TabIndex = 9;
            this.textBoxcounty.TextChanged += new System.EventHandler(this.textBoxcounty_TextChanged);
            // 
            // buttonrejecttest
            // 
            this.buttonrejecttest.Location = new System.Drawing.Point(559, 98);
            this.buttonrejecttest.Name = "buttonrejecttest";
            this.buttonrejecttest.Size = new System.Drawing.Size(120, 23);
            this.buttonrejecttest.TabIndex = 8;
            this.buttonrejecttest.Text = "reject test";
            this.buttonrejecttest.UseVisualStyleBackColor = true;
            this.buttonrejecttest.Click += new System.EventHandler(this.buttonrejecttest_Click);
            // 
            // buttonfeetest
            // 
            this.buttonfeetest.Location = new System.Drawing.Point(414, 98);
            this.buttonfeetest.Name = "buttonfeetest";
            this.buttonfeetest.Size = new System.Drawing.Size(120, 23);
            this.buttonfeetest.TabIndex = 5;
            this.buttonfeetest.Text = "fee test";
            this.buttonfeetest.UseVisualStyleBackColor = true;
            this.buttonfeetest.Click += new System.EventHandler(this.buttonfeetest_Click);
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
            this.label2.Size = new System.Drawing.Size(23, 12);
            this.label2.TabIndex = 5;
            this.label2.Text = "fee";
            // 
            // textBoxfeevoice
            // 
            this.textBoxfeevoice.Location = new System.Drawing.Point(88, 29);
            this.textBoxfeevoice.Name = "textBoxfeevoice";
            this.textBoxfeevoice.Size = new System.Drawing.Size(591, 21);
            this.textBoxfeevoice.TabIndex = 4;
            this.textBoxfeevoice.TextChanged += new System.EventHandler(this.textBoxfeevoice_TextChanged);
            // 
            // FormVoice
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(839, 497);
            this.Controls.Add(this.groupBoxvoiceconfig);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.textBoxserver);
            this.Controls.Add(this.buttonlogin);
            this.Controls.Add(this.richTextBox1);
            this.MaximizeBox = false;
            this.Name = "FormVoice";
            this.Opacity = 0.8D;
            this.Text = "CdmVoice";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.FormVoice_FormClosing);
            this.Load += new System.EventHandler(this.Form1_Load);
            this.groupBoxvoiceconfig.ResumeLayout(false);
            this.groupBoxvoiceconfig.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.RichTextBox richTextBox1;
        private System.Windows.Forms.Button buttonlogin;
        private System.Windows.Forms.TextBox textBoxserver;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.GroupBox groupBoxvoiceconfig;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.TextBox textBoxreject;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.TextBox textBoxfeevoice;
        private System.Windows.Forms.Button buttonfeetest;
        private System.Windows.Forms.Button buttonrejecttest;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.TextBox textBoxcounty;
    }
}

