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
using HandyEmit.SmartEmit.Func;

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
            Label _for = DefineLabel();
            Label _endfor = DefineLabel();
            LocalBuilder index = DeclareLocal(typeof(Int32));
            il.Int32Map(init);
            Emit(OpCodes.Stloc_S, index);
            Emit(OpCodes.Br, _endfor);
            MarkLabel(_for);
            builder?.Invoke(index);
            Emit(OpCodes.Ldloc_S, index);
            Emit(OpCodes.Ldc_I4_1);
            Emit(OpCodes.Add);
            Emit(OpCodes.Stloc_S, index);
            MarkLabel(_endfor);
            Emit(OpCodes.Ldloc_S, index);
            Emit(OpCodes.Ldloc_S, length);
            Emit(OpCodes.Ldlen);
            Emit(OpCodes.Clt);
            Emit(OpCodes.Brtrue_S, _for);
        }

        /// <summary>
        /// 基础For正循环
        /// </summary>
        /// <param name="init"></param>
        /// <param name="length"></param>
        /// <param name="builder"></param>
        public void For(LocalBuilder init, LocalBuilder length, Action<LocalBuilder> build)
        {
            Label _for = DefineLabel();
            Label _endfor = DefineLabel();
            LocalBuilder index = DeclareLocal(typeof(Int32));
            Emit(OpCodes.Ldloc_S, init);
            Emit(OpCodes.Stloc_S, index);
            Emit(OpCodes.Br, _endfor);
            MarkLabel(_for);
            build?.Invoke(index);
            Emit(OpCodes.Ldloc_S, index);
            Emit(OpCodes.Ldc_I4_1);
            Emit(OpCodes.Add);
            Emit(OpCodes.Stloc_S, index);
            MarkLabel(_endfor);
            Emit(OpCodes.Ldloc_S, index);
            Emit(OpCodes.Ldloc_S, length);
            Emit(OpCodes.Ldlen);
            Emit(OpCodes.Clt);
            Emit(OpCodes.Brtrue_S, _for);
        }

        /// <summary>
        /// 基础For正循环
        /// </summary>
        /// <param name="init"></param>
        /// <param name="length"></param>
        /// <param name="builder"></param>
        public void For(Int32 init, Int32 length, Action<LocalBuilder> build)
        {
            Label _for = DefineLabel();
            Label _endfor = DefineLabel();
            LocalBuilder index = DeclareLocal(typeof(Int32));
            il.Int32Map(init);
            Emit(OpCodes.Stloc_S, index);
            Emit(OpCodes.Br, _endfor);
            MarkLabel(_for);
            build?.Invoke(index);
            Emit(OpCodes.Ldloc_S, index);
            Emit(OpCodes.Ldc_I4_1);
            Emit(OpCodes.Add);
            Emit(OpCodes.Stloc_S, index);
            MarkLabel(_endfor);
            Emit(OpCodes.Ldloc_S, index);
            il.Int32Map(length);
            Emit(OpCodes.Clt);
            Emit(OpCodes.Brtrue_S, _for);
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
            Label _for = DefineLabel();
            Label _endfor = DefineLabel();
            LocalBuilder index = DeclareLocal(typeof(Int32));
            Emit(OpCodes.Ldloc_S, init);
            Emit(OpCodes.Ldlen);
            Emit(OpCodes.Ldc_I4_1);
            Emit(OpCodes.Sub);
            Emit(OpCodes.Stloc_S, index);
            Emit(OpCodes.Br, _endfor);
            MarkLabel(_for);
            builder?.Invoke(index);
            Emit(OpCodes.Ldloc_S, index);
            Emit(OpCodes.Ldc_I4_1);
            Emit(OpCodes.Sub);
            Emit(OpCodes.Stloc_S, index);
            MarkLabel(_endfor);
            Emit(OpCodes.Ldloc_S, index);
            Emit(OpCodes.Ldc_I4_0);
            Emit(OpCodes.Clt);
            Emit(OpCodes.Ldc_I4_0);
            Emit(OpCodes.Ceq);
            Emit(OpCodes.Brtrue_S, _for);
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

        #region Try

        /// <summary>
        /// Try捕获
        /// </summary>
        /// <param name="builder"></param>
        /// <returns></returns>
        public TryCatchManager Try(Action<ILGenerator> builder)
        {
            BeginExceptionBlock();
            return new TryCatchManager(il);
        }

        #endregion

        #endregion
    }
}
