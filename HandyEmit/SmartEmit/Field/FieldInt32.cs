using System;
using System.Collections.Generic;
using System.Reflection.Emit;
using System.Text;

namespace HandyEmit.SmartEmit.Field
{
    public class FieldInt32 : CanCompute<Int32>
    {
        internal FieldInt32(LocalBuilder stack, ILGenerator il) : base(stack, il)
        {
        }

        public void Cover()
        {

        }
    }
}
