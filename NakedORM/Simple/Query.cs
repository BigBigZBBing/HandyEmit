using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq.Expressions;
using System.Reflection;
using System.Text;
using NakedORM.Core;

namespace NakedORM.Simple
{
    public static class QueryFunc
    {
        /// <summary>
        /// 主键查询(模型中必须带[Key]特性)
        /// </summary>
        /// <typeparam name="T">实体类型</typeparam>
        /// <param name="con">数据库上下文</param>
        /// <param name="key">主键值</param>
        /// <param name="Reveal">显示字段</param>
        /// <returns></returns>
        public static DbSlice<IEnumerable<T>> GetKey<T>(
            this DbNakedContext con,
            Object key,
            Expression<Func<T, dynamic>> Reveal = null
            ) where T : class, new()
        {
            var where = new List<DbField>()._(DbCore.EntityKey<T>(), key);
            var sqlStr = SqlTemplet.ProSql(DbCore.EntityField<T>(Reveal), DbCore.EntityTable<T>(), DbCore.ParamWhere(where));
            return con.SqlRead<T>(sqlStr, Reveal, where, null);
        }

        /// <summary>
        /// 获取数据集
        /// </summary>
        /// <typeparam name="T">实体类型</typeparam>
        /// <param name="con">数据库上下文</param>
        /// <param name="where">查询条件</param>
        /// <param name="Reveal">显示字段</param>
        /// <param name="GroupBy">分组</param>
        /// <param name="OrderBy">排序</param>
        /// <returns></returns>
        public static DbSlice<IEnumerable<T>> GetList<T>(
            this DbNakedContext con,
            List<DbField> where,
            Expression<Func<T, dynamic>> Reveal = null,
            Expression<Func<T, dynamic>> GroupBy = null,
            Expression<Func<T, dynamic>> OrderBy = null
            ) where T : class, new()
        {
            var sqlStr = SqlTemplet.ProSql(DbCore.EntityField<T>(Reveal), DbCore.EntityTable<T>(), DbCore.ParamWhere(where), DbCore.EntityGroupBy<T>(GroupBy), DbCore.EntityOrderBy<T>(OrderBy));
            return con.SqlRead<T>(sqlStr, Reveal, where, null);
        }

        /// <summary>
        /// 获取数据集
        /// </summary>
        /// <typeparam name="T">实体类型</typeparam>
        /// <param name="con">数据库上下文</param>
        /// <param name="where">查询条件</param>
        /// <param name="Reveal">显示字段</param>
        /// <param name="GroupBy">分组</param>
        /// <param name="OrderBy">排序</param>
        /// <returns></returns>
        public static DbSlice<IEnumerable<T>> GetList<T>(
            this DbNakedContext con,
            Action<List<DbField>> where = null,
            Expression<Func<T, dynamic>> Reveal = null,
            Expression<Func<T, dynamic>> GroupBy = null,
            Expression<Func<T, dynamic>> OrderBy = null
            ) where T : class, new()
        {
            List<DbField> fields = new List<DbField>(); where?.Invoke(fields);
            var sqlStr = SqlTemplet.ProSql(DbCore.EntityField<T>(Reveal), DbCore.EntityTable<T>(), DbCore.ParamWhere(fields), DbCore.EntityGroupBy<T>(GroupBy), DbCore.EntityOrderBy<T>(OrderBy));
            return con.SqlRead<T>(sqlStr, Reveal, fields, null);
        }

        /// <summary>
        /// 分页查询
        /// </summary>
        /// <typeparam name="T">实体类型</typeparam>
        /// <param name="con">数据库上下文</param>
        /// <param name="pager">分页条件</param>
        /// <param name="where">查询条件</param>
        /// <param name="Reveal">显示字段</param>
        /// <param name="GroupBy">分组</param>
        /// <param name="OrderBy">排序</param>
        /// <returns></returns>
        public static DbSlice<IEnumerable<T>> GetListPager<T>(
            this DbNakedContext con,
            DbPager pager,
            List<DbField> where,
            Expression<Func<T, dynamic>> Reveal = null,
            Expression<Func<T, dynamic>> GroupBy = null,
            Expression<Func<T, dynamic>> OrderBy = null
            ) where T : class, new()
        {
            var sqlStr = SqlTemplet.OldPagerToSql(SqlTemplet.ProSql(DbCore.EntityField<T>(Reveal), DbCore.EntityTable<T>(), DbCore.ParamWhere(where), DbCore.EntityGroupBy<T>(GroupBy), DbCore.EntityOrderBy<T>(OrderBy)), DbCore.EntityPagerKey<T>());
            return con.SqlRead<T>(sqlStr, Reveal, where, pager);
        }

        /// <summary>
        /// 分页查询
        /// </summary>
        /// <typeparam name="T">实体类型</typeparam>
        /// <param name="con">数据库上下文</param>
        /// <param name="pager">分页条件</param>
        /// <param name="where">查询条件</param>
        /// <param name="Reveal">显示字段</param>
        /// <param name="GroupBy">分组</param>
        /// <param name="OrderBy">排序</param>
        /// <returns></returns>
        public static DbSlice<IEnumerable<T>> GetListPager<T>(
            this DbNakedContext con,
            DbPager pager,
            Action<List<DbField>> where = null,
            Expression<Func<T, dynamic>> Reveal = null,
            Expression<Func<T, dynamic>> GroupBy = null,
            Expression<Func<T, dynamic>> OrderBy = null
            ) where T : class, new()
        {
            List<DbField> fields = new List<DbField>();
            where?.Invoke(fields);
            var sqlStr = SqlTemplet.OldPagerToSql(SqlTemplet.ProSql(DbCore.EntityField<T>(Reveal), DbCore.EntityTable<T>(), DbCore.ParamWhere(fields), DbCore.EntityGroupBy<T>(GroupBy), DbCore.EntityOrderBy<T>(OrderBy)), DbCore.EntityPagerKey<T>());
            return con.SqlRead<T>(sqlStr, Reveal, fields, pager);
        }

        /// <summary>
        /// 分页查询(2012以上使用)
        /// </summary>
        /// <typeparam name="T">实体类型</typeparam>
        /// <param name="con">数据库上下文</param>
        /// <param name="pager">分页条件</param>
        /// <param name="where">查询条件</param>
        /// <param name="Reveal">显示字段</param>
        /// <param name="GroupBy">分组</param>
        /// <param name="OrderBy">排序</param>
        /// <returns></returns>
        public static DbSlice<IEnumerable<T>> GetListPager2012<T>(
            this DbNakedContext con,
            DbPager pager,
            List<DbField> where,
            Expression<Func<T, dynamic>> Reveal = null,
            Expression<Func<T, dynamic>> GroupBy = null,
            Expression<Func<T, dynamic>> OrderBy = null
            ) where T : class, new()
        {
            var sqlStr = SqlTemplet.PagerToSql(SqlTemplet.ProSql(DbCore.EntityField<T>(Reveal), DbCore.EntityTable<T>(), DbCore.ParamWhere(where), DbCore.EntityGroupBy<T>(GroupBy), DbCore.EntityOrderBy<T>(OrderBy)), DbCore.EntityPagerKey<T>());
            return con.SqlRead<T>(sqlStr, Reveal, where, pager);
        }

        /// <summary>
        /// 分页查询(2012以上使用)
        /// </summary>
        /// <typeparam name="T">实体类型</typeparam>
        /// <param name="con">数据库上下文</param>
        /// <param name="pager">分页条件</param>
        /// <param name="where">查询条件</param>
        /// <param name="Reveal">显示字段</param>
        /// <param name="GroupBy">分组</param>
        /// <param name="OrderBy">排序</param>
        /// <returns></returns>
        public static DbSlice<IEnumerable<T>> GetListPager2012<T>(
            this DbNakedContext con,
            DbPager pager,
            Action<IList<DbField>> where = null,
            Expression<Func<T, dynamic>> Reveal = null,
            Expression<Func<T, dynamic>> GroupBy = null,
            Expression<Func<T, dynamic>> OrderBy = null
            ) where T : class, new()
        {
            List<DbField> fields = new List<DbField>(); where?.Invoke(fields);
            var sqlStr = SqlTemplet.PagerToSql(SqlTemplet.ProSql(DbCore.EntityField<T>(Reveal), DbCore.EntityTable<T>(), DbCore.ParamWhere(fields), DbCore.EntityGroupBy<T>(GroupBy), DbCore.EntityOrderBy<T>(OrderBy)), DbCore.EntityPagerKey<T>());
            return con.SqlRead<T>(sqlStr, Reveal, fields, pager);
        }
    }
}
