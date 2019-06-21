using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Common;
using System.Text;

namespace Utilities.Data
{
    public static class SqlClientExtensions
    {
        public static void OpenIfClosed(this DbConnection dbConnection)
        {
            if (dbConnection.State == ConnectionState.Closed)
            {
                dbConnection.Open();
            }
        }
    }
}
