using System;
using System.Collections.Generic;
using System.Text;

namespace NgNet.Net
{
    public static class NetUtils
    {
        public static bool IsIPv4(string ipv4)
        {
            return NgNet.Text.RegexUtils.IsIPv4(ipv4); 
        }
    }
}
