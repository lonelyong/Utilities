using System;
using System.IO;
using System.Text;
using System.Security.Cryptography;

namespace NgNet.Security
{
    public static class HashHelper
    {
        #region HashText
        #region MD5
        /// <summary>
        /// 获取文本32位MD5
        /// </summary>
        /// <param name="text"></param>
        /// <returns></returns>
        public static string StringMd5(string text)
        {
            if (string.IsNullOrEmpty(text)) { return string.Empty; }
            MD5 md5 = MD5.Create();
            byte[] bytValue = Encoding.UTF8.GetBytes(text);
            byte[] bytHash = md5.ComputeHash(bytValue);
            md5.Clear();
            string sHash = Bytes2String(bytHash);
            return sHash;
        }
        #endregion

        #region SHA
        /// <summary>
        /// 获取文本SHA1
        /// </summary>
        /// <param name="text"></param>
        /// <returns></returns>
        public static string StringSHA1(string text)
        {
            if (string.IsNullOrEmpty(text)) { return ""; }
            var provider = new System.Security.Cryptography.SHA1CryptoServiceProvider();

            byte[] bytValue = System.Text.Encoding.UTF8.GetBytes(text);
            byte[] bytHash = provider.ComputeHash(bytValue);
            provider.Clear();

            //根据计算得到的Hash码翻译为SHA-1码
            string sHash = Bytes2String(bytHash);
            //根据大小写规则决定返回的字符串
            return sHash.ToLower();
        }
        /// <summary>
        /// 获取文本SHA256
        /// </summary>
        /// <param name="text"></param>
        /// <returns></returns>
        public static string StringSHA256(string text)
        {
            if (string.IsNullOrEmpty(text)) { return ""; }
            var provider = new System.Security.Cryptography.SHA256CryptoServiceProvider();

            byte[] bytValue = System.Text.Encoding.UTF8.GetBytes(text);
            byte[] bytHash = provider.ComputeHash(bytValue);
            provider.Clear();

            //根据计算得到的Hash码翻译为SHA-1码
            string sHash = Bytes2String(bytHash);
            //根据大小写规则决定返回的字符串
            return sHash.ToLower();
        }
        /// <summary>
        /// 获取文本SHA384
        /// </summary>
        /// <param name="text"></param>
        /// <returns></returns>
        public static string StringSHA384(string text)
        {
            if (string.IsNullOrEmpty(text)) { return ""; }
            var provider = new System.Security.Cryptography.SHA384CryptoServiceProvider();
            byte[] bytValue = System.Text.Encoding.UTF8.GetBytes(text);
            byte[] bytHash = provider.ComputeHash(bytValue);
            provider.Clear();

            //根据计算得到的Hash码翻译为SHA-1码
            string sHash = Bytes2String(bytHash);
            //根据大小写规则决定返回的字符串
            return sHash.ToLower();
        }
        /// <summary>
        /// 获取文本SHA512
        /// </summary>
        /// <param name="text"></param>
        /// <returns></returns>
        public static string StringSHA512(string text)
        {
            if (string.IsNullOrEmpty(text)) { return string.Empty; }
            var provider = new System.Security.Cryptography.SHA512CryptoServiceProvider();

            byte[] bytValue = System.Text.Encoding.UTF8.GetBytes(text);
            byte[] bytHash = provider.ComputeHash(bytValue);
            provider.Clear();

            //根据计算得到的Hash码翻译为SHA-1码
            string sHash = Bytes2String(bytHash);
            //根据大小写规则决定返回的字符串
            return sHash.ToLower();
        }

        private static string Bytes2String(byte[] bytHash)
        {

            #region method one
            /*/根据计算得到的Hash码翻译为16进制码
            string sHash = "", sTemp = "";
            for (int counter = 0; counter < bytHash.Count(); counter++)
            {
                long i = bytHash[counter] / 16;
                if (i > 9)
                {
                    sTemp = ((char)(i - 10 + 0x41)).ToString();
                }
                else
                {
                    sTemp = ((char)(i + 0x30)).ToString();
                }
                i = bytHash[counter] % 16;
                if (i > 9)
                {
                    sTemp += ((char)(i - 10 + 0x41)).ToString();
                }
                else
                {
                    sTemp += ((char)(i + 0x30)).ToString();
                }
                sHash += sTemp;
            }
            return sHash;
             **/
            #endregion

            #region method two
            StringBuilder sb = new StringBuilder();
            if (bytHash == null)
                return string.Empty;
            else
                foreach (var byt in bytHash)
                {
                    sb.Append(byt.ToString("X2"));
                }
            return sb.ToString();
            #endregion
        }

        #endregion
        #endregion

        #region HashFile
        /// <summary>
        /// 计算文件的 MD5 值
        /// </summary>
        /// <param name="path">要计算 MD5 值的文件名和路径</param>
        /// <returns>MD5 值16进制字符串</returns>
        public static string FileMD5(string path)
        {
            if (System.IO.File.Exists(path) == false)
                return string.Empty;

            FileStream fs = new FileStream(path, FileMode.Open, FileAccess.Read);
            var md5 = SHA512CryptoServiceProvider.Create();
            byte[] hashByts = md5.ComputeHash(fs);
            return Bytes2String(hashByts);
        }
        /// <summary>
        /// 计算文件的 sha1 值
        /// </summary>
        /// <param name="path">要计算 MD5 值的文件名和路径</param>
        /// <returns>MD5 值16进制字符串</returns>
        public static string FileSHA1(string path)
        {
            if (System.IO.File.Exists(path) == false)
                return string.Empty;

            FileStream fs = new FileStream(path, FileMode.Open, FileAccess.Read);
            var sha1 = SHA1.Create();
            byte[] hashByts = sha1.ComputeHash(fs);
            return Bytes2String(hashByts);
        }
        /// <summary>
        /// 计算文件的 sha256 值
        /// </summary>
        /// <param name="path">要计算 MD5 值的文件名和路径</param>
        /// <returns>MD5 值16进制字符串</returns>
        public static string FileSHA256(string path)
        {
            if (System.IO.File.Exists(path) == false)
                return string.Empty;

            FileStream fs = new FileStream(path, FileMode.Open, FileAccess.Read);
            var sha256 = SHA256.Create();
            byte[] hashByts = sha256.ComputeHash(fs);
            return Bytes2String(hashByts);
        }
        /// <summary>
        /// 计算文件的 sha384 值
        /// </summary>
        /// <param name="path">要计算 MD5 值的文件名和路径</param>
        /// <returns>MD5 值16进制字符串</returns>
        public static string FileSHA384(string path)
        {
            if (System.IO.File.Exists(path) == false)
                return string.Empty;

            FileStream fs = new FileStream(path, FileMode.Open, FileAccess.Read);
            var sha384 = SHA384.Create();
            byte[] hashByts = sha384.ComputeHash(fs);
            return Bytes2String(hashByts);
        }
        /// <summary>
        /// 计算文件的 sha512 值
        /// </summary>
        /// <param name="path">要计算 MD5 值的文件名和路径</param>
        /// <returns>MD5 值16进制字符串</returns>
        public static string FileSHA512(string path)
        {
            if (System.IO.File.Exists(path) == false)
                return string.Empty;

            FileStream fs = new FileStream(path, FileMode.Open, FileAccess.Read);
            var sha512 = SHA512.Create();
            byte[] hashByts = sha512.ComputeHash(fs);
            return Bytes2String(hashByts);
        }
        /// <summary>
        /// 字节数组转换为16进制表示的字符串
        /// </summary>
        #endregion
    }
}
