using NgNet.Reflection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace NgNet.Net
{
    public class HttpRequestData
    {
        public static string DefaultContentType = Mimes.APPLICATION_JSON;

        public static string DefaultCharset = Encoding.UTF8.WebName;

        private static Dictionary<HttpConvertDataHanlderKey, HttpRequestConvertFormDataHanlder> _convertFormDataHanlders = new Dictionary<HttpConvertDataHanlderKey, HttpRequestConvertFormDataHanlder>();

        private static Dictionary<Type, HttpRequestConvertQueryStringHanlder> _convertQueryStringHanlders = new Dictionary<Type, HttpRequestConvertQueryStringHanlder>();

        public static void RegiserConvertFormDataHanlder(Type[] types, string[] contentTypes, HttpRequestConvertFormDataHanlder hanlder)
        {
            foreach (var type in types)
            {
                foreach (var ct in contentTypes)
                {
                    var key = new HttpConvertDataHanlderKey(type, ct);
                    if (_convertFormDataHanlders.ContainsKey(key))
                    {
                        _convertFormDataHanlders[key] = hanlder;
                    }
                    else
                    {
                        _convertFormDataHanlders.Add(key, hanlder);
                    }
                }
            }
        }

        public static void RegisterConvertQueryStringHanlder(Type[] types, HttpRequestConvertQueryStringHanlder hanlder)
        {
            if (hanlder == null)
            {
                throw new Exception("Hanlder不能为空");
            }
            foreach (var item in types)
            {
                if (item == null)
                {
                    throw new Exception("Type不能为空");
                }
                if (_convertQueryStringHanlders.ContainsKey(item))
                {
                    _convertQueryStringHanlders[item] = hanlder;
                }
                else
                {
                    _convertQueryStringHanlders.Add(item, hanlder);
                }
            }
        }

        static HttpRequestData()
        {
            RegiserConvertFormDataHanlder(new Type[] { typeof(object) }, new string[] { Mimes.APPLICATION_JSON, Mimes.TEXT_JSON }, (object formData, string contentType, string charset) =>
            {
                var json = Newtonsoft.Json.JsonConvert.SerializeObject(formData);
                return Encoding.GetEncoding(charset).GetBytes(json);
            });
            RegiserConvertFormDataHanlder(new Type[] { typeof(object) }, new string[] { Mimes.APPLICATION_XML, Mimes.TEXT_XML }, (object formData, string contentType, string charset) =>
            {
                var xml = NgNet.Xml.XmlConvert.SerializeObject(formData);
                return Encoding.GetEncoding(charset).GetBytes(xml);
            });
            RegisterConvertQueryStringHanlder(new Type[] { typeof(Dictionary<string, string>), typeof(Dictionary<string, object>) }, (object queryString) =>
            {
                if (queryString.GetType() == typeof(Dictionary<string, string>))
                {
                    var qs = (Dictionary<string, string>)queryString;
                    return string.Join("&", qs.Select(t => $"{t.Key}={t.Value}"));
                }
                else if (queryString.GetType() == typeof(Dictionary<string, object>))
                {
                    var qs = (Dictionary<string, object>)queryString;
                    return string.Join("&", qs.Select(t => $"{t.Key}={t.Value}"));
                }
                else
                {
                    throw new Exception("QueryString的类型与Hanlder不匹配");
                }
            });
        }

        private HttpRequestConvertFormDataHanlder GetConvertFormDataHanlder()
        {
            var formDataType = FormData.GetType();
            var key = new HttpConvertDataHanlderKey(formDataType, ContentType);
            HttpRequestConvertFormDataHanlder hanlder = null;
            if (_convertFormDataHanlders.ContainsKey(key))
            {
                hanlder = _convertFormDataHanlders[key];
            }
            if (hanlder == null)
            {
                var closestAncestor = formDataType.FindClosestAncestor(_convertFormDataHanlders.Where(t => ContentType.Contains(t.Key.ContentType, StringComparison.OrdinalIgnoreCase)).Select(t => t.Key.Type));
                if (closestAncestor != null)
                {
                    key = new HttpConvertDataHanlderKey(closestAncestor, ContentType);
                    hanlder = _convertFormDataHanlders[key];
                }
            }
            if (hanlder == null)
            {
                throw new Exception($"未注册ContentType为{ContentType}，DataType为{formDataType.FullName}的序列化Hanlder");
            }
            return hanlder;
        }

        private HttpRequestConvertQueryStringHanlder GetConvertQueryStringHanlder()
        {
            var queryStringDataType = QueryString.GetType();
            HttpRequestConvertQueryStringHanlder hanlder = null;
            if (_convertQueryStringHanlders.ContainsKey(queryStringDataType))
            {
                hanlder = _convertQueryStringHanlders[queryStringDataType];
            }
            if (hanlder == null)
            {
                var closestAncestor = queryStringDataType.FindClosestAncestor(_convertQueryStringHanlders.Keys);
                if (closestAncestor != null)
                {
                    hanlder = _convertQueryStringHanlders[closestAncestor];
                }
            }
            if (hanlder == null)
            {
                throw new Exception($"未注册DataType为{queryStringDataType.FullName}的QueryString序列化Hanlder");
            }
            return hanlder;
        }

        public object FormData { get; set; }

        public object QueryString { get; set; }

        public string ContentType { get; set; } = DefaultContentType;

        public List<string> AcceptContentType { get; set; } = new List<string>();

        public string Charset { get; set; } = DefaultCharset;

        public byte[] GetByteFormData()
        {
            return FormData == null ? null : GetConvertFormDataHanlder().Invoke(FormData, ContentType, Charset);
        }

        public string GetQueryString()
        {
            return QueryString == null ? null : GetConvertQueryStringHanlder().Invoke(QueryString);
        }

        public void CombineQueryString(ref string url)
        {
            url = $"{url}?{GetQueryString()}";
        }

    }
}
