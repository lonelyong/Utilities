using System;
using System.Collections.Generic;
using System.Text;

namespace NgNet.Text
{
    public static class RegexPatterns
    {
        public const string CHINESE_MOBILE_PHONE = @"^1[3-9]{1}\d{9}$";

        public const string CHINESE_TELEPHONE = @"^(\d{3,4}-)?\d{6,8}$";

        public const string CHINESE_ID = @"^(11|12|13|14|15|21|22|23|31|32|33|34|35|36|37|41|42|43|44|45|46|50|51|52|53|54|61|62|63|64|65|71|81|82|91)(\d{13}|\d{15}[\dx])$";

        public const string EMAIL = @"^[a-zA-Z0-9]+([.\-_][a-zA-Z0-9]+)*@[a-zA-Z0-9]+(.[a-zA-Z0-9\-]+)*.[a-zA-Z]{2,}$";

        public const string DOMAIN = @"^[a-zA-Z0-9]+(.[a-zA-Z0-9\-]+)*.[a-zA-Z]{2,}$";

        public const string IPV4 = @"^\d{1,3}(.\d{1,3}){3}$";

        public const string IPV6 = "";

        public const string URL_HTTP = @"http(s?)://[a-zA-Z0-9\-]+.";

        public const string URL_FTP = "";

        public const string INT = @"^-?\d+$";

        public const string UINT = @"^\d+$";

        public const string DOUBLE = @"^-?\d+(\.\d+)?$";

        public const string UDOUBLE = @"^\d+(\.\d+)?$";

        public const string LETTER = "^[a-zA-Z]+$";

        public const string TIME = @"^(([0-1]?[0-9])|(2[0-3])):[0-5]?[0-9]:[0-5]?[0-9]$";

        public const string DATE = @"^\d{1,4}[-/]((0?[1-9])|(1[0-2])])[-/]((0?1-9)|([1-2][0-9])|(3[0-1]))$";

        public const string DATETIME = @"^\d{1,4}[-/]((0?[1-9])|(1[0-2])])[-/]((0?1-9)|([1-2][0-9])|(3[0-1])) (([0-1]?[0-9])|(2[0-3])):[0-5]?[0-9]:[0-5]?[0-9]$";
    }
}
