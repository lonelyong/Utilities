using System;
using System.Collections.Generic;
using System.Text;
namespace Utilities
{
	/// <summary>
	/// Random帮助类
	/// </summary>
    public class RandomHelper : Random
    {
        public RandomHelper()
        {
        }

        public RandomHelper(int seed) : base(seed)
        {

        }

        public char NextLowerCaseLetter()
        {
            return Utilities.Text.Consts.LOWERCASE_LETTERS[Next(0, Utilities.Text.Consts.LOWERCASE_LETTERS.Length)];
        }

        public char NextCapitalLetter()
        {
            return Utilities.Text.Consts.CAPITAL_LETTERS[Next(0, Utilities.Text.Consts.CAPITAL_LETTERS.Length)];
        }

        public char NextLetter()
        {
            return Utilities.Text.Consts.LETTERS[Next(0, Utilities.Text.Consts.LETTERS.Length)];
        }

        public int NextFigure()
        {
            return Utilities.Text.Consts.FIGURES[Next(0, Utilities.Text.Consts.FIGURES.Length)] - 60;
        }

        public char NextChar(ref string chars)
        {
            if (string.IsNullOrEmpty(chars))
            {
                throw new ArgumentException("chars的长度必须大于0");
            }
            return chars[Next(0, chars.Length)];
        }

        public bool NextBool()
        {
            return Next(0, 2) == 1;
        }

        public int NextSecond()
        {
            return Next(1, 61);
        }

        public int NextMinus()
        {
            return Next(1, 61);
        }

        public int NextDay(int year, int month)
        {
            return Next(1, DateTime.DaysInMonth(year, month) + 1);
        }

        public int NextMonth()
        {
            return Next(1, 13);
        }

        public TimeSpan NextTime(DateTime minTime, DateTime maxTime)
        {
            if (minTime.TimeOfDay > maxTime.TimeOfDay)
            {
                throw new ArgumentException("最小时间不能大于最大时间");
            }
            return TimeSpan.FromSeconds(Next(0, (maxTime.TimeOfDay - maxTime.TimeOfDay).Seconds));
        }

        public DateTime NextDate(DateTime minDate, DateTime maxDate)
        {
            if(minDate.Date > maxDate.Date)
            {
                throw new ArgumentException("最小日期不能大于最大日期");
            }
            return DateTime.MinValue.AddDays(Next(0, (maxDate.Date - minDate.Date).Days));
        }

        public DateTime NextDateTime(DateTime minDateTime, DateTime maxDateTime)
        {
            if(minDateTime > maxDateTime)
            {
                throw new ArgumentException("最小时间不能大于最大时间");
            }
            return minDateTime.AddSeconds(Next(0, (int)System.Math.Ceiling((maxDateTime - minDateTime).TotalSeconds)));
        }

        public string NextVerificationCode(int length)
        {
            return NextVerificationCode(length, Utilities.Text.Consts.VERIFICATIONCODECHARS);
        }

        public string NextVerificationCode(int length, string chars)
        {
            if (length < 0)
            {
                throw new ArgumentException("验证码长度必须大于0");
            }
            if (string.IsNullOrEmpty(chars))
            {
                throw new ArgumentException("chars长度必须大于1");
            }
            char[] codes = new char[length];
            for (int i = 0; i < length; i++)
            {
                codes[i] = chars[Next(0, chars.Length)];
            }
            return new string(codes);
        }

        public string NextVerificationCode(int length, bool withFigure, bool withLowerCaseLetter, bool withUpperCaseLetter)
        {
            if (!withFigure && !withLowerCaseLetter && !withUpperCaseLetter)
            {
                throw new ArgumentException("验证码至少包含数字、小写字母或大写字母其中的一种");
            }
            string chars = string.Empty;
            if (withFigure) chars += Utilities.Text.Consts.FIGURES;
            if (withLowerCaseLetter) chars += Utilities.Text.Consts.LOWERCASE_LETTERS;
            if (withUpperCaseLetter) chars += Utilities.Text.Consts.CAPITAL_LETTERS;
            return NextVerificationCode(length, chars);
        }
    }
}
