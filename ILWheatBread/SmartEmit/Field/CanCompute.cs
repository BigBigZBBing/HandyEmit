using System;
using System.Collections.Generic;
using System.Reflection.Emit;
using System.Text;

namespace ILWheatBread.SmartEmit.Field
{
    /// <summary>
    /// 可计算类型方案
    /// </summary>
    /// <typeparam name="T"></typeparam>
    public class CanCompute<T> : FieldManager<T>
    {
        internal CanCompute(LocalBuilder stack, ILGenerator generator) : base(stack, generator)
        {
        }

        //internal Func<EmitBasic, LocalBuilder, T> ToCodeDomLevel = ManagerGX.ToCodeDomeLevel<Func<EmitBasic, LocalBuilder, T>>();

        /// <summary>
        /// 返回一个原始值
        /// </summary>
        /// <returns></returns>
        //public T ToValueType()
        //{
        //    return ToCodeDomLevel(this, this);
        //}

        public static implicit operator FieldInt32(CanCompute<T> field) => new FieldInt32(field.stack, field.generator);
        public static implicit operator FieldInt64(CanCompute<T> field) => new FieldInt64(field.stack, field.generator);
        public static implicit operator FieldFloat(CanCompute<T> field) => new FieldFloat(field.stack, field.generator);
        public static implicit operator FieldDouble(CanCompute<T> field) => new FieldDouble(field.stack, field.generator);
        public static implicit operator FieldDecimal(CanCompute<T> field) => new FieldDecimal(field.stack, field.generator);

        #region 大于

        /// <summary>
        /// 大于
        /// </summary>
        /// <param name="field"></param>
        /// <param name="value"></param>
        /// <returns></returns>
        public static FieldBoolean operator >(CanCompute<T> field, Decimal value)
        {
            return ManagerGX.Comparer(field, value, OpCodes.Cgt);
        }

        /// <summary>
        /// 大于
        /// </summary>
        /// <param name="field"></param>
        /// <param name="value"></param>
        /// <returns></returns>
        public static FieldBoolean operator >(CanCompute<T> field, Double value)
        {
            return ManagerGX.Comparer(field, value, OpCodes.Cgt);
        }

        /// <summary>
        /// 大于
        /// </summary>
        /// <param name="field"></param>
        /// <param name="value"></param>
        /// <returns></returns>
        public static FieldBoolean operator >(CanCompute<T> field, Single value)
        {
            return ManagerGX.Comparer(field, value, OpCodes.Cgt);
        }

        /// <summary>
        /// 大于
        /// </summary>
        /// <param name="field"></param>
        /// <param name="value"></param>
        /// <returns></returns>
        public static FieldBoolean operator >(CanCompute<T> field, Int64 value)
        {
            return ManagerGX.Comparer(field, value, OpCodes.Cgt);
        }

        /// <summary>
        /// 大于
        /// </summary>
        /// <param name="field"></param>
        /// <param name="value"></param>
        /// <returns></returns>
        public static FieldBoolean operator >(CanCompute<T> field, Int32 value)
        {
            return ManagerGX.Comparer(field, value, OpCodes.Cgt);
        }

        /// <summary>
        /// 大于
        /// </summary>
        /// <param name="field"></param>
        /// <param name="value"></param>
        /// <returns></returns>
        public static FieldBoolean operator >(CanCompute<T> field, LocalBuilder value)
        {
            return ManagerGX.Comparer(field, value, OpCodes.Cgt);
        }

        /// <summary>
        /// 大于
        /// </summary>
        /// <param name="field"></param>
        /// <param name="value"></param>
        /// <returns></returns>
        public static FieldBoolean operator >(CanCompute<T> field, CanCompute<Decimal> value)
        {
            return ManagerGX.Comparer(field, value, OpCodes.Cgt);
        }

        /// <summary>
        /// 大于
        /// </summary>
        /// <param name="field"></param>
        /// <param name="value"></param>
        /// <returns></returns>
        public static FieldBoolean operator >(CanCompute<T> field, CanCompute<Double> value)
        {
            return ManagerGX.Comparer(field, value, OpCodes.Cgt);
        }

        /// <summary>
        /// 大于
        /// </summary>
        /// <param name="field"></param>
        /// <param name="value"></param>
        /// <returns></returns>
        public static FieldBoolean operator >(CanCompute<T> field, CanCompute<Single> value)
        {
            return ManagerGX.Comparer(field, value, OpCodes.Cgt);
        }

        /// <summary>
        /// 大于
        /// </summary>
        /// <param name="field"></param>
        /// <param name="value"></param>
        /// <returns></returns>
        public static FieldBoolean operator >(CanCompute<T> field, CanCompute<Int64> value)
        {
            return ManagerGX.Comparer(field, value, OpCodes.Cgt);
        }

        /// <summary>
        /// 大于
        /// </summary>
        /// <param name="field"></param>
        /// <param name="value"></param>
        /// <returns></returns>
        public static FieldBoolean operator >(CanCompute<T> field, CanCompute<Int32> value)
        {
            return ManagerGX.Comparer(field, value, OpCodes.Cgt);
        }

        #endregion

        #region 小于

        /// <summary>
        /// 小于
        /// </summary>
        /// <param name="field"></param>
        /// <param name="value"></param>
        /// <returns></returns>
        public static FieldBoolean operator <(CanCompute<T> field, Decimal value)
        {
            return ManagerGX.Comparer(field, value, OpCodes.Clt);
        }

        /// <summary>
        /// 小于
        /// </summary>
        /// <param name="field"></param>
        /// <param name="value"></param>
        /// <returns></returns>
        public static FieldBoolean operator <(CanCompute<T> field, Double value)
        {
            return ManagerGX.Comparer(field, value, OpCodes.Clt);
        }

        /// <summary>
        /// 小于
        /// </summary>
        /// <param name="field"></param>
        /// <param name="value"></param>
        /// <returns></returns>
        public static FieldBoolean operator <(CanCompute<T> field, Single value)
        {
            return ManagerGX.Comparer(field, value, OpCodes.Clt);
        }

        /// <summary>
        /// 小于
        /// </summary>
        /// <param name="field"></param>
        /// <param name="value"></param>
        /// <returns></returns>
        public static FieldBoolean operator <(CanCompute<T> field, Int64 value)
        {
            return ManagerGX.Comparer(field, value, OpCodes.Clt);
        }

        /// <summary>
        /// 小于
        /// </summary>
        /// <param name="field"></param>
        /// <param name="value"></param>
        /// <returns></returns>
        public static FieldBoolean operator <(CanCompute<T> field, Int32 value)
        {
            return ManagerGX.Comparer(field, value, OpCodes.Clt);
        }

        /// <summary>
        /// 小于
        /// </summary>
        /// <param name="field"></param>
        /// <param name="value"></param>
        /// <returns></returns>
        public static FieldBoolean operator <(CanCompute<T> field, LocalBuilder value)
        {
            return ManagerGX.Comparer(field, value, OpCodes.Clt);
        }

        /// <summary>
        /// 小于
        /// </summary>
        /// <param name="field"></param>
        /// <param name="value"></param>
        /// <returns></returns>
        public static FieldBoolean operator <(CanCompute<T> field, CanCompute<Decimal> value)
        {
            return ManagerGX.Comparer(field, value, OpCodes.Clt);
        }

        /// <summary>
        /// 小于
        /// </summary>
        /// <param name="field"></param>
        /// <param name="value"></param>
        /// <returns></returns>
        public static FieldBoolean operator <(CanCompute<T> field, CanCompute<Double> value)
        {
            return ManagerGX.Comparer(field, value, OpCodes.Clt);
        }

        /// <summary>
        /// 小于
        /// </summary>
        /// <param name="field"></param>
        /// <param name="value"></param>
        /// <returns></returns>
        public static FieldBoolean operator <(CanCompute<T> field, CanCompute<Single> value)
        {
            return ManagerGX.Comparer(field, value, OpCodes.Clt);
        }

        /// <summary>
        /// 小于
        /// </summary>
        /// <param name="field"></param>
        /// <param name="value"></param>
        /// <returns></returns>
        public static FieldBoolean operator <(CanCompute<T> field, CanCompute<Int64> value)
        {
            return ManagerGX.Comparer(field, value, OpCodes.Clt);
        }

        /// <summary>
        /// 小于
        /// </summary>
        /// <param name="field"></param>
        /// <param name="value"></param>
        /// <returns></returns>
        public static FieldBoolean operator <(CanCompute<T> field, CanCompute<Int32> value)
        {
            return ManagerGX.Comparer(field, value, OpCodes.Clt);
        }

        #endregion

        #region 大于等于

        /// <summary>
        /// 大于等于
        /// </summary>
        /// <param name="field"></param>
        /// <param name="value"></param>
        /// <returns></returns>
        public static FieldBoolean operator >=(CanCompute<T> field, Decimal value)
        {
            return ManagerGX.Comparer(field, value, OpCodes.Cgt, OpCodes.Ceq);
        }

        /// <summary>
        /// 大于等于
        /// </summary>
        /// <param name="field"></param>
        /// <param name="value"></param>
        /// <returns></returns>
        public static FieldBoolean operator >=(CanCompute<T> field, Double value)
        {
            return ManagerGX.Comparer(field, value, OpCodes.Cgt, OpCodes.Ceq);
        }

        /// <summary>
        /// 大于等于
        /// </summary>
        /// <param name="field"></param>
        /// <param name="value"></param>
        /// <returns></returns>
        public static FieldBoolean operator >=(CanCompute<T> field, Single value)
        {
            return ManagerGX.Comparer(field, value, OpCodes.Cgt, OpCodes.Ceq);
        }

        /// <summary>
        /// 大于等于
        /// </summary>
        /// <param name="field"></param>
        /// <param name="value"></param>
        /// <returns></returns>
        public static FieldBoolean operator >=(CanCompute<T> field, Int64 value)
        {
            return ManagerGX.Comparer(field, value, OpCodes.Cgt, OpCodes.Ceq);
        }

        /// <summary>
        /// 大于等于
        /// </summary>
        /// <param name="field"></param>
        /// <param name="value"></param>
        /// <returns></returns>
        public static FieldBoolean operator >=(CanCompute<T> field, Int32 value)
        {
            return ManagerGX.Comparer(field, value, OpCodes.Cgt, OpCodes.Ceq);
        }

        /// <summary>
        /// 大于等于
        /// </summary>
        /// <param name="field"></param>
        /// <param name="value"></param>
        /// <returns></returns>
        public static FieldBoolean operator >=(CanCompute<T> field, LocalBuilder value)
        {
            return ManagerGX.Comparer(field, value, OpCodes.Cgt, OpCodes.Ceq);
        }

        /// <summary>
        /// 大于等于
        /// </summary>
        /// <param name="field"></param>
        /// <param name="value"></param>
        /// <returns></returns>
        public static FieldBoolean operator >=(CanCompute<T> field, CanCompute<Decimal> value)
        {
            return ManagerGX.Comparer(field, value, OpCodes.Cgt, OpCodes.Ceq);
        }

        /// <summary>
        /// 大于等于
        /// </summary>
        /// <param name="field"></param>
        /// <param name="value"></param>
        /// <returns></returns>
        public static FieldBoolean operator >=(CanCompute<T> field, CanCompute<Double> value)
        {
            return ManagerGX.Comparer(field, value, OpCodes.Cgt, OpCodes.Ceq);
        }

        /// <summary>
        /// 大于等于
        /// </summary>
        /// <param name="field"></param>
        /// <param name="value"></param>
        /// <returns></returns>
        public static FieldBoolean operator >=(CanCompute<T> field, CanCompute<Single> value)
        {
            return ManagerGX.Comparer(field, value, OpCodes.Cgt, OpCodes.Ceq);
        }

        /// <summary>
        /// 大于等于
        /// </summary>
        /// <param name="field"></param>
        /// <param name="value"></param>
        /// <returns></returns>
        public static FieldBoolean operator >=(CanCompute<T> field, CanCompute<Int64> value)
        {
            return ManagerGX.Comparer(field, value, OpCodes.Cgt, OpCodes.Ceq);
        }

        /// <summary>
        /// 大于等于
        /// </summary>
        /// <param name="field"></param>
        /// <param name="value"></param>
        /// <returns></returns>
        public static FieldBoolean operator >=(CanCompute<T> field, CanCompute<Int32> value)
        {
            return ManagerGX.Comparer(field, value, OpCodes.Cgt, OpCodes.Ceq);
        }

        #endregion

        #region 小于等于

        /// <summary>
        /// 小于等于
        /// </summary>
        /// <param name="field"></param>
        /// <param name="value"></param>
        /// <returns></returns>
        public static FieldBoolean operator <=(CanCompute<T> field, Decimal value)
        {
            return ManagerGX.Comparer(field, value, OpCodes.Clt, OpCodes.Ceq);
        }

        /// <summary>
        /// 小于等于
        /// </summary>
        /// <param name="field"></param>
        /// <param name="value"></param>
        /// <returns></returns>
        public static FieldBoolean operator <=(CanCompute<T> field, Double value)
        {
            return ManagerGX.Comparer(field, value, OpCodes.Clt, OpCodes.Ceq);
        }

        /// <summary>
        /// 小于等于
        /// </summary>
        /// <param name="field"></param>
        /// <param name="value"></param>
        /// <returns></returns>
        public static FieldBoolean operator <=(CanCompute<T> field, Single value)
        {
            return ManagerGX.Comparer(field, value, OpCodes.Clt, OpCodes.Ceq);
        }

        /// <summary>
        /// 小于等于
        /// </summary>
        /// <param name="field"></param>
        /// <param name="value"></param>
        /// <returns></returns>
        public static FieldBoolean operator <=(CanCompute<T> field, Int64 value)
        {
            return ManagerGX.Comparer(field, value, OpCodes.Clt, OpCodes.Ceq);
        }

        /// <summary>
        /// 小于等于
        /// </summary>
        /// <param name="field"></param>
        /// <param name="value"></param>
        /// <returns></returns>
        public static FieldBoolean operator <=(CanCompute<T> field, Int32 value)
        {
            return ManagerGX.Comparer(field, value, OpCodes.Clt, OpCodes.Ceq);
        }

        /// <summary>
        /// 小于等于
        /// </summary>
        /// <param name="field"></param>
        /// <param name="value"></param>
        /// <returns></returns>
        public static FieldBoolean operator <=(CanCompute<T> field, LocalBuilder value)
        {
            return ManagerGX.Comparer(field, value, OpCodes.Clt, OpCodes.Ceq);
        }

        /// <summary>
        /// 小于等于
        /// </summary>
        /// <param name="field"></param>
        /// <param name="value"></param>
        /// <returns></returns>
        public static FieldBoolean operator <=(CanCompute<T> field, CanCompute<Decimal> value)
        {
            return ManagerGX.Comparer(field, value, OpCodes.Clt, OpCodes.Ceq);
        }

        /// <summary>
        /// 小于等于
        /// </summary>
        /// <param name="field"></param>
        /// <param name="value"></param>
        /// <returns></returns>
        public static FieldBoolean operator <=(CanCompute<T> field, CanCompute<Double> value)
        {
            return ManagerGX.Comparer(field, value, OpCodes.Clt, OpCodes.Ceq);
        }

        /// <summary>
        /// 小于等于
        /// </summary>
        /// <param name="field"></param>
        /// <param name="value"></param>
        /// <returns></returns>
        public static FieldBoolean operator <=(CanCompute<T> field, CanCompute<Single> value)
        {
            return ManagerGX.Comparer(field, value, OpCodes.Clt, OpCodes.Ceq);
        }

        /// <summary>
        /// 小于等于
        /// </summary>
        /// <param name="field"></param>
        /// <param name="value"></param>
        /// <returns></returns>
        public static FieldBoolean operator <=(CanCompute<T> field, CanCompute<Int64> value)
        {
            return ManagerGX.Comparer(field, value, OpCodes.Clt, OpCodes.Ceq);
        }

        /// <summary>
        /// 小于等于
        /// </summary>
        /// <param name="field"></param>
        /// <param name="value"></param>
        /// <returns></returns>
        public static FieldBoolean operator <=(CanCompute<T> field, CanCompute<Int32> value)
        {
            return ManagerGX.Comparer(field, value, OpCodes.Clt, OpCodes.Ceq);
        }

        #endregion

        #region 相等

        /// <summary>
        /// 相等
        /// </summary>
        /// <param name="field"></param>
        /// <param name="value"></param>
        /// <returns></returns>
        public static FieldBoolean operator ==(CanCompute<T> field, Decimal value)
        {
            return ManagerGX.Comparer(field, value, OpCodes.Ceq);
        }

        /// <summary>
        /// 相等
        /// </summary>
        /// <param name="field"></param>
        /// <param name="value"></param>
        /// <returns></returns>
        public static FieldBoolean operator ==(CanCompute<T> field, Double value)
        {
            return ManagerGX.Comparer(field, value, OpCodes.Ceq);
        }

        /// <summary>
        /// 相等
        /// </summary>
        /// <param name="field"></param>
        /// <param name="value"></param>
        /// <returns></returns>
        public static FieldBoolean operator ==(CanCompute<T> field, Single value)
        {
            return ManagerGX.Comparer(field, value, OpCodes.Ceq);
        }

        /// <summary>
        /// 相等
        /// </summary>
        /// <param name="field"></param>
        /// <param name="value"></param>
        /// <returns></returns>
        public static FieldBoolean operator ==(CanCompute<T> field, Int64 value)
        {
            return ManagerGX.Comparer(field, value, OpCodes.Ceq);
        }

        /// <summary>
        /// 相等
        /// </summary>
        /// <param name="field"></param>
        /// <param name="value"></param>
        /// <returns></returns>
        public static FieldBoolean operator ==(CanCompute<T> field, Int32 value)
        {
            return ManagerGX.Comparer(field, value, OpCodes.Ceq);
        }

        /// <summary>
        /// 相等
        /// </summary>
        /// <param name="field"></param>
        /// <param name="value"></param>
        /// <returns></returns>
        public static FieldBoolean operator ==(CanCompute<T> field, LocalBuilder value)
        {
            return ManagerGX.Comparer(field, value, OpCodes.Ceq);
        }

        /// <summary>
        /// 相等
        /// </summary>
        /// <param name="field"></param>
        /// <param name="value"></param>
        /// <returns></returns>
        public static FieldBoolean operator ==(CanCompute<T> field, CanCompute<Decimal> value)
        {
            return ManagerGX.Comparer(field, value, OpCodes.Ceq);
        }

        /// <summary>
        /// 相等
        /// </summary>
        /// <param name="field"></param>
        /// <param name="value"></param>
        /// <returns></returns>
        public static FieldBoolean operator ==(CanCompute<T> field, CanCompute<Double> value)
        {
            return ManagerGX.Comparer(field, value, OpCodes.Ceq);
        }

        /// <summary>
        /// 相等
        /// </summary>
        /// <param name="field"></param>
        /// <param name="value"></param>
        /// <returns></returns>
        public static FieldBoolean operator ==(CanCompute<T> field, CanCompute<Single> value)
        {
            return ManagerGX.Comparer(field, value, OpCodes.Ceq);
        }

        /// <summary>
        /// 相等
        /// </summary>
        /// <param name="field"></param>
        /// <param name="value"></param>
        /// <returns></returns>
        public static FieldBoolean operator ==(CanCompute<T> field, CanCompute<Int64> value)
        {
            return ManagerGX.Comparer(field, value, OpCodes.Ceq);
        }

        /// <summary>
        /// 相等
        /// </summary>
        /// <param name="field"></param>
        /// <param name="value"></param>
        /// <returns></returns>
        public static FieldBoolean operator ==(CanCompute<T> field, CanCompute<Int32> value)
        {
            return ManagerGX.Comparer(field, value, OpCodes.Ceq);
        }

        #endregion

        #region 不相等

        /// <summary>
        /// 不相等
        /// </summary>
        /// <param name="field"></param>
        /// <param name="value"></param>
        /// <returns></returns>
        public static FieldBoolean operator !=(CanCompute<T> field, Decimal value)
        {
            return ManagerGX.Comparer(
                ManagerGX.Comparer(field, value, OpCodes.Ceq),
                field.NewInt32(), OpCodes.Ceq);
        }

        /// <summary>
        /// 不相等
        /// </summary>
        /// <param name="field"></param>
        /// <param name="value"></param>
        /// <returns></returns>
        public static FieldBoolean operator !=(CanCompute<T> field, Double value)
        {
            return ManagerGX.Comparer(
                ManagerGX.Comparer(field, value, OpCodes.Ceq),
                field.NewInt32(), OpCodes.Ceq);
        }

        /// <summary>
        /// 不相等
        /// </summary>
        /// <param name="field"></param>
        /// <param name="value"></param>
        /// <returns></returns>
        public static FieldBoolean operator !=(CanCompute<T> field, Single value)
        {
            return ManagerGX.Comparer(
                ManagerGX.Comparer(field, value, OpCodes.Ceq),
                field.NewInt32(), OpCodes.Ceq);
        }

        /// <summary>
        /// 不相等
        /// </summary>
        /// <param name="field"></param>
        /// <param name="value"></param>
        /// <returns></returns>
        public static FieldBoolean operator !=(CanCompute<T> field, Int64 value)
        {
            return ManagerGX.Comparer(
                ManagerGX.Comparer(field, value, OpCodes.Ceq),
                field.NewInt32(), OpCodes.Ceq);
        }

        /// <summary>
        /// 不相等
        /// </summary>
        /// <param name="field"></param>
        /// <param name="value"></param>
        /// <returns></returns>
        public static FieldBoolean operator !=(CanCompute<T> field, Int32 value)
        {
            return ManagerGX.Comparer(
                ManagerGX.Comparer(field, value, OpCodes.Ceq),
                field.NewInt32(), OpCodes.Ceq);
        }

        /// <summary>
        /// 不相等
        /// </summary>
        /// <param name="field"></param>
        /// <param name="value"></param>
        /// <returns></returns>
        public static FieldBoolean operator !=(CanCompute<T> field, LocalBuilder value)
        {
            return ManagerGX.Comparer(
                ManagerGX.Comparer(field, value, OpCodes.Ceq),
                field.NewInt32(), OpCodes.Ceq);
        }

        /// <summary>
        /// 不相等
        /// </summary>
        /// <param name="field"></param>
        /// <param name="value"></param>
        /// <returns></returns>
        public static FieldBoolean operator !=(CanCompute<T> field, CanCompute<Decimal> value)
        {
            return ManagerGX.Comparer(
                ManagerGX.Comparer(field, value, OpCodes.Ceq),
                field.NewInt32(), OpCodes.Ceq);
        }

        /// <summary>
        /// 不相等
        /// </summary>
        /// <param name="field"></param>
        /// <param name="value"></param>
        /// <returns></returns>
        public static FieldBoolean operator !=(CanCompute<T> field, CanCompute<Double> value)
        {
            return ManagerGX.Comparer(
                ManagerGX.Comparer(field, value, OpCodes.Ceq),
                field.NewInt32(), OpCodes.Ceq);
        }

        /// <summary>
        /// 不相等
        /// </summary>
        /// <param name="field"></param>
        /// <param name="value"></param>
        /// <returns></returns>
        public static FieldBoolean operator !=(CanCompute<T> field, CanCompute<Single> value)
        {
            return ManagerGX.Comparer(
                ManagerGX.Comparer(field, value, OpCodes.Ceq),
                field.NewInt32(), OpCodes.Ceq);
        }

        /// <summary>
        /// 不相等
        /// </summary>
        /// <param name="field"></param>
        /// <param name="value"></param>
        /// <returns></returns>
        public static FieldBoolean operator !=(CanCompute<T> field, CanCompute<Int64> value)
        {
            return ManagerGX.Comparer(
                ManagerGX.Comparer(field, value, OpCodes.Ceq),
                field.NewInt32(), OpCodes.Ceq);
        }

        /// <summary>
        /// 不相等
        /// </summary>
        /// <param name="field"></param>
        /// <param name="value"></param>
        /// <returns></returns>
        public static FieldBoolean operator !=(CanCompute<T> field, CanCompute<Int32> value)
        {
            return ManagerGX.Comparer(
                ManagerGX.Comparer(field, value, OpCodes.Ceq),
                field.NewInt32(), OpCodes.Ceq);
        }

        #endregion

        #region 相加

        /// <summary>
        /// 相加
        /// </summary>
        /// <param name="field"></param>
        /// <param name="value"></param>
        /// <returns></returns>
        public static CanCompute<Decimal> operator +(CanCompute<T> field, Decimal value)
        {
            return ManagerGX.Compute<T, Decimal>(field, value, OpCodes.Add);
        }

        /// <summary>
        /// 相加
        /// </summary>
        /// <param name="field"></param>
        /// <param name="value"></param>
        /// <returns></returns>
        public static CanCompute<Double> operator +(CanCompute<T> field, Double value)
        {
            return ManagerGX.Compute<T, Double>(field, value, OpCodes.Add);
        }

        /// <summary>
        /// 相加
        /// </summary>
        /// <param name="field"></param>
        /// <param name="value"></param>
        /// <returns></returns>
        public static CanCompute<Single> operator +(CanCompute<T> field, Single value)
        {
            return ManagerGX.Compute<T, Single>(field, value, OpCodes.Add);
        }

        /// <summary>
        /// 相加
        /// </summary>
        /// <param name="field"></param>
        /// <param name="value"></param>
        /// <returns></returns>
        public static CanCompute<Int64> operator +(CanCompute<T> field, Int64 value)
        {
            return ManagerGX.Compute<T, Int64>(field, value, OpCodes.Add);
        }

        /// <summary>
        /// 相加
        /// </summary>
        /// <param name="field"></param>
        /// <param name="value"></param>
        /// <returns></returns>
        public static CanCompute<Int32> operator +(CanCompute<T> field, Int32 value)
        {
            return ManagerGX.Compute<T, Int32>(field, value, OpCodes.Add);
        }

        /// <summary>
        /// 相加
        /// </summary>
        /// <param name="field"></param>
        /// <param name="value"></param>
        /// <returns></returns>
        public static FieldManager operator +(CanCompute<T> field, LocalBuilder value)
        {
            return ManagerGX.Compute<T>(field, value, OpCodes.Add);
        }

        /// <summary>
        /// 相加
        /// </summary>
        /// <param name="field"></param>
        /// <param name="value"></param>
        /// <returns></returns>
        public static CanCompute<Decimal> operator +(CanCompute<T> field, CanCompute<Decimal> value)
        {
            return ManagerGX.Compute<T, Decimal>(field, value, OpCodes.Add);
        }

        /// <summary>
        /// 相加
        /// </summary>
        /// <param name="field"></param>
        /// <param name="value"></param>
        /// <returns></returns>
        public static CanCompute<Double> operator +(CanCompute<T> field, CanCompute<Double> value)
        {
            return ManagerGX.Compute<T, Double>(field, value, OpCodes.Add);
        }

        /// <summary>
        /// 相加
        /// </summary>
        /// <param name="field"></param>
        /// <param name="value"></param>
        /// <returns></returns>
        public static CanCompute<Single> operator +(CanCompute<T> field, CanCompute<Single> value)
        {
            return ManagerGX.Compute<T, Single>(field, value, OpCodes.Add);
        }

        /// <summary>
        /// 相加
        /// </summary>
        /// <param name="field"></param>
        /// <param name="value"></param>
        /// <returns></returns>
        public static CanCompute<Int64> operator +(CanCompute<T> field, CanCompute<Int64> value)
        {
            return ManagerGX.Compute<T, Int64>(field, value, OpCodes.Add);
        }

        /// <summary>
        /// 相加
        /// </summary>
        /// <param name="field"></param>
        /// <param name="value"></param>
        /// <returns></returns>
        public static CanCompute<Int32> operator +(CanCompute<T> field, CanCompute<Int32> value)
        {
            return ManagerGX.Compute<T, Int32>(field, value, OpCodes.Add);
        }

        #endregion

        #region 相减

        /// <summary>
        /// 相减
        /// </summary>
        /// <param name="field"></param>
        /// <param name="value"></param>
        /// <returns></returns>
        public static CanCompute<Decimal> operator -(CanCompute<T> field, Decimal value)
        {
            return ManagerGX.Compute<T, Decimal>(field, value, OpCodes.Sub);
        }

        /// <summary>
        /// 相减
        /// </summary>
        /// <param name="field"></param>
        /// <param name="value"></param>
        /// <returns></returns>
        public static CanCompute<Double> operator -(CanCompute<T> field, Double value)
        {
            return ManagerGX.Compute<T, Double>(field, value, OpCodes.Sub);
        }

        /// <summary>
        /// 相减
        /// </summary>
        /// <param name="field"></param>
        /// <param name="value"></param>
        /// <returns></returns>
        public static CanCompute<Single> operator -(CanCompute<T> field, Single value)
        {
            return ManagerGX.Compute<T, Single>(field, value, OpCodes.Sub);
        }

        /// <summary>
        /// 相减
        /// </summary>
        /// <param name="field"></param>
        /// <param name="value"></param>
        /// <returns></returns>
        public static CanCompute<Int64> operator -(CanCompute<T> field, Int64 value)
        {
            return ManagerGX.Compute<T, Int64>(field, value, OpCodes.Sub);
        }

        /// <summary>
        /// 相减
        /// </summary>
        /// <param name="field"></param>
        /// <param name="value"></param>
        /// <returns></returns>
        public static CanCompute<Int32> operator -(CanCompute<T> field, Int32 value)
        {
            return ManagerGX.Compute<T, Int32>(field, value, OpCodes.Sub);
        }

        /// <summary>
        /// 相减
        /// </summary>
        /// <param name="field"></param>
        /// <param name="value"></param>
        /// <returns></returns>
        public static FieldManager operator -(CanCompute<T> field, LocalBuilder value)
        {
            return ManagerGX.Compute(field, value, OpCodes.Sub);
        }

        /// <summary>
        /// 相减
        /// </summary>
        /// <param name="field"></param>
        /// <param name="value"></param>
        /// <returns></returns>
        public static CanCompute<Decimal> operator -(CanCompute<T> field, CanCompute<Decimal> value)
        {
            return ManagerGX.Compute<T, Decimal>(field, value, OpCodes.Sub);
        }

        /// <summary>
        /// 相减
        /// </summary>
        /// <param name="field"></param>
        /// <param name="value"></param>
        /// <returns></returns>
        public static CanCompute<Double> operator -(CanCompute<T> field, CanCompute<Double> value)
        {
            return ManagerGX.Compute<T, Double>(field, value, OpCodes.Sub);
        }

        /// <summary>
        /// 相减
        /// </summary>
        /// <param name="field"></param>
        /// <param name="value"></param>
        /// <returns></returns>
        public static CanCompute<Single> operator -(CanCompute<T> field, CanCompute<Single> value)
        {
            return ManagerGX.Compute<T, Single>(field, value, OpCodes.Sub);
        }

        /// <summary>
        /// 相减
        /// </summary>
        /// <param name="field"></param>
        /// <param name="value"></param>
        /// <returns></returns>
        public static CanCompute<Int64> operator -(CanCompute<T> field, CanCompute<Int64> value)
        {
            return ManagerGX.Compute<T, Int64>(field, value, OpCodes.Sub);
        }

        /// <summary>
        /// 相减
        /// </summary>
        /// <param name="field"></param>
        /// <param name="value"></param>
        /// <returns></returns>
        public static CanCompute<Int32> operator -(CanCompute<T> field, CanCompute<Int32> value)
        {
            return ManagerGX.Compute<T, Int32>(field, value, OpCodes.Sub);
        }

        #endregion

        #region 相乘

        /// <summary>
        /// 相乘
        /// </summary>
        /// <param name="field"></param>
        /// <param name="value"></param>
        /// <returns></returns>
        public static CanCompute<Decimal> operator *(CanCompute<T> field, Decimal value)
        {
            return ManagerGX.Compute<T, Decimal>(field, value, OpCodes.Mul);
        }

        /// <summary>
        /// 相乘
        /// </summary>
        /// <param name="field"></param>
        /// <param name="value"></param>
        /// <returns></returns>
        public static CanCompute<Double> operator *(CanCompute<T> field, Double value)
        {
            return ManagerGX.Compute<T, Double>(field, value, OpCodes.Mul);
        }

        /// <summary>
        /// 相乘
        /// </summary>
        /// <param name="field"></param>
        /// <param name="value"></param>
        /// <returns></returns>
        public static CanCompute<Single> operator *(CanCompute<T> field, Single value)
        {
            return ManagerGX.Compute<T, Single>(field, value, OpCodes.Mul);
        }

        /// <summary>
        /// 相乘
        /// </summary>
        /// <param name="field"></param>
        /// <param name="value"></param>
        /// <returns></returns>
        public static CanCompute<Int64> operator *(CanCompute<T> field, Int64 value)
        {
            return ManagerGX.Compute<T, Int64>(field, value, OpCodes.Mul);
        }

        /// <summary>
        /// 相乘
        /// </summary>
        /// <param name="field"></param>
        /// <param name="value"></param>
        /// <returns></returns>
        public static CanCompute<Int32> operator *(CanCompute<T> field, Int32 value)
        {
            return ManagerGX.Compute<T, Int32>(field, value, OpCodes.Mul);
        }

        /// <summary>
        /// 相乘
        /// </summary>
        /// <param name="field"></param>
        /// <param name="value"></param>
        /// <returns></returns>
        public static FieldManager operator *(CanCompute<T> field, LocalBuilder value)
        {
            return ManagerGX.Compute(field, value, OpCodes.Mul);
        }

        /// <summary>
        /// 相乘
        /// </summary>
        /// <param name="field"></param>
        /// <param name="value"></param>
        /// <returns></returns>
        public static CanCompute<Decimal> operator *(CanCompute<T> field, CanCompute<Decimal> value)
        {
            return ManagerGX.Compute<T, Decimal>(field, value, OpCodes.Mul);
        }

        /// <summary>
        /// 相乘
        /// </summary>
        /// <param name="field"></param>
        /// <param name="value"></param>
        /// <returns></returns>
        public static CanCompute<Double> operator *(CanCompute<T> field, CanCompute<Double> value)
        {
            return ManagerGX.Compute<T, Double>(field, value, OpCodes.Mul);
        }

        /// <summary>
        /// 相乘
        /// </summary>
        /// <param name="field"></param>
        /// <param name="value"></param>
        /// <returns></returns>
        public static CanCompute<Single> operator *(CanCompute<T> field, CanCompute<Single> value)
        {
            return ManagerGX.Compute<T, Single>(field, value, OpCodes.Mul);
        }

        /// <summary>
        /// 相乘
        /// </summary>
        /// <param name="field"></param>
        /// <param name="value"></param>
        /// <returns></returns>
        public static CanCompute<Int64> operator *(CanCompute<T> field, CanCompute<Int64> value)
        {
            return ManagerGX.Compute<T, Int64>(field, value, OpCodes.Mul);
        }

        /// <summary>
        /// 相乘
        /// </summary>
        /// <param name="field"></param>
        /// <param name="value"></param>
        /// <returns></returns>
        public static CanCompute<Int32> operator *(CanCompute<T> field, CanCompute<Int32> value)
        {
            return ManagerGX.Compute<T, Int32>(field, value, OpCodes.Mul);
        }

        #endregion

        #region 相除

        /// <summary>
        /// 相除
        /// </summary>
        /// <param name="field"></param>
        /// <param name="value"></param>
        /// <returns></returns>
        public static CanCompute<Decimal> operator /(CanCompute<T> field, Decimal value)
        {
            return ManagerGX.Compute<T, Decimal>(field, value, OpCodes.Rem);
        }

        /// <summary>
        /// 相除
        /// </summary>
        /// <param name="field"></param>
        /// <param name="value"></param>
        /// <returns></returns>
        public static CanCompute<Double> operator /(CanCompute<T> field, Double value)
        {
            return ManagerGX.Compute<T, Double>(field, value, OpCodes.Rem);
        }

        /// <summary>
        /// 相除
        /// </summary>
        /// <param name="field"></param>
        /// <param name="value"></param>
        /// <returns></returns>
        public static CanCompute<Single> operator /(CanCompute<T> field, Single value)
        {
            return ManagerGX.Compute<T, Single>(field, value, OpCodes.Rem);
        }

        /// <summary>
        /// 相除
        /// </summary>
        /// <param name="field"></param>
        /// <param name="value"></param>
        /// <returns></returns>
        public static CanCompute<Int64> operator /(CanCompute<T> field, Int64 value)
        {
            return ManagerGX.Compute<T, Int64>(field, value, OpCodes.Rem);
        }

        /// <summary>
        /// 相除
        /// </summary>
        /// <param name="field"></param>
        /// <param name="value"></param>
        /// <returns></returns>
        public static CanCompute<Int32> operator /(CanCompute<T> field, Int32 value)
        {
            return ManagerGX.Compute<T, Int32>(field, value, OpCodes.Rem);
        }

        /// <summary>
        /// 相除
        /// </summary>
        /// <param name="field"></param>
        /// <param name="value"></param>
        /// <returns></returns>
        public static FieldManager operator /(CanCompute<T> field, LocalBuilder value)
        {
            return ManagerGX.Compute(field, value, OpCodes.Rem);
        }

        /// <summary>
        /// 相除
        /// </summary>
        /// <param name="field"></param>
        /// <param name="value"></param>
        /// <returns></returns>
        public static CanCompute<Decimal> operator /(CanCompute<T> field, CanCompute<Decimal> value)
        {
            return ManagerGX.Compute<T, Decimal>(field, value, OpCodes.Rem);
        }

        /// <summary>
        /// 相除
        /// </summary>
        /// <param name="field"></param>
        /// <param name="value"></param>
        /// <returns></returns>
        public static CanCompute<Double> operator /(CanCompute<T> field, CanCompute<Double> value)
        {
            return ManagerGX.Compute<T, Double>(field, value, OpCodes.Rem);
        }

        /// <summary>
        /// 相除
        /// </summary>
        /// <param name="field"></param>
        /// <param name="value"></param>
        /// <returns></returns>
        public static CanCompute<Single> operator /(CanCompute<T> field, CanCompute<Single> value)
        {
            return ManagerGX.Compute<T, Single>(field, value, OpCodes.Rem);
        }

        /// <summary>
        /// 相除
        /// </summary>
        /// <param name="field"></param>
        /// <param name="value"></param>
        /// <returns></returns>
        public static CanCompute<Int64> operator /(CanCompute<T> field, CanCompute<Int64> value)
        {
            return ManagerGX.Compute<T, Int64>(field, value, OpCodes.Rem);
        }

        /// <summary>
        /// 相除
        /// </summary>
        /// <param name="field"></param>
        /// <param name="value"></param>
        /// <returns></returns>
        public static CanCompute<Int32> operator /(CanCompute<T> field, CanCompute<Int32> value)
        {
            return ManagerGX.Compute<T, Int32>(field, value, OpCodes.Rem);
        }

        #endregion
    }
}
