Param(
    $Start = 0,
    $Stop = 2147483647,
    $DestFile = "$PSScriptRoot\onyx2-hashmap.db"
)

Add-Type -TypeDefinition @"
using System.Text;
using System.Security.Cryptography;

namespace Truesec 
{
    public static class Onyx2HashMapGenerator
    {
        static readonly byte[] salt = new byte[8]
        {
            0x01, 0x02, 0x03, 0x04,
            0x05, 0x06, 0x07, 0x08
        };

        static string CreatePassword(int seed)
        {
            var stringBuilder = new StringBuilder();
            var random = new Random(seed);
            for (int i=0; i<20; i++)
                stringBuilder.Append("abcdefghijklmnopqrstuvwxyzABCDEFGHIJKLMNOPQRSTUVWXYZ1234567890*!=&?&/"[random.Next("abcdefghijklmnopqrstuvwxyzABCDEFGHIJKLMNOPQRSTUVWXYZ1234567890*!=&?&/".Length)]);
            return stringBuilder.ToString();
        }

        private static void GenerateSecret(int seed, FileStream writer) {
            var password = CreatePassword(seed);
            var bytes = Encoding.UTF8.GetBytes(password);
            var rijndaelManaged = new RijndaelManaged();
            rijndaelManaged.KeySize = 256;
            rijndaelManaged.BlockSize = 128;
            rijndaelManaged.Padding = PaddingMode.PKCS7;
            var rfc2898DeriveBytes = new Rfc2898DeriveBytes(bytes, salt, 1);
            rijndaelManaged.Key = rfc2898DeriveBytes.GetBytes(rijndaelManaged.KeySize / 8);
            rijndaelManaged.IV = rfc2898DeriveBytes.GetBytes(rijndaelManaged.BlockSize / 8);
            rijndaelManaged.Mode = CipherMode.CFB;

            using (var encryptor = rijndaelManaged.CreateEncryptor())
            {
                using (var ms = new MemoryStream())
                using (var cryptoStream = new CryptoStream(ms, encryptor, CryptoStreamMode.Write))
                {
                    cryptoStream.Write(new byte[]{0x00,0x00,0x00,0x00,0x00,0x00,0x00,0x00}, 0, 8);
                    cryptoStream.FlushFinalBlock();
                    writer.Write(ms.ToArray(), 0, 4);
                }
            }
        }

        public static void PreComputeTable(string outfile, int start, int stop = Int32.MaxValue)
        {            
            using (var writer = new FileStream(outfile, FileMode.Create, FileAccess.Write, FileShare.None))
            {
                for( int seed=start; seed<stop; seed++ ) {
                    GenerateSecret(seed, writer);
                }
            }
        }
    }
}
"@

[Truesec.Onyx2HashMapGenerator]::PreComputeTable($DestFile, $start, $stop)
