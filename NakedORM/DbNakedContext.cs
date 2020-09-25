using System;
using System.Collections.Generic;
using System.Text;
using System.Data;

namespace NakedORM
{
    /// <summary>
    /// DB连接上下文
    /// </summary>
    public class DbNakedContext : IDisposable
    {
        private bool disposedValue;

        public DbNakedContext(IDbConnection Connection)
        {
            this._Connection = Connection;
            this._Command = this._Connection.CreateCommand();
        }

        /// <summary>
        /// 数据库连接
        /// </summary>
        internal IDbConnection _Connection { get; set; }

        /// <summary>
        /// 连接运行时命令行
        /// </summary>
        internal IDbCommand _Command { get; set; }

        /// <summary>
        /// 连接运行时事务
        /// </summary>
        internal IDbTransaction _Transaction { get; set; }

        /// <summary>
        /// 开启事务
        /// </summary>
        /// <returns></returns>
        public DbNakedTransaction BeginTranScoape()
        {
            _Transaction = _Connection.BeginTransaction(IsolationLevel.Unspecified);
            return new DbNakedTransaction(_Transaction);
        }

        /// <summary>
        /// 开启事务
        /// </summary>
        /// <returns></returns>
        public DbNakedTransaction BeginTranScoape(IsolationLevel il)
        {
            _Transaction = _Connection.BeginTransaction(il);
            return new DbNakedTransaction(_Transaction);
        }

        protected virtual void Dispose(bool disposing)
        {
            if (!disposedValue)
            {
                if (disposing)
                {
                    // TODO: 释放托管状态(托管对象)
                    this._Command.Dispose();
                    this._Connection.Close();
                    this._Connection.Dispose();
                    this._Command = null;
                    this._Connection = null;
                    this._Connection = null;
                }

                // TODO: 释放未托管的资源(未托管的对象)并替代终结器
                // TODO: 将大型字段设置为 null
                disposedValue = true;
            }
        }

        // // TODO: 仅当“Dispose(bool disposing)”拥有用于释放未托管资源的代码时才替代终结器
        // ~DbContext()
        // {
        //     // 不要更改此代码。请将清理代码放入“Dispose(bool disposing)”方法中
        //     Dispose(disposing: false);
        // }

        public void Dispose()
        {
            // 不要更改此代码。请将清理代码放入“Dispose(bool disposing)”方法中
            Dispose(disposing: true);
            GC.SuppressFinalize(this);
        }
    }

}
