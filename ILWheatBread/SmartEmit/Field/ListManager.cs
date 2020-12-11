using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection.Emit;
using System.Text;
using System.Threading.Tasks;

namespace ILWheatBread.SmartEmit.Field
{
    public class FieldList<T> : ListManager
    {
        public FieldList(LocalBuilder stack, ILGenerator generator) : base(stack, generator, typeof(T))
        {
        }

        public void Add(FieldEntity<T> value)
        {
            this.CallvirtMethod<List<T>>("Add", value);
        }

        public void Contains(FieldEntity<T> value)
        {
            this.CallvirtMethod<List<T>>("Contains", value);
        }

        public void RemoveAt(LocalBuilder value)
        {
            this.CallvirtMethod<List<T>>("RemoveAt", value);
        }
    }

    public class ListManager : EmitBasic
    {

        internal LocalBuilder stack { get; set; }

        /// <summary>
        /// 数组初始类型
        /// </summary>
        public Type OriginType { get; set; }

        internal FieldInt32 ILLength { get; set; }

        public ListManager(LocalBuilder stack, ILGenerator generator, Type type) : base(generator)
        {
            this.stack = stack;
            OriginType = type;
        }

        /// <summary>
        /// 从内存中推入计算堆
        /// </summary>
        public void Pop()
        {
            base.Emit(OpCodes.Ldloc_S, this.stack);
        }

        /// <summary>
        /// 推出计算堆到内存
        /// </summary>
        public void Push()
        {
            base.Emit(OpCodes.Stloc_S, this.stack);
        }

        /// <summary>
        /// 获取数组长度
        /// </summary>
        /// <returns></returns>
        public FieldInt32 GetLength()
        {
            return new FieldInt32(this.CallvirtMethod("get_Item", OriginType.GetProperty("Count").PropertyType).ReturnRef(), generator);
        }
    }
}
