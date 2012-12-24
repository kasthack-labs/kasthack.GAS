namespace GAS
{
    partial class frm_main
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(frm_main));
            this.property_table = new System.Windows.Forms.PropertyGrid();
            this.status_null = new System.Windows.Forms.StatusStrip();
            this.splitContainer1 = new System.Windows.Forms.SplitContainer();
            this.dgv_status = new System.Windows.Forms.DataGridView();
            this.dataGridViewTextBoxColumn2 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.dataGridViewTextBoxColumn3 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.dataGridViewTextBoxColumn4 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.dataGridViewTextBoxColumn5 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.dataGridViewTextBoxColumn6 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.dataGridViewTextBoxColumn7 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.dataGridViewTextBoxColumn8 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.button5 = new System.Windows.Forms.Button();
            this.drv_targets = new System.Windows.Forms.DataGridView();
            this.cell_State = new System.Windows.Forms.DataGridViewCheckBoxColumn();
            this.cell_IP = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.cell_Attack_Method = new System.Windows.Forms.DataGridViewComboBoxColumn();
            this.cell_admin = new System.Windows.Forms.DataGridViewButtonColumn();
            this.button4 = new System.Windows.Forms.Button();
            this.button1 = new System.Windows.Forms.Button();
            this.button3 = new System.Windows.Forms.Button();
            this.button2 = new System.Windows.Forms.Button();
            this.pic_logo = new System.Windows.Forms.PictureBox();
            this.btn_about = new System.Windows.Forms.Button();
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer1)).BeginInit();
            this.splitContainer1.Panel1.SuspendLayout();
            this.splitContainer1.Panel2.SuspendLayout();
            this.splitContainer1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgv_status)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.drv_targets)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.pic_logo)).BeginInit();
            this.SuspendLayout();
            // 
            // property_table
            // 
            this.property_table.Dock = System.Windows.Forms.DockStyle.Fill;
            this.property_table.Location = new System.Drawing.Point(0, 0);
            this.property_table.Name = "property_table";
            this.property_table.PropertySort = System.Windows.Forms.PropertySort.Alphabetical;
            this.property_table.Size = new System.Drawing.Size(197, 468);
            this.property_table.TabIndex = 2;
            // 
            // status_null
            // 
            this.status_null.Location = new System.Drawing.Point(0, 470);
            this.status_null.Name = "status_null";
            this.status_null.Size = new System.Drawing.Size(884, 22);
            this.status_null.TabIndex = 9;
            // 
            // splitContainer1
            // 
            this.splitContainer1.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.splitContainer1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.splitContainer1.Location = new System.Drawing.Point(0, 0);
            this.splitContainer1.Name = "splitContainer1";
            // 
            // splitContainer1.Panel1
            // 
            this.splitContainer1.Panel1.Controls.Add(this.dgv_status);
            this.splitContainer1.Panel1.Controls.Add(this.button5);
            this.splitContainer1.Panel1.Controls.Add(this.drv_targets);
            this.splitContainer1.Panel1.Controls.Add(this.button4);
            this.splitContainer1.Panel1.Controls.Add(this.button1);
            this.splitContainer1.Panel1.Controls.Add(this.button3);
            this.splitContainer1.Panel1.Controls.Add(this.button2);
            this.splitContainer1.Panel1.Controls.Add(this.pic_logo);
            // 
            // splitContainer1.Panel2
            // 
            this.splitContainer1.Panel2.Controls.Add(this.property_table);
            this.splitContainer1.Size = new System.Drawing.Size(884, 470);
            this.splitContainer1.SplitterDistance = 681;
            this.splitContainer1.TabIndex = 16;
            // 
            // dgv_status
            // 
            this.dgv_status.AllowUserToAddRows = false;
            this.dgv_status.AllowUserToDeleteRows = false;
            this.dgv_status.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.dgv_status.BackgroundColor = System.Drawing.Color.White;
            this.dgv_status.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgv_status.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.dataGridViewTextBoxColumn2,
            this.dataGridViewTextBoxColumn3,
            this.dataGridViewTextBoxColumn4,
            this.dataGridViewTextBoxColumn5,
            this.dataGridViewTextBoxColumn6,
            this.dataGridViewTextBoxColumn7,
            this.dataGridViewTextBoxColumn8});
            this.dgv_status.GridColor = System.Drawing.Color.White;
            this.dgv_status.Location = new System.Drawing.Point(190, 320);
            this.dgv_status.Name = "dgv_status";
            this.dgv_status.ReadOnly = true;
            this.dgv_status.Size = new System.Drawing.Size(486, 116);
            this.dgv_status.TabIndex = 22;
            // 
            // dataGridViewTextBoxColumn2
            // 
            this.dataGridViewTextBoxColumn2.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.Fill;
            this.dataGridViewTextBoxColumn2.HeaderText = "Idle";
            this.dataGridViewTextBoxColumn2.Name = "dataGridViewTextBoxColumn2";
            this.dataGridViewTextBoxColumn2.ReadOnly = true;
            // 
            // dataGridViewTextBoxColumn3
            // 
            this.dataGridViewTextBoxColumn3.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.Fill;
            this.dataGridViewTextBoxColumn3.HeaderText = "Connecting";
            this.dataGridViewTextBoxColumn3.Name = "dataGridViewTextBoxColumn3";
            this.dataGridViewTextBoxColumn3.ReadOnly = true;
            // 
            // dataGridViewTextBoxColumn4
            // 
            this.dataGridViewTextBoxColumn4.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.Fill;
            this.dataGridViewTextBoxColumn4.HeaderText = "Requesting";
            this.dataGridViewTextBoxColumn4.Name = "dataGridViewTextBoxColumn4";
            this.dataGridViewTextBoxColumn4.ReadOnly = true;
            // 
            // dataGridViewTextBoxColumn5
            // 
            this.dataGridViewTextBoxColumn5.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.Fill;
            this.dataGridViewTextBoxColumn5.HeaderText = "Downloading";
            this.dataGridViewTextBoxColumn5.Name = "dataGridViewTextBoxColumn5";
            this.dataGridViewTextBoxColumn5.ReadOnly = true;
            // 
            // dataGridViewTextBoxColumn6
            // 
            this.dataGridViewTextBoxColumn6.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.Fill;
            this.dataGridViewTextBoxColumn6.HeaderText = "Downloaded";
            this.dataGridViewTextBoxColumn6.Name = "dataGridViewTextBoxColumn6";
            this.dataGridViewTextBoxColumn6.ReadOnly = true;
            // 
            // dataGridViewTextBoxColumn7
            // 
            this.dataGridViewTextBoxColumn7.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.Fill;
            this.dataGridViewTextBoxColumn7.HeaderText = "Requested";
            this.dataGridViewTextBoxColumn7.Name = "dataGridViewTextBoxColumn7";
            this.dataGridViewTextBoxColumn7.ReadOnly = true;
            // 
            // dataGridViewTextBoxColumn8
            // 
            this.dataGridViewTextBoxColumn8.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.Fill;
            this.dataGridViewTextBoxColumn8.HeaderText = "Failed";
            this.dataGridViewTextBoxColumn8.Name = "dataGridViewTextBoxColumn8";
            this.dataGridViewTextBoxColumn8.ReadOnly = true;
            // 
            // button5
            // 
            this.button5.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.button5.Image = global::GAS.Properties.Resources.del;
            this.button5.Location = new System.Drawing.Point(653, 442);
            this.button5.Name = "button5";
            this.button5.Size = new System.Drawing.Size(23, 23);
            this.button5.TabIndex = 21;
            this.button5.UseVisualStyleBackColor = true;
            // 
            // drv_targets
            // 
            this.drv_targets.AllowUserToAddRows = false;
            this.drv_targets.AllowUserToDeleteRows = false;
            this.drv_targets.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.drv_targets.BackgroundColor = System.Drawing.Color.White;
            this.drv_targets.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.drv_targets.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.cell_State,
            this.cell_IP,
            this.cell_Attack_Method,
            this.cell_admin});
            this.drv_targets.GridColor = System.Drawing.Color.White;
            this.drv_targets.Location = new System.Drawing.Point(190, 11);
            this.drv_targets.Name = "drv_targets";
            this.drv_targets.Size = new System.Drawing.Size(486, 303);
            this.drv_targets.TabIndex = 16;
            // 
            // cell_State
            // 
            this.cell_State.HeaderText = "";
            this.cell_State.Name = "cell_State";
            this.cell_State.Resizable = System.Windows.Forms.DataGridViewTriState.False;
            this.cell_State.Width = 40;
            // 
            // cell_IP
            // 
            this.cell_IP.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.Fill;
            this.cell_IP.HeaderText = "IP";
            this.cell_IP.Name = "cell_IP";
            // 
            // cell_Attack_Method
            // 
            this.cell_Attack_Method.HeaderText = "Attack Method";
            this.cell_Attack_Method.Items.AddRange(new object[] {
            "TCP",
            "UDP",
            "HTTP",
            "ReCoil",
            "SlowLOIC",
            "RefRef",
            "AhrDosme",
            "SlowPost",
            "Post",
            "TMOF"});
            this.cell_Attack_Method.Name = "cell_Attack_Method";
            this.cell_Attack_Method.Resizable = System.Windows.Forms.DataGridViewTriState.False;
            this.cell_Attack_Method.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.Automatic;
            // 
            // cell_admin
            // 
            this.cell_admin.HeaderText = "Play \\ Stop";
            this.cell_admin.Name = "cell_admin";
            this.cell_admin.Resizable = System.Windows.Forms.DataGridViewTriState.True;
            this.cell_admin.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.Automatic;
            this.cell_admin.Text = "Play \\ Stop";
            // 
            // button4
            // 
            this.button4.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.button4.Image = global::GAS.Properties.Resources.add;
            this.button4.Location = new System.Drawing.Point(624, 442);
            this.button4.Name = "button4";
            this.button4.Size = new System.Drawing.Size(23, 23);
            this.button4.TabIndex = 20;
            this.button4.UseVisualStyleBackColor = true;
            // 
            // button1
            // 
            this.button1.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.button1.Image = global::GAS.Properties.Resources.net;
            this.button1.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.button1.Location = new System.Drawing.Point(190, 442);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(82, 23);
            this.button1.TabIndex = 17;
            this.button1.Text = "Hive Mind";
            this.button1.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.button1.UseVisualStyleBackColor = true;
            this.button1.Click += new System.EventHandler(this.button1_Click);
            // 
            // button3
            // 
            this.button3.Anchor = System.Windows.Forms.AnchorStyles.Bottom;
            this.button3.Image = global::GAS.Properties.Resources.stopall;
            this.button3.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.button3.Location = new System.Drawing.Point(440, 442);
            this.button3.Name = "button3";
            this.button3.Size = new System.Drawing.Size(70, 23);
            this.button3.TabIndex = 19;
            this.button3.Text = "Stop All";
            this.button3.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.button3.UseVisualStyleBackColor = true;
            // 
            // button2
            // 
            this.button2.Anchor = System.Windows.Forms.AnchorStyles.Bottom;
            this.button2.Image = global::GAS.Properties.Resources.playall;
            this.button2.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.button2.Location = new System.Drawing.Point(364, 442);
            this.button2.Name = "button2";
            this.button2.Size = new System.Drawing.Size(70, 23);
            this.button2.TabIndex = 18;
            this.button2.Text = "Start All";
            this.button2.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.button2.UseVisualStyleBackColor = true;
            // 
            // pic_logo
            // 
            this.pic_logo.BackColor = System.Drawing.Color.Gray;
            this.pic_logo.BackgroundImage = global::GAS.Properties.Resources.tmp_bkgrnd;
            this.pic_logo.Dock = System.Windows.Forms.DockStyle.Left;
            this.pic_logo.Image = global::GAS.Properties.Resources.New_logo2;
            this.pic_logo.InitialImage = null;
            this.pic_logo.Location = new System.Drawing.Point(0, 0);
            this.pic_logo.Name = "pic_logo";
            this.pic_logo.Size = new System.Drawing.Size(184, 468);
            this.pic_logo.SizeMode = System.Windows.Forms.PictureBoxSizeMode.Zoom;
            this.pic_logo.TabIndex = 3;
            this.pic_logo.TabStop = false;
            // 
            // btn_about
            // 
            this.btn_about.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.btn_about.Image = global::GAS.Properties.Resources.inf;
            this.btn_about.Location = new System.Drawing.Point(1, 471);
            this.btn_about.Name = "btn_about";
            this.btn_about.Size = new System.Drawing.Size(22, 22);
            this.btn_about.TabIndex = 17;
            this.btn_about.Text = "\r\n";
            this.btn_about.UseVisualStyleBackColor = true;
            this.btn_about.Click += new System.EventHandler(this.btn_about_Click);
            // 
            // frm_main
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(884, 492);
            this.Controls.Add(this.btn_about);
            this.Controls.Add(this.splitContainer1);
            this.Controls.Add(this.status_null);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MinimumSize = new System.Drawing.Size(800, 530);
            this.Name = "frm_main";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "GAS: New GUI. applause, ladies and gentlemen!";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.frm_main_FormClosing);
            this.Load += new System.EventHandler(this.frm_main_Load);
            this.splitContainer1.Panel1.ResumeLayout(false);
            this.splitContainer1.Panel2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer1)).EndInit();
            this.splitContainer1.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.dgv_status)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.drv_targets)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.pic_logo)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.PropertyGrid property_table;
        private System.Windows.Forms.PictureBox pic_logo;
        private System.Windows.Forms.StatusStrip status_null;
        private System.Windows.Forms.SplitContainer splitContainer1;
        private System.Windows.Forms.DataGridView dgv_status;
        private System.Windows.Forms.DataGridViewTextBoxColumn dataGridViewTextBoxColumn2;
        private System.Windows.Forms.DataGridViewTextBoxColumn dataGridViewTextBoxColumn3;
        private System.Windows.Forms.DataGridViewTextBoxColumn dataGridViewTextBoxColumn4;
        private System.Windows.Forms.DataGridViewTextBoxColumn dataGridViewTextBoxColumn5;
        private System.Windows.Forms.DataGridViewTextBoxColumn dataGridViewTextBoxColumn6;
        private System.Windows.Forms.DataGridViewTextBoxColumn dataGridViewTextBoxColumn7;
        private System.Windows.Forms.DataGridViewTextBoxColumn dataGridViewTextBoxColumn8;
        private System.Windows.Forms.Button button5;
        private System.Windows.Forms.DataGridView drv_targets;
        private System.Windows.Forms.Button button4;
        private System.Windows.Forms.Button button1;
        private System.Windows.Forms.Button button3;
        private System.Windows.Forms.Button button2;
        private System.Windows.Forms.DataGridViewCheckBoxColumn cell_State;
        private System.Windows.Forms.DataGridViewTextBoxColumn cell_IP;
        private System.Windows.Forms.DataGridViewComboBoxColumn cell_Attack_Method;
        private System.Windows.Forms.DataGridViewButtonColumn cell_admin;
        private System.Windows.Forms.Button btn_about;
    }
}