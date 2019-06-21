using System;
using System.Collections.Generic;
using System.Text;
using System.Linq;
using System.Data;
using System.Data.Common;
using System.Collections.Concurrent;

namespace Utilities.Data
{
    public class SqlHelper
    {
        private DbConnection _conn;

        private ISqlClientProvider _sqlClientProvider;

        private DbTransaction _transaction;

        //public DbTransaction Transaction
        //{
        //    get
        //    {
        //        return _transaction;
        //    }
        //}

        //public DbConnection Connection
        //{
        //    get
        //    {
        //        return _conn;
        //    }
        //}

        public bool TranscationIsEnabled
        {
            get
            {
                return _transaction != null;
            }
        }

        public SqlHelper(ISqlClientProvider sqlClientProvider)
        {
            _sqlClientProvider = sqlClientProvider;
            _conn = sqlClientProvider.CreateConnection();
            _conn.StateChange += ConnectionStateChanged;
        }

        #region transcation
        private void ConnectionStateChanged(object sender, StateChangeEventArgs e)
        {
            if(e.CurrentState == ConnectionState.Closed || e.CurrentState == ConnectionState.Broken)
            {
                _transaction = null;
            }
        }

        private void ThrowIfTransactionIsNotBegan()
        {
            if(_transaction == null)
            {
                throw new InvalidOperationException("尚未开始事务");
            }
        }

        public void BeginTransaction(IsolationLevel? isolationLevel = null)
        {
            if(TranscationIsEnabled)
            {
                throw new InvalidOperationException("开始新的事务前必须停止已有的事务");
            }
            _conn.OpenIfClosed();
            if(isolationLevel == null)
            {
                _transaction = _conn.BeginTransaction();
            }
            else
            {
                _transaction = _conn.BeginTransaction(isolationLevel.Value);
            }
        }

        public void CommitTransaction()
        {
            ThrowIfTransactionIsNotBegan();
            try
            {
                _transaction.Commit();
            }
            catch
            {
                _transaction = null;
                throw;
            }
           
        }

        public void RollbackTransaction()
        {
            ThrowIfTransactionIsNotBegan();
            try
            {
                _transaction.Rollback();
            }
            catch
            {
                _transaction = null;
                throw;
            }
          
        }
        #endregion

        #region select
        public void Select(string selectCommandText, CommandType commandType, DataTable dt)
        {
            SqlUtils.Select(selectCommandText, commandType, dt, _conn, _sqlClientProvider);
        }

        public void Select(string selectSql, DataTable dt)
        {
            Select(selectSql, CommandType.Text, dt);
        }

        public DataTable Select(string selectCommandText, CommandType commandType)
        {
            var dt = new DataTable();
            Select(selectCommandText, commandType, dt);
            return dt;
        }

        public DataTable Select(string selectSql)
        {
            return Select(selectSql, CommandType.Text);
        }
        #endregion

        #region ExecuteNonQuery
        public int ExecuteNonQuery(string commandText, CommandType commandType)
        {
            if (TranscationIsEnabled)
            {
                return SqlUtils.ExecuteNonQuery(commandText, commandType, _transaction, _sqlClientProvider);
            }
            else
            {
                return SqlUtils.ExecuteNonQuery(commandText, commandType, _conn, _sqlClientProvider);
            }
        }

        public int ExecuteNonQuery(string commandText)
        {
            return SqlUtils.ExecuteNonQuery(commandText, CommandType.Text);
        }
        #endregion
    }
}
