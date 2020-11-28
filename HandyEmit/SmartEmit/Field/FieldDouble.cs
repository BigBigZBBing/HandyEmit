using System;
using System.Collections.Generic;
using System.Reflection.Emit;
using System.Text;

namespace HandyEmit.SmartEmit.Field
{
    public class FieldDouble : CanCompute<Double>
    {
        internal FieldDouble(LocalBuilder stack, ILGenerator generator) : base(stack, generator)
        {
        }
    }
}
