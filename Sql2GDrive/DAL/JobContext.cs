using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
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
        [Key] public int Id { get; set; }
        [Required] public string Name { get; set; } = "New job";
        public int ConnectionId { get; set; }
        public Connection Connection { get; set; } = new Connection();
        public int ScheduleId { get; set; }
        public Schedule Schedule { get; set; } = new Schedule();
        public bool IsSaveToLocalFolder { get; set; } = false;
        public string FolderPath { get; set; } = "";
        public bool IsDel { get; set; } = false;
    }

    public class Connection
    {
        [Key]
        public int Id { get; set; }
        [Required]
        public string Server { get; set; }
        [Required]
        public string Database { get; set; }

        public bool AuthSql { get; set; }
        public bool AuthWindows { get; set; }
        public string Login { get; set; }
        public string Password { get; set; }

        public Connection()
        {
            Server = "localhost";
            Database = "your_database";
        }
    }

    public class Schedule
    {
        [Key]
        public int Id { get; set; }
        public int DayToRunId { get; set; }
        public DayToRun DayToRun { get; set; }
        public List<TimeToRun> TimeToRun { get; set; }

        public Schedule()
        {
            DayToRun = new DayToRun();
            TimeToRun = new List<TimeToRun>();
        }
    }

    public class TimeToRun
    {
        [Key]
        public int Id { get; set; }
        [Required]
        public DateTime Time { get; set; }
    }

    public class DayToRun
    {
        [Key]
        public int Id { get; set; }
        public int RunMode { get; set; }//0 - none, 1 - autorun
        public bool Monday { get; set; }
        public bool Tuesday { get; set; }
        public bool Wednesday { get; set; }
        public bool Thursday { get; set; }
        public bool Friday { get; set; }
        public bool Saturday { get; set; }
        public bool Sunday { get; set; }
    }
}
