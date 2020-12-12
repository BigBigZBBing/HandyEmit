using ILWheatBread.SmartEmit.Field;
using ILWheatBread.SmartEmit.Func;
using System;
using System.Collections.Generic;
using System.Reflection;
using System.Reflection.Emit;
using System.Text;

namespace ILWheatBread.SmartEmit
{
    /// <summary>
    /// 字段管理方案(具有身份的变量)
    /// </summary>
    /// <typeparam name="T"></typeparam>
    public partial class FieldManager<T> : VariableManager
    {
        /// <summary>
        /// 变量身份类型
        /// </summary>
        internal Type identity;

        internal FieldManager(LocalBuilder stack, ILGenerator generator) : base(stack, generator)
        {
            identity = typeof(T);
        }

        /// <summary>
        /// 内存值装箱
        /// </summary>
        /// <returns></returns>
        public FieldObject AsObject()
        {
            var temp = this.NewObject();
            Output();
            if (identity.IsValueType)
            {
                Emit(OpCodes.Box, typeof(Object));
            }
            else
            {
                Emit(OpCodes.Castclass, typeof(Object));
            }
            temp.Input();
            return temp;
        }

        /// <summary>
        /// 调用该类型的函数
        /// </summary>
        /// <param name="methodName"></param>
        /// <param name="parameters"></param>
        /// <returns></returns>
        public MethodManager Invoke(String methodName, params LocalBuilder[] parameters)
        {
            return this.CallvirtMethod(methodName, identity, parameters);
        }

        public static implicit operator LocalBuilder(FieldManager<T> field) => field.instance;
        public static implicit operator FieldString(FieldManager<T> field) => new FieldString(field.instance, field.generator);
        public static implicit operator FieldBoolean(FieldManager<T> field) => new FieldBoolean(field.instance, field.generator);
        public static implicit operator FieldDateTime(FieldManager<T> field) => new FieldDateTime(field.instance, field.generator);
        public static implicit operator FieldInt32(FieldManager<T> field) => new FieldInt32(field.instance, field.generator);
        public static implicit operator FieldInt64(FieldManager<T> field) => new FieldInt64(field.instance, field.generator);
        public static implicit operator FieldFloat(FieldManager<T> field) => new FieldFloat(field.instance, field.generator);
        public static implicit operator FieldDouble(FieldManager<T> field) => new FieldDouble(field.instance, field.generator);
        public static implicit operator FieldDecimal(FieldManager<T> field) => new FieldDecimal(field.instance, field.generator);
        public static implicit operator CanCompute<Int32>(FieldManager<T> field) => new CanCompute<Int32>(field.instance, field.generator);
        public static implicit operator CanCompute<Int64>(FieldManager<T> field) => new CanCompute<Int64>(field.instance, field.generator);
        public static implicit operator CanCompute<Single>(FieldManager<T> field) => new CanCompute<Single>(field.instance, field.generator);
        public static implicit operator CanCompute<Double>(FieldManager<T> field) => new CanCompute<Double>(field.instance, field.generator);
        public static implicit operator CanCompute<Decimal>(FieldManager<T> field) => new CanCompute<Decimal>(field.instance, field.generator);
    }
}
