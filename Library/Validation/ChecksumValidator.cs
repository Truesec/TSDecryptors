using System;
using System.IO;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;
using Truesec.Decryptors.Logging;
using Truesec.Decryptors.Interfaces;

namespace Truesec.Decryptors.Validation
{
    public class ChecksumValidator : IValidator
    {
        private readonly ILogger logger;

        public ChecksumValidator(ILogger logger)
        {
            this.logger = logger;
        }

        public async Task<bool> ValidateFileAsync(string filename, string checksum)
        {
            var sha1 = await GetSHA1HashFromFileAsync(filename);
            var result = sha1 == checksum;
            this.logger?.Log("Validation " + (result ? "Success" : "Failed"));
            return result;
        }

        public static Task<String> GetSHA1HashFromFileAsync(String fileName)
        {
            return Task.Run(() =>  {
                var file = new FileStream(fileName, FileMode.Open);
                SHA1 sha1 = new SHA1CryptoServiceProvider();
                byte[] retVal = sha1.ComputeHash(file);
                file.Close();

                StringBuilder sb = new StringBuilder();
                for (int i = 0; i < retVal.Length; i++)
                {
                    sb.Append(retVal[i].ToString("x2"));
                }
                return sb.ToString();
            });
        }
    }
}
