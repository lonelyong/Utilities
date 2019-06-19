using System;
using System.Collections.Generic;
using System.Text;

namespace Utilities.Net
{
    public enum HttpMethod : int
    {
        GET = 1,
        POST = 2,
        PUT = 3,
        DELETE = 4,
        OPTION = 8
    }
}
