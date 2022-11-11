using System.Collections.Generic;
using Truesec.Decryptors.Interfaces;

namespace Truesec.Decryptors.FakesAndMocks
{
    internal class InMemorySettings : ISettingsProvider
    {
        private IDictionary<string, object> settings = new Dictionary<string, object>();

        public void Set(string setting, bool value)
        {
            settings[setting] = value;
        }

        public void Set(string setting, string value)
        {
            settings[setting] = value;
        }

        public bool GetBoolean(string setting)
        {
            return settings.ContainsKey(setting) ? (bool)settings[setting] : false;
        }

        public string GetString(string setting)
        {
            return settings.ContainsKey(setting) ? (string)settings[setting] : string.Empty;
        }
    }
}
