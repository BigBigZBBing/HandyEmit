using System;
using System.Collections.Generic;
using System.Reflection.Emit;
using System.Text;

namespace HandyEmit.SmartEmit.Field
{
    public class FieldFloat : CanCompute<Single>
    {
        internal FieldFloat(LocalBuilder stack, ILGenerator generator) : base(stack, generator)
        {
        }
    }
}
