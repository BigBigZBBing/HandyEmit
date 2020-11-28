using System;
using System.Collections.Generic;
using System.Reflection.Emit;
using System.Text;

namespace EmitPackage.SmartEmit
{
    public partial class FieldManager1<T>
    {
        //#region 运算符

        //#region 相加

        ///// <summary>
        ///// 相加
        ///// </summary>
        ///// <param name="field"></param>
        ///// <param name="value"></param>
        ///// <returns></returns>
        //public static FieldManager<T> operator +(FieldManager<T> field, T value)
        //{
        //    if (typeof(T) == typeof(Boolean)) throw new Exception("invalid type");
        //    if (typeof(T).Name.IndexOf("[]") > -1) throw new Exception("invalid type");
        //    if (typeof(T).Name.IndexOf("List`") > -1) throw new Exception("invalid type");
        //    field.PushIn();
        //    field.il.EmitValue(value);
        //    field.il.Emit(OpCodes.Add);
        //    field.PushOn();
        //    return field;
        //}

        ///// <summary>
        ///// 相加
        ///// </summary>
        ///// <param name="field"></param>
        ///// <param name="value"></param>
        ///// <returns></returns>
        //public static FieldManager<T> operator +(FieldManager<T> field, LocalBuilder value)
        //{
        //    if (typeof(T) == typeof(Boolean)) throw new Exception("invalid type");
        //    if (typeof(T).Name.IndexOf("[]") > -1) throw new Exception("invalid type");
        //    if (typeof(T).Name.IndexOf("List`") > -1) throw new Exception("invalid type");
        //    field.PushIn();
        //    field.il.Emit(OpCodes.Ldloc_S, value);
        //    field.il.Emit(OpCodes.Add);
        //    field.PushOn();
        //    return field;
        //}

        ///// <summary>
        ///// 相加
        ///// </summary>
        ///// <param name="field"></param>
        ///// <param name="value"></param>
        ///// <returns></returns>
        //public static FieldManager<T> operator +(FieldManager<T> field, FieldManager<T> value)
        //{
        //    if (typeof(T) == typeof(Boolean)) throw new Exception("invalid type");
        //    if (typeof(T).Name.IndexOf("[]") > -1) throw new Exception("invalid type");
        //    if (typeof(T).Name.IndexOf("List`") > -1) throw new Exception("invalid type");
        //    field.PushIn();
        //    value.PushIn();
        //    field.il.Emit(OpCodes.Add);
        //    field.PushOn();
        //    return field;
        //}

        //#endregion

        //#region 相减

        ///// <summary>
        ///// 相减
        ///// </summary>
        ///// <param name="field"></param>
        ///// <param name="value"></param>
        ///// <returns></returns>
        //public static FieldManager<T> operator -(FieldManager<T> field, T value)
        //{
        //    if (typeof(T) == typeof(String)) throw new Exception("invalid type");
        //    if (typeof(T) == typeof(Boolean)) throw new Exception("invalid type");
        //    if (typeof(T).Name.IndexOf("[]") > -1) throw new Exception("invalid type");
        //    if (typeof(T).Name.IndexOf("List`") > -1) throw new Exception("invalid type");
        //    field.PushIn();
        //    field.il.EmitValue(value);
        //    field.il.Emit(OpCodes.Sub);
        //    field.PushOn();
        //    return field;
        //}

        ///// <summary>
        ///// 相减
        ///// </summary>
        ///// <param name="field"></param>
        ///// <param name="value"></param>
        ///// <returns></returns>
        //public static FieldManager<T> operator -(FieldManager<T> field, LocalBuilder value)
        //{
        //    if (typeof(T) == typeof(String)) throw new Exception("invalid type");
        //    if (typeof(T) == typeof(Boolean)) throw new Exception("invalid type");
        //    if (typeof(T).Name.IndexOf("[]") > -1) throw new Exception("invalid type");
        //    if (typeof(T).Name.IndexOf("List`") > -1) throw new Exception("invalid type");
        //    field.PushIn();
        //    field.il.Emit(OpCodes.Ldloc_S, value);
        //    field.il.Emit(OpCodes.Sub);
        //    field.PushOn();
        //    return field;
        //}

        ///// <summary>
        ///// 相减
        ///// </summary>
        ///// <param name="field"></param>
        ///// <param name="value"></param>
        ///// <returns></returns>
        //public static FieldManager<T> operator -(FieldManager<T> field, FieldManager<T> value)
        //{
        //    if (typeof(T) == typeof(String)) throw new Exception("invalid type");
        //    if (typeof(T) == typeof(Boolean)) throw new Exception("invalid type");
        //    if (typeof(T).Name.IndexOf("[]") > -1) throw new Exception("invalid type");
        //    if (typeof(T).Name.IndexOf("List`") > -1) throw new Exception("invalid type");
        //    field.PushIn();
        //    value.PushIn();
        //    field.il.Emit(OpCodes.Sub);
        //    field.PushOn();
        //    return field;
        //}

        //#endregion

        //#region 相乘

        ///// <summary>
        ///// 相乘
        ///// </summary>
        ///// <param name="field"></param>
        ///// <param name="value"></param>
        ///// <returns></returns>
        //public static FieldManager<T> operator *(FieldManager<T> field, T value)
        //{
        //    if (typeof(T) == typeof(String)) throw new Exception("invalid type");
        //    if (typeof(T) == typeof(Boolean)) throw new Exception("invalid type");
        //    if (typeof(T).Name.IndexOf("[]") > -1) throw new Exception("invalid type");
        //    if (typeof(T).Name.IndexOf("List`") > -1) throw new Exception("invalid type");
        //    field.PushIn();
        //    field.il.EmitValue(value);
        //    field.il.Emit(OpCodes.Mul);
        //    field.PushOn();
        //    return field;
        //}

        ///// <summary>
        ///// 相乘
        ///// </summary>
        ///// <param name="field"></param>
        ///// <param name="value"></param>
        ///// <returns></returns>
        //public static FieldManager<T> operator *(FieldManager<T> field, LocalBuilder value)
        //{
        //    if (typeof(T) == typeof(String)) throw new Exception("invalid type");
        //    if (typeof(T) == typeof(Boolean)) throw new Exception("invalid type");
        //    if (typeof(T).Name.IndexOf("[]") > -1) throw new Exception("invalid type");
        //    if (typeof(T).Name.IndexOf("List`") > -1) throw new Exception("invalid type");
        //    field.PushIn();
        //    field.il.Emit(OpCodes.Ldloc_S, value);
        //    field.il.Emit(OpCodes.Mul);
        //    field.PushOn();
        //    return field;
        //}

        ///// <summary>
        ///// 相乘
        ///// </summary>
        ///// <param name="field"></param>
        ///// <param name="value"></param>
        ///// <returns></returns>
        //public static FieldManager<T> operator *(FieldManager<T> field, FieldManager<T> value)
        //{
        //    if (typeof(T) == typeof(String)) throw new Exception("invalid type");
        //    if (typeof(T) == typeof(Boolean)) throw new Exception("invalid type");
        //    if (typeof(T).Name.IndexOf("[]") > -1) throw new Exception("invalid type");
        //    if (typeof(T).Name.IndexOf("List`") > -1) throw new Exception("invalid type");
        //    field.PushIn();
        //    value.PushIn();
        //    field.il.Emit(OpCodes.Mul);
        //    field.PushOn();
        //    return field;
        //}

        //#endregion

        //#region 相除

        ///// <summary>
        ///// 相除
        ///// </summary>
        ///// <param name="field"></param>
        ///// <param name="value"></param>
        ///// <returns></returns>
        //public static FieldManager<T> operator /(FieldManager<T> field, T value)
        //{
        //    if (typeof(T) == typeof(String)) throw new Exception("invalid type");
        //    if (typeof(T) == typeof(Boolean)) throw new Exception("invalid type");
        //    if (typeof(T).Name.IndexOf("[]") > -1) throw new Exception("invalid type");
        //    if (typeof(T).Name.IndexOf("List`") > -1) throw new Exception("invalid type");
        //    field.PushIn();
        //    field.il.EmitValue(value);
        //    field.il.Emit(OpCodes.Rem);
        //    field.il.Emit(OpCodes.Stloc_S, field.stack);
        //    return field;
        //}

        ///// <summary>
        ///// 相除
        ///// </summary>
        ///// <param name="field"></param>
        ///// <param name="value"></param>
        ///// <returns></returns>
        //public static FieldManager<T> operator /(FieldManager<T> field, LocalBuilder value)
        //{
        //    if (typeof(T) == typeof(String)) throw new Exception("invalid type");
        //    if (typeof(T) == typeof(Boolean)) throw new Exception("invalid type");
        //    if (typeof(T).Name.IndexOf("[]") > -1) throw new Exception("invalid type");
        //    if (typeof(T).Name.IndexOf("List`") > -1) throw new Exception("invalid type");
        //    field.PushIn();
        //    field.il.Emit(OpCodes.Ldloc_S, value);
        //    field.il.Emit(OpCodes.Rem);
        //    field.il.Emit(OpCodes.Stloc_S, field.stack);
        //    return field;
        //}

        ///// <summary>
        ///// 相除
        ///// </summary>
        ///// <param name="field"></param>
        ///// <param name="value"></param>
        ///// <returns></returns>
        //public static FieldManager<T> operator /(FieldManager<T> field, FieldManager<T> value)
        //{
        //    if (typeof(T) == typeof(String)) throw new Exception("invalid type");
        //    if (typeof(T) == typeof(Boolean)) throw new Exception("invalid type");
        //    if (typeof(T).Name.IndexOf("[]") > -1) throw new Exception("invalid type");
        //    if (typeof(T).Name.IndexOf("List`") > -1) throw new Exception("invalid type");
        //    field.PushIn();
        //    value.PushIn();
        //    field.il.Emit(OpCodes.Rem);
        //    field.il.Emit(OpCodes.Stloc_S, field.stack);
        //    return field;
        //}

        //#endregion

        //#endregion
    }
}
