namespace Sql2GDrive
{
    public class UploadFileResult
    {
        public bool Result { get; private set; }
        public string FileId { get; private set; }
        public string Message { get; private set; }



        private UploadFileResult() {}

        public UploadFileResult(bool result, string fileId, string message)
        {
            Result = result;
            FileId = fileId;
            Message = message;
        }
    }
}
