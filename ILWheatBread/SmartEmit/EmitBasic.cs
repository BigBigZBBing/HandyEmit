﻿using System;
using System.Collections.Generic;
using System.Reflection;
using System.Reflection.Emit;
using System.Text;

namespace ILWheatBread.SmartEmit
{
    public abstract class EmitBasic
    {
        internal ILGenerator generator;

        private Dictionary<Type, Delegate> emitMethod => new Dictionary<Type, Delegate>();

        private Type generatorType => typeof(ILGenerator);

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
        public void Emit(OpCode opcode, String str) => DispatchEmit(opcode, str);
        public void Emit(OpCode opcode, FieldInfo field) => DispatchEmit(opcode, field);
        public void Emit(OpCode opcode, Label[] labels) => DispatchEmit(opcode, labels);
        public void Emit(OpCode opcode, Label label) => DispatchEmit(opcode, label);
        public void Emit(OpCode opcode, LocalBuilder local) => DispatchEmit(opcode, local);
        public void Emit(OpCode opcode, Single arg) => DispatchEmit(opcode, arg);
        public void Emit(OpCode opcode, Byte arg) => DispatchEmit(opcode, arg);
        public void Emit(OpCode opcode, SByte arg) => DispatchEmit(opcode, arg);
        public void Emit(OpCode opcode, Int16 arg) => DispatchEmit(opcode, arg);
        public void Emit(OpCode opcode, Double arg) => DispatchEmit(opcode, arg);
        public void Emit(OpCode opcode, MethodInfo meth) => DispatchEmit(opcode, meth);
        public void Emit(OpCode opcode, Int32 arg) => DispatchEmit(opcode, arg);
        public void Emit(OpCode opcode, Int64 arg) => DispatchEmit(opcode, arg);
        public void Emit(OpCode opcode, Type cls) => DispatchEmit(opcode, cls);
        public void Emit(OpCode opcode, SignatureHelper signature) => DispatchEmit(opcode, signature);
        public void Emit(OpCode opcode, ConstructorInfo con) => DispatchEmit(opcode, con);
        public void Emit(OpCode opcode) => DispatchEmit(opcode);
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

        private void DispatchEmit<T>(OpCode opcode, T value)
        {
            if (CacheManager.retValue) generator.Emit(OpCodes.Pop);
            ((Action<OpCode, T>)CacheMethod<T>()).Invoke(opcode, value);
        }

        private void DispatchEmit(OpCode opcode)
        {
            if (CacheManager.retValue) generator.Emit(OpCodes.Pop);
            generator.Emit(opcode);
        }

        private Delegate CacheMethod<T>()
        {
            if (emitMethod.ContainsKey(typeof(T)))
            {
                return emitMethod[typeof(T)];
            }
            MethodInfo method = generatorType.GetMethod("Emit", new[] { typeof(OpCode), typeof(T) });
            Delegate deleg = method.CreateDelegate(typeof(Action<OpCode, T>), generator);
            emitMethod.Add(typeof(T), deleg);
            return deleg;
        }
    }
}
