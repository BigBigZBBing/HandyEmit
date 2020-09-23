using System;
using System.Collections.Generic;
using System.Text;
using System.Reflection.Emit;
using System.IO;
using System.Runtime.Serialization;
using System.Runtime.Serialization.Formatters.Binary;
using System.Reflection;
using System.Diagnostics.SymbolStore;
using System.Runtime.InteropServices;
using System.Linq;

namespace HandyEmit.SmartEmit
{
    /// <summary>
    /// 函数内快速构建方案
    /// </summary>
    public partial class FuncGenerator : EmitBasic
    {
        private ILGenerator il;

        internal FuncGenerator(ILGenerator il) : base(il)
        {
            this.il = il;
        }

        #region 功能型语法

        #region For

        /// <summary>
        /// 基础For正循环
        /// </summary>
        /// <param name="init"></param>
        /// <param name="length"></param>
        /// <param name="builder"></param>
        public void For(Int32 init, LocalBuilder length, Action<LocalBuilder> builder)
        {
            Label _for = il.DefineLabel();
            Label _endfor = il.DefineLabel();
            LocalBuilder index = il.DeclareLocal(typeof(Int32));
            il.Int32Map(init);
            il.Emit(OpCodes.Stloc_S, index);
            il.Emit(OpCodes.Br, _endfor);
            il.MarkLabel(_for);
            builder?.Invoke(index);
            il.Emit(OpCodes.Ldloc_S, index);
            il.Emit(OpCodes.Ldc_I4_1);
            il.Emit(OpCodes.Add);
            il.Emit(OpCodes.Stloc_S, index);
            il.MarkLabel(_endfor);
            il.Emit(OpCodes.Ldloc_S, index);
            il.Emit(OpCodes.Ldloc_S, length);
            il.Emit(OpCodes.Ldlen);
            il.Emit(OpCodes.Clt);
            il.Emit(OpCodes.Brtrue_S, _for);
        }

        /// <summary>
        /// 基础For正循环
        /// </summary>
        /// <param name="init"></param>
        /// <param name="length"></param>
        /// <param name="builder"></param>
        public void For(LocalBuilder init, LocalBuilder length, Action<LocalBuilder> build)
        {
            Label _for = il.DefineLabel();
            Label _endfor = il.DefineLabel();
            LocalBuilder index = il.DeclareLocal(typeof(Int32));
            il.Emit(OpCodes.Ldloc_S, init);
            il.Emit(OpCodes.Stloc_S, index);
            il.Emit(OpCodes.Br, _endfor);
            il.MarkLabel(_for);
            build?.Invoke(index);
            il.Emit(OpCodes.Ldloc_S, index);
            il.Emit(OpCodes.Ldc_I4_1);
            il.Emit(OpCodes.Add);
            il.Emit(OpCodes.Stloc_S, index);
            il.MarkLabel(_endfor);
            il.Emit(OpCodes.Ldloc_S, index);
            il.Emit(OpCodes.Ldloc_S, length);
            il.Emit(OpCodes.Ldlen);
            il.Emit(OpCodes.Clt);
            il.Emit(OpCodes.Brtrue_S, _for);
        }

        /// <summary>
        /// 基础For正循环
        /// </summary>
        /// <param name="init"></param>
        /// <param name="length"></param>
        /// <param name="builder"></param>
        public void For(Int32 init, Int32 length, Action<LocalBuilder> build)
        {
            Label _for = il.DefineLabel();
            Label _endfor = il.DefineLabel();
            LocalBuilder index = il.DeclareLocal(typeof(Int32));
            il.Int32Map(init);
            il.Emit(OpCodes.Stloc_S, index);
            il.Emit(OpCodes.Br, _endfor);
            il.MarkLabel(_for);
            build?.Invoke(index);
            il.Emit(OpCodes.Ldloc_S, index);
            il.Emit(OpCodes.Ldc_I4_1);
            il.Emit(OpCodes.Add);
            il.Emit(OpCodes.Stloc_S, index);
            il.MarkLabel(_endfor);
            il.Emit(OpCodes.Ldloc_S, index);
            il.Int32Map(length);
            il.Emit(OpCodes.Clt);
            il.Emit(OpCodes.Brtrue_S, _for);
        }

        #endregion

        #region Forr

        /// <summary>
        /// 基础For倒循环
        /// </summary>
        /// <param name="init"></param>
        /// <param name="length"></param>
        /// <param name="builder"></param>
        public void Forr(LocalBuilder init, Action<LocalBuilder> builder)
        {
            Label _for = il.DefineLabel();
            Label _endfor = il.DefineLabel();
            LocalBuilder index = il.DeclareLocal(typeof(Int32));
            il.Emit(OpCodes.Ldloc_S, init);
            il.Emit(OpCodes.Ldlen);
            il.Emit(OpCodes.Ldc_I4_1);
            il.Emit(OpCodes.Sub);
            il.Emit(OpCodes.Stloc_S, index);
            il.Emit(OpCodes.Br, _endfor);
            il.MarkLabel(_for);
            builder?.Invoke(index);
            il.Emit(OpCodes.Ldloc_S, index);
            il.Emit(OpCodes.Ldc_I4_1);
            il.Emit(OpCodes.Sub);
            il.Emit(OpCodes.Stloc_S, index);
            il.MarkLabel(_endfor);
            il.Emit(OpCodes.Ldloc_S, index);
            il.Emit(OpCodes.Ldc_I4_0);
            il.Emit(OpCodes.Clt);
            il.Emit(OpCodes.Ldc_I4_0);
            il.Emit(OpCodes.Ceq);
            il.Emit(OpCodes.Brtrue_S, _for);
        }

        #endregion

        #region IF

        /// <summary>
        /// IF判断
        /// </summary>
        /// <param name="assert"></param>
        /// <param name="builder"></param>
        /// <returns></returns>
        public AssertManager IF(LocalBuilder assert, Action<ILGenerator> builder)
        {
            return new AssertManager(il, (assert, builder));
        }

        /// <summary>
        /// IF判断
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="assert"></param>
        /// <param name="builder"></param>
        /// <returns></returns>
        public AssertManager IF<T>(FieldManager<T> assert, Action<ILGenerator> builder)
        {
            return new AssertManager(il, (assert, builder));
        }

        #endregion

        #endregion
    }
}
