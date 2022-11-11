using System.Threading.Tasks;

namespace Truesec.Decryptors.Interfaces
{
    public interface IValidator
    {
        Task<bool> ValidateFileAsync(string filename, string checksum);
    }
}
