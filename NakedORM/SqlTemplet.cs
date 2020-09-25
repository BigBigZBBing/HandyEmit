using System;
using System.Collections.Generic;
using System.Text;

namespace NakedORM
{
    internal static class SqlTemplet
    {
        /// <summary>
        /// 生产执行用的SQL
        /// </summary>
        /// <param name="select">获取字段</param>
        /// <param name="from">数据集</param>
        /// <param name="where">条件</param>
        /// <returns></returns>
        internal static String ProSql(String select = "", String from = "", String where = "", String GroupBy = "", String OrderBy = "")
        {
            if (where != String.Empty)
                where = $" WHERE {where} ";
            return $" SELECT {select} FROM {from} {where} {GroupBy} {OrderBy} ";
        }

        /// <summary>
        /// 分页查询(2012以后方式)
        /// </summary>
        /// <param name="Sql"></param>
        /// <param name="Key"></param>
        /// <returns></returns>
        internal static String OldPagerToSql(String Sql, String Key)
        {
            return $@"SELECT PAGER.* 
                      FROM ( SELECT WITHTABLE.*,
                      ROW_NUMBER() OVER ( ORDER BY WITHTABLE.{Key} ASC) RN,
                      COUNT(*) OVER() TotalCount 
                      FROM ( {Sql} ) WITHTABLE ) PAGER 
                      WHERE PAGER.RN BETWEEN ((@PagerIndex - 1) * @PagerSize + 1 ) AND ( @PagerIndex * @PagerSize ) ";
        }

        /// <summary>
        /// 分页查询(2012以前方式)
        /// </summary>
        /// <param name="Sql"></param>
        /// <param name="Key"></param>
        /// <returns></returns>
        internal static String PagerToSql(String Sql, String Key)
        {
            return $@" SELECT PAGER.*,
                       COUNT(*) OVER() TotalCount 
                       FROM ( {Sql} ) PAGER 
                       ORDER BY PAGER.{Key} ASC 
                       OFFSET ((@PagerIndex - 1) * @PagerSize) ROWS FETCH NEXT @PagerSize ROWS ONLY ";
        }

        /// <summary>
        /// 插入数据
        /// </summary>
        /// <param name="Table"></param>
        /// <param name="Fields"></param>
        /// <param name="DataSet"></param>
        /// <returns></returns>
        internal static String InsertSql(String Table, String Fields, String DataSet)
        {
            return $@"INSERT INTO {Table} 
                      SELECT {Fields} FROM
                      ( {DataSet} ) DATA";
        }

        /// <summary>
        /// 批量插入数据
        /// </summary>
        /// <param name="Table"></param>
        /// <param name="Fields"></param>
        /// <param name="DataSet"></param>
        /// <returns></returns>
        internal static String BulkInsertSql(String Table, String Fields)
        {
            return $@"INSERT INTO {Table} 
                      SELECT {Fields} FROM @TVP ";
        }

        /// <summary>
        /// 更新数据
        /// </summary>
        /// <param name="Table"></param>
        /// <param name="Set"></param>
        /// <param name="Where"></param>
        /// <returns></returns>
        internal static String UpdateSql(String Table, String Set, String Where)
        {
            return $@"UPDATE {Table} SET 
                      {Set} 
                      WHERE 
                      {Where} ";
        }

        /// <summary>
        /// 删除数据
        /// </summary>
        /// <param name="Table"></param>
        /// <param name="Where"></param>
        /// <returns></returns>
        internal static String DeleteSql(String Table, String Where)
        {
            return $@"DELETE FROM {Table} 
                      WHERE 
                      {Where} ";
        }
    }
}
