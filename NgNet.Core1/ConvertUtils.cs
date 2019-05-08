using System;
using System.Collections.Generic;
using System.Text;
using NgNet.Reflection;

namespace NgNet
{
    public static class ConvertUtils
    {
        private static readonly DateTime _unixTimeStart = new DateTime(1970, 1, 1);

        /// <summary>
        /// 将具有short值的对象转为short
        /// </summary>
        /// <param name="objShort"></param>
        /// <param name="def"></param>
        /// <returns></returns>
        public static short ToShort(object objShort, short def)
        {
            short.TryParse(objShort?.ToString(), out def);
            return def;
        }
        /// <summary>
        /// 将具有int值的对象转为int
        /// </summary>
        /// <param name="objInt"></param>
        /// <param name="def"></param>
        /// <returns></returns>
        public static int ToInt(object objInt, int def)
        {
            int.TryParse(objInt?.ToString(), out def);
            return def;
        }

        /// <summary>
        /// 将具有long值的对象转为long
        /// </summary>
        /// <param name="objLong"></param>
        /// <param name="def"></param>
        /// <returns></returns>
        public static long ToLong(object objLong, long def)
        {
            long.TryParse(objLong?.ToString(), out def);
            return def;
        }

        /// <summary>
        /// 将阿拉伯数字转为中国数字
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="num"></param>
        /// <returns></returns>
        public static string ToChineseFigures<T>(T num) where T : struct
        {
            if(!typeof(T).IsNumericType() || !typeof(T).IsNullableNumericType())
            {
                throw new Exception("T必须是数值类型");
            }
            return string.Empty;
        }

        /// <summary>
        /// 将阿拉伯数字转为中国大写数字
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="num"></param>
        /// <returns></returns>
        public static string ToUpperChineseFigures<T>(T num)
        {
            if (!typeof(T).IsNumericType() || !typeof(T).IsNullableNumericType())
            {
                throw new Exception("T必须是数值类型");
            }
            return string.Empty;
        }

        /// <summary>
        /// 将具有bool值的对象转为bool
        /// </summary>
        /// <param name="objBool"></param>
        /// <param name="def"></param>
        /// <returns></returns>
        public static bool ToBool(object objBool, bool def)
        {
            bool.TryParse(objBool?.ToString(), out def);
            return def;
        }
        /// <summary>
        /// 将具有DateTime值的对象转为DateTime
        /// </summary>
        /// <param name="objDateTime"></param>
        /// <param name="def"></param>
        /// <returns></returns>
        public static DateTime ToDateTime(object objDateTime, DateTime def)
        {
            DateTime.TryParse(objDateTime?.ToString(), out def);
            return def;
        }

        /// <summary>
        /// 将时间戳转为DateTime对象
        /// </summary>
        /// <param name="timestamp"></param>
        /// <returns></returns>
        public static DateTime ToDateTime(long timestamp)
        {
            return new DateTime(timestamp);
        }

        /// <summary>
        /// 将事件转为Unix时间戳
        /// </summary>
        /// <param name="dateTime"></param>
        /// <returns></returns>
        public static long ToTimestamp(DateTime dateTime)
        {
            return (dateTime - _unixTimeStart).Seconds;
        }

        /// <summary>
        /// 获取当前时间的Unix时间戳
        /// </summary>
        /// <returns></returns>
        public static long ToTimestamp()
        {
            return ToTimestamp(DateTime.Now);
        }


    }
}
