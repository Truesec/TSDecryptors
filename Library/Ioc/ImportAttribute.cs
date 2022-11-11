using System;

namespace Truesec.Decryptors.Ioc
{
    [AttributeUsage(AttributeTargets.Property)]
    public class ImportAttribute : Attribute
    {
        public ImportAttribute(string name = "")
        {
            this.Name = name;
        }

        public string Name { get; set; }
    }
}
