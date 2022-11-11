using System;
using CristiPotlog.Controls;
using Truesec.Decryptors.Interfaces;

namespace Truesec.Decryptors.Pages
{
    public partial class LicensePage : WizardPage, IWizardPage
    {
        public LicensePage(IDecryptorModel model, ILogger logger) : base(model, logger)
        {
            InitializeComponent();

            this.Style = WizardPageStyle.Standard;
            this.Title = "License Agreement";
            this.Description = "Please read the following license agreement and confirm that you agree with all " +
                              "terms and conditions.";
        }

        public override void Activated()
        {
            try {
                rtbLicense.LoadFile(AppDomain.CurrentDomain.BaseDirectory + @"\Resources\License.rtf");
            }
            catch { }
        }

        private void checkBox1_CheckedChanged(object sender, EventArgs e)
        {
            Model.LicenseAccepted = cbLicenseAccepted.Checked;
            Model.RaiseModelUpdated();
        }
    }
}
