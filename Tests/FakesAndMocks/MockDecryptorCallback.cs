using System;
using Truesec.Decryptors.Interfaces;

namespace Truesec.Decryptors.FakesAndMocks
{
    internal class MockDecryptorCallback : IDecryptorCallback
    {
        private readonly Action<FileStatus> fileProgressionCallback;
        private readonly Action<DecryptorStatus> statusProgressionCallback;

        public MockDecryptorCallback(Action<FileStatus> fileProgressionCallback, Action<DecryptorStatus> statusProgressionCallback)
        {
            this.fileProgressionCallback = fileProgressionCallback;
            this.statusProgressionCallback = statusProgressionCallback;
        }

        public void StatusProgression(DecryptorStatus status)
        {
            statusProgressionCallback(status);
        }

        public void FileProgession(string filename, FileStatus status, string newFilename = "")
        {
            fileProgressionCallback(status);
        }

        public void Progress()
        {

        }

        public void ResetProgression()
        {

        }
    }
}
