using System;
using System.Reflection.Emit;

namespace ILWheatBread.SmartEmit.Field
{
    public class FieldString : FieldManager<String>
    {
        internal FieldString(LocalBuilder stack, ILGenerator generator) : base(stack, generator)
        {
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

        public static FieldBoolean operator ==(FieldString field, String value)
        {
            return ManagerGX.Comparer(field, value, OpCodes.Ceq);
        }

        public static FieldBoolean operator ==(FieldString field, LocalBuilder value)
        {
            return ManagerGX.Comparer(field, value, OpCodes.Ceq);
        }

        public static FieldBoolean operator ==(FieldString field, FieldString value)
        {
            return ManagerGX.Comparer(field, value, OpCodes.Ceq);
        }

        public static FieldBoolean operator !=(FieldString field, String value)
        {
            return ManagerGX.Comparer(
               ManagerGX.Comparer(field, value, OpCodes.Ceq),
               field.NewInt32(), OpCodes.Ceq);
        }

        public static FieldBoolean operator !=(FieldString field, LocalBuilder value)
        {
            return ManagerGX.Comparer(
                ManagerGX.Comparer(field, value, OpCodes.Ceq),
                field.NewInt32(), OpCodes.Ceq);
        }

        public static FieldBoolean operator !=(FieldString field, FieldString value)
        {
            return ManagerGX.Comparer(
               ManagerGX.Comparer(field, value, OpCodes.Ceq),
               field.NewInt32(), OpCodes.Ceq);
        }

        public static FieldString operator +(FieldString field, String value)
        {
            return ManagerGX.Compute(field, value, OpCodes.Add);
        }

        public static VariableManager operator +(FieldString field, LocalBuilder value)
        {
            return ManagerGX.Compute(field, value, OpCodes.Add);
        }

        public static FieldString operator +(FieldString field, FieldString value)
        {
            return ManagerGX.Compute(field, value, OpCodes.Add);
        }
    }
}
