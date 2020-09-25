using NakedORM.Core;
using System;
using System.Collections.Generic;
using System.Data;
using System.Text;

namespace NakedORM
{
    public static class CustomFunc
    {
        /// <summary>
        /// 自定义SQL查询
        /// </summary>
        /// <typeparam name="T">实体类型</typeparam>
        /// <param name="con">数据库连接</param>
        /// <param name="sql">执行SQL</param>
        /// <param name="parameter">参数化参数</param>
        /// <returns></returns>
        public static DbSlice<IEnumerable<T>> Query<T>(this DbNakedContext con, String sql, IList<DbField> parameter) where T : class, new()
        {
            return con.SqlRead<T>(sql, null, parameter, null);
        }

        /// <summary>
        /// 自定义SQL查询
        /// </summary>
        /// <typeparam name="T">实体类型</typeparam>
        /// <param name="con">数据库连接</param>
        /// <param name="sql">执行SQL</param>
        /// <param name="parameter">参数化参数</param>
        /// <returns></returns>
        public static DbSlice<IEnumerable<T>> Query<T>(this DbNakedContext con, String sql, Action<IList<DbField>> parameter) where T : class, new()
        {
            List<DbField> fields = new List<DbField>(); parameter?.Invoke(fields);
            return con.SqlRead<T>(sql, null, fields, null);
        }
    }
}
