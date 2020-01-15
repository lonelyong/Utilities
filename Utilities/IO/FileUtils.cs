using System;
using System.Collections.Generic;
using System.Text;
using System.IO;
using System.Linq;

namespace Utilities.IO
{
	public static class FileUtils
	{
	    public static void AppendText(string path, string content, Encoding encoding = null)
        {
            encoding = encoding ?? Encoding.UTF8;
            using (var fs = new FileStream(path, FileMode.Append, FileAccess.Write, FileShare.Read))
            {
                var bytes = encoding.GetBytes(content);
                fs.Write(bytes, 0, bytes.Length);
            }
        }

        public static void PrependText(string path, string content, Encoding encoding = null)
        {
            encoding = encoding ?? Encoding.UTF8;
            using (var fs = new FileStream(path, FileMode.Create, FileAccess.Write, FileShare.Read))
            {
                var oldContent = new byte[fs.Length];
                if(fs.Length > 0)
                {
                    fs.Read(oldContent, 0, oldContent.Length);
                }
                var bytes = encoding.GetBytes(content);
                fs.Write(bytes, 0, bytes.Length);
                if (oldContent.Any())
                {
                    fs.Write(oldContent, 0, oldContent.Length);
                }
            }
        }
	}
}
