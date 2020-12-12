using ILWheatBread.SmartEmit.Field;
using ILWheatBread;
using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Linq.Expressions;
using System.Reflection;
using System.Reflection.Emit;
using System.Text;
using ILWheatBread.Attributes;

namespace ILWheatBread.SmartEmit
{
    /// <summary>
    /// 代码层优化方案
    /// </summary>
    internal static partial class ManagerGX
    {
        #region 基础类型

        /// <summary>
        /// 核心实现方案(String)
        /// </summary>
        /// <param name="il"></param>
        /// <param name="value"></param>
        /// <returns></returns>
        internal static FieldString NewString(this EmitBasic basic, String value = default(String))
        {
            return new FieldString(NewField(basic, value), basic);
        }

        /// <summary>
        /// 核心实现方案(Boolean)
        /// </summary>
        /// <param name="basic"></param>
        /// <param name="value"></param>
        /// <returns></returns>
        internal static FieldBoolean NewBoolean(this EmitBasic basic, Boolean value = default(Boolean))
        {
            return new FieldBoolean(NewField(basic, value), basic);
        }

        /// <summary>
        /// 核心实现方案(Int32)
        /// </summary>
        /// <param name="basic"></param>
        /// <param name="value"></param>
        /// <returns></returns>
        internal static FieldInt32 NewInt32(this EmitBasic basic, Int32 value = default(Int32))
        {
            return new FieldInt32(NewField(basic, value), basic);
        }

        /// <summary>
        /// 核心实现方案(Int64)
        /// </summary>
        /// <param name="basic"></param>
        /// <param name="value"></param>
        /// <returns></returns>
        internal static FieldInt64 NewInt64(this EmitBasic basic, Int64 value = default(Int64))
        {
            return new FieldInt64(NewField(basic, value), basic);
        }

        /// <summary>
        /// 核心实现方案(Float)
        /// </summary>
        /// <param name="basic"></param>
        /// <param name="value"></param>
        /// <returns></returns>
        internal static FieldFloat NewFloat(this EmitBasic basic, Single value = default(Single))
        {
            return new FieldFloat(NewField(basic, value), basic);
        }

        /// <summary>
        /// 核心实现方案(Double)
        /// </summary>
        /// <param name="basic"></param>
        /// <param name="value"></param>
        /// <returns></returns>
        internal static FieldDouble NewDouble(this EmitBasic basic, Double value = default(Double))
        {
            return new FieldDouble(NewField(basic, value), basic);
        }

        /// <summary>
        /// 核心实现方案(Decimal)
        /// </summary>
        /// <param name="basic"></param>
        /// <param name="value"></param>
        /// <returns></returns>
        internal static FieldDecimal NewDecimal(this EmitBasic basic, Decimal value = default(Decimal))
        {
            return new FieldDecimal(NewField(basic, value), basic);
        }

        /// <summary>
        /// 核心实现方案(DateTime)
        /// </summary>
        /// <param name="basic"></param>
        /// <param name="value"></param>
        /// <returns></returns>
        internal static FieldDateTime NewDateTime(this EmitBasic basic, DateTime value = default(DateTime))
        {
            return new FieldDateTime(NewField(basic, value), basic);
        }

        /// <summary>
        /// 核心实现方案(Object)
        /// </summary>
        /// <param name="basic"></param>
        /// <param name="value"></param>
        /// <returns></returns>
        internal static FieldObject NewObject(this EmitBasic basic, Object value = default(Object))
        {
            return new FieldObject(NewField(basic, value), basic);
        }

        #endregion

        #region 结构化类型

        /// <summary>
        /// 核心实现方案(Entity)
        /// </summary>
        /// <param name="basic"></param>
        /// <param name="value"></param>
        /// <returns></returns>
        internal static FieldEntity<T> NewEntity<T>(this EmitBasic basic) where T : class
        {
            LocalBuilder item = basic.DeclareLocal(typeof(T));
            basic.Emit(OpCodes.Newobj, typeof(T).GetConstructor(Type.EmptyTypes));
            basic.Emit(OpCodes.Stloc_S, item);
            return new FieldEntity<T>(item, basic);
        }

        /// <summary>
        /// 核心实现方案(Array)
        /// </summary>
        /// <param name="basic"></param>
        /// <param name="value"></param>
        /// <returns></returns>
        internal static FieldArray<T> NewArray<T>(this EmitBasic basic, Int32 length = default(Int32))
        {
            LocalBuilder item = basic.DeclareLocal(typeof(T[]));
            basic.Int32Map(length);
            basic.Emit(OpCodes.Newarr, typeof(T));
            basic.Emit(OpCodes.Stloc_S, item);
            return new FieldArray<T>(item, basic, length);
        }

        /// <summary>
        /// 核心实现方案(Array)
        /// </summary>
        /// <param name="basic"></param>
        /// <param name="value"></param>
        /// <returns></returns>
        internal static FieldArray<T> NewArray<T>(this EmitBasic basic, LocalBuilder length)
        {
            LocalBuilder item = basic.DeclareLocal(typeof(T[]));
            basic.Emit(OpCodes.Ldloc_S, length);
            basic.Emit(OpCodes.Newarr, typeof(T));
            basic.Emit(OpCodes.Stloc_S, item);
            return new FieldArray<T>(item, basic, -1);
        }

        /// <summary>
        /// 核心实现方案(List)
        /// </summary>
        /// <param name="basic"></param>
        /// <param name="value"></param>
        /// <returns></returns>
        internal static FieldList<T> NewList<T>(this EmitBasic basic)
        {
            LocalBuilder item = basic.DeclareLocal(typeof(List<T>));
            basic.Emit(OpCodes.Newobj, typeof(List<T>).GetConstructor(Type.EmptyTypes));
            basic.Emit(OpCodes.Stloc_S, item);
            return new FieldList<T>(item, basic);
        }

        #endregion

        /// <summary>
        /// 基础For正循环
        /// </summary>
        /// <param name="basic"></param>
        /// <param name="init">初始值</param>
        /// <param name="length">目标值</param>
        /// <param name="build">回调索引</param>
        internal static void For(this EmitBasic basic, LocalBuilder init, LocalBuilder length, Action<FieldInt32> build)
        {
            Label _for = basic.DefineLabel();
            Label _endfor = basic.DefineLabel();
            LocalBuilder index = basic.DeclareLocal(typeof(Int32));
            basic.Emit(OpCodes.Ldloc_S, init);
            basic.Emit(OpCodes.Stloc_S, index);
            basic.Emit(OpCodes.Br, _endfor);
            basic.MarkLabel(_for);
            build?.Invoke(new FieldInt32(index, basic));
            basic.Emit(OpCodes.Ldloc_S, index);
            basic.Emit(OpCodes.Ldc_I4_1);
            basic.Emit(OpCodes.Add);
            basic.Emit(OpCodes.Stloc_S, index);
            basic.MarkLabel(_endfor);
            basic.Emit(OpCodes.Ldloc_S, index);
            basic.Emit(OpCodes.Ldloc_S, length);
            //basic.Emit(OpCodes.Ldlen);
            basic.Emit(OpCodes.Clt);
            basic.Emit(OpCodes.Brtrue_S, _for);
        }

        /// <summary>
        /// 基础For正循环
        /// </summary>
        /// <param name="basic"></param>
        /// <param name="init">初始值</param>
        /// <param name="length">目标值</param>
        /// <param name="build">回调索引</param>
        internal static void For(this EmitBasic basic, Int32 init, LocalBuilder length, Action<FieldInt32> build)
        {
            Label _for = basic.DefineLabel();
            Label _endfor = basic.DefineLabel();
            LocalBuilder index = basic.DeclareLocal(typeof(Int32));
            basic.Int32Map(init);
            basic.Emit(OpCodes.Stloc_S, index);
            basic.Emit(OpCodes.Br, _endfor);
            basic.MarkLabel(_for);
            build?.Invoke(new FieldInt32(index, basic));
            basic.Emit(OpCodes.Ldloc_S, index);
            basic.Emit(OpCodes.Ldc_I4_1);
            basic.Emit(OpCodes.Add);
            basic.Emit(OpCodes.Stloc_S, index);
            basic.MarkLabel(_endfor);
            basic.Emit(OpCodes.Ldloc_S, index);
            basic.Emit(OpCodes.Ldloc_S, length);
            //basic.Emit(OpCodes.Ldlen);
            basic.Emit(OpCodes.Clt);
            basic.Emit(OpCodes.Brtrue_S, _for);
        }

        /// <summary>
        /// 获取属性结构
        /// </summary>
        /// <param name="Props"></param>
        /// <param name="Instance"></param>
        /// <returns></returns>
        internal static IEnumerable<KeyValuePair<String, FastProperty>> GetProps(PropertyInfo[] Props, Object Instance)
        {
            foreach (var Prop in Props)
            {
                yield return new KeyValuePair<String, FastProperty>(Prop.Name, new FastProperty(Prop, Instance));
            }
        }


        #region 常用方法

        /// <summary>
        /// 抛异常方案
        /// </summary>
        /// <param name="Message"></param>
        internal static void ShowEx(String Message)
        {
            throw new Exception(Message);
        }

        #endregion

        #region 私有方法

        /// <summary>
        /// 初始化基础类型
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="basic"></param>
        /// <param name="value"></param>
        /// <returns></returns>
        static LocalBuilder NewField<T>(EmitBasic basic, T value)
        {
            LocalBuilder item = basic.DeclareLocal(typeof(T));
            basic.EmitValue(value);
            basic.Emit(OpCodes.Stloc_S, item);
            return item;
        }

        /// <summary>
        /// 实体转换成变量管理方案
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="il"></param>
        /// <param name="Entity"></param>
        /// <returns></returns>
        static LocalBuilder MapToEntity<T>(this EmitBasic basic, T Entity)
        {
            if (Entity == null) ManagerGX.ShowEx("entity is not null!");
            var type = typeof(T);
            var ctor = type.GetConstructor(Type.EmptyTypes);
            if (!ctor.IsPublic) ManagerGX.ShowEx("type need ctor public!");
            var name = type.FullName;
            LocalBuilder model = basic.DeclareLocal(type);
            basic.Emit(OpCodes.Newobj, ctor);
            basic.Emit(OpCodes.Stloc, model);

            FastProperty[] emits = type.CachePropsManager();

            for (int i = 0; i < emits.Length; i++)
            {
                var propValue = emits[i].Get(Entity);
                if (propValue == null) continue;
                basic.Emit(OpCodes.Ldloc, model);
                basic.EmitValue(propValue, emits[i].PropertyType);
                basic.Emit(OpCodes.Callvirt, emits[i].SetMethod);
            }
            return model;
        }

        /// <summary>
        /// 实体转换成变量管理方案
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="il"></param>
        /// <param name="Entity"></param>
        /// <returns></returns>
        static LocalBuilder MapToEntity(this EmitBasic basic, Object instance, Type type)
        {
            if (instance == null) ManagerGX.ShowEx("entity is not null!");
            var ctor = type.GetConstructor(Type.EmptyTypes);
            if (!ctor.IsPublic) ManagerGX.ShowEx("type need ctor public!");
            var name = type.FullName;
            LocalBuilder model = basic.DeclareLocal(type);
            basic.Emit(OpCodes.Newobj, type);
            basic.Emit(OpCodes.Stloc, model);

            FastProperty[] emits = type.CachePropsManager();

            for (int i = 0; i < emits.Length; i++)
            {
                var propValue = emits[i].Get(instance);
                if (propValue == null) continue;
                basic.Emit(OpCodes.Ldloc, model);
                basic.EmitValue(propValue, emits[i].PropertyType);
                basic.Emit(OpCodes.Callvirt, emits[i].SetMethod);
            }
            return model;
        }

        #endregion
    }
}
