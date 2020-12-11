﻿using System;
using System.Collections.Generic;
using System.Reflection.Emit;
using System.Text;

namespace ILWheatBread.SmartEmit.Field
{
    public class FieldBoolean : FieldManager<Boolean>
    {
        internal FieldBoolean(LocalBuilder stack, ILGenerator il) : base(stack, il)
        {
        }

        //internal Func<LocalBuilder, Boolean> ToCodeDomLevel = ManagerGX.ToCodeDomeLevel<Func<LocalBuilder, Boolean>>();

        /// <summary>
        /// 返回一个原始值
        /// </summary>
        /// <returns></returns>
        //public Boolean ToBoolean()
        //{
        //    return ToCodeDomLevel(this);
        //}

        #region 相等

        /// <summary>
        /// 相等
        /// </summary>
        /// <param name="field"></param>
        /// <param name="value"></param>
        /// <returns></returns>
        public static FieldBoolean operator ==(FieldBoolean field, Boolean value)
        {
            return ManagerGX.Comparer(field, value, OpCodes.Ceq);
        }

        /// <summary>
        /// 相等
        /// </summary>
        /// <param name="field"></param>
        /// <param name="value"></param>
        /// <returns></returns>
        public static FieldBoolean operator ==(FieldBoolean field, LocalBuilder value)
        {
            return ManagerGX.Comparer(field, value, OpCodes.Ceq);
        }

        /// <summary>
        /// 相等
        /// </summary>
        /// <param name="field"></param>
        /// <param name="value"></param>
        /// <returns></returns>
        public static FieldBoolean operator ==(FieldBoolean field, FieldBoolean value)
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
        public static FieldBoolean operator !=(FieldBoolean field, Boolean value)
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
        public static FieldBoolean operator !=(FieldBoolean field, LocalBuilder value)
        {
            return ManagerGX.Comparer(
                ManagerGX.Comparer(field, value, OpCodes.Ceq),
                field.il.NewInt32(), OpCodes.Ceq);
        }

        /// <summary>
        /// 不相等
        /// </summary>
        /// <param name="field"></param>
        /// <param name="value"></param>
        /// <returns></returns>
        public static FieldBoolean operator !=(FieldBoolean field, FieldBoolean value)
        {
            return ManagerGX.Comparer(
                ManagerGX.Comparer(field, value, OpCodes.Ceq),
                field.NewInt32(), OpCodes.Ceq);
        }

        #endregion

        /// <summary>
        /// 或者
        /// </summary>
        /// <param name="field"></param>
        /// <param name="value"></param>
        /// <returns></returns>
        public static FieldBoolean operator |(FieldBoolean field, FieldBoolean value)
        {
            var assert = field.NewBoolean();
            var _true = field.DefineLabel();
            field.Pop();
            field.Emit(OpCodes.Ldc_I4_1);
            field.Emit(OpCodes.Beq_S, _true);
            field.Emit(OpCodes.Ldc_I4_0);
            assert.Push();
            value.Pop();
            field.Emit(OpCodes.Ldc_I4_1);
            field.Emit(OpCodes.Beq_S, _true);
            field.MarkLabel(_true);
            return assert;
        }

        /// <summary>
        /// 并且
        /// </summary>
        /// <param name="field"></param>
        /// <param name="value"></param>
        /// <returns></returns>
        public static FieldBoolean operator &(FieldBoolean field, FieldBoolean value)
        {
            var assert = field.NewBoolean();
            var _false = field.DefineLabel();
            field.Pop();
            field.Emit(OpCodes.Ldc_I4_0);
            field.Emit(OpCodes.Beq_S, _false);
            value.Pop();
            field.Emit(OpCodes.Ldc_I4_0);
            field.Emit(OpCodes.Beq_S, _false);
            field.Emit(OpCodes.Ldc_I4_1);
            assert.Push();
            field.MarkLabel(_false);
            return assert;
        }
    }
}
