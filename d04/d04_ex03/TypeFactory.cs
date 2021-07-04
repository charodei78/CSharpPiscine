using System;
using System.Reflection;
using BindingFlags = System.Reflection.BindingFlags;

namespace d04_ex03
{
    public class TypeFactory
    {
        public static T CreateWithConstructor<T>() where T : class
        {
            var typeInfo = typeof(T);

            var constructor = typeInfo.GetConstructor(Type.EmptyTypes);

            return (T) constructor.Invoke(null);
        }

        public static T CreateWithActivator<T>() where T : class
        {
            return (T) Activator.CreateInstance(typeof(T));
        }
        
        public static T CreateWithParameters<T>(Object[] parameters) where T : class
        {
            return (T) Activator.CreateInstance(typeof(T), parameters);
        }
    }
}