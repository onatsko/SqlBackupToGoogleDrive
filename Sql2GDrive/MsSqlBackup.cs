using System.Data.SqlClient;
using System.IO;

namespace Sql2GDrive
{
    public class MsSqlBackup
    {
        public delegate void BackupDatabaseProgressHandler(string status, int backupPercent);
        public event BackupDatabaseProgressHandler BackupDatabaseProgressChanged;

        public void BackupDatabase(SqlConnection con, string databaseName, string backupName, string backupDescription, string backupFilename, bool compressBackup)
        {
            con.FireInfoMessageEventOnUserErrors = true;
            con.InfoMessage += OnInfoMessage;
            con.Open();
            var sql = "backup database {0} to disk = {1} with description = {2}, name = {3}, stats = 1 " +
                      (compressBackup ? ", COMPRESSION" : "") + ";";
            using (var cmd = new SqlCommand(string.Format(
                sql,
                QuoteIdentifier(databaseName),
                QuoteString(backupFilename),
                QuoteString(backupDescription),
                QuoteString(backupName)), con))
            {
                cmd.CommandTimeout = 0;
                cmd.ExecuteNonQuery();
            }
            con.Close();
            con.InfoMessage -= OnInfoMessage;
            con.FireInfoMessageEventOnUserErrors = false;
        }

        private void OnInfoMessage(object sender, SqlInfoMessageEventArgs e)
        {
            foreach (SqlError info in e.Errors)
            {
                if (info.Class > 10)
                {
                    // TODO: treat this as a genuine error
                }
                else
                {
                    Backup_ProgressChanged(info.Message);
                }
            }
        }

        private void Backup_ProgressChanged(string message)
        {
            var percentString = message.Split(" ")[0].Trim();
            if (int.TryParse(percentString, out int percent))
            {
                BackupDatabaseProgressChanged?.Invoke(message, percent);
            }
        }

        private string QuoteIdentifier(string name)
        {
            return "[" + name.Replace("]", "]]") + "]";
        }

        private string QuoteString(string text)
        {
            return "'" + text.Replace("'", "''") + "'";
        }

    }
}
