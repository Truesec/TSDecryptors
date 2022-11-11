using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using Truesec.Decryptors.Logging;
using System.Collections.Concurrent;
using Truesec.Decryptors.Interfaces;

namespace Truesec.Decryptors.Implementations
{
    public struct RunDecryptorState
    {
        public IList<string> TargetDirectories;
        public string TargetExtension;
        public bool UseRecursion;
        public string DatabaseFilename;
    }

    public abstract class DecryptorBase<TKey, TValue>: IDecryptor
    {
        protected ConcurrentDictionary<TKey, TValue> filesToDecrypt = new ConcurrentDictionary<TKey, TValue>();
        protected readonly IDecryptorCallback decryptorCallback;
        protected readonly ILogger logger;
        protected readonly IDecryptorChecker decryptorChecker;

        // Buffer size used when performing decryption
        protected const int BufferSizeInBytes = 16 * 1024;

        public DecryptorBase(IDecryptorCallback decryptorCallback, ILogger logger, IDecryptorChecker decryptorChecker)
        {
            this.decryptorCallback = decryptorCallback;
            this.logger = logger;
            this.decryptorChecker = decryptorChecker;
        }

        public Task RunDecryptor(IList<string> targetDirectories, string extension, bool recursive, string dbFilename)
        {
            LoadMagics();

            // Run iteration on background thread
            return Task.Run(() => {
                try
                {
                    decryptorCallback.StatusProgression(DecryptorStatus.Init);
                    foreach (var targetDirectory in targetDirectories)
                    {
                        FindFiles(targetDirectory, extension, recursive);
                    }
                    
                    if(filesToDecrypt.Any()) {
                        logger?.Log($"Found {CountOfFiles()} files that will be evaluated!");
                        decryptorCallback.StatusProgression(DecryptorStatus.ParsingDatabase); 
                        decryptorCallback.ResetProgression();
                        Iterate(dbFilename);
                    } else {
                        decryptorCallback.StatusProgression(DecryptorStatus.Done);
                    }
                }
                catch (System.Exception excpt)
                {
                    logger?.Log(excpt.Message);
                    decryptorCallback.StatusProgression(DecryptorStatus.Failed);
                }
            });
        }

        protected abstract void LoadMagics();
        protected abstract void FindFiles(string targetDir, string targetExt, bool recursive = true);
        protected abstract void Iterate(string dbfile);
        protected abstract double CountOfFiles();
    }
}
