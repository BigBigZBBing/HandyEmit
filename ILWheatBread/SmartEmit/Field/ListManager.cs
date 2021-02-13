using System.Collections.Generic;
using System.Reflection.Emit;
using System.Runtime.CompilerServices;

namespace ILWheatBread.SmartEmit.Field
{
    public class FieldList<T> : FieldManager<List<T>>
    {
        
        public FieldList(LocalBuilder stack, ILGenerator generator) : base(stack, generator)
        {
        }

        
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public void Add(LocalBuilder value)
        {
            Invoke("Add", value);
        }

        
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public void Contains(LocalBuilder value)
        {
            Invoke("Contains", value);
        }

        
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public void RemoveAt(LocalBuilder value)
        {
            Invoke("RemoveAt", value);
        }

        
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public FieldInt32 GetCount()
        {
            return new FieldInt32(Invoke("get_Count").ReturnRef(), generator);
        }
    }
}
