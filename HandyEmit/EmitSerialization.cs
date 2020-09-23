using System;
using System.Collections.Generic;
using System.Text;

namespace HandyEmit
{
    [AttributeUsage(AttributeTargets.Class, Inherited = true)]
    public class EmitSerialization : Attribute
    {
    }
}
