using System;
using System.Collections;
using System.Collections.Generic;
using System.Net;
using System.Net.NetworkInformation;
using System.Text;
using System.Linq;

namespace NgNet.Net
{
	public static class PortUtils
	{
		/// <summary>
		/// 获取正在使用的端口
		/// </summary>
		/// <returns></returns>
		public static List<IPEndPoint> GetUsedIPEndPoints()
		{
			//获取一个对象，该对象提供有关本地计算机的网络连接和通信统计数据的信息。  
			var ipGlobalProperties = IPGlobalProperties.GetIPGlobalProperties();

			//获取有关本地计算机上的 Internet 协议版本 4 (IPV4) 传输控制协议 (TCP) 侦听器的终结点信息。  
			var tcpIeps = ipGlobalProperties.GetActiveTcpListeners();

			//获取有关本地计算机上的 Internet 协议版本 4 (IPv4) 用户数据报协议 (UDP) 侦听器的信息。  
			var udpIeps = ipGlobalProperties.GetActiveUdpListeners();

			//获取有关本地计算机上的 Internet 协议版本 4 (IPV4) 传输控制协议 (TCP) 连接的信息。  
			var tcpConnectionInformation = ipGlobalProperties.GetActiveTcpConnections();

			var ieps = new List<IPEndPoint>();
			ieps.AddRange(tcpIeps);
			ieps.AddRange(udpIeps);
			ieps.AddRange(tcpConnectionInformation.Select(t=>t.LocalEndPoint));
			return ieps;
		}

		///  
		/// 判断指定的网络端点（只判断端口）是否被使用  
		///  
		public static bool IsUsedIPEndPoint(int port)
		{
			return GetUsedIPEndPoints().Any(t => t.Port == port);
		}

		///  
		/// 判断指定的网络端点（判断IP和端口）是否被使用  
		///  
		public static bool IsUsedIPEndPoint(string ip, int port)
		{
			return GetUsedIPEndPoints().Any(t => t.Port == port && t.Address.ToString().Equals(ip));
		}
		///
		/// 返回可用端口号
		///
		/// 端口开始数字
		///
		public static int GetOneUnusedPort(int startPort)
		{
			while (IsUsedIPEndPoint(startPort))
			{
				startPort++;
			}
			return startPort;
		}
		/// <summary>
		/// 获取一个值，该值指示指定的端口号是不是有效的端口号
		/// </summary>
		/// <param name="port"></param>
		/// <returns></returns>
		public static bool IsPort(int port)
		{
			return port >= 0 && port <= 65535;
		}
	}
}
