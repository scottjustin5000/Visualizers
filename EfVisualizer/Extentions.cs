using System;
using System.Collections;
using System.Collections.Generic;
using System.Data.Common;
using System.Data.Entity;
using System.Data.Entity.Infrastructure;
using System.Data.EntityClient;
using System.Data.Objects;
using System.Data.SqlClient;
using System.Linq;
using System.Reflection;
using System.Text;

namespace EfVisualizer
{
    public static class Extensions
    {
        internal static QueryData ToSqlString(this IQueryable queryable)
        {


            var iqProp = queryable.GetType().GetProperty("InternalQuery", BindingFlags.Instance | BindingFlags.NonPublic | BindingFlags.Public);

            var iq = iqProp.GetValue(queryable, null);

            var oqProp = iq.GetType().GetProperty("ObjectQuery", BindingFlags.Instance | BindingFlags.NonPublic | BindingFlags.Public);

            var oq = oqProp.GetValue(iq, null);

            var objectQuery = oq as ObjectQuery;

            return objectQuery.ToSqlString();
        }
        internal static QueryData ToSqlString(this DbContext dbContext)
        {
            var sqlOptions = new QueryData();
            var objectContext = ((IObjectContextAdapter)dbContext).ObjectContext;
            sqlOptions.ConnectionString = (objectContext.Connection as EntityConnection).StoreConnection.ConnectionString;
            sqlOptions.SQLCommand = GetSQLCommands(objectContext);
            return sqlOptions;
        }
        internal static string GetSQLCommands(ObjectContext context)
        {
            bool next;
            var stringBuilder = new StringBuilder();
            IEnumerator<DbCommand> enumerator = GetContextCommands(context).GetEnumerator();
            try
            {
                while (true)
                {
                    next = enumerator.MoveNext();
                    if (!next)
                    {
                        break;
                    }
                    DbCommand current = enumerator.Current;

                    IEnumerator enumerator1 = current.Parameters.GetEnumerator();
                    try
                    {
                        while (true)
                        {
                            next = enumerator1.MoveNext();
                            if (!next)
                            {
                                break;
                            }
                            DbParameter dbParameter = (DbParameter)enumerator1.Current;
                            SqlParameter sqlParameter = new SqlParameter(dbParameter.DbType.ToString(), dbParameter.Value);
                            stringBuilder.AppendLine(string.Format("DECLARE {0} {1};", dbParameter.ParameterName, sqlParameter.SqlDbType));
                            stringBuilder.AppendLine(string.Format("SET {0} = '{1}';", dbParameter.ParameterName, dbParameter.Value));
                        }
                    }
                    finally
                    {
                        IDisposable disposable = enumerator1 as IDisposable;
                        bool isDisposable = disposable == null;
                        if (!isDisposable)
                        {
                            disposable.Dispose();
                        }
                    }
                    stringBuilder.AppendLine();
                    stringBuilder.Append(current.CommandText);
                    stringBuilder.AppendFormat("{0}GO{0}{0}", Environment.NewLine);
                }
            }
            finally
            {
                enumerator.Dispose();
            }
            string str = stringBuilder.ToString();
            return str;
        }
        public static QueryData ToSqlString(this ObjectQuery objectQuery)
        {
            QueryData sqlOptions = new QueryData();

            string sql = objectQuery.ToTraceString();

            var sb = new StringBuilder();
            if (objectQuery.Context != null && objectQuery.Context.Connection != null && objectQuery.Context.Connection is EntityConnection)
                sqlOptions.ConnectionString = (objectQuery.Context.Connection as EntityConnection).StoreConnection.ConnectionString;

            foreach (ObjectParameter parameter in objectQuery.Parameters)
            {
                SqlParameter r = new SqlParameter(parameter.ParameterType.FullName, parameter.Value);

                sb.AppendLine(string.Format("DECLARE @{0} {1};", parameter.Name, r.SqlDbType));
                sb.AppendLine(string.Format("SET @{0} = '{1}';", parameter.Name, parameter.Value));
            }

            sb.AppendLine();
            sb.AppendLine(sql);

            sqlOptions.SQLCommand = sb.ToString();

            return sqlOptions;
        }

        public static QueryData ToSqlString(this ObjectContext objectContext)
        {
            QueryData sQLQueryOption = new QueryData();
            sQLQueryOption.ConnectionString = (objectContext.Connection as EntityConnection).StoreConnection.ConnectionString;
            sQLQueryOption.SQLCommand = GetSQLCommands(objectContext);

            return sQLQueryOption;
        }
        private static IEnumerable<DbCommand> GetContextCommands(ObjectContext context)
        {
            Assembly assembly = Assembly.Load("System.Data.Entity, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089");
            Type type = assembly.GetType("System.Data.Mapping.Update.Internal.UpdateTranslator");
            Type type1 = assembly.GetType("System.Data.Mapping.Update.Internal.FunctionUpdateCommand");
            Type type2 = assembly.GetType("System.Data.Mapping.Update.Internal.DynamicUpdateCommand");
            object[] objectStateManager = new object[4];
            objectStateManager[0] = context.ObjectStateManager;
            objectStateManager[1] = ((EntityConnection)context.Connection).GetMetadataWorkspace();
            objectStateManager[2] = context.Connection;
            objectStateManager[3] = context.CommandTimeout;
            object[] objArray = objectStateManager;
            object obj = Activator.CreateInstance(type, BindingFlags.Instance | BindingFlags.NonPublic, null, objArray, null);
            MethodInfo method = type.GetMethod("ProduceCommands", BindingFlags.Instance | BindingFlags.NonPublic);
            IEnumerable enumerable = method.Invoke(obj, null) as IEnumerable;
            IEnumerator enumerator = enumerable.GetEnumerator();
            try
            {
                while (true)
                {
                    bool next = enumerator.MoveNext();
                    if (!next)
                    {
                        break;
                    }
                    object current = enumerator.Current;
                    bool isInstance = !type1.IsInstanceOfType(current);
                    if (isInstance)
                    {
                        isInstance = !type2.IsInstanceOfType(current);
                        if (!isInstance)
                        {
                            MethodInfo methodInfo = type2.GetMethod("CreateCommand", BindingFlags.Instance | BindingFlags.NonPublic);
                            objectStateManager = new object[2];
                            objectStateManager[0] = obj;
                            objectStateManager[1] = new Dictionary<int, object>();
                            object[] objArray1 = objectStateManager;
                            yield return methodInfo.Invoke(current, objArray1) as DbCommand;
                        }
                    }
                    else
                    {
                        FieldInfo field = type1.GetField("m_dbCommand", BindingFlags.Instance | BindingFlags.NonPublic);
                        yield return field.GetValue(current) as DbCommand;
                    }
                }
            }
            finally
            {
                IDisposable disposable = enumerator as IDisposable;
                bool isDisposable = disposable == null;
                if (!isDisposable)
                {
                    disposable.Dispose();
                }
            }
        }

    }
}
