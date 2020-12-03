using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection.Emit;
using System.Text;
using System.Threading.Tasks;

namespace ILWheatBread.SmartEmit.Func
{
    /// <summary>
    /// 函数后期解决方案
    /// </summary>
    public class MethodManager : EmitBasic
    {
        /// <summary>
        /// 返回类型
        /// </summary>
        internal Type ReturnType { get; set; }

        public MethodManager(ILGenerator generator, Type returnType) : base(generator)
        {
            ReturnType = returnType;
        }

        /// <summary>
        /// 调用函数后返回参数不需要就直接释放
        /// </summary>
        public void EmptyEnd()
        {
            Emit(OpCodes.Pop);
        }

        /// <summary>
        /// 调用函数后参数指针
        /// </summary>
        /// <returns></returns>
        public LocalBuilder ReturnRef()
        {
            LocalBuilder ret = DeclareLocal(ReturnType);
            Emit(OpCodes.Stloc_S, ret);
            return ret;
        }
    }
}
