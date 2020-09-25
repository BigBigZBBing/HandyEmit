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
        private ILGenerator il;

        internal TryCatchManager(ILGenerator il)
        {
            this.il = il;
        }

        public TryCatchManager Catch(Action<ILGenerator> builder)
        {
            il.BeginCatchBlock(typeof(Exception));
            builder(il);
            return this;
        }

        public TryCatchManager Finally(Action<ILGenerator> builder)
        {
            il.BeginFinallyBlock();
            builder(il);
            return this;
        }

        public void TryEnd()
        {
            il.EndExceptionBlock();
        }
    }
}
