using System;
using System.Collections.Generic;
using System.Data;
using System.Text;

namespace Utilities.Data
{
    public sealed class DbColumn
    {
        public string ColumnName { get; set; }

        public DbType DbType { get; set; }
    }
}
