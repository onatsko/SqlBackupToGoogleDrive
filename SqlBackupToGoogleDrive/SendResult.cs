namespace SqlBackupToGoogleDrive
{
    public class SendResult
    {
        public bool Result { get; private set; } 
        public string Message { get; private set; }


        private SendResult() {}

        public SendResult(bool result, string message)
        {
            Result = result;
            Message = message;
        }
    }
}
