using System;
using System.Reflection.Emit;

namespace ILWheatBread.SmartEmit.Field
{
    public class FieldObject : FieldManager<Object>
    {
        internal Type asidentity { get; set; }

        internal FieldObject(LocalBuilder stack, ILGenerator generator) : base(stack, generator)
        {
            asidentity = stack.LocalType;
        }

        public FieldObject As<T>()
        {
            LocalBuilder temp = DeclareLocal(typeof(T));
            Output();
            Emit(OpCodes.Castclass, typeof(T));
            Emit(OpCodes.Stloc_S, temp);
            return new FieldObject(temp, this);
        }

        public FieldObject As(Type type)
        {
            LocalBuilder temp = DeclareLocal(type);
            Output();
            Emit(OpCodes.Castclass, type);
            Emit(OpCodes.Stloc_S, temp);
            return new FieldObject(temp, this);
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

        public static FieldBoolean operator ==(FieldObject field, Object value)
        {
            return ManagerGX.Comparer(field, value, OpCodes.Ceq);
        }

        public static FieldBoolean operator ==(FieldObject field, LocalBuilder value)
        {
            return ManagerGX.Comparer(field, value, OpCodes.Ceq);
        }

        public static FieldBoolean operator ==(FieldObject field, SmartEmit.VariableManager value)
        {
            return ManagerGX.Comparer(field, value, OpCodes.Ceq);
        }

        public static FieldBoolean operator !=(FieldObject field, Object value)
        {
            return ManagerGX.Comparer(
               ManagerGX.Comparer(field, value, OpCodes.Ceq),
               field.NewInt32(), OpCodes.Ceq);
        }

        public static FieldBoolean operator !=(FieldObject field, LocalBuilder value)
        {
            return ManagerGX.Comparer(
                ManagerGX.Comparer(field, value, OpCodes.Ceq),
                field.NewInt32(), OpCodes.Ceq);
        }

        public static FieldBoolean operator !=(FieldObject field, SmartEmit.VariableManager value)
        {
            return ManagerGX.Comparer(
               ManagerGX.Comparer(field, value, OpCodes.Ceq),
               field.NewInt32(), OpCodes.Ceq);
        }
    }
}
