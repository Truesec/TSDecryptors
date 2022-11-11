using System;
using System.Linq;
using System.Collections.ObjectModel;

namespace Truesec.Decryptors.Ioc
{
    public class MappingCollection : Collection<Mapping>
    {
        public Mapping this[Type type, string name]
        {
            get
            {
                var map = (from m in this where m.Interface == type && m.Name == name select m).FirstOrDefault();
                if (map != null)
                    return map;
                else
                {
                    map = new Mapping()
                    {
                        Interface = type,
                        Name = name
                    };
                    this.Add(map);
                    return map;
                }
            }
        }

        public void RemoveInstance(object instance)
        {
            var map = (from m in this where m.Instance.Equals(instance) select m).FirstOrDefault();
            if (map != null)
                map.Instance = null;
        }

        public bool IsRegistered(Type @type, string name)
        {
            var map = (from m in this where m.Interface == type && m.Name == name select m).FirstOrDefault();
            return map != null && (map.Class != null || map.Instance != null);
        }
    }
}
