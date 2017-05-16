namespace face
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
        /// 设计器支持所需的方法 - 不要
        /// 使用代码编辑器修改此方法的内容。
        /// </summary>
        private void InitializeComponent()
        {
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(Form1));
            this.textBoxpicturesource = new System.Windows.Forms.TextBox();
            this.label1 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.textBoxcurrentimage = new System.Windows.Forms.TextBox();
            this.buttoncompare = new System.Windows.Forms.Button();
            this.richTextBox1 = new System.Windows.Forms.RichTextBox();
            this.pictureBoxsource = new System.Windows.Forms.PictureBox();
            this.pictureBoxcurrentimage = new System.Windows.Forms.PictureBox();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.comboBoxcameras = new System.Windows.Forms.ComboBox();
            this.buttoncameracapture = new System.Windows.Forms.Button();
            this.buttoncameraclose = new System.Windows.Forms.Button();
            this.buttoncamera = new System.Windows.Forms.Button();
            this.videoSourcePlayer = new AForge.Controls.VideoSourcePlayer();
            this.buttonchecksource = new System.Windows.Forms.Button();
            this.buttontrain = new System.Windows.Forms.Button();
            this.clear = new System.Windows.Forms.Button();
            this.axWindowsMediaPlayer1 = new AxWMPLib.AxWindowsMediaPlayer();
            this.menuStrip1 = new System.Windows.Forms.MenuStrip();
            this.videoToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.openToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.openToolStripMenuItem1 = new System.Windows.Forms.ToolStripMenuItem();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBoxsource)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBoxcurrentimage)).BeginInit();
            this.groupBox1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.axWindowsMediaPlayer1)).BeginInit();
            this.menuStrip1.SuspendLayout();
            this.SuspendLayout();
            // 
            // textBoxpicturesource
            // 
            this.textBoxpicturesource.Location = new System.Drawing.Point(180, 48);
            this.textBoxpicturesource.Name = "textBoxpicturesource";
            this.textBoxpicturesource.Size = new System.Drawing.Size(342, 21);
            this.textBoxpicturesource.TabIndex = 0;
            this.textBoxpicturesource.Text = "C:\\Emgu\\emgucv-windesktop 3.2.0.2682\\bin\\xiaosy.jpg";
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(115, 51);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(53, 12);
            this.label1.TabIndex = 1;
            this.label1.Text = "picture1";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(115, 78);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(53, 12);
            this.label2.TabIndex = 3;
            this.label2.Text = "picture2";
            // 
            // textBoxcurrentimage
            // 
            this.textBoxcurrentimage.Location = new System.Drawing.Point(180, 75);
            this.textBoxcurrentimage.Name = "textBoxcurrentimage";
            this.textBoxcurrentimage.Size = new System.Drawing.Size(342, 21);
            this.textBoxcurrentimage.TabIndex = 2;
            this.textBoxcurrentimage.Text = "C:\\Emgu\\emgucv-windesktop 3.2.0.2682\\bin\\xiaosy3.jpg";
            this.textBoxcurrentimage.TextChanged += new System.EventHandler(this.textBoxcurrentimage_TextChanged);
            // 
            // buttoncompare
            // 
            this.buttoncompare.Location = new System.Drawing.Point(604, 78);
            this.buttoncompare.Name = "buttoncompare";
            this.buttoncompare.Size = new System.Drawing.Size(75, 23);
            this.buttoncompare.TabIndex = 4;
            this.buttoncompare.Text = "check";
            this.buttoncompare.UseVisualStyleBackColor = true;
            this.buttoncompare.Click += new System.EventHandler(this.buttoncompare_Click);
            // 
            // richTextBox1
            // 
            this.richTextBox1.Location = new System.Drawing.Point(12, 526);
            this.richTextBox1.Name = "richTextBox1";
            this.richTextBox1.Size = new System.Drawing.Size(943, 125);
            this.richTextBox1.TabIndex = 5;
            this.richTextBox1.Text = "";
            // 
            // pictureBoxsource
            // 
            this.pictureBoxsource.Location = new System.Drawing.Point(12, 186);
            this.pictureBoxsource.Name = "pictureBoxsource";
            this.pictureBoxsource.Size = new System.Drawing.Size(455, 334);
            this.pictureBoxsource.TabIndex = 6;
            this.pictureBoxsource.TabStop = false;
            // 
            // pictureBoxcurrentimage
            // 
            this.pictureBoxcurrentimage.Location = new System.Drawing.Point(500, 186);
            this.pictureBoxcurrentimage.Name = "pictureBoxcurrentimage";
            this.pictureBoxcurrentimage.Size = new System.Drawing.Size(455, 334);
            this.pictureBoxcurrentimage.TabIndex = 7;
            this.pictureBoxcurrentimage.TabStop = false;
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.comboBoxcameras);
            this.groupBox1.Controls.Add(this.buttoncameracapture);
            this.groupBox1.Controls.Add(this.buttoncameraclose);
            this.groupBox1.Controls.Add(this.buttoncamera);
            this.groupBox1.Location = new System.Drawing.Point(1035, 12);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(277, 182);
            this.groupBox1.TabIndex = 27;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "camera";
            // 
            // comboBoxcameras
            // 
            this.comboBoxcameras.FormattingEnabled = true;
            this.comboBoxcameras.Location = new System.Drawing.Point(39, 118);
            this.comboBoxcameras.Name = "comboBoxcameras";
            this.comboBoxcameras.Size = new System.Drawing.Size(175, 20);
            this.comboBoxcameras.TabIndex = 28;
            // 
            // buttoncameracapture
            // 
            this.buttoncameracapture.Location = new System.Drawing.Point(39, 89);
            this.buttoncameracapture.Name = "buttoncameracapture";
            this.buttoncameracapture.Size = new System.Drawing.Size(136, 23);
            this.buttoncameracapture.TabIndex = 27;
            this.buttoncameracapture.Text = "capture";
            this.buttoncameracapture.UseVisualStyleBackColor = true;
            this.buttoncameracapture.Click += new System.EventHandler(this.buttoncameracapture_Click);
            // 
            // buttoncameraclose
            // 
            this.buttoncameraclose.Location = new System.Drawing.Point(39, 59);
            this.buttoncameraclose.Name = "buttoncameraclose";
            this.buttoncameraclose.Size = new System.Drawing.Size(136, 23);
            this.buttoncameraclose.TabIndex = 26;
            this.buttoncameraclose.Text = "close";
            this.buttoncameraclose.UseVisualStyleBackColor = true;
            this.buttoncameraclose.Click += new System.EventHandler(this.buttoncameraclose_Click);
            // 
            // buttoncamera
            // 
            this.buttoncamera.Location = new System.Drawing.Point(39, 25);
            this.buttoncamera.Name = "buttoncamera";
            this.buttoncamera.Size = new System.Drawing.Size(136, 23);
            this.buttoncamera.TabIndex = 25;
            this.buttoncamera.Text = "connect";
            this.buttoncamera.UseVisualStyleBackColor = true;
            this.buttoncamera.Click += new System.EventHandler(this.buttoncamera_Click);
            // 
            // videoSourcePlayer
            // 
            this.videoSourcePlayer.Location = new System.Drawing.Point(961, 200);
            this.videoSourcePlayer.Name = "videoSourcePlayer";
            this.videoSourcePlayer.Size = new System.Drawing.Size(351, 428);
            this.videoSourcePlayer.TabIndex = 10;
            this.videoSourcePlayer.Text = "videoSourcePlayer";
            this.videoSourcePlayer.VideoSource = null;
            // 
            // buttonchecksource
            // 
            this.buttonchecksource.Location = new System.Drawing.Point(604, 48);
            this.buttonchecksource.Name = "buttonchecksource";
            this.buttonchecksource.Size = new System.Drawing.Size(164, 23);
            this.buttonchecksource.TabIndex = 28;
            this.buttonchecksource.Text = "display source";
            this.buttonchecksource.UseVisualStyleBackColor = true;
            this.buttonchecksource.Click += new System.EventHandler(this.buttonchecksource_Click);
            // 
            // buttontrain
            // 
            this.buttontrain.Location = new System.Drawing.Point(604, 107);
            this.buttontrain.Name = "buttontrain";
            this.buttontrain.Size = new System.Drawing.Size(75, 23);
            this.buttontrain.TabIndex = 29;
            this.buttontrain.Text = "train";
            this.buttontrain.UseVisualStyleBackColor = true;
            this.buttontrain.Click += new System.EventHandler(this.buttontrain_Click);
            // 
            // clear
            // 
            this.clear.Location = new System.Drawing.Point(604, 136);
            this.clear.Name = "clear";
            this.clear.Size = new System.Drawing.Size(75, 23);
            this.clear.TabIndex = 30;
            this.clear.Text = "clear";
            this.clear.UseVisualStyleBackColor = true;
            this.clear.Click += new System.EventHandler(this.clear_Click);
            // 
            // axWindowsMediaPlayer1
            // 
            this.axWindowsMediaPlayer1.Enabled = true;
            this.axWindowsMediaPlayer1.Location = new System.Drawing.Point(787, 12);
            this.axWindowsMediaPlayer1.Name = "axWindowsMediaPlayer1";
            this.axWindowsMediaPlayer1.OcxState = ((System.Windows.Forms.AxHost.State)(resources.GetObject("axWindowsMediaPlayer1.OcxState")));
            this.axWindowsMediaPlayer1.Size = new System.Drawing.Size(242, 168);
            this.axWindowsMediaPlayer1.TabIndex = 31;
            // 
            // menuStrip1
            // 
            this.menuStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.videoToolStripMenuItem,
            this.openToolStripMenuItem});
            this.menuStrip1.Location = new System.Drawing.Point(0, 0);
            this.menuStrip1.Name = "menuStrip1";
            this.menuStrip1.Size = new System.Drawing.Size(1324, 25);
            this.menuStrip1.TabIndex = 32;
            this.menuStrip1.Text = "menuStrip1";
            // 
            // videoToolStripMenuItem
            // 
            this.videoToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.openToolStripMenuItem1});
            this.videoToolStripMenuItem.Name = "videoToolStripMenuItem";
            this.videoToolStripMenuItem.Size = new System.Drawing.Size(52, 21);
            this.videoToolStripMenuItem.Text = "video";
            // 
            // openToolStripMenuItem
            // 
            this.openToolStripMenuItem.Name = "openToolStripMenuItem";
            this.openToolStripMenuItem.Size = new System.Drawing.Size(50, 21);
            this.openToolStripMenuItem.Text = "open";
            // 
            // openToolStripMenuItem1
            // 
            this.openToolStripMenuItem1.Name = "openToolStripMenuItem1";
            this.openToolStripMenuItem1.Size = new System.Drawing.Size(152, 22);
            this.openToolStripMenuItem1.Text = "open";
            this.openToolStripMenuItem1.Click += new System.EventHandler(this.openToolStripMenuItem1_Click);
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1324, 663);
            this.Controls.Add(this.axWindowsMediaPlayer1);
            this.Controls.Add(this.clear);
            this.Controls.Add(this.buttontrain);
            this.Controls.Add(this.buttonchecksource);
            this.Controls.Add(this.groupBox1);
            this.Controls.Add(this.pictureBoxcurrentimage);
            this.Controls.Add(this.pictureBoxsource);
            this.Controls.Add(this.richTextBox1);
            this.Controls.Add(this.buttoncompare);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.textBoxcurrentimage);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.textBoxpicturesource);
            this.Controls.Add(this.videoSourcePlayer);
            this.Controls.Add(this.menuStrip1);
            this.MainMenuStrip = this.menuStrip1;
            this.Name = "Form1";
            this.Text = "Form1";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.Form1_FormClosing);
            this.Load += new System.EventHandler(this.Form1_Load);
            ((System.ComponentModel.ISupportInitialize)(this.pictureBoxsource)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBoxcurrentimage)).EndInit();
            this.groupBox1.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.axWindowsMediaPlayer1)).EndInit();
            this.menuStrip1.ResumeLayout(false);
            this.menuStrip1.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.TextBox textBoxpicturesource;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.TextBox textBoxcurrentimage;
        private System.Windows.Forms.Button buttoncompare;
        private System.Windows.Forms.RichTextBox richTextBox1;
        private System.Windows.Forms.PictureBox pictureBoxsource;
        private System.Windows.Forms.PictureBox pictureBoxcurrentimage;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.ComboBox comboBoxcameras;
        private System.Windows.Forms.Button buttoncameracapture;
        private System.Windows.Forms.Button buttoncameraclose;
        private System.Windows.Forms.Button buttoncamera;
        private AForge.Controls.VideoSourcePlayer videoSourcePlayer;
        private System.Windows.Forms.Button buttonchecksource;
        private System.Windows.Forms.Button buttontrain;
        private System.Windows.Forms.Button clear;
        private AxWMPLib.AxWindowsMediaPlayer axWindowsMediaPlayer1;
        private System.Windows.Forms.MenuStrip menuStrip1;
        private System.Windows.Forms.ToolStripMenuItem videoToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem openToolStripMenuItem1;
        private System.Windows.Forms.ToolStripMenuItem openToolStripMenuItem;
    }
}

