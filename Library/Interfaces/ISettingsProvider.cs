namespace Truesec.Decryptors.Interfaces
{
    public interface ISettingsProvider
    {
        void Set(string setting, string value);
        void Set(string setting, bool value);

        string GetString(string setting);
        bool GetBoolean(string setting);
    }
}