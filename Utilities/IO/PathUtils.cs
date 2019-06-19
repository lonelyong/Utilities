using System;
using System.Collections.Generic;
using System.Text;
using System.IO;

namespace Utilities.IO
{
    public static class PathUtils
    {
        public static bool IsValidPath(string path, PlatformKinds platformKind)
        {
            return true;
        }

        public static bool IsValidName(string name, PlatformKinds platformKind)
        {
            return true;
        }

		public static void Delete(string path)
		{
			if (File.Exists(path))
			{
				File.Delete(path);
			}
			else if (Directory.Exists(path))
			{
				Directory.Delete(path);
			}
			else
			{
				throw new FileNotFoundException("要删除的文件（夹）不存在", path);
			}
		}

		public static bool TryDelete(string path, bool ingoreNotFound = false)
		{
			try
			{
				if (File.Exists(path))
				{
					File.Delete(path);
				}
				else if (Directory.Exists(path))
				{
					Directory.Delete(path);
				}
				else if(!ingoreNotFound)
				{
					return false;
				}
				else
				{
					return false;
				}
				return true;
			}
			catch
			{
				return false;
			}
		}

		public static bool Exists(string path)
		{
			return File.Exists(path) || Directory.Exists(path);
		}
    }
}
