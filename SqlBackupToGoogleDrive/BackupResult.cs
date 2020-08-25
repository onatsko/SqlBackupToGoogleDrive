namespace SqlBackupToGoogleDrive
{
    public class BackupResult
    {
        public bool Result { get; private set; } 
        public string Message { get; private set; }


        private BackupResult() {}

        public BackupResult(bool result, string message)
        {
            Result = result;
            Message = message;
        }
    }
}
