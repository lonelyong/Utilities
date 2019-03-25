using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NgNet.Security
{
    /// <summary>
    /// CRC计算类
    /// </summary>
    public static class CrcHelper
    {
        #region private static fileds
        private static ulong[] crc32Table;
        #endregion

        #region constructor
        static CrcHelper()
        {
            crc32Table = GetCRC32Table();
        }
        #endregion

        #region public static methods
        /// <summary>
        /// 获取CRC32表
        /// </summary>
        /// <returns></returns>
        public static ulong[] GetCRC32Table()
        {
            ulong[] _table = new ulong[256];
            ulong _crc;
            int i, j;
            for (i = 0; i < 256; i++)
            {
                _crc = (ulong)i;
                for (j = 8; j > 0; j--)
                {
                    if ((_crc & 1) == 1)
                        _crc = (_crc >> 1) ^ 0xEDB88320;
                    else
                        _crc >>= 1;
                }
                _table[i] = _crc;
            }
            return _table;
        }

        /// <summary>
        /// 计算CRC32值
        /// </summary>
        /// <param name="bytes"></param>
        /// <returns></returns>
        public static ulong GetCRC32(byte[] bytes)
        {

            int iCount = bytes.Length;
            ulong crc = 0xFFFFFFFF;
            for (int i = 0; i < iCount; i++)
            {
                crc = ((crc >> 8) & 0x00FFFFFF) ^ crc32Table[(crc ^ bytes[i]) & 0xFF];
            }
            return crc ^ 0xFFFFFFFF; ;
        }

        /// <summary>
        /// 计算指定字符串的CRC32值
        /// </summary>
        /// <param name="sInputString"></param>
        /// <returns></returns>
        public static ulong GetCRC32Str(string sInputString)
        {
            //生成码表
            byte[] buffer = System.Text.ASCIIEncoding.ASCII.GetBytes(sInputString);
            return GetCRC32(buffer);
        }


        #endregion
    }
} 
