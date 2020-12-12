using ILWheatBread.SmartEmit.Func;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Reflection.Emit;
using System.Text;
using System.Threading.Tasks;

namespace ILWheatBread.SmartEmit
{
    /// <summary>
    /// 未进入包装的基础码扩展
    /// </summary>
    public static class EmitBasicExtension
    {
        /// <summary>
        /// 函数返回
        /// </summary>
        /// <param name="basic"></param>
        public static void EmitReturn(this EmitBasic basic)
        {
            basic.Emit(OpCodes.Ret);
        }

        /// <summary>
        /// 函数参数加载入堆
        /// </summary>
        /// <param name="basic"></param>
        /// <param name="index"></param>
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

        /// <summary>
        /// 函数参数转成指针对象
        /// </summary>
        /// <param name="basic"></param>
        /// <param name="index">传递的参数索引</param>
        /// <param name="type">参数类型</param>
        /// <returns></returns>
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

        /// <summary>
        /// 抛出异常
        /// </summary>
        /// <param name="basic"></param>
        /// <param name="ex"></param>
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

        /// <summary>
        /// 调用无参非静态函数
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="basic"></param>
        /// <param name="MethodName"></param>
        public static MethodManager CallvirtMethod<T>(this VariableManager basic, String MethodName)
        {
            Type type = typeof(T);
            MethodInfo method = type.GetMethod(MethodName, Type.EmptyTypes);
            basic.Output();
            basic.Emit(OpCodes.Callvirt, method);
            if (method.ReturnType != null && method.ReturnType != typeof(void)) CacheManager.retValue = true;
            return new MethodManager(basic, method.ReturnType);
        }

        /// <summary>
        /// 调用有参非静态函数
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="basic"></param>
        /// <param name="MethodName"></param>
        /// <param name="parameters"></param>
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

        /// <summary>
        /// 调用无参非静态函数
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="basic"></param>
        /// <param name="MethodName"></param>
        public static MethodManager CallvirtMethod(this VariableManager basic, String MethodName, Type type)
        {
            MethodInfo method = type.GetMethod(MethodName, Type.EmptyTypes);
            basic.Output();
            basic.Emit(OpCodes.Callvirt, method);
            if (method.ReturnType != null && method.ReturnType != typeof(void)) CacheManager.retValue = true;
            return new MethodManager(basic, method.ReturnType);
        }

        /// <summary>
        /// 调用有参非静态函数
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="basic"></param>
        /// <param name="MethodName"></param>
        /// <param name="parameters"></param>
        public static MethodManager CallvirtMethod(this VariableManager basic, String MethodName, Type type, params LocalBuilder[] parameters)
        {
            MethodInfo method = type.GetMethod(MethodName, parameters.Select(x => x.LocalType).ToArray());
            basic.Output();
            parameters.ToList().ForEach(x => basic.Emit(OpCodes.Ldloc_S, x));
            basic.Emit(OpCodes.Callvirt, method);
            if (method.ReturnType != null && method.ReturnType != typeof(void)) CacheManager.retValue = true;
            return new MethodManager(basic, method.ReturnType);
        }

        /// <summary>
        /// 调用无参静态函数
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="basic"></param>
        /// <param name="MethodName"></param>
        /// <returns></returns>
        public static MethodManager CallMethod(this EmitBasic basic, String MethodName, Type type)
        {
            MethodInfo method = type.GetMethod(MethodName, Type.EmptyTypes);
            basic.Emit(OpCodes.Call, method);
            if (method.ReturnType != null && method.ReturnType != typeof(void)) CacheManager.retValue = true;
            return new MethodManager(basic, method.ReturnType);
        }

        /// <summary>
        /// 调用有参静态函数
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="basic"></param>
        /// <param name="MethodName"></param>
        /// <param name="parameters"></param>
        /// <returns></returns>
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
