using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using Microsoft.EntityFrameworkCore;

namespace Sql2GoogleDrive.DAL
{
    public class JobContext : DbContext
    {
        public DbSet<Job> Jobs { get; set; }
        public DbSet<Connection> Connections { get; set; }
        public DbSet<Schedule> Schedules { get; set; }
        public DbSet<TimeToRun> TimeToRuns { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder options)
            => options.UseSqlite($"Data Source=sql2gdrive.db");
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

        protected bool Equals(Job other)
        {
            return Id == other.Id 
                   && Name == other.Name 
                   && ConnectionId == other.ConnectionId 
                   && Equals(Connection, other.Connection) 
                   && ScheduleId == other.ScheduleId 
                   && Equals(Schedule, other.Schedule) 
                   && IsSaveToLocalFolder == other.IsSaveToLocalFolder 
                   && FolderPath == other.FolderPath 
                   && IsDel == other.IsDel;
        }

        public override bool Equals(object obj)
        {
            if (ReferenceEquals(null, obj)) return false;
            if (ReferenceEquals(this, obj)) return true;
            if (obj.GetType() != this.GetType()) return false;
            return Equals((Job) obj);
        }

        public override int GetHashCode()
        {
            var hashCode = new HashCode();
            hashCode.Add(Id);
            hashCode.Add(Name);
            hashCode.Add(ConnectionId);
            hashCode.Add(Connection);
            hashCode.Add(ScheduleId);
            hashCode.Add(Schedule);
            hashCode.Add(IsSaveToLocalFolder);
            hashCode.Add(FolderPath);
            hashCode.Add(IsDel);
            return hashCode.ToHashCode();
        }
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

        protected bool Equals(Connection other)
        {
            return Id == other.Id 
                   && Server == other.Server 
                   && Database == other.Database 
                   && AuthSql == other.AuthSql 
                   && AuthWindows == other.AuthWindows 
                   && Login == other.Login 
                   && Password == other.Password;
        }

        public override bool Equals(object obj)
        {
            if (ReferenceEquals(null, obj)) return false;
            if (ReferenceEquals(this, obj)) return true;
            if (obj.GetType() != this.GetType()) return false;
            return Equals((Connection) obj);
        }

        public override int GetHashCode()
        {
            return HashCode.Combine(Id, Server, Database, AuthSql, AuthWindows, Login, Password);
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

        protected bool Equals(Schedule other)
        {
            return Id == other.Id && DayToRunId == other.DayToRunId && Equals(DayToRun, other.DayToRun) && EachListElementsEquals(TimeToRun, other.TimeToRun);
        }

        public override bool Equals(object obj)
        {
            if (ReferenceEquals(null, obj)) return false;
            if (ReferenceEquals(this, obj)) return true;
            if (obj.GetType() != this.GetType()) return false;
            return Equals((Schedule) obj);
        }

        public override int GetHashCode()
        {
            return HashCode.Combine(Id, DayToRunId, DayToRun, TimeToRun);
        }


        private bool EachListElementsEquals(List<TimeToRun> x, List<TimeToRun> y)
        {
            if (x.Count != y.Count)
                return false;

            for (int i = 0; i < x.Count; i++)
            {
                if (x[i].Id != y[i].Id || x[i].Time != y[i].Time)
                    return false;
            }

            return true;
        }
    }

    public class TimeToRun
    {
        [Key]
        public int Id { get; set; }
        [Required]
        public int ScheduleId { get; set; }
        [Required]
        public DateTime Time { get; set; }

        public TimeToRun(int scheduleId)
        {
            ScheduleId = scheduleId;
        }

        public TimeToRun(int scheduleId, DateTime time): this(scheduleId)
        {
            Time = time;
        }

        protected bool Equals(TimeToRun other)
        {
            return Id == other.Id && ScheduleId == other.ScheduleId && Time.Equals(other.Time);
        }

        public override bool Equals(object obj)
        {
            if (ReferenceEquals(null, obj)) return false;
            if (ReferenceEquals(this, obj)) return true;
            if (obj.GetType() != this.GetType()) return false;
            return Equals((TimeToRun) obj);
        }

        public override int GetHashCode()
        {
            return HashCode.Combine(Id, ScheduleId, Time);
        }
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

        protected bool Equals(DayToRun other)
        {
            return Id == other.Id && RunMode == other.RunMode && Monday == other.Monday && Tuesday == other.Tuesday && Wednesday == other.Wednesday && Thursday == other.Thursday && Friday == other.Friday && Saturday == other.Saturday && Sunday == other.Sunday;
        }

        public override bool Equals(object obj)
        {
            if (ReferenceEquals(null, obj)) return false;
            if (ReferenceEquals(this, obj)) return true;
            if (obj.GetType() != this.GetType()) return false;
            return Equals((DayToRun) obj);
        }

        public override int GetHashCode()
        {
            var hashCode = new HashCode();
            hashCode.Add(Id);
            hashCode.Add(RunMode);
            hashCode.Add(Monday);
            hashCode.Add(Tuesday);
            hashCode.Add(Wednesday);
            hashCode.Add(Thursday);
            hashCode.Add(Friday);
            hashCode.Add(Saturday);
            hashCode.Add(Sunday);
            return hashCode.ToHashCode();
        }
    }
}
