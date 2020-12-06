using System;
using System.Collections.Generic;
using System.Reflection.Emit;
using System.Text;

namespace ILWheatBread.SmartEmit.Field
{
    public class FieldDouble : CanCompute<Double>
    {
        internal FieldDouble(LocalBuilder stack, ILGenerator generator) : base(stack, generator)
        {
        }


        //internal Func<LocalBuilder, Double> ToCodeDomLevel = ManagerGX.ToCodeDomeLevel<Func<LocalBuilder, Double>>();

        /// <summary>
        /// 返回一个原始值
        /// </summary>
        /// <returns></returns>
        //public Double ToDouble()
        //{
        //    return ToCodeDomLevel(this);
        //}
    }
}
