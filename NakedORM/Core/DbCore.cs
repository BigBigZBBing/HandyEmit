using ILWheatBread;
using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Data;
using System.Linq;
using System.Linq.Expressions;
using System.Reflection;
using System.Reflection.Emit;
using System.Text;
using System.Diagnostics;


namespace NakedORM.Core
{
    internal static class DbCore
    {

        /// <summary>
        /// 读取数据集
        /// </summary>
        /// <param name="con">SQL连接对象</param>
        /// <param name="sql">SQL字符串</param>
        /// <param name="where">查询参数化对象</param>
        /// <param name="pager">分页参数化对象</param>
        /// <param name="tran">事务对象</param>
        /// <returns></returns>
        internal static IDataReader Read(this DbNakedContext con, String sql, IList<DbField> where, DbPager pager)
        {
            //判断连接是否打开
            if (con._Connection.State != ConnectionState.Open)
                con._Connection.Open();

            //创建命令对象
            IDbCommand command = con._Command;

            //设置运行的SQL
            command.CommandText = sql;

            //注入参数
            command.InjectParams<Object>(where: where, pager: pager);

            //分配事务
            if (con._Transaction.Connection != null)
                command.Transaction = con._Transaction;

            //执行命令
            return command.ExecuteReader();
        }

        /// <summary>
        /// 执行增删改
        /// </summary>
        /// <typeparam name="T">数据实体</typeparam>
        /// <param name="con">SQL连接对象</param>
        /// <param name="sql">SQL字符串</param>
        /// <param name="set">修改参数化对象</param>
        /// <param name="where">查询参数化对象</param>
        /// <param name="Entities">新增参数化对象</param>
        /// <param name="tran">事务对象</param>
        /// <returns></returns>
        internal static DbSlice<Int32> Execute<T>(this DbNakedContext con, String sql, IList<DbField> set = null, IList<DbField> where = null, IList<T> entities = null)
        {
            var slice = new DbSlice<Int32>();

            //判断连接是否打开
            if (con._Connection.State != ConnectionState.Open)
                con._Connection.Open();

            IDbCommand command = null;

            Stopwatch runTime = new Stopwatch();
            runTime.Start();
            try
            {
                //创建命令对象
                //command = con._Command;
                command = con._Command;

                //设置运行的SQL
                command.CommandText = sql;

                //注入参数
                command.InjectParams<T>(set: set, where: where, entities: entities);

                //分配事务
                if (con._Transaction.Connection != null)
                    command.Transaction = con._Transaction;

                //执行命令
                slice.Data = command.ExecuteNonQuery();
                slice.Succeed = true;
            }
            catch (System.Exception ex)
            {
                slice.Succeed = false;
                slice.Message = ex.Message;
                throw ex;
            }
            finally
            {
                runTime.Stop();
                slice.ExecuteTime = runTime.Elapsed;
                slice.ExecuteSql = sql;
            }
            return slice;
        }

        /// <summary>
        /// 注入参数
        /// </summary>
        /// <param name="command">命令对象</param>
        /// <param name="where">参数化对象</param>
        internal static void InjectParams<T>(this IDbCommand command, IList<DbField> set = null, IList<DbField> where = null, IList<T> entities = null, DbPager pager = null)
        {
            //数据参数对象
            IDbDataParameter parameter;

            if (command.Parameters.Count != 0)
                command.Parameters.Clear();

            if (where != null)
            {
                //参数注入
                foreach (var prop in where)
                {
                    parameter = command.CreateParameter();
                    parameter.ParameterName = ToWherePropertyName(prop);
                    parameter.Value = prop.Value;
                    command.Parameters.Add(parameter);
                }
            }

            if (set != null)
            {
                //参数注入
                foreach (var prop in set)
                {
                    parameter = command.CreateParameter();
                    parameter.ParameterName = ToSetPropertyName(prop);
                    parameter.Value = prop.Value;
                    command.Parameters.Add(parameter);
                }
            }

            if (entities != null)
            {
                List<EmitProperty> props = typeof(T).ToEmitProps().ToList();
                //参数注入
                for (int i = 0, t = 0; i < entities.Count; t++)
                {
                    foreach (var prop in props)
                    {
                        parameter = command.CreateParameter();
                        parameter.ParameterName = $"{prop.PropertyName}{t}";
                        parameter.Value = prop.Get(entities[t]);
                        command.Parameters.Add(parameter);
                    }
                }
            }

            //是否需要分页配参数
            if (pager != null)
            {
                parameter = command.CreateParameter();
                parameter.ParameterName = PublishMembers.PagerIndex;
                parameter.Value = pager.PagerIndex;
                command.Parameters.Add(parameter);

                parameter = command.CreateParameter();
                parameter.ParameterName = PublishMembers.PagerSize;
                parameter.Value = pager.PagerSize;
                command.Parameters.Add(parameter);
            }

        }

        /// <summary>
        /// Where参数化标记
        /// </summary>
        /// <param name="field"></param>
        /// <returns></returns>
        internal static String ToWherePropertyName(DbField field)
        {
            return $"{field.Field}_where";
        }

        /// <summary>
        /// Set参数化标记
        /// </summary>
        /// <param name="field"></param>
        /// <returns></returns>
        internal static String ToSetPropertyName(DbField field)
        {
            return $"{field.Field}_set";
        }

        /// <summary>
        /// 查询是否存在主键
        /// </summary>
        /// <typeparam name="T">实体对象</typeparam>
        /// <returns></returns>
        internal static String EntityKey<TEntity>()
        {
            //获取实体类型
            var type = typeof(TEntity);

            //获取所有属性
            var props = type.GetProperties();

            foreach (var prop in props)
            {
                //判断是否存在Key属性
                if (prop.CustomAttributes
                        .FirstOrDefault(x => x.AttributeType == typeof(KeyAttribute)) != null)
                    return prop.Name;
            }

            //不存在主键抛异常
            throw new Exception("No key was found");
        }

        /// <summary>
        /// 查询是否存在分页主键
        /// </summary>
        /// <typeparam name="T">实体对象</typeparam>
        /// <returns></returns>
        internal static String EntityPagerKey<TEntity>()
        {
            //获取实体类型
            var type = typeof(TEntity);

            //获取所有属性
            var props = type.GetProperties();

            StringBuilder builder = new StringBuilder();

            foreach (var prop in props)
            {
                //判断是否存在PagerKey属性
                if (prop.CustomAttributes
                        .FirstOrDefault(x => x.AttributeType == typeof(PagerKeyAttribute)) != null)
                    builder.Append($" , {prop.Name}");
            }
            if (builder.Length > 1)
            {
                builder.Remove(0, 2);
            }
            return builder.ToString();
        }

        /// <summary>
        /// 查询实体表名
        /// </summary>
        /// <typeparam name="TEntity">实体对象</typeparam>
        /// <returns></returns>
        internal static String EntityTable<TEntity>()
        {
            //获取实体类型
            var type = typeof(TEntity);

            //判断是否存在属性
            if (type.CustomAttributes.Count() > 0)
            {
                //判断是否存在Table属性
                var attr = type.CustomAttributes
                    .FirstOrDefault(x => x.AttributeType == typeof(TableAttribute));

                //存在Table属性就返回Table值
                if (attr.ConstructorArguments.Count > 0)
                    return attr.ConstructorArguments[0].Value.ToString();
            }
            return type.Name;
        }

        /// <summary>
        /// 查询实体所有字段名称
        /// </summary>
        /// <typeparam name="TEntity">实体对象</typeparam>
        /// <returns></returns>
        internal static String EntityField<TEntity>(Expression<Func<TEntity, dynamic>> Reveal)
        {
            String[] field = new String[0];

            if (Reveal != null)
            {
                //获取所有成员
                var members = ((NewExpression)Reveal.Body).Members;

                field = new String[members.Count];

                //遍历所有的成员
                for (int i = 0; i < field.Length; i++)
                {
                    field[i] = members[i].Name;
                }
            }
            else
            {
                //获取实体类型
                var type = typeof(TEntity);

                //获取所有属性
                var props = type.GetProperties();

                field = new String[props.Length];

                //添加所有的属性名称
                for (int i = 0; i < props.Length; i++)
                {
                    field[i] = props[i].Name;
                }
            }

            return String.Join(",", field);
        }

        /// <summary>
        /// 查询实体所有非主键字段名称
        /// </summary>
        /// <typeparam name="TEntity"></typeparam>
        /// <returns></returns>
        internal static String EntityFieldNoKey<TEntity>()
        {
            String[] field = new String[0];

            //获取实体类型
            var type = typeof(TEntity);

            //获取所有属性
            var props = type.GetProperties()
                    .Where(x => x.CustomAttributes
                    .FirstOrDefault(s => s.AttributeType == typeof(KeyAttribute)) is null)
                    .ToArray();

            field = new String[props.Length];

            //添加所有的属性名称
            for (int i = 0; i < props.Length; i++)
            {
                field[i] = props[i].Name;
            }

            return String.Join(",", field);
        }

        /// <summary>
        /// 生产参数化条件
        /// </summary>
        /// <param name="fields">需要查询的字段数组</param>
        /// <returns></returns>
        internal static String ParamWhere(IList<DbField> fields)
        {
            if (fields == null || fields.Count == 0) return "";

            StringBuilder builder = new StringBuilder();


            //获取没有Or分组的数据集
            var noOR = fields.Where(x => String.IsNullOrEmpty(x.OrGroup));

            //生成AND的SQL
            foreach (var field in noOR)
            {
                builder.Append($" AND {field.AnalysisFunc()} ");
            }

            if (builder.Length > 4)
            {
                builder.Remove(0, 4);
            }

            //获取Or的组
            var group = fields.GroupBy(x => x.OrGroup).Where(x => !String.IsNullOrEmpty(x.Key));

            //生成Or的组别SQL
            foreach (var field in group)
            {
                //获取单组数据集
                var single = fields.Where(x => x.OrGroup == field.Key);

                if (single.Count() > 1)
                {
                    //判断前面是否有条件
                    if (builder.Length > 0)
                    {
                        builder.Append(" AND ");
                    }

                    builder.Append(" ( ");

                    Int32 pos = builder.Length;

                    foreach (var item in single)
                    {
                        builder.Append($" OR {item.AnalysisFunc()} ");
                    }

                    if (builder.Length > 3)
                    {
                        builder.Remove(pos, 3);
                    }

                    builder.Append(" ) ");
                }
            }

            return builder.ToString();
        }

        /// <summary>
        /// 生成参数化赋值
        /// </summary>
        /// <param name="fields"></param>
        /// <returns></returns>
        internal static String ParamSet(IList<DbField> fields)
        {
            StringBuilder builder = new StringBuilder();

            if (fields == null || fields.Count == 0) return "";

            foreach (var field in fields)
            {
                builder.Append($", {field.Field}=@{field.Field}_set ");
            }

            builder.Remove(0, 1);

            return builder.ToString();
        }

        /// <summary>
        /// 生成插入数据集
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="Entities"></param>
        /// <returns></returns>
        internal static String InsertValues<T>(Int32 Count)
        {
            StringBuilder builder = new StringBuilder();

            List<EmitProperty> props = typeof(T).ToEmitProps().ToList();

            builder.Append(String.Join(" UNION ALL ", GetInsertSet(builder, props, Count)));

            return builder.ToString();
        }

        /// <summary>
        /// 拼接插入的数据集
        /// </summary>
        /// <param name="builder"></param>
        /// <param name="props"></param>
        /// <param name="Count"></param>
        /// <returns></returns>
        internal static IEnumerable<String> GetInsertSet(StringBuilder builder, IList<EmitProperty> props, Int32 Count)
        {
            int test = 0;
            for (int i = 0; i < Count; i++)
            {
                if (i != (Count - 1) && (props.Count * (i + 1)) >= 2100) break;
                yield return $" SELECT { String.Join(",", GetInsertField(props, i)) } ";
            }
        }

        /// <summary>
        /// 拼接非主键的参数集
        /// </summary>
        /// <param name="props"></param>
        /// <param name="index"></param>
        /// <returns></returns>
        internal static IEnumerable<String> GetInsertField(IList<EmitProperty> props, Int32 index)
        {
            foreach (var prop in props)
            {
                yield return $" @{prop.PropertyName}{index} AS {prop.PropertyName} ";
            }
        }

        /// <summary>
        /// 条件解析成SQL
        /// </summary>
        /// <param name="field">查询条件</param>
        /// <returns></returns>
        internal static String AnalysisFunc(this DbField field)
        {
            string compare;

            switch (field.DbFunc)
            {
                case DbFunc.Equal: compare = $" {field.Field} = @{field.Field}_where "; break;
                case DbFunc.Less: compare = $" {field.Field} < @{field.Field}_where "; break;
                case DbFunc.Greater: compare = $" {field.Field} > @{field.Field}_where "; break;
                case DbFunc.LessEqual: compare = $" {field.Field} <= @{field.Field}_where "; break;
                case DbFunc.GreaterEqual: compare = $" {field.Field} >= @{field.Field}_where "; break;
                case DbFunc.Like: compare = $" {field.Field} LIKE @{field.Field} "; break;
                case DbFunc.In: compare = $" CHARINDEX(','+ltrim(str({field.Field}))+',',','+@{field.Field}_where+',')>0 "; break;
                default: throw new Exception("分析判断类型错误！");
            }
            return compare;
        }

        /// <summary>
        /// 分组表达式转成SQL
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="OrderBy"></param>
        /// <returns></returns>
        internal static String EntityGroupBy<T>(Expression<Func<T, dynamic>> GroupBy)
        {
            //判断是否为空
            if (GroupBy == null) return "";

            StringBuilder builder = new StringBuilder();

            //多个字段排序
            if (GroupBy.Body is NewExpression)
            {
                var members = (GroupBy.Body as NewExpression).Members;

                //遍历所有的成员
                foreach (var member in members)
                {
                    builder.Append($" , {member.Name} ");
                }

                //判断长度是否大于1个
                if (builder.Length > 1)
                {
                    builder.Remove(0, 2);
                }
            }
            //单个字段排序
            else
            {
                if (GroupBy.Body is UnaryExpression)
                {
                    builder.Append($" {((MemberExpression)((UnaryExpression)GroupBy.Body).Operand).Member.Name} ");
                }
                else if (GroupBy.Body is MemberExpression)
                {
                    builder.Append($" {((MemberExpression)GroupBy.Body).Member.Name} ");
                }
                else if (GroupBy.Body is ParameterExpression)
                {
                    builder.Append($" {((ParameterExpression)GroupBy.Body).Type.Name} ");
                }
            }

            if (builder.Length > 0)
            {
                builder.Insert(0, " GROUP BY ");
            }

            return builder.ToString();
        }

        /// <summary>
        /// 排序表达式转成SQL
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="OrderBy"></param>
        /// <returns></returns>
        internal static String EntityOrderBy<T>(Expression<Func<T, dynamic>> OrderBy)
        {
            //判断是否为空
            if (OrderBy == null) return "";

            StringBuilder builder = new StringBuilder();

            //多个字段排序
            if (OrderBy.Body is NewExpression)
            {
                var members = ((NewExpression)OrderBy.Body).Members;

                //遍历所有的成员
                foreach (var member in members)
                {
                    builder.Append($" , {member.Name} ");
                }

                //判断长度是否大于1个
                if (builder.Length > 1)
                {
                    builder.Remove(0, 2);
                }
            }
            //单个字段排序
            else
            {
                if (OrderBy.Body is UnaryExpression)
                {
                    builder.Append($" {((MemberExpression)((UnaryExpression)OrderBy.Body).Operand).Member.Name} ");
                }
                else if (OrderBy.Body is MemberExpression)
                {
                    builder.Append($" {((MemberExpression)OrderBy.Body).Member.Name} ");
                }
                else if (OrderBy.Body is ParameterExpression)
                {
                    builder.Append($" {((ParameterExpression)OrderBy.Body).Type.Name} ");
                }
            }

            if (builder.Length > 0)
            {
                builder.Insert(0, " ORDER BY ");
            }

            return builder.ToString();
        }

        /// <summary>
        /// Db缓存
        /// </summary>
        internal static ConcurrentDictionary<String, Delegate> DbReadCache = new ConcurrentDictionary<String, Delegate>();

        /// <summary>
        /// 执行SQL读取数据集
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="con">数据库连接</param>
        /// <param name="sqlStr">需要执行的SQL</param>
        /// <param name="where">查询条件</param>
        /// <param name="tran">事务对象</param>
        /// <returns></returns>
        internal static DbSlice<IEnumerable<T>> SqlRead<T>(this DbNakedContext con, string sqlStr, Expression<Func<T, dynamic>> Reveal, IList<DbField> where, DbPager pager) where T : class, new()
        {
            var slice = new DbSlice<IEnumerable<T>>();
            //开辟数据集容器
            IEnumerable<T> DataList = null;

            //开辟emit容器集合
            List<EmitProperty> emits = new List<EmitProperty>();

            if (Reveal is null)
            {
                foreach (var prop in typeof(T).GetProperties())
                {
                    emits.Add(new EmitProperty(prop));
                }
            }
            else
            {
                foreach (var prop in ((NewExpression)Reveal.Body).Arguments)
                {
                    emits.Add(new EmitProperty((PropertyInfo)((MemberExpression)prop).Member));
                }
            }

            Stopwatch runTime = new Stopwatch();
            runTime.Start();
            try
            {
                //数据库连接
                using (var dbRead = con.Read(sqlStr, where, pager))
                {
                    if (DbReadCache.TryGetValue(typeof(T).FullName, out var @delegate))
                    {
                        DataList = ((Func<IDataReader, DbPager, List<T>>)@delegate).Invoke(dbRead, pager);
                    }
                    else
                    {
                        @delegate = EmitDbRead<T>.DbRead(emits, pager);
                        DbReadCache.TryAdd(typeof(T).FullName, @delegate);
                        DataList = ((Func<IDataReader, DbPager, List<T>>)@delegate).Invoke(dbRead, pager);
                    }
                    slice.Succeed = true;
                }
            }
            catch (System.Exception ex)
            {
                slice.Succeed = false;
                slice.Message = ex.Message;
                slice.Data = DataList;
                throw ex;
            }
            finally
            {
                runTime.Stop();
                slice.ExecuteTime = runTime.Elapsed;
                slice.ExecuteSql = sqlStr;
                slice.Data = DataList;
            }
            return slice;
        }

        /// <summary>
        /// 类型转快速属性
        /// </summary>
        /// <param name="props"></param>
        /// <returns></returns>
        internal static IEnumerable<EmitProperty> ToEmitProps(this Type type)
        {
            foreach (var prop in type.GetProperties()
                .Where(x => x.CustomAttributes
                .FirstOrDefault(d => d.AttributeType == typeof(KeyAttribute)) is null))
            {
                yield return new EmitProperty(prop);
            }
        }
    }
}
