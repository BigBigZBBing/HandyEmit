using System;
using System.Collections.Generic;
using System.Text;
using System.Reflection.Emit;

namespace HandyEmit.SmartEmit
{
    /// <summary>
    /// 断言解决方案
    /// </summary>
    public class AssertManager
    {
        private ILGenerator il;
        private List<(LocalBuilder, Action<ILGenerator>)> context = new List<(LocalBuilder, Action<ILGenerator>)>();

        internal AssertManager(ILGenerator il, (LocalBuilder, Action<ILGenerator>) context)
        {
            this.il = il;
            this.context.Add(context);
        }

        /// <summary>
        /// else if()
        /// </summary>
        /// <param name="assert"></param>
        /// <param name="builder"></param>
        /// <returns></returns>
        public AssertManager ElseIF(LocalBuilder assert, Action<ILGenerator> builder)
        {
            context.Add((assert, builder));
            return this;
        }

        /// <summary>
        /// else if()
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="assert"></param>
        /// <param name="builder"></param>
        /// <returns></returns>
        public AssertManager ElseIF<T>(FieldManager<T> assert, Action<ILGenerator> builder)
        {
            context.Add((assert, builder));
            return this;
        }

        /// <summary>
        /// else
        /// </summary>
        /// <param name="builder"></param>
        public void Else(Action<ILGenerator> builder)
        {
            Label end = il.DefineLabel();
            Label lab = il.DefineLabel();
            Boolean first = true;
            foreach (var item in context)
            {
                if (!first) il.MarkLabel(lab);
                lab = il.DefineLabel();
                il.Emit(OpCodes.Ldloc_S, item.Item1);
                il.Emit(OpCodes.Brfalse_S, lab);
                item.Item2?.Invoke(il);
                il.Emit(OpCodes.Br_S, end);
                first = false;
            }
            il.MarkLabel(lab);
            builder?.Invoke(il);
            il.Emit(OpCodes.Br_S, end);
            il.MarkLabel(end);
        }

        /// <summary>
        /// end if
        /// </summary>
        public void IFEnd()
        {
            Label end = il.DefineLabel();
            Label lab = il.DefineLabel();
            Boolean first = true;
            foreach (var item in context)
            {
                if (!first) il.MarkLabel(lab);
                lab = il.DefineLabel();
                il.Emit(OpCodes.Ldloc_S, item.Item1);
                il.Emit(OpCodes.Brfalse_S, lab);
                item.Item2?.Invoke(il);
                il.Emit(OpCodes.Br_S, end);
                first = false;
            }
            il.MarkLabel(lab);
            il.MarkLabel(end);
        }
    }
}
