using Google.Apis.Auth.OAuth2;
using Google.Apis.Drive.v3;
using Google.Apis.Services;
using Google.Apis.Util.Store;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Net.Http;
using System.Threading;
using System.Windows.Forms;
using Google.Apis.Upload;

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


            var backupFileName = @$"{database}_{DateTime.Now.ToString("yyyy-MM-dd_HH-mm-ss")}.bak";
            var backupFolderName = folderForBackupFile;
            if (deleteLocalBackup || !Directory.Exists(folderForBackupFile))
            {
                backupFolderName = Path.GetDirectoryName(Application.ExecutablePath);
            }
            var backupFileFullPath = Path.Combine(backupFolderName!, backupFileName);

            txtLog.Text += $"Server: {server} {Environment.NewLine}";
            txtLog.Text += $"Database: {database} {Environment.NewLine}";
            txtLog.Text += isIntegratedSecurity ? $"Windows authentication {Environment.NewLine}" : $"SQL authentication, login: {login} {Environment.NewLine}";
            txtLog.Text += "Target backup file: " + (deleteLocalBackup ? "no" : backupFileFullPath) + $" {Environment.NewLine}";
            if (uploadToGoogleDrive)
            {
                txtLog.Text += $"Upload to cloud: yes {Environment.NewLine}";
                txtLog.Text += "Target in cloud: " + (needToCreateGoogleFolder ? folderForBackupFile : "root") + $" {Environment.NewLine}";
            }
            else
            {
                txtLog.Text += $"Upload to cloud: no {Environment.NewLine}";
            }
            txtLog.Text += $"Backup start at: {DateTime.Now.ToString("dd.MM.yyyy HH:mm:ss")} {Environment.NewLine}";

            try
            {
                var cnn = new SqlConnection
                {
                    ConnectionString = $"{GetConnectionStringFromForm()};Timeout=0"
                };

                //create backup
                var backup = new MsSqlBackup();
                backup.BackupDatabaseProgressChanged += BackupOnBackupAndUploadDatabaseProgressChanged;
                backup.BackupDatabase(cnn, database, backupFileName, "Backup created by Sql2GoogleDrive",
                    backupFileFullPath, compressBackup);
            }
            catch (Exception exception)
            {
                txtLog.Text += $"{DateTime.Now.ToString("dd.MM.yyyy HH:mm:ss")} Error: {exception.Message} {Environment.NewLine}";
                EndOfProcess();
                return;
            }
            txtLog.Text += $"Backup end at: {DateTime.Now.ToString("dd.MM.yyyy HH:mm:ss")} {Environment.NewLine}";

            if (uploadToGoogleDrive)
            {
                //upload backup
                var folder = googleFolder;

                string[] Scopes = {DriveService.Scope.DriveFile};
                string FolderMimeType = "application/vnd.google-apps.folder";
                UserCredential credential;

                txtLog.Text +=
                    $"Upload to Google Drive start at: {DateTime.Now.ToString("dd.MM.yyyy HH:mm:ss")} {Environment.NewLine}";
                txtLog.Text += $"Try to get credentials.... {Environment.NewLine}";

                try
                {
                    using (var stream =
                        new FileStream("GDriveCredentials.json", FileMode.Open, FileAccess.Read))
                    {
                        var credPath = System.Environment.GetFolderPath(
                            System.Environment.SpecialFolder.Personal);
                        credPath = Path.Combine(credPath, ".credentials/drive-dotnet-quickstart.json");

                        credential = GoogleWebAuthorizationBroker.AuthorizeAsync(
                            GoogleClientSecrets.Load(stream).Secrets, Scopes, "user", CancellationToken.None,
                            new FileDataStore(credPath, true)).Result;
                        Console.WriteLine("Credential file saved to: " + credPath);
                    }

                    txtLog.Text += $"Credentials - OK {Environment.NewLine}";


                    // Create Drive API service.
                    var service = new DriveService(new BaseClientService.Initializer()
                    {
                        HttpClientInitializer = credential,
                        ApplicationName = Program.AppName,
                    });
                    txtLog.Text += $"Create Drive API service - OK. {service.Name} {Environment.NewLine}";

                    string folderId = "";
                    if (needToCreateGoogleFolder)
                    {
                        txtLog.Text += $"Try to Get or Create folder '{googleFolder}'... {Environment.NewLine}";
                        //find folder
                        var folderList = new List<Google.Apis.Drive.v3.Data.File>();

                        string pageToken = null;
                        while (true)
                        {
                            FilesResource.ListRequest listRequest = service.Files.List();
                            listRequest.PageSize = 10;
                            listRequest.PageToken = pageToken;
                            listRequest.Fields = "nextPageToken, files(id, name, mimeType, size)";

                            var fileList = listRequest.Execute();

                            // List files.
                            IList<Google.Apis.Drive.v3.Data.File> folders = fileList.Files;
                            if (folders != null && folders.Count > 0)
                            {
                                folderList.AddRange(folders.Where(x =>
                                    x.MimeType.Equals(FolderMimeType, StringComparison.InvariantCultureIgnoreCase)));
                            }

                            pageToken = fileList.NextPageToken;
                            if (pageToken == null)
                                break;
                        }

                        folderId = folderList.FirstOrDefault(folderFile => folderFile.Name == googleFolder)?.Id ??
                                   "";
                        if (string.IsNullOrEmpty(folderId))
                        {
                            var bodyFolder = new Google.Apis.Drive.v3.Data.File
                            {
                                Name = googleFolder,
                                MimeType = FolderMimeType
                            };

                            var createdFolder = service
                                .Files
                                .Create(bodyFolder)
                                .Execute();

                            folderId = createdFolder.Id;
                            txtLog.Text += $"Create folder '{googleFolder}' - OK {Environment.NewLine}";
                        }
                        else
                        {
                            txtLog.Text += $"Get folder '{googleFolder}' - OK {Environment.NewLine}";
                        }
                    }

                    txtLog.Text += $"Try to upload file '{backupFileName}'... {Environment.NewLine}";
                    //UPLOAD NON-EXISTING TO DRIVE
                    var body = new Google.Apis.Drive.v3.Data.File
                    {
                        Name = backupFileName,
                        Description = $"backup file of database '{database}'",
                        MimeType = "application/octet-stream",
                        ModifiedTime = File.GetLastWriteTime(backupFileFullPath)
                    };
                    if (needToCreateGoogleFolder && !string.IsNullOrEmpty(folderId))
                    {
                        body.Parents = new List<string>() {folderId};
                    }

                    var byteArray = File.ReadAllBytes(backupFileFullPath);
                    var streamFile = new MemoryStream(byteArray);
                    fileForUploadSize = byteArray.LongLength;

                    var req = service.Files.Create(body, streamFile, "application/octet-stream");
                    req.Fields = "id";
                    req.ProgressChanged += ReqOnProgressChanged;
                    this.UploadFileProgressChanged += DisplayUploadFileProgress;
                    var t = req.UploadAsync();
                    t.Wait();
                    var progress = req.GetProgress();
                    txtLog.Text +=
                        $"Upload to Google Drive end at: {DateTime.Now.ToString("dd.MM.yyyy HH:mm:ss")} {Environment.NewLine}";
                }
                catch (Exception exception) when (exception is Google.GoogleApiException ||
                                                  exception is HttpRequestException ||
                                                  exception is Exception)
                {
                    txtLog.Text +=
                        $"{DateTime.Now.ToString("dd.MM.yyyy HH:mm:ss")} Error: {exception.Message} {Environment.NewLine}";
                    EndOfProcess();
                    return;
                }

                try
                {
                    if (deleteLocalBackup && File.Exists(backupFileFullPath))
                    {
                        txtLog.Text += $"Try to delete local file '{backupFileFullPath}'... {Environment.NewLine}";
                        File.Delete(backupFileFullPath);
                        txtLog.Text += $"Delete local file - OK {Environment.NewLine}";
                    }
                }
                catch (Exception exception)
                {
                    txtLog.Text +=
                        $"{DateTime.Now.ToString("dd.MM.yyyy HH:mm:ss")} Error: {exception.Message} {Environment.NewLine}";
                    EndOfProcess();
                    return;
                }
            }

            EndOfProcess();
        }

        private void EndOfProcess()
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


        private string GetConnectionStringFromForm()
        {
            if (isIntegratedSecurity)
            {
                return $"Data Source={server};Initial Catalog={database}; Integrated Security = True";
            }
            else
            {
                return $"Data Source={server};Initial Catalog={database};User ID={login};Password={password}";
            }
        }

        private void BackupOnBackupAndUploadDatabaseProgressChanged(string status, int backuppercent)
        {
            if (backuppercent >= 0 && backuppercent <= 100)
            {
                btnProcess.Text = $"Backup process {backuppercent}% completed (1/2)";
                prbBackupAndUpload.Value = backuppercent;
                this.Refresh();
            }
        }

        private delegate void UploadFileProgressHandler(string status, long bytesSent, long bytesAll, int uploadingPercent);
        private event UploadFileProgressHandler UploadFileProgressChanged;

        private void ReqOnProgressChanged(IUploadProgress progress)
        {
            var uploadingPercent = (int)(progress.BytesSent * 100 / fileForUploadSize);
            UploadFileProgressChanged?.Invoke(progress.Status.ToString(), progress.BytesSent, fileForUploadSize, uploadingPercent);
        }

        private void DisplayUploadFileProgress(string status, long bytesSent, long bytesAll, int uploadingPercent)
        {
            CheckForIllegalCrossThreadCalls = false;
            btnProcess.Text = $"Upload process {uploadingPercent}% completed (2/2)";
            prbBackupAndUpload.Value = 100 + uploadingPercent;
            this.Refresh();
        }

        private void btnProcess_Click(object sender, EventArgs e)
        {
            Close();
        }
    }
}
