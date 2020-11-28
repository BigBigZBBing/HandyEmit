using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection.Emit;
using System.Text;
using System.Threading.Tasks;

namespace HandyEmit.SmartEmit.Func
{
    public class TryCatchManager
    {
        private ILGenerator generator;

        internal TryCatchManager(ILGenerator generator)
        {
            this.generator = generator;
        }

        public TryCatchManager Catch(Action<ILGenerator> builder)
        {
            generator.BeginCatchBlock(typeof(Exception));
            builder(generator);
            return this;
        }

        public TryCatchManager Finally(Action<ILGenerator> builder)
        {
            generator.BeginFinallyBlock();
            builder(generator);
            return this;
        }

        public void TryEnd()
        {
            generator.EndExceptionBlock();
        }
    }
}
