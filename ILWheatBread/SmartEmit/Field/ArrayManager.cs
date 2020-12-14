using System;
using System.Reflection.Emit;

namespace ILWheatBread.SmartEmit.Field
{
    public class FieldArray<T> : FieldManager<T[]>
    {
        internal Int32 Length { get; set; }
        internal FieldInt32 ILLength { get; set; }

        internal FieldArray(LocalBuilder stack, ILGenerator generator, Int32 Length) : base(stack, generator)
        {
            this.Length = Length;
        }

        public FieldBoolean IsNull()
        {
            LocalBuilder assert = DeclareLocal(typeof(Boolean));
            Output();
            Emit(OpCodes.Ldnull);
            Emit(OpCodes.Ceq);
            Emit(OpCodes.Stloc_S, assert);
            return new FieldBoolean(assert, this);
        }

        public LocalBuilder this[Int32 index]
        {
            get
            {
                Emit(OpCodes.Ldloc_S, instance);
                this.EmitValue(index);
                this.PopArray(identity);
                return null;
            }
            set
            {
                Emit(OpCodes.Ldloc_S, instance);
                this.EmitValue(index);
                Emit(OpCodes.Ldloc_S, value);
                this.PushArray(identity);
            }
        }

        public void GetValue(CanCompute<Int32> index)
        {
            Emit(OpCodes.Ldloc_S, instance);
            Emit(OpCodes.Ldloc_S, index);
            this.PopArray(identity);
        }

        public void SetValue(CanCompute<Int32> index, LocalBuilder value)
        {
            Emit(OpCodes.Ldloc_S, instance);
            Emit(OpCodes.Ldloc_S, index);
            Emit(OpCodes.Ldloc_S, value);
            this.PushArray(identity);
        }

        public FieldBoolean Exists(LocalBuilder value)
        {
            var result = this.NewBoolean();
            Label trueTo = DefineLabel();
            Label falseTo = DefineLabel();
            this.For(0, GetLength(), build =>
            {
                GetValue(build);
                Emit(OpCodes.Ldloc_S, value);
                Emit(OpCodes.Ceq);
                Emit(OpCodes.Brfalse, falseTo);
                Emit(OpCodes.Ldc_I4_1);
                result.Input();
                Emit(OpCodes.Br_S, trueTo);
                MarkLabel(falseTo);
            });
            MarkLabel(trueTo);
            return result;
        }

        public FieldInt32 FindIndex(LocalBuilder value)
        {
            var result = this.NewInt32(-1);
            Label trueTo = DefineLabel();
            Label falseTo = DefineLabel();
            this.For(0, GetLength(), build =>
            {
                GetValue(build);
                Emit(OpCodes.Ldloc_S, value);
                Emit(OpCodes.Ceq);
                Emit(OpCodes.Brfalse, falseTo);
                build.Output();
                result.Input();
                Emit(OpCodes.Br_S, trueTo);
                MarkLabel(falseTo);
            });
            MarkLabel(trueTo);
            return result;
        }

        public void Copy(FieldArray<T> target, FieldInt32 length)
        {
            this.For(0, length, int1 =>
            {
                var local = DeclareLocal(target.identity);
                GetValue(int1);
                Emit(OpCodes.Stloc_S, local);
                target.SetValue(int1, local);
            });
        }

        public FieldInt32 GetLength()
        {
            if ((object)ILLength == null)
            {
                if (Length == -1)
                {
                    LocalBuilder temp = DeclareLocal(typeof(Int32));
                    Output();
                    Emit(OpCodes.Ldlen);
                    Emit(OpCodes.Stloc_S, temp);
                    ILLength = new FieldInt32(temp, generator);
                }
                else
                    ILLength = this.NewInt32(Length);
            }
            return ILLength;
        }
    }
}
