using HandyEmit.SmartEmit.Field;
using System;
using System.Collections.Generic;
using System.Reflection.Emit;
using System.Text;

namespace HandyEmit.SmartEmit
{
    public partial class FuncGenerator
    {
        #region 基础数据结构

        /// <summary>
        /// 初始化String
        /// </summary>
        /// <param name="value"></param>
        public FieldString NewString(String value = default(String))
        {
            return this.il.NewString(value);
        }

        /// <summary>
        /// 初始化Boolean
        /// </summary>
        /// <param name="value"></param>
        public FieldBoolean NewBoolean(Boolean value = default(Boolean))
        {
            return this.il.NewBoolean(value);
        }

        /// <summary>
        /// 初始化Int32
        /// </summary>
        /// <param name="value"></param>
        public CanCompute<Int32> NewInt32(Int32 value = default(Int32))
        {
            return this.il.NewInt32(value);
        }

        /// <summary>
        /// 初始化Int64
        /// </summary>
        /// <param name="value"></param>
        public CanCompute<Int64> NewInt64(Int64 value = default(Int64))
        {
            return this.il.NewInt64(value);
        }

        /// <summary>
        /// 初始化Float
        /// </summary>
        /// <param name="value"></param>
        public CanCompute<Single> NewFloat(Single value = default(Single))
        {
            return this.il.NewFloat(value);
        }

        /// <summary>
        /// 初始化Double
        /// </summary>
        /// <param name="value"></param>
        public CanCompute<Double> NewDouble(Double value = default(Double))
        {
            return this.il.NewDouble(value);
        }

        /// <summary>
        /// 初始化Decimal
        /// </summary>
        /// <param name="value"></param>
        public CanCompute<Decimal> NewDecimal(Double value = default(Double))
        {
            return this.il.NewDecimal(value);
        }

        /// <summary>
        /// 初始化数组
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="length"></param>
        public ArrayManager<T> NewArray<T>(Int32 length = default(Int32))
        {
            return this.il.NewArray<T>(length);
        }

        /// <summary>
        /// 初始化链表
        /// </summary>
        /// <typeparam name="T"></typeparam>
        public ListManager<T> NewList<T>()
        {
            return this.il.NewList<T>();
        }

        #endregion
    }
}
