﻿using ILWheatBread.SmartEmit.Field;
using ILWheatBread.SmartEmit.Func;
using System;
using System.Linq;
using System.Reflection;
using System.Reflection.Emit;
using System.Runtime.CompilerServices;

namespace ILWheatBread.SmartEmit
{
    public static class EmitBasicExtension
    {
        public static FieldNullable<T> AsNullable<T>(this FieldManager<T> field) where T : struct
        {
            return new FieldNullable<T>(field, field.generator);
        }

        public static void Argument(this EmitBasic basic, Int32 index)
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


        public static LocalBuilder ArgumentRef<T>(this EmitBasic basic, Int32 index) where T : class
        {
            LocalBuilder param = basic.DeclareLocal(typeof(T));
            basic.Argument(index);
            basic.Emit(OpCodes.Stloc_S, param);
            return param;
        }


        public static LocalBuilder ArgumentRef(this EmitBasic basic, Int32 index, Type type)
        {
            LocalBuilder param = basic.DeclareLocal(type);
            basic.Argument(index);
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


        public static MethodManager ReflectMethod<T>(this VariableManager basic, String MethodName)
        {
            Type type = typeof(T);
            MethodInfo method = type.GetMethod(MethodName, Type.EmptyTypes);
            basic.Output();
            basic.Emit(OpCodes.Callvirt, method);
            if (method.ReturnType != null && method.ReturnType != typeof(void)) CacheManager.retValue = true;
            return new MethodManager(basic, method.ReturnType);
        }


        public static MethodManager ReflectMethod<T>(this VariableManager basic, String MethodName, params LocalBuilder[] parameters)
        {
            Type type = typeof(T);
            MethodInfo method = type.GetMethod(MethodName, parameters.Select(x => x.LocalType).ToArray());
            basic.Output();
            parameters.ToList().ForEach(x => basic.Emit(OpCodes.Ldloc_S, x));
            basic.Emit(OpCodes.Callvirt, method);
            if (method.ReturnType != null && method.ReturnType != typeof(void)) CacheManager.retValue = true;
            return new MethodManager(basic, method.ReturnType);
        }


        public static MethodManager ReflectMethod(this VariableManager basic, String MethodName, Type type)
        {
            MethodInfo method = type.GetMethod(MethodName, Type.EmptyTypes);
            basic.Output();
            basic.Emit(OpCodes.Callvirt, method);
            if (method.ReturnType != null && method.ReturnType != typeof(void)) CacheManager.retValue = true;
            return new MethodManager(basic, method.ReturnType);
        }


        public static MethodManager ReflectMethod(this VariableManager basic, String MethodName, Type type, params LocalBuilder[] parameters)
        {
            MethodInfo method = type.GetMethod(MethodName, parameters.Select(x => x.LocalType).ToArray());
            if (method == null) throw new MethodAccessException("Not exists this method!");
            basic.Output();
            parameters.ToList().ForEach(x => basic.Emit(OpCodes.Ldloc_S, x));
            basic.Emit(OpCodes.Callvirt, method);
            if (method.ReturnType != null && method.ReturnType != typeof(void)) CacheManager.retValue = true;
            return new MethodManager(basic, method.ReturnType);
        }


        public static MethodManager ReflectStaticMethod(this EmitBasic basic, String MethodName, Type type)
        {
            MethodInfo method = type.GetMethod(MethodName, Type.EmptyTypes);
            basic.Emit(OpCodes.Call, method);
            if (method.ReturnType != null && method.ReturnType != typeof(void)) CacheManager.retValue = true;
            return new MethodManager(basic, method.ReturnType);
        }


        public static MethodManager ReflectStaticMethod(this EmitBasic basic, String MethodName, Type type, params LocalBuilder[] parameters)
        {
            MethodInfo method = type.GetMethod(MethodName, parameters.Select(x => x.LocalType).ToArray());
            parameters.ToList().ForEach(x => basic.Emit(OpCodes.Ldloc_S, x));
            basic.Emit(OpCodes.Call, method);
            if (method.ReturnType != null && method.ReturnType != typeof(void)) CacheManager.retValue = true;
            return new MethodManager(basic, method.ReturnType);
        }
    }
}
