using System;
using System.Collections.Generic;
using System.Text;
using System.Data;

namespace Utilities.Data
{
    static class SqlDataConvert
    {
        public static object ToDbType(this object val, DbType dbType)
        {
            switch (dbType)
            {
                case DbType.AnsiString:
                    break;
                case DbType.AnsiStringFixedLength:
                    break;
                case DbType.Binary:
                    break;
                case DbType.Boolean:
                    break;
                case DbType.Byte:
                    break;
                case DbType.Currency:
                    break;
                case DbType.Date:
                    break;
                case DbType.DateTime:
                    break;
                case DbType.DateTime2:
                    break;
                case DbType.DateTimeOffset:
                    break;
                case DbType.Decimal:
                    break;
                case DbType.Double:
                    break;
                case DbType.Guid:
                    break;
                case DbType.Int16:
                    break;
                case DbType.Int32:
                    break;
                case DbType.Int64:
                    break;
                case DbType.Object:
                    break;
                case DbType.SByte:
                    break;
                case DbType.Single:
                    break;
                case DbType.String:
                    break;
                case DbType.StringFixedLength:
                    break;
                case DbType.Time:
                    break;
                case DbType.UInt16:
                    break;
                case DbType.UInt32:
                    break;
                case DbType.UInt64:
                    break;
                case DbType.VarNumeric:
                    break;
                case DbType.Xml:
                    break;
                default:
                    break;
            }
            return val;
        }
    }
}
