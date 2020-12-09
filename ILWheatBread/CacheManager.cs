using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ILWheatBread.SmartEmit;

namespace ILWheatBread
{
    internal static class CacheManager
    {
        /// <summary>
        /// 实体Map缓存方案
        /// <para/>
        /// 解决：多次序列化实体结构
        /// </summary>
        internal static ConcurrentDictionary<String, FastProperty[]> EntityCache => new ConcurrentDictionary<String, FastProperty[]>();

        /// <summary>
        /// 公共使用的函数池
        /// </summary>
        internal static Dictionary<String, Delegate> DelegatePool => new Dictionary<String, Delegate>();

        /// <summary>
        /// 缓存存取器
        /// </summary>
        /// <param name="type"></param>
        /// <returns></returns>
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
