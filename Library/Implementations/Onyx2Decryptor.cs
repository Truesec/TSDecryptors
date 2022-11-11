using System.Security.Cryptography;
using System.Collections;
using System.Text;
using System.Collections.Generic;
using System;
using System.IO;
using System.Threading.Tasks;
using System.Linq;
using Truesec.Decryptors.Logging;
using Truesec.Decryptors.Interfaces;

namespace Truesec.Decryptors.Implementations
{
    public class Onyx2Decryptor : DecryptorBase<int, ArrayList>
    {
        // The length of the salt which is prepended to encrypted files
        private const int SaltLength = 8;

        // The RSA key is prepended to the encrypted files, this value is the max possible length of the rsa message
        private const int OnyxRSAMsgMaxLength = 344;

        // The magics are all known plaintexts
        // Data is Dictionary<extension, arraylist of bytearrays (possible magic bytes)>
        private Dictionary<string, ArrayList> magics = new Dictionary<string, ArrayList>();

        public Onyx2Decryptor(IDecryptorCallback decryptorCallback, ILogger logger, IDecryptorChecker decryptorChecker)
            : base(decryptorCallback, logger, decryptorChecker)
        { }

        // Helper to convert bytes to int32 
        // Note: Length should be four bytes
        private static int ByteArrToInt(byte[] bytes)
        {
            if (BitConverter.IsLittleEndian)
                Array.Reverse(bytes);
            return BitConverter.ToInt32(bytes, 0);
        }

        private void AddToMagics(byte[] arr, string[] extensions)
        {
            foreach (var extension in extensions)
            {
                if (!magics.ContainsKey(extension))
                    magics.Add(extension, new ArrayList());
                magics[extension].Add(arr);
            }
        }

        protected override double CountOfFiles()
        {
            return filesToDecrypt.Sum(f => f.Value.Count);
        }

        // Populate the 'magics' dictionary
        protected override void LoadMagics()
        {
            AddToMagics(new byte[] { 0x00, 0x3C, 0x00, 0x3F }, new string[] { ".xml", ".svg" });
            AddToMagics(new byte[] { 0x1A, 0x45, 0xDF, 0xA3 }, new string[] { ".mka", ".mkv", ".webm" });
            AddToMagics(new byte[] { 0x21, 0x42, 0x44, 0x4E }, new string[] { ".pst" });
            AddToMagics(new byte[] { 0x25, 0x21, 0x50, 0x53 }, new string[] { ".ps" });
            AddToMagics(new byte[] { 0x25, 0x50, 0x44, 0x46 }, new string[] { ".pdf" });
            AddToMagics(new byte[] { 0x2D, 0x2D, 0x2D, 0x2D }, new string[] { ".cer", ".crt" });
            AddToMagics(new byte[] { 0x30, 0x26, 0xB2, 0x75 }, new string[] { ".wma", ".wmv" });
            AddToMagics(new byte[] { 0x37, 0x7A, 0xBC, 0xAF }, new string[] { ".7zip", ".7z" });
            AddToMagics(new byte[] { 0x38, 0x42, 0x50, 0x53 }, new string[] { ".psd" });
            AddToMagics(new byte[] { 0x3C, 0x00, 0x00, 0x00 }, new string[] { ".xml", ".svg" });
            AddToMagics(new byte[] { 0x3C, 0x00, 0x3F, 0x00 }, new string[] { ".xml", ".svg" });
            AddToMagics(new byte[] { 0x3C, 0x3C, 0x3C, 0x20 }, new string[] { ".vdi" });
            AddToMagics(new byte[] { 0x3C, 0x3F, 0x78, 0x6D }, new string[] { ".xml", ".svg" });
            AddToMagics(new byte[] { 0x41, 0x54, 0x26, 0x54 }, new string[] { ".djvu" });
            AddToMagics(new byte[] { 0x43, 0x44, 0x30, 0x30 }, new string[] { ".iso" });
            AddToMagics(new byte[] { 0x44, 0x49, 0x43, 0x4D }, new string[] { ".dcm" });
            AddToMagics(new byte[] { 0x45, 0x4D, 0x55, 0x33 }, new string[] { ".iso" });
            AddToMagics(new byte[] { 0x46, 0x4F, 0x52, 0x4D }, new string[] { ".iff" });
            AddToMagics(new byte[] { 0x47, 0x49, 0x46, 0x38 }, new string[] { ".gif" });
            AddToMagics(new byte[] { 0x49, 0x49, 0x2A, 0x00 }, new string[] { ".tif" });
            AddToMagics(new byte[] { 0x4C, 0x6F, 0xA7, 0x94 }, new string[] { ".xml", ".svg" });
            AddToMagics(new byte[] { 0x4D, 0x4D, 0x00, 0x2A }, new string[] { ".tif" });
            AddToMagics(new byte[] { 0x4D, 0x53, 0x43, 0x46 }, new string[] { ".cab" });
            AddToMagics(new byte[] { 0x50, 0x4B, 0x03, 0x04 }, new string[] { ".zip", ".apk", ".docx", ".docm", ".jar", ".kmz", ".odp", ".ods", ".odt", ".pptx", ".pptm", ".xlsx", ".xlsm", ".xlsb" });
            AddToMagics(new byte[] { 0x50, 0x4B, 0x05, 0x06 }, new string[] { ".zip", ".apk", ".docx", ".docm", ".jar", ".kmz", ".odp", ".ods", ".odt", ".pptx", ".pptm", ".xlsx", ".xlsm", ".xlsb" });
            AddToMagics(new byte[] { 0x50, 0x4B, 0x07, 0x08 }, new string[] { ".zip", ".apk", ".docx", ".docm", ".jar", ".kmz", ".odp", ".ods", ".odt", ".pptx", ".pptm", ".xlsx", ".xlsm", ".xlsb" });
            AddToMagics(new byte[] { 0x50, 0x4D, 0x4F, 0x43 }, new string[] { ".dat" });
            AddToMagics(new byte[] { 0x52, 0x49, 0x46, 0x46 }, new string[] { ".avi", ".wav" });
            AddToMagics(new byte[] { 0x52, 0x49, 0x46, 0x58 }, new string[] { ".dcr" });
            AddToMagics(new byte[] { 0x52, 0x61, 0x72, 0x21 }, new string[] { ".rar" });
            AddToMagics(new byte[] { 0x53, 0x51, 0x4C, 0x69 }, new string[] { ".sql", ".db" });
            AddToMagics(new byte[] { 0x58, 0x46, 0x49, 0x52 }, new string[] { ".dcr" });
            AddToMagics(new byte[] { 0x66, 0x74, 0x79, 0x70 }, new string[] { ".3gp", ".3g2", ".mp4" });
            AddToMagics(new byte[] { 0x6B, 0x6F, 0x6C, 0x79 }, new string[] { ".dmg" });
            AddToMagics(new byte[] { 0x72, 0x65, 0x67, 0x66 }, new string[] { ".dat" });
            AddToMagics(new byte[] { 0x75, 0x73, 0x74, 0x61 }, new string[] { ".tar" });
            AddToMagics(new byte[] { 0x76, 0x2F, 0x31, 0x01 }, new string[] { ".exr" });
            AddToMagics(new byte[] { 0x7B, 0x5C, 0x72, 0x74 }, new string[] { ".rtf" });
            AddToMagics(new byte[] { 0x89, 0x50, 0x4E, 0x47 }, new string[] { ".png" });
            AddToMagics(new byte[] { 0xD0, 0xCF, 0x11, 0xE0 }, new string[] { ".doc", ".xls", ".ppt", ".msg" });
            AddToMagics(new byte[] { 0xFD, 0x37, 0x7A, 0x58 }, new string[] { ".xz" });
            AddToMagics(new byte[] { 0xFF, 0xD8, 0xFF, 0xDB }, new string[] { ".jpg", ".jpeg", ".exif" });
            AddToMagics(new byte[] { 0xFF, 0xD8, 0xFF, 0xE0 }, new string[] { ".jpg", ".jpeg", ".exif" });
            AddToMagics(new byte[] { 0xFF, 0xD8, 0xFF, 0xEE }, new string[] { ".jpg", ".jpeg", ".exif" });
            AddToMagics(new byte[] { 0xFF, 0xFE, 0x00, 0x00 }, new string[] { ".txt" });
        }

        // 1) Get the first four bytes of filename 
        // 2) Get the possible plaintexts (magic bytes) for the extension
        // 3) Xor the bytes and save to 'filesToDecrypt'
        private void GenerateStreams(string filename, string originalExt)
        {
            try
            {
                if(decryptorChecker.CheckFile(filename) == Malware.Onxyv2) { 
                    byte[] data = new byte[4];
                    using (FileStream fs = new FileStream(filename, FileMode.Open, FileAccess.Read))
                    {
                        fs.Seek(8, 0);
                        var bytes_read = fs.Read(data, 0, data.Length);
                        fs.Close();

                        if (bytes_read != data.Length)
                        {
                            logger?.Log("Onyx2Decryptor.GenerateStreams: Failed to read bytes from file '" + filename + "'");
                            decryptorCallback.FileProgession(filename, FileStatus.Failed);
                        }
                    }

                    ArrayList magicArr;
                    if (magics.TryGetValue(originalExt, out magicArr))
                    {
                        foreach (byte[] magic in magicArr)
                        {
                            byte[] stream = new byte[4];
                            for (int i = 0; i < 4; i++)
                            {
                                stream[i] = (byte)(data[i] ^ magic[i]);
                            }
                            int key = ByteArrToInt(stream);
                            if (!filesToDecrypt.ContainsKey(key))
                            {
                                filesToDecrypt[key] = new ArrayList();
                            }
                            filesToDecrypt[key].Add(filename);
                        }
                    }
                    else
                    {
                        logger?.Log("Onyx2Decryptor.GenerateStreams: Unknown file extension " + filename);
                    }
                } else {
                    decryptorCallback.FileProgession(filename, FileStatus.FileIsNotEncryptedMaybeOverwritten);
                }
            }
            catch (System.Exception excpt)
            {
                decryptorCallback.FileProgession(filename, FileStatus.Failed);
                logger?.Log("Onyx2Decryptor.GenerateStreams: [Exception] " + excpt.Message);
            }
        }

        // Recursively search 'targetDir' find files 'targetExt' and populate the 'cryptedFile' 
        // dictionary with the first 16 bytes (one block) and filename
        // We might run out of memory if there are billions of files in the dir
        protected override void FindFiles(string targetDir, string targetExt, bool recursive = true)
        {
            try
            {
                Parallel.ForEach(Directory.GetFiles(targetDir, targetExt, recursive ? SearchOption.AllDirectories : SearchOption.TopDirectoryOnly).AsEnumerable(),(string file) =>
                {
                    decryptorCallback.FileProgession(file, FileStatus.Waiting);
                    string extension = System.IO.Path.GetExtension(file);
                    string basename = file.Substring(0, file.Length - extension.Length);
                    var lastIndex = basename.LastIndexOf('.');
                    string origext = lastIndex > 0 ? basename.Substring(lastIndex) : extension;
                    GenerateStreams(file, origext);
                });
            }
            catch (System.Exception excpt)
            {
                logger?.Log("Onyx2Decryptor.FindFiles: [Exception] " + excpt.Message);
            }
        }

        // Iterate the dbfile, which contains all possible four byte ciphers 
        // The position of the four bytes equals the seed 
        protected override void Iterate(string dbfile)
        {
            logger?.Log("Onyx2Decryptor::Iterate(" + dbfile + ")");
            try
            {
                int seed = 0;
                int bytesRead = 0;
                int buffersize = 4 * 100000;
                byte[] buffer = new byte[buffersize];
                int chunksize = 4;
                byte[] firstbytes = new byte[chunksize];
                int seedProgression = 0;
                using (var fsSource = new FileStream(dbfile, FileMode.Open, FileAccess.Read))
                {
                    seedProgression = (int)(fsSource.Length / 1000000);
                    if (seedProgression == 0) seedProgression = 1;
                    bool stillFilesToDecrypt = filesToDecrypt.Any(pair => pair.Value.Count > 0);
                    while (stillFilesToDecrypt)
                    {
                        int n = fsSource.Read(buffer, 0, buffersize);                        
                        if (n == 0)
                            break;
                        
                        for(int i=0;i<buffersize;i+=chunksize)
                        {
                            Array.Copy(buffer, i, firstbytes, 0, chunksize);

                            try
                            {
                                if(TryDecryption(ByteArrToInt(firstbytes), seed) == false) {
                                    stillFilesToDecrypt = false;
                                    break;
                                }
                            }
                            catch (Exception ex)
                            {
                                logger?.Log("Onyx2Decryptor.Iterate: [Exception] " + ex.Message);
                            }
                            seed += 1;
                            if (seed % seedProgression == 0)
                            {
                                decryptorCallback.Progress();
                            }
                        }

                        bytesRead += buffersize;
                    }
                }
                decryptorCallback.StatusProgression(DecryptorStatus.Done);
            }
            catch (FileNotFoundException ioEx)
            {
                decryptorCallback.StatusProgression(DecryptorStatus.Failed);
                logger?.Log("Onyx2Decryptor.Iterate: [Exception] " + ioEx.Message);
            }
        }

        // Compares as int32 with the plaintext guess
        // If it matches, calculate the secrets and decrypt the file
        private bool TryDecryption(int key, int seed)
        {
            var filesDecrypted = new List<string>();
            ArrayList files;
            if (filesToDecrypt.TryGetValue(key, out files))
            {
                filesDecrypted.Clear();
                foreach (string filename in files)
                {
                    decryptorCallback.FileProgession(filename, FileStatus.Decrypting);
                    logger?.Log("Onyx2Decryptor::TryDecryption(" + key + "," + seed + ") => Found seed for '" + filename + "'");
                    string extension = System.IO.Path.GetExtension(filename);
                    string outputFile = filename.Substring(0, filename.Length - extension.Length);

                    // Check for existing files (don't overwrite)
                    while (System.IO.File.Exists(outputFile))
                    {
                        var outputFileName = Path.GetFileNameWithoutExtension(outputFile);
                        var fileInfo = new FileInfo(outputFile);
                        var outputFileExtension = fileInfo.Extension;
                        var path = fileInfo.Directory.ToString();

                        outputFile = $"{path}\\{outputFileName} (copy){outputFileExtension}";
                    }

                    string password = CreatePassword(seed);
                    try
                    {
                        DecryptOnyx2File(filename, outputFile, password);
                    }
                    catch (Exception ex)
                    {
                        logger?.Log("Onyx2Decryptor.TryDecryption: [Exception] " + ex.Message + "(filename="+filename+")");
                    }
                    if (System.IO.File.Exists(outputFile))
                    {
                        logger?.Log("Onyx2Decryptor.TryDecryption: Successful decryption of '" + filename + "'");
                        decryptorCallback.FileProgession(filename, FileStatus.Decrypted, outputFile);
                        filesDecrypted.Add(filename);
                        //foreach(var array in filesToDecrypt)
                        //    array.Value.Remove(filename);
                    }
                    else
                    {
                        logger?.Log("Onyx2Decryptor.TryDecryption: Failed to create outputFile '" + outputFile + "'");
                        decryptorCallback.FileProgession(filename, FileStatus.Failed, outputFile);
                    }
                }
            }
            foreach (var array in filesToDecrypt.Values)
            {
                foreach (var filename in filesDecrypted)
                {
                    array.Remove(filename);
                }
            }
            return filesToDecrypt.Any(pair => pair.Value.Count > 0);
        }

        // Helper to create the proper password once we know the seed
        private static string CreatePassword(int seed)
        {
            StringBuilder stringBuilder = new StringBuilder();
            Random random = new Random(seed);
            for (int i = 0; i < 40; i++)
                stringBuilder.Append("abcdefghijklmnopqrstuvwxyzABCDEFGHIJKLMNOPQRSTUVWXYZ1234567890*!=&?&/"[random.Next("abcdefghijklmnopqrstuvwxyzABCDEFGHIJKLMNOPQRSTUVWXYZ1234567890*!=&?&/".Length)]);
            return stringBuilder.ToString();
        }

        // Decrypt the file to the final decrypted result file 
        // Derive key and IV from the now known password
        private void DecryptOnyx2File(string inputFile, string outputFile, string password)
        {
            // Decryptor for Onyx variant seen in june 2022
            byte[] passwordBytes = System.Text.Encoding.UTF8.GetBytes(password);
            byte[] inputBytes = System.IO.File.ReadAllBytes(inputFile);
            byte[] salt = new byte[SaltLength];

            // Read salt
            Array.Copy(inputBytes, 0, salt, 0, SaltLength);

            // Define AES decryptor to match the Onyx AES encryptor
#pragma warning disable SYSLIB0022
            using (var AES = new RijndaelManaged())
            {
                AES.KeySize = 256;
                AES.BlockSize = 128;
                var derived = new Rfc2898DeriveBytes(passwordBytes, salt, 1);
                AES.Key = derived.GetBytes(AES.KeySize / 8);
                AES.IV = derived.GetBytes(AES.BlockSize / 8);
                AES.Padding = PaddingMode.PKCS7;
                AES.Mode = CipherMode.CFB;
                using (FileStream outputFileStream = new FileStream(outputFile, FileMode.Create))
                {
                    using (ICryptoTransform decryptor = AES.CreateDecryptor())
                    {
                        using (MemoryStream memoryStream = new MemoryStream())
                        {
                            // Calculate which bytes are the AES ciphertext
                            int RSAMsgLength = OnyxRSAMsgMaxLength;
                            RSAMsgLength -= (inputBytes.Length - SaltLength - OnyxRSAMsgMaxLength) % 16;
                            int bytesToRead = inputBytes.Length - RSAMsgLength - SaltLength;

                            // Write ciphertext to memorystream
                            memoryStream.Write(inputBytes, SaltLength, bytesToRead);
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
