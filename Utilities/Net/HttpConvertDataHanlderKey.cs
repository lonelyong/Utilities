using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Utilities.Net
{
    internal class HttpConvertDataHanlderKey : IEqualityComparer<HttpConvertDataHanlderKey>
    {
        private string _baseContentType;

        public Type Type { get; }

        public string ContentType { get; }

        public HttpConvertDataHanlderKey(Type type, string contentType)
        {
            if (string.IsNullOrWhiteSpace(contentType))
            {
                throw new Exception("ContentType不能为空");
            }
            Type = type ?? throw new Exception("Type不能为空");
            ContentType = contentType;
            var cts = ContentType.Split(new char[] { ';' }, StringSplitOptions.RemoveEmptyEntries);
            if (cts.Where(t => !t.TrimStart().StartsWith("charset")).Count() > 1)
            {
                throw new Exception("ContentType包含多个值");
            }
            _baseContentType = cts.First();
        }

        public bool Equals(HttpConvertDataHanlderKey x, HttpConvertDataHanlderKey y)
        {
            return x == y;
        }

        public int GetHashCode(HttpConvertDataHanlderKey obj)
        {
            return obj._baseContentType.GetHashCode() + obj.Type.GetHashCode();
        }

        public override int GetHashCode()
        {
            return GetHashCode(this);
        }

        public override bool Equals(object obj)
        {
            return Equals(this, obj as HttpConvertDataHanlderKey);
        }

        public static bool operator ==(HttpConvertDataHanlderKey left, HttpConvertDataHanlderKey right)
        {
            if ((object)left == null && (object)right == null)
            {
                return true;
            }
            else if ((object)left == null || (object)right == null)
            {
                return false;
            }
            else
            {
                return left.Type == right.Type && left._baseContentType.Equals(right._baseContentType, StringComparison.OrdinalIgnoreCase);
            }
        }

        public static bool operator !=(HttpConvertDataHanlderKey left, HttpConvertDataHanlderKey right)
        {
            return !(left == right);
        }
    }
}
