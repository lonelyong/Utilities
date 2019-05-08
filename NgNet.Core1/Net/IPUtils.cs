using System;
using System.Collections.Generic;
using System.Net;
using System.Text;

namespace NgNet.Net
{
	public static class IPUtils
	{
		/// <summary>
		/// 获取当前主机的内网Ipv4地址
		/// </summary>
		/// <returns></returns>
		public static IPAddress GetLocalHostInnerIpv4()
		{
			IPHostEntry hostEntry = Dns.GetHostEntry(Dns.GetHostName());
			foreach (var item in hostEntry.AddressList)
			{
				if (item.AddressFamily == System.Net.Sockets.AddressFamily.InterNetwork)
				{
					return item;
				}
			}
			return null;
		}
	}
}
