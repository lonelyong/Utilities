using System;
using System.Drawing;
using System.Security.Cryptography;

namespace Utilities.Drawing
{
	public class IdentifyingCodeGenerator
	{
		//#region private fields
		//private int _length = 4;

		//private Font _font = new Font("Arial", FONT_WIDTH, FontStyle.Bold | FontStyle.Italic);

		//private const string PATTERN = Text.Consts.BaseFigures + Text.Consts.CapitalEnglishLetter + Text.Consts.EnglishLetters;

		//private static byte[] _randByts = new byte[4];

		//private static RNGCryptoServiceProvider _rand = new RNGCryptoServiceProvider();

		//private Font[] _fonts =
		//{
		//	new Font(new FontFamily("Times New Roman"),Next(12, 14),FontStyle.Regular),
		//	new Font(new FontFamily("Georgia"), Next(12, 14),FontStyle.Regular | FontStyle.Italic),
		//	new Font(new FontFamily("Arial"), Next(12, 14),FontStyle.Bold),
		//	new Font(new FontFamily("Comic Sans MS"), Next(12, 14),FontStyle.Italic),
		//	new Font(new FontFamily("Simsun"), Next(12, 14), FontStyle.Bold| FontStyle.Italic)
		//};
		//#endregion

		//#region public fields
		//public const int MINLENGTH = 4;

		//public const int MAXLENGTH = 8;

		//public const int HEIGHT = 24;

		//public const int FONT_WIDTH = 16;

		//public const int PERFONT_DOTCOUNT = 6;

		//public const int SUM_LINECOUNT = 6;

		//public event EventHandler LengthChanged;
		//#endregion

		//#region public properties
		//public int Length
		//{
		//	get
		//	{
		//		return _length;
		//	}
		//	set
		//	{
		//		if (value > MAXLENGTH || value < MINLENGTH)
		//			throw new Exception($"{nameof(Length)}不能大于{MAXLENGTH}且不能小于{MINLENGTH}");
		//		_length = value;
		//		LengthChanged?.Invoke(this, new EventArgs());
		//	}
		//}
		//#endregion

		//#region private methods
		//private int GetWidth(string code)
		//{
		//	return code.Length * 16;
		//}

		//private static int Next(int max)
		//{
		//	IdentifyingCodeGenerator._rand.GetBytes(IdentifyingCodeGenerator._randByts);
		//	int value = BitConverter.ToInt32(IdentifyingCodeGenerator._randByts, 0);
		//	value %= max + 1;
		//	if (value < 0)
		//	{
		//		value = -value;
		//	}
		//	return value;
		//}

		//private static int Next(int min, int max)
		//{
		//	return Next(max - min) + min;
		//}
		//#endregion

		//#region public methods
		//public string CreateCode()
		//{
		//	string result = "";
		//	Random random = new Random(~unchecked((int)DateTime.Now.Ticks));
		//	for (int i = 0; i < Length; i++)
		//	{
		//		int rnd = random.Next(0, PATTERN.Length);
		//		result += PATTERN[rnd];
		//	}
		//	return result;
		//}

		//public Bitmap CreateCodePicture(string code)
		//{
		//	int int_ImageWidth = code.Length * FONT_WIDTH;
		//	Bitmap _bitmap = new Bitmap(int_ImageWidth, HEIGHT);
		//	Graphics g = Graphics.FromImage(_bitmap);
		//	g.Clear(Color.White);
		//	for (int i = 0; i < SUM_LINECOUNT; i++)
		//	{
		//		int x1 = Next(_bitmap.Width - 1);
		//		int x2 = Next(_bitmap.Width - 1);
		//		int y1 = Next(_bitmap.Height - 1);
		//		int y2 = Next(_bitmap.Height - 1);
		//		g.DrawLine(new Pen(GetRandomColor()), x1, y1, x2, y2);
		//	}
		//	int _x = -12, _y = 0;
		//	for (int i = 0; i < code.Length; i++)
		//	{
		//		_x += Next(12, 16);
		//		_y = Next(-2, 2);
		//		string _strChar = code[i].ToString();
		//		_strChar = Next(1) == 1 ? _strChar.ToLower() : _strChar.ToUpper();
		//		Brush _brush = new SolidBrush(GetRandomColor());
		//		Point _pot = new Point(_x, _y);
		//		g.DrawString(_strChar, _fonts[Next(_fonts.Length - 1)], _brush, _pot);
		//	}
		//	for (int i = 0; i < PERFONT_DOTCOUNT * code.Length; i++)
		//	{
		//		int x = Next(_bitmap.Width - 1);
		//		int y = Next(_bitmap.Height - 1);
		//		_bitmap.SetPixel(x, y, Color.FromArgb(Next(0, 255), Next(0, 255), Next(0, 255)));
		//	}
		//	_bitmap = TwistImage(_bitmap, true, Next(1, 3), Next(4, 6));
		//	g.DrawRectangle(new Pen(Color.LightGray, 1), 0, 0, int_ImageWidth - 1, (FONT_WIDTH - 1));
		//	return _bitmap;
		//}

		///// <summary>
		///// 字体随机颜色
		///// </summary>
		//public Color GetRandomColor()
		//{
		//	Random _first = new Random((int)DateTime.Now.Ticks);
		//	Random _second = new Random((int)DateTime.Now.Ticks + Next(100000));
		//	int int_Red = _first.Next(180);
		//	int int_Green = _second.Next(180);
		//	int int_Blue = (int_Red + int_Green > 300) ? 0 : 400 - int_Red - int_Green;
		//	int_Blue = (int_Blue > 255) ? 255 : int_Blue;
		//	return Color.FromArgb(int_Red, int_Green, int_Blue);
		//}

		///// <summary>
		///// 正弦曲线Wave扭曲图片
		///// </summary>
		///// <param name="srcBmp">要扭曲的图片</param>
		///// <param name="bXDir">如果扭曲则选择为True</param>
		///// <param name="nMultValue">波形的幅度倍数，越大扭曲的程度越高,一般为3</param>
		///// <param name="dPhase">波形的起始相位,取值区间[0-2*PI)</param>
		//public System.Drawing.Bitmap TwistImage(Bitmap srcBmp, bool bXDir, double dMultValue, double dPhase)
		//{
		//	Bitmap destBmp = new Bitmap(srcBmp.Width, srcBmp.Height);
		//	Graphics graph = Graphics.FromImage(destBmp);
		//	graph.FillRectangle(new SolidBrush(Color.White), 0, 0, destBmp.Width, destBmp.Height);
		//	graph.Dispose();
		//	double dBaseAxisLen = bXDir ? destBmp.Height : destBmp.Width;
		//	for (int i = 0; i < destBmp.Width; i++)
		//	{
		//		for (int j = 0; j < destBmp.Height; j++)
		//		{
		//			double dx = 0;
		//			dx = bXDir ? (System.Math.PI * j) / dBaseAxisLen : (System.Math.PI * i) / dBaseAxisLen;
		//			dx += dPhase;
		//			double dy = System.Math.Sin(dx);
		//			int nOldX = 0, nOldY = 0;
		//			nOldX = bXDir ? i + (int)(dy * dMultValue) : i;
		//			nOldY = bXDir ? j : j + (int)(dy * dMultValue);
		//			Color color = srcBmp.GetPixel(i, j);
		//			if (nOldX >= 0 && nOldX < destBmp.Width
		//			 && nOldY >= 0 && nOldY < destBmp.Height)
		//			{
		//				destBmp.SetPixel(nOldX, nOldY, color);
		//			}
		//		}
		//	}
		//	srcBmp.Dispose();
		//	return destBmp;
		//}
		//#endregion
	}
}
