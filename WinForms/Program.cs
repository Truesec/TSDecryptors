using System;
using System.Windows.Forms;
using Truesec.Decryptors.Implementations;
using Truesec.Decryptors.Download;
using Truesec.Decryptors.Ioc;
using Truesec.Decryptors.Controls;
using Truesec.Decryptors.Validation;
using Truesec.Decryptors.Models;
using Truesec.Decryptors.Pages;
using Truesec.Decryptors.Interfaces;
using Truesec.Decryptors.Providers;

namespace Truesec.Decryptors
{
    static class Program
    {
        /// <summary>
        ///  The main entry point for the application.
        /// </summary>
        [STAThread]
        static void Main()
        {
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);

            Application.DoEvents();
            var container = SetupIoC();
            Application.Run(new DecryptorWizard(container));
        }

        private static IContainer SetupIoC()
        {
            var container = new Container();

            // Download and validation of database files
            container.Register<IFileDownloader, FileDownloader>();
            container.Register<IValidator, ChecksumValidator>();

            // Register settings provider
            container.Register<ISettingsProvider>(new SettingsProvider());
            
            // Register pages
            container.Register<IWizardPage, WelcomePage>(Constants.WELCOME_PAGE);
            container.Register<IWizardPage, LicensePage>(Constants.LICENSE_PAGE);
            container.Register<IWizardPage, FoldersPage>(Constants.FOLDERS_PAGE);
            container.Register<IWizardPage, SettingsPage>(Constants.SETTINGS_PAGE);
            container.Register<IWizardPage, ProgressPage>(Constants.PROGRESS_PAGE);

            // Register decryptors
            container.Register<IDecryptor, Onyx2Decryptor>(Constants.ONYX2_DECRYPTOR);
            container.Register<IDecryptor, ChaosDecryptor>(Constants.CHAOS_DECRYPTOR);
            container.Register<IDecryptor, SolidbitDecryptor>(Constants.SOLIDBIT_DECRYPTOR);

            // Register logger
            container.Register<ILogger, Log4NetProvider>();

            // Decryptor checker
            container.Register<IDecryptorChecker, DecryptorChecker>();

            // Register the model
            container.Register<IDecryptorModel>(
                new DecryptorModel(container)
            );
            return container;
        }
    }
}