using System;
using System.Collections.Generic;
using System.Text;
using Utilities.Reflection;

namespace Utilities.Math
{
    public static class MathConvert
    {
		/// <summary>
		/// 将数字转为2-64进制的数字字符串
		/// </summary>
		/// <typeparam name="TValue"></typeparam>
		/// <param name="value"></param>
		/// <param name="toBase">2-64</param>
		/// <returns></returns>
		public static string ToString<TValue>(TValue value, int toBase) where TValue : struct
		{
			if (!typeof(TValue).IsIntergerType())
			{
				throw new ArgumentException($"{nameof(value)}不是整数数值类型");
			}
			return string.Empty;
		}
    }
}
