using MySql.Data.MySqlClient;
using Oracle.ManagedDataAccess.Client;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Text;

namespace NakedORM
{
    public class Repository : IRepository, IDisposable
    {
        private bool disposedValue;

        public Repository(string connectString)
        {
            ConnectString = connectString;
        }

        /// <summary>
        /// 连接字符串
        /// </summary>
        private String ConnectString { get; set; }

        /// <summary>
        /// 数据库连接实例
        /// </summary>
        private IDbConnection con;

        /// <summary>
        /// 初始化MSSQL
        /// </summary>
        public DbNakedContext ConnectMSSQL()
        {
            con = new SqlConnection(ConnectString);
            if (con.State != ConnectionState.Open)
                con.Open();
            return new DbNakedContext(con);
        }

        /// <summary>
        /// 初始化MYSQL
        /// </summary>
        public DbNakedContext ConnectMySQL()
        {
            con = new MySqlConnection(ConnectString);
            if (con.State != ConnectionState.Open)
                con.Open();
            return new DbNakedContext(con);
        }

        /// <summary>
        /// 初始化ORACLE
        /// </summary>
        public DbNakedContext ConnectOracle()
        {
            con = new OracleConnection(ConnectString);
            if (con.State != ConnectionState.Open)
                con.Open();
            return new DbNakedContext(con);
        }

        protected virtual void Dispose(bool disposing)
        {
            if (!disposedValue)
            {
                if (disposing)
                {
                    // TODO: 释放托管状态(托管对象)
                    if (con.State == ConnectionState.Open)
                        con.Close();
                    con.Dispose();
                }

                // TODO: 释放未托管的资源(未托管的对象)并替代终结器
                // TODO: 将大型字段设置为 null
                disposedValue = true;
            }
        }

        // // TODO: 仅当“Dispose(bool disposing)”拥有用于释放未托管资源的代码时才替代终结器
        // ~Repository()
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
