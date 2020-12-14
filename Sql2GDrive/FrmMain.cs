using Google.Apis.Auth.OAuth2;
using Google.Apis.Drive.v3;
using Google.Apis.Services;
using Google.Apis.Util.Store;
using Microsoft.EntityFrameworkCore;
using Sql2GoogleDrive.DAL;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.IO;
using System.Linq;
using System.Threading;
using System.Windows.Forms;

namespace Sql2GDrive
{
    public partial class FrmMain : Form
    {
        private Job _job;
        private bool _isLoadingData;
        private int _jobRunCount = 0;

        public FrmMain()
        {
            InitializeComponent();

            dgvJobListSetup();
            dgvRunTimeListSetup();
            EnableDisableJobDetailControls(false);

            GetJobs();
            AdviseToControlEvents();

            lblCount.Text = $"Processed job count: {_jobRunCount}";
            timer1.Interval = 1000 * 60;//one per minute
            timer1.Start();
        }

        private void ClearJobDetailControls()
        {
            _job = null;

            txtJobName.Text = "";

            txtConServer.Text = "";
            txtConDatabase.Text = "";
            txtConLogin.Text = "";
            txtConPassword.Text = "";

            chbSaveToFolder.Checked = false;
            txtFolder.Text = "";

            rdbConAuthSql.Checked = false;
            rdbConAuthWin.Checked = false;

            rdbAutorunModeAuto.Checked = false;
            rdbAutorunModeNone.Checked = false;

            chbMonday.Checked = false;
            chbTuesday.Checked = false;
            chbWednesday.Checked = false;
            chbThursday.Checked = false;
            chbFriday.Checked = false;
            chbSaturday.Checked = false;
            chbSunday.Checked = false;
        }

        private void EnableDisableJobDetailControls(bool state)
        {
            txtJobName.Enabled = state;

            grbConnection.Enabled = state;
            grbSchedule.Enabled = state;
            chbSaveToFolder.Enabled = state;
            txtFolder.Enabled = state;
            btnFolderSelect.Enabled = state;


            rdbConAuthSql.Enabled = state;
            rdbConAuthWin.Enabled = state;

            rdbAutorunModeAuto.Enabled = state;
            rdbAutorunModeNone.Enabled = state;

            btnJobSave.Enabled = state;

            //btnBackupAndUpload.Enabled = state;
            //btnBackupOnly.Enabled = state;

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
            dgvJobList.Columns[i].Width = 20;
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

        private void dgvRunTimeListSetup()
        {
            dgvRunTime.DataSource = null;
            dgvRunTime.Columns.Clear();
            dgvRunTime.EditMode = DataGridViewEditMode.EditProgrammatically;
            dgvRunTime.AutoGenerateColumns = false;
            dgvRunTime.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
            dgvRunTime.AllowUserToAddRows = false;
            dgvRunTime.AllowUserToDeleteRows = false;
            dgvRunTime.RowHeadersVisible = false;

            int i = 0;
            dgvRunTime.ColumnCount = 2;

            dgvRunTime.Columns[i].Visible = true;
            dgvRunTime.Columns[i].HeaderText = "#";
            dgvRunTime.Columns[i].Width = 20;
            dgvRunTime.Columns[i].DataPropertyName = "Id";
            dgvRunTime.Columns[i].AutoSizeMode = DataGridViewAutoSizeColumnMode.None;
            dgvRunTime.Columns[i].Name = dgvRunTime.Columns[i].DataPropertyName;
            i += 1;
            dgvRunTime.Columns[i].Visible = true;
            dgvRunTime.Columns[i].HeaderText = "Time";
            dgvRunTime.Columns[i].Width = 75;
            dgvRunTime.Columns[i].DataPropertyName = "Time";
            dgvRunTime.Columns[i].DefaultCellStyle.Format = "HH:mm";
            dgvRunTime.Columns[i].Name = dgvRunTime.Columns[i].DataPropertyName;
            dgvRunTime.Columns[i].AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill;
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
                else
                {
                    ClearJobDetailControls();
                    EnableDisableJobDetailControls(false);
                    btnBackupAndUpload.Enabled = false;
                    btnBackupOnly.Enabled = false;
                }
            }
        }

        private void tsbJobAdd_Click(object sender, EventArgs e)
        {
            if (!CheckIfNeedSave())
                return;

            _isLoadingData = true;

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
            job.Schedule.TimeToRun = new List<TimeToRun>();

            using (var db = new JobContext())
            {
                db.TimeToRuns.AddRange(job.Schedule.TimeToRun);
                db.Schedules.Add(job.Schedule);
                db.Connections.Add(job.Connection);
                db.Jobs.Add(job);
                db.SaveChanges();
            }

            GetJobs();
            SetJobSelectedInList(job.Id);

            EnableDisableJobDetailControls(true);


            _isLoadingData = false;
        }

        private void SetJobSelectedInList(int jobId)
        {
            for (int i = 0; i < dgvJobList.RowCount; i++)
            {
                if ((int)dgvJobList["Id", i].Value == jobId)
                {
                    dgvJobList.CurrentCell = dgvJobList["id", i];
                    dgvJobList_SelectionChanged(null, null);
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

            RunJob(_job);
        }

        private void RunJob(Job job)
        {
           // var tProgressThread = new Thread(() =>
//Task.Run(() =>
               // {
                    var frmDo = new FrmBackupAndUpload(
                        job.Connection.Server, //txtConServer.Text,
                        job.Connection.Database, //txtConDatabase.Text,
                        job.Connection.AuthWindows, //rdbConAuthWin.Checked,
                        job.Connection.AuthSql
                            ? _job.Connection.Login
                            : "", //(rdbConAuthSql.Checked ? txtConLogin.Text : ""),
                        job.Connection.AuthSql
                            ? _job.Connection.Password
                            : "", //(rdbConAuthSql.Checked ? txtConPassword.Text : ""),
                        true,
                        true,
                        true,
                        job.Connection.Database, //txtConDatabase.Text.ToUpper(),
                        !job.IsSaveToLocalFolder, //!chbSaveToFolder.Checked,
                        job.IsSaveToLocalFolder ? job.FolderPath : "", //chbSaveToFolder.Checked ? txtFolder.Text : "",
                        true
                    );
                    frmDo.Show();
                    frmDo.Run();
                //}
            //);

        //tProgressThread.Start();


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

        private Job GetJobFullFromDb(int id)
        {
            using (var db = new JobContext())
            {
                var job = db.Jobs
                    .Include(x => x.Connection)
                    .Include(x => x.Schedule)
                    .ThenInclude(x => x.DayToRun)
                    .FirstOrDefault(x => x.Id == id);

                if (job != null)
                {
                    var scheduleId = job.ScheduleId;
                    var times = db.TimeToRuns.Where(x => x.ScheduleId == scheduleId).ToList();
                    if (times.Count > 0)
                    {
                        job.Schedule.TimeToRun = times;
                    }
                }

                return job;
            }
        }

        //спрашиваем о сохранении, перед тем как перейти на следующий элемент
        private bool CheckIfNeedSave()
        {
            if (_job == null)
                return true;
            if (_isLoadingData)
                return true;


            var jobInDb = GetJobFullFromDb(_job.Id);
            
            if (jobInDb.IsDel)
                return true;

            if (!jobInDb.Equals(_job))
            {
                var q = MessageBox.Show("Save changes?", Text, MessageBoxButtons.YesNoCancel,
                    MessageBoxIcon.Question);

                if (q == DialogResult.No)
                    return true;

                if (q == DialogResult.Yes)
                {
                    JobSave();
                    return true;
                }

                if (q == DialogResult.Cancel)
                    return false;

            }

            return true;

        }

        private void dgvJobList_SelectionChanged(object sender, EventArgs e)
        {
            EnableDisableJobDetailControls(false);
            btnBackupAndUpload.Enabled = false;
            btnBackupOnly.Enabled = false;

            if (dgvJobList.CurrentCellAddress.Y < 0)
                return;

            if (!CheckIfNeedSave())
                return;

            _isLoadingData = true;
            var id = (int) dgvJobList["Id", dgvJobList.CurrentCellAddress.Y].Value;
            using (var db = new JobContext())
            {
                _job = GetJobFullFromDb(id);
                if (_job == null)
                    return;

                FillData();
            }

            _isLoadingData = false;
        }

        private void FillData()
        {
            //EnableDisableJobDetailControls(true);

            txtJobName.Text = _job.Name;

            txtConServer.Text = _job.Connection.Server;
            txtConDatabase.Text = _job.Connection.Database;
            if (_job.Connection.AuthSql)
            {
                rdbConAuthSql.Checked = true;
                rdbConAuthWin.Checked = false;

                txtConLogin.Text = _job.Connection.Login;
                txtConPassword.Text = _job.Connection.Password;
            }
            else if (_job.Connection.AuthWindows)
            {
                rdbConAuthWin.Checked = true;
                rdbConAuthSql.Checked = false;

                txtConLogin.Text = "";
                txtConPassword.Text = "";
            }

            if (_job.IsSaveToLocalFolder)
            {
                chbSaveToFolder.Checked = true;
                txtFolder.Text = _job.FolderPath;
            }
            else
            {
                chbSaveToFolder.Checked = false;
                txtFolder.Text = "";
            }
            chbSaveToFolder_CheckedChanged(null, null);

            if (_job.Schedule.DayToRun.RunMode == 0)//none
            {
                rdbAutorunModeNone.Checked = true;
                rdbAutorunModeAuto.Checked = false;

                chbMonday.Checked = false;
                chbTuesday.Checked = false;
                chbWednesday.Checked = false;
                chbThursday.Checked = false;
                chbFriday.Checked = false;
                chbSaturday.Checked = false;
                chbSunday.Checked = false;

                dgvRunTime.DataSource = _job.Schedule.TimeToRun;
            }
            else if (_job.Schedule.DayToRun.RunMode == 1)//autorun
            {
                rdbAutorunModeNone.Checked = false;
                rdbAutorunModeAuto.Checked = true;

                chbMonday.Checked = _job.Schedule.DayToRun.Monday;
                chbTuesday.Checked = _job.Schedule.DayToRun.Tuesday;
                chbWednesday.Checked = _job.Schedule.DayToRun.Wednesday;
                chbThursday.Checked = _job.Schedule.DayToRun.Thursday;
                chbFriday.Checked = _job.Schedule.DayToRun.Friday;
                chbSaturday.Checked = _job.Schedule.DayToRun.Saturday;
                chbSunday.Checked = _job.Schedule.DayToRun.Sunday;

                dgvRunTime.DataSource = _job.Schedule.TimeToRun.OrderBy(x => x.Time).ToList();
            }
            SetControlAccessRunMode();

            btnBackupAndUpload.Enabled = true;
            btnBackupOnly.Enabled = true;
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

        private void AdviseToControlEvents()
        {
            txtJobName.TextChanged += (o, e) =>
            {
                if (_job == null) return;
                if (_isLoadingData) return;
                _job.Name = ((TextBox)o).Text;
            };
            txtConServer.TextChanged += (o, e) =>
            {
                if (_job == null) return;
                if (_isLoadingData) return;
                _job.Connection.Server = ((TextBox)o).Text;
            };
            txtConDatabase.TextChanged += (o, e) =>
            {
                if (_job == null) return;
                if (_isLoadingData) return;
                _job.Connection.Database= ((TextBox)o).Text;
            };
            txtConLogin.TextChanged += (o, e) =>
            {
                if (_job == null) return;
                if (_isLoadingData) return;
                _job.Connection.Login= ((TextBox)o).Text;
            };
            txtConPassword.TextChanged += (o, e) =>
            {
                if (_job == null) return;
                if (_isLoadingData) return;
                _job.Connection.Password= ((TextBox)o).Text;
            };
            rdbConAuthSql.CheckedChanged += (o, e) =>
            {
                if (_job == null) return;
                if (_isLoadingData) return;
                _job.Connection.AuthSql = rdbConAuthSql.Checked;
                //_job.Connection.AuthWindows = !rdbConAuthSql.Checked;
            };
            rdbConAuthWin.CheckedChanged += (o, e) =>
            {
                if (_job == null) return;
                if (_isLoadingData) return;
                //_job.Connection.AuthSql = !rdbConAuthWin.Checked;
                _job.Connection.AuthWindows = rdbConAuthWin.Checked;
            };

            chbSaveToFolder.CheckedChanged += (o, e) =>
            {
                if (_job == null) return;
                if (_isLoadingData) return;
                if (chbSaveToFolder.Checked)
                {
                    _job.IsSaveToLocalFolder = true;
                    _job.FolderPath = txtFolder.Text;
                }
                else
                {
                    _job.IsSaveToLocalFolder = false;
                    _job.FolderPath = "";
                }
            };
            txtFolder.TextChanged += (o, e) =>
            {
                if (_job == null) return;
                if (_isLoadingData) return;
                _job.FolderPath= ((TextBox)o).Text;
            };

            rdbAutorunModeAuto.CheckedChanged += (o, e) =>
            {
                if (_job == null) return;
                if (_isLoadingData) return;
                _job.Schedule.DayToRun.RunMode = 1;//0 - none, 1 - autorun
            };
            rdbAutorunModeNone.CheckedChanged += (o, e) =>
            {
                if (_job == null) return;
                if (_isLoadingData) return;
                _job.Schedule.DayToRun.RunMode = 0;// rdbAutorunModeAuto.Checked;
            };

            chbMonday.CheckedChanged += (o, e) =>
            {
                if (_job == null) return;
                if (_isLoadingData) return;
                _job.Schedule.DayToRun.Monday = ((CheckBox) o).Checked;
            };
            chbTuesday.CheckedChanged += (o, e) =>
            {
                if (_job == null) return;
                if (_isLoadingData) return;
                _job.Schedule.DayToRun.Tuesday = ((CheckBox)o).Checked;
            };
            chbWednesday.CheckedChanged += (o, e) =>
            {
                if (_job == null) return;
                if (_isLoadingData) return;
                _job.Schedule.DayToRun.Wednesday = ((CheckBox)o).Checked;
            };
            chbThursday.CheckedChanged += (o, e) =>
            {
                if (_job == null) return;
                if (_isLoadingData) return;
                _job.Schedule.DayToRun.Thursday = ((CheckBox)o).Checked;
            };
            chbFriday.CheckedChanged += (o, e) =>
            {
                if (_job == null) return;
                if (_isLoadingData) return;
                _job.Schedule.DayToRun.Friday = ((CheckBox)o).Checked;
            };
            chbSaturday.CheckedChanged += (o, e) =>
            {
                if (_job == null) return;
                if (_isLoadingData) return;
                _job.Schedule.DayToRun.Saturday = ((CheckBox)o).Checked;
            };
            chbSunday.CheckedChanged += (o, e) =>
            {
                if (_job == null) return;
                if (_isLoadingData) return;
                _job.Schedule.DayToRun.Sunday = ((CheckBox)o).Checked;
            };





            //txtNameShort.TextChanged += (o, e) =>
            //{
            //    if (!_formFilled) return;
            //    _formElementObject.NameShort = ((TextBox)o).Text;
            //};
            /*
            chbIsUser.CheckedChanged += (o, e) =>
                                            {
                                                if (!_formFilled) return;
                                                if (chbIsUser.Checked)
                                                {
                                                    btnUserOpen.Enabled = true;
                                                    cmbUser.Enabled = true;
                                                    var u = new User((long) cmbUser.SelectedValue,
                                                                 OndMain.mainSettings.local_CurrentUser);
                                                    _formElementObject.user = u;
                                                }
                                                else
                                                {
                                                    btnUserOpen.Enabled = false;
                                                    cmbUser.Enabled = false;
                                                    _formElementObject.user = null;
                                                }
                                            };
            cmbUser.SelectedValueChanged += (o, e) =>
                                                {
                                                    if (!_formFilled) return;
                                                    if (chbIsUser.Checked)
                                                    {
                                                        var u = new User( (long) ((ComboBox)o).SelectedValue;,
                                                                     OndMain.mainSettings.local_CurrentUser);
                                                        _formElementObject.user = u;
                                                    }
                                                    else
                                                    {
                                                        _formElementObject.user = null;
                                                    }

                                                };
            nudScheduleEndMinute.ValueChanged += (o, e) =>
            {
                if (!_formFilled) return;
                _formElementObject.scheduleEndMinute = (int)nudScheduleEndMinute.Value;
            };
            */


        }

        private void btnJobSave_Click(object sender, EventArgs e)
        {
            JobSave();
        }

        private void JobSave()
        {
            using (var db = new JobContext())
            {
                var con = _job.Connection;
                if (con.Id == 0)
                    db.Connections.Add(con);
                else
                {
                    db.Connections.Update(con);
                }
                db.SaveChanges();

                for (int i = 0; i < _job.Schedule.TimeToRun.Count; i++)
                {
                    var time = _job.Schedule.TimeToRun[i];
                    if (time.Id == 0)
                        db.TimeToRuns.Add(time);
                    else
                    {
                        db.TimeToRuns.Update(time);
                    }
                }
                db.SaveChanges();


                var sch = _job.Schedule;
                if (sch.Id == 0)
                    db.Schedules.Add(sch);
                else
                {
                    db.Schedules.Update(sch);
                }
                db.SaveChanges();

                db.Jobs.Update(_job);
                db.SaveChanges();
            }

            for (int i = 0; i < dgvJobList.RowCount; i++)
            {
                if ((int)dgvJobList["Id", i].Value == _job.Id)
                {
                    dgvJobList["Name", i].Value = _job.Name;
                    break;
                }
            }
        }

        private void btnRunTimeAdd_Click(object sender, EventArgs e)
        {
            var timeToRun = new TimeToRun(_job.ScheduleId, dtpRunTime.Value);
            _job.Schedule.TimeToRun.Add(timeToRun);
            dgvRunTime.DataSource = _job.Schedule.TimeToRun.OrderBy(x => x.Time).ToList();
        }

        private void timer1_Tick(object sender, EventArgs e)
        {
            RunJobsByTimer();
        }

        private void RunJobsByTimer()
        {
            var runToday = new DayToRun();
            switch (DateTime.Today.DayOfWeek)
            {
                case DayOfWeek.Monday:
                    runToday.Monday = true;
                    break;
                case DayOfWeek.Tuesday:
                    runToday.Tuesday = true;
                    break;
                case DayOfWeek.Wednesday:
                    runToday.Wednesday= true;
                    break;
                case DayOfWeek.Thursday:
                    runToday.Thursday = true;
                    break;
                case DayOfWeek.Friday:
                    runToday.Friday = true;
                    break;
                case DayOfWeek.Saturday:
                    runToday.Saturday = true;
                    break;
                case DayOfWeek.Sunday:
                    runToday.Sunday = true;
                    break;
            }

            var hourNow = DateTime.Now.Hour;
            var minuteNow = DateTime.Now.Minute;


            using (var db = new JobContext())
            {
                var jobIdsForRun = db.Jobs
                    .Where(x => !x.IsDel
                                && x.Schedule.DayToRun.RunMode == 1
                                )
                    .Select(x => x.Id)
                    .ToList();

                if (jobIdsForRun.Count > 0)
                {
                    foreach (var jobId in jobIdsForRun)
                    {
                        var job = GetJobFullFromDb(jobId);
                        //не смогло транслировать в линкью
                        //&& x.Schedule.DayToRun.EqualsDate(runToday)
                        if (job.Schedule.DayToRun.EqualsDate(runToday))
                        {
                            foreach (var jobTime in job.Schedule.TimeToRun)
                            {
                                if (jobTime.Time.Hour == hourNow && jobTime.Time.Minute == minuteNow)
                                {
                                    RunJob(job);
                                    _jobRunCount++;
                                    lblCount.Text = $"Processed job count: {_jobRunCount}";
                                }
                            }
                        }
                    }
                }
            }
        }

        private void btnRunTimeDelete_Click(object sender, EventArgs e)
        {

        }

        private void tsbJobEdit_Click(object sender, EventArgs e)
        {
            if (dgvJobList.CurrentCellAddress.Y < 0)
                return;

            EnableDisableJobDetailControls(true);
        }

        private void tsbJobDelete_Click(object sender, EventArgs e)
        {
            if (dgvJobList.CurrentCellAddress.Y < 0)
                return;

            var result =
                MessageBox.Show(
                    $"Do you want to delete job '{dgvJobList["Name", dgvJobList.CurrentCellAddress.Y].Value}'?",
                    Program.AppName, MessageBoxButtons.YesNoCancel, MessageBoxIcon.Question);

            if (result != DialogResult.Yes)
                return;

            var jobId = (int)dgvJobList["Id", dgvJobList.CurrentCellAddress.Y].Value;

            using (var db = new JobContext())
            {
                var job = db.Jobs.Where(x => x.Id == jobId).FirstOrDefault();
                if (job != null)
                {
                    job.IsDel = true;
                    db.SaveChanges();
                }
            }

            GetJobs();
        }
    }


}
