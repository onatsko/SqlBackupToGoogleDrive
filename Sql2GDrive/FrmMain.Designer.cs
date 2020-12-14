namespace Sql2GDrive
{
    partial class FrmMain
    {
        /// <summary>
        ///  Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        ///  Clean up any resources being used.
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
        ///  Required method for Designer support - do not modify
        ///  the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.components = new System.ComponentModel.Container();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(FrmMain));
            this.dgvJobList = new System.Windows.Forms.DataGridView();
            this.toolStrip1 = new System.Windows.Forms.ToolStrip();
            this.tsbJobAdd = new System.Windows.Forms.ToolStripButton();
            this.tsbJobEdit = new System.Windows.Forms.ToolStripButton();
            this.tsbJobDelete = new System.Windows.Forms.ToolStripButton();
            this.btnGoogleAuthorize = new System.Windows.Forms.Button();
            this.folderBrowserDialog1 = new System.Windows.Forms.FolderBrowserDialog();
            this.btnBackupOnly = new System.Windows.Forms.Button();
            this.chbSaveToFolder = new System.Windows.Forms.CheckBox();
            this.btnBackupAndUpload = new System.Windows.Forms.Button();
            this.btnFolderSelect = new System.Windows.Forms.Button();
            this.txtFolder = new System.Windows.Forms.TextBox();
            this.label1 = new System.Windows.Forms.Label();
            this.grbConnection = new System.Windows.Forms.GroupBox();
            this.label11 = new System.Windows.Forms.Label();
            this.btnConTest = new System.Windows.Forms.Button();
            this.rdbConAuthWin = new System.Windows.Forms.RadioButton();
            this.rdbConAuthSql = new System.Windows.Forms.RadioButton();
            this.label5 = new System.Windows.Forms.Label();
            this.txtConPassword = new System.Windows.Forms.TextBox();
            this.label4 = new System.Windows.Forms.Label();
            this.txtConLogin = new System.Windows.Forms.TextBox();
            this.label3 = new System.Windows.Forms.Label();
            this.txtConDatabase = new System.Windows.Forms.TextBox();
            this.label2 = new System.Windows.Forms.Label();
            this.txtConServer = new System.Windows.Forms.TextBox();
            this.grbSchedule = new System.Windows.Forms.GroupBox();
            this.label7 = new System.Windows.Forms.Label();
            this.btnRunTimeDelete = new System.Windows.Forms.Button();
            this.dgvRunTime = new System.Windows.Forms.DataGridView();
            this.dtpRunTime = new System.Windows.Forms.DateTimePicker();
            this.btnRunTimeAdd = new System.Windows.Forms.Button();
            this.label6 = new System.Windows.Forms.Label();
            this.chbSunday = new System.Windows.Forms.CheckBox();
            this.chbSaturday = new System.Windows.Forms.CheckBox();
            this.chbFriday = new System.Windows.Forms.CheckBox();
            this.chbThursday = new System.Windows.Forms.CheckBox();
            this.chbWednesday = new System.Windows.Forms.CheckBox();
            this.chbTuesday = new System.Windows.Forms.CheckBox();
            this.chbMonday = new System.Windows.Forms.CheckBox();
            this.rdbAutorunModeNone = new System.Windows.Forms.RadioButton();
            this.rdbAutorunModeAuto = new System.Windows.Forms.RadioButton();
            this.label8 = new System.Windows.Forms.Label();
            this.label9 = new System.Windows.Forms.Label();
            this.label10 = new System.Windows.Forms.Label();
            this.txtJobName = new System.Windows.Forms.TextBox();
            this.btnJobSave = new System.Windows.Forms.Button();
            this.timer1 = new System.Windows.Forms.Timer(this.components);
            this.lblCount = new System.Windows.Forms.Label();
            ((System.ComponentModel.ISupportInitialize)(this.dgvJobList)).BeginInit();
            this.toolStrip1.SuspendLayout();
            this.grbConnection.SuspendLayout();
            this.grbSchedule.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgvRunTime)).BeginInit();
            this.SuspendLayout();
            // 
            // dgvJobList
            // 
            this.dgvJobList.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left)));
            this.dgvJobList.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgvJobList.Location = new System.Drawing.Point(12, 80);
            this.dgvJobList.Name = "dgvJobList";
            this.dgvJobList.Size = new System.Drawing.Size(333, 419);
            this.dgvJobList.TabIndex = 0;
            this.dgvJobList.Text = "dataGridView1";
            this.dgvJobList.SelectionChanged += new System.EventHandler(this.dgvJobList_SelectionChanged);
            // 
            // toolStrip1
            // 
            this.toolStrip1.Dock = System.Windows.Forms.DockStyle.None;
            this.toolStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.tsbJobAdd,
            this.tsbJobEdit,
            this.tsbJobDelete});
            this.toolStrip1.Location = new System.Drawing.Point(12, 52);
            this.toolStrip1.Name = "toolStrip1";
            this.toolStrip1.Size = new System.Drawing.Size(81, 25);
            this.toolStrip1.TabIndex = 1;
            this.toolStrip1.Text = "tsJobsMenu";
            // 
            // tsbJobAdd
            // 
            this.tsbJobAdd.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.tsbJobAdd.Image = ((System.Drawing.Image)(resources.GetObject("tsbJobAdd.Image")));
            this.tsbJobAdd.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.tsbJobAdd.Name = "tsbJobAdd";
            this.tsbJobAdd.Size = new System.Drawing.Size(23, 22);
            this.tsbJobAdd.Text = "Add";
            this.tsbJobAdd.Click += new System.EventHandler(this.tsbJobAdd_Click);
            // 
            // tsbJobEdit
            // 
            this.tsbJobEdit.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.tsbJobEdit.Image = ((System.Drawing.Image)(resources.GetObject("tsbJobEdit.Image")));
            this.tsbJobEdit.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.tsbJobEdit.Name = "tsbJobEdit";
            this.tsbJobEdit.Size = new System.Drawing.Size(23, 22);
            this.tsbJobEdit.Text = "Edit";
            this.tsbJobEdit.Click += new System.EventHandler(this.tsbJobEdit_Click);
            // 
            // tsbJobDelete
            // 
            this.tsbJobDelete.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.tsbJobDelete.Image = ((System.Drawing.Image)(resources.GetObject("tsbJobDelete.Image")));
            this.tsbJobDelete.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.tsbJobDelete.Name = "tsbJobDelete";
            this.tsbJobDelete.Size = new System.Drawing.Size(23, 22);
            this.tsbJobDelete.Text = "Delete";
            this.tsbJobDelete.Click += new System.EventHandler(this.tsbJobDelete_Click);
            // 
            // btnGoogleAuthorize
            // 
            this.btnGoogleAuthorize.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.btnGoogleAuthorize.Location = new System.Drawing.Point(12, 12);
            this.btnGoogleAuthorize.Name = "btnGoogleAuthorize";
            this.btnGoogleAuthorize.Size = new System.Drawing.Size(333, 23);
            this.btnGoogleAuthorize.TabIndex = 3;
            this.btnGoogleAuthorize.Text = "Authorize in Google account";
            this.btnGoogleAuthorize.UseVisualStyleBackColor = true;
            this.btnGoogleAuthorize.Click += new System.EventHandler(this.btnGoogleAuthorize_Click);
            // 
            // btnBackupOnly
            // 
            this.btnBackupOnly.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.btnBackupOnly.Location = new System.Drawing.Point(351, 476);
            this.btnBackupOnly.Name = "btnBackupOnly";
            this.btnBackupOnly.Size = new System.Drawing.Size(638, 23);
            this.btnBackupOnly.TabIndex = 14;
            this.btnBackupOnly.Text = "Create backup only to folder";
            this.btnBackupOnly.UseVisualStyleBackColor = true;
            this.btnBackupOnly.Click += new System.EventHandler(this.btnBackupOnly_Click);
            // 
            // chbSaveToFolder
            // 
            this.chbSaveToFolder.AutoSize = true;
            this.chbSaveToFolder.Location = new System.Drawing.Point(351, 330);
            this.chbSaveToFolder.Name = "chbSaveToFolder";
            this.chbSaveToFolder.Size = new System.Drawing.Size(139, 19);
            this.chbSaveToFolder.TabIndex = 13;
            this.chbSaveToFolder.Text = "Save backup in folder";
            this.chbSaveToFolder.UseVisualStyleBackColor = true;
            this.chbSaveToFolder.CheckedChanged += new System.EventHandler(this.chbSaveToFolder_CheckedChanged);
            // 
            // btnBackupAndUpload
            // 
            this.btnBackupAndUpload.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.btnBackupAndUpload.Location = new System.Drawing.Point(351, 447);
            this.btnBackupAndUpload.Name = "btnBackupAndUpload";
            this.btnBackupAndUpload.Size = new System.Drawing.Size(638, 23);
            this.btnBackupAndUpload.TabIndex = 12;
            this.btnBackupAndUpload.Text = "Create backup && Upload to Google Drive";
            this.btnBackupAndUpload.UseVisualStyleBackColor = true;
            this.btnBackupAndUpload.Click += new System.EventHandler(this.btnBackupAndUpload_Click);
            // 
            // btnFolderSelect
            // 
            this.btnFolderSelect.Enabled = false;
            this.btnFolderSelect.Location = new System.Drawing.Point(632, 354);
            this.btnFolderSelect.Name = "btnFolderSelect";
            this.btnFolderSelect.Size = new System.Drawing.Size(28, 23);
            this.btnFolderSelect.TabIndex = 11;
            this.btnFolderSelect.Text = "...";
            this.btnFolderSelect.UseVisualStyleBackColor = true;
            this.btnFolderSelect.Click += new System.EventHandler(this.btnFIleSelect_Click);
            // 
            // txtFolder
            // 
            this.txtFolder.Enabled = false;
            this.txtFolder.Location = new System.Drawing.Point(374, 355);
            this.txtFolder.Name = "txtFolder";
            this.txtFolder.Size = new System.Drawing.Size(252, 23);
            this.txtFolder.TabIndex = 10;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(10, 238);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(40, 15);
            this.label1.TabIndex = 9;
            this.label1.Text = "Folder";
            // 
            // grbConnection
            // 
            this.grbConnection.Controls.Add(this.label11);
            this.grbConnection.Controls.Add(this.btnConTest);
            this.grbConnection.Controls.Add(this.rdbConAuthWin);
            this.grbConnection.Controls.Add(this.rdbConAuthSql);
            this.grbConnection.Controls.Add(this.label5);
            this.grbConnection.Controls.Add(this.txtConPassword);
            this.grbConnection.Controls.Add(this.label4);
            this.grbConnection.Controls.Add(this.txtConLogin);
            this.grbConnection.Controls.Add(this.label3);
            this.grbConnection.Controls.Add(this.txtConDatabase);
            this.grbConnection.Controls.Add(this.label2);
            this.grbConnection.Controls.Add(this.txtConServer);
            this.grbConnection.Controls.Add(this.label1);
            this.grbConnection.Location = new System.Drawing.Point(351, 109);
            this.grbConnection.Name = "grbConnection";
            this.grbConnection.Size = new System.Drawing.Size(315, 200);
            this.grbConnection.TabIndex = 0;
            this.grbConnection.TabStop = false;
            this.grbConnection.Text = "Connection";
            // 
            // label11
            // 
            this.label11.AutoSize = true;
            this.label11.Location = new System.Drawing.Point(6, 25);
            this.label11.Name = "label11";
            this.label11.Size = new System.Drawing.Size(39, 15);
            this.label11.TabIndex = 10;
            this.label11.Text = "Server";
            // 
            // btnConTest
            // 
            this.btnConTest.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.btnConTest.Location = new System.Drawing.Point(98, 163);
            this.btnConTest.Name = "btnConTest";
            this.btnConTest.Size = new System.Drawing.Size(211, 23);
            this.btnConTest.TabIndex = 7;
            this.btnConTest.Text = "Test connection";
            this.btnConTest.UseVisualStyleBackColor = true;
            this.btnConTest.Click += new System.EventHandler(this.btnConTest_Click);
            // 
            // rdbConAuthWin
            // 
            this.rdbConAuthWin.AutoSize = true;
            this.rdbConAuthWin.Location = new System.Drawing.Point(149, 80);
            this.rdbConAuthWin.Name = "rdbConAuthWin";
            this.rdbConAuthWin.Size = new System.Drawing.Size(74, 19);
            this.rdbConAuthWin.TabIndex = 4;
            this.rdbConAuthWin.Text = "Windows";
            this.rdbConAuthWin.UseVisualStyleBackColor = true;
            this.rdbConAuthWin.CheckedChanged += new System.EventHandler(this.rdbConAuthWin_CheckedChanged);
            // 
            // rdbConAuthSql
            // 
            this.rdbConAuthSql.AutoSize = true;
            this.rdbConAuthSql.Checked = true;
            this.rdbConAuthSql.Location = new System.Drawing.Point(97, 80);
            this.rdbConAuthSql.Name = "rdbConAuthSql";
            this.rdbConAuthSql.Size = new System.Drawing.Size(46, 19);
            this.rdbConAuthSql.TabIndex = 3;
            this.rdbConAuthSql.TabStop = true;
            this.rdbConAuthSql.Text = "SQL";
            this.rdbConAuthSql.UseVisualStyleBackColor = true;
            this.rdbConAuthSql.CheckedChanged += new System.EventHandler(this.rdbConAuthSql_CheckedChanged);
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(5, 80);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(86, 15);
            this.label5.TabIndex = 2;
            this.label5.Text = "Authentication";
            // 
            // txtConPassword
            // 
            this.txtConPassword.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.txtConPassword.Location = new System.Drawing.Point(98, 134);
            this.txtConPassword.Name = "txtConPassword";
            this.txtConPassword.PasswordChar = '*';
            this.txtConPassword.Size = new System.Drawing.Size(211, 23);
            this.txtConPassword.TabIndex = 6;
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(6, 137);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(57, 15);
            this.label4.TabIndex = 0;
            this.label4.Text = "Password";
            // 
            // txtConLogin
            // 
            this.txtConLogin.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.txtConLogin.Location = new System.Drawing.Point(98, 105);
            this.txtConLogin.Name = "txtConLogin";
            this.txtConLogin.Size = new System.Drawing.Size(211, 23);
            this.txtConLogin.TabIndex = 5;
            this.txtConLogin.Text = "sa";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(6, 108);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(37, 15);
            this.label3.TabIndex = 0;
            this.label3.Text = "Login";
            // 
            // txtConDatabase
            // 
            this.txtConDatabase.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.txtConDatabase.Location = new System.Drawing.Point(98, 51);
            this.txtConDatabase.Name = "txtConDatabase";
            this.txtConDatabase.Size = new System.Drawing.Size(211, 23);
            this.txtConDatabase.TabIndex = 2;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(6, 54);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(55, 15);
            this.label2.TabIndex = 0;
            this.label2.Text = "Database";
            // 
            // txtConServer
            // 
            this.txtConServer.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.txtConServer.Location = new System.Drawing.Point(98, 22);
            this.txtConServer.Name = "txtConServer";
            this.txtConServer.Size = new System.Drawing.Size(211, 23);
            this.txtConServer.TabIndex = 1;
            this.txtConServer.Text = "localhost";
            // 
            // grbSchedule
            // 
            this.grbSchedule.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.grbSchedule.Controls.Add(this.label7);
            this.grbSchedule.Controls.Add(this.btnRunTimeDelete);
            this.grbSchedule.Controls.Add(this.dgvRunTime);
            this.grbSchedule.Controls.Add(this.dtpRunTime);
            this.grbSchedule.Controls.Add(this.btnRunTimeAdd);
            this.grbSchedule.Controls.Add(this.label6);
            this.grbSchedule.Controls.Add(this.chbSunday);
            this.grbSchedule.Controls.Add(this.chbSaturday);
            this.grbSchedule.Controls.Add(this.chbFriday);
            this.grbSchedule.Controls.Add(this.chbThursday);
            this.grbSchedule.Controls.Add(this.chbWednesday);
            this.grbSchedule.Controls.Add(this.chbTuesday);
            this.grbSchedule.Controls.Add(this.chbMonday);
            this.grbSchedule.Controls.Add(this.rdbAutorunModeNone);
            this.grbSchedule.Controls.Add(this.rdbAutorunModeAuto);
            this.grbSchedule.Location = new System.Drawing.Point(672, 109);
            this.grbSchedule.Name = "grbSchedule";
            this.grbSchedule.Size = new System.Drawing.Size(325, 239);
            this.grbSchedule.TabIndex = 15;
            this.grbSchedule.TabStop = false;
            this.grbSchedule.Text = "Autorun schedule";
            // 
            // label7
            // 
            this.label7.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.label7.AutoSize = true;
            this.label7.Location = new System.Drawing.Point(32, 45);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(67, 15);
            this.label7.TabIndex = 4;
            this.label7.Text = "Day to start";
            // 
            // btnRunTimeDelete
            // 
            this.btnRunTimeDelete.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.btnRunTimeDelete.Location = new System.Drawing.Point(220, 178);
            this.btnRunTimeDelete.Name = "btnRunTimeDelete";
            this.btnRunTimeDelete.Size = new System.Drawing.Size(94, 23);
            this.btnRunTimeDelete.TabIndex = 8;
            this.btnRunTimeDelete.Text = "Delete selected";
            this.btnRunTimeDelete.UseVisualStyleBackColor = true;
            this.btnRunTimeDelete.Click += new System.EventHandler(this.btnRunTimeDelete_Click);
            // 
            // dgvRunTime
            // 
            this.dgvRunTime.AllowUserToAddRows = false;
            this.dgvRunTime.AllowUserToDeleteRows = false;
            this.dgvRunTime.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.dgvRunTime.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgvRunTime.Location = new System.Drawing.Point(125, 63);
            this.dgvRunTime.Name = "dgvRunTime";
            this.dgvRunTime.ReadOnly = true;
            this.dgvRunTime.Size = new System.Drawing.Size(89, 138);
            this.dgvRunTime.TabIndex = 7;
            this.dgvRunTime.Text = "dataGridView1";
            // 
            // dtpRunTime
            // 
            this.dtpRunTime.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.dtpRunTime.CustomFormat = "HH:mm";
            this.dtpRunTime.Format = System.Windows.Forms.DateTimePickerFormat.Custom;
            this.dtpRunTime.Location = new System.Drawing.Point(125, 207);
            this.dtpRunTime.Name = "dtpRunTime";
            this.dtpRunTime.Size = new System.Drawing.Size(89, 23);
            this.dtpRunTime.TabIndex = 6;
            // 
            // btnRunTimeAdd
            // 
            this.btnRunTimeAdd.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.btnRunTimeAdd.Location = new System.Drawing.Point(220, 207);
            this.btnRunTimeAdd.Name = "btnRunTimeAdd";
            this.btnRunTimeAdd.Size = new System.Drawing.Size(94, 23);
            this.btnRunTimeAdd.TabIndex = 5;
            this.btnRunTimeAdd.Text = "Add time";
            this.btnRunTimeAdd.UseVisualStyleBackColor = true;
            this.btnRunTimeAdd.Click += new System.EventHandler(this.btnRunTimeAdd_Click);
            // 
            // label6
            // 
            this.label6.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.label6.AutoSize = true;
            this.label6.Location = new System.Drawing.Point(125, 45);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(73, 15);
            this.label6.TabIndex = 4;
            this.label6.Text = "Time to start";
            // 
            // chbSunday
            // 
            this.chbSunday.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.chbSunday.AutoSize = true;
            this.chbSunday.Location = new System.Drawing.Point(32, 211);
            this.chbSunday.Name = "chbSunday";
            this.chbSunday.Size = new System.Drawing.Size(65, 19);
            this.chbSunday.TabIndex = 2;
            this.chbSunday.Text = "Sunday";
            this.chbSunday.UseVisualStyleBackColor = true;
            // 
            // chbSaturday
            // 
            this.chbSaturday.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.chbSaturday.AutoSize = true;
            this.chbSaturday.Location = new System.Drawing.Point(32, 186);
            this.chbSaturday.Name = "chbSaturday";
            this.chbSaturday.Size = new System.Drawing.Size(72, 19);
            this.chbSaturday.TabIndex = 2;
            this.chbSaturday.Text = "Saturday";
            this.chbSaturday.UseVisualStyleBackColor = true;
            // 
            // chbFriday
            // 
            this.chbFriday.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.chbFriday.AutoSize = true;
            this.chbFriday.Location = new System.Drawing.Point(32, 161);
            this.chbFriday.Name = "chbFriday";
            this.chbFriday.Size = new System.Drawing.Size(58, 19);
            this.chbFriday.TabIndex = 2;
            this.chbFriday.Text = "Friday";
            this.chbFriday.UseVisualStyleBackColor = true;
            // 
            // chbThursday
            // 
            this.chbThursday.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.chbThursday.AutoSize = true;
            this.chbThursday.Location = new System.Drawing.Point(32, 136);
            this.chbThursday.Name = "chbThursday";
            this.chbThursday.Size = new System.Drawing.Size(74, 19);
            this.chbThursday.TabIndex = 2;
            this.chbThursday.Text = "Thursday";
            this.chbThursday.UseVisualStyleBackColor = true;
            // 
            // chbWednesday
            // 
            this.chbWednesday.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.chbWednesday.AutoSize = true;
            this.chbWednesday.Location = new System.Drawing.Point(32, 111);
            this.chbWednesday.Name = "chbWednesday";
            this.chbWednesday.Size = new System.Drawing.Size(87, 19);
            this.chbWednesday.TabIndex = 2;
            this.chbWednesday.Text = "Wednesday";
            this.chbWednesday.UseVisualStyleBackColor = true;
            // 
            // chbTuesday
            // 
            this.chbTuesday.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.chbTuesday.AutoSize = true;
            this.chbTuesday.Location = new System.Drawing.Point(32, 86);
            this.chbTuesday.Name = "chbTuesday";
            this.chbTuesday.Size = new System.Drawing.Size(69, 19);
            this.chbTuesday.TabIndex = 2;
            this.chbTuesday.Text = "Tuesday";
            this.chbTuesday.UseVisualStyleBackColor = true;
            // 
            // chbMonday
            // 
            this.chbMonday.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.chbMonday.AutoSize = true;
            this.chbMonday.Location = new System.Drawing.Point(32, 63);
            this.chbMonday.Name = "chbMonday";
            this.chbMonday.Size = new System.Drawing.Size(70, 19);
            this.chbMonday.TabIndex = 2;
            this.chbMonday.Text = "Monday";
            this.chbMonday.UseVisualStyleBackColor = true;
            // 
            // rdbAutorunModeNone
            // 
            this.rdbAutorunModeNone.AutoSize = true;
            this.rdbAutorunModeNone.Location = new System.Drawing.Point(125, 23);
            this.rdbAutorunModeNone.Name = "rdbAutorunModeNone";
            this.rdbAutorunModeNone.Size = new System.Drawing.Size(84, 19);
            this.rdbAutorunModeNone.TabIndex = 1;
            this.rdbAutorunModeNone.TabStop = true;
            this.rdbAutorunModeNone.Text = "no autorun";
            this.rdbAutorunModeNone.UseVisualStyleBackColor = true;
            this.rdbAutorunModeNone.CheckedChanged += new System.EventHandler(this.rdbAutorunModeNone_CheckedChanged);
            // 
            // rdbAutorunModeAuto
            // 
            this.rdbAutorunModeAuto.AutoSize = true;
            this.rdbAutorunModeAuto.Location = new System.Drawing.Point(6, 22);
            this.rdbAutorunModeAuto.Name = "rdbAutorunModeAuto";
            this.rdbAutorunModeAuto.Size = new System.Drawing.Size(85, 19);
            this.rdbAutorunModeAuto.TabIndex = 0;
            this.rdbAutorunModeAuto.TabStop = true;
            this.rdbAutorunModeAuto.Text = "set autorun";
            this.rdbAutorunModeAuto.UseVisualStyleBackColor = true;
            this.rdbAutorunModeAuto.CheckedChanged += new System.EventHandler(this.rdbAutorunModeAuto_CheckedChanged);
            // 
            // label8
            // 
            this.label8.AutoSize = true;
            this.label8.Location = new System.Drawing.Point(351, 312);
            this.label8.Name = "label8";
            this.label8.Size = new System.Drawing.Size(315, 15);
            this.label8.TabIndex = 16;
            this.label8.Text = "If not saved, the archive will only be uploaded to the cloud";
            // 
            // label9
            // 
            this.label9.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.label9.AutoSize = true;
            this.label9.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point);
            this.label9.Location = new System.Drawing.Point(351, 420);
            this.label9.Name = "label9";
            this.label9.Size = new System.Drawing.Size(278, 15);
            this.label9.TabIndex = 17;
            this.label9.Text = "You can follow these steps now with job settings:";
            // 
            // label10
            // 
            this.label10.AutoSize = true;
            this.label10.Location = new System.Drawing.Point(351, 83);
            this.label10.Name = "label10";
            this.label10.Size = new System.Drawing.Size(58, 15);
            this.label10.TabIndex = 18;
            this.label10.Text = "Job name";
            // 
            // txtJobName
            // 
            this.txtJobName.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.txtJobName.Location = new System.Drawing.Point(414, 80);
            this.txtJobName.Name = "txtJobName";
            this.txtJobName.Size = new System.Drawing.Size(583, 23);
            this.txtJobName.TabIndex = 19;
            // 
            // btnJobSave
            // 
            this.btnJobSave.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.btnJobSave.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point);
            this.btnJobSave.Image = ((System.Drawing.Image)(resources.GetObject("btnJobSave.Image")));
            this.btnJobSave.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.btnJobSave.Location = new System.Drawing.Point(672, 354);
            this.btnJobSave.Name = "btnJobSave";
            this.btnJobSave.Size = new System.Drawing.Size(325, 23);
            this.btnJobSave.TabIndex = 20;
            this.btnJobSave.Text = "Save job";
            this.btnJobSave.UseVisualStyleBackColor = true;
            this.btnJobSave.Click += new System.EventHandler(this.btnJobSave_Click);
            // 
            // timer1
            // 
            this.timer1.Tick += new System.EventHandler(this.timer1_Tick);
            // 
            // lblCount
            // 
            this.lblCount.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.lblCount.Location = new System.Drawing.Point(642, 16);
            this.lblCount.Name = "lblCount";
            this.lblCount.Size = new System.Drawing.Size(355, 19);
            this.lblCount.TabIndex = 21;
            this.lblCount.Text = "label12";
            this.lblCount.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // FrmMain
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 15F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1009, 511);
            this.Controls.Add(this.lblCount);
            this.Controls.Add(this.btnJobSave);
            this.Controls.Add(this.txtJobName);
            this.Controls.Add(this.label10);
            this.Controls.Add(this.label9);
            this.Controls.Add(this.label8);
            this.Controls.Add(this.grbSchedule);
            this.Controls.Add(this.grbConnection);
            this.Controls.Add(this.txtFolder);
            this.Controls.Add(this.btnFolderSelect);
            this.Controls.Add(this.btnBackupAndUpload);
            this.Controls.Add(this.chbSaveToFolder);
            this.Controls.Add(this.btnBackupOnly);
            this.Controls.Add(this.btnGoogleAuthorize);
            this.Controls.Add(this.toolStrip1);
            this.Controls.Add(this.dgvJobList);
            this.MinimumSize = new System.Drawing.Size(1025, 520);
            this.Name = "FrmMain";
            this.Text = "SqlBackuper to Google Drive";
            ((System.ComponentModel.ISupportInitialize)(this.dgvJobList)).EndInit();
            this.toolStrip1.ResumeLayout(false);
            this.toolStrip1.PerformLayout();
            this.grbConnection.ResumeLayout(false);
            this.grbConnection.PerformLayout();
            this.grbSchedule.ResumeLayout(false);
            this.grbSchedule.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgvRunTime)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.DataGridView dgvJobList;
        private System.Windows.Forms.ToolStrip toolStrip1;
        private System.Windows.Forms.ToolStripButton tsbJobAdd;
        private System.Windows.Forms.ToolStripButton tsbJobEdit;
        private System.Windows.Forms.ToolStripButton tsbJobDelete;
        private System.Windows.Forms.Button btnGoogleAuthorize;
        private System.Windows.Forms.FolderBrowserDialog folderBrowserDialog1;
        private System.Windows.Forms.Button btnJobSave;
        private System.Windows.Forms.CheckBox chbSaveToFolder;
        private System.Windows.Forms.Button btnBackupAndUpload;
        private System.Windows.Forms.Button btnFolderSelect;
        private System.Windows.Forms.TextBox txtFolder;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.GroupBox grbConnection;
        private System.Windows.Forms.Button btnConTest;
        private System.Windows.Forms.RadioButton rdbConAuthWin;
        private System.Windows.Forms.RadioButton rdbConAuthSql;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.TextBox txtConPassword;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.TextBox txtConLogin;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.TextBox txtConDatabase;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.TextBox txtConServer;
        private System.Windows.Forms.GroupBox grbSchedule;
        private System.Windows.Forms.CheckBox chbSunday;
        private System.Windows.Forms.CheckBox chbSaturday;
        private System.Windows.Forms.CheckBox chbFriday;
        private System.Windows.Forms.CheckBox chbThursday;
        private System.Windows.Forms.CheckBox chbWednesday;
        private System.Windows.Forms.CheckBox chbTuesday;
        private System.Windows.Forms.CheckBox chbMonday;
        private System.Windows.Forms.RadioButton rdbAutorunModeNone;
        private System.Windows.Forms.RadioButton rdbAutorunModeAuto;
        private System.Windows.Forms.DataGridView dgvRunTime;
        private System.Windows.Forms.DateTimePicker dtpRunTime;
        private System.Windows.Forms.Button btnTimeAdd;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.Label label7;
        private System.Windows.Forms.Button btnRunTimeDelete;
        private System.Windows.Forms.Label label8;
        private System.Windows.Forms.Label label9;
        private System.Windows.Forms.Button btnBackupOnly;
        private System.Windows.Forms.Label label10;
        private System.Windows.Forms.TextBox txtJobName;
        private System.Windows.Forms.Label label11;
        private System.Windows.Forms.Button btnRunTimeAdd;
        private System.Windows.Forms.Timer timer1;
        private System.Windows.Forms.Label lblCount;
    }
}

