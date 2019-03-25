using System;
using System.Collections.Generic;
using System.Text;
using System.IO;

namespace NgNet.IO
{
	public static class FileUtils
	{
		public static void SaveString(string str, string fullFileName, Encoding encoding)
		{
			if(str == null)
			{
				str = string.Empty;
			}

			using (var fs = new FileStream(fullFileName, FileMode.Open, FileAccess.Write, FileShare.Read))
			{
				fs.Write(encoding.GetBytes(str));
				fs.Flush();
			}
		}
	}
}
