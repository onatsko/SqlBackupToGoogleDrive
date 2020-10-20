using Google.Apis.Auth.OAuth2;
using Google.Apis.Drive.v3;
using Google.Apis.Services;
using Google.Apis.Util.Store;
using System;
using System.Data;
using System.Data.SqlClient;
using System.IO;
using System.Threading;
using System.Windows.Forms;

namespace Sql2GDrive
{
    public partial class FrmMain : Form
    {

        public FrmMain()
        {
            InitializeComponent();
        }

        private string GetGoogleAccountCredentialsFolder()
        {
            var fullFolderPath = new FileDataStore(Program.AppName, false);

            var credPath = fullFolderPath.FolderPath;
            return credPath;
        }

        private bool IsGoogleAccountConnected()
        {
            var credPath = GetGoogleAccountCredentialsFolder();

            if (Directory.Exists(credPath) && Directory.GetFiles(credPath).Length>0)
            {
                return true;
            }

            return false;
        }

        private void btnGoogleAuthorize_Click(object sender, EventArgs e)
        {
            var credPath = GetGoogleAccountCredentialsFolder();
            if (IsGoogleAccountConnected())
            {
                try
                {
                    Directory.Delete(credPath, true);
                }
                catch (Exception exception)
                {
                    MessageBox.Show($"Ошибка удаления каталога '{credPath}'");
                }
            }


            string[] Scopes = { DriveService.Scope.DriveFile };
            UserCredential credential;

            using (var stream =
                new FileStream("GDriveCredentials.json", FileMode.Open, FileAccess.Read))
            {
                credential = GoogleWebAuthorizationBroker.AuthorizeAsync(
                    GoogleClientSecrets.Load(stream).Secrets, Scopes, "user", CancellationToken.None, new FileDataStore(credPath, true)).Result;
                Console.WriteLine("Credential file saved to: " + credPath);
            }

            // Create Drive API service.
            var service = new DriveService(new BaseClientService.Initializer()
            {
                HttpClientInitializer = credential,
                ApplicationName = Program.AppName,
            });

        }

        private void btnConTest_Click(object sender, EventArgs e)
        {
            var cnn = new SqlConnection
            {
                ConnectionString =
                    $"Data Source={txtConServer.Text};Initial Catalog={txtConDatabase.Text};User ID={txtConLogin.Text};Password={txtConPassword.Text};Timeout=15"
            };

            try
            {
                cnn.Open();
                if (cnn.State == ConnectionState.Open)
                {
                    MessageBox.Show("Test OK");
                }
                else
                {
                    MessageBox.Show($"Test Failed: {cnn.State.ToString()}");
                }

                cnn.Close();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }

        }

        private void SetControlAccess()
        {
            if (rdbConAuthSql.Checked)
            {
                txtConLogin.Enabled = true;
                txtConPassword.Enabled = true;
            }
            else
            {
                txtConLogin.Enabled = false;
                txtConPassword.Enabled = false;
            }
        }

        private void rdbConAuthSql_CheckedChanged(object sender, EventArgs e)
        {
            SetControlAccess();
        }

        private void rdbConAuthWin_CheckedChanged(object sender, EventArgs e)
        {
            SetControlAccess();
        }

        private void btnFIleSelect_Click(object sender, EventArgs e)
        {
            if (!string.IsNullOrEmpty(txtFolder.Text) && Directory.Exists(txtFolder.Text))
            {
                folderBrowserDialog1.SelectedPath = txtFolder.Text;
            }

            folderBrowserDialog1.ShowNewFolderButton = true;
            var result = folderBrowserDialog1.ShowDialog();
            if (result == DialogResult.OK)
            {
                txtFolder.Text = folderBrowserDialog1.SelectedPath;
            }
        }

        private void btnBackupAndUpload_Click(object sender, EventArgs e)
        {
            if (!IsGoogleAccountConnected())
            {
                MessageBox.Show("Сначала подключите аккаунт Google для загрузки файла!", Program.AppName,
                    MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            var frmDo = new FrmBackupAndUpload(
                txtConServer.Text,
                txtConDatabase.Text,
                rdbConAuthWin.Checked,
                (rdbConAuthSql.Checked ? txtConLogin.Text : ""),
                (rdbConAuthSql.Checked ? txtConPassword.Text : ""),
                true,
                true,
                true,
                txtConDatabase.Text.ToUpper(),
                !chbSaveToFolder.Checked,
                chbSaveToFolder.Checked ? txtFolder.Text : "",
                true
                );
            frmDo.Show();
            frmDo.Run();

        }

        private void btnBackupOnly_Click(object sender, EventArgs e)
        {
            var frmDo = new FrmBackupAndUpload(
                txtConServer.Text,
                txtConDatabase.Text,
                rdbConAuthWin.Checked,
                (rdbConAuthSql.Checked ? txtConLogin.Text : ""),
                (rdbConAuthSql.Checked ? txtConPassword.Text : ""),
                true,
                false,
                false,
                "",
                false,
                chbSaveToFolder.Checked ? txtFolder.Text : "",
                true
            );
            frmDo.Show();
            frmDo.Run();
        }

        private void chbSaveToFolder_CheckedChanged(object sender, EventArgs e)
        {
            if (chbSaveToFolder.Checked)
            {
                txtFolder.Enabled = true;
                btnFolderSelect.Enabled = true;
            }
            else
            {
                txtFolder.Enabled = false;
                btnFolderSelect.Enabled = false;
            }
        }
    }


}
