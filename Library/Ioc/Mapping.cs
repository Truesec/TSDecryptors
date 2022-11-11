using System;

namespace Truesec.Decryptors.Ioc
{
    public class Mapping
    {
        public Type Interface { get; set; }
        public Type Class { get; set; }
        public object Instance { get; set; }
        public string Name { get; set; }
    }
}
