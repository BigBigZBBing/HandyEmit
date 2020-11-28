using System;
using System.Collections.Generic;
using System.Text;
using System.Reflection.Emit;

namespace ILWheatBread.SmartEmit
{
    /// <summary>
    /// 断言解决方案
    /// </summary>
    public class AssertManager
    {
        private ILGenerator generator;
        private List<(LocalBuilder, Action<ILGenerator>)> context = new List<(LocalBuilder, Action<ILGenerator>)>();

        internal AssertManager(ILGenerator generator, (LocalBuilder, Action<ILGenerator>) context)
        {
            this.generator = generator;
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
            Label end = generator.DefineLabel();
            Label lab = generator.DefineLabel();
            Boolean first = true;
            foreach (var item in context)
            {
                if (!first) generator.MarkLabel(lab);
                lab = generator.DefineLabel();
                generator.Emit(OpCodes.Ldloc_S, item.Item1);
                generator.Emit(OpCodes.Brfalse_S, lab);
                item.Item2?.Invoke(generator);
                generator.Emit(OpCodes.Br_S, end);
                first = false;
            }
            generator.MarkLabel(lab);
            builder?.Invoke(generator);
            generator.Emit(OpCodes.Br_S, end);
            generator.MarkLabel(end);
        }

        /// <summary>
        /// end if
        /// </summary>
        public void IFEnd()
        {
            Label end = generator.DefineLabel();
            Label lab = generator.DefineLabel();
            Boolean first = true;
            foreach (var item in context)
            {
                if (!first) generator.MarkLabel(lab);
                lab = generator.DefineLabel();
                generator.Emit(OpCodes.Ldloc_S, item.Item1);
                generator.Emit(OpCodes.Brfalse_S, lab);
                item.Item2?.Invoke(generator);
                generator.Emit(OpCodes.Br_S, end);
                first = false;
            }
            generator.MarkLabel(lab);
            generator.MarkLabel(end);
        }
    }
}
