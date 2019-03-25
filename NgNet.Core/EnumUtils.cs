using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Reflection;
using System.ComponentModel;
using System.Data;

namespace NgNet
{
    /// <summary>
    /// 枚举帮助类
    /// </summary>
    public static class EnumUtils
    {
        #region private fields
        public const string VALUE = "Value";

        public const string DESCRIPTION = "Description";

        public const string SPLITER = "， ";

        #endregion

        #region private methods

        #endregion

        #region public methods
        /// <summary>
        /// 获取指定枚举项的Description
        /// </summary>
        /// <param name="enum">枚举项</param>
        /// <returns>返回DescriptionAttribute的描述</returns>
        public static string GetEnumDescription(this Enum @enum)
        {
            var enumType = @enum.GetType();
            var fi = enumType.GetField(Enum.GetName(enumType, @enum));
            var descAttr = fi.GetCustomAttribute(typeof(DescriptionAttribute)) as DescriptionAttribute;
            if (descAttr == null)
                return @enum.ToString();
            else return descAttr.Description;
        }
        /// <summary>
        /// 获取指定枚举项组合的Description
        /// </summary>
        /// <param name="enum">枚举项</param>
        /// <returns>返回DescriptionAttribute的描述</returns>
        public static string GetEnumDescriptions(this Enum @enum)
        {
            var enumType = @enum.GetType();
            var first = true;
            var descrption = string.Empty;
            var int64 = Convert.ToInt64(@enum);
            if (int64 == 0)
                return GetEnumDescription(@enum);
            foreach (var item1 in Enum.GetValues(enumType))
            {
                if ((int64 & Convert.ToInt64(Convert.ChangeType(item1, Enum.GetUnderlyingType(enumType)))) != 0)
                {
                    if (first)
                        first = false;
                    else
                        descrption += SPLITER;
                    descrption += GetEnumDescription((Enum)Enum.ToObject(enumType, item1));
                }
            }
            return descrption;
        }
        /// <summary>
        /// 获取指定枚举类型的项与项描述的表
        /// </summary>
        /// <param name="enumType"></param>
        /// <returns></returns>
        public static DataTable GetEnumDescriptions(Type enumType)
        {
            var dt = new DataTable();
            DataRow dr;
            dt.Columns.Add(VALUE, enumType);
            dt.Columns.Add(DESCRIPTION, typeof(string));

            foreach (Enum item in Enum.GetValues(enumType))
            {
                dr = dt.NewRow();
                dr[VALUE] = item;
                dr[DESCRIPTION] = GetEnumDescription(item);
                dt.Rows.Add(dr);
            }
            return dt;
        }
        /// <summary>
        /// 获取指定枚举项集合的描述
        /// </summary>
        /// <typeparam name="TEnum">枚举类型</typeparam>
        /// <param name="enumArray">枚举项集合</param>
        /// <returns></returns>
        public static DataTable GetEnumArrayDescriptions<TEnum>(this IEnumerable<TEnum> enumArray) where TEnum : new()
        {
            var enumType = typeof(TEnum);
            var flagAttr = enumType.GetCustomAttribute(typeof(FlagsAttribute)) as FlagsAttribute;
            if (flagAttr == null)
                throw new Exception($"枚举{nameof(enumType.Name)}必须具有FlagsAttribute标记");

            var dt = new DataTable();
            dt.Columns.Add(VALUE, enumType);
            dt.Columns.Add(DESCRIPTION, typeof(string));

            if (enumArray.Count() == 0)
                return dt;


            foreach (TEnum item in enumArray)
            {
                var dr = dt.NewRow();
                dr[VALUE] = item;
                dr[DESCRIPTION] = GetEnumDescriptions((Enum)Enum.ToObject(enumType, item));
                dt.Rows.Add(dr);
            }
            return dt;
        }
        #endregion
    }
}
