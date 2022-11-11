using System;
using Truesec.Decryptors.Interfaces;

namespace Truesec.Decryptors.Providers
{
    internal class SettingsProvider : ISettingsProvider {
        public void Set(string setting, bool value) {
            switch(setting) {
                case Constants.USE_RECURSION_SETTING:
                    Properties.Settings.Default.Recurse = value;
                    break;
                case Constants.LICENSE_ACCEPTED_SETTING:
                    Properties.Settings.Default.LicenseAccepted = value;
                    break;
                default:
                    break;
            }
            Properties.Settings.Default.Save();
        }

        public void Set(string setting, string value) {
            switch(setting) {
                 case Constants.DATABASE_SETTING:
                    Properties.Settings.Default.DatabaseFileName = value;
                    break;
                case Constants.EXTENSION_SETTING:
                    Properties.Settings.Default.Extension = value;
                    break;
                case Constants.DECRYPTOR_NAME_SETTING:
                    Properties.Settings.Default.DecryptorName = value;
                    break;
                default:
                    break;
            }
            Properties.Settings.Default.Save();
        }

        public bool GetBoolean(string setting) {
            switch(setting) {
                case Constants.USE_RECURSION_SETTING:
                    return Properties.Settings.Default.Recurse;
                case Constants.LICENSE_ACCEPTED_SETTING:
                    return Properties.Settings.Default.LicenseAccepted;
                default:
                    throw new System.ArgumentException("Unknown setting");
            }
        }

        public string GetString(string setting) {
            switch(setting) {
                 case Constants.DATABASE_SETTING:
                    return Properties.Settings.Default.DatabaseFileName;
                case Constants.EXTENSION_SETTING:
                    return Properties.Settings.Default.Extension;
                case Constants.DECRYPTOR_NAME_SETTING:
                    return Properties.Settings.Default.DecryptorName;
                default:
                    throw new System.ArgumentException("Unknown setting");
            }
        }
    }
}