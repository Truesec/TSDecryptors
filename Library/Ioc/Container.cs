using System;
using System.Collections.Generic;
using System.Linq;
using Truesec.Decryptors.Interfaces;

namespace Truesec.Decryptors.Ioc
{
    public class Container : IContainer
    {
        private MappingCollection mapping;

        public Container()
        {
            this.mapping = new MappingCollection();
            this.Register<IContainer>(this);
        }

        public object Resolve(Type @interface, string name = "")
        {
            var instance = this.FindInstanceOfType(@interface, name);
            if (instance != null) return instance;

            var type = this.FindType(@interface, name);
            if (type == null) throw new Exception("The type " + @interface.ToString() + " cannot be resolved");

            var obj = ConstructObject(type);
            if (obj == null) throw new Exception("Cannot resolve " + @interface.ToString());

            return obj;
        }

        public T Resolve<T>(string name = "")
        {
            return (T)Resolve(typeof(T), name);
        }

        private object ConstructObject(Type type)
        {
            var constructor = type.GetConstructors().FirstOrDefault();
            if (constructor != null)
            {
                var constparams = new List<object>();
                foreach (var param in constructor.GetParameters())
                {
                    constparams.Add(this.Resolve(param.ParameterType));
                }
                var obj = constructor.Invoke(constparams.ToArray());

                //now check for export params and do the imports
                foreach (var prop in type.GetProperties())
                {
                    var attr = prop.GetCustomAttributes(typeof(ImportAttribute), false).FirstOrDefault() as ImportAttribute;
                    if (attr != null)
                    {
                        var inject = this.Resolve(prop.PropertyType, attr.Name);
                        prop.SetValue(obj, inject, null);
                    }
                }
                return obj;
            }
            else
            {
                throw new Exception("There is no constructor for type " + type.ToString());
            }
        }

        private Type FindType(Type @interface, string name)
        {
            Type type;
            if (@interface.IsInterface)
            {
                return this.mapping[@interface, name].Class;
            }
            else
            {
                type = @interface;
            }
            return type;
        }

        private object FindInstanceOfType(Type type, string name)
        {
            return (from val in this.mapping where type.IsInstanceOfType(val.Instance) && val.Name == name select val.Instance).FirstOrDefault();
        }

        public void Register<T, C>(string name = "") where C : T
        {
            var @interface = typeof(T);
            var @class = typeof(C);

            var map = this.mapping[@interface, name];
            map.Class = @class;
            map.Instance = null;
        }

        public bool IsRegistered<T>(string name = "")
        {
            var type = typeof(T);
            return this.mapping.IsRegistered(type, name);
        }

        public void Register<T>(T instance, string name = "")
        {
            var @interface = typeof(T);
            this.mapping[@interface, name].Instance = instance;
        }

        public void Remove(object instance)
        {
            this.mapping.RemoveInstance(instance);
        }
    }
}
