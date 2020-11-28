using System;
using System.Collections.Generic;
using System.Reflection.Emit;
using System.Text;

namespace HandyEmit.SmartEmit.Field
{
    public class FieldDateTime : FieldManager<DateTime>
    {
        internal FieldDateTime(LocalBuilder stack, ILGenerator generator) : base(stack, generator)
        {
        }

        #region 相等

        /// <summary>
        /// 相等
        /// </summary>
        /// <param name="field"></param>
        /// <param name="value"></param>
        /// <returns></returns>
        public static FieldBoolean operator ==(FieldDateTime field, DateTime value)
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
        public static FieldBoolean operator ==(FieldDateTime field, FieldDateTime value)
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
        public static FieldBoolean operator !=(FieldDateTime field, DateTime value)
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
        public static FieldBoolean operator !=(FieldDateTime field, FieldDateTime value)
        {
            return ManagerGX.Comparer(
               ManagerGX.Comparer(field, value, OpCodes.Ceq),
               field.NewInt32(), OpCodes.Ceq);
        }

        #endregion

    }
}
