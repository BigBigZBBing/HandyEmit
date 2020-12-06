using ILWheatBread.SmartEmit.Field;
using System;
using System.Collections.Generic;
using System.Reflection.Emit;
using System.Text;

namespace ILWheatBread.SmartEmit
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
        /// 初始化String
        /// </summary>
        /// <param name="value"></param>
        public FieldString NewString(LocalBuilder value)
        {
            return new FieldString(value, this);
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
        /// 初始化Boolean
        /// </summary>
        /// <param name="value"></param>
        public FieldBoolean NewBoolean(LocalBuilder value)
        {
            return new FieldBoolean(value, this);
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
        /// 初始化Int32
        /// </summary>
        /// <param name="value"></param>
        public CanCompute<Int32> NewInt32(LocalBuilder value)
        {
            return new FieldInt32(value, this);
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
        /// 初始化Int64
        /// </summary>
        /// <param name="value"></param>
        public CanCompute<Int64> NewInt64(LocalBuilder value)
        {
            return new FieldInt64(value, this);
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
        /// 初始化Float
        /// </summary>
        /// <param name="value"></param>
        public CanCompute<Single> NewFloat(LocalBuilder value)
        {
            return new FieldFloat(value, this);
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
        /// 初始化Double
        /// </summary>
        /// <param name="value"></param>
        public CanCompute<Double> NewDouble(LocalBuilder value)
        {
            return new FieldDouble(value, this);
        }

        /// <summary>
        /// 初始化Decimal
        /// </summary>
        /// <param name="value"></param>
        public CanCompute<Decimal> NewDecimal(Decimal value = default(Decimal))
        {
            return ManagerGX.NewDecimal(this, value);
        }

        /// <summary>
        /// 初始化Decimal
        /// </summary>
        /// <param name="value"></param>
        public CanCompute<Decimal> NewDecimal(LocalBuilder value)
        {
            return new FieldDecimal(value, this);
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
        /// 初始化DateTime
        /// </summary>
        /// <param name="value"></param>
        public CanCompute<Decimal> NewDateTime(LocalBuilder value)
        {
            return new FieldDecimal(value, this);
        }

        /// <summary>
        /// 初始化数组
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="length"></param>
        public FieldArray<T> NewArray<T>(Int32 length)
        {
            return ManagerGX.NewArray<T>(this, length);
        }

        /// <summary>
        /// 初始化数组
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="length"></param>
        public FieldArray<T> NewArray<T>(CanCompute<Int32> length)
        {
            return ManagerGX.NewArray<T>(this, length);
        }

        /// <summary>
        /// 初始化数组
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="value"></param>
        /// <returns></returns>
        public FieldArray<T> NewArray<T>(LocalBuilder value)
        {
            return new FieldArray<T>(value, this, -1);
        }

        /// <summary>
        /// 初始化实体类型
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <returns></returns>
        public FieldEntity<T> NewEntity<T>() where T : class, new()
        {
            return ManagerGX.NewEntity<T>(this);
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
