using System;
using System.Threading.Tasks;
using System.Collections.Generic;
using Truesec.Decryptors.Interfaces;
using Truesec.Decryptors.Ioc;
using Truesec.Decryptors.Implementations;
using Truesec.Decryptors.Logging;

namespace Truesec.Decryptors.Models
{
    public class DecryptorModel : IDecryptorModel
    {
        private IList<string> folders;
        private readonly ISettingsProvider settings;
        private readonly ILogger logger;
        private readonly IContainer container;

        public IList<string> Folders
        {
            get => folders;
            set => folders = value;
        }

        public DecryptorModel(IContainer ioc)
        {
            folders = new List<string>();
            container = ioc;
            settings = container.Resolve<ISettingsProvider>();
            logger = container.Resolve<ILogger>();
        }

        public void AddFolder(string folder)
        {
            folders.Add(folder);
        }

        public bool IsValidForDecryption
        {
            get
            {
                return LicenseAccepted &&
                    folders.Count > 0 &&
                    !string.IsNullOrWhiteSpace(Extension) &&
                    (!string.IsNullOrWhiteSpace(Database) || DecryptorName == Constants.SOLIDBIT_DECRYPTOR) &&
                    !string.IsNullOrWhiteSpace(DecryptorName);
            }
        }

        public bool LicenseAccepted
        {
            get => settings.GetBoolean(Constants.LICENSE_ACCEPTED_SETTING);
            set => settings.Set(Constants.LICENSE_ACCEPTED_SETTING, value);
        }

        public string Extension
        {
            get => settings.GetString(Constants.EXTENSION_SETTING);
            set => settings.Set(Constants.EXTENSION_SETTING, value);
        }

        public bool UseRecursion
        {
            get => settings.GetBoolean(Constants.USE_RECURSION_SETTING);
            set => settings.Set(Constants.USE_RECURSION_SETTING, value);
        }

        public string Database
        {
            get => settings.GetString(Constants.DATABASE_SETTING);
            set => settings.Set(Constants.DATABASE_SETTING, value);
        }

        public string DecryptorName
        {
            get => settings.GetString(Constants.DECRYPTOR_NAME_SETTING);
            set => settings.Set(Constants.DECRYPTOR_NAME_SETTING, value);
        }

        public void RaiseModelUpdated()
        {
            ModelUpdated?.Invoke(this, new EventArgs());
        }

        public event EventHandler ModelUpdated;

        public Task RunDecryptor() {
            logger.Log("====");
            logger.Log($"DecryptorName: {DecryptorName}");
            logger.Log($"Extension: {Extension}");
            logger.Log($"Database: {Database}");
            logger.Log($"UseRecursion: {UseRecursion}");
            foreach(var folder in folders)
                logger.Log($"Folders: {folder}");
            logger.Log("====");

            var decryptor = container.Resolve<IDecryptor>(this.DecryptorName);
            return decryptor.RunDecryptor(this.Folders, this.Extension, this.UseRecursion, this.Database);
        }
    }
}
