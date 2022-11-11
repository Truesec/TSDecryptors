
using System.IO;
using Truesec.Decryptors.Interfaces;

namespace Truesec.Decryptors.Implementations {

    public enum Malware {
        Unknown, 
        Chaos, 
        Onxyv2,
        Solidbit
    }

    public interface IDecryptorChecker { 
        Malware CheckFile(string filename);
    }

    public class DecryptorChecker : IDecryptorChecker {

        public Malware CheckFile(string filename) {
            var result = Malware.Unknown;
            try {
                using (var file = File.OpenRead(filename))
                {
                    byte[] destination = new byte[8];
                    file.Read(destination, 0, 8);
                    if(IsFileEncryptedWithChaosMalware(destination)){
                        result = Malware.Chaos;
                    } else if(IsFileEncryptedWithOnyx2Malware(destination)){
                        result = Malware.Onxyv2;
                    } else {
                        result = Malware.Solidbit;
                    }
                }
            } catch {
                result = Malware.Unknown;
            }
            return result;
        }

        private static bool IsFileEncryptedWithChaosMalware(byte[] file) {
            var isEncryptedWithChaos = true;
            for(int i=0; i<file.Length; i++)
            {
                isEncryptedWithChaos &= file[i] == Constants.CHAOS_FILE_IDENTIFIER[i];
            }
            return isEncryptedWithChaos;
        }

        private static bool IsFileEncryptedWithOnyx2Malware(byte[] file) {
            var isEncryptedWithOnyx2 = true;
            for(int i=0; i<file.Length; i++)
            {
                isEncryptedWithOnyx2 &= file[i] == Constants.ONYX2_FILE_IDENTIFIER[i];
            }
            return isEncryptedWithOnyx2;
        }
    }
}
