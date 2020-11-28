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
    }
}
