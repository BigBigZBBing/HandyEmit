using System;
using System.Collections.Generic;
using System.Reflection;
using System.Reflection.Emit;
using System.Text;

namespace HandyEmit.SmartEmit
{
    public abstract class EmitBasic
    {
        internal ILGenerator generator;
        public EmitBasic(ILGenerator generator)
        {
            this.generator = generator;
        }

        public static implicit operator ILGenerator(EmitBasic basic) => basic.generator;

        #region 代码层继承
        public void BeginCatchBlock(Type exceptionType) => generator.BeginCatchBlock(exceptionType);
        public void BeginExceptFilterBlock() => generator.BeginExceptFilterBlock();
        public Label BeginExceptionBlock() => generator.BeginExceptionBlock();
        public void BeginFaultBlock() => generator.BeginFaultBlock();
        public void BeginFinallyBlock() => generator.BeginFinallyBlock();
        public void BeginScope() => generator.BeginScope();
        public void Emit(OpCode opcode, String str) => generator.Emit(opcode, str);
        public void Emit(OpCode opcode, FieldInfo field) => generator.Emit(opcode, field);
        public void Emit(OpCode opcode, Label[] labels) => generator.Emit(opcode, labels);
        public void Emit(OpCode opcode, Label label) => generator.Emit(opcode, label);
        public void Emit(OpCode opcode, LocalBuilder local) => generator.Emit(opcode, local);
        public void Emit(OpCode opcode, Single arg) => generator.Emit(opcode, arg);
        public void Emit(OpCode opcode, Byte arg) => generator.Emit(opcode, arg);
        public void Emit(OpCode opcode, SByte arg) => generator.Emit(opcode, arg);
        public void Emit(OpCode opcode, Int16 arg) => generator.Emit(opcode, arg);
        public void Emit(OpCode opcode, Double arg) => generator.Emit(opcode, arg);
        public void Emit(OpCode opcode, MethodInfo meth) => generator.Emit(opcode, meth);
        public void Emit(OpCode opcode, Int32 arg) => generator.Emit(opcode, arg);
        public void Emit(OpCode opcode, Int64 arg) => generator.Emit(opcode, arg);
        public void Emit(OpCode opcode, Type cls) => generator.Emit(opcode, cls);
        public void Emit(OpCode opcode, SignatureHelper signature) => generator.Emit(opcode, signature);
        public void Emit(OpCode opcode, ConstructorInfo con) => generator.Emit(opcode, con);
        public void Emit(OpCode opcode) => generator.Emit(opcode);
        public void MarkLabel(Label loc) => generator.MarkLabel(loc);
        public LocalBuilder DeclareLocal(Type localType, Boolean pinned) => generator.DeclareLocal(localType, pinned);
        public LocalBuilder DeclareLocal(Type localType) => generator.DeclareLocal(localType);
        public Label DefineLabel() => generator.DefineLabel();
        public void EmitCall(OpCode opcode, MethodInfo methodInfo, Type[] optionalParameterTypes) =>
            generator.EmitCall(opcode, methodInfo, optionalParameterTypes);
        public void EmitCalli(OpCode opcode, CallingConventions callingConvention, Type returnType, Type[] parameterTypes, Type[] optionalParameterTypes) =>
            generator.EmitCalli(opcode, callingConvention, returnType, parameterTypes, optionalParameterTypes);
        public void EmitWriteLine(string value) => generator.EmitWriteLine(value);
        public void EmitWriteLine(FieldInfo fld) => generator.EmitWriteLine(fld);
        public void EmitWriteLine(LocalBuilder localBuilder) => generator.EmitWriteLine(localBuilder);
        public void EndExceptionBlock() => generator.EndExceptionBlock();
        public void EndScope() => generator.EndScope();
        public void ThrowException(Type excType) => generator.ThrowException(excType);
        public void UsingNamespace(string usingNamespace) => generator.UsingNamespace(usingNamespace);

        #endregion
    }
}
