using System;
using System.Collections.Generic;
using System.Text;

namespace HandyEmit.Attributes
{
    [AttributeUsage(AttributeTargets.Class, Inherited = true)]
    public class EmitSerialization : Attribute
    {
    }
}
