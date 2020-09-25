using System;
using System.Collections.Generic;
using System.Data;
using System.Text;

namespace NakedORM
{
    public class DbNakedTransaction : IDisposable
    {
        private bool disposedValue;

        public DbNakedTransaction(IDbTransaction tran)
        {
            this.tran = tran;
        }

        internal IDbTransaction tran { get; set; }

        /// <summary>
        /// 提交事务
        /// </summary>
        public void Commit()
        {
            if (this.tran != null && this.tran.Connection != null)
                this.tran.Commit();
        }

        /// <summary>
        /// 回退事务
        /// </summary>
        public void Rollback()
        {
            if (this.tran != null && this.tran.Connection != null)
                this.tran.Rollback();
        }

        protected virtual void Dispose(bool disposing)
        {
            if (!disposedValue)
            {
                if (disposing)
                {
                    // TODO: 释放托管状态(托管对象)
                    this.tran?.Dispose();
                    this.tran = null;
                }

                // TODO: 释放未托管的资源(未托管的对象)并替代终结器
                // TODO: 将大型字段设置为 null
                disposedValue = true;
            }
        }

        // // TODO: 仅当“Dispose(bool disposing)”拥有用于释放未托管资源的代码时才替代终结器
        // ~DbNakedTransaction()
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
