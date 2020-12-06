using System;
using System.Collections.Generic;
using System.Reflection.Emit;
using System.Text;

namespace ILWheatBread.SmartEmit.Field
{
    public class FieldInt32 : CanCompute<Int32>
    {
        internal FieldInt32(LocalBuilder stack, ILGenerator generator) : base(stack, generator)
        {
        }

        //internal Func<LocalBuilder, Int32> ToCodeDomLevel = ManagerGX.ToCodeDomeLevel<Func<LocalBuilder, Int32>>();

        /// <summary>
        /// 返回一个原始值
        /// </summary>
        /// <returns></returns>
        //public Int32 ToInt32()
        //{
        //    return ToCodeDomLevel(this);
        //}
    }
}
