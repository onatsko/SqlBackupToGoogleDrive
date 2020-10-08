using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using System.Threading;
using Google.Apis.Auth.OAuth2;
using Google.Apis.Drive.v3;
using Google.Apis.Drive.v3.Data;
using Google.Apis.Services;
using Google.Apis.Upload;
using Google.Apis.Util.Store;
using File = Google.Apis.Drive.v3.Data.File;

namespace SqlBackupToGoogleDrive
{
    public class File2GDrive
    {
        // If modifying these scopes, delete your previously saved credentials
        // at ~/.credentials/drive-dotnet-quickstart.json
        static string[] Scopes = { DriveService.Scope.DriveFile };
        static string ApplicationName = "File2GDrive";

        private readonly DriveService service;
        private readonly UserCredential credential;
        public File2GDrive()
        {
            using (var stream =
                new FileStream("GDriveCredentials.json", FileMode.Open, FileAccess.Read))
            {
                // The file token.json stores the user's access and refresh tokens, and is created
                // automatically when the authorization flow completes for the first time.
                string credPath = "token.json";
                credential = GoogleWebAuthorizationBroker.AuthorizeAsync(
                    GoogleClientSecrets.Load(stream).Secrets,
                    Scopes,
                    "user",
                    CancellationToken.None,
                    new FileDataStore(credPath, true)).Result;
                Console.WriteLine("Credential file saved to: " + credPath);
            }

            // Create Drive API service.
            service = new DriveService(new BaseClientService.Initializer()
            {
                HttpClientInitializer = credential,
                ApplicationName = ApplicationName,
            });
        }

        public void GetFilesList()
        {
            // Define parameters of request.
            FilesResource.ListRequest listRequest = service.Files.List();
            listRequest.PageSize = 10;
            listRequest.Fields = "nextPageToken, files(id, name)";

            // List files.
            IList<Google.Apis.Drive.v3.Data.File> files = listRequest.Execute()
                .Files;
            Console.WriteLine("Files:");
            if (files != null && files.Count > 0)
            {
                foreach (var file in files)
                {
                    Console.WriteLine("{0} ({1})", file.Name, file.Id);
                }
            }
            else
            {
                Console.WriteLine("No files found.");
            }
        }

        public void CreateFolder(string folderName)
        {
            File body = new File();
            body.Name = folderName;
            body.MimeType = "application/vnd.google-apps.folder";

            service.Files.Create(body).Execute();
        }

        public void UploadFile(FileInfo file)
        {
            var fileMetadata = new File()
            {
                Name = file.Name,
                MimeType = "application/octet-stream"
            };
            FilesResource.CreateMediaUpload request;
            using (var stream = new System.IO.FileStream(file.FullName,
                System.IO.FileMode.Open))
            {
                request = service.Files.Create(
                    fileMetadata, stream, "*/*");
                request.Fields = "id";

                // Add handlers which will be notified on progress changes and upload completion.
                // Notification of progress changed will be invoked when the upload was started,
                // on each upload chunk, and on success or failure.
                request.ProgressChanged += Upload_ProgressChanged;
                request.ResponseReceived += Upload_ResponseReceived;

                var xx = request.Upload();
            }
            var fileResult = request.ResponseBody;
            if (fileResult!=null)
                Console.WriteLine("File ID: " + fileResult.Id);
        }

        void Upload_ProgressChanged(IUploadProgress progress)
        {
            Console.WriteLine(progress.Status + " " + progress.BytesSent);
        }

        void Upload_ResponseReceived(Google.Apis.Drive.v3.Data.File file)
        {
            Console.WriteLine(file.Name + " was uploaded successfully");
        }
    }
}
