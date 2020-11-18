using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using NakedORM.Core;

namespace NakedORM.Simple
{
    public static class InsertFunc
    {
        /// <summary>
        /// 单数据插入
        /// </summary>
        /// <typeparam name="T">实体类型</typeparam>
        /// <param name="con">数据库上下文</param>
        /// <param name="entity">实体对象</param>
        /// <returns></returns>
        public static DbSlice<Int32> Add<T>(this DbNakedContext con, T entity) where T : class
        {
            if (typeof(T).IsGenericType) throw new Exception("批量新增请用AddRange函数");
            String sqlStr = SqlTemplet.InsertSql(DbCore.EntityTable<T>(), DbCore.EntityFieldNoKey<T>(), DbCore.InsertValues<T>(1));
            return con.Execute<T>(sqlStr, entities: new List<T>() { entity });
        }

        /// <summary>
        /// 批量数据插入
        /// </summary>
        /// <typeparam name="T">实体类型</typeparam>
        /// <param name="con">数据库上下文</param>
        /// <param name="entities">实体对象集合</param>
        /// <returns></returns>
        public static DbSlice<Int32> AddRange<T>(this DbNakedContext con, IList<T> entities) where T : class
        {
            DbSlice<Int32> res = null;
            String sqlStr = SqlTemplet.BulkInsertSql(DbCore.EntityTable<T>(), DbCore.EntityFieldNoKey<T>());
            var exres = con.Execute<T>(sqlStr, entities: entities);
            return res;
        }
    }
}
