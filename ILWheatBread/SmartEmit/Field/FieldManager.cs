using ILWheatBread.SmartEmit.Field;
using System;
using System.Collections.Generic;
using System.Reflection;
using System.Reflection.Emit;
using System.Text;

namespace ILWheatBread.SmartEmit
{
    public partial class FieldManager<T> : FieldManager
    {
        internal FieldManager(LocalBuilder stack, ILGenerator generator) : base(stack, generator, typeof(T))
        {
        }

        public FieldObject AsObject()
        {

            var temp = this.NewObject();
            Pop();
            if (OriginType.IsValueType)
            {
                Emit(OpCodes.Box, typeof(Object));
            }
            else
            {
                Emit(OpCodes.Castclass, typeof(Object));
            }
            temp.Push();
            return temp;
        }

        public static implicit operator LocalBuilder(FieldManager<T> field) => field.stack;
        public static implicit operator FieldString(FieldManager<T> field) => new FieldString(field.stack, field.generator);
        public static implicit operator FieldBoolean(FieldManager<T> field) => new FieldBoolean(field.stack, field.generator);
        public static implicit operator FieldDateTime(FieldManager<T> field) => new FieldDateTime(field.stack, field.generator);
        public static implicit operator FieldInt32(FieldManager<T> field) => new FieldInt32(field.stack, field.generator);
        public static implicit operator FieldInt64(FieldManager<T> field) => new FieldInt64(field.stack, field.generator);
        public static implicit operator FieldFloat(FieldManager<T> field) => new FieldFloat(field.stack, field.generator);
        public static implicit operator FieldDouble(FieldManager<T> field) => new FieldDouble(field.stack, field.generator);
        public static implicit operator FieldDecimal(FieldManager<T> field) => new FieldDecimal(field.stack, field.generator);
        public static implicit operator CanCompute<Int32>(FieldManager<T> field) => new CanCompute<Int32>(field.stack, field.generator);
        public static implicit operator CanCompute<Int64>(FieldManager<T> field) => new CanCompute<Int64>(field.stack, field.generator);
        public static implicit operator CanCompute<Single>(FieldManager<T> field) => new CanCompute<Single>(field.stack, field.generator);
        public static implicit operator CanCompute<Double>(FieldManager<T> field) => new CanCompute<Double>(field.stack, field.generator);
        public static implicit operator CanCompute<Decimal>(FieldManager<T> field) => new CanCompute<Decimal>(field.stack, field.generator);
    }

    /// <summary>
    /// 自变量管理方案
    /// </summary>
    public class FieldManager : EmitBasic
    {
        internal LocalBuilder stack;
        internal Type OriginType;

        internal FieldManager(LocalBuilder stack, ILGenerator generator, Type type) : base(generator)
        {
            this.stack = stack;
            this.OriginType = type;
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
    }
}
