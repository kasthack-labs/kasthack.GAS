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
            this.tab_irc = new System.Windows.Forms.TabPage();
            this.tab_rss = new System.Windows.Forms.TabPage();
            this.gb_null1 = new System.Windows.Forms.GroupBox();
            this.gb_null2 = new System.Windows.Forms.GroupBox();
            this.tab_control.SuspendLayout();
            this.tab_irc.SuspendLayout();
            this.tab_rss.SuspendLayout();
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
            this.tab_control.Controls.Add(this.tab_irc);
            this.tab_control.Controls.Add(this.tab_rss);
            this.tab_control.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tab_control.Location = new System.Drawing.Point(0, 0);
            this.tab_control.Name = "tab_control";
            this.tab_control.SelectedIndex = 0;
            this.tab_control.Size = new System.Drawing.Size(427, 314);
            this.tab_control.TabIndex = 0;
            // 
            // tab_irc
            // 
            this.tab_irc.Controls.Add(this.gb_null1);
            this.tab_irc.Location = new System.Drawing.Point(4, 22);
            this.tab_irc.Name = "tab_irc";
            this.tab_irc.Padding = new System.Windows.Forms.Padding(3);
            this.tab_irc.Size = new System.Drawing.Size(419, 288);
            this.tab_irc.TabIndex = 0;
            this.tab_irc.Text = "IRC HM";
            this.tab_irc.UseVisualStyleBackColor = true;
            // 
            // tab_rss
            // 
            this.tab_rss.Controls.Add(this.gb_null2);
            this.tab_rss.Location = new System.Drawing.Point(4, 22);
            this.tab_rss.Name = "tab_rss";
            this.tab_rss.Padding = new System.Windows.Forms.Padding(3);
            this.tab_rss.Size = new System.Drawing.Size(419, 288);
            this.tab_rss.TabIndex = 1;
            this.tab_rss.Text = "RSS HM";
            this.tab_rss.UseVisualStyleBackColor = true;
            // 
            // gb_null1
            // 
            this.gb_null1.Location = new System.Drawing.Point(8, 6);
            this.gb_null1.Name = "gb_null1";
            this.gb_null1.Size = new System.Drawing.Size(403, 274);
            this.gb_null1.TabIndex = 0;
            this.gb_null1.TabStop = false;
            this.gb_null1.Text = "IRC Hive Mind:";
            // 
            // gb_null2
            // 
            this.gb_null2.Location = new System.Drawing.Point(8, 6);
            this.gb_null2.Name = "gb_null2";
            this.gb_null2.Size = new System.Drawing.Size(403, 276);
            this.gb_null2.TabIndex = 0;
            this.gb_null2.TabStop = false;
            this.gb_null2.Text = "RSS Hive Mind:";
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
            this.tab_irc.ResumeLayout(false);
            this.tab_rss.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Timer timer1;
        private System.Windows.Forms.TabControl tab_control;
        private System.Windows.Forms.TabPage tab_irc;
        private System.Windows.Forms.TabPage tab_rss;
        private System.Windows.Forms.GroupBox gb_null1;
        private System.Windows.Forms.GroupBox gb_null2;
    }
}