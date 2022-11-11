using System;

namespace Truesec.Decryptors.Interfaces
{
    public interface IContainer
    {
        void Register<TFrom, TTo>(string key = "") where TTo : TFrom;
        void Register<TInterface>(TInterface instance, string key = "");
        bool IsRegistered<T>(string name = "");
        T Resolve<T>(string key = "");
        object Resolve(Type type, string key = "");
        void Remove(object instance);
    }
}
