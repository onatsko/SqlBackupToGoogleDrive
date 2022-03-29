using System;

namespace Sql2GoogleDrive;

public class WorkerProgressChangedArgs : EventArgs
{
    public string Progress { get; private set; }
    public int ProgressValue { get; private set; }
    public WorkerProgressChangedArgs(string progress, int value)
    {
        Progress = progress;
        ProgressValue = value;
    }
}
