using Utilities.Reflection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;

namespace Utilities.Net
{
    public class HttpReponseData<TResult> where TResult : class
    {
        private static Dictionary<HttpConvertDataHanlderKey, HttpReponseConvertDataHanlder> _convertHanlders = new Dictionary<HttpConvertDataHanlderKey, HttpReponseConvertDataHanlder>();

        static HttpReponseData()
        {
            RegisterConverter(new Type[] { typeof(object) }, new string[] { Mimes.APPLICATION_XML, Mimes.TEXT_XML }, (Type type, byte[] data, string contentType, string charset) =>
            {
                return Xml.XmlConvert.DeserializeObject(Encoding.GetEncoding(charset).GetString(data), type);
            });
            RegisterConverter(new Type[] { typeof(object) }, new string[] { Mimes.APPLICATION_JSON, Mimes.TEXT_JSON }, (Type type, byte[] data, string contentType, string charset) =>
            {
                return Newtonsoft.Json.JsonConvert.DeserializeObject(Encoding.GetEncoding(charset).GetString(data), type);
            });
            RegisterConverter(new Type[] { typeof(string) }, new string[] { Mimes.APPLICATION_JSON, Mimes.APPLICATION_XML, Mimes.TEXT_HTML, Mimes.TEXT_XML, Mimes.TEXT_JSON, Mimes.TEXT_PLAIN }, (Type type, byte[] data, string contentType, string charset) =>
            {
                return Encoding.GetEncoding(charset).GetString(data);
            });
        }

        private HttpReponseConvertDataHanlder GetConvertResultHanlder()
        {
            var key = new HttpConvertDataHanlderKey(typeof(TResult), ContentType);
            HttpReponseConvertDataHanlder hanlder = null;
            if (_convertHanlders.ContainsKey(key))
            {
                hanlder = _convertHanlders[key];
            }

            if (hanlder == null)
            {
                var closestAncestor = typeof(TResult).FindClosestAncestor(_convertHanlders.Where(t => ContentType.Contains(t.Key.ContentType, StringComparison.OrdinalIgnoreCase)).Select(t => t.Key.Type));
                if (closestAncestor != null)
                {
                    key = new HttpConvertDataHanlderKey(closestAncestor, ContentType);
                    hanlder = _convertHanlders[key];
                }
            }
            if (hanlder == null)
            {
                throw new Exception($"无法反序列化ContentType为{ContentType}的结果到{typeof(TResult).FullName}，因为未注册Hanlder");
            }
            return hanlder;
        }

        public static void RegisterConverter(Type[] types, string[] contentTypes, HttpReponseConvertDataHanlder hanlder)
        {
            if (hanlder == null)
            {
                throw new Exception("hanlder不能为空");
            }
            foreach (var type in types)
            {
                foreach (var ct in contentTypes)
                {
                    var key = new HttpConvertDataHanlderKey(type, ct);
                    if (_convertHanlders.ContainsKey(key))
                    {
                        _convertHanlders[key] = hanlder;
                    }
                    else
                    {
                        _convertHanlders.Add(key, hanlder);
                    }
                }
            }
        }

        public TResult Data { get; }

        public byte[] ByteData { get; }

        public string ContentType { get; }

        public string Charset { get; }

        public HttpStatusCode HttpStatusCode { get; set; }

        public HttpReponseData(byte[] bytData, string contentType, string charset, HttpStatusCode httpStatusCode)
        {
            ByteData = bytData;
            ContentType = contentType;
            Charset = charset;
            HttpStatusCode = httpStatusCode;
            var hander = GetConvertResultHanlder();
            Data = (TResult)hander.Invoke(typeof(TResult), ByteData, ContentType, Charset);
        }

        public bool IsRequestSuccessful
        {
            get
            {
                return (int)HttpStatusCode >= 200 && (int)HttpStatusCode < 400;
            }
        }
    }
}
