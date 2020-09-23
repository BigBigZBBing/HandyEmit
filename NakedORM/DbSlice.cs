using System;
using System.Collections.Generic;
using System.Text;

namespace NakedORM
{
    /// <summary>
    /// 数据库切片结果集
    /// </summary>
    /// <typeparam name="T"></typeparam>
    public class DbSlice<T>
    {
        internal DbSlice()
        {
        }

        /// <summary>
        /// 执行的SQL
        /// </summary>
        public String ExecuteSql { get; internal set; }

        /// <summary>
        /// 返回数据集
        /// </summary>
        public T Data { get; internal set; }

        /// <summary>
        /// 返回结果
        /// </summary>
        public Boolean Succeed { get; internal set; }

        /// <summary>
        /// 执行时间
        /// </summary>
        public TimeSpan ExecuteTime { get; internal set; }

        /// <summary>
        /// 异常消息
        /// </summary>
        public String Message { get; internal set; }

    }
}
