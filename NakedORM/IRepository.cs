using System;
using System.Collections.Generic;
using System.Data;
using System.Text;

namespace NakedORM
{
    public interface IRepository
    {
        /// <summary>
        /// 初始化MSSQL
        /// </summary>
        DbNakedContext ConnectMSSQL();

        /// <summary>
        /// 初始化MYSQL
        /// </summary>
        DbNakedContext ConnectMySQL();

        /// <summary>
        /// 初始化ORACLE
        /// </summary>
        DbNakedContext ConnectOracle();
    }
}
