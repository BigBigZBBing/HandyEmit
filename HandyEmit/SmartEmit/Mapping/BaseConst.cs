using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;

namespace HandyEmit.SmartEmit.Mapping
{
    internal static class BaseConst<T>
    {

        internal static readonly MethodInfo _ListAdd = typeof(List<T>).GetMethod("Add", new Type[] { typeof(T) });
        internal static readonly MethodInfo _ListContains = typeof(List<T>).GetMethod("Contains", new Type[] { typeof(T) });
        internal static readonly MethodInfo _ListAddRange = typeof(List<T>).GetMethod("AddRange", new Type[] { typeof(IEnumerable<T>) });
        internal static readonly MethodInfo _ListClear = typeof(List<T>).GetMethod("Clear");
        internal static readonly MethodInfo _ListIndexOf = typeof(List<T>).GetMethod("IndexOf", new Type[] { typeof(T) });
        internal static readonly MethodInfo _ListLastIndexOf = typeof(List<T>).GetMethod("LastIndexOf", new Type[] { typeof(T) });
        internal static readonly MethodInfo _ListRemove = typeof(List<T>).GetMethod("Remove", new Type[] { typeof(T) });
        internal static readonly MethodInfo _ListRemoveAt = typeof(List<T>).GetMethod("RemoveAt", new Type[] { typeof(Int32) });
        internal static readonly MethodInfo _ListToArray = typeof(List<T>).GetMethod("ToArray");
        internal static readonly MethodInfo _ArrayIndexOf = typeof(Array).GetMethod("IndexOf", new Type[] { typeof(Array), typeof(T) });
        internal static readonly MethodInfo _ArrayLastIndexOf = typeof(Array).GetMethod("LastIndexOf", new Type[] { typeof(Array), typeof(T) });
        internal static readonly MethodInfo _EnumerableContains = typeof(Enumerable).GetMethod("Contains", new Type[] { typeof(IEnumerable<T>), typeof(T) });
        internal static readonly MethodInfo _EnumerableCount = typeof(Enumerable).GetMethod("Count", new Type[] { typeof(IEnumerable<T>) });
        internal static readonly MethodInfo _DateTimeCount = typeof(DateTime).GetMethod("Count", new Type[] { typeof(IEnumerable<T>) });

    }
}
