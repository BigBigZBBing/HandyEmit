using ILWheatBread.SmartEmit.Func;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection.Emit;
using System.Text;
using System.Threading.Tasks;

namespace ILWheatBread.SmartEmit
{
    /// <summary>
    /// 基础变量管理方案
    /// </summary>
    public class VariableManager : EmitBasic
    {
        /// <summary>
        /// 变量内存指针
        /// </summary>
        internal LocalBuilder instance;

        internal VariableManager(LocalBuilder stack, ILGenerator generator) : base(generator)
        {
            this.instance = stack;
        }

        /// <summary>
        /// 从内存中推入计算堆
        /// </summary>
        public void Output()
        {
            base.Emit(OpCodes.Ldloc_S, this.instance);
        }

        /// <summary>
        /// 推出计算堆到内存
        /// </summary>
        public void Input()
        {
            base.Emit(OpCodes.Stloc_S, this.instance);
        }
    }
}
