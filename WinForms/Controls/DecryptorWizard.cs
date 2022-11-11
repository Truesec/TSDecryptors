using CristiPotlog.Controls;
using System;
using System.Collections.Generic;
using System.Reflection;
using System.Windows.Forms;
using Truesec.Decryptors.Ioc;
using Truesec.Decryptors.Models;
using Truesec.Decryptors.Pages;
using Truesec.Decryptors.Interfaces;

namespace Truesec.Decryptors.Controls
{
    public partial class DecryptorWizard : Form
    {
        private IDecryptorModel model;
        private readonly IContainer container;

        public DecryptorWizard(IContainer diContainer)
        {
            base.AutoScaleMode = AutoScaleMode.Dpi;
            InitializeComponent();

            container = diContainer;
            model = container.Resolve<IDecryptorModel>();
            model.ModelUpdated += OnModelUpdated;

            InitializePages();
        }

        private void InitializePages()
        {
            var pages = new List<IWizardPage>();
            pages.Add(container.Resolve<IWizardPage>(Constants.WELCOME_PAGE));
            if (!model.LicenseAccepted)
            {
                pages.Add(container.Resolve<IWizardPage>(Constants.LICENSE_PAGE));
            }

            pages.Add(container.Resolve<IWizardPage>(Constants.SETTINGS_PAGE));
            pages.Add(container.Resolve<IWizardPage>(Constants.FOLDERS_PAGE));
            pages.Add(container.Resolve<IWizardPage>(Constants.PROGRESS_PAGE));

            this.wizardPanel.Pages.AddRange(pages.ToArray());
        }

        private void wizardPanel_AfterSwitchPages(object sender, Wizard.AfterSwitchPagesEventArgs e)
        {
            // get wizard page to be displayed
            var newPage = this.wizardPanel.Pages[e.NewIndex];

            // check if license page
            if (newPage is LicensePage)
            {
                // sync next button's state with check box
                this.wizardPanel.NextEnabled = model.LicenseAccepted;
            }
            else if (newPage is FoldersPage)
            {
                this.wizardPanel.NextEnabled = model.IsValidForDecryption;
            }
            // check if progress page
            else if (newPage is ProgressPage)
            {
                // start the sample task
                this.wizardPanel.BackEnabled = false;
                this.wizardPanel.NextEnabled = false;
            }
        }
        private void OnModelUpdated(object sender, EventArgs args)
        {
            var currentPage = this.wizardPanel.SelectedPage;
            if (currentPage is LicensePage)
            {
                this.wizardPanel.NextEnabled = model.LicenseAccepted;
            } else if(currentPage is FoldersPage)
            {
                this.wizardPanel.NextEnabled = model.IsValidForDecryption;
            } else if(currentPage is ProgressPage)
            {
                this.wizardPanel.CancelText = "OK";
                this.wizardPanel.CancelEnabled = true;
            }
        }

        private void wizardPanel_Help(object sender, EventArgs e)
        {
            MessageBox.Show("If you need assistance or are interested in learning more, " +
                            "please visit https://www.truesec.com!\n\nVersion: " +
                            Assembly.GetEntryAssembly().GetName().Version,
                            this.Text,
                            MessageBoxButtons.OK,
                            MessageBoxIcon.Information);
        }

        private void wizardPanel_Cancel(object sender, System.ComponentModel.CancelEventArgs e)
        {
            if(wizardPanel.CancelText != "OK")
            {
                // ask user to confirm
                if (MessageBox.Show("Are you sure you want to exit the wizard?",
                                    this.Text,
                                    MessageBoxButtons.YesNo,
                                    MessageBoxIcon.Question) != DialogResult.Yes)
                {
                    // cancel closing
                    e.Cancel = true;
                }
            }
        }
    }
}
