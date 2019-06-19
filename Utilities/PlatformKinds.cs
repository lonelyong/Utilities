using System;
using System.Collections.Generic;
using System.Text;

namespace Utilities
{
	[Flags]
	public enum PlatformKinds : int
	{
		Windows = 1,
		UnixLike = 2 
	}
}
