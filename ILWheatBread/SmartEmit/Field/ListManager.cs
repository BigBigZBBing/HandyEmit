using System.Collections.Generic;
using System.Reflection.Emit;

namespace ILWheatBread.SmartEmit.Field
{
    public class FieldList<T> : FieldManager<List<T>>
    {
        public FieldList(LocalBuilder stack, ILGenerator generator) : base(stack, generator)
        {
        }

        public void Add(LocalBuilder value)
        {
            Invoke("Add", value);
        }

        public void Contains(LocalBuilder value)
        {
            Invoke("Contains", value);
        }

        public void RemoveAt(LocalBuilder value)
        {
            Invoke("RemoveAt", value);
        }

        public FieldInt32 GetCount()
        {
            return new FieldInt32(Invoke("get_Count").ReturnRef(), generator);
        }
    }
}
