namespace GAS
{
    partial class frm_hive_mind
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(frm_hive_mind));
            this.timer1 = new System.Windows.Forms.Timer(this.components);
            this.tab_control = new System.Windows.Forms.TabControl();
            this.tab_hm = new System.Windows.Forms.TabPage();
            this.rb_nohm = new System.Windows.Forms.RadioButton();
            this.rb_rss = new System.Windows.Forms.RadioButton();
            this.rb_irc = new System.Windows.Forms.RadioButton();
            this.gb_settings = new System.Windows.Forms.GroupBox();
            this.button2 = new System.Windows.Forms.Button();
            this.gb_null2 = new System.Windows.Forms.GroupBox();
            this.gb_null1 = new System.Windows.Forms.GroupBox();
            this.txt_ircpass = new System.Windows.Forms.TextBox();
            this.lbl_ircpass = new System.Windows.Forms.Label();
            this.txt_ircusername = new System.Windows.Forms.TextBox();
            this.lbl_ircusername = new System.Windows.Forms.Label();
            this.lbl_ircport = new System.Windows.Forms.Label();
            this.lbl_ircchannel = new System.Windows.Forms.Label();
            this.lbl_ircserv = new System.Windows.Forms.Label();
            this.txt_ircport = new System.Windows.Forms.TextBox();
            this.txt_ircchannel = new System.Windows.Forms.TextBox();
            this.txt_ircserv = new System.Windows.Forms.TextBox();
            this.tab_proxy = new System.Windows.Forms.TabPage();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.button1 = new System.Windows.Forms.Button();
            this.textBox1 = new System.Windows.Forms.TextBox();
            this.label3 = new System.Windows.Forms.Label();
            this.textBox2 = new System.Windows.Forms.TextBox();
            this.label2 = new System.Windows.Forms.Label();
            this.textBox3 = new System.Windows.Forms.TextBox();
            this.label1 = new System.Windows.Forms.Label();
            this.lbl_dointerval = new System.Windows.Forms.Label();
            this.lbl_dourl = new System.Windows.Forms.Label();
            this.txt_dointerval = new System.Windows.Forms.TextBox();
            this.txt_dourl = new System.Windows.Forms.TextBox();
            this.label4 = new System.Windows.Forms.Label();
            this.tab_control.SuspendLayout();
            this.tab_hm.SuspendLayout();
            this.gb_settings.SuspendLayout();
            this.gb_null2.SuspendLayout();
            this.gb_null1.SuspendLayout();
            this.tab_proxy.SuspendLayout();
            this.groupBox1.SuspendLayout();
            this.SuspendLayout();
            // 
            // timer1
            // 
            this.timer1.Enabled = true;
            this.timer1.Interval = 1;
            // 
            // tab_control
            // 
            this.tab_control.Controls.Add(this.tab_hm);
            this.tab_control.Controls.Add(this.tab_proxy);
            this.tab_control.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tab_control.Location = new System.Drawing.Point(0, 0);
            this.tab_control.Name = "tab_control";
            this.tab_control.SelectedIndex = 0;
            this.tab_control.Size = new System.Drawing.Size(427, 314);
            this.tab_control.TabIndex = 0;
            // 
            // tab_hm
            // 
            this.tab_hm.Controls.Add(this.rb_nohm);
            this.tab_hm.Controls.Add(this.rb_rss);
            this.tab_hm.Controls.Add(this.rb_irc);
            this.tab_hm.Controls.Add(this.gb_settings);
            this.tab_hm.Controls.Add(this.gb_null2);
            this.tab_hm.Controls.Add(this.gb_null1);
            this.tab_hm.Location = new System.Drawing.Point(4, 22);
            this.tab_hm.Name = "tab_hm";
            this.tab_hm.Padding = new System.Windows.Forms.Padding(3);
            this.tab_hm.Size = new System.Drawing.Size(419, 288);
            this.tab_hm.TabIndex = 0;
            this.tab_hm.Text = "Hive Mind";
            this.tab_hm.UseVisualStyleBackColor = true;
            // 
            // rb_nohm
            // 
            this.rb_nohm.AutoSize = true;
            this.rb_nohm.Checked = true;
            this.rb_nohm.Location = new System.Drawing.Point(149, 159);
            this.rb_nohm.Name = "rb_nohm";
            this.rb_nohm.Size = new System.Drawing.Size(121, 17);
            this.rb_nohm.TabIndex = 1;
            this.rb_nohm.TabStop = true;
            this.rb_nohm.Text = "Don\'t use Hive Mind";
            this.rb_nohm.UseVisualStyleBackColor = true;
            this.rb_nohm.CheckedChanged += new System.EventHandler(this.rb_nohm_CheckedChanged);
            // 
            // rb_rss
            // 
            this.rb_rss.AutoSize = true;
            this.rb_rss.CheckAlign = System.Drawing.ContentAlignment.MiddleCenter;
            this.rb_rss.Location = new System.Drawing.Point(222, 3);
            this.rb_rss.Name = "rb_rss";
            this.rb_rss.Size = new System.Drawing.Size(14, 13);
            this.rb_rss.TabIndex = 4;
            this.rb_rss.UseVisualStyleBackColor = true;
            this.rb_rss.CheckedChanged += new System.EventHandler(this.radioButton2_CheckedChanged);
            // 
            // rb_irc
            // 
            this.rb_irc.AutoSize = true;
            this.rb_irc.CheckAlign = System.Drawing.ContentAlignment.MiddleCenter;
            this.rb_irc.Location = new System.Drawing.Point(12, 3);
            this.rb_irc.Name = "rb_irc";
            this.rb_irc.Size = new System.Drawing.Size(14, 13);
            this.rb_irc.TabIndex = 3;
            this.rb_irc.UseVisualStyleBackColor = true;
            this.rb_irc.CheckedChanged += new System.EventHandler(this.radioButton1_CheckedChanged);
            // 
            // gb_settings
            // 
            this.gb_settings.Controls.Add(this.label4);
            this.gb_settings.Controls.Add(this.button2);
            this.gb_settings.Location = new System.Drawing.Point(6, 179);
            this.gb_settings.Name = "gb_settings";
            this.gb_settings.Size = new System.Drawing.Size(408, 101);
            this.gb_settings.TabIndex = 2;
            this.gb_settings.TabStop = false;
            this.gb_settings.Text = "Settings (?)";
            // 
            // button2
            // 
            this.button2.Location = new System.Drawing.Point(327, 72);
            this.button2.Name = "button2";
            this.button2.Size = new System.Drawing.Size(75, 23);
            this.button2.TabIndex = 0;
            this.button2.Text = "Save";
            this.button2.UseVisualStyleBackColor = true;
            // 
            // gb_null2
            // 
            this.gb_null2.Controls.Add(this.lbl_dointerval);
            this.gb_null2.Controls.Add(this.lbl_dourl);
            this.gb_null2.Controls.Add(this.txt_dointerval);
            this.gb_null2.Controls.Add(this.txt_dourl);
            this.gb_null2.Enabled = false;
            this.gb_null2.Location = new System.Drawing.Point(213, 12);
            this.gb_null2.Name = "gb_null2";
            this.gb_null2.Size = new System.Drawing.Size(201, 141);
            this.gb_null2.TabIndex = 1;
            this.gb_null2.TabStop = false;
            this.gb_null2.Text = "     DAMN Overlord:";
            // 
            // gb_null1
            // 
            this.gb_null1.Controls.Add(this.txt_ircpass);
            this.gb_null1.Controls.Add(this.lbl_ircpass);
            this.gb_null1.Controls.Add(this.txt_ircusername);
            this.gb_null1.Controls.Add(this.lbl_ircusername);
            this.gb_null1.Controls.Add(this.lbl_ircport);
            this.gb_null1.Controls.Add(this.lbl_ircchannel);
            this.gb_null1.Controls.Add(this.lbl_ircserv);
            this.gb_null1.Controls.Add(this.txt_ircport);
            this.gb_null1.Controls.Add(this.txt_ircchannel);
            this.gb_null1.Controls.Add(this.txt_ircserv);
            this.gb_null1.Enabled = false;
            this.gb_null1.Location = new System.Drawing.Point(6, 12);
            this.gb_null1.Name = "gb_null1";
            this.gb_null1.Size = new System.Drawing.Size(201, 141);
            this.gb_null1.TabIndex = 0;
            this.gb_null1.TabStop = false;
            this.gb_null1.Text = "     IRC Hive Mind:";
            // 
            // txt_ircpass
            // 
            this.txt_ircpass.Location = new System.Drawing.Point(109, 110);
            this.txt_ircpass.Name = "txt_ircpass";
            this.txt_ircpass.Size = new System.Drawing.Size(86, 20);
            this.txt_ircpass.TabIndex = 9;
            // 
            // lbl_ircpass
            // 
            this.lbl_ircpass.AutoSize = true;
            this.lbl_ircpass.Location = new System.Drawing.Point(109, 94);
            this.lbl_ircpass.Name = "lbl_ircpass";
            this.lbl_ircpass.Size = new System.Drawing.Size(72, 13);
            this.lbl_ircpass.TabIndex = 8;
            this.lbl_ircpass.Text = "Pass (not req)";
            // 
            // txt_ircusername
            // 
            this.txt_ircusername.Location = new System.Drawing.Point(6, 110);
            this.txt_ircusername.Name = "txt_ircusername";
            this.txt_ircusername.Size = new System.Drawing.Size(97, 20);
            this.txt_ircusername.TabIndex = 7;
            // 
            // lbl_ircusername
            // 
            this.lbl_ircusername.AutoSize = true;
            this.lbl_ircusername.Location = new System.Drawing.Point(6, 94);
            this.lbl_ircusername.Name = "lbl_ircusername";
            this.lbl_ircusername.Size = new System.Drawing.Size(97, 13);
            this.lbl_ircusername.TabIndex = 6;
            this.lbl_ircusername.Text = "Username (not req)";
            // 
            // lbl_ircport
            // 
            this.lbl_ircport.AutoSize = true;
            this.lbl_ircport.Location = new System.Drawing.Point(6, 55);
            this.lbl_ircport.Name = "lbl_ircport";
            this.lbl_ircport.Size = new System.Drawing.Size(29, 13);
            this.lbl_ircport.TabIndex = 5;
            this.lbl_ircport.Text = "Port:";
            // 
            // lbl_ircchannel
            // 
            this.lbl_ircchannel.AutoSize = true;
            this.lbl_ircchannel.Location = new System.Drawing.Point(73, 55);
            this.lbl_ircchannel.Name = "lbl_ircchannel";
            this.lbl_ircchannel.Size = new System.Drawing.Size(49, 13);
            this.lbl_ircchannel.TabIndex = 4;
            this.lbl_ircchannel.Text = "Channel:";
            // 
            // lbl_ircserv
            // 
            this.lbl_ircserv.AutoSize = true;
            this.lbl_ircserv.Location = new System.Drawing.Point(6, 16);
            this.lbl_ircserv.Name = "lbl_ircserv";
            this.lbl_ircserv.Size = new System.Drawing.Size(41, 13);
            this.lbl_ircserv.TabIndex = 3;
            this.lbl_ircserv.Text = "Server:";
            // 
            // txt_ircport
            // 
            this.txt_ircport.Location = new System.Drawing.Point(6, 71);
            this.txt_ircport.Name = "txt_ircport";
            this.txt_ircport.Size = new System.Drawing.Size(64, 20);
            this.txt_ircport.TabIndex = 2;
            // 
            // txt_ircchannel
            // 
            this.txt_ircchannel.Location = new System.Drawing.Point(76, 71);
            this.txt_ircchannel.Name = "txt_ircchannel";
            this.txt_ircchannel.Size = new System.Drawing.Size(119, 20);
            this.txt_ircchannel.TabIndex = 1;
            // 
            // txt_ircserv
            // 
            this.txt_ircserv.Location = new System.Drawing.Point(6, 32);
            this.txt_ircserv.Name = "txt_ircserv";
            this.txt_ircserv.Size = new System.Drawing.Size(189, 20);
            this.txt_ircserv.TabIndex = 0;
            // 
            // tab_proxy
            // 
            this.tab_proxy.Controls.Add(this.groupBox1);
            this.tab_proxy.Location = new System.Drawing.Point(4, 22);
            this.tab_proxy.Name = "tab_proxy";
            this.tab_proxy.Padding = new System.Windows.Forms.Padding(3);
            this.tab_proxy.Size = new System.Drawing.Size(419, 288);
            this.tab_proxy.TabIndex = 1;
            this.tab_proxy.Text = "Proxy";
            this.tab_proxy.UseVisualStyleBackColor = true;
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.button1);
            this.groupBox1.Controls.Add(this.textBox1);
            this.groupBox1.Controls.Add(this.label3);
            this.groupBox1.Controls.Add(this.textBox2);
            this.groupBox1.Controls.Add(this.label2);
            this.groupBox1.Controls.Add(this.textBox3);
            this.groupBox1.Controls.Add(this.label1);
            this.groupBox1.Location = new System.Drawing.Point(79, 57);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(261, 175);
            this.groupBox1.TabIndex = 6;
            this.groupBox1.TabStop = false;
            // 
            // button1
            // 
            this.button1.Location = new System.Drawing.Point(156, 129);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(75, 23);
            this.button1.TabIndex = 6;
            this.button1.Text = "Save";
            this.button1.UseVisualStyleBackColor = true;
            // 
            // textBox1
            // 
            this.textBox1.Location = new System.Drawing.Point(42, 19);
            this.textBox1.Name = "textBox1";
            this.textBox1.Size = new System.Drawing.Size(189, 20);
            this.textBox1.TabIndex = 0;
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(16, 106);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(49, 13);
            this.label3.TabIndex = 5;
            this.label3.Text = "Protocol:";
            // 
            // textBox2
            // 
            this.textBox2.Location = new System.Drawing.Point(51, 61);
            this.textBox2.Name = "textBox2";
            this.textBox2.Size = new System.Drawing.Size(180, 20);
            this.textBox2.TabIndex = 1;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(16, 64);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(29, 13);
            this.label2.TabIndex = 4;
            this.label2.Text = "Port:";
            // 
            // textBox3
            // 
            this.textBox3.Location = new System.Drawing.Point(71, 103);
            this.textBox3.Name = "textBox3";
            this.textBox3.Size = new System.Drawing.Size(160, 20);
            this.textBox3.TabIndex = 2;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(16, 22);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(20, 13);
            this.label1.TabIndex = 3;
            this.label1.Text = "IP:";
            // 
            // lbl_dointerval
            // 
            this.lbl_dointerval.AutoSize = true;
            this.lbl_dointerval.Location = new System.Drawing.Point(6, 55);
            this.lbl_dointerval.Name = "lbl_dointerval";
            this.lbl_dointerval.Size = new System.Drawing.Size(45, 13);
            this.lbl_dointerval.TabIndex = 9;
            this.lbl_dointerval.Text = "Interval:";
            // 
            // lbl_dourl
            // 
            this.lbl_dourl.AutoSize = true;
            this.lbl_dourl.Location = new System.Drawing.Point(6, 16);
            this.lbl_dourl.Name = "lbl_dourl";
            this.lbl_dourl.Size = new System.Drawing.Size(23, 13);
            this.lbl_dourl.TabIndex = 8;
            this.lbl_dourl.Text = "Url:";
            // 
            // txt_dointerval
            // 
            this.txt_dointerval.Location = new System.Drawing.Point(6, 71);
            this.txt_dointerval.Name = "txt_dointerval";
            this.txt_dointerval.Size = new System.Drawing.Size(189, 20);
            this.txt_dointerval.TabIndex = 7;
            // 
            // txt_dourl
            // 
            this.txt_dourl.Location = new System.Drawing.Point(6, 32);
            this.txt_dourl.Name = "txt_dourl";
            this.txt_dourl.Size = new System.Drawing.Size(189, 20);
            this.txt_dourl.TabIndex = 6;
            // 
            // label4
            // 
            this.label4.Location = new System.Drawing.Point(12, 16);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(309, 79);
            this.label4.TabIndex = 1;
            this.label4.Text = "WTF:\r\n* keys\r\n** Limits\r\n***allowed attacks              *** allow stop\r\n*** allo" +
    "w no sign                 *** max attacks\r\n***allow plugins                  ***" +
    " allow start\r\n\t\r\n\t";
            // 
            // frm_hive_mind
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(427, 314);
            this.Controls.Add(this.tab_control);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.Fixed3D;
            this.Icon = ( (System.Drawing.Icon)( resources.GetObject("$this.Icon") ) );
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "frm_hive_mind";
            this.Opacity = 0D;
            this.ShowInTaskbar = false;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "GAS :: Hive Mind CP";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.frm_hive_mind_FormClosing);
            this.Load += new System.EventHandler(this.frm_hive_mind_Load);
            this.tab_control.ResumeLayout(false);
            this.tab_hm.ResumeLayout(false);
            this.tab_hm.PerformLayout();
            this.gb_settings.ResumeLayout(false);
            this.gb_null2.ResumeLayout(false);
            this.gb_null2.PerformLayout();
            this.gb_null1.ResumeLayout(false);
            this.gb_null1.PerformLayout();
            this.tab_proxy.ResumeLayout(false);
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Timer timer1;
        private System.Windows.Forms.TabControl tab_control;
        private System.Windows.Forms.TabPage tab_hm;
        private System.Windows.Forms.TabPage tab_proxy;
        private System.Windows.Forms.GroupBox gb_null1;
        private System.Windows.Forms.GroupBox gb_null2;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.TextBox textBox1;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.TextBox textBox2;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.TextBox textBox3;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Button button1;
        private System.Windows.Forms.GroupBox gb_settings;
        private System.Windows.Forms.TextBox txt_ircpass;
        private System.Windows.Forms.Label lbl_ircpass;
        private System.Windows.Forms.TextBox txt_ircusername;
        private System.Windows.Forms.Label lbl_ircusername;
        private System.Windows.Forms.Label lbl_ircport;
        private System.Windows.Forms.Label lbl_ircchannel;
        private System.Windows.Forms.Label lbl_ircserv;
        private System.Windows.Forms.TextBox txt_ircport;
        private System.Windows.Forms.TextBox txt_ircchannel;
        private System.Windows.Forms.TextBox txt_ircserv;
        private System.Windows.Forms.RadioButton rb_irc;
        private System.Windows.Forms.RadioButton rb_rss;
        private System.Windows.Forms.RadioButton rb_nohm;
        private System.Windows.Forms.Button button2;
        private System.Windows.Forms.Label lbl_dointerval;
        private System.Windows.Forms.Label lbl_dourl;
        private System.Windows.Forms.TextBox txt_dointerval;
        private System.Windows.Forms.TextBox txt_dourl;
        private System.Windows.Forms.Label label4;
    }
}