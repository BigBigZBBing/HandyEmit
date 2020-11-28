using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection.Emit;
using System.Text;
using System.Threading.Tasks;

namespace ILWheatBread.SmartEmit
{
    public static class EmitBasicExtension
    {
        /// <summary>
        /// 函数返回
        /// </summary>
        /// <param name="basic"></param>
        public static void EmitReturn(this EmitBasic basic)
        {
            basic.Emit(OpCodes.Ret);
        }

        /// <summary>
        /// 函数参数指令
        /// </summary>
        /// <param name="basic"></param>
        /// <param name="index"></param>
        public static void EmitParam(this EmitBasic basic, Int32 index)
        {
            switch (index)
            {
                case 0: basic.Emit(OpCodes.Ldarg_0); break;
                case 1: basic.Emit(OpCodes.Ldarg_1); break;
                case 2: basic.Emit(OpCodes.Ldarg_2); break;
                case 3: basic.Emit(OpCodes.Ldarg_3); break;
                default: basic.Emit(OpCodes.Ldarg_S, index); break;
            }
        }

        /// <summary>
        /// 抛出异常
        /// </summary>
        /// <param name="basic"></param>
        /// <param name="ex"></param>
        public static void EmitThrow(this EmitBasic basic, LocalBuilder ex)
        {
            if (ex == null)
            {
                basic.Emit(OpCodes.Rethrow);
                return;
            }
            basic.Emit(OpCodes.Ldloc_S, ex);
            basic.Emit(OpCodes.Throw);
        }
    }
}
