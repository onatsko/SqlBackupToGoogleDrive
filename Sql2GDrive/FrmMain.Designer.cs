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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(FrmMain));
            this.dgvJobList = new System.Windows.Forms.DataGridView();
            this.toolStrip1 = new System.Windows.Forms.ToolStrip();
            this.tsbJobAdd = new System.Windows.Forms.ToolStripButton();
            this.tsbJobEdit = new System.Windows.Forms.ToolStripButton();
            this.tsbJobDelete = new System.Windows.Forms.ToolStripButton();
            this.tabControl1 = new System.Windows.Forms.TabControl();
            this.tpJobDetail = new System.Windows.Forms.TabPage();
            this.btnCreateBackupOnly = new System.Windows.Forms.Button();
            this.chbSaveToFolder = new System.Windows.Forms.CheckBox();
            this.btnBackupAndUpload = new System.Windows.Forms.Button();
            this.btnFolderSelect = new System.Windows.Forms.Button();
            this.txtFolder = new System.Windows.Forms.TextBox();
            this.label6 = new System.Windows.Forms.Label();
            this.grbConnection = new System.Windows.Forms.GroupBox();
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
            this.label1 = new System.Windows.Forms.Label();
            this.tbpSchedule = new System.Windows.Forms.TabPage();
            this.btnGoogleAuthorize = new System.Windows.Forms.Button();
            this.folderBrowserDialog1 = new System.Windows.Forms.FolderBrowserDialog();
            ((System.ComponentModel.ISupportInitialize)(this.dgvJobList)).BeginInit();
            this.toolStrip1.SuspendLayout();
            this.tabControl1.SuspendLayout();
            this.tpJobDetail.SuspendLayout();
            this.grbConnection.SuspendLayout();
            this.SuspendLayout();
            // 
            // dgvJobList
            // 
            this.dgvJobList.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left)));
            this.dgvJobList.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgvJobList.Location = new System.Drawing.Point(12, 80);
            this.dgvJobList.Name = "dgvJobList";
            this.dgvJobList.Size = new System.Drawing.Size(333, 424);
            this.dgvJobList.TabIndex = 0;
            this.dgvJobList.Text = "dataGridView1";
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
            // 
            // tsbJobEdit
            // 
            this.tsbJobEdit.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.tsbJobEdit.Image = ((System.Drawing.Image)(resources.GetObject("tsbJobEdit.Image")));
            this.tsbJobEdit.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.tsbJobEdit.Name = "tsbJobEdit";
            this.tsbJobEdit.Size = new System.Drawing.Size(23, 22);
            this.tsbJobEdit.Text = "Edit";
            // 
            // tsbJobDelete
            // 
            this.tsbJobDelete.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.tsbJobDelete.Image = ((System.Drawing.Image)(resources.GetObject("tsbJobDelete.Image")));
            this.tsbJobDelete.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.tsbJobDelete.Name = "tsbJobDelete";
            this.tsbJobDelete.Size = new System.Drawing.Size(23, 22);
            this.tsbJobDelete.Text = "Delete";
            // 
            // tabControl1
            // 
            this.tabControl1.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.tabControl1.Controls.Add(this.tpJobDetail);
            this.tabControl1.Controls.Add(this.tbpSchedule);
            this.tabControl1.Location = new System.Drawing.Point(351, 52);
            this.tabControl1.Name = "tabControl1";
            this.tabControl1.SelectedIndex = 0;
            this.tabControl1.Size = new System.Drawing.Size(635, 452);
            this.tabControl1.TabIndex = 2;
            // 
            // tpJobDetail
            // 
            this.tpJobDetail.Controls.Add(this.btnCreateBackupOnly);
            this.tpJobDetail.Controls.Add(this.chbSaveToFolder);
            this.tpJobDetail.Controls.Add(this.btnBackupAndUpload);
            this.tpJobDetail.Controls.Add(this.btnFolderSelect);
            this.tpJobDetail.Controls.Add(this.txtFolder);
            this.tpJobDetail.Controls.Add(this.label6);
            this.tpJobDetail.Controls.Add(this.grbConnection);
            this.tpJobDetail.Location = new System.Drawing.Point(4, 24);
            this.tpJobDetail.Name = "tpJobDetail";
            this.tpJobDetail.Padding = new System.Windows.Forms.Padding(3);
            this.tpJobDetail.Size = new System.Drawing.Size(627, 424);
            this.tpJobDetail.TabIndex = 0;
            this.tpJobDetail.Text = "Job details";
            this.tpJobDetail.UseVisualStyleBackColor = true;
            // 
            // btnCreateBackupOnly
            // 
            this.btnCreateBackupOnly.Location = new System.Drawing.Point(56, 304);
            this.btnCreateBackupOnly.Name = "btnCreateBackupOnly";
            this.btnCreateBackupOnly.Size = new System.Drawing.Size(235, 23);
            this.btnCreateBackupOnly.TabIndex = 14;
            this.btnCreateBackupOnly.Text = "Create backup only";
            this.btnCreateBackupOnly.UseVisualStyleBackColor = true;
            this.btnCreateBackupOnly.Click += new System.EventHandler(this.btnBackupOnly_Click);
            // 
            // chbSaveToFolder
            // 
            this.chbSaveToFolder.AutoSize = true;
            this.chbSaveToFolder.Location = new System.Drawing.Point(3, 210);
            this.chbSaveToFolder.Name = "chbSaveToFolder";
            this.chbSaveToFolder.Size = new System.Drawing.Size(139, 19);
            this.chbSaveToFolder.TabIndex = 13;
            this.chbSaveToFolder.Text = "Save backup in folder";
            this.chbSaveToFolder.UseVisualStyleBackColor = true;
            this.chbSaveToFolder.CheckedChanged += new System.EventHandler(this.chbSaveToFolder_CheckedChanged);
            // 
            // btnBackupAndUpload
            // 
            this.btnBackupAndUpload.Location = new System.Drawing.Point(56, 264);
            this.btnBackupAndUpload.Name = "btnBackupAndUpload";
            this.btnBackupAndUpload.Size = new System.Drawing.Size(235, 23);
            this.btnBackupAndUpload.TabIndex = 12;
            this.btnBackupAndUpload.Text = "Create backup && Upload to Google Drive";
            this.btnBackupAndUpload.UseVisualStyleBackColor = true;
            this.btnBackupAndUpload.Click += new System.EventHandler(this.btnBackupAndUpload_Click);
            // 
            // btnFolderSelect
            // 
            this.btnFolderSelect.Enabled = false;
            this.btnFolderSelect.Image = ((System.Drawing.Image)(resources.GetObject("btnFolderSelect.Image")));
            this.btnFolderSelect.Location = new System.Drawing.Point(263, 234);
            this.btnFolderSelect.Name = "btnFolderSelect";
            this.btnFolderSelect.Size = new System.Drawing.Size(28, 23);
            this.btnFolderSelect.TabIndex = 11;
            this.btnFolderSelect.UseVisualStyleBackColor = true;
            this.btnFolderSelect.Click += new System.EventHandler(this.btnFIleSelect_Click);
            // 
            // txtFolder
            // 
            this.txtFolder.Enabled = false;
            this.txtFolder.Location = new System.Drawing.Point(56, 235);
            this.txtFolder.Name = "txtFolder";
            this.txtFolder.Size = new System.Drawing.Size(201, 23);
            this.txtFolder.TabIndex = 10;
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Location = new System.Drawing.Point(10, 238);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(40, 15);
            this.label6.TabIndex = 9;
            this.label6.Text = "Folder";
            // 
            // grbConnection
            // 
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
            this.grbConnection.Location = new System.Drawing.Point(3, 4);
            this.grbConnection.Name = "grbConnection";
            this.grbConnection.Size = new System.Drawing.Size(294, 200);
            this.grbConnection.TabIndex = 0;
            this.grbConnection.TabStop = false;
            this.grbConnection.Text = "Connection";
            // 
            // btnConTest
            // 
            this.btnConTest.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.btnConTest.Location = new System.Drawing.Point(98, 163);
            this.btnConTest.Name = "btnConTest";
            this.btnConTest.Size = new System.Drawing.Size(190, 23);
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
            this.txtConPassword.Size = new System.Drawing.Size(190, 23);
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
            this.txtConLogin.Size = new System.Drawing.Size(190, 23);
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
            this.txtConDatabase.Size = new System.Drawing.Size(190, 23);
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
            this.txtConServer.Size = new System.Drawing.Size(190, 23);
            this.txtConServer.TabIndex = 1;
            this.txtConServer.Text = "localhost";
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(6, 25);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(39, 15);
            this.label1.TabIndex = 0;
            this.label1.Text = "Server";
            // 
            // tbpSchedule
            // 
            this.tbpSchedule.Location = new System.Drawing.Point(4, 24);
            this.tbpSchedule.Name = "tbpSchedule";
            this.tbpSchedule.Padding = new System.Windows.Forms.Padding(3);
            this.tbpSchedule.Size = new System.Drawing.Size(627, 424);
            this.tbpSchedule.TabIndex = 1;
            this.tbpSchedule.Text = "Schedule";
            this.tbpSchedule.UseVisualStyleBackColor = true;
            // 
            // btnGoogleAuthorize
            // 
            this.btnGoogleAuthorize.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.btnGoogleAuthorize.Location = new System.Drawing.Point(12, 12);
            this.btnGoogleAuthorize.Name = "btnGoogleAuthorize";
            this.btnGoogleAuthorize.Size = new System.Drawing.Size(333, 23);
            this.btnGoogleAuthorize.TabIndex = 3;
            this.btnGoogleAuthorize.Text = "Выбрать Google аккаунт";
            this.btnGoogleAuthorize.UseVisualStyleBackColor = true;
            this.btnGoogleAuthorize.Click += new System.EventHandler(this.btnGoogleAuthorize_Click);
            // 
            // FrmMain
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 15F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(998, 516);
            this.Controls.Add(this.btnGoogleAuthorize);
            this.Controls.Add(this.tabControl1);
            this.Controls.Add(this.toolStrip1);
            this.Controls.Add(this.dgvJobList);
            this.Name = "FrmMain";
            this.Text = "SqlBackuper to Google Drive";
            ((System.ComponentModel.ISupportInitialize)(this.dgvJobList)).EndInit();
            this.toolStrip1.ResumeLayout(false);
            this.toolStrip1.PerformLayout();
            this.tabControl1.ResumeLayout(false);
            this.tpJobDetail.ResumeLayout(false);
            this.tpJobDetail.PerformLayout();
            this.grbConnection.ResumeLayout(false);
            this.grbConnection.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.DataGridView dgvJobList;
        private System.Windows.Forms.ToolStrip toolStrip1;
        private System.Windows.Forms.ToolStripButton tsbJobAdd;
        private System.Windows.Forms.ToolStripButton tsbJobEdit;
        private System.Windows.Forms.ToolStripButton tsbJobDelete;
        private System.Windows.Forms.TabControl tabControl1;
        private System.Windows.Forms.TabPage tpJobDetail;
        private System.Windows.Forms.TabPage tbpSchedule;
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
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Button btnGoogleAuthorize;
        private System.Windows.Forms.Button btnFolderSelect;
        private System.Windows.Forms.TextBox txtFolder;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.Button btnBackupAndUpload;
        private System.Windows.Forms.FolderBrowserDialog folderBrowserDialog1;
        private System.Windows.Forms.CheckBox chbSaveToFolder;
        private System.Windows.Forms.Button btnCreateBackupOnly;
    }
}

