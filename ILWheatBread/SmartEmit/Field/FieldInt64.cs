using System;
using System.Collections.Generic;
using System.Reflection.Emit;
using System.Text;

namespace ILWheatBread.SmartEmit.Field
{
    public class FieldInt64 : CanCompute<Int64>
    {
        internal FieldInt64(LocalBuilder stack, ILGenerator generator) : base(stack, generator)
        {
        }

        //internal Func<LocalBuilder, Int64> ToCodeDomLevel = ManagerGX.ToCodeDomeLevel<Func<LocalBuilder, Int64>>();

        /// <summary>
        /// 返回一个原始值
        /// </summary>
        /// <returns></returns>
        //public Int64 ToInt64()
        //{
        //    return ToCodeDomLevel(this);
        //}
    }
}
