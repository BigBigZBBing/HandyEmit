using NakedORM.Core;
using System;
using System.Collections.Generic;
using System.Data;
using System.Text;

namespace NakedORM.Simple
{
    public static class DeleteFunc
    {
        /// <summary>
        /// 根据主键删除
        /// </summary>
        /// <typeparam name="T">实体类型</typeparam>
        /// <param name="con">数据库上下文</param>
        /// <param name="key">主键</param>
        /// <returns></returns>
        public static DbSlice<Int32> Delete<T>(this DbNakedContext con, Object key)
        {
            var where = new List<DbField>()._(DbCore.EntityKey<T>(), key);
            String sqlStr = SqlTemplet.DeleteSql(DbCore.EntityTable<T>(), DbCore.ParamWhere(where));
            return con.Execute<T>(sqlStr, where: where);
        }

        /// <summary>
        /// 根据条件删除
        /// </summary>
        /// <typeparam name="T">实体类型</typeparam>
        /// <param name="con">数据库上下文</param>
        /// <param name="where">查询条件</param>
        /// <returns></returns>
        public static DbSlice<Int32> Delete<T>(this DbNakedContext con, IList<DbField> where)
        {
            String sqlStr = SqlTemplet.DeleteSql(DbCore.EntityTable<T>(), DbCore.ParamWhere(where));
            return con.Execute<T>(sqlStr, where: where);
        }

        /// <summary>
        /// 根据条件删除
        /// </summary>
        /// <typeparam name="T">实体类型</typeparam>
        /// <param name="con">数据库上下文</param>
        /// <param name="where">查询条件</param>
        /// <returns></returns>
        public static DbSlice<Int32> Delete<T>(this DbNakedContext con, Action<IList<DbField>> where)
        {
            IList<DbField> wherefields = new List<DbField>(); where?.Invoke(wherefields);
            String sqlStr = SqlTemplet.DeleteSql(DbCore.EntityTable<T>(), DbCore.ParamWhere(wherefields));
            return con.Execute<T>(sqlStr, where: wherefields);
        }
    }
}
