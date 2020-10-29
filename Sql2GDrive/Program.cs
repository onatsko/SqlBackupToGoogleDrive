using Sql2GoogleDrive;
using System;
using System.IO;
using System.Windows.Forms;

namespace Sql2GDrive
{
    static class Program
    {
        /// <summary>
        ///  The main entry point for the application.
        /// </summary>
        [STAThread]
        static void Main(string[] args)
        {
            Application.SetHighDpiMode(HighDpiMode.SystemAware);
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
            Application.Run(new FrmMain());
        }

        public const string AppName = "Sql2GoogleDrive";

    }
}
