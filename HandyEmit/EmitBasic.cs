using System;
using System.Collections.Generic;
using System.Reflection;
using System.Reflection.Emit;
using System.Text;

namespace HandyEmit
{
    public abstract class EmitBasic
    {
        ILGenerator il;

        public EmitBasic(ILGenerator il)
        {
            this.il = il;
        }

        #region 代码层继承
        public void BeginCatchBlock(Type exceptionType) => il.BeginCatchBlock(exceptionType);
        public void BeginExceptFilterBlock() => il.BeginExceptFilterBlock();
        public Label BeginExceptionBlock() => il.BeginExceptionBlock();
        public void BeginFaultBlock() => il.BeginFaultBlock();
        public void BeginFinallyBlock() => il.BeginFinallyBlock();
        public void BeginScope() => il.BeginScope();
        public void Emit(OpCode opcode, String str) => il.Emit(opcode, str);
        public void Emit(OpCode opcode, FieldInfo field) => il.Emit(opcode, field);
        public void Emit(OpCode opcode, Label[] labels) => il.Emit(opcode, labels);
        public void Emit(OpCode opcode, Label label) => il.Emit(opcode, label);
        public void Emit(OpCode opcode, LocalBuilder local) => il.Emit(opcode, local);
        public void Emit(OpCode opcode, Single arg) => il.Emit(opcode, arg);
        public void Emit(OpCode opcode, Byte arg) => il.Emit(opcode, arg);
        public void Emit(OpCode opcode, SByte arg) => il.Emit(opcode, arg);
        public void Emit(OpCode opcode, Int16 arg) => il.Emit(opcode, arg);
        public void Emit(OpCode opcode, Double arg) => il.Emit(opcode, arg);
        public void Emit(OpCode opcode, MethodInfo meth) => il.Emit(opcode, meth);
        public void Emit(OpCode opcode, Int32 arg) => il.Emit(opcode, arg);
        public void Emit(OpCode opcode, Int64 arg) => il.Emit(opcode, arg);
        public void Emit(OpCode opcode, Type cls) => il.Emit(opcode, cls);
        public void Emit(OpCode opcode, SignatureHelper signature) => il.Emit(opcode, signature);
        public void Emit(OpCode opcode, ConstructorInfo con) => il.Emit(opcode, con);
        public void Emit(OpCode opcode) => il.Emit(opcode);
        public void MarkLabel(Label loc) => il.MarkLabel(loc);
        public LocalBuilder DeclareLocal(Type localType, Boolean pinned) => il.DeclareLocal(localType, pinned);
        public LocalBuilder DeclareLocal(Type localType) => il.DeclareLocal(localType);
        public Label DefineLabel() => il.DefineLabel();
        public void EmitCall(OpCode opcode, MethodInfo methodInfo, Type[] optionalParameterTypes) =>
            il.EmitCall(opcode, methodInfo, optionalParameterTypes);
        public void EmitCalli(OpCode opcode, CallingConventions callingConvention, Type returnType, Type[] parameterTypes, Type[] optionalParameterTypes) =>
            il.EmitCalli(opcode, callingConvention, returnType, parameterTypes, optionalParameterTypes);
        public void EmitWriteLine(string value) => il.EmitWriteLine(value);
        public void EmitWriteLine(FieldInfo fld) => il.EmitWriteLine(fld);
        public void EmitWriteLine(LocalBuilder localBuilder) => il.EmitWriteLine(localBuilder);
        public void EndExceptionBlock() => il.EndExceptionBlock();
        public void EndScope() => il.EndScope();
        public void ThrowException(Type excType) => il.ThrowException(excType);
        public void UsingNamespace(string usingNamespace) => il.UsingNamespace(usingNamespace);

        #endregion
    }
}
