using System;
using System.IO;
using System.Text;
using System.Security.Cryptography;

namespace Utilities.Security
{
    public static class HashHelper
    {
		private static HashAlgorithm GetHashAlgorithm(string algorithm)
		{
			switch (algorithm)
			{
				case nameof(MD5):
					return MD5.Create();
				case nameof(SHA1):
					return SHA1.Create();
				case nameof(SHA256):
					return SHA256.Create();
				case nameof(SHA384):
					return SHA384.Create();
				case nameof(SHA512):
					return SHA512.Create();
				default:
					throw new NotSupportedException();
			}
		}

		private static string GetTextHashByAlgorithm(string text, string algorithm)
		{
			if (string.IsNullOrEmpty(text)) return string.Empty;
			var alg = GetHashAlgorithm(algorithm);
			using (alg)
			{
				var bytValue = System.Text.Encoding.UTF8.GetBytes(text);
				var bytHash = alg.ComputeHash(bytValue);
				alg.Clear();
				//根据计算得到的HASH码翻译为SHA-1码
				return Bytes2String(bytHash);
			}
		}

		private static string GetFileHashByAlgorithm(string path, string algorithm)
		{
			if (!System.IO.File.Exists(path))
				throw new FileNotFoundException();

			using (var fs = new FileStream(path, FileMode.Open, FileAccess.Read))
			{
				using (var alg = GetHashAlgorithm(algorithm))
				{
					var hashByts = alg.ComputeHash(fs);
					alg.Clear();
					return Bytes2String(hashByts);
				}
					
			
			}
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
			var sb = new StringBuilder();
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
		#region Text
		#region MD5
		/// <summary>
		/// 获取文本32位MD5
		/// </summary>
		/// <param name="text"></param>
		/// <returns></returns>
		public static string StringMd5(string text)
		{
			return GetTextHashByAlgorithm(text, nameof(MD5));
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
			return GetTextHashByAlgorithm(text, nameof(SHA1));
        }
        /// <summary>
        /// 获取文本SHA256
        /// </summary>
        /// <param name="text"></param>
        /// <returns></returns>
        public static string StringSHA256(string text)
        {
			return GetTextHashByAlgorithm(text, nameof(SHA256));
		}
        /// <summary>
        /// 获取文本SHA384
        /// </summary>
        /// <param name="text"></param>
        /// <returns></returns>
        public static string StringSHA384(string text)
        {
			return GetTextHashByAlgorithm(text, nameof(SHA384));
		}
        /// <summary>
        /// 获取文本SHA512
        /// </summary>
        /// <param name="text"></param>
        /// <returns></returns>
        public static string StringSHA512(string text)
        {
			return GetTextHashByAlgorithm(text, nameof(SHA512));
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
			return GetFileHashByAlgorithm(path, nameof(MD5));
        }
        /// <summary>
        /// 计算文件的 sha1 值
        /// </summary>
        /// <param name="path">要计算 MD5 值的文件名和路径</param>
        /// <returns>MD5 值16进制字符串</returns>
        public static string FileSHA1(string path)
        {
			return GetFileHashByAlgorithm(path, nameof(SHA1));
		}
        /// <summary>
        /// 计算文件的 sha256 值
        /// </summary>
        /// <param name="path">要计算 MD5 值的文件名和路径</param>
        /// <returns>MD5 值16进制字符串</returns>
        public static string FileSHA256(string path)
        {
			return GetFileHashByAlgorithm(path, nameof(SHA256));
		}
        /// <summary>
        /// 计算文件的 sha384 值
        /// </summary>
        /// <param name="path">要计算 MD5 值的文件名和路径</param>
        /// <returns>MD5 值16进制字符串</returns>
        public static string FileSHA384(string path)
        {
			return GetFileHashByAlgorithm(path, nameof(SHA384));
		}
        /// <summary>
        /// 计算文件的 sha512 值
        /// </summary>
        /// <param name="path">要计算 MD5 值的文件名和路径</param>
        /// <returns>MD5 值16进制字符串</returns>
        public static string FileSHA512(string path)
        {
			return GetFileHashByAlgorithm(path, nameof(SHA512));
		}
        #endregion
    }
}
