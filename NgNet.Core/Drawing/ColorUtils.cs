using System;
using System.Collections.Generic;
using System.Drawing;
using System.Text;

namespace NgNet.Drawing
{
	class ColorUtils
	{        
		/// <summary>
		/// 获取与指定颜色相近的颜色
		/// </summary>
		/// <param name="c">指定的颜色</param>
		/// <param name="deep">True:加深 False:变浅</param>
		/// <param name="level">指定的加深或变浅的程度</param>
		/// <returns></returns>
		public static Color GetSimilarColor(Color c, bool deep, sbyte level)
		{
			float a = c.A;
			float r = c.R;
			float g = c.G;
			float b = c.B;
			int l = level;
			int lm = sbyte.MaxValue;
			if (deep)
			{
				//a -= (byte)(a / lm);
				r -= r / lm * l;
				g -= g / lm * l;
				b -= b / lm * l;
			}
			else
			{
				//a += (byte)((byte.MaxValue - a) / lm);
				r += (byte.MaxValue - r) / lm * l;
				g += (byte.MaxValue - g) / lm * l;
				b += (byte.MaxValue - b) / lm * l;
			}
			return Color.FromArgb((byte)a, (byte)r, (byte)g, (byte)b);
		}

		/// <summary>
		/// 获取指定颜色的反差色
		/// </summary>
		/// <param name="c">指定的颜色</param>
		/// <returns></returns>
		public static Color GetOppositeColor(Color c)
		{
			byte r = (byte)(byte.MaxValue - c.R);
			byte g = (byte)(byte.MaxValue - c.G);
			byte b = (byte)(byte.MaxValue - c.B);
			return Color.FromArgb(c.A, r, g, b);
		}
	}
}
