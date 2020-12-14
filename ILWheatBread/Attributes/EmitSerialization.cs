using System;

namespace ILWheatBread.Attributes
{
    [AttributeUsage(AttributeTargets.Class, Inherited = true)]
    public class EmitSerialization : Attribute
    {
    }
}
