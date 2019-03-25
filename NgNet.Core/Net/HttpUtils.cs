using System;
using System.Collections.Generic;
using System.Net;
using System.Text;
using System.Linq;
using NgNet.Reflection;
using System.Collections.Specialized;
using System.IO;

namespace NgNet.Net
{
    public static class HttpUtils
    {
        private const string AJAX_HEADER = "";

        private static HttpMethod[] _formableHttpMethods = new HttpMethod[] { HttpMethod.POST, HttpMethod.PUT };

        private static HttpMethod[] _nonformableHttpMethods = new HttpMethod[] { HttpMethod.GET, HttpMethod.OPTION, HttpMethod.DELETE };

        public static HttpReponseData<TResult> SendRequest<TResult>(string url, HttpMethod method, HttpRequestData data, bool useAjax = false) where TResult : class
        {
            HttpReponseData<TResult> result;
            if (data?.QueryString != null)
            {
                data.CombineQueryString(ref url);
            }
            var req = WebRequest.CreateHttp(url);
            req.Method = method.ToString();
            if (useAjax) req.UseAjax();
            if (_formableHttpMethods.Contains(method))
            {
                if (data?.FormData != null)
                {
                    req.AddHeader(HttpRequestHeader.ContentType, data.ContentType);
                    byte[] bytData = data.GetByteFormData();
                    if (bytData != null)
                    {
                        using (var stream = req.GetRequestStreamAsync().Result)
                        {
                            stream.Write(bytData, 0, bytData.Length);
                        }
                    }
                }
            }
            try
            {
                using (var rep = (HttpWebResponse)req.GetResponseAsync().Result)
                {
                    using (var stream = rep.GetResponseStream())
                    {
                        var byt = stream.ReadByte();
                        var bytes = new List<byte>();
                        while ( byt != -1)
                        {
                            bytes.Add((byte)byt);
                            byt = stream.ReadByte();
                        }
                        result = new HttpReponseData<TResult>(bytes.ToArray(), rep.ContentType, rep.CharacterSet, rep.StatusCode) { };
                    }
                }
            }
            catch (WebException ex)
            {
                if (ex.Response == null)
                {
                    throw ex;
                }
                using (var rep = ex.Response as HttpWebResponse)
                {
                    result = new HttpReponseData<TResult>(null, rep.ContentType, rep.CharacterSet, rep.StatusCode);
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
            finally
            {
                req?.Abort();
            }
            return result;
        }

        public static HttpWebRequest AddHeader(this HttpWebRequest request, string name, string value)
        {
            request.Headers.Add(name, value);
            return request;
        }

        public static HttpWebRequest AddHeader(this HttpWebRequest request, HttpRequestHeader header, string value)
        {
            request.Headers.Add(header, value);
            return request;
        }

        public static HttpWebRequest AddHeaders(this HttpWebRequest request, NameValueCollection headers)
        {
            if (headers != null)
            {
                foreach (var item in headers.AllKeys)
                {
                    request.Headers.Add(item, headers[item]);
                }
            }
            return request;
        }

        public static HttpWebRequest AddHeaders(this HttpWebRequest request, Dictionary<HttpRequestHeader, string> headers)
        {
            if (headers != null)
            {
                foreach (var item in headers)
                {
                    request.Headers.Add(item.Key, item.Value);
                }
            }
            return request;
        }

        public static bool IsAjax(this HttpWebRequest request)
        {
            return string.Compare(request.Headers[HttpHeaders.X_REQUEST_WITH], AJAX_HEADER, true) == 0;
        }

        public static HttpWebRequest UseAjax(this HttpWebRequest request)
        {
            if (!request.IsAjax())
            {
                request.Headers.Add(HttpHeaders.X_REQUEST_WITH, AJAX_HEADER);
            }
            return request;
        }
    }

}