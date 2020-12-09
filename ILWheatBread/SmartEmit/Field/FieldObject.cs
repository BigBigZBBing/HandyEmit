using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection.Emit;
using System.Text;
using System.Threading.Tasks;

namespace ILWheatBread.SmartEmit.Field
{
    public class FieldObject : FieldManager<Object>
    {
        internal FieldObject(LocalBuilder stack, ILGenerator generator) : base(stack, generator)
        {
        }


    }
}
