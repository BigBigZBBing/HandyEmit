using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection.Emit;
using System.Text;
using HandyEmit.SmartEmit.Mapping;

namespace HandyEmit.SmartEmit
{
    /// <summary>
    /// 链表管理方案
    /// </summary>
    /// <typeparam name="T"></typeparam>
    public class ListManager<T>
    {
        Int32 maxlen;
        LocalBuilder stack;
        ILGenerator il;

        internal ListManager(LocalBuilder stack, ILGenerator il, Int32 maxlen = 0)
        {
            this.stack = stack;
            this.il = il;
            this.maxlen = 0;
        }

        /// <summary>
        /// 推入计算堆
        /// </summary>
        public void PushIn()
        {
            il.Emit(OpCodes.Ldloc_S, stack);
        }

        /// <summary>
        /// 推出计算堆
        /// </summary>
        public void PushOn()
        {
            il.Emit(OpCodes.Stloc_S, stack);
        }

        /// <summary>
        /// 新增
        /// </summary>
        public void Add()
        {
            il.Emit(OpCodes.Ldloc_S, stack);
        }

        /// <summary>
        /// 删除链表子链
        /// </summary>
        public FieldManager<Boolean> Remove(T value)
        {
            var res = this.il.NewBoolean();
            this.PushIn();
            this.il.Emit(OpCodes.Callvirt, BaseConst<T>._ListRemove);
            res.Stloc();
            return res;
        }

        /// <summary>
        /// 清空链表
        /// </summary>
        public void Clear()
        {
            this.PushIn();
            this.il.Emit(OpCodes.Callvirt, BaseConst<T>._ListClear);
        }

        /// <summary>
        /// 获取总数
        /// </summary>
        /// <returns></returns>
        public FieldManager<Int32> Count()
        {
            return this.il.NewInt32(maxlen);
        }

        /// <summary>
        /// 判断是否存在
        /// </summary>
        /// <returns></returns>
        public FieldManager<Boolean> Contains(T value)
        {
            var res = this.il.NewBoolean();
            this.PushIn();
            this.il.Emit(OpCodes.Callvirt, BaseConst<T>._ListContains);
            res.Stloc();
            return res;
        }

        /// <summary>
        /// 类型转换
        /// </summary>
        /// <param name="field"></param>
        public static implicit operator LocalBuilder(ListManager<T> field) => field.stack;

    }
}
