using System;
using System.Collections.Generic;
using System.Text;

namespace NgNet
{
	[Flags]
	public enum PlatformKinds : int
	{
		Windows = 1,
		UnixLike = 2 
	}
}
