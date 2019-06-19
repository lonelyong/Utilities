using System;
using System.Collections.Generic;
using System.Text;
using System.Text.RegularExpressions;

namespace Utilities.Text
{
    public static class RegexUtils
    {
		/// <summary>
		/// 获取一个值该值指示指定的字符串是不是手机号
		/// </summary>
		/// <param name="mobilePhone">手机号</param>
		/// <returns>true/false</returns>
        public static bool IsMobilePhone(this string mobilePhone)
        {
			return Regex.IsMatch(mobilePhone, RegexPatterns.CHINESE_MOBILE_PHONE);
		}
		/// <summary>
		/// 获取一个值该值指示指定的字符串是不是固定电话号
		/// </summary>
		/// <param name="mobilePhone">固定电话号</param>
		/// <returns>true/false</returns>
		public static bool IsTelephone(this string telephone)
        {
			return Regex.IsMatch(telephone, RegexPatterns.CHINESE_TELEPHONE);

		}
		/// <summary>
		/// 获取一个值该值指示指定的字符串是不是ipv4
		/// </summary>
		/// <param name="mobilePhone">ipv4</param>
		/// <returns>true/false</returns>
		public static bool IsIPv4(string ipv4)
        {
            return Regex.IsMatch(ipv4, RegexPatterns.IPV4);
        }
		/// <summary>
		/// 获取一个值该值指示指定的字符串是不是邮箱地址
		/// </summary>
		/// <param name="mobilePhone">邮箱地址</param>
		/// <returns>true/false</returns>
		public static bool IsEmail(this string email)
		{
			return Regex.IsMatch(email, RegexPatterns.EMAIL);
		}
		/// <summary>
		/// 获取一个值该值指示指定的字符串是不是身份证号
		/// </summary>
		/// <param name="mobilePhone">身份证号</param>
		/// <returns>true/false</returns>
		public static bool IsIdCard(this string idcard)
		{
			return Regex.IsMatch(idcard, RegexPatterns.CHINESE_ID);
		}
		/// <summary>
		/// 获取一个值该值指示指定的字符串是不是日期字符串
		/// </summary>
		/// <param name="mobilePhone">日期字符串</param>
		/// <returns>true/false</returns>
		public static bool IsDate(this string dateString)
		{
			return Regex.IsMatch(dateString, RegexPatterns.DATE);
		}
		/// <summary>
		/// 获取一个值该值指示指定的字符串是不是时间字符串
		/// </summary>
		/// <param name="mobilePhone">时间字符串</param>
		/// <returns>true/false</returns>
		public static bool IsTime(this string timeString)
		{
			return Regex.IsMatch(timeString, RegexPatterns.TIME);
		}
		/// <summary>
		/// 获取一个值该值指示指定的字符串是不是日期时间字符串
		/// </summary>
		/// <param name="mobilePhone">日期时间字符串</param>
		/// <returns>true/false</returns>
		public static bool IsDateTime(this string datetimeString)
		{
			return Regex.IsMatch(datetimeString, RegexPatterns.DATETIME);
		}
		/// <summary>
		/// 获取一个值该值指示指定的字符串是不是FTP地址
		/// </summary>
		/// <param name="mobilePhone">FTP地址</param>
		/// <returns>true/false</returns>
		public static bool IsFtpURL(this string ftpURL)
		{
			return Regex.IsMatch(ftpURL, RegexPatterns.URL_FTP);
		}
		/// <summary>
		/// 获取一个值该值指示指定的字符串是不是HTTP(s)地址
		/// </summary>
		/// <param name="mobilePhone">HTTP(s)地址</param>
		/// <returns>true/false</returns>
		public static bool IsHttpURL(this string httpURL)
		{
			return Regex.IsMatch(httpURL, RegexPatterns.URL_HTTP);
		}
		/// <summary>
		/// 获取一个值该值指示指定的字符串是不是域名
		/// </summary>
		/// <param name="mobilePhone">域名</param>
		/// <returns>true/false</returns>
		public static bool IsDomain(this string domain)
		{
			return Regex.IsMatch(domain, RegexPatterns.DOMAIN);
		}
	}
}
