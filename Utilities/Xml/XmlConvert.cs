using System;
using System.Collections.Generic;
using System.Data;
using System.IO;
using System.Text;
using System.Xml;
using System.Xml.Serialization;

namespace Utilities.Xml
{
    public  static class XmlConvert
    {
        private static XmlSerializerFactory _xmlSerializerFactory = new XmlSerializerFactory();

        public static string SerializeObject<T>(T obj)
        {
            var xmlSerializer = _xmlSerializerFactory.CreateSerializer(typeof(T));
            using (var memStream = new MemoryStream())
            {
                xmlSerializer.Serialize(memStream, obj);
                memStream.Position = 0;
                using (var sr = new StreamReader(memStream))
                {
                    return sr.ReadToEnd();
                }
            }
        }

        public static T DeserializeObject<T>(string xml)
        {
            return (T)DeserializeObject(xml, typeof(T));
        }

        public static object DeserializeObject(string xml, Type type)
        {
            using (var sr = new StringReader(xml))
            {
                var xmlSerializer = _xmlSerializerFactory.CreateSerializer(type);
                return xmlSerializer.Deserialize(sr);
            }
        }

        public static DataTable DeserializeDataTable(string xml)
        {
            DataTable dt = new DataTable();


            return dt;
        }

        public static string SerializeDataTable(DataTable dt)
        {
            return string.Empty;
        }
    }
}
