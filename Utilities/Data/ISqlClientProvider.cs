using System;
using System.Collections.Generic;
using System.Text;
using System.Data;
using System.Data.Common;

namespace Utilities.Data
{
    public interface ISqlClientProvider 
    {
        DbConnection CreateConnection();

        DbCommand CreateCommand(string sql, CommandType commandType);

        DbCommand CreateCommand(string sql, CommandType commandType, DbConnection dbConnection);

        DbDataAdapter CreateDataAdapter();

        DbDataAdapter CreateDataAdapter(string selectCommandText, DbConnection dbConnection);

        DbDataAdapter CreateDataAdapter(DbCommand selectCommand);
    }
}
