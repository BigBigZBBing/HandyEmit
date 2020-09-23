using System;
using System.Collections.Generic;
using System.Reflection.Emit;
using System.Text;

namespace HandyEmit.SmartEmit.Field
{
    public class FieldBoolean : FieldManager<Boolean>
    {
        internal FieldBoolean(LocalBuilder stack, ILGenerator il) : base(stack, il)
        {
        }

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
        //public static FieldBoolean operator ==(FieldBoolean field, LocalBuilder value)
        //{
        //    return ManagerGX.Comparer(field, value, OpCodes.Ceq);
        //}

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
                field.il.NewInt32(), OpCodes.Ceq);
        }

        /// <summary>
        /// 不相等
        /// </summary>
        /// <param name="field"></param>
        /// <param name="value"></param>
        /// <returns></returns>
        //public static FieldBoolean operator !=(FieldBoolean field, LocalBuilder value)
        //{
        //    return ManagerGX.Comparer(
        //        ManagerGX.Comparer(field, value, OpCodes.Ceq),
        //        field.il.NewInt32(), OpCodes.Ceq);
        //}

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
                field.il.NewInt32(), OpCodes.Ceq);
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
            var assert = field.il.NewBoolean();
            var _true = field.il.DefineLabel();
            field.PushLd();
            field.il.Emit(OpCodes.Ldc_I4_1);
            field.il.Emit(OpCodes.Beq_S, _true);
            field.il.Emit(OpCodes.Ldc_I4_0);
            assert.PushSt();
            value.PushLd();
            field.il.Emit(OpCodes.Ldc_I4_1);
            field.il.Emit(OpCodes.Beq_S, _true);
            field.il.MarkLabel(_true);
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
            var assert = field.il.NewBoolean();
            var _false = field.il.DefineLabel();
            field.PushLd();
            field.il.Emit(OpCodes.Ldc_I4_0);
            field.il.Emit(OpCodes.Beq_S, _false);
            value.PushLd();
            field.il.Emit(OpCodes.Ldc_I4_0);
            field.il.Emit(OpCodes.Beq_S, _false);
            field.il.Emit(OpCodes.Ldc_I4_1);
            assert.PushSt();
            field.il.MarkLabel(_false);
            return assert;
        }
    }
}
