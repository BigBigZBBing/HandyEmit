using System;
using System.Collections.Generic;
using System.Text;

namespace ILWheatBread.Attributes
{
    [AttributeUsage(AttributeTargets.Class, Inherited = true)]
    public class EmitSerialization : Attribute
    {
    }
}
