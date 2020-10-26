using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.EntityFrameworkCore;

namespace Sql2GoogleDrive.DAL.TaskContext
{
    public class JobContext : DbContext
    {
        public DbSet<Job> Jobs { get; set; }
        public DbSet<Connection> Connections { get; set; }
        public DbSet<Schedule> Schedules { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder options)
            => options.UseSqlite("Data Source=sql2gdrive.db");
    }

    public class Job
    {
        public int JobId { get; set; }
        public Connection Connection { get; set; }
        public Schedule Schedule { get; set; }
    }

    public class Connection
    {
        public int ConnectionId { get; set; }
        public string Server { get; set; }
        public string Database { get; set; }

        public bool AuthSql { get; set; }
        public bool AuthWindows { get; set; }
        public string Login { get; set; }
        public string Password { get; set; }
    }

    public class Schedule
    {
        public int ScheduleId { get; set; }
        public List<TimeToRun> TimeToRun { get; set; } = new List<TimeToRun>();
        public List<DayToRun> RunAtDays { get; set; } = new List<DayToRun>();
    }

    public class TimeToRun
    {
        public int TimeToRunId { get; set; }
        public DateTime Time { get; set; }
    }

    public class DayToRun
    {
        public int DayToRunId { get; set; }
        public bool Monday { get; set; }
        public bool Tuesday { get; set; }
        public bool Wednesday { get; set; }
        public bool Thursday { get; set; }
        public bool Friday { get; set; }
        public bool Saturday { get; set; }
        public bool Sunday { get; set; }
    }
}
