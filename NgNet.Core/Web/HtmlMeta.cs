using System;
using System.Collections.Generic;
using System.Text;
using NgNet.Collections;

namespace NgNet.Web
{
	public class HtmlMeta
	{
		const string _HTTP_EQUIV = "http-equiv";
		const string _CONTENT = "CONTENT";
		const string _NAME = "name";

		private Dictionary<string, string> _metaDic = new Dictionary<string, string>();

		public HtmlMeta AddHttpEquiv(string httpHquiv, string content)
		{
			_metaDic.AddOrReplace(_HTTP_EQUIV, httpHquiv).AddOrReplace(_CONTENT, content);
			return this;
		}

		public HtmlMeta AddName(string name, string content)
		{
			_metaDic.AddOrReplace(_NAME, name).AddOrReplace(_CONTENT, content);
			return this;
		}

		public HtmlMeta Add(string key, string value)
		{
			_metaDic.AddOrReplace(key, value);
			return this;
		}

		public override string ToString()
		{
			var sb = new StringBuilder();
			sb.Append('<');
			foreach (var item in _metaDic)
			{
				sb.Append(' ').Append(item.Key).Append("=\"").Append(item.Value).Append("\"");
			}
			sb.Append('>');
			return sb.ToString();
		}
	}
}
