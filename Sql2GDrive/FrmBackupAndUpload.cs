using Sql2GoogleDrive;
using System;
using System.Threading;
using System.Windows.Forms;

namespace Sql2GDrive
{
    public partial class FrmBackupAndUpload : Form
    {
        private string server;
        private string database;
        private bool isIntegratedSecurity;
        private string login;
        private string password;
        private bool compressBackup;
        private bool uploadToGoogleDrive;
        private bool needToCreateGoogleFolder;
        private string googleFolder;
        private bool deleteLocalBackup;
        private string folderForBackupFile;
        private bool waitUserPressButtonForCloseForm;

        private long fileForUploadSize = 1;

        private Worker worker;

        private Thread workerThread;

        public FrmBackupAndUpload(
            string server,
            string database,
            bool isIntegratedSecurity,
            string login,
            string password,
            bool compressBackup,
            bool uploadToGoogleDrive,
            bool needToCreateGoogleFolder,
            string googleFolder,
            bool deleteLocalBackup,
            string folderForBackupFile,
            bool waitUserPressButtonForCloseForm)
        {
            this.server = server;
            this.database = database.ToUpper();
            this.isIntegratedSecurity = isIntegratedSecurity;
            this.login = login;
            this.password = password;
            this.compressBackup = compressBackup;
            this.uploadToGoogleDrive = uploadToGoogleDrive;
            this.googleFolder = googleFolder;
            this.deleteLocalBackup = deleteLocalBackup;
            this.needToCreateGoogleFolder = needToCreateGoogleFolder;
            this.folderForBackupFile = folderForBackupFile;
            this.waitUserPressButtonForCloseForm = waitUserPressButtonForCloseForm;

            InitializeComponent();
        }

        public void Run()
        {
            txtLog.Text = "";
            txtLog.ReadOnly = true;

            btnProcess.Enabled = false;
            prbBackupAndUpload.Visible = true;

            prbBackupAndUpload.Minimum = 0;
            prbBackupAndUpload.Maximum = uploadToGoogleDrive ? 202 : 102;

            var appPath = Application.ExecutablePath;
            
            worker = new Worker(
                                appPath, 
                                server,
                                 database,
                                 isIntegratedSecurity,
                                 login,
                                 password,
                                 compressBackup,
                                 uploadToGoogleDrive,
                                 needToCreateGoogleFolder,
                                 googleFolder,
                                 deleteLocalBackup,
                                 folderForBackupFile,
                                 waitUserPressButtonForCloseForm
                                 );

            worker.OnLog += new EventHandler<WorkerLogEvent>(OnWorkerLogEvent);
            worker.WorkerProgressChanged += new EventHandler<WorkerProgressChangedArgs>(OnWorkerProgressChanged);
            worker.OnStop += new EventHandler<WorkerOnStopEvent>(EndOfProcess);
            workerThread = new Thread(new ThreadStart(worker.StartWork));
            workerThread.Start();
        }

        private void OnWorkerLogEvent(object sender, WorkerLogEvent e)
        {
            // Cross thread - so you don't get the cross-threading exception
            if (this.InvokeRequired)
            {
                this.BeginInvoke((MethodInvoker)delegate
                {
                    OnWorkerLogEvent(sender, e);
                });
                return;
            }

            // Change control
            txtLog.Text += e.Log;
        }

        private void OnWorkerProgressChanged(object sender, WorkerProgressChangedArgs e)
        {
            // Cross thread - so you don't get the cross-threading exception
            if (this.InvokeRequired)
            {
                this.BeginInvoke((MethodInvoker)delegate
                {
                    OnWorkerProgressChanged(sender, e);
                });
                return;
            }

            // Change control
            btnProcess.Text = e.Progress;
            prbBackupAndUpload.Value = e.ProgressValue;
        }

        private void EndOfProcess(object sender, WorkerOnStopEvent e)
        {
            if (waitUserPressButtonForCloseForm)
            {
                btnProcess.Enabled = true;
                if (uploadToGoogleDrive)
                {
                    btnProcess.Text = "Backup and upload completed. Press button for close form.";
                }
                else
                {
                    btnProcess.Text = "Backup completed. Press button for close form.";
                }

                prbBackupAndUpload.Visible = false;
            }
            else
            {
                Close();
            }
        }

        private void btnProcess_Click(object sender, EventArgs e)
        {
            Close();
        }
    }
}
