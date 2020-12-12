using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection.Emit;
using System.Text;
using System.Threading.Tasks;

namespace ILWheatBread.SmartEmit.Field
{
    /// <summary>
    /// 数组变量管理方案
    /// </summary>
    public class FieldArray<T> : FieldManager<T[]>
    {
        internal Int32 Length { get; set; }
        internal FieldInt32 ILLength { get; set; }

        internal FieldArray(LocalBuilder stack, ILGenerator generator, Int32 Length) : base(stack, generator)
        {
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
                Emit(OpCodes.Ldloc_S, instance);
                this.Int32Map(index);
                this.PopArray(identity);
                return null;
            }
            set
            {
                Emit(OpCodes.Ldloc_S, instance);
                this.Int32Map(index);
                Emit(OpCodes.Ldloc_S, value);
                this.PushArray(identity);
            }
        }

        /// <summary>
        /// 获取指定索引的内存单元的值
        /// </summary>
        /// <param name="index"></param>
        public void GetValue(CanCompute<Int32> index)
        {
            Emit(OpCodes.Ldloc_S, instance);
            Emit(OpCodes.Ldloc_S, index);
            this.PopArray(identity);
        }

        /// <summary>
        /// 给指定索引的内存单元赋值或替换
        /// </summary>
        /// <param name="index"></param>
        /// <param name="value"></param>
        public void SetValue(CanCompute<Int32> index, LocalBuilder value)
        {
            Emit(OpCodes.Ldloc_S, instance);
            Emit(OpCodes.Ldloc_S, index);
            Emit(OpCodes.Ldloc_S, value);
            this.PushArray(identity);
        }

        /// <summary>
        /// 判断元素是否存在数组中
        /// </summary>
        /// <param name="value">判断的元素的指针</param>
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
                result.Input();
                Emit(OpCodes.Br_S, trueTo);
                MarkLabel(falseTo);
            });
            MarkLabel(trueTo);
            return result;
        }

        /// <summary>
        /// 寻找元素在数组中的位置
        /// </summary>
        /// <param name="value">判断的元素的指针</param>
        /// <returns></returns>
        public FieldInt32 FindIndex(LocalBuilder value)
        {
            var result = this.NewInt32(-1);
            Label trueTo = DefineLabel();
            Label falseTo = DefineLabel();
            this.For(0, GetLength(), build =>
            {
                GetValue(build);
                Emit(OpCodes.Ldloc_S, value);
                Emit(OpCodes.Ceq);
                Emit(OpCodes.Brfalse, falseTo);
                build.Output();
                result.Input();
                Emit(OpCodes.Br_S, trueTo);
                MarkLabel(falseTo);
            });
            MarkLabel(trueTo);
            return result;
        }

        /// <summary>
        /// 复制数组(浅克隆)
        /// </summary>
        /// <param name="target">目标数组指针</param>
        /// <param name="length">复制的长度</param>
        public void Copy(FieldArray<T> target, FieldInt32 length)
        {
            this.For(0, length, int1 =>
            {
                var local = DeclareLocal(target.identity);
                GetValue(int1);
                Emit(OpCodes.Stloc_S, local);
                target.SetValue(int1, local);
            });
        }

        /// <summary>
        /// 获取数组长度
        /// </summary>
        /// <returns></returns>
        public FieldInt32 GetLength()
        {
            if ((object)ILLength == null)
            {
                //一般来说长度是有个缓存的  但是创建数组使用的是IL类型的长度 长度会被设定为-1
                if (Length == -1)
                {
                    LocalBuilder temp = DeclareLocal(typeof(Int32));
                    Output();
                    Emit(OpCodes.Ldlen);
                    Emit(OpCodes.Stloc_S, temp);
                    ILLength = new FieldInt32(temp, generator);
                }
                else
                    ILLength = this.NewInt32(Length);
            }
            return ILLength;
        }
    }
}
