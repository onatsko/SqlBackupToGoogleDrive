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
    }
}
