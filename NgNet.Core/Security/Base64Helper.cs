using System;
using System.Text;

namespace NgNet.Security
{
    #region <class - Base64Encrypt>
    public class Base64Helper
    {

        #region Base64加解密
        /// <summary>
        /// base64加密
        /// </summary>
        /// <param name="inputText">要加密的字符串</param>
        /// <param name="encoding">编码方式</param>
        /// <returns></returns>
        public static string ToBase64String(string inputText, Encoding encoding)
        {
            //如果字符串为空，则返回
            if (String.IsNullOrEmpty(inputText))
                return string.Empty;
            return System.Convert.ToBase64String(encoding.GetBytes(inputText));

            #region 自定义代码获取Base64Code
            /*
            //如果字符串为空，则返回
            if (String.IsNullOrEmpty(inputText))
                return string.Empty;
            char[] Base64Code = Text.Constant.Base64Code.ToCharArray();
            byte empty = (byte)0;
            List<byte> byteMessage = new List<byte>(encoding.GetBytes(inputText));
            StringBuilder outmessage;
            int messageLen = byteMessage.Count;
            int page = messageLen / 3;
            int use = 0;
            if ((use = messageLen % 3) > 0)
            {
                for (int i = 0; i < 3 - use; i++)
                    byteMessage.Add(empty);
                page++;
            }
            outmessage = new System.Text.StringBuilder(page * 4);
            for (int i = 0; i < page; i++)
            {
                byte[] instr = new byte[3];
                instr[0] = byteMessage[i * 3];
                instr[1] = byteMessage[i * 3 + 1];
                instr[2] = byteMessage[i * 3 + 2];
                int[] outstr = new int[4];
                outstr[0] = instr[0] >> 2;
                outstr[1] = ((instr[0] & 0x03) << 4) ^ (instr[1] >> 4);
                if (!instr[1].Equals(empty))
                    outstr[2] = ((instr[1] & 0x0f) << 2) ^ (instr[2] >> 6);
                else
                    outstr[2] = 64;
                if (!instr[2].Equals(empty))
                    outstr[3] = (instr[2] & 0x3f);
                else
                    outstr[3] = 64;
                outmessage.Append(Base64Code[outstr[0]]);
                outmessage.Append(Base64Code[outstr[1]]);
                outmessage.Append(Base64Code[outstr[2]]);
                outmessage.Append(Base64Code[outstr[3]]);
            }
            return outmessage.ToString();*/
            #endregion
        }

        /// <summary>
        /// base64解密
        /// </summary>
        /// <param name="inputText">要解密的字符串</param>
        /// <param name="encoding">编码方式</param>
        /// <returns></returns>
        public static string FormBase64String(string inputText, Encoding encoding)
        {  //如果字符串为空，则返回
            if (String.IsNullOrEmpty(inputText))
                return string.Empty;
            return encoding.GetString(System.Convert.FromBase64String(inputText));

            #region 自定义代码解密Base64Code
            /*
            //如果字符串为空，则返回
            if (String.IsNullOrEmpty(text))
                return string.Empty;
            //将空格替换为加号
            text = text.Replace(" ", "+");
            if ((text.Length % 4) != 0)
                return "包含不正确的BASE64编码";
            if (!Regex.IsMatch(text, "^[A-Z0-9/+=]*$", RegexOptions.IgnoreCase))
                return "包含不正确的BASE64编码";
            string Base64Code = Text.Constant.Base64Code;
            int page = text.Length / 4;
            List<byte> outMessage = new List<byte>(page * 3);
            char[] message = text.ToCharArray();
            for (int i = 0; i < page; i++)
            {
                byte[] instr = new byte[4];
                instr[0] = (byte)Base64Code.IndexOf(message[i * 4]);
                instr[1] = (byte)Base64Code.IndexOf(message[i * 4 + 1]);
                instr[2] = (byte)Base64Code.IndexOf(message[i * 4 + 2]);
                instr[3] = (byte)Base64Code.IndexOf(message[i * 4 + 3]);
                byte[] outstr = new byte[3];
                outstr[0] = (byte)((instr[0] << 2) ^ ((instr[1] & 0x30) >> 4));
                if (instr[2] != 64)
                {
                    outstr[1] = (byte)((instr[1] << 4) ^ ((instr[2] & 0x3c) >> 2));
                }
                else
                {
                    outstr[2] = 0;
                }
                if (instr[3] != 64)
                {
                    outstr[2] = (byte)((instr[2] << 6) ^ instr[3]);
                }
                else
                {
                    outstr[2] = 0;
                }
                outMessage.Add(outstr[0]);
                if (outstr[1] != 0)
                    outMessage.Add(outstr[1]);
                if (outstr[2] != 0)
                    outMessage.Add(outstr[2]);
            }
            byte[] outbyte = outMessage.ToArray();
            return encoding.GetString(outbyte);*/
            #endregion
        }
        #endregion
    }
    #endregion
}
