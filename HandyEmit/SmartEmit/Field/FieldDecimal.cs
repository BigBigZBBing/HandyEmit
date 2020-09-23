using System;
using System.Collections.Generic;
using System.Reflection.Emit;
using System.Text;

namespace HandyEmit.SmartEmit.Field
{
    public class FieldDecimal : CanCompute<Decimal>
    {
        internal FieldDecimal(LocalBuilder stack, ILGenerator il) : base(stack, il)
        {
        }
    }
}
