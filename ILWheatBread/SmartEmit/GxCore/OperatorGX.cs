using ILWheatBread.SmartEmit.Field;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection.Emit;
using System.Text;
using System.Threading.Tasks;

namespace ILWheatBread.SmartEmit
{
    internal static partial class ManagerGX
    {

        /// <summary>
        /// 比较器
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="field"></param>
        /// <param name="value"></param>
        /// <param name="code"></param>
        /// <returns></returns>
        internal static FieldBoolean Comparer<T>(FieldManager<T> field, T value, params OpCode[] codes)
        {
            var res = field.NewBoolean();
            field.Pop();
            foreach (var code in codes)
            {
                field.EmitValue(value);
                field.Emit(code);
            }
            field.Emit(OpCodes.Stloc_S, res);
            return res;
        }

        /// <summary>
        /// 比较器
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="field"></param>
        /// <param name="value"></param>
        /// <param name="code"></param>
        /// <returns></returns>
        internal static FieldBoolean Comparer<T, T1>(CanCompute<T> field, T1 value, params OpCode[] codes)
        {
            var res = field.NewBoolean();
            field.Pop();
            foreach (var code in codes)
            {
                field.EmitValue(value);
                field.Emit(code);
            }
            field.Emit(OpCodes.Stloc_S, res);
            return res;
        }

        /// <summary>
        /// 比较器
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="field"></param>
        /// <param name="value"></param>
        /// <param name="code"></param>
        /// <returns></returns>
        //internal static FieldBoolean Comparer<T>(FieldManager<T> field, LocalBuilder value, params OpCode[] codes)
        //{
        //    var res = field.NewBoolean();
        //    field.Pop();
        //    foreach (var code in codes)
        //    {
        //        field.Emit(OpCodes.Ldloc_S, value);
        //        field.Emit(code);
        //    }
        //    field.Emit(OpCodes.Stloc_S, res);
        //    return res;
        //}

        /// <summary>
        /// 比较器
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="field"></param>
        /// <param name="value"></param>
        /// <param name="code"></param>
        /// <returns></returns>
        internal static FieldBoolean Comparer<T, T1>(FieldManager<T> field, FieldManager<T1> value, params OpCode[] codes)
        {
            var res = field.NewBoolean();
            field.Pop();
            foreach (var code in codes)
            {
                value.Pop();
                field.Emit(code);
            }
            field.Emit(OpCodes.Stloc_S, res);
            return res;
        }

        /// <summary>
        /// 计算器
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="field"></param>
        /// <param name="value"></param>
        /// <param name="code"></param>
        /// <returns></returns>
        internal static FieldManager<T> Compute<T, T1>(FieldManager<T> field, T1 value, OpCode code)
        {
            field.Pop();
            field.EmitValue(value);
            field.Emit(code);
            field.Push();
            return field;
        }

        /// <summary>
        /// 计算器
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="field"></param>
        /// <param name="value"></param>
        /// <param name="code"></param>
        /// <returns></returns>
        //internal static FieldManager<T1> Compute<T, T1>(FieldManager<T> field, LocalBuilder value, OpCode code)
        //{
        //    field.Pop();
        //    field.Emit(OpCodes.Ldloc_S, value);
        //    field.Emit(code);
        //    field.Push();
        //    return field as FieldManager<T1>;
        //}

        /// <summary>
        /// 计算器
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="field"></param>
        /// <param name="value"></param>
        /// <param name="code"></param>
        /// <returns></returns>
        internal static FieldManager<T> Compute<T, T1>(FieldManager<T> field, FieldManager<T1> value, OpCode code)
        {
            field.Pop();
            value.Pop();
            field.Emit(code);
            field.Push();
            return field;
        }
    }
}
