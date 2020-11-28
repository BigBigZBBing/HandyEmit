using ILWheatBread;
using System;
using System.Collections.Generic;
using System.Data;
using System.Reflection;
using System.Reflection.Emit;
using System.Text;

namespace NakedORM.Core
{
    internal static class EmitDbRead<T>
    {
        internal static readonly String TotalCount = PublishMembers.TotalCount;
        internal static readonly String PagerIndex = PublishMembers.PagerIndex;
        internal static readonly String IsDBNull = PublishMembers.IsDBNull;
        internal static readonly String get_Item = PublishMembers.get_Item;
        internal static readonly String GetOrdinal = PublishMembers.GetOrdinal;
        internal static readonly String _Read = PublishMembers._Read;
        internal static readonly String GetDateTime = PublishMembers.GetDateTime;
        internal static readonly String GetDecimal = PublishMembers.GetDecimal;
        internal static readonly String GetDouble = PublishMembers.GetDouble;
        internal static readonly String GetInt32 = PublishMembers.GetInt32;
        internal static readonly String GetInt64 = PublishMembers.GetInt64;
        internal static readonly String GetString = PublishMembers.GetString;
        internal static readonly String GetValue = PublishMembers.GetValue;
        internal static readonly String GetFieldType = PublishMembers.GetFieldType;
        internal static readonly String GetTypeFromHandle = PublishMembers.GetTypeFromHandle;
        internal static readonly String Add = PublishMembers.Add;
        internal static readonly String WriteLine = PublishMembers.WriteLine;

        internal static readonly MethodInfo ConsoleWriteLine = typeof(Console).GetMethod(WriteLine, new Type[] { typeof(Int64) });
        internal static readonly MethodInfo DataRecord_ItemGetter_Int = typeof(IDataRecord).GetMethod(get_Item, new Type[] { typeof(Int32) });
        internal static readonly MethodInfo DataRecord_GetOrdinal = typeof(IDataRecord).GetMethod(GetOrdinal);
        internal static readonly MethodInfo DataReader_Read = typeof(IDataReader).GetMethod(_Read);
        internal static readonly MethodInfo DataRecord_GetDateTime = typeof(IDataRecord).GetMethod(GetDateTime);
        internal static readonly MethodInfo DataRecord_GetDecimal = typeof(IDataRecord).GetMethod(GetDecimal);
        internal static readonly MethodInfo DataRecord_GetDouble = typeof(IDataRecord).GetMethod(GetDouble);
        internal static readonly MethodInfo DataRecord_GetInt32 = typeof(IDataRecord).GetMethod(GetInt32);
        internal static readonly MethodInfo DataRecord_GetInt64 = typeof(IDataRecord).GetMethod(GetInt64);
        internal static readonly MethodInfo DataRecord_GetString = typeof(IDataRecord).GetMethod(GetString);
        internal static readonly MethodInfo DataRecord_IsDBNull = typeof(IDataRecord).GetMethod(IsDBNull);
        internal static readonly MethodInfo DataRecord_GetValue = typeof(IDataRecord).GetMethod(GetValue);
        internal static readonly MethodInfo DataRecord_GetFieldType = typeof(IDataRecord).GetMethod(GetFieldType);
        internal static readonly MethodInfo Convert_IsDBNull = typeof(Convert).GetMethod(IsDBNull);
        internal static readonly MethodInfo List_Add = typeof(List<T>).GetMethod(Add);
        internal static readonly MethodInfo Pager_CountSetter = typeof(DbPager).GetProperty(TotalCount).GetSetMethod();
        internal static readonly MethodInfo Pager_CountGetter = typeof(DbPager).GetProperty(TotalCount).GetGetMethod();
        internal static readonly MethodInfo Pager_IndexGetter = typeof(DbPager).GetProperty(PagerIndex).GetGetMethod();
        internal static readonly MethodInfo Type_GetTypeFromHandle = typeof(Type).GetMethod(GetTypeFromHandle, new Type[] { typeof(RuntimeTypeHandle) });

        /// <summary>
        /// 数据转成实体(Emit技术)
        /// </summary>
        /// <param name="emits"></param>
        /// <param name="pager"></param>
        /// <returns></returns>
        internal static Func<IDataReader, DbPager, List<T>> DbRead(List<EmitProperty> emits, DbPager pager)
        {
            DynamicMethod dm = new DynamicMethod(string.Empty, typeof(List<T>), new Type[] { typeof(IDataReader), typeof(DbPager) });
            //初始化IL生成器
            ILGenerator il = dm.GetILGenerator();

            Label start = il.DefineLabel();
            Label end = il.DefineLabel();
            //新建一个对象List<T> 给一个默认值
            LocalBuilder list = il.DeclareLocal(typeof(List<T>));
            il.Emit(OpCodes.Newobj, typeof(List<T>).GetConstructor(Type.EmptyTypes));
            il.Emit(OpCodes.Stloc_S, list);
            //获取所有字段的位置
            LocalBuilder[] colIndices = GetColumnIndices(il, emits);

            //设置loop标签
            il.Emit(OpCodes.Br, start);
            il.MarkLabel(start);

            //调用Read方法
            il.Emit(OpCodes.Ldarg_0);
            //il.Emit(OpCodes.Ldloc_S, );
            il.Emit(OpCodes.Callvirt, DataReader_Read);
            //如果返回false跳到exit标签
            il.Emit(OpCodes.Brfalse, end);

            Label noPager = il.DefineLabel();
            //if(pager != null)
            il.Emit(OpCodes.Ldarg_1);
            il.Emit(OpCodes.Ldnull);
            il.Emit(OpCodes.Ceq);
            il.Emit(OpCodes.Brtrue, noPager);
            // {
            SmartPager(il);
            // }
            il.MarkLabel(noPager);

            //创建对象并赋值
            LocalBuilder item = il.DeclareLocal(typeof(T));
            // T item = new T();
            il.Emit(OpCodes.Newobj, typeof(T).GetConstructor(Type.EmptyTypes));
            il.Emit(OpCodes.Stloc_S, item);

            BuildItem<T>(il, emits, item, colIndices);

            //list.Add(item);
            il.Emit(OpCodes.Ldloc_S, list);
            il.Emit(OpCodes.Ldloc_S, item);
            il.Emit(OpCodes.Callvirt, List_Add);
            // }
            il.Emit(OpCodes.Br, start);
            il.MarkLabel(end);
            // return list;
            il.Emit(OpCodes.Ldloc_S, list);
            il.Emit(OpCodes.Ret);

            return (Func<IDataReader, DbPager, List<T>>)dm.CreateDelegate(typeof(Func<IDataReader, DbPager, List<T>>));
        }

        /// <summary>
        /// 分页功能
        /// </summary>
        /// <param name="il"></param>
        private static void SmartPager(ILGenerator il)
        {
            //[ Int32 index = dbRead.GetOrdinal("TotalCount") ]
            LocalBuilder index = il.DeclareLocal(typeof(Int32));
            il.Emit(OpCodes.Ldarg_0);
            il.Emit(OpCodes.Ldstr, TotalCount);
            il.Emit(OpCodes.Callvirt, DataRecord_GetOrdinal);
            il.Emit(OpCodes.Stloc_S, index);

            //[ Type fieldType = dbRead.GetFieldType("TotalCount") ]
            LocalBuilder fieldType = il.DeclareLocal(typeof(Type));
            il.Emit(OpCodes.Ldarg_0);
            il.Emit(OpCodes.Ldloc_S, index);
            il.Emit(OpCodes.Callvirt, DataRecord_GetFieldType);
            il.Emit(OpCodes.Stloc_S, fieldType);

            Label _false = il.DefineLabel();
            Label _fin = il.DefineLabel();
            LocalBuilder value;

            //[ Type _Int64 = typeof(Int64) ]
            LocalBuilder _Int64 = il.DeclareLocal(typeof(Type));
            il.Emit(OpCodes.Ldtoken, typeof(Int64));
            il.Emit(OpCodes.Call, Type_GetTypeFromHandle);
            il.Emit(OpCodes.Stloc_S, _Int64);

            //[ if((Object)fieldType == (Object)_Int64) ]
            il.Emit(OpCodes.Ldloc_S, fieldType);
            il.Emit(OpCodes.Ldloc_S, _Int64);
            il.Emit(OpCodes.Ceq);
            il.Emit(OpCodes.Brfalse, _false);
            //  {
            value = il.DeclareLocal(typeof(Int64));
            il.Emit(OpCodes.Ldarg_0);
            il.Emit(OpCodes.Ldloc_S, index);
            il.Emit(OpCodes.Callvirt, DataRecord_GetInt64);
            il.Emit(OpCodes.Stloc_S, value);

            il.Emit(OpCodes.Br, _fin);
            //  }
            //  else
            //  {
            il.MarkLabel(_false);

            value = il.DeclareLocal(typeof(Int32));
            il.Emit(OpCodes.Ldarg_0);
            il.Emit(OpCodes.Ldloc_S, index);
            il.Emit(OpCodes.Callvirt, DataRecord_GetInt32);
            il.Emit(OpCodes.Stloc_S, value);
            //  }
            il.MarkLabel(_fin);

            il.Emit(OpCodes.Ldarg_1);
            il.Emit(OpCodes.Ldloc_S, value);
            il.Emit(OpCodes.Callvirt, Pager_CountSetter);
        }

        /// <summary>
        /// 获取所有字段所在的目录
        /// </summary>
        /// <param name="il"></param>
        /// <param name="emits"></param>
        /// <returns></returns>
        private static LocalBuilder[] GetColumnIndices(ILGenerator il, IList<EmitProperty> emits)
        {
            LocalBuilder[] colIndices = new LocalBuilder[emits.Count];
            for (int i = 0; i < colIndices.Length; i++)
            {
                //Int32 arg;
                colIndices[i] = il.DeclareLocal(typeof(Int32));
                il.Emit(OpCodes.Ldarg_0);
                //arg = dbRead.GetOrdinal(emit.PropertyName);
                il.Emit(OpCodes.Ldstr, emits[i].PropertyName);
                il.Emit(OpCodes.Callvirt, DataRecord_GetOrdinal);
                il.Emit(OpCodes.Stloc_S, colIndices[i]);
            }
            return colIndices;
        }

        /// <summary>
        /// 构建对象字段
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="il"></param>
        /// <param name="columnInfoes"></param>
        /// <param name="item"></param>
        /// <param name="Propertys"></param>
        private static void BuildItem<T>(ILGenerator il, List<EmitProperty> columnInfoes, LocalBuilder item, LocalBuilder[] Propertys)
        {
            for (int index = 0; index < Propertys.Length; index++)
            {
                if (IsCompatibleType(columnInfoes[index].PropertyType, typeof(Int32)))
                {
                    // item.%Property% = arg.GetInt32(%index%);
                    ReadInt32(il, item, columnInfoes, Propertys, index);
                }
                else if (IsCompatibleType(columnInfoes[index].PropertyType, typeof(Int32?)))
                {
                    // item.%Property% = arg.IsDBNull ? default(int?) : (int?)arg.GetInt32(%index%);
                    ReadNullableInt32(il, item, columnInfoes, Propertys, index);
                }
                else if (IsCompatibleType(columnInfoes[index].PropertyType, typeof(Int64)))
                {
                    // item.%Property% = arg.GetInt64(%index%);
                    ReadInt64(il, item, columnInfoes, Propertys, index);
                }
                else if (IsCompatibleType(columnInfoes[index].PropertyType, typeof(Int64?)))
                {
                    // item.%Property% = arg.IsDBNull ? default(long?) : (long?)arg.GetInt64(%index%);
                    ReadNullableInt64(il, item, columnInfoes, Propertys, index);
                }
                else if (IsCompatibleType(columnInfoes[index].PropertyType, typeof(Decimal)))
                {
                    // item.%Property% = arg.GetDecimal(%index%);
                    ReadDecimal(il, item, columnInfoes[index].SetMethod, Propertys[index]);
                }
                else if (IsCompatibleType(columnInfoes[index].PropertyType, typeof(Decimal?)))
                {
                    // item.%Property% = arg.IsDBNull ? default(decimal?) : (int?)arg.GetDecimal(%index%);
                    ReadNullableDecimal(il, item, columnInfoes[index].SetMethod, Propertys[index]);
                }
                else if (IsCompatibleType(columnInfoes[index].PropertyType, typeof(DateTime)))
                {
                    // item.%Property% = arg.GetDateTime(%index%);
                    ReadDateTime(il, item, columnInfoes[index].SetMethod, Propertys[index]);
                }
                else if (IsCompatibleType(columnInfoes[index].PropertyType, typeof(DateTime?)))
                {
                    // item.%Property% = arg.IsDBNull ? default(DateTime?) : (int?)arg.GetDateTime(%index%);
                    ReadNullableDateTime(il, item, columnInfoes[index].SetMethod, Propertys[index]);
                }
                else
                {
                    // item.%Property% = (%PropertyType%)arg[%index%];
                    ReadObject(il, item, columnInfoes, Propertys, index);
                }
            }
        }

        /// <summary>
        /// 类型兼容性
        /// </summary>
        /// <param name="t1"></param>
        /// <param name="t2"></param>
        /// <returns></returns>
        private static bool IsCompatibleType(Type t1, Type t2)
        {
            if (t1 == t2)
                return true;
            if (t1.IsEnum && Enum.GetUnderlyingType(t1) == t2)
                return true;
            var u1 = Nullable.GetUnderlyingType(t1);
            var u2 = Nullable.GetUnderlyingType(t2);
            if (u1 != null && u2 != null)
                return IsCompatibleType(u1, u2);
            return false;
        }

        /// <summary>
        /// Int32赋值
        /// </summary>
        /// <param name="il"></param>
        /// <param name="item"></param>
        /// <param name="columnInfoes"></param>
        /// <param name="colIndices"></param>
        /// <param name="i"></param>
        private static void ReadInt32(ILGenerator il, LocalBuilder item, List<EmitProperty> columnInfoes, LocalBuilder[] colIndices, int i)
        {
            il.Emit(OpCodes.Ldloc_S, item);
            il.Emit(OpCodes.Ldarg_0);
            il.Emit(OpCodes.Ldloc_S, colIndices[i]);
            il.Emit(OpCodes.Callvirt, DataRecord_GetInt32);
            il.Emit(OpCodes.Callvirt, columnInfoes[i].SetMethod);
        }

        /// <summary>
        /// Int32?赋值
        /// </summary>
        /// <param name="il"></param>
        /// <param name="item"></param>
        /// <param name="columnInfoes"></param>
        /// <param name="colIndices"></param>
        /// <param name="i"></param>
        private static void ReadNullableInt32(ILGenerator il, LocalBuilder item, List<EmitProperty> columnInfoes, LocalBuilder[] colIndices, int i)
        {
            var local = il.DeclareLocal(columnInfoes[i].PropertyType);
            Label intNull = il.DefineLabel();
            Label intCommon = il.DefineLabel();
            il.Emit(OpCodes.Ldloca, local);
            il.Emit(OpCodes.Ldarg_0);
            il.Emit(OpCodes.Ldloc_S, colIndices[i]);
            il.Emit(OpCodes.Callvirt, DataRecord_IsDBNull);
            il.Emit(OpCodes.Brtrue_S, intNull);
            il.Emit(OpCodes.Ldarg_0);
            il.Emit(OpCodes.Ldloc_S, colIndices[i]);
            il.Emit(OpCodes.Callvirt, DataRecord_GetInt32);
            il.Emit(OpCodes.Call, columnInfoes[i].PropertyType.GetConstructor(new Type[] { Nullable.GetUnderlyingType(columnInfoes[i].PropertyType) }));
            il.Emit(OpCodes.Br_S, intCommon);
            il.MarkLabel(intNull);
            il.Emit(OpCodes.Initobj, columnInfoes[i].PropertyType);
            il.MarkLabel(intCommon);
            il.Emit(OpCodes.Ldloc_S, item);
            il.Emit(OpCodes.Ldloc, local);
            il.Emit(OpCodes.Callvirt, columnInfoes[i].SetMethod);
        }

        /// <summary>
        /// Int64赋值
        /// </summary>
        /// <param name="il"></param>
        /// <param name="item"></param>
        /// <param name="columnInfoes"></param>
        /// <param name="colIndices"></param>
        /// <param name="i"></param>
        private static void ReadInt64(ILGenerator il, LocalBuilder item, List<EmitProperty> columnInfoes, LocalBuilder[] colIndices, int i)
        {
            il.Emit(OpCodes.Ldloc_S, item);
            il.Emit(OpCodes.Ldarg_0);
            il.Emit(OpCodes.Ldloc_S, colIndices[i]);
            il.Emit(OpCodes.Callvirt, DataRecord_GetInt64);
            il.Emit(OpCodes.Callvirt, columnInfoes[i].SetMethod);
        }

        /// <summary>
        /// Int64?赋值
        /// </summary>
        /// <param name="il"></param>
        /// <param name="item"></param>
        /// <param name="columnInfoes"></param>
        /// <param name="colIndices"></param>
        /// <param name="i"></param>
        private static void ReadNullableInt64(ILGenerator il, LocalBuilder item, List<EmitProperty> columnInfoes, LocalBuilder[] colIndices, int i)
        {
            var local = il.DeclareLocal(columnInfoes[i].PropertyType);
            Label intNull = il.DefineLabel();
            Label intCommon = il.DefineLabel();
            il.Emit(OpCodes.Ldloca, local);
            il.Emit(OpCodes.Ldarg_0);
            il.Emit(OpCodes.Ldloc_S, colIndices[i]);
            il.Emit(OpCodes.Callvirt, DataRecord_IsDBNull);
            il.Emit(OpCodes.Brtrue_S, intNull);
            il.Emit(OpCodes.Ldarg_0);
            il.Emit(OpCodes.Ldloc_S, colIndices[i]);
            il.Emit(OpCodes.Callvirt, DataRecord_GetInt64);
            il.Emit(OpCodes.Call, columnInfoes[i].PropertyType.GetConstructor(new Type[] { Nullable.GetUnderlyingType(columnInfoes[i].PropertyType) }));
            il.Emit(OpCodes.Br_S, intCommon);
            il.MarkLabel(intNull);
            il.Emit(OpCodes.Initobj, columnInfoes[i].PropertyType);
            il.MarkLabel(intCommon);
            il.Emit(OpCodes.Ldloc_S, item);
            il.Emit(OpCodes.Ldloc, local);
            il.Emit(OpCodes.Callvirt, columnInfoes[i].SetMethod);
        }

        /// <summary>
        /// Decimal赋值
        /// </summary>
        /// <param name="il"></param>
        /// <param name="item"></param>
        /// <param name="setMethod"></param>
        /// <param name="colIndex"></param>
        private static void ReadDecimal(ILGenerator il, LocalBuilder item, MethodInfo setMethod, LocalBuilder colIndex)
        {
            il.Emit(OpCodes.Ldloc_S, item);
            il.Emit(OpCodes.Ldarg_0);
            il.Emit(OpCodes.Ldloc_S, colIndex);
            il.Emit(OpCodes.Callvirt, DataRecord_GetDecimal);
            il.Emit(OpCodes.Callvirt, setMethod);
        }

        /// <summary>
        /// Decimal?赋值
        /// </summary>
        /// <param name="il"></param>
        /// <param name="item"></param>
        /// <param name="setMethod"></param>
        /// <param name="colIndex"></param>
        private static void ReadNullableDecimal(ILGenerator il, LocalBuilder item, MethodInfo setMethod, LocalBuilder colIndex)
        {
            var local = il.DeclareLocal(typeof(decimal?));
            Label decimalNull = il.DefineLabel();
            Label decimalCommon = il.DefineLabel();
            il.Emit(OpCodes.Ldloca, local);
            il.Emit(OpCodes.Ldarg_0);
            il.Emit(OpCodes.Ldloc_S, colIndex);
            il.Emit(OpCodes.Callvirt, DataRecord_IsDBNull);
            il.Emit(OpCodes.Brtrue_S, decimalNull);
            il.Emit(OpCodes.Ldarg_0);
            il.Emit(OpCodes.Ldloc_S, colIndex);
            il.Emit(OpCodes.Callvirt, DataRecord_GetDecimal);
            il.Emit(OpCodes.Call, typeof(decimal?).GetConstructor(new Type[] { typeof(decimal) }));
            il.Emit(OpCodes.Br_S, decimalCommon);
            il.MarkLabel(decimalNull);
            il.Emit(OpCodes.Initobj, typeof(decimal?));
            il.MarkLabel(decimalCommon);
            il.Emit(OpCodes.Ldloc_S, item);
            il.Emit(OpCodes.Ldloc, local);
            il.Emit(OpCodes.Callvirt, setMethod);
        }

        /// <summary>
        /// DateTime赋值
        /// </summary>
        /// <param name="il"></param>
        /// <param name="item"></param>
        /// <param name="setMethod"></param>
        /// <param name="colIndex"></param>
        private static void ReadDateTime(ILGenerator il, LocalBuilder item, MethodInfo setMethod, LocalBuilder colIndex)
        {
            il.Emit(OpCodes.Ldloc_S, item);
            il.Emit(OpCodes.Ldarg_0);
            il.Emit(OpCodes.Ldloc_S, colIndex);
            il.Emit(OpCodes.Callvirt, DataRecord_GetDateTime);
            il.Emit(OpCodes.Callvirt, setMethod);
        }

        /// <summary>
        /// DateTime?赋值
        /// </summary>
        /// <param name="il"></param>
        /// <param name="item"></param>
        /// <param name="setMethod"></param>
        /// <param name="colIndex"></param>
        private static void ReadNullableDateTime(ILGenerator il, LocalBuilder item, MethodInfo setMethod, LocalBuilder colIndex)
        {
            var local = il.DeclareLocal(typeof(DateTime?));
            Label dtNull = il.DefineLabel();
            Label dtCommon = il.DefineLabel();
            il.Emit(OpCodes.Ldloca, local);
            il.Emit(OpCodes.Ldarg_0);
            il.Emit(OpCodes.Ldloc_S, colIndex);
            il.Emit(OpCodes.Callvirt, DataRecord_IsDBNull);
            il.Emit(OpCodes.Brtrue_S, dtNull);
            il.Emit(OpCodes.Ldarg_0);
            il.Emit(OpCodes.Ldloc_S, colIndex);
            il.Emit(OpCodes.Callvirt, DataRecord_GetDateTime);
            il.Emit(OpCodes.Call, typeof(DateTime?).GetConstructor(new Type[] { typeof(DateTime) }));
            il.Emit(OpCodes.Br_S, dtCommon);
            il.MarkLabel(dtNull);
            il.Emit(OpCodes.Initobj, typeof(DateTime?));
            il.MarkLabel(dtCommon);
            il.Emit(OpCodes.Ldloc_S, item);
            il.Emit(OpCodes.Ldloc, local);
            il.Emit(OpCodes.Callvirt, setMethod);
        }

        /// <summary>
        /// 特殊对象赋值
        /// </summary>
        /// <param name="il"></param>
        /// <param name="item"></param>
        /// <param name="columnInfoes"></param>
        /// <param name="colIndices"></param>
        /// <param name="i"></param>
        private static void ReadObject(ILGenerator il, LocalBuilder item, List<EmitProperty> columnInfoes, LocalBuilder[] colIndices, int i)
        {
            Label common = il.DefineLabel();
            //item.
            il.Emit(OpCodes.Ldloc_S, item);
            il.Emit(OpCodes.Ldarg_0);
            //_=item[(%Property%)]
            il.Emit(OpCodes.Ldloc_S, colIndices[i]);
            il.Emit(OpCodes.Callvirt, DataRecord_ItemGetter_Int);
            il.Emit(OpCodes.Dup);
            //if(_.IsDBNull())
            il.Emit(OpCodes.Call, Convert_IsDBNull);
            il.Emit(OpCodes.Brfalse_S, common);
            //{
            //_=null
            il.Emit(OpCodes.Pop);
            il.Emit(OpCodes.Ldnull);
            //}
            il.MarkLabel(common);
            //(PropertyType)_
            il.Emit(OpCodes.Unbox_Any, columnInfoes[i].PropertyType);
            //item.(%Property%)=_
            il.Emit(OpCodes.Callvirt, columnInfoes[i].SetMethod);
        }

        /// <summary>
        /// 控制台功能
        /// </summary>
        /// <param name="il"></param>
        /// <param name="item"></param>
        private static void ConsoleWL(ILGenerator il, LocalBuilder item = null)
        {
            il.Emit(OpCodes.Ldarg_0);
            if (item == null)
                il.Emit(OpCodes.Ldc_I4, 99999);
            else
                il.Emit(OpCodes.Stloc_S, item);
            il.Emit(OpCodes.Call, ConsoleWriteLine);
            il.Emit(OpCodes.Pop);
        }
    }
}
