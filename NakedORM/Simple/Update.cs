using NakedORM.Core;
using System;
using System.Collections.Generic;
using System.Data;
using System.Text;

namespace NakedORM.Simple
{
    public static class UpdateFunc
    {
        /// <summary>
        /// 根据条件更新数据
        /// </summary>
        /// <typeparam name="T">实体类型</typeparam>
        /// <param name="con">数据库连接</param>
        /// <param name="set">更新范围</param>
        /// <param name="where">更新条件</param>
        /// <returns></returns>
        public static DbSlice<Int32> Update<T>(this DbNakedContext con, IList<DbField> set, IList<DbField> where)
        {
            String sqlStr = SqlTemplet.UpdateSql(DbCore.EntityTable<T>(), DbCore.ParamSet(set), DbCore.ParamWhere(where));
            return con.Execute<T>(sqlStr, set, where);
        }

        /// <summary>
        /// 根据条件更新数据
        /// </summary>
        /// <typeparam name="T">实体类型</typeparam>
        /// <param name="con">数据库连接</param>
        /// <param name="set">更新范围</param>
        /// <param name="where">更新条件</param>
        /// <returns></returns>
        public static DbSlice<Int32> Update<T>(this DbNakedContext con, Action<IList<DbField>> set, Action<IList<DbField>> where)
        {
            IList<DbField> wherefields = new List<DbField>(); where?.Invoke(wherefields);
            IList<DbField> setfields = new List<DbField>(); set?.Invoke(setfields);
            String sqlStr = SqlTemplet.UpdateSql(DbCore.EntityTable<T>(), DbCore.ParamSet(setfields), DbCore.ParamWhere(wherefields));
            return con.Execute<T>(sqlStr, setfields, wherefields);
        }
    }
}
