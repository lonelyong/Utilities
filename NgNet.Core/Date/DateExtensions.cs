using System;
using System.Collections.Generic;
using System.Text;

namespace NgNet.Date
{
	public static class DateExtensions
	{
		public static readonly DateTime _unixStartTime = new DateTime(1970, 1, 1);

		public static long ToUnixTimestamp(this DateTime dateTime)
		{
			return (long)(dateTime - _unixStartTime).TotalSeconds;
		}
	}
}
