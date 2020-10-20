using System;
using System.IO;

namespace File2GDrive
{
    class Program
    {
        static void Main(string[] args)
        {
            //var file = @"e:\Nau\Work\PDAA\DB_Arhive\PDAA_2020-06-16_15-20.bak";
            var file = @"e:\tmp\foxminded\Numbers_Minus.txt";
            var folder = "!test!";

            var service = new File2GDrive();

            var folderId = service.CreateOrGetFolder(folder);
            service.UploadFileProgressChanged += DisplayUploadFileProgress;
            service.UploadFileCompleted += DisplayUploadFileComplete;
            var result = service.UploadFile( file, folderId);
            if (result.Result)
            {
                Console.WriteLine($"FileID: {result.FileId}");
            }
            else
            {
                Console.WriteLine($"Error: {result.Message}");
            }

            Console.ReadLine();
        }

        private static void DisplayUploadFileProgress(string status, long bytesSent, long bytesAll, int uploadingPercent)
        {
            Console.WriteLine($"{status} {bytesSent} {uploadingPercent}%");
        }

        private static void DisplayUploadFileComplete(string fileId)
        {
            Console.WriteLine($"Uploaded file Id is {fileId}");
        }
    }
}
