using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;
using System.Security.Cryptography;

namespace NgNet.Security
{
    public class DesHelper
    {
        #region private filed
        /// <summary>
        /// 密钥加密算法，默认为SHA1
        /// </summary>
        private HashAlgorithm KeyHash;

        private List<int> supportedKeySize;

        private Encoding _encoding = Encoding.Default;

        private DES des;
        #endregion

        #region public properties
        /// <summary>
        /// 获取或设置文本编码的方式
        /// </summary>
        public Encoding encoding
        {
            set { _encoding = value == null ? Encoding.Default : value; }
            get { return _encoding; }
        }
        /// <summary>
        /// 通过字符串设置密钥
        /// </summary>
        public string StringKey
        {
            set
            {
                if (string.IsNullOrEmpty(value))
                    throw new Exception("密钥不能为空");
                byte[] keyHash = KeyHash.ComputeHash(encoding.GetBytes(value));
                byte[] tmp = new byte[8];
                for (int i = 0; i < 8; i++)
                {
                    tmp[i] = keyHash[i];
                }
                this.Key = tmp;
                for (int i = 8; i < 16; i++)
                {
                    tmp[i - 8] = keyHash[i];
                }
                this.IV = tmp;
            }
        }
        /// <summary>
        /// 
        /// </summary>
        public byte[] Key
        {
            set
            {
                if (!supportedKeySize.Contains(value.Length * 8))
                    throw new Exception("密钥长度不对");
                this.des.Key = value;
            }
            get { return this.Key; }
        }
        /// <summary>
        /// 设置对称加密算法的初始化向量
        /// </summary>
        public byte[] IV
        {
            set
            {
                if (!supportedKeySize.Contains(value.Length * 8))
                    throw new Exception("向量长度不对");
                this.des.IV = value;
            }
            get { return this.IV; }
        }
        /// <summary>
        ///  获取密钥大小
        /// </summary>
        public int KeySize
        {
            get { return des.KeySize; }
        }
        /// <summary>
        /// 获取支持的密钥大小
        /// </summary>
        public KeySizes[] LegalKeySizes
        {
            get { return des.LegalKeySizes; }
        }
        /// <summary>
        /// 获取支持的块大小
        /// </summary>
        public KeySizes[] LegalBlockSizes
        {
            get { return des.LegalBlockSizes; }


        }
        /// <summary>
        /// 获取支持的密钥大小
        /// </summary>
        public int[] SupportedKeySize
        {
            get
            {
                List<int> tmp = new List<int>();
                int step = 0;
                foreach (KeySizes item in des.LegalKeySizes)
                {
                    if (item.SkipSize == 0)
                        if (item.MaxSize == item.MinSize)
                            step = item.MaxSize;
                        else
                            step = item.MaxSize - item.MinSize;
                    else
                        step = item.SkipSize;

                    for (int i = item.MinSize; i <= item.MaxSize; i += step)
                    {
                        if (!tmp.Contains(i))
                            tmp.Add(i);
                    }
                }
                return tmp.ToArray();
            }

        }
        #endregion

        #region constructor
        public DesHelper(string key, DES des)
        {
            if (des == null)
                throw new Exception("des不能为null");
            else
                this.des = des;
            if (string.IsNullOrEmpty(key))
                throw new Exception("密钥不能为空");
            //获取支持的密钥长度
            this.supportedKeySize = new List<int>(SupportedKeySize);
            // 初始化默认的key的加密方式
            this.KeyHash = SHA1.Create();
            this.StringKey = key;
        }

        #endregion

        #region instance method
        #region 加解密字符串
        /// <summary>
        ///  加密字符串
        /// </summary>
        /// <param name="scr"></param>
        /// <returns></returns>
        public string EncryptString(string scr)
        {

            MemoryStream ms = new MemoryStream();
            CryptoStream cs = new CryptoStream(ms, des.CreateEncryptor(), CryptoStreamMode.Write);
            byte[] inputByteArray = encoding.GetBytes(scr);
            cs.Write(inputByteArray, 0, inputByteArray.Length);
            cs.FlushFinalBlock();
            StringBuilder ret = new StringBuilder();
            foreach (byte b in ms.ToArray())
            {
                ret.AppendFormat("{0:X2}", b);
            }
            return ret.ToString();
        }
        /// <summary>
        ///  解密字符串
        /// </summary>
        /// <param name="scr"></param>
        /// <returns></returns>
        public string DecryptString(string scr)
        {
            byte[] inputByteArray = new byte[scr.Length / 2];
            for (int x = 0; x < scr.Length / 2; x++)
            {
                int i = (System.Convert.ToInt32(scr.Substring(x * 2, 2), 16));
                inputByteArray[x] = (byte)i;
            }
            MemoryStream ms = new MemoryStream();
            CryptoStream cs = new CryptoStream(ms, des.CreateDecryptor(), CryptoStreamMode.Write);
            cs.Write(inputByteArray, 0, inputByteArray.Length);
            cs.FlushFinalBlock();
            StringBuilder ret = new StringBuilder();
            return encoding.GetString(ms.ToArray());
        }
        #endregion

        #region 加解密文件
        /// <summary>
        ///  加密文件
        /// </summary>
        /// <param name="filePath">要加密的文件位置</param>
        /// <param name="savePath">加密后文件保存到的位置</param>
        /// <returns></returns>
        public bool EncryptFile(string filePath, string savePath)
        {
            FileStream fs = File.OpenRead(filePath);
            byte[] inputByteArray = new byte[fs.Length];
            fs.Read(inputByteArray, 0, (int)fs.Length);
            fs.Close();

            MemoryStream ms = new MemoryStream();
            CryptoStream cs = new CryptoStream(ms, des.CreateEncryptor(), CryptoStreamMode.Write);
            cs.Write(inputByteArray, 0, inputByteArray.Length);
            cs.FlushFinalBlock();
            fs = File.OpenWrite(savePath);
            foreach (byte b in ms.ToArray())
            {
                fs.WriteByte(b);
            }
            fs.Close();
            cs.Close();
            ms.Close();
            return true;
        }
        /// <summary>
        /// 解密文件
        /// </summary>
        /// <param name="filePath">要解密的文件</param>
        /// <param name="savePath">解密后保存到的位置</param>
        /// <param name="keyStr"></param>
        /// <returns></returns>
        public bool DecryptFile(string filePath, string savePath)
        {
            FileStream fs = File.OpenRead(filePath);
            byte[] inputByteArray = new byte[fs.Length];
            fs.Read(inputByteArray, 0, (int)fs.Length);
            fs.Close();

            MemoryStream ms = new MemoryStream();
            CryptoStream cs = new CryptoStream(ms, des.CreateDecryptor(), CryptoStreamMode.Write);
            cs.Write(inputByteArray, 0, inputByteArray.Length);
            cs.FlushFinalBlock();
            fs = File.OpenWrite(savePath);
            foreach (byte b in ms.ToArray())
            {
                fs.WriteByte(b);
            }
            fs.Close();
            cs.Close();
            ms.Close();
            return true;
        }
        #endregion
        #endregion

        #region static methods
        #region 加密字符串
        /// <summary>
        /// 加密字符串
        /// </summary>
        /// <param name="inputStr">输入字符串</param>
        /// <param name="keyStr">密码，可以为“”</param>
        /// <returns>输出加密后字符串</returns>
        public static string EncryptString(string inputStr, string keyStr)
        {
            if (string.IsNullOrEmpty(keyStr))
                throw new Exception("加密密钥不能为空");

            DesHelper des = new DesHelper(keyStr, DESCryptoServiceProvider.Create());

            return des.EncryptString(inputStr);
        }

        /// <summary>
        /// 解密字符串
        /// </summary>
        /// <param name="inputStr">要解密的字符串</param>
        /// <param name="keyStr">密钥</param>
        /// <returns>解密后的结果</returns>
        static public string SDecryptString(string inputStr, string keyStr)
        {
            if (string.IsNullOrEmpty(keyStr))
                throw new Exception("加密密钥不能为空");

            DesHelper des = new DesHelper(keyStr, DESCryptoServiceProvider.Create());

            return des.DecryptString(inputStr);
        }
        #endregion

        #region 加解密文件
        /// <summary>
        /// 加密文件
        /// </summary>
        /// <param name="filePath">输入文件路径</param>
        /// <param name="savePath">加密后输出文件路径</param>
        /// <param name="keyStr">密码，可以为“”</param>
        /// <returns></returns>  
        public static bool EncryptFile(string filePath, string savePath, string keyStr)
        {
            if (string.IsNullOrEmpty(keyStr))
                throw new Exception("加密密钥不能为空");

            DesHelper des = new DesHelper(keyStr, DESCryptoServiceProvider.Create());

            return des.EncryptFile(filePath, savePath);
        }

        /// <summary>
        /// 解密文件
        /// </summary>
        /// <param name="filePath">输入文件路径</param>
        /// <param name="savePath">解密后输出文件路径</param>
        /// <param name="keyStr">密码，可以为“”</param>
        /// <returns></returns>    
        public static bool DecryptFile(string filePath, string savePath, string keyStr)
        {
            if (string.IsNullOrEmpty(keyStr))
                throw new Exception("加密密钥不能为空");

            DesHelper des = new DesHelper(keyStr, DESCryptoServiceProvider.Create());

            return des.DecryptFile(filePath, savePath);
        }
        #endregion}
        #endregion
    }
}
