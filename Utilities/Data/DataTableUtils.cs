using System;
using System.Collections.Generic;
using System.Text;
using System.Linq;
using System.Data;

namespace Utilities.Data
{
    public static class DataTableUtils
    {
        /// <summary>
        /// 获取一个值，改值指示指定的DataTable是不是NULL或者行数为空
        /// </summary>
        /// <param name="dt"></param>
        /// <returns></returns>
        public static bool IsNullOrEmpty(this DataTable dt)
        {
            return dt == null || dt.Rows.Count == 0;
        }

        /// <summary>
        /// 将一个源DataTable的行复制到目标DataTable
        /// </summary>
        /// <param name="targetDt"></param>
        /// <param name="sourceDt"></param>
        public static void ImportTable(this DataTable targetDt, DataTable sourceDt)
        {
            if(targetDt == null)
            {
                throw new ArgumentNullException(nameof(targetDt));
            }
            if (!sourceDt.IsNullOrEmpty())
            {
                foreach (DataRow dr in sourceDt.Rows)
                {
                    targetDt.ImportRow(dr);
                }
            }
        }

        /// <summary>
        /// 获取DataRow指定列的值在Sql语句中表示形式，一般非数值类型会加单引号
        /// </summary>
        /// <param name="dr">DataRow</param>
        /// <param name="dc"></param>
        /// <returns></returns>
        public static string SqlValue(this DataRow dr, DataColumn dc)
        {
            var objVal = dr[dc];
            return objVal?.ToString();
        }

        /// <summary>
        /// <seealso cref="SqlValue(DataRow, DataColumn)"/>
        /// </summary>
        /// <param name="dr"><seealso cref="SqlValue(DataRow, DataColumn)"/></param>
        /// <param name="colIndex"></param>
        /// <returns></returns>
        public static string SqlValue(this DataRow dr, int colIndex)
        {
            return dr.SqlValue(dr.Table.Columns[colIndex]);
        }

        /// <summary>
        /// <see cref="SqlValue(DataRow, DataColumn)"/>
        /// </summary>
        /// <param name="dr"><see cref="SqlValue(DataRow, DataColumn)"/></param>
        /// <param name="colName"></param>
        /// <returns></returns>
        public static string SqlValue(this DataRow dr, string colName)
        {
            return dr.SqlValue(dr.Table.Columns[colName]);
        }

        /// <summary>
        /// 获取DataRow指定列的值转换为指定DbType类型的值，该值可用作于DBParameter的Value
        /// </summary>
        /// <param name="dr"></param>
        /// <param name="dc"></param>
        /// <param name="dbType"></param>
        /// <returns></returns>
        public static object SqlValue(this DataRow dr, DataColumn dc, DbType dbType)
        {
            return dr[dc].ToDbType(dbType);
        }

        /// <summary>
        /// <see cref="SqlValue(DataRow, DataColumn, DbType)"/>
        /// </summary>
        /// <param name="dr"></param>
        /// <param name="colIndex"></param>
        /// <param name="dbType"></param>
        /// <returns></returns>
        public static object SqlValue(this DataRow dr, int colIndex, DbType dbType)
        {
            return SqlValue(dr, dr.Table.Columns[colIndex], dbType);
        }

        /// <summary>
        /// <see cref="SqlValue(DataRow, DataColumn, DbType)"/>
        /// </summary>
        /// <param name="dr"></param>
        /// <param name="colName"></param>
        /// <param name="dbType"></param>
        /// <returns></returns>
        public static object SqlValue(this DataRow dr, string colName, DbType dbType)
        {
            return SqlValue(dr, dr.Table.Columns[colName], dbType);
        }

        /// <summary>
        /// 获取指定DataRow的指定列的值，并将值转换为指定的类型
        /// </summary>
        /// <typeparam name="T">将值转换到的类型</typeparam>
        /// <param name="dr"></param>
        /// <param name="dc"></param>
        /// <returns></returns>
        public static T Value<T>(this DataRow dr, DataColumn dc)
        {
            var objVal = dr[dc];
            return (T)Convert.ChangeType(objVal, typeof(T));
        }
        
        /// <summary>
        /// <see cref="Value{T}(DataRow, DataColumn)"/>
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="dr"></param>
        /// <param name="colIndex"></param>
        /// <returns></returns>
        public static T Value<T>(this DataRow dr, int colIndex)
        {
            return dr.Value<T>(dr.Table.Columns[colIndex]);
        }

        /// <summary>
        /// <see cref="Value{T}(DataRow, DataColumn)"/>
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="dr"></param>
        /// <param name="colName"></param>
        /// <returns></returns>
        public static T Value<T>(DataRow dr, string colName)
        {
            return dr.Value<T>(dr.Table.Columns[colName]);
        }

        /// <summary>
        /// 获取指定DataRow的指定列的值，并将值转换为指定的类型，如果转换失败则返回指定的默认值
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="dr"></param>
        /// <param name="dc"></param>
        /// <param name="defaultValue"></param>
        /// <returns></returns>
        public static T Value<T>(this DataRow dr, DataColumn dc, T defaultValue)
        {
            try
            {
                var objVal = dr[dc];
                return (T)Convert.ChangeType(objVal, typeof(T));
            }
            catch
            {
                return defaultValue;
            }
           
        }

        /// <summary>
        /// <see cref="Value{T}(DataRow, DataColumn, T)"/>
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="dr"></param>
        /// <param name="colIndex"></param>
        /// <param name="defaultValue"></param>
        /// <returns></returns>
        public static T Value<T>(this DataRow dr, int colIndex, T defaultValue)
        {
            return dr.Value<T>(dr.Table.Columns[colIndex], defaultValue);
        }

        /// <summary>
        /// <see cref="Value{T}(DataRow, DataColumn, T)"/>
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="dr"></param>
        /// <param name="colName"></param>
        /// <param name="defaultValue"></param>
        /// <returns></returns>
        public static T Value<T>(DataRow dr, string colName, T defaultValue)
        {
            return dr.Value<T>(dr.Table.Columns[colName], defaultValue);
        }

        /// <summary>
        /// 按指定条件筛选指定的DataTable的所有行
        /// </summary>
        /// <param name="dt"></param>
        /// <param name="predicate"></param>
        /// <returns></returns>
        public static IEnumerable<DataRow> Where(this DataTable dt, Predicate<DataRow> predicate)
        {
            foreach (DataRow dr in dt.Rows)
            {
                if (predicate(dr))
                {
                    yield return dr;
                }
            }
            
        }
    }
}
