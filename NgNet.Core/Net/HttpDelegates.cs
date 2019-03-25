using System;
using System.Collections.Generic;
using System.Text;

namespace NgNet.Net
{

    public delegate byte[] HttpRequestConvertFormDataHanlder(object formData, string contentType, string charset);

    public delegate string HttpRequestConvertQueryStringHanlder(object queryString);

    public delegate object HttpReponseConvertDataHanlder(Type type, byte[] data, string contentType, string charset);
}
