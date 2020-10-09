using Google.Apis.Auth.OAuth2;
using Google.Apis.Drive.v3;
using Google.Apis.Services;
using Google.Apis.Upload;
using Google.Apis.Util.Store;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading;
using File = Google.Apis.Drive.v3.Data.File;

namespace File2GDrive
{
    public class File2GDrive
    {
        private const string FolderMimeType = "application/vnd.google-apps.folder";


        private readonly string[] scopes = { DriveService.Scope.DriveFile };
        private readonly string applicationName = "File2GDrive";
        private readonly DriveService service;

        public File2GDrive()
        {
            UserCredential credential;

            using (var stream =
                new FileStream("Credentials/GDriveCredentials.json", FileMode.Open, FileAccess.Read))
            {
                // The file token.json stores the user's access and refresh tokens, and is created
                // automatically when the authorization flow completes for the first time.
                string credPath = "token.json";
                credential = GoogleWebAuthorizationBroker.AuthorizeAsync(
                    GoogleClientSecrets.Load(stream).Secrets,
                    scopes,
                    "user",
                    CancellationToken.None,
                    new FileDataStore(credPath, true)).Result;
                Console.WriteLine("Credential file saved to: " + credPath);
            }

            // Create Drive API service.
            service = new DriveService(new BaseClientService.Initializer()
            {
                HttpClientInitializer = credential,
                ApplicationName = applicationName,
            });
        }

        public List<File> GetFiles()
        {
            var result = new List<File>();

            string pageToken = null;
            while (true)
            {
                FilesResource.ListRequest listRequest = service.Files.List();
                listRequest.PageSize = 10;
                listRequest.PageToken = pageToken;
                listRequest.Fields = "nextPageToken, files(id, name, mimeType, size)";

                var fileList = listRequest.Execute();

                // List files.
                IList<File> folders = fileList.Files;
                if (folders != null && folders.Count > 0)
                {
                    result.AddRange(folders.Where(x => !x.MimeType.Equals(FolderMimeType, StringComparison.InvariantCultureIgnoreCase)));
                }

                pageToken = fileList.NextPageToken;
                if (pageToken == null)
                    break;
            }

            return result;
        }

        public List<File> GetFolders()
        {
            var result = new List<File>();

            string pageToken = null;
            while (true)
            {
                FilesResource.ListRequest listRequest = service.Files.List();
                listRequest.PageSize = 10;
                listRequest.PageToken = pageToken;
                listRequest.Fields = "nextPageToken, files(id, name, mimeType, size)";

                var fileList = listRequest.Execute();

                // List files.
                IList<File> folders = fileList.Files;
                if (folders != null && folders.Count > 0)
                {
                    result.AddRange(folders.Where(x => x.MimeType.Equals(FolderMimeType, StringComparison.InvariantCultureIgnoreCase)));
                }

                pageToken = fileList.NextPageToken;
                if (pageToken == null)
                    break;
            }

            return result;
        }

        /// <summary>
        /// If folder with param name exists, return FolderId of exists folder.
        /// Or create new folder
        /// </summary>
        /// <param name="folderName">Folder name</param>
        /// <returns>Folder.Id</returns>
        public string CreateOrGetFolder(string folderName)
        {
            var existingFolders = GetFolders();
            foreach (var folder in existingFolders)
            {
                if (folder.Name == folderName)
                    return folder.Id;
            }

            var body = new File
            {
                Name = folderName, 
                MimeType = FolderMimeType
            };

            var createdFolder = service
                .Files
                .Create(body)
                .Execute();

            return createdFolder.Id;
        }

        /// <summary>
        /// Forced create new folder
        /// </summary>
        /// <param name="folderName">Folder name</param>
        /// <returns>FolderId</returns>
        public string CreateForceFolder(string folderName)
        {
            var body = new File
            {
                Name = folderName,
                MimeType = "application/vnd.google-apps.folder"
            };

            var folder = service
                .Files
                .Create(body)
                .Execute();

            return folder.Id;
        }

        private bool IsFolderIdExists(string folderId)
        {
            var existingFolders = GetFolders();

            if (existingFolders.Select(x => x.Id).Contains(folderId))
                return true;

            return false;
        }

        public UploadFileResult UploadFile(string file, string folderId = null)
        {
            var fi = new FileInfo(file);
            var fileMetadata = new File()
            {
                Name = fi.Name,
                MimeType = "application/octet-stream"
            };

            //if folderId is specified, try to find folder and set
            if (!string.IsNullOrEmpty(folderId))
            {
                if (!IsFolderIdExists(folderId))
                {
                    //throw new ArgumentException($"Folder with Id '{folderId}' is absent");
                    return new UploadFileResult(false, "", $"Folder with Id '{folderId}' is absent");
                }

                fileMetadata.Parents = new List<string>() {folderId};
            }

            using (var stream = new FileStream(fi.FullName, FileMode.Open))
            {
                var request = service.Files.Create(fileMetadata, stream, "*/*");
                request.Fields = "id";

                // Add handlers which will be notified on progress changes and upload completion.
                // Notification of progress changed will be invoked when the upload was started,
                // on each upload chunk, and on success or failure.
                request.ProgressChanged += Upload_ProgressChanged;
                request.ResponseReceived += Upload_ResponseReceived;

                var progress = request.Upload();
                if (progress.Status == UploadStatus.Completed)
                {
                    var fileResult = request.ResponseBody;
                    //if (fileResult != null)
                    //    Console.WriteLine("File ID: " + fileResult.Id);

                    return new UploadFileResult(true, fileResult.Id, "");
                }
                else
                {
                    return new UploadFileResult(false, "", $"Status: {progress.Status}, Exception: {progress.Exception}");
                }
            }
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
