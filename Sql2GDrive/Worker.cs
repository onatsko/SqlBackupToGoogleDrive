using Google.Apis.Auth.OAuth2;
using Google.Apis.Drive.v3;
using Google.Apis.Services;
using Google.Apis.Upload;
using Google.Apis.Util.Store;
using Sql2GDrive;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.IO;
using System.Linq;
using System.Net.Http;
using System.Threading;

namespace Sql2GoogleDrive;

public class Worker
{
    private string appPath;
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

    public Worker(
                string appPath,
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
        this.appPath = appPath;
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
    }


    public event EventHandler<WorkerLogEvent> OnLog;
    public event EventHandler<WorkerProgressChangedArgs> WorkerProgressChanged;
    public event EventHandler<WorkerOnStopEvent> OnStop;


    public void StartWork()
    {
        var backupFileName = @$"{database}_{DateTime.Now.ToString("yyyy-MM-dd_HH-mm-ss")}.bak";
        var backupFolderName = folderForBackupFile;
        if (deleteLocalBackup || !Directory.Exists(folderForBackupFile))
        {
            backupFolderName = Path.GetDirectoryName(appPath);
        }
        var backupFileFullPath = Path.Combine(backupFolderName!, backupFileName);

        LogToEvent($"Server: {server} {Environment.NewLine}");
        LogToEvent($"Database: {database} {Environment.NewLine}");
        LogToEvent(isIntegratedSecurity ? $"Windows authentication {Environment.NewLine}" : $"SQL authentication, login: {login} {Environment.NewLine}");
        LogToEvent("Target backup file: " + (deleteLocalBackup ? "no" : backupFileFullPath) + $" {Environment.NewLine}");
        if (uploadToGoogleDrive)
        {
            LogToEvent($"Upload to cloud: yes {Environment.NewLine}");
            LogToEvent("Target in cloud: " + (needToCreateGoogleFolder ? folderForBackupFile : "root") + $" {Environment.NewLine}");
        }
        else
        {
            LogToEvent($"Upload to cloud: no {Environment.NewLine}");
        }
        LogToEvent($"Backup start at: {DateTime.Now.ToString("dd.MM.yyyy HH:mm:ss")} {Environment.NewLine}");

        try
        {
            var cnn = new SqlConnection
            {
                ConnectionString = $"{GetConnectionStringFromParams()};Timeout=0"
            };

            //create backup
            var backup = new MsSqlBackup();
            backup.BackupDatabaseProgressChanged += BackupOnBackupAndUploadDatabaseProgressChanged;
            backup.BackupDatabase(cnn, database, backupFileName, "Backup created by Sql2GoogleDrive",
                backupFileFullPath, compressBackup);
        }
        catch (Exception exception)
        {
            LogToEvent($"{DateTime.Now.ToString("dd.MM.yyyy HH:mm:ss")} Error: {exception.Message} {Environment.NewLine}");
            EndOfProcess();
            return;
        }
        LogToEvent($"Backup end at: {DateTime.Now.ToString("dd.MM.yyyy HH:mm:ss")} {Environment.NewLine}");

        if (uploadToGoogleDrive)
        {
            //upload backup
            var folder = googleFolder;

            string[] Scopes = { DriveService.Scope.DriveFile };
            string FolderMimeType = "application/vnd.google-apps.folder";
            UserCredential credential;

            LogToEvent($"Upload to Google Drive start at: {DateTime.Now.ToString("dd.MM.yyyy HH:mm:ss")} {Environment.NewLine}");
            LogToEvent($"Try to get credentials.... {Environment.NewLine}");

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

                LogToEvent($"Credentials - OK {Environment.NewLine}");


                // Create Drive API service.
                var service = new DriveService(new BaseClientService.Initializer()
                {
                    HttpClientInitializer = credential,
                    ApplicationName = Program.AppName,
                });
                LogToEvent($"Create Drive API service - OK. {service.Name} {Environment.NewLine}");

                string folderId = "";
                if (needToCreateGoogleFolder)
                {
                    LogToEvent($"Try to Get or Create folder '{googleFolder}'... {Environment.NewLine}");
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
                        LogToEvent($"Create folder '{googleFolder}' - OK {Environment.NewLine}");
                    }
                    else
                    {
                        LogToEvent($"Get folder '{googleFolder}' - OK {Environment.NewLine}");
                    }
                }

                LogToEvent($"Try to upload file '{backupFileName}'... {Environment.NewLine}");
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
                    body.Parents = new List<string>() { folderId };
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
                LogToEvent($"Upload to Google Drive end at: {DateTime.Now.ToString("dd.MM.yyyy HH:mm:ss")} {Environment.NewLine}");
            }
            catch (Exception exception) when (exception is Google.GoogleApiException ||
                                              exception is HttpRequestException ||
                                              exception is Exception)
            {
                LogToEvent($"{DateTime.Now.ToString("dd.MM.yyyy HH:mm:ss")} Error: {exception.Message} {Environment.NewLine}");
                EndOfProcess();
                return;
            }

            try
            {
                if (deleteLocalBackup && File.Exists(backupFileFullPath))
                {
                    LogToEvent($"Try to delete local file '{backupFileFullPath}'... {Environment.NewLine}");
                    File.Delete(backupFileFullPath);
                    LogToEvent($"Delete local file - OK {Environment.NewLine}");
                }
            }
            catch (Exception exception)
            {
                LogToEvent($"{DateTime.Now.ToString("dd.MM.yyyy HH:mm:ss")} Error: {exception.Message} {Environment.NewLine}");
                EndOfProcess();
                return;
            }
        }

        EndOfProcess();
    }

    private void LogToEvent(string log)
    {
        OnLogEvent(new WorkerLogEvent(log));
    }

    private void OnLogEvent(WorkerLogEvent e)
    {
        if (OnLog != null)
        {
            OnLog(this, e);
        }
    }

    private void EndOfProcess()
    {
        if (OnStop != null)
        {
            OnStop(this, new WorkerOnStopEvent());
        }
    }

    private string GetConnectionStringFromParams()
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
            if (WorkerProgressChanged != null)
            {
                WorkerProgressChanged(this, new WorkerProgressChangedArgs($"Backup process {backuppercent}% completed (1/2)", backuppercent));
            }
        }
    }

    private delegate void UploadFileProgressHandler(int uploadingPercent);
    private event UploadFileProgressHandler UploadFileProgressChanged;

    private void ReqOnProgressChanged(IUploadProgress progress)
    {
        var uploadingPercent = (int)(progress.BytesSent * 100 / fileForUploadSize);
        UploadFileProgressChanged?.Invoke(uploadingPercent);
    }

    private void DisplayUploadFileProgress(int uploadingPercent)
    {
        WorkerProgressChanged(this, new WorkerProgressChangedArgs($"Backup process {uploadingPercent}% completed (2/2)", 100 + uploadingPercent));
    }

}
