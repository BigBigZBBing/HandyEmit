using HandyEmit.SmartEmit.Field;
using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Linq.Expressions;
using System.Reflection;
using System.Reflection.Emit;
using System.Text;

namespace HandyEmit.SmartEmit
{
    /// <summary>
    /// 代码层优化方案
    /// </summary>
    public static class ManagerGX
    {

        /// <summary>
        /// 异常缓存
        /// </summary>
        internal static Exception ex = new Exception();

        /// <summary>
        /// 根据泛型去自动适配推入计算堆的方式
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="il"></param>
        /// <param name="value"></param>
        internal static void EmitValue<T>(this ILGenerator il, T value)
        {
            if (typeof(T) == typeof(String))
            {
                il.Emit(OpCodes.Ldstr, Convert.ToString(value));
            }
            else if (typeof(T) == typeof(Boolean))
            {
                switch (Convert.ToBoolean(value))
                {
                    case true: il.Emit(OpCodes.Ldc_I4_1); break;
                    case false: il.Emit(OpCodes.Ldc_I4_0); break;
                    default: throw new Exception("boolean to error!");
                }
            }
            else if (typeof(T) == typeof(SByte))
            {
                il.Int32Map(Convert.ToSByte(value));
            }
            else if (typeof(T) == typeof(Byte))
            {
                il.Int32Map(Convert.ToByte(value));
            }
            else if (typeof(T) == typeof(Int16))
            {
                il.Int32Map(Convert.ToInt16(value));
            }
            else if (typeof(T) == typeof(Int32))
            {
                il.Int32Map(Convert.ToInt32(value));
            }
            else if (typeof(T) == typeof(Int64))
            {
                il.Emit(OpCodes.Ldc_I8, Convert.ToInt64(value));
            }
            else if (typeof(T) == typeof(Single))
            {
                il.Emit(OpCodes.Ldc_R4, Convert.ToSingle(value));
            }
            else if (typeof(T) == typeof(Double))
            {
                il.Emit(OpCodes.Ldc_R8, Convert.ToDouble(value));
            }
            else if (typeof(T) == typeof(Decimal))
            {
                Int32[] bits = Decimal.GetBits(Convert.ToDecimal(value));
                il.Emit(OpCodes.Ldc_I4, bits[0]);
                il.Emit(OpCodes.Ldc_I4, bits[1]);
                il.Emit(OpCodes.Ldc_I4, bits[2]);
                il.Emit((bits[3] & 0x80000000) != 0 ? OpCodes.Ldc_I4_1 : OpCodes.Ldc_I4_0);
                il.Emit(OpCodes.Ldc_I4, (Byte)((bits[3] >> 16) & 0x7f));
            }
            else if (typeof(T).CustomAttributes.FirstOrDefault(x => x.AttributeType == typeof(EmitSerialization)) != null)
            {
                il.Emit(OpCodes.Ldloc, il.MapToEntity(value));
            }
            else
            {
                throw new Exception("not exist datatype!");
            }
        }

        /// <summary>
        /// 根据类型去拆箱推入计算堆的方式
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="il"></param>
        /// <param name="value"></param>
        internal static void EmitValue(this ILGenerator il, Object value, Type type)
        {
            if (type == typeof(String))
            {
                il.Emit(OpCodes.Ldstr, Convert.ToString(value));
            }
            else if (type == typeof(Boolean))
            {
                switch (Convert.ToBoolean(value))
                {
                    case true: il.Emit(OpCodes.Ldc_I4_1); break;
                    case false: il.Emit(OpCodes.Ldc_I4_0); break;
                    default: throw new Exception("boolean to error!");
                }
            }
            else if (type == typeof(SByte))
            {
                il.Int32Map(Convert.ToSByte(value));
            }
            else if (type == typeof(Byte))
            {
                il.Int32Map(Convert.ToByte(value));
            }
            else if (type == typeof(Int16))
            {
                il.Int32Map(Convert.ToInt16(value));
            }
            else if (type == typeof(Int32))
            {
                il.Int32Map(Convert.ToInt32(value));
            }
            else if (type == typeof(Int64))
            {
                il.Emit(OpCodes.Ldc_I8, Convert.ToInt64(value));
            }
            else if (type == typeof(Single))
            {
                il.Emit(OpCodes.Ldc_R4, Convert.ToSingle(value));
            }
            else if (type == typeof(Double))
            {
                il.Emit(OpCodes.Ldc_R8, Convert.ToDouble(value));
            }
            else if (type == typeof(Decimal))
            {
                Int32[] bits = Decimal.GetBits(Convert.ToDecimal(value));
                il.Emit(OpCodes.Ldc_I4, bits[0]);
                il.Emit(OpCodes.Ldc_I4, bits[1]);
                il.Emit(OpCodes.Ldc_I4, bits[2]);
                il.Emit((bits[3] & 0x80000000) != 0 ? OpCodes.Ldc_I4_1 : OpCodes.Ldc_I4_0);
                il.Emit(OpCodes.Ldc_I4, (Byte)((bits[3] >> 16) & 0x7f));
            }
            else if (type.CustomAttributes.FirstOrDefault(x => x.AttributeType == typeof(EmitSerialization)) != null)
            {
                il.Emit(OpCodes.Ldloc, il.MapToEntity(value, type));
            }
            else
            {
                throw new Exception("not exist datatype!");
            }
        }

        /// <summary>
        /// 实体转换成变量管理方案
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="il"></param>
        /// <param name="Entity"></param>
        /// <returns></returns>
        internal static LocalBuilder MapToEntity<T>(this ILGenerator il, T Entity)
        {
            if (Entity == null) ManagerGX.GxException("entity is not null!");
            var type = typeof(T);
            var ctor = type.GetConstructor(Type.EmptyTypes);
            if (!ctor.IsPublic) ManagerGX.GxException("type need ctor public!");
            var name = type.FullName;
            LocalBuilder model = il.DeclareLocal(type);
            il.Emit(OpCodes.Newobj, ctor);
            il.Emit(OpCodes.Stloc, model);

            EmitProperty[] emits = type.CachePropsManage();

            for (int i = 0; i < emits.Length; i++)
            {
                var propValue = emits[i].Get(Entity);
                if (propValue == null) continue;
                il.Emit(OpCodes.Ldloc, model);
                il.EmitValue(propValue, emits[i].PropertyType);
                il.Emit(OpCodes.Callvirt, emits[i].SetMethod);
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
        internal static LocalBuilder MapToEntity(this ILGenerator il, Object instance, Type type)
        {
            if (instance == null) ManagerGX.GxException("entity is not null!");
            var ctor = type.GetConstructor(Type.EmptyTypes);
            if (!ctor.IsPublic) ManagerGX.GxException("type need ctor public!");
            var name = type.FullName;
            LocalBuilder model = il.DeclareLocal(type);
            il.Emit(OpCodes.Newobj, type);
            il.Emit(OpCodes.Stloc, model);

            EmitProperty[] emits = type.CachePropsManage();

            for (int i = 0; i < emits.Length; i++)
            {
                var propValue = emits[i].Get(instance);
                if (propValue == null) continue;
                il.Emit(OpCodes.Ldloc, model);
                il.EmitValue(propValue, emits[i].PropertyType);
                il.Emit(OpCodes.Callvirt, emits[i].SetMethod);
            }
            return model;
        }

        /// <summary>
        /// Int32自动适配指令码
        /// </summary>
        /// <param name="il"></param>
        /// <param name="value"></param>
        internal static void Int32Map(this ILGenerator il, Int32 value)
        {
            switch (value)
            {
                case -1: il.Emit(OpCodes.Ldc_I4_M1); break;
                case 0: il.Emit(OpCodes.Ldc_I4_0); break;
                case 1: il.Emit(OpCodes.Ldc_I4_1); break;
                case 2: il.Emit(OpCodes.Ldc_I4_2); break;
                case 3: il.Emit(OpCodes.Ldc_I4_3); break;
                case 4: il.Emit(OpCodes.Ldc_I4_4); break;
                case 5: il.Emit(OpCodes.Ldc_I4_5); break;
                case 6: il.Emit(OpCodes.Ldc_I4_6); break;
                case 7: il.Emit(OpCodes.Ldc_I4_7); break;
                case 8: il.Emit(OpCodes.Ldc_I4_8); break;
                default: il.Emit(OpCodes.Ldc_I4, value); break;
            }
        }

        /// <summary>
        /// 比较器
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="field"></param>
        /// <param name="value"></param>
        /// <param name="code"></param>
        /// <returns></returns>
        internal static FieldBoolean Comparer<T>(FieldManager<T> field, T value, params OpCode[] codes)
        {
            var res = field.il.NewBoolean();
            field.PushLd();
            foreach (var code in codes)
            {
                field.il.EmitValue(value);
                field.il.Emit(code);
            }
            field.il.Emit(OpCodes.Stloc_S, res);
            return res;
        }

        /// <summary>
        /// 比较器
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="field"></param>
        /// <param name="value"></param>
        /// <param name="code"></param>
        /// <returns></returns>
        internal static FieldBoolean Comparer<T, T1>(CanCompute<T> field, T1 value, params OpCode[] codes)
        {
            var res = field.il.NewBoolean();
            field.PushLd();
            foreach (var code in codes)
            {
                field.il.EmitValue(value);
                field.il.Emit(code);
            }
            field.il.Emit(OpCodes.Stloc_S, res);
            return res;
        }

        /// <summary>
        /// 比较器
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="field"></param>
        /// <param name="value"></param>
        /// <param name="code"></param>
        /// <returns></returns>
        internal static FieldBoolean Comparer<T>(FieldManager<T> field, LocalBuilder value, params OpCode[] codes)
        {
            var res = field.il.NewBoolean();
            field.PushLd();
            foreach (var code in codes)
            {
                field.il.Emit(OpCodes.Ldloc_S, value);
                field.il.Emit(code);
            }
            field.il.Emit(OpCodes.Stloc_S, res);
            return res;
        }

        /// <summary>
        /// 比较器
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="field"></param>
        /// <param name="value"></param>
        /// <param name="code"></param>
        /// <returns></returns>
        internal static FieldBoolean Comparer<T, T1>(FieldManager<T> field, FieldManager<T1> value, params OpCode[] codes)
        {
            var res = field.il.NewBoolean();
            field.PushLd();
            foreach (var code in codes)
            {
                value.PushLd();
                field.il.Emit(code);
            }
            field.il.Emit(OpCodes.Stloc_S, res);
            return res;
        }

        /// <summary>
        /// 计算器
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="field"></param>
        /// <param name="value"></param>
        /// <param name="code"></param>
        /// <returns></returns>
        internal static FieldManager<T> Compute<T, T1>(FieldManager<T> field, T1 value, OpCode code)
        {
            field.PushLd();
            field.il.EmitValue(value);
            field.il.Emit(code);
            field.PushSt();
            return field;
        }

        /// <summary>
        /// 计算器
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="field"></param>
        /// <param name="value"></param>
        /// <param name="code"></param>
        /// <returns></returns>
        internal static FieldManager<T1> Compute<T, T1>(FieldManager<T> field, LocalBuilder value, OpCode code)
        {
            field.PushLd();
            field.il.Emit(OpCodes.Ldloc_S, value);
            field.il.Emit(code);
            field.PushSt();
            return field as FieldManager<T1>;
        }

        /// <summary>
        /// 计算器
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="field"></param>
        /// <param name="value"></param>
        /// <param name="code"></param>
        /// <returns></returns>
        internal static FieldManager<T> Compute<T, T1>(FieldManager<T> field, FieldManager<T1> value, OpCode code)
        {
            field.PushLd();
            value.PushLd();
            field.il.Emit(code);
            field.PushSt();
            return field;
        }

        /// <summary>
        /// 内部渗透方案(String)
        /// </summary>
        /// <param name="il"></param>
        /// <param name="value"></param>
        /// <returns></returns>
        internal static FieldString NewString(this ILGenerator il, String value = default(String))
        {
            LocalBuilder item = il.DeclareLocal(typeof(String));
            il.Emit(value == null ? OpCodes.Ldnull : OpCodes.Ldstr, value);
            il.Emit(OpCodes.Stloc_S, item);
            return new FieldString(item, il);
        }

        /// <summary>
        /// 内部渗透方案(Boolean)
        /// </summary>
        /// <param name="il"></param>
        /// <param name="value"></param>
        /// <returns></returns>
        internal static FieldBoolean NewBoolean(this ILGenerator il, Boolean value = default(Boolean))
        {
            LocalBuilder item = il.DeclareLocal(typeof(Boolean));
            il.EmitValue(value);
            il.Emit(OpCodes.Stloc_S, item);
            return new FieldBoolean(item, il);
        }

        /// <summary>
        /// 内部渗透方案(Int32)
        /// </summary>
        /// <param name="il"></param>
        /// <param name="value"></param>
        /// <returns></returns>
        internal static FieldInt32 NewInt32(this ILGenerator il, Int32 value = default(Int32))
        {
            LocalBuilder item = il.DeclareLocal(typeof(Int32));
            il.EmitValue(value);
            il.Emit(OpCodes.Stloc_S, item);
            return new FieldInt32(item, il);
        }

        /// <summary>
        /// 内部渗透方案(Int64)
        /// </summary>
        /// <param name="il"></param>
        /// <param name="value"></param>
        /// <returns></returns>
        internal static FieldInt64 NewInt64(this ILGenerator il, Int64 value = default(Int64))
        {
            LocalBuilder item = il.DeclareLocal(typeof(Int64));
            il.Emit(OpCodes.Ldc_I8, value);
            il.Emit(OpCodes.Stloc_S, item);
            return new FieldInt64(item, il);
        }

        /// <summary>
        /// 内部渗透方案(Float)
        /// </summary>
        /// <param name="il"></param>
        /// <param name="value"></param>
        /// <returns></returns>
        internal static FieldFloat NewFloat(this ILGenerator il, Single value = default(Single))
        {
            LocalBuilder item = il.DeclareLocal(typeof(Single));
            il.Emit(OpCodes.Ldc_R4, value);
            il.Emit(OpCodes.Stloc_S, item);
            return new FieldFloat(item, il);
        }

        /// <summary>
        /// 内部渗透方案(Double)
        /// </summary>
        /// <param name="il"></param>
        /// <param name="value"></param>
        /// <returns></returns>
        internal static FieldDouble NewDouble(this ILGenerator il, Double value = default(Double))
        {
            LocalBuilder item = il.DeclareLocal(typeof(Double));
            il.Emit(OpCodes.Ldc_R8, value);
            il.Emit(OpCodes.Stloc_S, item);
            return new FieldDouble(item, il);
        }

        /// <summary>
        /// 内部渗透方案(Decimal)
        /// </summary>
        /// <param name="il"></param>
        /// <param name="value"></param>
        /// <returns></returns>
        internal static FieldDecimal NewDecimal(this ILGenerator il, Double value = default(Double))
        {
            LocalBuilder item = il.DeclareLocal(typeof(Decimal));
            il.Emit(OpCodes.Ldc_R8, value);
            il.Emit(OpCodes.Newobj, typeof(Decimal).GetConstructor(new Type[] { typeof(Double) }));
            il.Emit(OpCodes.Stloc_S, item);
            return new FieldDecimal(item, il);
        }

        /// <summary>
        /// 内部渗透方案(Array)
        /// </summary>
        /// <param name="il"></param>
        /// <param name="value"></param>
        /// <returns></returns>
        internal static ArrayManager<T> NewArray<T>(this ILGenerator il, Int32 length = default(Int32))
        {
            LocalBuilder item = il.DeclareLocal(typeof(T[]));
            il.Emit(OpCodes.Ldc_I4, length);
            il.Emit(OpCodes.Newarr, typeof(T));
            il.Emit(OpCodes.Stloc_S, item);
            return new ArrayManager<T>(item, il, length);
        }

        /// <summary>
        /// 内部渗透方案(List)
        /// </summary>
        /// <param name="il"></param>
        /// <param name="value"></param>
        /// <returns></returns>
        internal static ListManager<T> NewList<T>(this ILGenerator il)
        {
            LocalBuilder item = il.DeclareLocal(typeof(List<T>));
            il.Emit(OpCodes.Newobj, typeof(List<T>));
            il.Emit(OpCodes.Stloc_S, item);
            return new ListManager<T>(item, il);
        }

        /// <summary>
        /// 获取属性结构
        /// </summary>
        /// <param name="Props"></param>
        /// <param name="Instance"></param>
        /// <returns></returns>
        internal static IEnumerable<KeyValuePair<String, EmitProperty>> GetProps(PropertyInfo[] Props, Object Instance)
        {
            foreach (var Prop in Props)
            {
                yield return new KeyValuePair<String, EmitProperty>(Prop.Name, new EmitProperty(Prop, Instance));
            }
        }

        /// <summary>
        /// 抛异常方案
        /// </summary>
        /// <param name="Message"></param>
        internal static void GxException(String Message)
        {
            throw new Exception(Message);
        }
    }
}
