using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection.Emit;
using System.Text;
using System.Threading.Tasks;

namespace ILWheatBread.SmartEmit.Func
{
    /// <summary>
    /// Try延续语法方案
    /// </summary>
    public class TryCatchManager
    {
        private ILGenerator generator;

        internal TryCatchManager(ILGenerator generator)
        {
            this.generator = generator;
        }

        /// <summary>
        /// 捕获
        /// </summary>
        /// <param name="builder">回调包裹逻辑</param>
        /// <returns></returns>
        public TryCatchManager Catch(Action<LocalBuilder> builder)
        {
            generator.BeginCatchBlock(typeof(Exception));
            var ex = generator.DeclareLocal(typeof(Exception));
            generator.Emit(OpCodes.Stloc_S, ex);
            builder(ex);
            return this;
        }

        /// <summary>
        /// 最终调用
        /// </summary>
        /// <param name="builder">回调包裹逻辑</param>
        /// <returns></returns>
        public TryCatchManager Finally(Action builder)
        {
            generator.BeginFinallyBlock();
            builder();
            return this;
        }

        /// <summary>
        /// 结束Try(必须调用)
        /// </summary>
        public void TryEnd()
        {
            generator.EndExceptionBlock();
        }
    }
}
