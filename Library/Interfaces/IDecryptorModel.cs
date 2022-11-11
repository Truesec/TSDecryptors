using System;
using System.Threading.Tasks;
using System.Collections.Generic;

namespace Truesec.Decryptors.Interfaces
{
    public interface IDecryptorModel
    {
        IList<string> Folders { get; }
        void AddFolder(string folder);
        bool LicenseAccepted { get; set; }
        string Database { get; set; }
        string Extension { get; set; }
        bool UseRecursion { get; set; }
        string DecryptorName { get; set; }
        event EventHandler ModelUpdated;
        void RaiseModelUpdated();
        Task RunDecryptor();

        bool IsValidForDecryption { get; }
    }
}
