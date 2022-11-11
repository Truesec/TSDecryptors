using System;
using System.Threading.Tasks;
using System.Collections.Generic;
using Truesec.Decryptors.Interfaces;

namespace Truesec.Decryptors.FakesAndMocks {
    public class FakeDecryptor : IDecryptor {
        public Task RunDecryptor(IList<string> targetDirectories, string extension, bool recursive, string dbFilename)
        {
            throw new NotImplementedException();
        }
    }
}
