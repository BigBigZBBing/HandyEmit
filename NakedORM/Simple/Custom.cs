using NakedORM.Core;
using System;
using System.Collections.Generic;
using System.Data;
using System.Text;

namespace NakedORM
{
    public static class CustomFunc
    {
        public static DbSlice<IEnumerable<T>> Query<T>(this DbNakedContext con, String sql, IList<DbField> where) where T : class, new()
        {
            return con.SqlRead<T>(sql, null, where, null);
        }

        public static DbSlice<IEnumerable<T>> Query<T>(this DbNakedContext con, String sql, Action<IList<DbField>> where) where T : class, new()
        {
            List<DbField> fields = new List<DbField>(); where?.Invoke(fields);
            return con.SqlRead<T>(sql, null, fields, null);
        }
    }
}
