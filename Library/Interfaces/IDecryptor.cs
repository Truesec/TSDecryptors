using System.Threading.Tasks;
using System.Collections.Generic;

namespace Truesec.Decryptors.Interfaces {
    public interface IDecryptor {
        Task RunDecryptor(IList<string> targetDirectories, string extension, bool recursive, string dbFilename);
    }

    public enum DecryptorStatus
    {
        Init = 0,
        ParsingDatabase = 1,
        DecryptingFiles = 2,
        Done = 3,
        Failed = 4
    }

    public enum FileStatus
    {
        Waiting = 0,
        Decrypting,
        Decrypted,
        Failed,
        FileIsNotEncryptedMaybeOverwritten
    }

    public interface IDecryptorCallback
    {
        void StatusProgression(DecryptorStatus status);
        void FileProgession(string filename, FileStatus status, string newFilename = "");
        void Progress();
        void ResetProgression();
    }
}
