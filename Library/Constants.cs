using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Truesec.Decryptors
{
    public static class Constants
    {
        // Name and description
        public const string NAME = "Chaos Family Decryptors by Truesec";
        public const string DESCRIPTION = "With this decryptor you will be able to attempt to decrypt files that have been " +
                                           "encrypted with the Chaos or Onyx2 malware.";

        // Pages
        public const string WELCOME_PAGE = "Welcome";
        public const string LICENSE_PAGE = "License";
        public const string SETTINGS_PAGE = "Settings";
        public const string FOLDERS_PAGE = "Folders";
        public const string PROGRESS_PAGE = "Progress";

        // Settings
        public const string LICENSE_ACCEPTED_SETTING = "LicenseAccepted";
        public const string DATABASE_SETTING  = "Database";
        public const string EXTENSION_SETTING = "Extension";
        public const string USE_RECURSION_SETTING = "UseRecursion";
        public const string DECRYPTOR_NAME_SETTING = "DecryptorName";

        // Decryptors
        public const string ONYX2_DECRYPTOR = "Onyx/VSOP";
        public const string CHAOS_DECRYPTOR = "Chaos";
        public const string SOLIDBIT_DECRYPTOR = "Solidbit";

        public static byte[] CHAOS_FILE_IDENTIFIER = { (byte)60, (byte)69, (byte)110, (byte)99, 
                                                       (byte)114, (byte)121, (byte)112, (byte)116 };

        public static byte[] ONYX2_FILE_IDENTIFIER = { (byte)01, (byte)02, (byte)03, (byte)04, 
                                                       (byte)05, (byte)06, (byte)07, (byte)08 };

        // Database files
        public const string CHAOS_DB_DOWNLOAD_URL = "https://datafile-public.s3.eu-north-1.amazonaws.com/chaos/";
        public const string CHAOS_DB_FILENAME = "chaos.db";
        public const string CHAOS_DB_CHECKSUM = "dc2450974290c93e01540aa6958f69a8a3cbf7df";

        public const string ONYX2_DB_DOWNLOAD_URL = "https://datafile-public.s3.eu-north-1.amazonaws.com/onyxv2/"; // TODO: Needs actual url
        public const string ONYX2_DB_FILENAME = "onyxv2.db";
        public const string ONYX2_DB_CHECKSUM = "55fd923316e65f64e556070e508fee80f25e5d94"; // TODO: Needs actual checksum

        public const string DATABASE_DEFAULT_EXTENSION = "db";
        public const string DATABASE_DEFAULT_FILTER = "Database(*.db)|*.db";
        public const string SAVE_DATABASE_DIALOG_TITLE = "Select destination to save";
    }
}
