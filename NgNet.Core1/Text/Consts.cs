using System;
using System.Collections.Generic;
using System.Text;

namespace NgNet.Text
{
    public static class Consts
    {
        public const string FIGURES = "0123456789";

        public const string LOWERCASE_LETTERS = "abcdefghijklmnopqrstuvwxyz";

        public const string CAPITAL_LETTERS = "ABCDEFGHIJKLMNOPQRSTUVWXYZ";

        public const string LETTERS = LOWERCASE_LETTERS + CAPITAL_LETTERS;

        public const string CHINESE_FIGURES = "零一二三四五六七八九十百千万亿";

        public const string CAPITAL_CHINESE_FIGURES = "零壹贰叁肆伍陆柒捌玖佰仟万亿";

        public const string VERIFICATIONCODECHARS = LETTERS + FIGURES; 

        public const string BASE64_CHARATERS = "";
    }
}
