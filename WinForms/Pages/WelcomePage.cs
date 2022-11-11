using CristiPotlog.Controls;
using Truesec.Decryptors.Interfaces;

namespace Truesec.Decryptors.Pages
{
    public partial class WelcomePage : WizardPage, IWizardPage
    {
        public WelcomePage(IDecryptorModel model, ILogger logger) : base(model, logger)
        {
            InitializeComponent();
        }
    }
}
