using HandyEmit.SmartEmit.Field;
using System;
using System.Collections.Generic;
using System.Reflection;
using System.Reflection.Emit;
using System.Text;

namespace HandyEmit.SmartEmit
{
    /// <summary>
    /// 自变量管理方案
    /// </summary>
    public partial class FieldManager<T> : EmitBasic
    {
        internal LocalBuilder stack;
        internal ILGenerator il;
        internal Type type;

        internal FieldManager(LocalBuilder stack, ILGenerator il) : base(il)
        {
            this.stack = stack;
            this.il = il;
            this.type = typeof(T);
        }

        /// <summary>
        /// 从内存中推入计算堆
        /// </summary>
        public void Ldloc()
        {
            base.Emit(OpCodes.Ldloc_S, this.stack);
        }

        /// <summary>
        /// 推出计算堆到内存
        /// </summary>
        public void Stloc()
        {
            base.Emit(OpCodes.Stloc_S, this.stack);
        }


        public static implicit operator LocalBuilder(FieldManager<T> field) => field.stack;
        public static implicit operator FieldBoolean(FieldManager<T> field) => new FieldBoolean(field.stack, field.il);
        public static implicit operator FieldDateTime(FieldManager<T> field) => new FieldDateTime(field.stack, field.il);
        public static implicit operator FieldString(FieldManager<T> field) => new FieldString(field.stack, field.il);
        public static implicit operator FieldInt32(FieldManager<T> field) => new FieldInt32(field.stack, field.il);
        public static implicit operator FieldInt64(FieldManager<T> field) => new FieldInt64(field.stack, field.il);
        public static implicit operator FieldFloat(FieldManager<T> field) => new FieldFloat(field.stack, field.il);
        public static implicit operator FieldDouble(FieldManager<T> field) => new FieldDouble(field.stack, field.il);
        public static implicit operator FieldDecimal(FieldManager<T> field) => new FieldDecimal(field.stack, field.il);
        public static implicit operator CanCompute<Int32>(FieldManager<T> field) => new CanCompute<Int32>(field.stack, field.il);
        public static implicit operator CanCompute<Int64>(FieldManager<T> field) => new CanCompute<Int64>(field.stack, field.il);
        public static implicit operator CanCompute<Single>(FieldManager<T> field) => new CanCompute<Single>(field.stack, field.il);
        public static implicit operator CanCompute<Double>(FieldManager<T> field) => new CanCompute<Double>(field.stack, field.il);
        public static implicit operator CanCompute<Decimal>(FieldManager<T> field) => new CanCompute<Decimal>(field.stack, field.il);
    }
}
