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
            this.components = new System.ComponentModel.Container();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(frm_hive_mind));
            this.timer1 = new System.Windows.Forms.Timer(this.components);
            this.tab_control = new System.Windows.Forms.TabControl();
            this.tab_hm = new System.Windows.Forms.TabPage();
            this.groupBox2 = new System.Windows.Forms.GroupBox();
            this.gb_null2 = new System.Windows.Forms.GroupBox();
            this.gb_null1 = new System.Windows.Forms.GroupBox();
            this.textBox8 = new System.Windows.Forms.TextBox();
            this.label8 = new System.Windows.Forms.Label();
            this.textBox7 = new System.Windows.Forms.TextBox();
            this.label7 = new System.Windows.Forms.Label();
            this.label6 = new System.Windows.Forms.Label();
            this.label5 = new System.Windows.Forms.Label();
            this.label4 = new System.Windows.Forms.Label();
            this.textBox6 = new System.Windows.Forms.TextBox();
            this.textBox5 = new System.Windows.Forms.TextBox();
            this.textBox4 = new System.Windows.Forms.TextBox();
            this.tab_proxy = new System.Windows.Forms.TabPage();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.button1 = new System.Windows.Forms.Button();
            this.textBox1 = new System.Windows.Forms.TextBox();
            this.label3 = new System.Windows.Forms.Label();
            this.textBox2 = new System.Windows.Forms.TextBox();
            this.label2 = new System.Windows.Forms.Label();
            this.textBox3 = new System.Windows.Forms.TextBox();
            this.label1 = new System.Windows.Forms.Label();
            this.rb_irc = new System.Windows.Forms.RadioButton();
            this.rb_rss = new System.Windows.Forms.RadioButton();
            this.button2 = new System.Windows.Forms.Button();
            this.rb_nohm = new System.Windows.Forms.RadioButton();
            this.tab_control.SuspendLayout();
            this.tab_hm.SuspendLayout();
            this.groupBox2.SuspendLayout();
            this.gb_null1.SuspendLayout();
            this.tab_proxy.SuspendLayout();
            this.groupBox1.SuspendLayout();
            this.SuspendLayout();
            // 
            // timer1
            // 
            this.timer1.Enabled = true;
            this.timer1.Interval = 1;
            this.timer1.Tick += new System.EventHandler(this.timer1_Tick);
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
            this.tab_hm.Controls.Add(this.groupBox2);
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
            // groupBox2
            // 
            this.groupBox2.Controls.Add(this.button2);
            this.groupBox2.Location = new System.Drawing.Point(6, 179);
            this.groupBox2.Name = "groupBox2";
            this.groupBox2.Size = new System.Drawing.Size(408, 101);
            this.groupBox2.TabIndex = 2;
            this.groupBox2.TabStop = false;
            this.groupBox2.Text = "Settings (?)";
            // 
            // gb_null2
            // 
            this.gb_null2.Enabled = false;
            this.gb_null2.Location = new System.Drawing.Point(213, 12);
            this.gb_null2.Name = "gb_null2";
            this.gb_null2.Size = new System.Drawing.Size(201, 141);
            this.gb_null2.TabIndex = 1;
            this.gb_null2.TabStop = false;
            this.gb_null2.Text = "     RSS Hive Mind:";
            // 
            // gb_null1
            // 
            this.gb_null1.Controls.Add(this.textBox8);
            this.gb_null1.Controls.Add(this.label8);
            this.gb_null1.Controls.Add(this.textBox7);
            this.gb_null1.Controls.Add(this.label7);
            this.gb_null1.Controls.Add(this.label6);
            this.gb_null1.Controls.Add(this.label5);
            this.gb_null1.Controls.Add(this.label4);
            this.gb_null1.Controls.Add(this.textBox6);
            this.gb_null1.Controls.Add(this.textBox5);
            this.gb_null1.Controls.Add(this.textBox4);
            this.gb_null1.Enabled = false;
            this.gb_null1.Location = new System.Drawing.Point(6, 12);
            this.gb_null1.Name = "gb_null1";
            this.gb_null1.Size = new System.Drawing.Size(201, 141);
            this.gb_null1.TabIndex = 0;
            this.gb_null1.TabStop = false;
            this.gb_null1.Text = "     IRC Hive Mind:";
            // 
            // textBox8
            // 
            this.textBox8.Location = new System.Drawing.Point(108, 110);
            this.textBox8.Name = "textBox8";
            this.textBox8.Size = new System.Drawing.Size(87, 20);
            this.textBox8.TabIndex = 9;
            // 
            // label8
            // 
            this.label8.AutoSize = true;
            this.label8.Location = new System.Drawing.Point(108, 94);
            this.label8.Name = "label8";
            this.label8.Size = new System.Drawing.Size(35, 13);
            this.label8.TabIndex = 8;
            this.label8.Text = "label8";
            // 
            // textBox7
            // 
            this.textBox7.Location = new System.Drawing.Point(6, 110);
            this.textBox7.Name = "textBox7";
            this.textBox7.Size = new System.Drawing.Size(96, 20);
            this.textBox7.TabIndex = 7;
            // 
            // label7
            // 
            this.label7.AutoSize = true;
            this.label7.Location = new System.Drawing.Point(6, 94);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(35, 13);
            this.label7.TabIndex = 6;
            this.label7.Text = "label7";
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Location = new System.Drawing.Point(6, 55);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(35, 13);
            this.label6.TabIndex = 5;
            this.label6.Text = "label6";
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(73, 55);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(35, 13);
            this.label5.TabIndex = 4;
            this.label5.Text = "label5";
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(6, 16);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(35, 13);
            this.label4.TabIndex = 3;
            this.label4.Text = "label4";
            // 
            // textBox6
            // 
            this.textBox6.Location = new System.Drawing.Point(6, 71);
            this.textBox6.Name = "textBox6";
            this.textBox6.Size = new System.Drawing.Size(64, 20);
            this.textBox6.TabIndex = 2;
            // 
            // textBox5
            // 
            this.textBox5.Location = new System.Drawing.Point(76, 71);
            this.textBox5.Name = "textBox5";
            this.textBox5.Size = new System.Drawing.Size(119, 20);
            this.textBox5.TabIndex = 1;
            // 
            // textBox4
            // 
            this.textBox4.Location = new System.Drawing.Point(6, 32);
            this.textBox4.Name = "textBox4";
            this.textBox4.Size = new System.Drawing.Size(189, 20);
            this.textBox4.TabIndex = 0;
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
            // rb_irc
            // 
            this.rb_irc.AutoSize = true;
            this.rb_irc.Location = new System.Drawing.Point(12, 3);
            this.rb_irc.Name = "rb_irc";
            this.rb_irc.Size = new System.Drawing.Size(14, 13);
            this.rb_irc.TabIndex = 3;
            this.rb_irc.TabStop = true;
            this.rb_irc.UseVisualStyleBackColor = true;
            this.rb_irc.CheckedChanged += new System.EventHandler(this.radioButton1_CheckedChanged);
            // 
            // rb_rss
            // 
            this.rb_rss.AutoSize = true;
            this.rb_rss.Location = new System.Drawing.Point(222, 3);
            this.rb_rss.Name = "rb_rss";
            this.rb_rss.Size = new System.Drawing.Size(14, 13);
            this.rb_rss.TabIndex = 4;
            this.rb_rss.TabStop = true;
            this.rb_rss.UseVisualStyleBackColor = true;
            this.rb_rss.CheckedChanged += new System.EventHandler(this.radioButton2_CheckedChanged);
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
            // rb_nohm
            // 
            this.rb_nohm.AutoSize = true;
            this.rb_nohm.Location = new System.Drawing.Point(8, 159);
            this.rb_nohm.Name = "rb_nohm";
            this.rb_nohm.Size = new System.Drawing.Size(121, 17);
            this.rb_nohm.TabIndex = 1;
            this.rb_nohm.TabStop = true;
            this.rb_nohm.Text = "Don\'t use Hive Mind";
            this.rb_nohm.UseVisualStyleBackColor = true;
            this.rb_nohm.CheckedChanged += new System.EventHandler(this.rb_nohm_CheckedChanged);
            // 
            // frm_hive_mind
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(427, 314);
            this.Controls.Add(this.tab_control);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.Fixed3D;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "frm_hive_mind";
            this.ShowInTaskbar = false;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "GAS :: Hive Mind CP";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.frm_hive_mind_FormClosing);
            this.Load += new System.EventHandler(this.frm_hive_mind_Load);
            this.tab_control.ResumeLayout(false);
            this.tab_hm.ResumeLayout(false);
            this.tab_hm.PerformLayout();
            this.groupBox2.ResumeLayout(false);
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
        private System.Windows.Forms.GroupBox groupBox2;
        private System.Windows.Forms.TextBox textBox8;
        private System.Windows.Forms.Label label8;
        private System.Windows.Forms.TextBox textBox7;
        private System.Windows.Forms.Label label7;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.TextBox textBox6;
        private System.Windows.Forms.TextBox textBox5;
        private System.Windows.Forms.TextBox textBox4;
        private System.Windows.Forms.RadioButton rb_irc;
        private System.Windows.Forms.RadioButton rb_rss;
        private System.Windows.Forms.RadioButton rb_nohm;
        private System.Windows.Forms.Button button2;
    }
}