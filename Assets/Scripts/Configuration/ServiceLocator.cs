using System;
using System.Collections.Generic;

namespace Configuration
{
    public static class ServiceLocator
    {
        private static readonly Dictionary<Type, object> services = new Dictionary<Type, object>();

        public static void Register<T>(T service)
        {
            var type = typeof(T);
            if (!services.ContainsKey(type))
            {
                services[type] = service;
            }
        }

        public static T Resolve<T>()
        {
            var type = typeof(T);
            if (services.ContainsKey(type))
            {
                return (T)services[type];
            }
            throw new Exception($"Service of type {type} not registered.");
        }
    }
}
