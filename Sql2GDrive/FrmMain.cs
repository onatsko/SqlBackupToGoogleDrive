using Google.Apis.Auth.OAuth2;
using Google.Apis.Drive.v3;
using Google.Apis.Services;
using Google.Apis.Util.Store;
using System;
using System.Data;
using System.Data.SqlClient;
using System.IO;
using System.Linq;
using System.Threading;
using System.Windows.Forms;
using Sql2GoogleDrive.DAL.TaskContext;

namespace Sql2GDrive
{
    public partial class FrmMain : Form
    {

        public FrmMain()
        {
            InitializeComponent();

            dgvJobListSetup();
            EnableDisableJobDetailControls(false);

            GetJobs();

        }

        private void EnableDisableJobDetailControls(bool state)
        {
            txtJobName.Enabled = state;

            grbConnection.Enabled = state;
            grbSchedule.Enabled = state;
            chbSaveToFolder.Enabled = state;
            txtFolder.Enabled = state;
            btnFolderSelect.Enabled = state;

            if (!state)
            {
                rdbConAuthSql.Checked = state;
                rdbConAuthWin.Checked = state;
            }

            if (!state)
            {
                rdbAutorunModeAuto.Checked = state;
                rdbAutorunModeNone.Checked = state;
            }

            btnBackupAndUpload.Enabled = state;
            btnBackupOnly.Enabled = state;

        }

        private void dgvJobListSetup()
        {
            dgvJobList.DataSource = null;
            dgvJobList.Columns.Clear();
            dgvJobList.EditMode = DataGridViewEditMode.EditProgrammatically;
            dgvJobList.AutoGenerateColumns = false;
            dgvJobList.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
            dgvJobList.AllowUserToAddRows = false;
            dgvJobList.AllowUserToDeleteRows = false;
            dgvJobList.RowHeadersVisible = false;

            int i = 0;
            dgvJobList.ColumnCount = 2;

            dgvJobList.Columns[i].Visible = true;
            dgvJobList.Columns[i].HeaderText = "#";
            dgvJobList.Columns[i].Width = 15;
            dgvJobList.Columns[i].DataPropertyName = "Id";
            dgvJobList.Columns[i].AutoSizeMode = DataGridViewAutoSizeColumnMode.None;
            dgvJobList.Columns[i].Name = dgvJobList.Columns[i].DataPropertyName;
            i += 1;
            dgvJobList.Columns[i].Visible = true;
            dgvJobList.Columns[i].HeaderText = "Job name";
            dgvJobList.Columns[i].Width = 150;
            dgvJobList.Columns[i].DataPropertyName = "Name";
            dgvJobList.Columns[i].Name = dgvJobList.Columns[i].DataPropertyName;
            dgvJobList.Columns[i].AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill;
        }

        private void GetJobs()
        {
            dgvJobList.DataSource = null;
            dgvJobList.Rows.Clear();
            
            using (var db = new JobContext())
            {
                var jobs = db.Jobs.Where(x => !x.IsDel).ToList();
                if (jobs != null && jobs.Count > 0)
                {
                    dgvJobList.DataSource = jobs;
                }
            }
        }

        private void tsbJobAdd_Click(object sender, EventArgs e)
        {
            var job = new Job();
            job.Name = "New job";
            job.Connection = new Connection()
            {
                Server = "localhost",
                Database = "db",
                AuthSql = true,
                AuthWindows = false,
                Login = "sa",
                Password = ""
            };
            job.Schedule = new Schedule();


            using (var db = new JobContext())
            {
                db.Jobs.Add(job);
                db.SaveChanges();
            }

            GetJobs();

            for (int i = 0; i < dgvJobList.RowCount; i++)
            {
                if (job.Id == (int) dgvJobList["Id", i].Value)
                {
                    dgvJobList.CurrentCell = dgvJobList["id", i];
                    break;
                }
            }
        }

        private string GetGoogleAccountCredentialsFolder()
        {
            var fullFolderPath = new FileDataStore(Program.AppName, false);

            var credPath = fullFolderPath.FolderPath;
            return credPath;
        }

        private bool IsGoogleAccountConnected()
        {
            var credPath = GetGoogleAccountCredentialsFolder();

            if (Directory.Exists(credPath) && Directory.GetFiles(credPath).Length>0)
            {
                return true;
            }

            return false;
        }

        private void btnGoogleAuthorize_Click(object sender, EventArgs e)
        {
            var credPath = GetGoogleAccountCredentialsFolder();
            if (IsGoogleAccountConnected())
            {
                try
                {
                    Directory.Delete(credPath, true);
                }
                catch (Exception exception)
                {
                    MessageBox.Show($"Ошибка удаления каталога '{credPath}'");
                }
            }


            string[] Scopes = { DriveService.Scope.DriveFile };
            UserCredential credential;

            using (var stream =
                new FileStream("GDriveCredentials.json", FileMode.Open, FileAccess.Read))
            {
                credential = GoogleWebAuthorizationBroker.AuthorizeAsync(
                    GoogleClientSecrets.Load(stream).Secrets, Scopes, "user", CancellationToken.None, new FileDataStore(credPath, true)).Result;
                Console.WriteLine("Credential file saved to: " + credPath);
            }

            // Create Drive API service.
            var service = new DriveService(new BaseClientService.Initializer()
            {
                HttpClientInitializer = credential,
                ApplicationName = Program.AppName,
            });

        }

        private void btnConTest_Click(object sender, EventArgs e)
        {
            var cnn = new SqlConnection
            {
                ConnectionString =
                    $"Data Source={txtConServer.Text};Initial Catalog={txtConDatabase.Text};User ID={txtConLogin.Text};Password={txtConPassword.Text};Timeout=15"
            };

            try
            {
                cnn.Open();
                if (cnn.State == ConnectionState.Open)
                {
                    MessageBox.Show("Test OK");
                }
                else
                {
                    MessageBox.Show($"Test Failed: {cnn.State.ToString()}");
                }

                cnn.Close();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }

        }

        private void SetControlAccessConAuth()
        {
            if (rdbConAuthSql.Checked)
            {
                txtConLogin.Enabled = true;
                txtConPassword.Enabled = true;
            }
            else
            {
                txtConLogin.Enabled = false;
                txtConPassword.Enabled = false;
            }
        }

        private void rdbConAuthSql_CheckedChanged(object sender, EventArgs e)
        {
            SetControlAccessConAuth();
        }

        private void rdbConAuthWin_CheckedChanged(object sender, EventArgs e)
        {
            SetControlAccessConAuth();
        }

        private void btnFIleSelect_Click(object sender, EventArgs e)
        {
            if (!string.IsNullOrEmpty(txtFolder.Text) && Directory.Exists(txtFolder.Text))
            {
                folderBrowserDialog1.SelectedPath = txtFolder.Text;
            }

            folderBrowserDialog1.ShowNewFolderButton = true;
            var result = folderBrowserDialog1.ShowDialog();
            if (result == DialogResult.OK)
            {
                txtFolder.Text = folderBrowserDialog1.SelectedPath;
            }
        }

        private void btnBackupAndUpload_Click(object sender, EventArgs e)
        {
            if (!IsGoogleAccountConnected())
            {
                MessageBox.Show("Сначала подключите аккаунт Google для загрузки файла!", Program.AppName,
                    MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            var frmDo = new FrmBackupAndUpload(
                txtConServer.Text,
                txtConDatabase.Text,
                rdbConAuthWin.Checked,
                (rdbConAuthSql.Checked ? txtConLogin.Text : ""),
                (rdbConAuthSql.Checked ? txtConPassword.Text : ""),
                true,
                true,
                true,
                txtConDatabase.Text.ToUpper(),
                !chbSaveToFolder.Checked,
                chbSaveToFolder.Checked ? txtFolder.Text : "",
                true
                );
            frmDo.Show();
            frmDo.Run();

        }

        private void btnBackupOnly_Click(object sender, EventArgs e)
        {
            var frmDo = new FrmBackupAndUpload(
                txtConServer.Text,
                txtConDatabase.Text,
                rdbConAuthWin.Checked,
                (rdbConAuthSql.Checked ? txtConLogin.Text : ""),
                (rdbConAuthSql.Checked ? txtConPassword.Text : ""),
                true,
                false,
                false,
                "",
                false,
                chbSaveToFolder.Checked ? txtFolder.Text : "",
                true
            );
            frmDo.Show();
            frmDo.Run();
        }

        private void chbSaveToFolder_CheckedChanged(object sender, EventArgs e)
        {
            if (chbSaveToFolder.Checked)
            {
                txtFolder.Enabled = true;
                btnFolderSelect.Enabled = true;
            }
            else
            {
                txtFolder.Enabled = false;
                btnFolderSelect.Enabled = false;
            }
        }

        private void dgvJobList_SelectionChanged(object sender, EventArgs e)
        {
            EnableDisableJobDetailControls(false);
            if (dgvJobList.CurrentCellAddress.Y < 0)
                return;

            var id = (int) dgvJobList["Id", dgvJobList.CurrentCellAddress.Y].Value;
            using (var db = new JobContext())
            {
                var job = db.Jobs.FirstOrDefault(x => x.Id == id);
                if (job == null)
                    return;

                FillData(job);
            }
        }

        private void FillData(Job job)
        {
            EnableDisableJobDetailControls(true);

            txtJobName.Text = job.Name;

            txtConServer.Text = job.Connection.Server;
            txtConDatabase.Text = job.Connection.Database;
            if (job.Connection.AuthSql)
            {
                rdbConAuthSql.Checked = true;
                rdbConAuthWin.Checked = false;

                txtConLogin.Text = job.Connection.Login;
                txtConPassword.Text = job.Connection.Password;
            }
            else if (job.Connection.AuthWindows)
            {
                rdbConAuthWin.Checked = true;
                rdbConAuthSql.Checked = false;

                txtConLogin.Text = "";
                txtConPassword.Text = "";
            }

            if (job.IsSaveToLocalFolder)
            {
                chbSaveToFolder.Checked = true;
                txtFolder.Text = job.FolderPath;
            }
            else
            {
                chbSaveToFolder.Checked = false;
                txtFolder.Text = "";
            }
            chbSaveToFolder_CheckedChanged(null, null);

            if (job.Schedule.DayToRun.RunMode == 0)//none
            {
                rdbAutorunModeNone.Checked = true;
                rdbAutorunModeAuto.Checked = false;

                chbMonday.Checked = job.Schedule.DayToRun.Monday;
                chbTuesday.Enabled = job.Schedule.DayToRun.Tuesday; 
                chbWednesday.Enabled = job.Schedule.DayToRun.Wednesday; 
                chbThursday.Enabled = job.Schedule.DayToRun.Thursday; 
                chbFriday.Enabled = job.Schedule.DayToRun.Friday; 
                chbSaturday.Enabled = job.Schedule.DayToRun.Saturday; 
                chbSunday.Enabled = job.Schedule.DayToRun.Sunday;

                dgvRunTime.DataSource = job.Schedule.TimeToRun;
            }
            else if (job.Schedule.DayToRun.RunMode == 1)//autorun
            {
                rdbAutorunModeNone.Checked = false;
                rdbAutorunModeAuto.Checked = true;

                chbMonday.Checked = job.Schedule.DayToRun.Monday;
                chbTuesday.Enabled = job.Schedule.DayToRun.Tuesday;
                chbWednesday.Enabled = job.Schedule.DayToRun.Wednesday;
                chbThursday.Enabled = job.Schedule.DayToRun.Thursday;
                chbFriday.Enabled = job.Schedule.DayToRun.Friday;
                chbSaturday.Enabled = job.Schedule.DayToRun.Saturday;
                chbSunday.Enabled = job.Schedule.DayToRun.Sunday;

                dgvRunTime.DataSource = job.Schedule.TimeToRun;
            }
            SetControlAccessRunMode();

        }

        private void SetControlAccessRunMode()
        {
            if (rdbAutorunModeAuto.Checked)
            {
                chbMonday.Enabled = true;
                chbTuesday.Enabled = true;
                chbWednesday.Enabled = true;
                chbThursday.Enabled = true;
                chbFriday.Enabled = true;
                chbSaturday.Enabled = true;
                chbSunday.Enabled = true;

                dgvRunTime.Enabled = true;
                dtpRunTime.Enabled = true;
                btnRunTimeAdd.Enabled = true;
                btnRunTimeDelete.Enabled = true;
            }
            else
            {
                chbMonday.Enabled = false;
                chbTuesday.Enabled = false;
                chbWednesday.Enabled = false;
                chbThursday.Enabled = false;
                chbFriday.Enabled = false;
                chbSaturday.Enabled = false;
                chbSunday.Enabled = false;

                dgvRunTime.Enabled = false;
                dtpRunTime.Enabled = false;
                btnRunTimeAdd.Enabled = false;
                btnRunTimeDelete.Enabled = false;
            }
        }

        private void rdbAutorunModeAuto_CheckedChanged(object sender, EventArgs e)
        {
            SetControlAccessRunMode();
        }

        private void rdbAutorunModeNone_CheckedChanged(object sender, EventArgs e)
        {
            SetControlAccessRunMode();
        }
    }


}
