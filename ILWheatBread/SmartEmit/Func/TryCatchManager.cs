using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection.Emit;
using System.Text;
using System.Threading.Tasks;

namespace ILWheatBread.SmartEmit.Func
{
    public class TryCatchManager
    {
        private ILGenerator generator;

        internal TryCatchManager(ILGenerator generator)
        {
            this.generator = generator;
        }

        public TryCatchManager Catch(Action<LocalBuilder> builder)
        {
            generator.BeginCatchBlock(typeof(Exception));
            var ex = generator.DeclareLocal(typeof(Exception));
            generator.Emit(OpCodes.Stloc_S, ex);
            builder(ex);
            return this;
        }

        public TryCatchManager Finally(Action builder)
        {
            generator.BeginFinallyBlock();
            builder();
            return this;
        }

        public void TryEnd()
        {
            generator.EndExceptionBlock();
        }
    }
}
