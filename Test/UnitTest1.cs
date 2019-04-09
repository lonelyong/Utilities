using System;
using Xunit;
using System.Collections.Generic;
namespace Test
{
    public class UnitTest1
    {
        [Fact]
        public void Test1()
        {
           var res = NgNet.Net.HttpUtils.SendRequest<string>("https://api.link.hicode.net/url/unzip", NgNet.Net.HttpMethod.GET, new NgNet.Net.HttpRequestData() {
               QueryString = new Dictionary<string, string>() { { "SLINK", "1" } }
           }, false);
        }

		[Fact]
		public void TestRsa()
		{
			var res = new NgNet.Security.RsaHelper(1024);
			var xmlKey = res.ToXmlString(true);
			Assert.StartsWith("", xmlKey);
		}
    }
}
