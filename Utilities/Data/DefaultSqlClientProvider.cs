using System;
using System.Collections.Generic;
using System.Text;
using System.Data;
using System.Data.Common;
namespace Utilities.Data
{
    public sealed class DefaultSqlClientProvider : ISqlClientProvider
    {
        private readonly Func<DbConnection> _connectionProvider;

        private readonly Func<string, CommandType, DbCommand> _commandProvider;

        private readonly Func<DbDataAdapter> _dataAdapterProvider;

        /// <summary>
        /// 使用连接字符串实例化新的Provider
        /// </summary>
        /// <param name="connectionStringProvider"></param>
        /// <param name="connectionProvider"></param>
        /// <param name="commandProvider"></param>
        /// <param name="dataAdapterProvider"></param>
        public DefaultSqlClientProvider(
            Func<DbConnection> connectionProvider,
            Func<string, CommandType, DbCommand> commandProvider,
            Func<DbDataAdapter> dataAdapterProvider)
        {
            _connectionProvider = connectionProvider;
            _commandProvider = commandProvider;
            _dataAdapterProvider = dataAdapterProvider;
        }

        public DbCommand CreateCommand(string commandText, CommandType commandType)
        {
            return _commandProvider.Invoke(commandText, commandType);
        }

        public DbCommand CreateCommand(string sql, CommandType commandType, DbConnection dbConnection)
        {
            var cmd = _commandProvider.Invoke(sql, commandType);
            cmd.Connection = dbConnection;
            return cmd;
        }

        public DbConnection CreateConnection()
        {
            return CreateConnection();
        }

        public DbDataAdapter CreateDataAdapter()
        {
            return _dataAdapterProvider.Invoke();
        }

        public DbDataAdapter CreateDataAdapter(string selectCommandText, DbConnection dbConnection)
        {
            var sdp = CreateDataAdapter();
            sdp.SelectCommand = CreateCommand(selectCommandText, CommandType.Text, dbConnection);
            return sdp;
        }

        public DbDataAdapter CreateDataAdapter(DbCommand selectCommand)
        {
            var sda = CreateDataAdapter();
            sda.SelectCommand = selectCommand;
            return sda;
        }
    }
}
