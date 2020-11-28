using System;
using System.Collections.Generic;
using System.Reflection.Emit;
using System.Text;

namespace ILWheatBread.SmartEmit.Field
{
    public class FieldString : FieldManager<String>
    {
        internal FieldString(LocalBuilder stack, ILGenerator generator) : base(stack, generator)
        {
        }

        #region 相等

        /// <summary>
        /// 相等
        /// </summary>
        /// <param name="field"></param>
        /// <param name="value"></param>
        /// <returns></returns>
        public static FieldBoolean operator ==(FieldString field, String value)
        {
            return ManagerGX.Comparer(field, value, OpCodes.Ceq);
        }

        /// <summary>
        /// 相等
        /// </summary>
        /// <param name="field"></param>
        /// <param name="value"></param>
        /// <returns></returns>
        //public static FieldBoolean operator ==(FieldString field, LocalBuilder value)
        //{
        //    return ManagerGX.Comparer(field, value, OpCodes.Ceq);
        //}

        /// <summary>
        /// 相等
        /// </summary>
        /// <param name="field"></param>
        /// <param name="value"></param>
        /// <returns></returns>
        public static FieldBoolean operator ==(FieldString field, FieldString value)
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
        public static FieldBoolean operator !=(FieldString field, String value)
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
        //public static FieldBoolean operator !=(FieldString field, LocalBuilder value)
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
        public static FieldBoolean operator !=(FieldString field, FieldString value)
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
        public static FieldString operator +(FieldString field, String value)
        {
            return ManagerGX.Compute(field, value, OpCodes.Add);
        }

        /// <summary>
        /// 相加
        /// </summary>
        /// <param name="field"></param>
        /// <param name="value"></param>
        /// <returns></returns>
        //public static FieldString operator +(FieldString field, LocalBuilder value)
        //{
        //    return ManagerGX.Compute(field, value, OpCodes.Add);
        //}

        /// <summary>
        /// 相加
        /// </summary>
        /// <param name="field"></param>
        /// <param name="value"></param>
        /// <returns></returns>
        public static FieldString operator +(FieldString field, FieldString value)
        {
            return ManagerGX.Compute(field, value, OpCodes.Add);
        }

        #endregion

    }
}
