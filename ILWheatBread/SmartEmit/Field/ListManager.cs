using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection.Emit;
using System.Text;
using System.Threading.Tasks;

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

        /// <summary>
        /// 获取链表长度
        /// </summary>
        /// <returns></returns>
        public FieldInt32 GetCount()
        {
            return new FieldInt32(Invoke("get_Count").ReturnRef(), generator);
        }
    }
}
