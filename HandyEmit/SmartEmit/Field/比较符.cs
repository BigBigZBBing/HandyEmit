using System;
using System.Collections.Generic;
using System.Reflection.Emit;
using System.Text;

namespace EmitPackage.SmartEmit
{
    public partial class FieldManager1<T>
    {

        //#region 大于

        ///// <summary>
        ///// 大于
        ///// </summary>
        ///// <param name="field"></param>
        ///// <param name="value"></param>
        ///// <returns></returns>
        //public static FieldManager<Boolean> operator >(FieldManager<T> field, T value)
        //{
        //    if (typeof(T) == typeof(String)) ManagerGX.GxException("invalid type");
        //    if (typeof(T) == typeof(Boolean)) ManagerGX.GxException("invalid type");
        //    if (typeof(T).Name.IndexOf("[]") > -1) ManagerGX.GxException("invalid type");
        //    if (typeof(T).Name.IndexOf("List`") > -1) ManagerGX.GxException("invalid type");
        //    var res = field.il.NewBoolean();
        //    field.PushIn();
        //    field.il.EmitValue(value);
        //    field.il.Emit(OpCodes.Cgt);
        //    field.il.Emit(OpCodes.Stloc_S, res);
        //    return res;
        //}

        ///// <summary>
        ///// 大于
        ///// </summary>
        ///// <param name="field"></param>
        ///// <param name="value"></param>
        ///// <returns></returns>
        //public static FieldManager<Boolean> operator >(FieldManager<T> field, LocalBuilder value)
        //{
        //    if (typeof(T) == typeof(String)) ManagerGX.GxException("invalid type");
        //    if (typeof(T) == typeof(Boolean)) ManagerGX.GxException("invalid type");
        //    if (typeof(T).Name.IndexOf("[]") > -1) ManagerGX.GxException("invalid type");
        //    if (typeof(T).Name.IndexOf("List`") > -1) ManagerGX.GxException("invalid type");
        //    var res = field.il.NewBoolean();
        //    field.PushIn();
        //    field.il.Emit(OpCodes.Ldloc_S, value);
        //    field.il.Emit(OpCodes.Cgt);
        //    field.il.Emit(OpCodes.Stloc_S, res);
        //    return res;
        //}

        ///// <summary>
        ///// 大于
        ///// </summary>
        ///// <param name="field"></param>
        ///// <param name="value"></param>
        ///// <returns></returns>
        //public static FieldManager<Boolean> operator >(FieldManager<T> field, FieldManager<T> value)
        //{
        //    if (typeof(T) == typeof(String)) ManagerGX.GxException("invalid type");
        //    if (typeof(T) == typeof(Boolean)) ManagerGX.GxException("invalid type");
        //    if (typeof(T).Name.IndexOf("[]") > -1) ManagerGX.GxException("invalid type");
        //    if (typeof(T).Name.IndexOf("List`") > -1) ManagerGX.GxException("invalid type");
        //    var res = field.il.NewBoolean();
        //    field.PushIn();
        //    value.PushIn();
        //    field.il.Emit(OpCodes.Cgt);
        //    field.il.Emit(OpCodes.Stloc_S, res);
        //    return res;
        //}

        //#endregion

        //#region 小于

        ///// <summary>
        ///// 小于
        ///// </summary>
        ///// <param name="field"></param>
        ///// <param name="value"></param>
        ///// <returns></returns>
        //public static FieldManager<Boolean> operator <(FieldManager<T> field, T value)
        //{
        //    if (typeof(T) == typeof(String)) ManagerGX.GxException("invalid type");
        //    if (typeof(T) == typeof(Boolean)) ManagerGX.GxException("invalid type");
        //    if (typeof(T).Name.IndexOf("[]") > -1) ManagerGX.GxException("invalid type");
        //    if (typeof(T).Name.IndexOf("List`") > -1) ManagerGX.GxException("invalid type");
        //    var res = field.il.NewBoolean();
        //    field.PushIn();
        //    field.il.EmitValue(value);
        //    field.il.Emit(OpCodes.Clt);
        //    field.il.Emit(OpCodes.Stloc_S, res);
        //    return res;
        //}

        ///// <summary>
        ///// 小于
        ///// </summary>
        ///// <param name="field"></param>
        ///// <param name="value"></param>
        ///// <returns></returns>
        //public static FieldManager<Boolean> operator <(FieldManager<T> field, LocalBuilder value)
        //{
        //    if (typeof(T) == typeof(String)) ManagerGX.GxException("invalid type");
        //    if (typeof(T) == typeof(Boolean)) ManagerGX.GxException("invalid type");
        //    if (typeof(T).Name.IndexOf("[]") > -1) ManagerGX.GxException("invalid type");
        //    if (typeof(T).Name.IndexOf("List`") > -1) ManagerGX.GxException("invalid type");
        //    var res = field.il.NewBoolean();
        //    field.PushIn();
        //    field.il.Emit(OpCodes.Ldloc_S, value);
        //    field.il.Emit(OpCodes.Clt);
        //    field.il.Emit(OpCodes.Stloc_S, res);
        //    return res;
        //}

        ///// <summary>
        ///// 小于
        ///// </summary>
        ///// <param name="field"></param>
        ///// <param name="value"></param>
        ///// <returns></returns>
        //public static FieldManager<Boolean> operator <(FieldManager<T> field, FieldManager<T> value)
        //{
        //    if (typeof(T) == typeof(String)) ManagerGX.GxException("invalid type");
        //    if (typeof(T) == typeof(Boolean)) ManagerGX.GxException("invalid type");
        //    if (typeof(T).Name.IndexOf("[]") > -1) ManagerGX.GxException("invalid type");
        //    if (typeof(T).Name.IndexOf("List`") > -1) ManagerGX.GxException("invalid type");
        //    var res = field.il.NewBoolean();
        //    field.PushIn();
        //    value.PushIn();
        //    field.il.Emit(OpCodes.Clt);
        //    field.il.Emit(OpCodes.Stloc_S, res);
        //    return res;
        //}

        //#endregion

        //#region 大于等于

        ///// <summary>
        ///// 大于等于
        ///// </summary>
        ///// <param name="field"></param>
        ///// <param name="value"></param>
        ///// <returns></returns>
        //public static FieldManager<Boolean> operator >=(FieldManager<T> field, T value)
        //{
        //    if (typeof(T) == typeof(String)) ManagerGX.GxException("invalid type");
        //    if (typeof(T) == typeof(Boolean)) ManagerGX.GxException("invalid type");
        //    if (typeof(T).Name.IndexOf("[]") > -1) ManagerGX.GxException("invalid type");
        //    if (typeof(T).Name.IndexOf("List`") > -1) ManagerGX.GxException("invalid type");
        //    var res = field.il.NewBoolean();
        //    field.PushIn();
        //    field.il.EmitValue(value);
        //    field.il.Emit(OpCodes.Cgt);
        //    field.il.EmitValue(value);
        //    field.il.Emit(OpCodes.Ceq);
        //    field.il.Emit(OpCodes.Stloc_S, res);
        //    return res;
        //}

        ///// <summary>
        ///// 大于等于
        ///// </summary>
        ///// <param name="field"></param>
        ///// <param name="value"></param>
        ///// <returns></returns>
        //public static FieldManager<Boolean> operator >=(FieldManager<T> field, LocalBuilder value)
        //{
        //    if (typeof(T) == typeof(String)) ManagerGX.GxException("invalid type");
        //    if (typeof(T) == typeof(Boolean)) ManagerGX.GxException("invalid type");
        //    if (typeof(T).Name.IndexOf("[]") > -1) ManagerGX.GxException("invalid type");
        //    if (typeof(T).Name.IndexOf("List`") > -1) ManagerGX.GxException("invalid type");
        //    var res = field.il.NewBoolean();
        //    field.PushIn();
        //    field.il.Emit(OpCodes.Ldloc_S, value);
        //    field.il.Emit(OpCodes.Cgt);
        //    field.il.Emit(OpCodes.Ldloc_S, value);
        //    field.il.Emit(OpCodes.Ceq);
        //    field.il.Emit(OpCodes.Stloc_S, res);
        //    return res;
        //}

        ///// <summary>
        ///// 大于等于
        ///// </summary>
        ///// <param name="field"></param>
        ///// <param name="value"></param>
        ///// <returns></returns>
        //public static FieldManager<Boolean> operator >=(FieldManager<T> field, FieldManager<T> value)
        //{
        //    if (typeof(T) == typeof(String)) ManagerGX.GxException("invalid type");
        //    if (typeof(T) == typeof(Boolean)) ManagerGX.GxException("invalid type");
        //    if (typeof(T).Name.IndexOf("[]") > -1) ManagerGX.GxException("invalid type");
        //    if (typeof(T).Name.IndexOf("List`") > -1) ManagerGX.GxException("invalid type");
        //    var res = field.il.NewBoolean();
        //    field.PushIn();
        //    value.PushIn();
        //    field.il.Emit(OpCodes.Cgt);
        //    value.PushIn();
        //    field.il.Emit(OpCodes.Ceq);
        //    field.il.Emit(OpCodes.Stloc_S, res);
        //    return res;
        //}

        //#endregion

        //#region 小于等于

        ///// <summary>
        ///// 小于等于
        ///// </summary>
        ///// <param name="field"></param>
        ///// <param name="value"></param>
        ///// <returns></returns>
        //public static FieldManager<Boolean> operator <=(FieldManager<T> field, T value)
        //{
        //    if (typeof(T) == typeof(String)) ManagerGX.GxException("invalid type");
        //    if (typeof(T) == typeof(Boolean)) ManagerGX.GxException("invalid type");
        //    if (typeof(T).Name.IndexOf("[]") > -1) ManagerGX.GxException("invalid type");
        //    if (typeof(T).Name.IndexOf("List`") > -1) ManagerGX.GxException("invalid type");
        //    var res = field.il.NewBoolean();
        //    field.PushIn();
        //    field.il.EmitValue(value);
        //    field.il.Emit(OpCodes.Clt);
        //    field.il.EmitValue(value);
        //    field.il.Emit(OpCodes.Ceq);
        //    field.il.Emit(OpCodes.Stloc_S, res);
        //    return res;
        //}

        ///// <summary>
        ///// 小于等于
        ///// </summary>
        ///// <param name="field"></param>
        ///// <param name="value"></param>
        ///// <returns></returns>
        //public static FieldManager<Boolean> operator <=(FieldManager<T> field, LocalBuilder value)
        //{
        //    if (typeof(T) == typeof(String)) ManagerGX.GxException("invalid type");
        //    if (typeof(T) == typeof(Boolean)) ManagerGX.GxException("invalid type");
        //    if (typeof(T).Name.IndexOf("[]") > -1) ManagerGX.GxException("invalid type");
        //    if (typeof(T).Name.IndexOf("List`") > -1) ManagerGX.GxException("invalid type");
        //    var res = field.il.NewBoolean();
        //    field.PushIn();
        //    field.il.Emit(OpCodes.Ldloc_S, value);
        //    field.il.Emit(OpCodes.Clt);
        //    field.il.Emit(OpCodes.Ldloc_S, value);
        //    field.il.Emit(OpCodes.Ceq);
        //    field.il.Emit(OpCodes.Stloc_S, res);
        //    return res;
        //}

        ///// <summary>
        ///// 小于等于
        ///// </summary>
        ///// <param name="field"></param>
        ///// <param name="value"></param>
        ///// <returns></returns>
        //public static FieldManager<Boolean> operator <=(FieldManager<T> field, FieldManager<T> value)
        //{
        //    if (typeof(T) == typeof(String)) ManagerGX.GxException("invalid type");
        //    if (typeof(T) == typeof(Boolean)) ManagerGX.GxException("invalid type");
        //    if (typeof(T).Name.IndexOf("[]") > -1) ManagerGX.GxException("invalid type");
        //    if (typeof(T).Name.IndexOf("List`") > -1) ManagerGX.GxException("invalid type");
        //    var res = field.il.NewBoolean();
        //    field.PushIn();
        //    value.PushIn();
        //    field.il.Emit(OpCodes.Clt);
        //    value.PushIn();
        //    field.il.Emit(OpCodes.Ceq);
        //    field.il.Emit(OpCodes.Stloc_S, res);
        //    return res;
        //}

        //#endregion

        //#region 相等

        ///// <summary>
        ///// 相等
        ///// </summary>
        ///// <param name="field"></param>
        ///// <param name="value"></param>
        ///// <returns></returns>
        //public static FieldManager<Boolean> operator ==(FieldManager<T> field, T value)
        //{
        //    var res = field.il.NewBoolean();
        //    field.PushIn();
        //    field.il.EmitValue(value);
        //    field.il.Emit(OpCodes.Call, typeof(Object).GetMethod("Equals", new Type[] { typeof(T), typeof(T) }));
        //    field.il.Emit(OpCodes.Stloc_S, res);
        //    return res;
        //}

        ///// <summary>
        ///// 相等
        ///// </summary>
        ///// <param name="field"></param>
        ///// <param name="value"></param>
        ///// <returns></returns>
        //public static FieldManager<Boolean> operator ==(FieldManager<T> field, LocalBuilder value)
        //{
        //    var res = field.il.NewBoolean();
        //    field.PushIn();
        //    field.il.Emit(OpCodes.Ldloc_S, value);
        //    field.il.Emit(OpCodes.Call, typeof(Object).GetMethod("Equals", new Type[] { typeof(T), typeof(T) }));
        //    field.il.Emit(OpCodes.Stloc_S, res);
        //    return res;
        //}

        ///// <summary>
        ///// 相等
        ///// </summary>
        ///// <param name="field"></param>
        ///// <param name="value"></param>
        ///// <returns></returns>
        //public static FieldManager<Boolean> operator ==(FieldManager<T> field, FieldManager<T> value)
        //{
        //    var res = field.il.NewBoolean();
        //    field.PushIn();
        //    value.PushIn();
        //    field.il.Emit(OpCodes.Call, typeof(Object).GetMethod("Equals", new Type[] { typeof(T), typeof(T) }));
        //    field.il.Emit(OpCodes.Stloc_S, res);
        //    return res;
        //}

        //#endregion

        //#region 不相等

        ///// <summary>
        ///// 不相等
        ///// </summary>
        ///// <param name="field"></param>
        ///// <param name="value"></param>
        ///// <returns></returns>
        //public static FieldManager<Boolean> operator !=(FieldManager<T> field, T value)
        //{
        //    var res = field.il.NewBoolean();
        //    field.PushIn();
        //    field.il.EmitValue(value);
        //    field.il.Emit(OpCodes.Call, typeof(Object).GetMethod("Equals", new Type[] { typeof(T), typeof(T) }));
        //    field.il.Emit(OpCodes.Ldc_I4_0);
        //    field.il.Emit(OpCodes.Ceq);
        //    field.il.Emit(OpCodes.Stloc_S, res);
        //    return res;
        //}

        ///// <summary>
        ///// 不相等
        ///// </summary>
        ///// <param name="field"></param>
        ///// <param name="value"></param>
        ///// <returns></returns>
        //public static FieldManager<Boolean> operator !=(FieldManager<T> field, LocalBuilder value)
        //{
        //    var res = field.il.NewBoolean();
        //    field.PushIn();
        //    field.il.Emit(OpCodes.Ldloc_S, value);
        //    field.il.Emit(OpCodes.Call, typeof(Object).GetMethod("Equals", new Type[] { typeof(T), typeof(T) }));
        //    field.il.Emit(OpCodes.Ldc_I4_0);
        //    field.il.Emit(OpCodes.Ceq);
        //    field.il.Emit(OpCodes.Stloc_S, res);
        //    return res;
        //}

        ///// <summary>
        ///// 不相等
        ///// </summary>
        ///// <param name="field"></param>
        ///// <param name="value"></param>
        ///// <returns></returns>
        //public static FieldManager<Boolean> operator !=(FieldManager<T> field, FieldManager<T> value)
        //{
        //    var res = field.il.NewBoolean();
        //    field.PushIn();
        //    value.PushIn();
        //    field.il.Emit(OpCodes.Call, typeof(Object).GetMethod("Equals", new Type[] { typeof(T), typeof(T) }));
        //    field.il.Emit(OpCodes.Ldc_I4_0);
        //    field.il.Emit(OpCodes.Ceq);
        //    field.il.Emit(OpCodes.Stloc_S, res);
        //    return res;
        //}

        //#endregion

    }


}
