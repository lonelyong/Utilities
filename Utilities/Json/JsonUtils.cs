using System;
using System.Collections.Generic;
using System.Text;
using System.Linq;
using Newtonsoft.Json;
using System.Dynamic;
using Newtonsoft.Json.Serialization;
namespace Utilities.Json
{
	public enum MultiObjectsSerializationOption : int
	{
		/// <summary>
		/// 后面的obj属性会替换前面的obj的同名属性
		/// </summary>
		ReplaceAll = 1,
		/// <summary>
		/// 后面的obj会替换前面obj值为null的同名属性
		/// </summary>
		ReplaceNull = 2,
		/// <summary>
		/// 
		/// </summary>
		ReplaceIngoreNull = 3,
		/// <summary>
		/// 后面的obj不会替换前面obj的同名属性
		/// </summary>
		NoReplace = 3
	}

    public static class JsonUtils
    {
        private static JsonContractResolver _lowerCasePropertyContractResolver = new JsonContractResolver() { PropertyNameFormat = PropertyNameFormat.LowerCase };

        public static string LowerCaseSerialize(object obj) 
        {
            return JsonConvert.SerializeObject(obj, new JsonSerializerSettings() { ContractResolver = _lowerCasePropertyContractResolver });
        }

        public static string SerializeObject(object obj, PropertyNameFormat propertyNameFormat)
        {
            return JsonConvert.SerializeObject(obj, new JsonSerializerSettings() { ContractResolver = new JsonContractResolver() { PropertyNameFormat = propertyNameFormat } });
        }

		public static object DeserializeObject(string json, Type type)
		{
			return JsonConvert.DeserializeObject(json, type);
		}

		public static dynamic DeserializeExpandoObject(string json)
		{
			return JsonConvert.DeserializeObject(json, typeof(ExpandoObject));
		}

		public static string SerializeObjects(MultiObjectsSerializationOption option, params object[] objs) 
		{
			const string nullJson = "{}";
			if (!objs.Any(o=>o!=null))
			{
				return nullJson;
			}
			return null;
		}
    }
}
