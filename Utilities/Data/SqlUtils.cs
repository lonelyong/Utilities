using System;
using System.Collections.Generic;
using System.Text;
using System.Collections.Concurrent;
using System.Data;
using System.Data.Common;
using System.Linq;

namespace Utilities.Data
{
    public static class SqlUtils
    {
        private static ISqlClientProvider _defaultSqlClientProvider;

        private static object _defaultSqlClientProviderLock = new object();

        private const string _INSERT_SQL_TEXT = "INSERT INTO {0} ({1}) VALUES({2});";
        
        public static ISqlClientProvider DefaultSqlClientProvider {
            get
            {
                return _defaultSqlClientProvider;
            }
            set
            {
                lock (_defaultSqlClientProviderLock)
                {
                    _defaultSqlClientProvider = value;
                }
            }
        }

        #region common


        private static void ThrowIfTransactionIsInvalid(DbTransaction dbTransaction)
        {
            if(dbTransaction.Connection == null)
            {
                throw new InvalidOperationException("事务已失效");
            }
        }

        private static void ThrowIfDefaultSqlClientProviderIsNotSet()
        {
            if(_defaultSqlClientProvider is null)
            {
                throw new InvalidOperationException($"未设置{nameof(DefaultSqlClientProvider)}");
            }
        }

        private static (string Fields, string Values) BuildFieldsAndValues(Dictionary<string, object> colAndVals)
        {
            var fields = string.Join(",", colAndVals.Keys);
            var values = string.Join(",", colAndVals.Keys.Select(k => "@" + k));
            return (fields, values);
        }
        #endregion

        #region Setup
        
        #endregion

        #region Select
        public static void Select(string selectCommandText, CommandType commandType, DataTable dt, DbConnection dbConnection, ISqlClientProvider sqlClientProvider)
        {
            dbConnection.OpenIfClosed();
            using (var cmd = sqlClientProvider.CreateCommand(selectCommandText, commandType, dbConnection))
            {
                using (var sda = sqlClientProvider.CreateDataAdapter(cmd))
                {
                    sda.Fill(dt);
                }
            }
        }

        public static void Select(string selectCommandText, CommandType commandType, DataTable dt, DbConnection dbConnection)
        {
            Select(selectCommandText, commandType, dt, dbConnection, DefaultSqlClientProvider);
        }

        public static void Select(string selectCommandText, CommandType commandType, DataTable dt)
        {
            using (var conn = DefaultSqlClientProvider.CreateConnection())
            {
                Select(selectCommandText, commandType, dt, conn);
            }
        }

        public static void Select(string selectSql, DataTable dt)
        {
            Select(selectSql, CommandType.Text, dt);
        }

        public static DataTable Select(string selectCommandText, CommandType commandType, DbConnection dbConnection, ISqlClientProvider sqlClientProvider)
        {
            var dt = new DataTable();
            Select(selectCommandText, commandType, dt, dbConnection, sqlClientProvider);
            return dt;
        }

        public static DataTable Select(string selectCommandText, CommandType commandType, DbConnection dbConnection)
        {
            var dt = new DataTable();
            Select(selectCommandText, commandType, dt, dbConnection, DefaultSqlClientProvider);
            return dt;
        }

        public static DataTable Select(string selectCommandText, CommandType commandType)
        {
            var dt = new DataTable();
            Select(selectCommandText, commandType, dt);
            return dt;
        }

        public static DataTable Select(string selectSql)
        {
            var dt = new DataTable();
            Select(selectSql, dt);
            return dt;
        }
        #endregion

        #region ExecuteNonQuery
        public static int ExecuteNonQuery(string commandText, CommandType commandType, DbTransaction dbTransaction, ISqlClientProvider sqlClientProvider)
        {
            ThrowIfTransactionIsInvalid(dbTransaction);
            using (var cmd = sqlClientProvider.CreateCommand(commandText, commandType, dbTransaction.Connection))
            {
                cmd.Transaction = dbTransaction;
                return cmd.ExecuteNonQuery();
            }
        }

        public static int ExecuteNonQuery(string commandText, CommandType commandType, DbTransaction dbTransaction)
        {
            return ExecuteNonQuery(commandText, commandType, dbTransaction, DefaultSqlClientProvider);
        }

        public static int ExecuteNonQuery(string commandText, DbTransaction dbTransaction)
        {
            return ExecuteNonQuery(commandText, CommandType.Text, dbTransaction);
        }

        public static int ExecuteNonQuery(string commandText, CommandType commandType, DbConnection dbConnection, ISqlClientProvider sqlClientProvider)
        {
            using (var cmd = sqlClientProvider.CreateCommand(commandText, commandType, dbConnection))
            {
                return cmd.ExecuteNonQuery();
            }
        }

        public static int ExecuteNonQuery(string commandText, CommandType commandType, DbConnection dbConnection)
        {
            return ExecuteNonQuery(commandText, commandType, dbConnection, DefaultSqlClientProvider);
        }

        public static int ExecuteNonQuery(string commandText, CommandType commandType)
        {
            using (var conn = DefaultSqlClientProvider.CreateConnection())
            {
                return ExecuteNonQuery(commandText, commandType, conn);
            }  
        }

        public static int ExecuteNonQuery(string commandText)
        {
            return ExecuteNonQuery(commandText, CommandType.Text);
        }
        #endregion

        #region ExecuteScalar
        public static T ExecuteScalar<T>(string commandText, CommandType commandType, DbTransaction dbTransaction, ISqlClientProvider sqlClientProvider)
        {
            ThrowIfTransactionIsInvalid(dbTransaction);
            using (var cmd = sqlClientProvider.CreateCommand(commandText, commandType, dbTransaction.Connection))
            {
                cmd.Transaction = dbTransaction;
                var objValue = cmd.ExecuteScalar();
                if (objValue == null)
                {
                    if (typeof(T).IsValueType)
                    {
                        throw new Exception("查询结过为空");
                    }
                }
                if (typeof(T) == objValue.GetType())
                {
                    return (T)objValue;
                }
                return (T)Convert.ChangeType(objValue, typeof(T));
            }
        }

        public static T ExecuteScalar<T>(string commandText, CommandType commandType, DbTransaction dbTransaction)
        {
            return ExecuteScalar<T>(commandText, commandType, dbTransaction, DefaultSqlClientProvider);
        }

        public static T ExecuteScalar<T>(string commandText, DbTransaction dbTransaction)
        {
            using (var conn = DefaultSqlClientProvider.CreateConnection())
            {
                return ExecuteScalar<T>(commandText, CommandType.Text, dbTransaction);
            }
        }

        public static T ExecuteScalar<T>(string commandText, CommandType commandType, DbConnection dbConnection, ISqlClientProvider sqlClientProvider)
        {
            using (var cmd = sqlClientProvider.CreateCommand(commandText, commandType, dbConnection))
            {
                var objValue = cmd.ExecuteScalar();
                if(objValue == null)
                {
                    if (typeof(T).IsValueType)
                    {
                        throw new Exception("查询结过为空");
                    }
                }
                if(typeof(T) == objValue.GetType())
                {
                    return (T)objValue;
                }
                return (T)Convert.ChangeType(objValue, typeof(T));
            }
        }

        public static T ExecuteScalar<T>(string commandText, CommandType commandType, DbConnection dbConnection)
        {
            return ExecuteScalar<T>(commandText, commandType, dbConnection, DefaultSqlClientProvider);
        }

        public static T ExecuteScalar<T>(string commandText, CommandType commandType)
        {
            using (var conn = DefaultSqlClientProvider.CreateConnection())
            {
                return ExecuteScalar<T>(commandText, commandType, conn);
            }
        }

        public static T ExecuteScalar<T>(string commandText)
        {
            return ExecuteScalar<T>(commandText, CommandType.Text);
        }
        #endregion

        #region Insert
        public static int Insert(string tableName, Dictionary<string, object> colAndVals, DbTransaction dbTransaction, ISqlClientProvider sqlClientProvider)
        {
            ThrowIfTransactionIsInvalid(dbTransaction);
            var colVal = BuildFieldsAndValues(colAndVals);
            var sql = string.Format(_INSERT_SQL_TEXT, tableName, colVal.Fields, colVal.Values);
            using (var cmd = sqlClientProvider.CreateCommand(sql, CommandType.Text, dbTransaction.Connection))
            {
                foreach (var kv in colAndVals)
                {
                    var para = cmd.CreateParameter();
                    para.Direction = ParameterDirection.Input;
                    para.ParameterName = '@' + kv.Key;
                    para.Value = kv.Value;
                    para.DbType = DbType.AnsiString;
                    cmd.Parameters.Add(para);
                }
                return cmd.ExecuteNonQuery();
            }
            
        }
        #endregion
    }
}
