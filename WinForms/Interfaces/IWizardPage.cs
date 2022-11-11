using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CristiPotlog.Controls;

namespace Truesec.Decryptors.Interfaces
{
    public interface IWizardPage
    {
        WizardPageStyle Style { get; set; }
        string Title { get; set; }
        string Description { get; set; }

        IDecryptorModel Model { get; }

        void Activated();
        void Deactivated();
    }
}
