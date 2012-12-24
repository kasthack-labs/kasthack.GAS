namespace GAS
{
    partial class frm_about
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing) {
            if ( disposing && ( components != null ) ) {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent() {
            this.components = new System.ComponentModel.Container();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(frm_about));
            this.pictureBox1 = new System.Windows.Forms.PictureBox();
            this.timer = new System.Windows.Forms.Timer(this.components);
            this.lbl_logo = new System.Windows.Forms.Label();
            this.btn_close = new System.Windows.Forms.Button();
            this.link_support = new System.Windows.Forms.LinkLabel();
            this.lbl_clr = new System.Windows.Forms.Label();
            this.gb_null3 = new System.Windows.Forms.GroupBox();
            this.lbl_os = new System.Windows.Forms.Label();
            this.lbl_gas64 = new System.Windows.Forms.Label();
            this.lbl_os64 = new System.Windows.Forms.Label();
            this.lbl_proc = new System.Windows.Forms.Label();
            this.lbl_wssize = new System.Windows.Forms.Label();
            this.lbl_pagesize = new System.Windows.Forms.Label();
            this.lbl_username = new System.Windows.Forms.Label();
            this.lbl_progv = new System.Windows.Forms.Label();
            this.gb_null4 = new System.Windows.Forms.GroupBox();
            this.panel = new System.Windows.Forms.Panel();
            this.lbl_somenots = new System.Windows.Forms.Label();
            this.lbl_stm = new System.Windows.Forms.Label();
            this.lbl_kst = new System.Windows.Forms.Label();
            this.lbl_null1 = new System.Windows.Forms.Label();
            ( (System.ComponentModel.ISupportInitialize)( this.pictureBox1 ) ).BeginInit();
            this.gb_null3.SuspendLayout();
            this.gb_null4.SuspendLayout();
            this.panel.SuspendLayout();
            this.SuspendLayout();
            // 
            // pictureBox1
            // 
            this.pictureBox1.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.pictureBox1.Image = global::GAS.Properties.Resources.New_logo;
            this.pictureBox1.Location = new System.Drawing.Point(12, 12);
            this.pictureBox1.Name = "pictureBox1";
            this.pictureBox1.Size = new System.Drawing.Size(188, 467);
            this.pictureBox1.SizeMode = System.Windows.Forms.PictureBoxSizeMode.AutoSize;
            this.pictureBox1.TabIndex = 0;
            this.pictureBox1.TabStop = false;
            // 
            // timer
            // 
            this.timer.Enabled = true;
            this.timer.Interval = 1;
            this.timer.Tick += new System.EventHandler(this.timer1_Tick);
            // 
            // lbl_logo
            // 
            this.lbl_logo.AutoSize = true;
            this.lbl_logo.Font = new System.Drawing.Font("Microsoft Sans Serif", 11.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ( (byte)( 204 ) ));
            this.lbl_logo.Location = new System.Drawing.Point(249, 12);
            this.lbl_logo.Name = "lbl_logo";
            this.lbl_logo.Size = new System.Drawing.Size(267, 18);
            this.lbl_logo.TabIndex = 1;
            this.lbl_logo.Text = "Ground-based Antisatellite System";
            // 
            // btn_close
            // 
            this.btn_close.Location = new System.Drawing.Point(503, 456);
            this.btn_close.Name = "btn_close";
            this.btn_close.Size = new System.Drawing.Size(75, 23);
            this.btn_close.TabIndex = 2;
            this.btn_close.Text = "OK";
            this.btn_close.UseVisualStyleBackColor = true;
            this.btn_close.Click += new System.EventHandler(this.btn_close_Click);
            // 
            // link_support
            // 
            this.link_support.AutoSize = true;
            this.link_support.Location = new System.Drawing.Point(325, 461);
            this.link_support.Name = "link_support";
            this.link_support.Size = new System.Drawing.Size(171, 13);
            this.link_support.TabIndex = 3;
            this.link_support.TabStop = true;
            this.link_support.Text = "https://github.com/kasthack/GAS";
            this.link_support.LinkClicked += new System.Windows.Forms.LinkLabelLinkClickedEventHandler(this.link_support_LinkClicked);
            // 
            // lbl_clr
            // 
            this.lbl_clr.AutoSize = true;
            this.lbl_clr.Location = new System.Drawing.Point(19, 98);
            this.lbl_clr.Name = "lbl_clr";
            this.lbl_clr.Size = new System.Drawing.Size(80, 13);
            this.lbl_clr.TabIndex = 4;
            this.lbl_clr.Text = "♦ CLR Version:";
            // 
            // gb_null3
            // 
            this.gb_null3.Controls.Add(this.lbl_os);
            this.gb_null3.Controls.Add(this.lbl_gas64);
            this.gb_null3.Controls.Add(this.lbl_os64);
            this.gb_null3.Controls.Add(this.lbl_proc);
            this.gb_null3.Controls.Add(this.lbl_wssize);
            this.gb_null3.Controls.Add(this.lbl_pagesize);
            this.gb_null3.Controls.Add(this.lbl_username);
            this.gb_null3.Controls.Add(this.lbl_clr);
            this.gb_null3.Location = new System.Drawing.Point(202, 297);
            this.gb_null3.Name = "gb_null3";
            this.gb_null3.Size = new System.Drawing.Size(376, 153);
            this.gb_null3.TabIndex = 5;
            this.gb_null3.TabStop = false;
            this.gb_null3.Text = "User\'s Info:";
            // 
            // lbl_os
            // 
            this.lbl_os.AutoSize = true;
            this.lbl_os.Location = new System.Drawing.Point(19, 121);
            this.lbl_os.Name = "lbl_os";
            this.lbl_os.Size = new System.Drawing.Size(74, 13);
            this.lbl_os.TabIndex = 10;
            this.lbl_os.Text = "♦ OS Version:";
            // 
            // lbl_gas64
            // 
            this.lbl_gas64.AutoSize = true;
            this.lbl_gas64.Location = new System.Drawing.Point(227, 29);
            this.lbl_gas64.Name = "lbl_gas64";
            this.lbl_gas64.Size = new System.Drawing.Size(96, 13);
            this.lbl_gas64.TabIndex = 6;
            this.lbl_gas64.Text = "♦ 64-bit app: False";
            // 
            // lbl_os64
            // 
            this.lbl_os64.AutoSize = true;
            this.lbl_os64.Location = new System.Drawing.Point(227, 52);
            this.lbl_os64.Name = "lbl_os64";
            this.lbl_os64.Size = new System.Drawing.Size(93, 13);
            this.lbl_os64.TabIndex = 9;
            this.lbl_os64.Text = "♦ 64-bit OS: False";
            // 
            // lbl_proc
            // 
            this.lbl_proc.AutoSize = true;
            this.lbl_proc.Location = new System.Drawing.Point(227, 75);
            this.lbl_proc.Name = "lbl_proc";
            this.lbl_proc.Size = new System.Drawing.Size(78, 13);
            this.lbl_proc.TabIndex = 9;
            this.lbl_proc.Text = "♦ Count of CP:";
            // 
            // lbl_wssize
            // 
            this.lbl_wssize.AutoSize = true;
            this.lbl_wssize.Location = new System.Drawing.Point(19, 75);
            this.lbl_wssize.Name = "lbl_wssize";
            this.lbl_wssize.Size = new System.Drawing.Size(100, 13);
            this.lbl_wssize.TabIndex = 8;
            this.lbl_wssize.Text = "♦ Physical Memory:";
            // 
            // lbl_pagesize
            // 
            this.lbl_pagesize.AutoSize = true;
            this.lbl_pagesize.Location = new System.Drawing.Point(19, 52);
            this.lbl_pagesize.Name = "lbl_pagesize";
            this.lbl_pagesize.Size = new System.Drawing.Size(69, 13);
            this.lbl_pagesize.TabIndex = 7;
            this.lbl_pagesize.Text = "♦ Page Size:";
            // 
            // lbl_username
            // 
            this.lbl_username.AutoSize = true;
            this.lbl_username.Location = new System.Drawing.Point(19, 29);
            this.lbl_username.Name = "lbl_username";
            this.lbl_username.Size = new System.Drawing.Size(69, 13);
            this.lbl_username.TabIndex = 5;
            this.lbl_username.Text = "♦ Username:";
            // 
            // lbl_progv
            // 
            this.lbl_progv.AutoSize = true;
            this.lbl_progv.Location = new System.Drawing.Point(476, 44);
            this.lbl_progv.Name = "lbl_progv";
            this.lbl_progv.Size = new System.Drawing.Size(40, 13);
            this.lbl_progv.TabIndex = 6;
            this.lbl_progv.Text = "0.0.0.0";
            // 
            // gb_null4
            // 
            this.gb_null4.Controls.Add(this.panel);
            this.gb_null4.Controls.Add(this.lbl_stm);
            this.gb_null4.Controls.Add(this.lbl_kst);
            this.gb_null4.Location = new System.Drawing.Point(206, 60);
            this.gb_null4.Name = "gb_null4";
            this.gb_null4.Size = new System.Drawing.Size(372, 231);
            this.gb_null4.TabIndex = 7;
            this.gb_null4.TabStop = false;
            this.gb_null4.Text = "Authors:";
            // 
            // panel
            // 
            this.panel.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.panel.Controls.Add(this.lbl_somenots);
            this.panel.Location = new System.Drawing.Point(21, 101);
            this.panel.Name = "panel";
            this.panel.Size = new System.Drawing.Size(331, 114);
            this.panel.TabIndex = 2;
            // 
            // lbl_somenots
            // 
            this.lbl_somenots.Location = new System.Drawing.Point(21, 13);
            this.lbl_somenots.Name = "lbl_somenots";
            this.lbl_somenots.Size = new System.Drawing.Size(284, 84);
            this.lbl_somenots.TabIndex = 0;
            this.lbl_somenots.Text = "♦ GAS (Ground-based Antisatellite System) - OpenSource program, based on souce-co" +
    "de of LOIC and IRC LOIC. \r\n\r\n♦ License: OpenSource, Public Domain\r\n\r\n♦ Program T" +
    "ype: Network testing";
            // 
            // lbl_stm
            // 
            this.lbl_stm.AutoSize = true;
            this.lbl_stm.Location = new System.Drawing.Point(127, 68);
            this.lbl_stm.Name = "lbl_stm";
            this.lbl_stm.Size = new System.Drawing.Size(98, 13);
            this.lbl_stm.TabIndex = 1;
            this.lbl_stm.Text = "♦ New GUI: STAM";
            // 
            // lbl_kst
            // 
            this.lbl_kst.AutoSize = true;
            this.lbl_kst.Location = new System.Drawing.Point(127, 35);
            this.lbl_kst.Name = "lbl_kst";
            this.lbl_kst.Size = new System.Drawing.Size(118, 13);
            this.lbl_kst.TabIndex = 0;
            this.lbl_kst.Text = "♦ Main code: kasthack";
            // 
            // lbl_null1
            // 
            this.lbl_null1.AutoSize = true;
            this.lbl_null1.Location = new System.Drawing.Point(206, 461);
            this.lbl_null1.Name = "lbl_null1";
            this.lbl_null1.Size = new System.Drawing.Size(113, 13);
            this.lbl_null1.TabIndex = 8;
            this.lbl_null1.Text = "♦ Bugfixes && Updates:";
            // 
            // frm_about
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(590, 491);
            this.Controls.Add(this.lbl_null1);
            this.Controls.Add(this.gb_null4);
            this.Controls.Add(this.lbl_progv);
            this.Controls.Add(this.gb_null3);
            this.Controls.Add(this.link_support);
            this.Controls.Add(this.btn_close);
            this.Controls.Add(this.lbl_logo);
            this.Controls.Add(this.pictureBox1);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.Fixed3D;
            this.Icon = ( (System.Drawing.Icon)( resources.GetObject("$this.Icon") ) );
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "frm_about";
            this.ShowInTaskbar = false;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "GAS :: About";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.frm_about_FormClosing);
            this.Load += new System.EventHandler(this.frm_about_Load);
            ( (System.ComponentModel.ISupportInitialize)( this.pictureBox1 ) ).EndInit();
            this.gb_null3.ResumeLayout(false);
            this.gb_null3.PerformLayout();
            this.gb_null4.ResumeLayout(false);
            this.gb_null4.PerformLayout();
            this.panel.ResumeLayout(false);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.PictureBox pictureBox1;
        private System.Windows.Forms.Timer timer;
        private System.Windows.Forms.Label lbl_logo;
        private System.Windows.Forms.Button btn_close;
        private System.Windows.Forms.LinkLabel link_support;
        private System.Windows.Forms.Label lbl_clr;
        private System.Windows.Forms.GroupBox gb_null3;
        private System.Windows.Forms.Label lbl_username;
        private System.Windows.Forms.Label lbl_gas64;
        private System.Windows.Forms.Label lbl_pagesize;
        private System.Windows.Forms.Label lbl_wssize;
        private System.Windows.Forms.Label lbl_os64;
        private System.Windows.Forms.Label lbl_proc;
        private System.Windows.Forms.Label lbl_progv;
        private System.Windows.Forms.Label lbl_os;
        private System.Windows.Forms.GroupBox gb_null4;
        private System.Windows.Forms.Panel panel;
        private System.Windows.Forms.Label lbl_somenots;
        private System.Windows.Forms.Label lbl_stm;
        private System.Windows.Forms.Label lbl_kst;
        private System.Windows.Forms.Label lbl_null1;
    }
}