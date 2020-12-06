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
using ILWheatBread.SmartEmit.Mapping;

namespace ILWheatBread.SmartEmit
{
    /// <summary>
    /// 代码层优化方案
    /// </summary>
    public static class ManagerGX
    {
        /// <summary>
        /// 根据泛型去自动适配推入计算堆的方式
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="basic"></param>
        /// <param name="value"></param>
        internal static void EmitValue<T>(this EmitBasic basic, T value)
        {
            if (typeof(T) == typeof(String))
            {
                basic.Emit(OpCodes.Ldstr, Convert.ToString(value));
            }
            else if (typeof(T) == typeof(Boolean))
            {
                switch (Convert.ToBoolean(value))
                {
                    case true: basic.Emit(OpCodes.Ldc_I4_1); break;
                    case false: basic.Emit(OpCodes.Ldc_I4_0); break;
                    default: throw new Exception("boolean to error!");
                }
            }
            else if (typeof(T) == typeof(SByte))
            {
                basic.Int32Map(Convert.ToSByte(value));
            }
            else if (typeof(T) == typeof(Byte))
            {
                basic.Int32Map(Convert.ToByte(value));
            }
            else if (typeof(T) == typeof(Int16))
            {
                basic.Int32Map(Convert.ToInt16(value));
            }
            else if (typeof(T) == typeof(UInt16))
            {
                basic.Int32Map(Convert.ToUInt16(value));
            }
            else if (typeof(T) == typeof(Int32))
            {
                basic.Int32Map(Convert.ToInt32(value));
            }
            else if (typeof(T) == typeof(UInt32))
            {
                basic.Int32Map((int)Convert.ToUInt32(value));
            }
            else if (typeof(T) == typeof(Int64))
            {
                basic.Emit(OpCodes.Ldc_I8, Convert.ToInt64(value));
            }
            else if (typeof(T) == typeof(UInt64))
            {
                basic.Emit(OpCodes.Ldc_I8, Convert.ToUInt64(value));
            }
            else if (typeof(T) == typeof(Single))
            {
                basic.Emit(OpCodes.Ldc_R4, Convert.ToSingle(value));
            }
            else if (typeof(T) == typeof(Double))
            {
                basic.Emit(OpCodes.Ldc_R8, Convert.ToDouble(value));
            }
            else if (typeof(T) == typeof(Decimal))
            {
                Int32[] bits = Decimal.GetBits(Convert.ToDecimal(value));
                basic.Int32Map(bits[0]);
                basic.Int32Map(bits[1]);
                basic.Int32Map(bits[2]);
                basic.Emit((bits[3] & 0x80000000) != 0 ? OpCodes.Ldc_I4_1 : OpCodes.Ldc_I4_0);
                basic.Int32Map((bits[3] >> 16) & 0x7f);
                basic.Emit(OpCodes.Newobj, BaseConst._DecimalCtor);
                //Int32[] bits = Decimal.GetBits(Convert.ToDecimal(value));
                //basic.Emit(OpCodes.Ldc_I4, bits[0]);
                //basic.Emit(OpCodes.Ldc_I4, bits[1]);
                //basic.Emit(OpCodes.Ldc_I4, bits[2]);
                //basic.Emit((bits[3] & 0x80000000) != 0 ? OpCodes.Ldc_I4_1 : OpCodes.Ldc_I4_0);
                //basic.Emit(OpCodes.Ldc_I4, (Byte)((bits[3] >> 16) & 0x7f));
            }
            else if (typeof(T).CustomAttributes.FirstOrDefault(x => x.AttributeType == typeof(EmitSerialization)) != null)
            {
                basic.Emit(OpCodes.Ldloc, basic.MapToEntity(value));
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
        /// <param name="basic"></param>
        /// <param name="value"></param>
        internal static void EmitValue(this EmitBasic basic, Object value, Type type)
        {
            if (type == typeof(String))
            {
                basic.Emit(OpCodes.Ldstr, Convert.ToString(value));
            }
            else if (type == typeof(Boolean))
            {
                switch (Convert.ToBoolean(value))
                {
                    case true: basic.Emit(OpCodes.Ldc_I4_1); break;
                    case false: basic.Emit(OpCodes.Ldc_I4_0); break;
                    default: throw new Exception("boolean to error!");
                }
            }
            else if (type == typeof(SByte))
            {
                basic.Int32Map(Convert.ToSByte(value));
            }
            else if (type == typeof(Byte))
            {
                basic.Int32Map(Convert.ToByte(value));
            }
            else if (type == typeof(Int16))
            {
                basic.Int32Map(Convert.ToInt16(value));
            }
            else if (type == typeof(UInt16))
            {
                basic.Int32Map(Convert.ToUInt16(value));
            }
            else if (type == typeof(Int32))
            {
                basic.Int32Map(Convert.ToInt32(value));
            }
            else if (type == typeof(UInt32))
            {
                basic.Int32Map((int)Convert.ToUInt32(value));
            }
            else if (type == typeof(Int64))
            {
                basic.Emit(OpCodes.Ldc_I8, Convert.ToInt64(value));
            }
            else if (type == typeof(UInt64))
            {
                basic.Emit(OpCodes.Ldc_I8, Convert.ToUInt64(value));
            }
            else if (type == typeof(Single))
            {
                basic.Emit(OpCodes.Ldc_R4, Convert.ToSingle(value));
            }
            else if (type == typeof(Double))
            {
                basic.Emit(OpCodes.Ldc_R8, Convert.ToDouble(value));
            }
            else if (type == typeof(Decimal))
            {
                Int32[] bits = Decimal.GetBits(Convert.ToDecimal(value));
                basic.Int32Map(bits[0]);
                basic.Int32Map(bits[1]);
                basic.Int32Map(bits[2]);
                basic.Emit((bits[3] & 0x80000000) != 0 ? OpCodes.Ldc_I4_1 : OpCodes.Ldc_I4_0);
                basic.Int32Map((bits[3] >> 16) & 0x7f);
                basic.Emit(OpCodes.Newobj, BaseConst._DecimalCtor);
                //Int32[] bits = Decimal.GetBits(Convert.ToDecimal(value));
                //basic.Emit(OpCodes.Ldc_I4, bits[0]);
                //basic.Emit(OpCodes.Ldc_I4, bits[1]);
                //basic.Emit(OpCodes.Ldc_I4, bits[2]);
                //basic.Emit((bits[3] & 0x80000000) != 0 ? OpCodes.Ldc_I4_1 : OpCodes.Ldc_I4_0);
                //basic.Emit(OpCodes.Ldc_I4, (Byte)((bits[3] >> 16) & 0x7f));
            }
            else if (type.CustomAttributes.FirstOrDefault(x => x.AttributeType == typeof(EmitSerialization)) != null)
            {
                basic.Emit(OpCodes.Ldloc, basic.MapToEntity(value, type));
            }
            else
            {
                throw new Exception("not exist datatype!");
            }
        }

        /// <summary>
        /// 根据类型适配弹出数组单元指令
        /// </summary>
        /// <param name="basic"></param>
        /// <param name="type"></param>
        internal static void PopArray(this EmitBasic basic, Type type)
        {
            if (type == typeof(String))
            {
                basic.Emit(OpCodes.Ldelem_Ref);
            }
            else if (type == typeof(Boolean))
            {
                basic.Emit(OpCodes.Ldelem_I1);
            }
            else if (type == typeof(SByte))
            {
                basic.Emit(OpCodes.Ldelem_I1);
            }
            else if (type == typeof(Byte))
            {
                basic.Emit(OpCodes.Ldelem_I1);
            }
            else if (type == typeof(Int16))
            {
                basic.Emit(OpCodes.Ldelem_I2);
            }
            else if (type == typeof(UInt16))
            {
                basic.Emit(OpCodes.Ldelem_I2);
            }
            else if (type == typeof(Int32))
            {
                basic.Emit(OpCodes.Ldelem_I4);
            }
            else if (type == typeof(UInt32))
            {
                basic.Emit(OpCodes.Ldelem_I4);
            }
            else if (type == typeof(Int64))
            {
                basic.Emit(OpCodes.Ldelem_I8);
            }
            else if (type == typeof(UInt64))
            {
                basic.Emit(OpCodes.Ldelem_I8);
            }
            else if (type == typeof(Single))
            {
                basic.Emit(OpCodes.Ldelem_R4);
            }
            else if (type == typeof(Double))
            {
                basic.Emit(OpCodes.Ldelem_R8);
            }
            else if (type == typeof(Decimal))
            {
                basic.Emit(OpCodes.Ldelem);
            }
            else if (type.CustomAttributes.FirstOrDefault(x => x.AttributeType == typeof(EmitSerialization)) != null)
            {
                basic.Emit(OpCodes.Ldelem);
            }
            else
            {
                throw new Exception("not exist datatype!");
            }
        }

        /// <summary>
        /// 根据类型适配推入数组单元指令
        /// </summary>
        /// <param name="basic"></param>
        /// <param name="type"></param>
        internal static void PushArray(this EmitBasic basic, Type type)
        {
            if (type == typeof(String))
            {
                basic.Emit(OpCodes.Stelem_Ref);
            }
            else if (type == typeof(Boolean))
            {
                basic.Emit(OpCodes.Stelem_I1);
            }
            else if (type == typeof(SByte))
            {
                basic.Emit(OpCodes.Stelem_I1);
            }
            else if (type == typeof(Byte))
            {
                basic.Emit(OpCodes.Stelem_I1);
            }
            else if (type == typeof(Int16))
            {
                basic.Emit(OpCodes.Stelem_I2);
            }
            else if (type == typeof(UInt16))
            {
                basic.Emit(OpCodes.Stelem_I2);
            }
            else if (type == typeof(Int32))
            {
                basic.Emit(OpCodes.Stelem_I4);
            }
            else if (type == typeof(UInt32))
            {
                basic.Emit(OpCodes.Stelem_I4);
            }
            else if (type == typeof(Int64))
            {
                basic.Emit(OpCodes.Stelem_I8);
            }
            else if (type == typeof(UInt64))
            {
                basic.Emit(OpCodes.Stelem_I8);
            }
            else if (type == typeof(Single))
            {
                basic.Emit(OpCodes.Stelem_R4);
            }
            else if (type == typeof(Double))
            {
                basic.Emit(OpCodes.Stelem_R8);
            }
            else if (type == typeof(Decimal))
            {
                basic.Emit(OpCodes.Stelem);
            }
            else if (type.CustomAttributes.FirstOrDefault(x => x.AttributeType == typeof(EmitSerialization)) != null)
            {
                basic.Emit(OpCodes.Stelem);
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
        internal static LocalBuilder MapToEntity<T>(this EmitBasic basic, T Entity)
        {
            if (Entity == null) ManagerGX.GxException("entity is not null!");
            var type = typeof(T);
            var ctor = type.GetConstructor(Type.EmptyTypes);
            if (!ctor.IsPublic) ManagerGX.GxException("type need ctor public!");
            var name = type.FullName;
            LocalBuilder model = basic.DeclareLocal(type);
            basic.Emit(OpCodes.Newobj, ctor);
            basic.Emit(OpCodes.Stloc, model);

            EmitProperty[] emits = type.CachePropsManager();

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
        internal static LocalBuilder MapToEntity(this EmitBasic basic, Object instance, Type type)
        {
            if (instance == null) ManagerGX.GxException("entity is not null!");
            var ctor = type.GetConstructor(Type.EmptyTypes);
            if (!ctor.IsPublic) ManagerGX.GxException("type need ctor public!");
            var name = type.FullName;
            LocalBuilder model = basic.DeclareLocal(type);
            basic.Emit(OpCodes.Newobj, type);
            basic.Emit(OpCodes.Stloc, model);

            EmitProperty[] emits = type.CachePropsManager();

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

        /// <summary>
        /// Int32自动适配指令码
        /// </summary>
        /// <param name="il"></param>
        /// <param name="value"></param>
        internal static void Int32Map(this EmitBasic basic, Int32 value)
        {
            switch (value)
            {
                case -1: basic.Emit(OpCodes.Ldc_I4_M1); break;
                case 0: basic.Emit(OpCodes.Ldc_I4_0); break;
                case 1: basic.Emit(OpCodes.Ldc_I4_1); break;
                case 2: basic.Emit(OpCodes.Ldc_I4_2); break;
                case 3: basic.Emit(OpCodes.Ldc_I4_3); break;
                case 4: basic.Emit(OpCodes.Ldc_I4_4); break;
                case 5: basic.Emit(OpCodes.Ldc_I4_5); break;
                case 6: basic.Emit(OpCodes.Ldc_I4_6); break;
                case 7: basic.Emit(OpCodes.Ldc_I4_7); break;
                case 8: basic.Emit(OpCodes.Ldc_I4_8); break;
                default:
                    if (value < SByte.MinValue || value > SByte.MaxValue)
                    {
                        basic.Emit(OpCodes.Ldc_I4, value);
                    }
                    else
                    {
                        basic.Emit(OpCodes.Ldc_I4_S, value);
                    }
                    break;
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
            var res = field.NewBoolean();
            field.Pop();
            foreach (var code in codes)
            {
                field.EmitValue(value);
                field.Emit(code);
            }
            field.Emit(OpCodes.Stloc_S, res);
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
            var res = field.NewBoolean();
            field.Pop();
            foreach (var code in codes)
            {
                field.EmitValue(value);
                field.Emit(code);
            }
            field.Emit(OpCodes.Stloc_S, res);
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
            var res = field.NewBoolean();
            field.Pop();
            foreach (var code in codes)
            {
                field.Emit(OpCodes.Ldloc_S, value);
                field.Emit(code);
            }
            field.Emit(OpCodes.Stloc_S, res);
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
            var res = field.NewBoolean();
            field.Pop();
            foreach (var code in codes)
            {
                value.Pop();
                field.Emit(code);
            }
            field.Emit(OpCodes.Stloc_S, res);
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
            field.Pop();
            field.EmitValue(value);
            field.Emit(code);
            field.Push();
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
            field.Pop();
            field.Emit(OpCodes.Ldloc_S, value);
            field.Emit(code);
            field.Push();
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
            field.Pop();
            value.Pop();
            field.Emit(code);
            field.Push();
            return field;
        }

        /// <summary>
        /// 基础For正循环
        /// </summary>
        /// <param name="init"></param>
        /// <param name="length"></param>
        /// <param name="build"></param>
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
        /// <param name="init"></param>
        /// <param name="length"></param>
        /// <param name="build"></param>
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
        /// 内部渗透方案(String)
        /// </summary>
        /// <param name="il"></param>
        /// <param name="value"></param>
        /// <returns></returns>
        internal static FieldString NewString(this EmitBasic basic, String value = default(String))
        {
            LocalBuilder item = basic.DeclareLocal(typeof(String));
            basic.Emit(value == null ? OpCodes.Ldnull : OpCodes.Ldstr, value);
            basic.Emit(OpCodes.Stloc_S, item);
            return new FieldString(item, basic);
        }

        /// <summary>
        /// 内部渗透方案(Boolean)
        /// </summary>
        /// <param name="basic"></param>
        /// <param name="value"></param>
        /// <returns></returns>
        internal static FieldBoolean NewBoolean(this EmitBasic basic, Boolean value = default(Boolean))
        {
            LocalBuilder item = basic.DeclareLocal(typeof(Boolean));
            basic.EmitValue(value);
            basic.Emit(OpCodes.Stloc_S, item);
            return new FieldBoolean(item, basic);
        }

        /// <summary>
        /// 内部渗透方案(Int32)
        /// </summary>
        /// <param name="basic"></param>
        /// <param name="value"></param>
        /// <returns></returns>
        internal static FieldInt32 NewInt32(this EmitBasic basic, Int32 value = default(Int32))
        {
            LocalBuilder item = basic.DeclareLocal(typeof(Int32));
            basic.EmitValue(value);
            basic.Emit(OpCodes.Stloc_S, item);
            return new FieldInt32(item, basic);
        }

        /// <summary>
        /// 内部渗透方案(Int64)
        /// </summary>
        /// <param name="basic"></param>
        /// <param name="value"></param>
        /// <returns></returns>
        internal static FieldInt64 NewInt64(this EmitBasic basic, Int64 value = default(Int64))
        {
            LocalBuilder item = basic.DeclareLocal(typeof(Int64));
            basic.Emit(OpCodes.Ldc_I8, value);
            basic.Emit(OpCodes.Stloc_S, item);
            return new FieldInt64(item, basic);
        }

        /// <summary>
        /// 内部渗透方案(Float)
        /// </summary>
        /// <param name="basic"></param>
        /// <param name="value"></param>
        /// <returns></returns>
        internal static FieldFloat NewFloat(this EmitBasic basic, Single value = default(Single))
        {
            LocalBuilder item = basic.DeclareLocal(typeof(Single));
            basic.Emit(OpCodes.Ldc_R4, value);
            basic.Emit(OpCodes.Stloc_S, item);
            return new FieldFloat(item, basic);
        }

        /// <summary>
        /// 内部渗透方案(Double)
        /// </summary>
        /// <param name="basic"></param>
        /// <param name="value"></param>
        /// <returns></returns>
        internal static FieldDouble NewDouble(this EmitBasic basic, Double value = default(Double))
        {
            LocalBuilder item = basic.DeclareLocal(typeof(Double));
            basic.Emit(OpCodes.Ldc_R8, value);
            basic.Emit(OpCodes.Stloc_S, item);
            return new FieldDouble(item, basic);
        }

        /// <summary>
        /// 内部渗透方案(Decimal)
        /// </summary>
        /// <param name="basic"></param>
        /// <param name="value"></param>
        /// <returns></returns>
        internal static FieldDecimal NewDecimal(this EmitBasic basic, Decimal value = default(Decimal))
        {
            LocalBuilder item = basic.DeclareLocal(typeof(Decimal));
            Int32[] bits = Decimal.GetBits(Convert.ToDecimal(value));
            basic.Int32Map(bits[0]);
            basic.Int32Map(bits[1]);
            basic.Int32Map(bits[2]);
            basic.Emit((bits[3] & 0x80000000) != 0 ? OpCodes.Ldc_I4_1 : OpCodes.Ldc_I4_0);
            basic.Int32Map((bits[3] >> 16) & 0x7f);
            basic.Emit(OpCodes.Newobj, BaseConst._DecimalCtor);
            basic.Emit(OpCodes.Stloc_S, item);
            //LocalBuilder item = basic.DeclareLocal(typeof(Decimal));
            //basic.Emit(OpCodes.Ldc_R8, value);
            //basic.Emit(OpCodes.Newobj, typeof(Decimal).GetConstructor(new Type[] { typeof(Double) }));
            //basic.Emit(OpCodes.Stloc_S, item);
            return new FieldDecimal(item, basic);
        }

        /// <summary>
        /// 内部渗透方案(DateTime)
        /// </summary>
        /// <param name="basic"></param>
        /// <param name="value"></param>
        /// <returns></returns>
        internal static FieldDateTime NewDateTime(this EmitBasic basic, DateTime datatime = default(DateTime))
        {
            LocalBuilder item = basic.DeclareLocal(typeof(DateTime));
            basic.Emit(OpCodes.Ldc_I8, datatime.Ticks);
            basic.Emit(OpCodes.Newobj, typeof(DateTime).GetConstructor(new Type[] { typeof(Int64) }));
            basic.Emit(OpCodes.Stloc_S, item);
            return new FieldDateTime(item, basic);
        }

        /// <summary>
        /// 内部渗透方案(Entity)
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
        /// 内部渗透方案(Array)
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
        /// 内部渗透方案(Array)
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
        /// 内部渗透方案(List)
        /// </summary>
        /// <param name="il"></param>
        /// <param name="value"></param>
        /// <returns></returns>
        //internal static ListManager<T> NewList<T>(this EmitBasic basic)
        //{
        //    LocalBuilder item = basic.DeclareLocal(typeof(List<T>));
        //    basic.Emit(OpCodes.Newobj, typeof(List<T>));
        //    basic.Emit(OpCodes.Stloc_S, item);
        //    return new ListManager<T>(item, basic);
        //}

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
        /// IL模式的Int32转成CodeDom模式的Int32值体现
        /// </summary>
        /// <param name="item"></param>
        /// <returns></returns>
        //internal static Int32 ILInt32ToDomInt32(EmitBasic basic, LocalBuilder item)
        //{
        //    if (!CacheManager.DelegatePool.ContainsKey("ILInt32ToDomInt32"))
        //    {
        //        CacheManager.DelegatePool.Add("ILInt32ToDomInt32",
        //           SmartBuilder.DynamicMethod<Func<EmitBasic, LocalBuilder, Int32>>("ILInt32ToDomInt32", func =>
        //           {
        //               func.Emit(OpCodes.Ldarg_0);
        //           }));
        //    }
        //    return ((Func<EmitBasic, LocalBuilder, Int32>)CacheManager.DelegatePool["ILInt32ToDomInt32"])(basic, item);
        //}

        /// <summary>
        /// 转换成CodeDom级别的实体
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <returns></returns>
        //internal static T ToCodeDomeLevel<T>() where T : class, Delegate
        //{
        //    Type type = typeof(T);
        //    if (!CacheManager.DelegatePool.ContainsKey(type.FullName))
        //    {
        //        var tt = SmartBuilder.DynamicMethod<T>("testt", func =>
        //        {
        //            func.EmitParam(0);
        //            func.EmitReturn();
        //        });
        //        CacheManager.DelegatePool.Add(type.FullName, tt);
        //    }
        //    return CacheManager.DelegatePool[type.FullName] as T;
        //}

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
