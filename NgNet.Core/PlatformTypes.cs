using System;
using System.Collections.Generic;
using System.Text;

namespace NgNet
{
    [Flags]
    public enum PlatformTypes : int
    {
        Windows = 1,
        Linux = 2,
        Unix = 4,
        Mac = 8,
        Android = 16,
        IOS = 32
    }
}
