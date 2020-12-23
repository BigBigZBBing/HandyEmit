using ILWheatBread.SmartEmit.Func;
using System;
using System.Linq;
using System.Reflection;
using System.Reflection.Emit;

namespace ILWheatBread.SmartEmit
{
    public static class EmitBasicExtension
    {

        public static void EmitParam(this EmitBasic basic, Int32 index)
        {
            switch (index)
            {
                case 0: basic.Emit(OpCodes.Ldarg_0); break;
                case 1: basic.Emit(OpCodes.Ldarg_1); break;
                case 2: basic.Emit(OpCodes.Ldarg_2); break;
                case 3: basic.Emit(OpCodes.Ldarg_3); break;
                default: basic.Emit(OpCodes.Ldarg_S, index); break;
            }
        }

        public static LocalBuilder EmitParamRef<T>(this EmitBasic basic, Int32 index) where T : class
        {
            LocalBuilder local = basic.DeclareLocal(typeof(LocalBuilder));
            LocalBuilder param = basic.DeclareLocal(typeof(T));
            basic.EmitParam(index);
            basic.Emit(OpCodes.Stloc_S, local);
            basic.Emit(OpCodes.Ldloc_S, local);
            basic.Emit(OpCodes.Stloc_S, param);
            return param;
        }

        public static LocalBuilder EmitParamRef(this EmitBasic basic, Int32 index, Type type)
        {
            LocalBuilder local = basic.DeclareLocal(typeof(LocalBuilder));
            LocalBuilder param = basic.DeclareLocal(type);
            basic.EmitParam(index);
            basic.Emit(OpCodes.Stloc_S, local);
            basic.Emit(OpCodes.Ldloc_S, local);
            basic.Emit(OpCodes.Stloc_S, param);
            return param;
        }

        public static void EmitThrow(this EmitBasic basic, LocalBuilder ex)
        {
            if (ex == null)
            {
                basic.Emit(OpCodes.Rethrow);
                return;
            }
            basic.Emit(OpCodes.Ldloc_S, ex);
            basic.Emit(OpCodes.Throw);
        }

        public static MethodManager CallvirtMethod<T>(this VariableManager basic, String MethodName)
        {
            Type type = typeof(T);
            MethodInfo method = type.GetMethod(MethodName, Type.EmptyTypes);
            basic.Output();
            basic.Emit(OpCodes.Callvirt, method);
            if (method.ReturnType != null && method.ReturnType != typeof(void)) CacheManager.retValue = true;
            return new MethodManager(basic, method.ReturnType);
        }

        public static MethodManager CallvirtMethod<T>(this VariableManager basic, String MethodName, params LocalBuilder[] parameters)
        {
            Type type = typeof(T);
            MethodInfo method = type.GetMethod(MethodName, parameters.Select(x => x.LocalType).ToArray());
            basic.Output();
            parameters.ToList().ForEach(x => basic.Emit(OpCodes.Ldloc_S, x));
            basic.Emit(OpCodes.Callvirt, method);
            if (method.ReturnType != null && method.ReturnType != typeof(void)) CacheManager.retValue = true;
            return new MethodManager(basic, method.ReturnType);
        }

        public static MethodManager CallvirtMethod(this VariableManager basic, String MethodName, Type type)
        {
            MethodInfo method = type.GetMethod(MethodName, Type.EmptyTypes);
            basic.Output();
            basic.Emit(OpCodes.Callvirt, method);
            if (method.ReturnType != null && method.ReturnType != typeof(void)) CacheManager.retValue = true;
            return new MethodManager(basic, method.ReturnType);
        }

        public static MethodManager CallvirtMethod(this VariableManager basic, String MethodName, Type type, params LocalBuilder[] parameters)
        {
            MethodInfo method = type.GetMethod(MethodName, parameters.Select(x => x.LocalType).ToArray());
            basic.Output();
            parameters.ToList().ForEach(x => basic.Emit(OpCodes.Ldloc_S, x));
            basic.Emit(OpCodes.Callvirt, method);
            if (method.ReturnType != null && method.ReturnType != typeof(void)) CacheManager.retValue = true;
            return new MethodManager(basic, method.ReturnType);
        }

        public static MethodManager CallMethod(this EmitBasic basic, String MethodName, Type type)
        {
            MethodInfo method = type.GetMethod(MethodName, Type.EmptyTypes);
            basic.Emit(OpCodes.Call, method);
            if (method.ReturnType != null && method.ReturnType != typeof(void)) CacheManager.retValue = true;
            return new MethodManager(basic, method.ReturnType);
        }

        public static MethodManager CallMethod(this EmitBasic basic, String MethodName, Type type, params LocalBuilder[] parameters)
        {
            MethodInfo method = type.GetMethod(MethodName, parameters.Select(x => x.LocalType).ToArray());
            parameters.ToList().ForEach(x => basic.Emit(OpCodes.Ldloc_S, x));
            basic.Emit(OpCodes.Call, method);
            if (method.ReturnType != null && method.ReturnType != typeof(void)) CacheManager.retValue = true;
            return new MethodManager(basic, method.ReturnType);
        }
    }
}
