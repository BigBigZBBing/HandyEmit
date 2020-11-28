using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection.Emit;
using System.Text;
using System.Threading.Tasks;

namespace ILWheatBread.SmartEmit.Field
{
    /// <summary>
    /// 数组内存管理方案
    /// </summary>
    public class FieldArray<T> : ArrayManager
    {
        public FieldArray(LocalBuilder stack, ILGenerator generator, Int32 Length) : base(stack, generator, typeof(T), Length)
        {

        }

        /// <summary>
        /// 判断元素是否存在数组中
        /// </summary>
        /// <param name="value"></param>
        /// <returns></returns>
        public FieldBoolean Exists(LocalBuilder value)
        {
            var result = this.NewBoolean();
            Label trueTo = DefineLabel();
            Label falseTo = DefineLabel();
            this.For(0, GetLength(), build =>
            {
                GetValue(build);
                Emit(OpCodes.Ldloc_S, value);
                Emit(OpCodes.Ceq);
                Emit(OpCodes.Brfalse, falseTo);
                Emit(OpCodes.Ldc_I4_1);
                result.PushOn();
                Emit(OpCodes.Br_S, trueTo);
                MarkLabel(falseTo);
            });
            MarkLabel(trueTo);
            return result;
        }

        public static implicit operator LocalBuilder(FieldArray<T> field) => field.stack;
    }

    public class ArrayManager : EmitBasic
    {
        internal LocalBuilder stack { get; set; }

        public Type OriginType { get; set; }

        public Int32 Length { get; set; }

        internal FieldInt32 ILLength { get; set; }

        public ArrayManager(LocalBuilder stack, ILGenerator generator, Type type, Int32 Length) : base(generator)
        {
            this.stack = stack;
            OriginType = type;
            this.Length = Length;
        }

        /// <summary>
        /// 用Dom方式获取数组指定索引
        /// 返回永远是NULL 这里是弹出计算堆方式
        /// </summary>
        /// <param name="index"></param>
        /// <returns></returns>
        public LocalBuilder this[Int32 index]
        {
            get
            {
                Emit(OpCodes.Ldloc_S, stack);
                this.Int32Map(index);
                Emit(OpCodes.Ldelem_Ref, stack);
                return null;
            }
            set
            {
                Emit(OpCodes.Ldloc_S, stack);
                this.Int32Map(index);
                Emit(OpCodes.Ldloc_S, value);
                Emit(OpCodes.Stelem_Ref);
            }
        }

        /// <summary>
        /// 获取指定索引的内存单元的值
        /// </summary>
        /// <param name="index"></param>
        public void GetValue(CanCompute<Int32> index)
        {
            Emit(OpCodes.Ldloc_S, stack);
            Emit(OpCodes.Ldloc_S, index);
            this.PopArray(OriginType);
        }

        /// <summary>
        /// 给指定索引的内存单元赋值或替换
        /// </summary>
        /// <param name="index"></param>
        /// <param name="value"></param>
        public void SetValue(CanCompute<Int32> index, LocalBuilder value)
        {
            Emit(OpCodes.Ldloc_S, stack);
            Emit(OpCodes.Ldloc_S, index);
            Emit(OpCodes.Ldloc_S, value);
            this.PushArray(OriginType);
        }

        /// <summary>
        /// 获取数组长度
        /// </summary>
        /// <returns></returns>
        public FieldInt32 GetLength()
        {
            if ((object)ILLength == null)
            {
                ILLength = this.NewInt32(Length);
            }
            return ILLength;
        }
    }
}
