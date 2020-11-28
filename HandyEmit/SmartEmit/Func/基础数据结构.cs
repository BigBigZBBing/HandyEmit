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
            return ManagerGX.NewString(this, value);
        }

        /// <summary>
        /// 初始化Boolean
        /// </summary>
        /// <param name="value"></param>
        public FieldBoolean NewBoolean(Boolean value = default(Boolean))
        {
            return ManagerGX.NewBoolean(this, value);
        }

        /// <summary>
        /// 初始化Int32
        /// </summary>
        /// <param name="value"></param>
        public CanCompute<Int32> NewInt32(Int32 value = default(Int32))
        {
            return ManagerGX.NewInt32(this, value);
        }

        /// <summary>
        /// 初始化Int64
        /// </summary>
        /// <param name="value"></param>
        public CanCompute<Int64> NewInt64(Int64 value = default(Int64))
        {
            return ManagerGX.NewInt64(this, value);
        }

        /// <summary>
        /// 初始化Float
        /// </summary>
        /// <param name="value"></param>
        public CanCompute<Single> NewFloat(Single value = default(Single))
        {
            return ManagerGX.NewFloat(this, value);
        }

        /// <summary>
        /// 初始化Double
        /// </summary>
        /// <param name="value"></param>
        public CanCompute<Double> NewDouble(Double value = default(Double))
        {
            return ManagerGX.NewDouble(this, value);
        }

        /// <summary>
        /// 初始化Decimal
        /// </summary>
        /// <param name="value"></param>
        public CanCompute<Decimal> NewDecimal(Double value = default(Double))
        {
            return ManagerGX.NewDecimal(this, value);
        }

        /// <summary>
        /// 初始化DateTime
        /// </summary>
        /// <param name="value"></param>
        public CanCompute<Decimal> NewDateTime(DateTime value = default(DateTime))
        {
            return ManagerGX.NewDateTime(this, value);
        }

        /// <summary>
        /// 初始化数组
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="length"></param>
        public FieldArray<T> NewArray<T>(Int32 length = default(Int32))
        {
            return ManagerGX.NewArray<T>(this, length);
        }

        /// <summary>
        /// 初始化链表
        /// </summary>
        /// <typeparam name="T"></typeparam>
        //public ListManager<T> NewList<T>()
        //{
        //    return NewList<T>();
        //}

        #endregion
    }
}
