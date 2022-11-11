using System;
using System.Security.Cryptography;
using System.Collections.Generic;
using System.Collections;
using System.Text;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using System.Collections.Concurrent;
using Truesec.Decryptors.Logging;
using Truesec.Decryptors.Interfaces;

/*
- Key and IV are 16 bytes
- Both generated exactly same time with System.Random, so Key = IV
- There is no slow key derivation needed, so no point in having a database 
*/
namespace Truesec.Decryptors.Implementations 
{
    public class SolidbitDecryptor : DecryptorBase<string, byte[]>
    {
        // The RSA key is prepended to the encrypted files, this value is the max possible length of the rsa message
        private const int OnyxRSAMsgMaxLength = 344;

        // 'magics' is a dict of all known plaintexts the data is Dictionary<bytes converted to int32, array of extensions with this plaintext> 
        private Dictionary<int, string[]> magics = new Dictionary<int, string[]>();

        public SolidbitDecryptor(IDecryptorCallback decryptorCallback, ILogger logger, IDecryptorChecker decryptorChecker)
            : base(decryptorCallback, logger, decryptorChecker)
        { }

        // Helper function to convert bytes to int32 
        // Note: array must not be longer than four bytes
        private static int ByteArrToInt(byte[] bytes) 
        {
            if (BitConverter.IsLittleEndian)
                Array.Reverse(bytes);
            return BitConverter.ToInt32(bytes, 0);
        }

        protected override double CountOfFiles()
        {
            return filesToDecrypt.Count();
        }

        // Populate the magics dictionary with all known plaintexts 
        // Note: The list is work in progress and it is based on the "extension whitelist" used in the ransomware 
        protected override void LoadMagics()
        {
            magics.Add(ByteArrToInt(new byte[]{0x00,0x3C,0x00,0x3F}),new string[]{".xml",".svg"});
            magics.Add(ByteArrToInt(new byte[]{0x1A,0x45,0xDF,0xA3}),new string[]{".mka",".mkv",".webm"});
            magics.Add(ByteArrToInt(new byte[]{0x21,0x42,0x44,0x4E}),new string[]{".pst"});
            magics.Add(ByteArrToInt(new byte[]{0x25,0x21,0x50,0x53}),new string[]{".ps"});
            magics.Add(ByteArrToInt(new byte[]{0x25,0x50,0x44,0x46}),new string[]{".pdf"});
            magics.Add(ByteArrToInt(new byte[]{0x2D,0x2D,0x2D,0x2D}),new string[]{".cer",".crt"});
            magics.Add(ByteArrToInt(new byte[]{0x30,0x26,0xB2,0x75}),new string[]{".wma",".wmv"});
            magics.Add(ByteArrToInt(new byte[]{0x37,0x7A,0xBC,0xAF}),new string[]{".7zip",".7z"});
            magics.Add(ByteArrToInt(new byte[]{0x38,0x42,0x50,0x53}),new string[]{".psd"});
            magics.Add(ByteArrToInt(new byte[]{0x3C,0x00,0x00,0x00}),new string[]{".xml",".svg"});
            magics.Add(ByteArrToInt(new byte[]{0x3C,0x00,0x3F,0x00}),new string[]{".xml",".svg"});
            magics.Add(ByteArrToInt(new byte[]{0x3C,0x3C,0x3C,0x20}),new string[]{".vdi"});
            magics.Add(ByteArrToInt(new byte[]{0x3C,0x3F,0x78,0x6D}),new string[]{".xml",".svg"});
            magics.Add(ByteArrToInt(new byte[]{0x41,0x54,0x26,0x54}),new string[]{".djvu"});
            magics.Add(ByteArrToInt(new byte[]{0x43,0x44,0x30,0x30}),new string[]{".iso"});
            magics.Add(ByteArrToInt(new byte[]{0x44,0x49,0x43,0x4D}),new string[]{".dcm"});
            magics.Add(ByteArrToInt(new byte[]{0x45,0x4D,0x55,0x33}),new string[]{".iso"});
            magics.Add(ByteArrToInt(new byte[]{0x46,0x4F,0x52,0x4D}),new string[]{".iff"});
            magics.Add(ByteArrToInt(new byte[]{0x47,0x49,0x46,0x38}),new string[]{".gif"});
            magics.Add(ByteArrToInt(new byte[]{0x49,0x49,0x2A,0x00}),new string[]{".tif"});
            magics.Add(ByteArrToInt(new byte[]{0x4C,0x6F,0xA7,0x94}),new string[]{".xml",".svg"});
            magics.Add(ByteArrToInt(new byte[]{0x4D,0x4D,0x00,0x2A}),new string[]{".tif"});
            magics.Add(ByteArrToInt(new byte[]{0x4D,0x53,0x43,0x46}),new string[]{".cab"});
            magics.Add(ByteArrToInt(new byte[]{0x50,0x4B,0x03,0x04}),new string[]{".zip",".apk",".docx",".docm",".jar",".kmz",".odp",".ods",".odt",".pptx",".pptm",".xlsx",".xlsm",".xlsb"});
            magics.Add(ByteArrToInt(new byte[]{0x50,0x4B,0x05,0x06}),new string[]{".zip",".apk",".docx",".docm",".jar",".kmz",".odp",".ods",".odt",".pptx",".pptm",".xlsx",".xlsm",".xlsb"});
            magics.Add(ByteArrToInt(new byte[]{0x50,0x4B,0x07,0x08}),new string[]{".zip",".apk",".docx",".docm",".jar",".kmz",".odp",".ods",".odt",".pptx",".pptm",".xlsx",".xlsm",".xlsb"});
            magics.Add(ByteArrToInt(new byte[]{0x50,0x4D,0x4F,0x43}),new string[]{".dat"});
            magics.Add(ByteArrToInt(new byte[]{0x52,0x49,0x46,0x46}),new string[]{".avi",".wav"});
            magics.Add(ByteArrToInt(new byte[]{0x52,0x49,0x46,0x58}),new string[]{".dcr"});
            magics.Add(ByteArrToInt(new byte[]{0x52,0x61,0x72,0x21}),new string[]{".rar"});
            magics.Add(ByteArrToInt(new byte[]{0x53,0x51,0x4C,0x69}),new string[]{".sql",".db"});
            magics.Add(ByteArrToInt(new byte[]{0x58,0x46,0x49,0x52}),new string[]{".dcr"});
            magics.Add(ByteArrToInt(new byte[]{0x66,0x74,0x79,0x70}),new string[]{".3gp",".3g2",".mp4"});
            magics.Add(ByteArrToInt(new byte[]{0x6B,0x6F,0x6C,0x79}),new string[]{".dmg"});
            magics.Add(ByteArrToInt(new byte[]{0x72,0x65,0x67,0x66}),new string[]{".dat"});
            magics.Add(ByteArrToInt(new byte[]{0x75,0x73,0x74,0x61}),new string[]{".tar"});
            magics.Add(ByteArrToInt(new byte[]{0x76,0x2F,0x31,0x01}),new string[]{".exr"});
            magics.Add(ByteArrToInt(new byte[]{0x7B,0x5C,0x72,0x74}),new string[]{".rtf"});
            magics.Add(ByteArrToInt(new byte[]{0x89,0x50,0x4E,0x47}),new string[]{".png"});
            magics.Add(ByteArrToInt(new byte[]{0xD0,0xCF,0x11,0xE0}),new string[]{".doc",".xls",".ppt",".msg"});
            magics.Add(ByteArrToInt(new byte[]{0xFD,0x37,0x7A,0x58}),new string[]{".xz"});
            magics.Add(ByteArrToInt(new byte[]{0xFF,0xD8,0xFF,0xDB}),new string[]{".jpg",".jpeg",".exif"});
            magics.Add(ByteArrToInt(new byte[]{0xFF,0xD8,0xFF,0xE0}),new string[]{".jpg",".jpeg",".exif"});
            magics.Add(ByteArrToInt(new byte[]{0xFF,0xD8,0xFF,0xEE}),new string[]{".jpg",".jpeg",".exif"});
            magics.Add(ByteArrToInt(new byte[]{0xFF,0xFE,0x00,0x00}),new string[]{".txt"});
        }

        // Recursively search 'targetDir' find files 'targetExt' and populate the 'cryptedFile' 
        // dictionary with the first 16 bytes (one block) and filename
        // We might run out of memory if there are billions of files in the dir
        protected override void FindFiles(string targetDir, string targetExt, bool recursive = true)
        {
            try
            {
                foreach (string file in Directory.GetFiles(targetDir, targetExt, recursive ? SearchOption.AllDirectories : SearchOption.TopDirectoryOnly))
                {
                    decryptorCallback.FileProgession(file, FileStatus.Waiting);
                    ReadFirstEncryptedBlock(file);
                }
            }
            catch (System.Exception excpt)
            {
                logger?.Log("SolidbitDecryptor.FindFiles: "+ excpt.Message);
            }
        }

        // Read the first 16 bytes (1 block) from the file 'filename' 
        // and save it in 'filesToDecrypt' dictionary
        private void ReadFirstEncryptedBlock(string filename)
        {
            byte[] data = new byte[16];
            try
            {
                using (FileStream fs = new FileStream(filename, FileMode.Open, FileAccess.Read))
                {
                    fs.Seek(0, 0);
                    var bytes_read = fs.Read(data, 0, data.Length);
                    fs.Close();

                    if (bytes_read != data.Length)
                    {
                        logger?.Log("SolidbitDecryptor.ReadFirstEncryptedBlock: Failed to read bytes from file '" + filename + "'");
                        decryptorCallback.FileProgession(filename, FileStatus.Failed);
                    }
                }
                filesToDecrypt[filename] = data;
            }
            catch (System.Exception excpt)
            {
                decryptorCallback.FileProgession(filename, FileStatus.Failed);
                logger?.Log("SolidbitDecryptor.ReadFirstEncryptedBlock: [Exception] " + excpt.Message);
            }
        }

        // Iterate all key/iv and try to decrypt every file in 'filesToDecrypt' 
        // assume key was valid if it results in the magic bytes of the file's extension
        protected override void Iterate(string dbfile)
        {
            try
            {
                // TODO: Run this in multiple threads
                Parallel.For(0, int.MaxValue, (seed) =>
                {
                    byte[] randbytes = GenerateBytes(seed);
                    try
                    {
                        TryDecryption(randbytes, randbytes);
                    }
                    catch (Exception excpt)
                    {
                        logger?.Log("SolidbitDecryptor.Iterate: [Exception] " + excpt.Message);
                    }

                    if (seed++ % (2147483648/1000000) == 0)
                    {
                        decryptorCallback.Progress();
                    }
                });
                decryptorCallback.StatusProgression(DecryptorStatus.Done);
            }
            catch (FileNotFoundException ioEx)
            {
                logger?.Log("SolidbitDecryptor.Iterate: [Exception] " + ioEx.Message);
            }
        }

        private byte[] GenerateBytes(int seed)
        {
            StringBuilder stringBuilder = new StringBuilder();
            Random random = new Random(seed);
            for(int i=0;i < 16; i++)
            {
                stringBuilder.Append("abcdefghijklmnopqrstuvwxyzABCDEFGHIJKLMNOPQRSTUVWXYZ1234567890*!=&?&/"[random.Next("abcdefghijklmnopqrstuvwxyzABCDEFGHIJKLMNOPQRSTUVWXYZ1234567890*!=&?&/".Length)]);
            }
            return Encoding.UTF8.GetBytes(stringBuilder.ToString());
        }

        private void TryDecryption(byte[] key, byte[] iv)
        {
            // Foreach key/iv, test decryption
            foreach (KeyValuePair<string, byte[]> cryptedFile in filesToDecrypt)
            {
                // If the first four bytes in the plaintext (as int) exists in magics we are golden
                int bytesAsInt = TestDecrypt(key, iv, cryptedFile.Value);
                string[] extensions;
                if (magics.TryGetValue(bytesAsInt, out extensions))
                {
                    // To avoid false positives, we also check that the extension is expected for the magic bytes
                    string extension = System.IO.Path.GetExtension(cryptedFile.Key);
                    string outputFile = cryptedFile.Key.Substring(0, cryptedFile.Key.Length - extension.Length);

                    // Check for existing files (don't overwrite)
                    while (System.IO.File.Exists(outputFile))
                    {
                        var outputFileName = Path.GetFileNameWithoutExtension(outputFile);
                        var fileInfo = new FileInfo(outputFile);
                        var outputFileExtension = fileInfo.Extension;
                        var path = fileInfo.Directory.ToString();

                        outputFile = $"{path}\\{outputFileName} (copy){outputFileExtension}";
                    }

                    if (Array.IndexOf(extensions, System.IO.Path.GetExtension(outputFile)) >= 0)
                    {
                        decryptorCallback.FileProgession(cryptedFile.Key, FileStatus.Decrypting);
                        DecryptSolidbitFile(cryptedFile.Key, outputFile, iv, key);
                        logger?.Log("SolidbitDecryptor.TryDecryption: Decrypted '" + outputFile + "' (key=" + BitConverter.ToString(key) + ", iv=" + BitConverter.ToString(iv) + ")");
                        decryptorCallback.FileProgession(cryptedFile.Key, FileStatus.Decrypted, outputFile);
                        byte[] outValue;
                        filesToDecrypt.TryRemove(cryptedFile.Key, out outValue);
                    }
                }
            }
        }

        // Decrypt ciphertext 'c' with 'key' and 'iv' and then return the resulting plaintext's first four bytes as int32 
        private static int TestDecrypt(byte[] key, byte[] iv, byte[] c) 
        {
            byte[] firstbytes = new byte[4];
            byte[] buffer = new byte[16];
#pragma warning disable SYSLIB0022
            using (var AES = new RijndaelManaged()) 
            {
                AES.KeySize = 128;
                AES.BlockSize = 128;
                AES.Key = key;
                AES.IV = iv;
                AES.Padding = PaddingMode.None;
                AES.Mode = CipherMode.CBC;
                using (ICryptoTransform decryptor = AES.CreateDecryptor())
                {
                    using (MemoryStream memoryStream = new MemoryStream()) 
                    {
                        memoryStream.Write(c, 0, 16);
                        memoryStream.Seek(0, 0);
                        
                        using (CryptoStream inputCryptoStream = new CryptoStream(memoryStream, decryptor, CryptoStreamMode.Read))
                        {
                            inputCryptoStream.Read(buffer, 0, 16);
                        }
                        firstbytes[0] = buffer[0];
                        firstbytes[1] = buffer[1];
                        firstbytes[2] = buffer[2];
                        firstbytes[3] = buffer[3];
                    }
                }
            }
#pragma warning restore SYSLIB0022
            return ByteArrToInt(firstbytes);
        }

        // Decrypt inputfile into outputfile 
        private static void DecryptSolidbitFile(string inputFile, string outputFile, byte[] iv, byte[] key)
        {
            byte[] inputBytes = System.IO.File.ReadAllBytes(inputFile);

            // Define AES decryptor to match the Solidbit AES encryptor
#pragma warning disable SYSLIB0022
            using (var AES = new RijndaelManaged())
            {
                AES.KeySize = 128;
                AES.BlockSize = 128;
                AES.Key = key;
                AES.IV = iv;
                AES.Padding = PaddingMode.None;
                AES.Mode = CipherMode.CBC;
                using (FileStream outputFileStream = new FileStream(outputFile, FileMode.Create))
                {
                    using (ICryptoTransform decryptor = AES.CreateDecryptor())
                    {
                        using (MemoryStream memoryStream = new MemoryStream())
                        {
                            // Calculate which bytes are the AES ciphertext
                            int RSAMsgLength = OnyxRSAMsgMaxLength;
                            RSAMsgLength -= (inputBytes.Length - OnyxRSAMsgMaxLength) % 16;
                            int bytesToRead = inputBytes.Length - RSAMsgLength;

                            // Write ciphertext to memorystream
                            memoryStream.Write(inputBytes, 0, bytesToRead);
                            memoryStream.Seek(0, 0);

                            using (CryptoStream inputCryptoStream = new CryptoStream(memoryStream, decryptor, CryptoStreamMode.Read))
                            {
                                // Write plaintext to new file
                                byte[] buffer = new byte[BufferSizeInBytes];
                                int count = 0;
                                while ((count = inputCryptoStream.Read(buffer, 0, buffer.Length)) > 0)
                                {
                                    outputFileStream.Write(buffer, 0, count);
                                }
                            }
                        }
                    }
                }
            }
#pragma warning restore SYSLIB0022
        }
    }
}
