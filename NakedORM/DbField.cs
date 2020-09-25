using System;
using System.Collections.Generic;
using System.Text;

namespace NakedORM
{
    /// <summary>
    /// 查询集合对象
    /// </summary>
    public sealed class DbField
    {
        internal DbField(string field, object value, DbFunc dbFunc = DbFunc.Equal, string orGroup = default)
        {
            Field = field;
            Value = value;
            DbFunc = dbFunc;
            OrGroup = orGroup;
        }

        /// <summary>
        /// 字段名称
        /// </summary>
        public String Field { get; set; }

        /// <summary>
        /// 字段值
        /// </summary>
        public Object Value { get; set; }

        /// <summary>
        /// 条件类型
        /// </summary>
        public DbFunc DbFunc { get; set; }

        /// <summary>
        /// Or分组唯一Key
        /// </summary>
        public String OrGroup { get; set; }
    }

    /// <summary>
    /// 分页集合
    /// </summary>
    public sealed class DbPager
    {
        public DbPager(Int32 pagerIndex = 1, Int32 pagerSize = 15, Int64 totalCount = 0)
        {
            PagerIndex = pagerIndex;
            PagerSize = pagerSize;
            TotalCount = totalCount;
        }

        /// <summary>
        /// 页数
        /// </summary>
        public Int32 PagerIndex { get; set; } = 1;

        /// <summary>
        /// 每页显示数
        /// </summary>
        public Int32 PagerSize { get; set; } = 15;

        /// <summary>
        /// 数据总数
        /// </summary>
        public Int64 TotalCount { get; set; }
    }

    /// <summary>
    /// 比较符扩展
    /// </summary>
    public enum DbFunc
    {
        /// <summary>
        /// 等于
        /// </summary>
        Equal,
        /// <summary>
        /// 小于
        /// </summary>
        Less,
        /// <summary>
        /// 大于
        /// </summary>
        Greater,
        /// <summary>
        /// 小于等于
        /// </summary>
        LessEqual,
        /// <summary>
        /// 大于等于
        /// </summary>
        GreaterEqual,
        /// <summary>
        /// 模糊查询
        /// </summary>
        Like,
        /// <summary>
        /// 包含
        /// </summary>
        In,
    }

    [AttributeUsage(AttributeTargets.Property, Inherited = true)]
    public class PagerKeyAttribute : Attribute
    {
        public PagerKeyAttribute() { }
    }

    public static class GX
    {
        /// <summary>
        /// 快捷新增字段集合
        /// </summary>
        /// <param name="field"></param>
        /// <param name="Field">字段名称</param>
        /// <param name="Value">字段值</param>
        /// <param name="DbFunc">判断条件</param>
        /// <returns></returns>
        public static IList<DbField> _(this IList<DbField> field, String Field, Object Value, DbFunc DbFunc = DbFunc.Equal, String orGroup = default)
        {
            field.Add(new DbField(Field, Value, DbFunc, orGroup));
            return field;
        }

        public static List<DbField> _(this List<DbField> field, String Field, Object Value, DbFunc DbFunc = DbFunc.Equal, String orGroup = default)
        {
            field.Add(new DbField(Field, Value, DbFunc, orGroup));
            return field;
        }

        public static void Add(this IList<DbField> where, string field, object value, DbFunc dbFunc = DbFunc.Equal, string orGroup = default)
        {
            where.Add(new DbField(field, value, dbFunc, orGroup));
        }

        public static void Add(this List<DbField> where, string field, object value, DbFunc dbFunc = DbFunc.Equal, string orGroup = default)
        {
            where.Add(new DbField(field, value, dbFunc, orGroup));
        }
    }
}
