using ILWheatBread.Attributes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection.Emit;
using System.Text;
using System.Threading.Tasks;

namespace ILWheatBread.SmartEmit
{
    internal static partial class ManagerGX
    {
        /// <summary>
        /// 根据泛型去自动适配推入计算堆的方式
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="basic"></param>
        /// <param name="value"></param>
        internal static void EmitValue<T>(this EmitBasic basic, T value)
        {
            if (value == null)
            {
                basic.Emit(OpCodes.Ldnull);
            }
            else if (typeof(T) == typeof(String))
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
                basic.Emit(OpCodes.Newobj, typeof(Decimal)
                    .GetConstructor(new Type[] { typeof(Int32), typeof(Int32), typeof(Int32), typeof(Boolean), typeof(Byte) }));
            }
            else if (typeof(T) == typeof(DateTime))
            {
                basic.Emit(OpCodes.Ldc_I8, Convert.ToDateTime(value).Ticks);
                basic.Emit(OpCodes.Newobj, typeof(DateTime).GetConstructor(new Type[] { typeof(Int64) }));
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
                basic.Emit(OpCodes.Newobj, typeof(Decimal)
                    .GetConstructor(new Type[] { typeof(Int32), typeof(Int32), typeof(Int32), typeof(Boolean), typeof(Byte) }));
            }
            else if (type == typeof(DateTime))
            {
                basic.Emit(OpCodes.Ldc_I8, Convert.ToDateTime(value).Ticks);
                basic.Emit(OpCodes.Newobj, typeof(DateTime).GetConstructor(new Type[] { typeof(Int64) }));
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
    }
}
