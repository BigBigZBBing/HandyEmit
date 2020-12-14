using ILWheatBread.SmartEmit;
using System;
using System.Collections.Concurrent;
using System.Collections.Generic;

namespace ILWheatBread
{
    internal static class CacheManager
    {
        internal static ConcurrentDictionary<String, FastProperty[]> EntityCache => new ConcurrentDictionary<String, FastProperty[]>();

        internal static Dictionary<String, Delegate> DelegatePool => new Dictionary<String, Delegate>();

        internal static Boolean retValue { get; set; }

        internal static FastProperty[] CachePropsManager(this Type type)
        {
            if (!EntityCache.TryGetValue(type.FullName, out FastProperty[] props))
            {
                props = EntityCache.GetOrAdd(type.FullName, x =>
                {
                    var nprops = type.GetProperties();
                    FastProperty[] tempemits = new FastProperty[nprops.Length];
                    for (int i = 0; i < nprops.Length; i++)
                    {
                        tempemits[i] = new FastProperty(nprops[i]);
                    }
                    return tempemits;
                });
                if (props == null) ManagerGX.ShowEx("cache get is null");
            }
            return props;
        }
    }
}
