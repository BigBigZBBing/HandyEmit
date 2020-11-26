using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection.Emit;
using System.Text;
using System.Threading.Tasks;

namespace HandyEmit.SmartEmit
{
    public static class EmitBasicExtension
    {
        public static void Return(this EmitBasic basic)
        {
            basic.Emit(OpCodes.Ret);
        }
    }
}
