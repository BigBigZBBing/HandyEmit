using System;
using System.Collections.Generic;
using System.Reflection.Emit;
using System.Text;

namespace ILWheatBread.SmartEmit.Field
{
    public class FieldFloat : CanCompute<Single>
    {
        internal FieldFloat(LocalBuilder stack, ILGenerator generator) : base(stack, generator)
        {
        }

        //internal Func<LocalBuilder, Single> ToCodeDomLevel = ManagerGX.ToCodeDomeLevel<Func<LocalBuilder, Single>>();

        /// <summary>
        /// 返回一个原始值
        /// </summary>
        /// <returns></returns>
        //public Single ToSingle()
        //{
        //    return ToCodeDomLevel(this);
        //}
    }
}
