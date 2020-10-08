using System;
using System.IO;

namespace SqlBackupToGoogleDrive
{
    public class SqlBackupToGoogleDrive
    {
        private string _connectionString;

        public SqlBackupToGoogleDrive(string connectionString)
        {
            _connectionString = connectionString;
        }

        public BackupResult CreateBackup()
        {
            return new BackupResult(false, "need to do");
        }

        public SendResult SendToGoogleDrive(string token, string file)
        {
            return new SendResult(false, "need to do");
        }





        static void Main(string[] args)
        {
            var fi = new FileInfo(@"e:\Nau\Work\PDAA\DB_Arhive\PDAA_2020-06-16_15-20.bak");

            var service = new File2GDrive();
            service.CreateFolder("!test!");
            //service.UploadFile(fi);

            Console.ReadLine();
        }

    }
}
