using CristiPotlog.Controls;
using System;
using System.Text.RegularExpressions;
using System.Windows.Forms;
using Truesec.Decryptors.Interfaces;

namespace Truesec.Decryptors.Pages
{
    public partial class FoldersPage : WizardPage, IWizardPage
    {
        public FoldersPage(IDecryptorModel model, ILogger logger) : base(model, logger)
        {
            InitializeComponent();
            this.Style = WizardPageStyle.Standard;
            this.Title = "Select files to decrypt";
        }

        private void btnClear_Click(object sender, EventArgs e)
        {
            Model.Folders.Clear();
            lstFiles.Items.Clear();
            Model.RaiseModelUpdated();
        }

        private void btnAddFolders_Click(object sender, EventArgs e)
        {
            if (fbdFolders.ShowDialog() == DialogResult.OK)
            {
                try
                {
                    bool shouldContinue = true;

                    // Perform check to see if they selected a root drive
                    if (Regex.IsMatch(fbdFolders.SelectedPath, @"^[A-Za-z]:\\$", RegexOptions.Multiline))
                    {
                        if (MessageBox.Show("You are attempting to search an entire drive, this operation could take a long time and use more memory.  If Recursion is enabled it can take even longer.  Do you want to continue?", "Long running operation", MessageBoxButtons.YesNoCancel) != DialogResult.Yes)
                        {
                            shouldContinue = false;
                        }
                    }

                    // If user was prompted and selected No/Cancel then this won't run
                    if (shouldContinue)
                    {
                        Model.AddFolder(fbdFolders.SelectedPath);
                        AddFolderToListView(fbdFolders.SelectedPath);
                    }
                }
                catch (Exception ex)
                {
                    logger?.Log("Failed to add folders: " + ex.Message);
                    MessageBox.Show("Something failed!", "Error!");
                }
            }
        }

        private void AddFolderToListView(string folder)
        {
            ListViewItem item = new ListViewItem();
            item.Text = folder;
            item.ToolTipText = folder;

            lstFiles.Items.Add(item);
            Model.RaiseModelUpdated();
        }
    }
}
