using System;

namespace Sql2GoogleDrive;

public class WorkerLogEvent : EventArgs
{
    public string Log { get; private set; }
    public WorkerLogEvent(string log)
    {
        Log = log;
    }
}
