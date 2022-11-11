using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.IO;
using System.Threading.Tasks;
using Truesec.Decryptors.Interfaces;
using Truesec.Decryptors.Implementations;
using Truesec.Decryptors.Ioc;
using Truesec.Decryptors.Logging;
using Truesec.Decryptors.Models;
using Truesec.Decryptors.FakesAndMocks;

namespace Truesec.Decryptors.Tests
{
    [TestClass]
    public class SolidbitTests
    {
        private Container container;
        [TestInitialize]
        public void Setup()
        {
            container = new Container();

            // Register the settings provider
            container.Register<ISettingsProvider>(new InMemorySettings());

            // Register decryptors
            container.Register<IDecryptor, SolidbitDecryptor>(Constants.SOLIDBIT_DECRYPTOR);

            // Register logger
            container.Register<ILogger, ConsoleLogger>();

            // Decryptor checker
            container.Register<IDecryptorChecker, DecryptorChecker>();

            // Register the model
            var model = new DecryptorModel(container);
            container.Register<IDecryptorModel>(model);
            model.Extension = "*";
            model.DecryptorName = Constants.SOLIDBIT_DECRYPTOR;
        }

        [TestMethod]
        public async Task TestWithoutRecursionInWrongDirectory()
        {
            // Arrange
            var model = container.Resolve<IDecryptorModel>();
            var executingDirectory = Environment.CurrentDirectory;
            model.Folders.Add(Path.Combine(executingDirectory, "..\\..\\..\\Files\\"));
            model.UseRecursion = false;

            var successCount = 0;
            container.Register<IDecryptorCallback>(new MockDecryptorCallback(
                (fileStatus) =>
                {
                    if (fileStatus == FileStatus.Decrypted)
                    {
                        successCount++;
                    }
                },
                (decryptorStatus) =>
                {

                }
            ));

            // Act
            await model.RunDecryptor();

            // Assert
            Assert.AreEqual(0, successCount);
        }

        [TestMethod]
        public async Task TestWithoutRecursionInCorrectDirectory()
        {
            // Arrange
            var model = container.Resolve<IDecryptorModel>();
            var executingDirectory = Environment.CurrentDirectory;
            model.Folders.Add(Path.Combine(executingDirectory, "..\\..\\..\\Files\\Solidbit\\"));
            model.UseRecursion = false;

            var successCount = 0;
            container.Register<IDecryptorCallback>(new MockDecryptorCallback(
                (fileStatus) =>
                {
                    if (fileStatus == FileStatus.Decrypted)
                        successCount++;
                },
                (decryptorStatus) =>
                {

                }
            ));

            // Act
            await model.RunDecryptor();

            // Assert
            Assert.AreEqual(5, successCount);
        }


        [TestMethod]
        public async Task TestWithRecursion()
        {
            // Arrange
            var model = container.Resolve<IDecryptorModel>();
            var executingDirectory = Environment.CurrentDirectory;
            model.Folders.Add(Path.Combine(executingDirectory, "..\\..\\..\\Files\\"));
            model.UseRecursion = true;

            var successCount = 0;
            container.Register<IDecryptorCallback>(new MockDecryptorCallback(
                (fileStatus) =>
                {
                    if (fileStatus == FileStatus.Decrypted)
                    {
                        successCount++;
                    }
                },
                (decryptorStatus) =>
                {
                }
            ));

            // Act
            await model.RunDecryptor();

            // Assert
            Assert.AreEqual(5, successCount);
        }
    }
}
