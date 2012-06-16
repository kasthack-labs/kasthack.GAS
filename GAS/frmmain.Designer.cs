namespace GAS
{
    partial class frmmain
    {
        /// <summary>
        /// Требуется переменная конструктора.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Освободить все используемые ресурсы.
        /// </summary>
        /// <param name="disposing">истинно, если управляемый ресурс должен быть удален; иначе ложно.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Код, автоматически созданный конструктором форм Windows

        /// <summary>
        /// Обязательный метод для поддержки конструктора - не изменяйте
        /// содержимое данного метода при помощи редактора кода.
        /// </summary>
        private void InitializeComponent()
        {
            this.components = new System.ComponentModel.Container();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.cmdTargetIP = new System.Windows.Forms.Button();
            this.txtTargetIP = new System.Windows.Forms.TextBox();
            this.label1 = new System.Windows.Forms.Label();
            this.label19 = new System.Windows.Forms.Label();
            this.lbFailed = new System.Windows.Forms.Label();
            this.lbRequested = new System.Windows.Forms.Label();
            this.label22 = new System.Windows.Forms.Label();
            this.label23 = new System.Windows.Forms.Label();
            this.lbDownloaded = new System.Windows.Forms.Label();
            this.lbDownloading = new System.Windows.Forms.Label();
            this.lbRequesting = new System.Windows.Forms.Label();
            this.label14 = new System.Windows.Forms.Label();
            this.label15 = new System.Windows.Forms.Label();
            this.label16 = new System.Windows.Forms.Label();
            this.tShowStats = new System.Windows.Forms.Timer(this.components);
            this.TTip = new System.Windows.Forms.ToolTip(this.components);
            this.lbConnecting = new System.Windows.Forms.Label();
            this.lbIdle = new System.Windows.Forms.Label();
            this.txtTarget = new System.Windows.Forms.TextBox();
            this.cmdAttack = new System.Windows.Forms.Button();
            this.chkUsegZip = new System.Windows.Forms.CheckBox();
            this.chkUseGet = new System.Windows.Forms.CheckBox();
            this.chkResp = new System.Windows.Forms.CheckBox();
            this.txtData = new System.Windows.Forms.TextBox();
            this.txtSubsite = new System.Windows.Forms.TextBox();
            this.cbMethod = new System.Windows.Forms.ComboBox();
            this.label13 = new System.Windows.Forms.Label();
            this.label12 = new System.Windows.Forms.Label();
            this.groupBox5 = new System.Windows.Forms.GroupBox();
            this.label11 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.groupBox4 = new System.Windows.Forms.GroupBox();
            this.label10 = new System.Windows.Forms.Label();
            this.label5 = new System.Windows.Forms.Label();
            this.groupBox2 = new System.Windows.Forms.GroupBox();
            this.label4 = new System.Windows.Forms.Label();
            this.chkMsgRandom = new System.Windows.Forms.CheckBox();
            this.chkRandom = new System.Windows.Forms.CheckBox();
            this.label18 = new System.Windows.Forms.Label();
            this.label17 = new System.Windows.Forms.Label();
            this.label9 = new System.Windows.Forms.Label();
            this.label7 = new System.Windows.Forms.Label();
            this.label6 = new System.Windows.Forms.Label();
            this.label8 = new System.Windows.Forms.Label();
            this.label20 = new System.Windows.Forms.Label();
            this.grp_Setting = new System.Windows.Forms.GroupBox();
            this.nudSLPT = new System.Windows.Forms.NumericUpDown();
            this.nudThreadNum = new System.Windows.Forms.NumericUpDown();
            this.nudPort = new System.Windows.Forms.NumericUpDown();
            this.nudTimeout = new System.Windows.Forms.NumericUpDown();
            this.tbSpeed = new System.Windows.Forms.TrackBar();
            this.groupBox1.SuspendLayout();
            this.groupBox5.SuspendLayout();
            this.groupBox4.SuspendLayout();
            this.groupBox2.SuspendLayout();
            this.grp_Setting.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize) (this.nudSLPT)).BeginInit();
            ((System.ComponentModel.ISupportInitialize) (this.nudThreadNum)).BeginInit();
            ((System.ComponentModel.ISupportInitialize) (this.nudPort)).BeginInit();
            ((System.ComponentModel.ISupportInitialize) (this.nudTimeout)).BeginInit();
            ((System.ComponentModel.ISupportInitialize) (this.tbSpeed)).BeginInit();
            this.SuspendLayout();
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.cmdTargetIP);
            this.groupBox1.Controls.Add(this.txtTargetIP);
            this.groupBox1.Controls.Add(this.label1);
            this.groupBox1.ForeColor = System.Drawing.Color.LightBlue;
            this.groupBox1.Location = new System.Drawing.Point(12, 6);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(465, 78);
            this.groupBox1.TabIndex = 13;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "1. Select your target";
            // 
            // cmdTargetIP
            // 
            this.cmdTargetIP.BackColor = System.Drawing.Color.FromArgb(((int) (((byte) (32)))), ((int) (((byte) (64)))), ((int) (((byte) (96)))));
            this.cmdTargetIP.ForeColor = System.Drawing.Color.Azure;
            this.cmdTargetIP.Location = new System.Drawing.Point(388, 23);
            this.cmdTargetIP.Name = "cmdTargetIP";
            this.cmdTargetIP.Size = new System.Drawing.Size(71, 25);
            this.cmdTargetIP.TabIndex = 4;
            this.cmdTargetIP.Text = "Lock on";
            this.cmdTargetIP.UseVisualStyleBackColor = false;
            this.cmdTargetIP.Click += new System.EventHandler(this.cmdTargetIP_Click);
            // 
            // txtTargetIP
            // 
            this.txtTargetIP.BackColor = System.Drawing.Color.FromArgb(((int) (((byte) (24)))), ((int) (((byte) (48)))), ((int) (((byte) (64)))));
            this.txtTargetIP.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.txtTargetIP.ForeColor = System.Drawing.Color.Azure;
            this.txtTargetIP.Location = new System.Drawing.Point(63, 23);
            this.txtTargetIP.Name = "txtTargetIP";
            this.txtTargetIP.Size = new System.Drawing.Size(319, 20);
            this.txtTargetIP.TabIndex = 3;
            this.txtTargetIP.Text = "http://kremlin.ru";
            this.TTip.SetToolTip(this.txtTargetIP, "If you know your target\'s IP, enter the IP here and click \"Lock on\"");
            // 
            // label1
            // 
            this.label1.Location = new System.Drawing.Point(6, 23);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(51, 17);
            this.label1.TabIndex = 2;
            this.label1.Text = "URL/IP";
            this.label1.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // label19
            // 
            this.label19.BackColor = System.Drawing.Color.Azure;
            this.label19.Location = new System.Drawing.Point(6, 40);
            this.label19.Name = "label19";
            this.label19.Size = new System.Drawing.Size(746, 10);
            this.label19.TabIndex = 25;
            this.label19.Text = "Idle";
            this.label19.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // lbFailed
            // 
            this.lbFailed.Location = new System.Drawing.Point(648, 41);
            this.lbFailed.Name = "lbFailed";
            this.lbFailed.Size = new System.Drawing.Size(101, 27);
            this.lbFailed.TabIndex = 24;
            this.lbFailed.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            this.TTip.SetToolTip(this.lbFailed, "How many times (in total) the webserver didn\'t respond. High number = server down" +
                    ".");
            // 
            // lbRequested
            // 
            this.lbRequested.Location = new System.Drawing.Point(541, 41);
            this.lbRequested.Name = "lbRequested";
            this.lbRequested.Size = new System.Drawing.Size(101, 27);
            this.lbRequested.TabIndex = 23;
            this.lbRequested.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            this.TTip.SetToolTip(this.lbRequested, "How many times (in total) a download has been requested");
            // 
            // label22
            // 
            this.label22.Location = new System.Drawing.Point(648, 16);
            this.label22.Name = "label22";
            this.label22.Size = new System.Drawing.Size(101, 27);
            this.label22.TabIndex = 22;
            this.label22.Text = "Failed";
            this.label22.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // label23
            // 
            this.label23.Location = new System.Drawing.Point(541, 16);
            this.label23.Name = "label23";
            this.label23.Size = new System.Drawing.Size(101, 27);
            this.label23.TabIndex = 21;
            this.label23.Text = "Requested";
            this.label23.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // lbDownloaded
            // 
            this.lbDownloaded.Location = new System.Drawing.Point(434, 41);
            this.lbDownloaded.Name = "lbDownloaded";
            this.lbDownloaded.Size = new System.Drawing.Size(101, 27);
            this.lbDownloaded.TabIndex = 20;
            this.lbDownloaded.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            this.TTip.SetToolTip(this.lbDownloaded, "How many times (in total) that a download has been initiated");
            // 
            // lbDownloading
            // 
            this.lbDownloading.BackColor = System.Drawing.Color.FromArgb(((int) (((byte) (12)))), ((int) (((byte) (24)))), ((int) (((byte) (32)))));
            this.lbDownloading.Location = new System.Drawing.Point(327, 41);
            this.lbDownloading.Name = "lbDownloading";
            this.lbDownloading.Size = new System.Drawing.Size(101, 27);
            this.lbDownloading.TabIndex = 19;
            this.lbDownloading.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            this.TTip.SetToolTip(this.lbDownloading, "How many threads that are downloading information from the server");
            // 
            // lbRequesting
            // 
            this.lbRequesting.Location = new System.Drawing.Point(220, 41);
            this.lbRequesting.Name = "lbRequesting";
            this.lbRequesting.Size = new System.Drawing.Size(101, 27);
            this.lbRequesting.TabIndex = 18;
            this.lbRequesting.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            this.TTip.SetToolTip(this.lbRequesting, "How many threads that are requesting information from the server");
            // 
            // label14
            // 
            this.label14.Location = new System.Drawing.Point(220, 16);
            this.label14.Name = "label14";
            this.label14.Size = new System.Drawing.Size(101, 27);
            this.label14.TabIndex = 13;
            this.label14.Text = "Requesting";
            this.label14.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // label15
            // 
            this.label15.Location = new System.Drawing.Point(113, 16);
            this.label15.Name = "label15";
            this.label15.Size = new System.Drawing.Size(101, 27);
            this.label15.TabIndex = 12;
            this.label15.Text = "Connecting";
            this.label15.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // label16
            // 
            this.label16.Location = new System.Drawing.Point(6, 16);
            this.label16.Name = "label16";
            this.label16.Size = new System.Drawing.Size(101, 27);
            this.label16.TabIndex = 11;
            this.label16.Text = "Idle";
            this.label16.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // tShowStats
            // 
            this.tShowStats.Interval = 10;
            this.tShowStats.Tick += new System.EventHandler(this.tShowStats_Tick);
            // 
            // lbConnecting
            // 
            this.lbConnecting.Location = new System.Drawing.Point(113, 41);
            this.lbConnecting.Name = "lbConnecting";
            this.lbConnecting.Size = new System.Drawing.Size(101, 27);
            this.lbConnecting.TabIndex = 17;
            this.lbConnecting.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            this.TTip.SetToolTip(this.lbConnecting, "How many threads that are trying to connect");
            // 
            // lbIdle
            // 
            this.lbIdle.Location = new System.Drawing.Point(6, 41);
            this.lbIdle.Name = "lbIdle";
            this.lbIdle.Size = new System.Drawing.Size(101, 27);
            this.lbIdle.TabIndex = 16;
            this.lbIdle.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            this.TTip.SetToolTip(this.lbIdle, "How many threads that are without work. Should be 0");
            // 
            // txtTarget
            // 
            this.txtTarget.BackColor = System.Drawing.Color.FromArgb(((int) (((byte) (24)))), ((int) (((byte) (48)))), ((int) (((byte) (64)))));
            this.txtTarget.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.txtTarget.Font = new System.Drawing.Font("Arial", 48F, ((System.Drawing.FontStyle) ((System.Drawing.FontStyle.Bold | System.Drawing.FontStyle.Italic))), System.Drawing.GraphicsUnit.Point, ((byte) (0)));
            this.txtTarget.ForeColor = System.Drawing.Color.Azure;
            this.txtTarget.Location = new System.Drawing.Point(6, 19);
            this.txtTarget.Name = "txtTarget";
            this.txtTarget.Size = new System.Drawing.Size(746, 81);
            this.txtTarget.TabIndex = 1;
            this.txtTarget.TabStop = false;
            this.txtTarget.Text = "N O N E !";
            this.txtTarget.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            this.TTip.SetToolTip(this.txtTarget, "The currently selected target");
            // 
            // cmdAttack
            // 
            this.cmdAttack.BackColor = System.Drawing.Color.FromArgb(((int) (((byte) (32)))), ((int) (((byte) (64)))), ((int) (((byte) (96)))));
            this.cmdAttack.Font = new System.Drawing.Font("Arial", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte) (0)));
            this.cmdAttack.ForeColor = System.Drawing.Color.Azure;
            this.cmdAttack.Location = new System.Drawing.Point(6, 19);
            this.cmdAttack.Name = "cmdAttack";
            this.cmdAttack.Size = new System.Drawing.Size(246, 53);
            this.cmdAttack.TabIndex = 1;
            this.cmdAttack.Text = "IMMA CHARGIN MAH LAZER";
            this.TTip.SetToolTip(this.cmdAttack, "I sincerely hope you can guess what this button does.");
            this.cmdAttack.UseVisualStyleBackColor = false;
            this.cmdAttack.Click += new System.EventHandler(this.cmdAttack_Click);
            // 
            // chkUsegZip
            // 
            this.chkUsegZip.AutoSize = true;
            this.chkUsegZip.Checked = true;
            this.chkUsegZip.CheckState = System.Windows.Forms.CheckState.Checked;
            this.chkUsegZip.Location = new System.Drawing.Point(655, 34);
            this.chkUsegZip.Name = "chkUsegZip";
            this.chkUsegZip.Size = new System.Drawing.Size(67, 17);
            this.chkUsegZip.TabIndex = 33;
            this.chkUsegZip.Text = "use gZip";
            this.TTip.SetToolTip(this.chkUsegZip, "If checked it adds gZip compression to the slowLOIC attack");
            this.chkUsegZip.UseVisualStyleBackColor = true;
            // 
            // chkUseGet
            // 
            this.chkUseGet.AutoSize = true;
            this.chkUseGet.Checked = true;
            this.chkUseGet.CheckState = System.Windows.Forms.CheckState.Checked;
            this.chkUseGet.Location = new System.Drawing.Point(655, 13);
            this.chkUseGet.Name = "chkUseGet";
            this.chkUseGet.Size = new System.Drawing.Size(68, 17);
            this.chkUseGet.TabIndex = 32;
            this.chkUseGet.Text = "use GET";
            this.TTip.SetToolTip(this.chkUseGet, "If checked it uses the GET method instead of POST.");
            this.chkUseGet.UseVisualStyleBackColor = true;
            // 
            // chkResp
            // 
            this.chkResp.AutoSize = true;
            this.chkResp.Location = new System.Drawing.Point(231, 72);
            this.chkResp.Name = "chkResp";
            this.chkResp.Size = new System.Drawing.Size(88, 17);
            this.chkResp.TabIndex = 7;
            this.chkResp.Text = "Wait for reply";
            this.TTip.SetToolTip(this.chkResp, "Don\'t disconnect before the server\'s started to answer");
            this.chkResp.UseVisualStyleBackColor = true;
            // 
            // txtData
            // 
            this.txtData.BackColor = System.Drawing.Color.FromArgb(((int) (((byte) (24)))), ((int) (((byte) (48)))), ((int) (((byte) (64)))));
            this.txtData.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.txtData.Font = new System.Drawing.Font("Arial", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte) (0)));
            this.txtData.ForeColor = System.Drawing.Color.Azure;
            this.txtData.Location = new System.Drawing.Point(330, 34);
            this.txtData.Name = "txtData";
            this.txtData.Size = new System.Drawing.Size(312, 20);
            this.txtData.TabIndex = 3;
            this.txtData.Text = "U dun goofed";
            this.txtData.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            this.TTip.SetToolTip(this.txtData, "The data to send in TCP/UDP mode");
            // 
            // txtSubsite
            // 
            this.txtSubsite.BackColor = System.Drawing.Color.FromArgb(((int) (((byte) (24)))), ((int) (((byte) (48)))), ((int) (((byte) (64)))));
            this.txtSubsite.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.txtSubsite.Font = new System.Drawing.Font("Arial", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte) (0)));
            this.txtSubsite.ForeColor = System.Drawing.Color.Azure;
            this.txtSubsite.Location = new System.Drawing.Point(62, 34);
            this.txtSubsite.Name = "txtSubsite";
            this.txtSubsite.Size = new System.Drawing.Size(259, 20);
            this.txtSubsite.TabIndex = 2;
            this.txtSubsite.Text = "/";
            this.txtSubsite.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            this.TTip.SetToolTip(this.txtSubsite, "What subsite to target (when using HTTP as type)");
            // 
            // cbMethod
            // 
            this.cbMethod.BackColor = System.Drawing.Color.FromArgb(((int) (((byte) (24)))), ((int) (((byte) (48)))), ((int) (((byte) (64)))));
            this.cbMethod.FlatStyle = System.Windows.Forms.FlatStyle.Popup;
            this.cbMethod.Font = new System.Drawing.Font("Arial", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte) (0)));
            this.cbMethod.ForeColor = System.Drawing.Color.Azure;
            this.cbMethod.FormattingEnabled = true;
            this.cbMethod.Items.AddRange(new object[] {
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
            this.cbMethod.Location = new System.Drawing.Point(64, 69);
            this.cbMethod.Name = "cbMethod";
            this.cbMethod.Size = new System.Drawing.Size(75, 22);
            this.cbMethod.TabIndex = 5;
            this.cbMethod.Text = "ReCoil";
            this.TTip.SetToolTip(this.cbMethod, "What type of attack to launch");
            this.cbMethod.SelectedIndexChanged += new System.EventHandler(this.cbMethod_SelectedIndexChanged);
            // 
            // label13
            // 
            this.label13.Location = new System.Drawing.Point(327, 16);
            this.label13.Name = "label13";
            this.label13.Size = new System.Drawing.Size(101, 27);
            this.label13.TabIndex = 14;
            this.label13.Text = "Downloading";
            this.label13.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // label12
            // 
            this.label12.Location = new System.Drawing.Point(434, 16);
            this.label12.Name = "label12";
            this.label12.Size = new System.Drawing.Size(101, 27);
            this.label12.TabIndex = 15;
            this.label12.Text = "Downloaded";
            this.label12.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // groupBox5
            // 
            this.groupBox5.Controls.Add(this.label19);
            this.groupBox5.Controls.Add(this.lbFailed);
            this.groupBox5.Controls.Add(this.lbRequested);
            this.groupBox5.Controls.Add(this.label22);
            this.groupBox5.Controls.Add(this.label23);
            this.groupBox5.Controls.Add(this.lbDownloaded);
            this.groupBox5.Controls.Add(this.lbDownloading);
            this.groupBox5.Controls.Add(this.lbRequesting);
            this.groupBox5.Controls.Add(this.lbConnecting);
            this.groupBox5.Controls.Add(this.lbIdle);
            this.groupBox5.Controls.Add(this.label12);
            this.groupBox5.Controls.Add(this.label13);
            this.groupBox5.Controls.Add(this.label14);
            this.groupBox5.Controls.Add(this.label15);
            this.groupBox5.Controls.Add(this.label16);
            this.groupBox5.ForeColor = System.Drawing.Color.LightBlue;
            this.groupBox5.Location = new System.Drawing.Point(12, 374);
            this.groupBox5.Name = "groupBox5";
            this.groupBox5.Size = new System.Drawing.Size(758, 71);
            this.groupBox5.TabIndex = 17;
            this.groupBox5.TabStop = false;
            this.groupBox5.Text = "Attack status";
            // 
            // label11
            // 
            this.label11.Location = new System.Drawing.Point(12, 374);
            this.label11.Name = "label11";
            this.label11.Size = new System.Drawing.Size(23, 26);
            this.label11.TabIndex = 21;
            // 
            // label3
            // 
            this.label3.Location = new System.Drawing.Point(12, 226);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(23, 26);
            this.label3.TabIndex = 19;
            // 
            // groupBox4
            // 
            this.groupBox4.Controls.Add(this.cmdAttack);
            this.groupBox4.ForeColor = System.Drawing.Color.LightBlue;
            this.groupBox4.Location = new System.Drawing.Point(512, 6);
            this.groupBox4.Name = "groupBox4";
            this.groupBox4.Size = new System.Drawing.Size(258, 78);
            this.groupBox4.TabIndex = 14;
            this.groupBox4.TabStop = false;
            this.groupBox4.Text = "2. Ready?";
            // 
            // label10
            // 
            this.label10.Location = new System.Drawing.Point(483, 3);
            this.label10.Name = "label10";
            this.label10.Size = new System.Drawing.Size(23, 26);
            this.label10.TabIndex = 20;
            // 
            // label5
            // 
            this.label5.Location = new System.Drawing.Point(12, 84);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(23, 26);
            this.label5.TabIndex = 18;
            // 
            // groupBox2
            // 
            this.groupBox2.Controls.Add(this.txtTarget);
            this.groupBox2.ForeColor = System.Drawing.Color.LightBlue;
            this.groupBox2.Location = new System.Drawing.Point(12, 100);
            this.groupBox2.Name = "groupBox2";
            this.groupBox2.Size = new System.Drawing.Size(758, 109);
            this.groupBox2.TabIndex = 15;
            this.groupBox2.TabStop = false;
            this.groupBox2.Text = "Selected target";
            // 
            // label4
            // 
            this.label4.Location = new System.Drawing.Point(327, 95);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(92, 18);
            this.label4.TabIndex = 31;
            this.label4.Text = "Sockets / Thread";
            this.label4.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // chkMsgRandom
            // 
            this.chkMsgRandom.Checked = true;
            this.chkMsgRandom.CheckState = System.Windows.Forms.CheckState.Checked;
            this.chkMsgRandom.Location = new System.Drawing.Point(437, 14);
            this.chkMsgRandom.Name = "chkMsgRandom";
            this.chkMsgRandom.Size = new System.Drawing.Size(212, 21);
            this.chkMsgRandom.TabIndex = 29;
            this.chkMsgRandom.Text = "Append random chars to the message";
            // 
            // chkRandom
            // 
            this.chkRandom.AutoSize = true;
            this.chkRandom.Checked = true;
            this.chkRandom.CheckState = System.Windows.Forms.CheckState.Checked;
            this.chkRandom.Location = new System.Drawing.Point(135, 13);
            this.chkRandom.Name = "chkRandom";
            this.chkRandom.Size = new System.Drawing.Size(185, 17);
            this.chkRandom.TabIndex = 28;
            this.chkRandom.Text = "Append random chars to the URL";
            this.chkRandom.UseVisualStyleBackColor = true;
            // 
            // label18
            // 
            this.label18.Location = new System.Drawing.Point(328, 15);
            this.label18.Name = "label18";
            this.label18.Size = new System.Drawing.Size(105, 19);
            this.label18.TabIndex = 25;
            this.label18.Text = "TCP / UDP message:";
            this.label18.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // label17
            // 
            this.label17.Location = new System.Drawing.Point(62, 14);
            this.label17.Name = "label17";
            this.label17.Size = new System.Drawing.Size(77, 20);
            this.label17.TabIndex = 24;
            this.label17.Text = "HTTP Subsite:";
            this.label17.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // label9
            // 
            this.label9.Location = new System.Drawing.Point(6, 16);
            this.label9.Name = "label9";
            this.label9.Size = new System.Drawing.Size(50, 18);
            this.label9.TabIndex = 23;
            this.label9.Text = "Timeout";
            this.label9.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // label7
            // 
            this.label7.Location = new System.Drawing.Point(161, 94);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(57, 18);
            this.label7.TabIndex = 22;
            this.label7.Text = "Threads";
            this.label7.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // label6
            // 
            this.label6.Location = new System.Drawing.Point(64, 94);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(75, 18);
            this.label6.TabIndex = 21;
            this.label6.Text = "Method";
            this.label6.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // label8
            // 
            this.label8.Location = new System.Drawing.Point(6, 94);
            this.label8.Name = "label8";
            this.label8.Size = new System.Drawing.Size(60, 18);
            this.label8.TabIndex = 20;
            this.label8.Text = "Port";
            this.label8.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // label20
            // 
            this.label20.Location = new System.Drawing.Point(362, 94);
            this.label20.Name = "label20";
            this.label20.Size = new System.Drawing.Size(390, 18);
            this.label20.TabIndex = 18;
            this.label20.Text = "<= faster     Speed     slower =>";
            this.label20.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // grp_Setting
            // 
            this.grp_Setting.Controls.Add(this.nudSLPT);
            this.grp_Setting.Controls.Add(this.nudThreadNum);
            this.grp_Setting.Controls.Add(this.nudPort);
            this.grp_Setting.Controls.Add(this.nudTimeout);
            this.grp_Setting.Controls.Add(this.chkUsegZip);
            this.grp_Setting.Controls.Add(this.chkUseGet);
            this.grp_Setting.Controls.Add(this.label4);
            this.grp_Setting.Controls.Add(this.chkMsgRandom);
            this.grp_Setting.Controls.Add(this.chkRandom);
            this.grp_Setting.Controls.Add(this.label18);
            this.grp_Setting.Controls.Add(this.label17);
            this.grp_Setting.Controls.Add(this.label9);
            this.grp_Setting.Controls.Add(this.label7);
            this.grp_Setting.Controls.Add(this.label6);
            this.grp_Setting.Controls.Add(this.label8);
            this.grp_Setting.Controls.Add(this.label20);
            this.grp_Setting.Controls.Add(this.chkResp);
            this.grp_Setting.Controls.Add(this.txtData);
            this.grp_Setting.Controls.Add(this.txtSubsite);
            this.grp_Setting.Controls.Add(this.cbMethod);
            this.grp_Setting.Controls.Add(this.tbSpeed);
            this.grp_Setting.ForeColor = System.Drawing.Color.LightBlue;
            this.grp_Setting.Location = new System.Drawing.Point(15, 226);
            this.grp_Setting.Name = "grp_Setting";
            this.grp_Setting.Size = new System.Drawing.Size(757, 141);
            this.grp_Setting.TabIndex = 23;
            this.grp_Setting.TabStop = false;
            this.grp_Setting.Text = "3. Attack options";
            // 
            // nudSLPT
            // 
            this.nudSLPT.Location = new System.Drawing.Point(331, 72);
            this.nudSLPT.Maximum = new decimal(new int[] {
            999999,
            0,
            0,
            0});
            this.nudSLPT.Name = "nudSLPT";
            this.nudSLPT.Size = new System.Drawing.Size(77, 20);
            this.nudSLPT.TabIndex = 37;
            this.nudSLPT.Value = new decimal(new int[] {
            500,
            0,
            0,
            0});
            // 
            // nudThreadNum
            // 
            this.nudThreadNum.Location = new System.Drawing.Point(145, 69);
            this.nudThreadNum.Maximum = new decimal(new int[] {
            99999999,
            0,
            0,
            0});
            this.nudThreadNum.Minimum = new decimal(new int[] {
            1,
            0,
            0,
            0});
            this.nudThreadNum.Name = "nudThreadNum";
            this.nudThreadNum.Size = new System.Drawing.Size(73, 20);
            this.nudThreadNum.TabIndex = 36;
            this.nudThreadNum.Value = new decimal(new int[] {
            500,
            0,
            0,
            0});
            // 
            // nudPort
            // 
            this.nudPort.Location = new System.Drawing.Point(9, 69);
            this.nudPort.Maximum = new decimal(new int[] {
            65536,
            0,
            0,
            0});
            this.nudPort.Name = "nudPort";
            this.nudPort.Size = new System.Drawing.Size(47, 20);
            this.nudPort.TabIndex = 35;
            this.nudPort.Value = new decimal(new int[] {
            80,
            0,
            0,
            0});
            // 
            // nudTimeout
            // 
            this.nudTimeout.Location = new System.Drawing.Point(9, 35);
            this.nudTimeout.Maximum = new decimal(new int[] {
            9999999,
            0,
            0,
            0});
            this.nudTimeout.Name = "nudTimeout";
            this.nudTimeout.Size = new System.Drawing.Size(47, 20);
            this.nudTimeout.TabIndex = 34;
            this.nudTimeout.Value = new decimal(new int[] {
            30,
            0,
            0,
            0});
            // 
            // tbSpeed
            // 
            this.tbSpeed.Location = new System.Drawing.Point(414, 65);
            this.tbSpeed.Maximum = 100;
            this.tbSpeed.Name = "tbSpeed";
            this.tbSpeed.Size = new System.Drawing.Size(338, 45);
            this.tbSpeed.TabIndex = 8;
            this.tbSpeed.Value = 100;
            // 
            // frmmain
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.FromArgb(((int) (((byte) (12)))), ((int) (((byte) (24)))), ((int) (((byte) (32)))));
            this.ClientSize = new System.Drawing.Size(780, 457);
            this.Controls.Add(this.grp_Setting);
            this.Controls.Add(this.groupBox1);
            this.Controls.Add(this.groupBox5);
            this.Controls.Add(this.label11);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.groupBox4);
            this.Controls.Add(this.label10);
            this.Controls.Add(this.label5);
            this.Controls.Add(this.groupBox2);
            this.ForeColor = System.Drawing.Color.LightBlue;
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.Fixed3D;
            this.Name = "frmmain";
            this.Text = "GAS. U ll feel it just after it\'ll be turned off";
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            this.groupBox5.ResumeLayout(false);
            this.groupBox4.ResumeLayout(false);
            this.groupBox2.ResumeLayout(false);
            this.groupBox2.PerformLayout();
            this.grp_Setting.ResumeLayout(false);
            this.grp_Setting.PerformLayout();
            ((System.ComponentModel.ISupportInitialize) (this.nudSLPT)).EndInit();
            ((System.ComponentModel.ISupportInitialize) (this.nudThreadNum)).EndInit();
            ((System.ComponentModel.ISupportInitialize) (this.nudPort)).EndInit();
            ((System.ComponentModel.ISupportInitialize) (this.nudTimeout)).EndInit();
            ((System.ComponentModel.ISupportInitialize) (this.tbSpeed)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.Button cmdTargetIP;
        private System.Windows.Forms.TextBox txtTargetIP;
        private System.Windows.Forms.ToolTip TTip;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label19;
        private System.Windows.Forms.Label lbFailed;
        private System.Windows.Forms.Label lbRequested;
        private System.Windows.Forms.Label label22;
        private System.Windows.Forms.Label label23;
        private System.Windows.Forms.Label lbDownloaded;
        private System.Windows.Forms.Label lbDownloading;
        private System.Windows.Forms.Label lbRequesting;
        private System.Windows.Forms.Label label14;
        private System.Windows.Forms.Label label15;
        private System.Windows.Forms.Label label16;
        private System.Windows.Forms.Timer tShowStats;
        private System.Windows.Forms.Label lbConnecting;
        private System.Windows.Forms.Label lbIdle;
        private System.Windows.Forms.TextBox txtTarget;
        private System.Windows.Forms.Button cmdAttack;
        private System.Windows.Forms.Label label13;
        private System.Windows.Forms.Label label12;
        private System.Windows.Forms.GroupBox groupBox5;
        private System.Windows.Forms.Label label11;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.GroupBox groupBox4;
        private System.Windows.Forms.Label label10;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.GroupBox groupBox2;
        private System.Windows.Forms.CheckBox chkUsegZip;
        private System.Windows.Forms.CheckBox chkUseGet;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.CheckBox chkMsgRandom;
        private System.Windows.Forms.CheckBox chkRandom;
        private System.Windows.Forms.Label label18;
        private System.Windows.Forms.Label label17;
        private System.Windows.Forms.Label label9;
        private System.Windows.Forms.Label label7;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.Label label8;
        private System.Windows.Forms.Label label20;
        private System.Windows.Forms.CheckBox chkResp;
        private System.Windows.Forms.GroupBox grp_Setting;
        private System.Windows.Forms.TextBox txtData;
        private System.Windows.Forms.TextBox txtSubsite;
        private System.Windows.Forms.ComboBox cbMethod;
        private System.Windows.Forms.TrackBar tbSpeed;
        private System.Windows.Forms.NumericUpDown nudSLPT;
        private System.Windows.Forms.NumericUpDown nudThreadNum;
        private System.Windows.Forms.NumericUpDown nudPort;
        private System.Windows.Forms.NumericUpDown nudTimeout;
    }
}

