using System;
using System.Collections.Generic;
using System.Reflection.Emit;
using System.Text;

namespace ILWheatBread.SmartEmit.Field
{
    public class FieldDecimal : CanCompute<Decimal>
    {
        internal FieldDecimal(LocalBuilder stack, ILGenerator generator) : base(stack, generator)
        {
        }

        //internal Func<LocalBuilder, Decimal> ToCodeDomLevel = ManagerGX.ToCodeDomeLevel<Func<LocalBuilder, Decimal>>();

        /// <summary>
        /// 返回一个原始值
        /// </summary>
        /// <returns></returns>
        //public Decimal ToDecimal()
        //{
        //    return ToCodeDomLevel(this);
        //}
    }
}
