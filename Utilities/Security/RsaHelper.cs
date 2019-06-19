using System.Text;
using System.Security.Cryptography;
using System;
using System.Xml;

namespace Utilities.Security
{
    public class RsaHelper : IDisposable
    {
        #region private field
        private const string PrivateKey = "<RSAKeyValue><Modulus>vo9zxoXT7A3e8Npkca9/wL4NzLYJmvh1eysc10jg+y5OgJ0aRu5IBl80OqgML/Nrw6MfdxHZKsvN/FSL9sUvXFfuwh0YSwHPMp1mp8JMomaCfTSW6sSMsAXOu9Ebwjxk91hE8/3xjf6FQHZbjd25IfHp/irNI9KS/wufOSTSZa6crZcYHwFTCSvwU1sMrJOCgTBx+94lvoNmJUMzSsP3YHVvJeBnnf4f1xMBjxACegPdiIRvKtxYuE9pip3/TiImxuVI+oUxqjGTcRVjGJyZOBlP3OwJx5LIuKmWMUGvTQ4Ny0nMdAPJ1Z63CSYai3DEeNgm4aQAp2hAWXbrVlxoyQ==</Modulus><Exponent>AQAB</Exponent><P>8N6QWIKbPxaGugFyaNlPw8DYhW1Z+j8c8BCRoDVe/uPsAVt+4mEG69l5sU3kQPadgl4WFhC8STKTZgqE7/JDpa8yl1nGI8DwzYoMKRWC5lBb2udUXjk6nA65qW+84mKa+YXQKnXt5Knx5qSWWrlPPlKHFvG2Lz8+jwpAjroD9nc=</P><Q>yofdm6AyofOlGCayKN4Ch3POhBBonCaMAkhwUGPuUUFj9qtYjAZICKnzLomUSxtaLNFc4lv99Em/j8zlz4l9N+W+TI+q1pmPKqHyvc0G20FhoeXhFhgi4n1yogKyU2oPTxapoVfrVeIPEVYL/yWSeFHbUGy2+UBhzqlvnfbIKr8=</Q><DP>4N7n47oLNdrR7NVs+nXnAdoISojMd9R0L6tISUmKZmxi6OGCk9YBoC5obh9J+VxDsHImLHNzVOLiuWEYuupyPTxS/vnRAZAfP8Shlbl/e4WKO9O78Dt9fDRDIOwwaymjhVBoBzwR3GjMbYc613gpgwWJGOKQM4vX27K2kokXKgE=</DP><DQ>vImoeiSyneMjsIztU3ABKLlc6cIgsCSON93ZeGzewRO/jDTMVGXQjwgu0wbOXvDSjGBGOI7spYo14xcaZI4YXH3qHnmrzuTayxRB1gDUPi3qRn2qgRfs+a1QlpMuEMrt/3EYbpNbB/NOlZPlzRrb9Fg3cvIRj40ov+spdgLzKh8=</DQ><InverseQ>5dyhUUd8WKaNiPuUhHkeyJ4P+10+SR5aEj8xYjmNY7H8RJqzl9THPiWJEFA0GMJhbIkvoBf/7a9LBf1MhygPVirKvmTbKdDRJsGZDZ/+NIyKquEKXsZk9jGo26FyExvAa0RuYO9pT2h0WLt9US6XHAQ+6JHhyHvBtBjwd0QbP2g=</InverseQ><D>L0IqRziXRvplqLuwn1UTzLwVdghLN4iYshQOEgH78a8ZPuI3SxH7AMGJlWZpp1aqChmhSwk9H1Tt378j+u4KAc+44aEPjkKDnRzWAZK5KKldgSmSp+RYs6qtC30fcjtCGulkKFJ5HmrZpxPzPqqXKCjzi9oXh+PIGsJKGYTdPOPz9o7oYrprcf8YfEUFl182FKAbLvMsyK9cenJQ1OwFKzDuat7x1zL3/sW7pOlGLyWeNO6U0TBqKqVAGFgEYutHmMDwc5zc3sHALREXet2PZ61sGYCBbpBmtcPYzpwqD8Iue56QGqWBZZNed34LQYA0wNT2ytbGIo960PwJ+ctfRQ==</D></RSAKeyValue>";
        private RSA rsa;
        #endregion

        #region public properties
        /// <summary>
        /// 密钥长度
        /// </summary>
        public int KeySize
        {
            get
            {
                return this.rsa.KeySize;
            }
        }

        /// <summary>
        /// 是否只能加密
        /// </summary>
        public bool PublicOnly
        {
            get { throw new NotImplementedException(); }
        }
        #endregion

        #region construct function
        /// <summary>
        /// rsa密钥长度
        /// </summary>
        /// <param name="keySize"></param>
        public RsaHelper(int keySize)
        {
            this.Reset(keySize);  
        }
        #endregion

        #region public method

        /// <summary>
        /// 随机生成密钥对
        /// </summary>
        public void Reset(int keySize)
        {
            this.rsa = RSA.Create(keySize);
        }
        /// <summary>
        /// 获取密钥
        /// </summary>
        /// <param name="OnlyPublic"></param>
        /// <returns></returns>
        public string ToXmlString(bool both)
        {
			return ToXmlString(rsa, both);
		}
		/// <summary>
		/// 设置rsa密钥
		/// </summary>
		/// <param name="xmlKey"></param>
		/// <param name="keySize"></param>
		public void FromXmlString(string xmlKey)
        {
			FromXmlString(rsa, xmlKey);
		}
        /// <summary>
        /// 加密字符串
        /// </summary>
        /// <param name="toEncrypt"></param>
        /// <returns></returns>
        public string Encrypt(string toEncrypt)
        {
            return System.Convert.ToBase64String(this.rsa.Encrypt(Encoding.UTF8.GetBytes(toEncrypt), RSAEncryptionPadding.Pkcs1));
        }
        /// <summary>
        /// 解密字符串
        /// </summary>
        /// <param name="ToDecrypt"></param>
        /// <returns></returns>
        public string Decrypt(string toDecrypt)
        {
            return Encoding.UTF8.GetString(this.rsa.Decrypt(System.Convert.FromBase64String(toDecrypt), RSAEncryptionPadding.Pkcs1));
        }
        #endregion

        #region 静态方法
		/// <summary>
		/// 设置指定rsa对象的key
		/// </summary>
		/// <param name="rsa"></param>
		/// <param name="xml"></param>
		public static void FromXmlString(RSA rsa, string xml)
		{
			RSAParameters parameters = new RSAParameters();
			XmlDocument xmlDoc = new XmlDocument();
			xmlDoc.LoadXml(xml);
			if (xmlDoc.DocumentElement.Name.Equals("RSAKeyValue"))
			{
				foreach (XmlNode node in xmlDoc.DocumentElement.ChildNodes)
				{
					switch (node.Name)
					{
						case "Modulus": parameters.Modulus = Convert.FromBase64String(node.InnerText); break;
						case "Exponent": parameters.Exponent = Convert.FromBase64String(node.InnerText); break;
						case "P": parameters.P = Convert.FromBase64String(node.InnerText); break;
						case "Q": parameters.Q = Convert.FromBase64String(node.InnerText); break;
						case "DP": parameters.DP = Convert.FromBase64String(node.InnerText); break;
						case "DQ": parameters.DQ = Convert.FromBase64String(node.InnerText); break;
						case "InverseQ": parameters.InverseQ = Convert.FromBase64String(node.InnerText); break;
						case "D": parameters.D = Convert.FromBase64String(node.InnerText); break;
					}
				}
			}
			else
			{
				throw new Exception("Invalid XML RSA key.");
			}

			rsa.ImportParameters(parameters);
		}

		/// <summary>
		/// 将指定rsa对象的key序列化为xml
		/// </summary>
		/// <param name="rsa"></param>
		/// <returns></returns>
		public static string ToXmlString(RSA rsa, bool both)
		{
			var parameters = rsa.ExportParameters(both);
			if (both)
			{
				return string.Format("<RSAKeyValue><Modulus>{0}</Modulus><Exponent>{1}</Exponent><P>{2}</P><Q>{3}</Q><DP>{4}</DP><DQ>{5}</DQ><InverseQ>{6}</InverseQ><D>{7}</D></RSAKeyValue>",
					Convert.ToBase64String(parameters.Modulus),
					Convert.ToBase64String(parameters.Exponent),
					Convert.ToBase64String(parameters.P),
					Convert.ToBase64String(parameters.Q),
					Convert.ToBase64String(parameters.DP),
					Convert.ToBase64String(parameters.DQ),
					Convert.ToBase64String(parameters.InverseQ),
					Convert.ToBase64String(parameters.D));
			}
			return string.Format("<RSAKeyValue><Modulus>{0}</Modulus><Exponent>{1}</Exponent></RSAKeyValue>",
					Convert.ToBase64String(parameters.Modulus),
					Convert.ToBase64String(parameters.Exponent));
		}

        /// 2048位RSA解密，仅可以解密本类中RSAEncrypt函数加密的字符串
        /// <param name="xmlPrivateKeys">私钥</param>
        /// <param name="toDecrypt">要解密的数据</param>
        /// <returns></returns>
        public static string JustDecrypt(string toDecrypt)
        {
            return Decrypt(PrivateKey, toDecrypt);
        }

        /// <summary>
        /// 2048位RSA加密,仅可以用本类中的RSADecrypt函数解密
        /// </summary>
        /// <param name="xmlPublicKeys">公钥</param>
        /// <param name="toEncrypt">要加密的数据</param>
        /// <returns></returns>
        public static string JustEncrypt(string toEncrypt)
        {
            return Encrypt(PrivateKey, toEncrypt);
        }

        /// 函数加密的字符串
        /// <param name="xmlPrivateKey">私钥</param>
        /// <param name="toDecrypt">要解密的数据</param>
        /// <returns></returns>
        public static string Decrypt(string xmlPrivateKey, string toDecrypt)
        {
			using (var rsa = RSA.Create())
			{
				FromXmlString(rsa, xmlPrivateKey);
				var rgb = System.Convert.FromBase64String(toDecrypt);
				var decByte = rsa.Decrypt(rgb, RSAEncryptionPadding.Pkcs1);
				return Encoding.UTF8.GetString(decByte);
			}
        }

        /// <summary>
        /// RSA加密,仅可以用本类中的RSADecrypt函数解密
        /// </summary>
        /// <param name="xmlPublicKey">公钥</param>
        /// <param name="toEncrypt">要加密的数据</param>
        /// <returns></returns>
        public static string Encrypt(string xmlPublicKey, string toEncrypt)
        {
			using (var rsa = RSA.Create())
			{
				FromXmlString(rsa, xmlPublicKey);
				var encByte = Encoding.UTF8.GetBytes(toEncrypt);
				return System.Convert.ToBase64String(rsa.Encrypt(encByte, RSAEncryptionPadding.Pkcs1));
			}
        }

        /// <summary>
        /// 判断是不是合法的密钥
        /// </summary>
        /// <param name="xmlKey">检测的密钥</param>
        /// <returns></returns>
        public static bool IsXmlKey(string xmlKey)
        {
			using (var rsa = RSA.Create())
			{
				try
				{

					FromXmlString(rsa, xmlKey);
					return true;
				}
				catch (Exception)
				{
					return false;
				}
			}
        }

        /// <summary>
        /// 获取指定密钥的大小
        /// </summary>
        /// <param name="xmlKey"></param>
        /// <returns></returns>
        public static int GetKeySize(string xmlKey)
        {
			using (var rsa = RSA.Create())
			{
				FromXmlString(rsa, xmlKey);
				return rsa.KeySize;
			}
        }

		public void Dispose()
		{
			((IDisposable)rsa).Dispose();
		}
		#endregion
	}
}
