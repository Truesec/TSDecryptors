using CristiPotlog.Controls;
using System.Windows.Forms;
using Truesec.Decryptors.Interfaces;

namespace Truesec.Decryptors.Pages
{
    public partial class ProgressPage : WizardPage, IWizardPage, IDecryptorCallback
    {
        private double progression = 0;

        public ProgressPage(IDecryptorModel model, ILogger logger, IContainer container) : base(model, logger)
        {
            InitializeComponent();
            this.Style = WizardPageStyle.Standard;
            this.Title = "Decrypting...";

            if (!container.IsRegistered<IDecryptorCallback>()) {
                container.Register<IDecryptorCallback>(this);
            }
        }

        public void SetState(bool state)
        {

        }

        public override async void Activated()
        {
            pbMinProgress.Maximum = 1000;
            pbMidProgress.Maximum = 1000;
            pbMaxProgress.Maximum = 1000;
            await Model.RunDecryptor();
        }

        public void StatusProgression(DecryptorStatus status)
        {
            if (this.InvokeRequired)
            {
                this.Invoke((MethodInvoker)delegate
                {
                    this.StatusProgression(status);
                });
            } else
            {
                switch (status)
                {
                    case DecryptorStatus.Init:
                        this.Title = "Parsing files...";
                        break;
                    case DecryptorStatus.ParsingDatabase:
                        this.Title = "Parsing database...";
                        break;
                    case DecryptorStatus.DecryptingFiles:
                        this.Title = "Decrypting files...";
                        break;
                    case DecryptorStatus.Done:
                        CheckFileResults();
                        ResetProgression();
                        Model.RaiseModelUpdated();
                        MessageBox.Show("Done");
                        break;
                    default:
                        break;
                }
            }
        }

        private void CheckFileResults()
        {
            foreach (ListViewItem item in lstFiles.Items)
            {
                if(item.SubItems[1].Text != "Decrypted!")
                {
                    lstFiles.Invoke((MethodInvoker)delegate
                    {
                        item.SubItems[1].Text = "Failed!";
                    });
                }
            }
        }

        public void FileProgession(string filename, FileStatus status, string newFilename)
        {
            if (this.InvokeRequired)
            {
                this.Invoke((MethodInvoker)delegate
                {
                    this.FileProgession(filename, status, newFilename);
                });
            }
            else
            {
                if (lstFiles.Items.ContainsKey(filename))
                {
                    var item = lstFiles.Items[filename];
                    lstFiles.FocusedItem = item;
                    switch (status)
                    {
                        case FileStatus.Waiting:
                            item.SubItems[1].Text = "Waiting...";
                            break;
                        case FileStatus.Decrypting:
                            item.SubItems[1].Text = "Decrypting...";
                            break;
                        case FileStatus.Decrypted:
                            item.SubItems[1].Text = "Decrypted!";
                            item.Text = newFilename;
                            break;
                        case FileStatus.Failed:
                            item.SubItems[1].Text = "Failed!";
                            break;
                        case FileStatus.FileIsNotEncryptedMaybeOverwritten:
                            item.SubItems[1].Text = "File may have been wiped!";
                            break;
                        default:
                            break;
                    }
                }
                else
                {
                    var item = new ListViewItem();
                    item.Name = filename;
                    item.Text = filename;
                    item.ToolTipText = filename;
                    item.SubItems.Add("Waiting...");

                    lstFiles.Items.Add(item);
                }
            }
        }

        public void Progress()
        {
            progression++;
            var minProgression = (int)(progression % 1000);
            var midProgression = (int)(progression / 1000);
            var maxProgression = (int)(progression / 1000000);
            pbMaxProgress.Invoke((MethodInvoker)delegate
            {
                pbMaxProgress.Value = maxProgression;
                pbMidProgress.Value = midProgression;
                pbMinProgress.Value = minProgression;
            });
        }

        public void ResetProgression()
        {
            progression = 0;
            pbMaxProgress.Invoke((MethodInvoker)delegate
            {
                pbMaxProgress.Value = (int)progression;
                pbMidProgress.Value = (int)progression;
                pbMinProgress.Value = (int)progression;
            });
        }
    }
}
