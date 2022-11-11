using CristiPotlog.Controls;
using System;
using System.IO;
using System.Windows.Forms;
using Truesec.Decryptors.Implementations;
using Truesec.Decryptors.Interfaces;

namespace Truesec.Decryptors.Pages
{
    public partial class SettingsPage : WizardPage, IWizardPage, IFileDownloaderCallback
    {
        private readonly IFileDownloader fileDownloader;
        private readonly IDecryptorChecker decryptorChecker;
        private readonly IContainer container;

        public SettingsPage(IDecryptorModel model, ILogger logger, IDecryptorChecker decryptorChecker, IContainer ioc) : base(model, logger)
        {
            InitializeComponent();
            this.Style = WizardPageStyle.Standard;
            this.Title = "Settings";

            this.rbSolidbit.CheckedChanged += delegate {
                this.tbDatabaseFile.Enabled = !this.rbSolidbit.Checked;
                this.btnSelectDatabase.Enabled = !this.rbSolidbit.Checked;
                this.btnDownload.Enabled = !this.rbSolidbit.Checked;
            };
            
            container = ioc;
            if (!container.IsRegistered<IFileDownloaderCallback>())
            {
                container.Register<IFileDownloaderCallback>(this);
            }
            this.fileDownloader = ioc.Resolve<IFileDownloader>();
            this.decryptorChecker = decryptorChecker;
        }

        private void btnSelectDatabase_Click(object sender, EventArgs e)
        {
            if (dlgSelectDatabase.ShowDialog() == DialogResult.OK)
            {
                tbDatabaseFile.Text = dlgSelectDatabase.FileName;
            }
        }

        private void tbDatabaseFile_TextChanged(object sender, EventArgs e)
        {
            Model.Database = tbDatabaseFile.Text;
            Model.RaiseModelUpdated();
        }

        public override void Activated()
        {
            tbDatabaseFile.Text = Model.Database;
            cbRecursive.Checked = Model.UseRecursion;
            tbExtension.Text = Model.Extension;
            if(Model.DecryptorName == Constants.CHAOS_DECRYPTOR)
            {
                rbChaos.Checked = true;
            } 
            else if(Model.DecryptorName == Constants.ONYX2_DECRYPTOR)
            {
                rbOnyx2.Checked = true;
            }
            else 
            {
                rbSolidbit.Checked = true;
            }
        }

        public override void Deactivated()
        {
            var extension = tbExtension.Text.Trim();
            Model.Extension = extension;
            Model.Database = tbDatabaseFile.Text;
            Model.UseRecursion = cbRecursive.Checked;
            if(rbChaos.Checked)
            {
                Model.DecryptorName = Constants.CHAOS_DECRYPTOR;
            } 
            else if(rbOnyx2.Checked)
            {
                Model.DecryptorName = Constants.ONYX2_DECRYPTOR;
            } 
            else
            {
                Model.DecryptorName = Constants.SOLIDBIT_DECRYPTOR;
            }
        }

        private void btnIdentify_Click(object sender, EventArgs e)
        {
            if (dlgIdentify.ShowDialog() == DialogResult.OK)
            {
                var malware = decryptorChecker.CheckFile(dlgIdentify.FileName);
                switch(malware){
                    case Malware.Chaos:
                        rbChaos.Checked = true;
                        MessageBox.Show($"This file looks to have been encrypted with the {Constants.CHAOS_DECRYPTOR} malware.", "Malware identified!");
                        break;
                    case Malware.Onxyv2:
                        rbOnyx2.Checked = true;
                        MessageBox.Show($"This file looks to have been encrypted with the {Constants.ONYX2_DECRYPTOR} malware.", "Malware identified!");
                        break;
                    default:
                        rbSolidbit.Checked = true;
                        MessageBox.Show($"This file doesn't look to have been encrypted with neither the {Constants.CHAOS_DECRYPTOR} nor the {Constants.ONYX2_DECRYPTOR} malware, you could try to leverage the {Constants.SOLIDBIT_DECRYPTOR} decryptor.", "Failed to identify malware!");
                        break;
                }
            }
        }



        private void btnDownload_Click(object sender, EventArgs e)
        {
            var filename = Constants.CHAOS_DB_FILENAME;
            if(rbOnyx2.Checked)
            {
                filename = Constants.ONYX2_DB_FILENAME;
            }
            dlgSaveFile.FileName = filename;
            dlgSaveFile.DefaultExt = Constants.DATABASE_DEFAULT_EXTENSION;
            dlgSaveFile.Filter = Constants.DATABASE_DEFAULT_FILTER;
            dlgSaveFile.Title = Constants.SAVE_DATABASE_DIALOG_TITLE;

            if (dlgSaveFile.ShowDialog() == DialogResult.OK)
            {
                filename = dlgSaveFile.FileName;
                tbDatabaseFile.Text = filename;
                var url = Constants.CHAOS_DB_DOWNLOAD_URL + Constants.CHAOS_DB_FILENAME;
                if (rbOnyx2.Checked)
                {
                    url = Constants.ONYX2_DB_DOWNLOAD_URL + Constants.ONYX2_DB_FILENAME;
                }            
                fileDownloader.DownloadFile(url, filename);
            }
        }

        public void DownloadProgession(long bytesReceived, long totalBytes)
        {
            if (this.InvokeRequired)
            {
                this.BeginInvoke((MethodInvoker)delegate
                {
                    this.DownloadProgession(bytesReceived, totalBytes);
                });
            }
            else
            {
                double totalMinimalPartialBytes = (double)totalBytes / 10000d;
                double minimalPartialPercentage = (bytesReceived % totalMinimalPartialBytes) / totalMinimalPartialBytes * 100;

                double totalPartialBytes = (double)totalBytes / 100;
                double partialPercentage = (bytesReceived % totalPartialBytes) / totalPartialBytes * 100;

                var progress = (double)bytesReceived / (double)totalBytes * 100.0;
                lblProgress.Text = String.Format("{0:0.##}%", Math.Truncate(progress * 100) / 100);
                pbMinimalPartialDownload.Value = (int)Math.Truncate(minimalPartialPercentage);
                pbPartialDownload.Value = (int)Math.Truncate(partialPercentage);
                pbDownload.Value = (int)Math.Truncate(progress);
            }
        }

        public void ResetProgression()
        {
            if (this.InvokeRequired) {
                this.BeginInvoke((MethodInvoker)ResetProgression);
            }
            else {
                pbDownload.Style = ProgressBarStyle.Blocks;
                pbDownload.Value = 0;
                pbMinimalPartialDownload.Value = 0;
                pbPartialDownload.Value = 0;
                lblProgress.Text = string.Empty;
            }
        }

        public void DownloadCompleted(DownloadStatus status)
        {
            if (this.InvokeRequired)
            {
                this.BeginInvoke((MethodInvoker)delegate
                {
                    this.DownloadCompleted(status);
                });
            }
            else
            {
                ResetProgression();
                if(status == DownloadStatus.Completed)
                {
                    var checksum = rbChaos.Checked ? Constants.CHAOS_DB_CHECKSUM : Constants.ONYX2_DB_CHECKSUM;
                    ValidateFileAsync(Model.Database, checksum);
                } else {
                    logger?.Log("Failed to download the database for " + (rbChaos.Checked ? Constants.CHAOS_DB_FILENAME : Constants.ONYX2_DB_FILENAME));
                    MessageBox.Show("File failed to download, try again", "Error!");
                }
            }
        }

        private async void ValidateFileAsync(string filename, string checksum)
        {
            try {
                pbDownload.Style = ProgressBarStyle.Marquee;
                var validator = container.Resolve<IValidator>();
                logger?.Log("Downloaded file being validated!");
                bool result = await validator.ValidateFileAsync(filename, checksum);
                if (result)
                {
                    logger?.Log("File downloaded and validated successfully");
                    MessageBox.Show("File has been downloaded and checksum validated!", "Success!!!");
                }
                else
                {
                    logger?.Log("File downloaded successfully but checksum is invalid!");
                    MessageBox.Show("Download succeeded, but checksum is invalid, please try again! If this error continues please contact Truesec!", "Invalid checksum"); ;
                }
            } catch(Exception ex)
            {
                logger?.Log("Something failed while validating checksum: " + ex.Message);
                MessageBox.Show("Download succeeded, but something failed while validating checksum. If this error continues please contact Truesec!", "Error!"); ;
            }
            finally
            {
                ResetProgression();
            }
        }
    }
}
