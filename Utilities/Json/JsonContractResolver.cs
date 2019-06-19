using System;
using System.Collections.Generic;
using System.Text;
using System.Linq;
using Newtonsoft.Json;

namespace Utilities.Json
{
    public class JsonContractResolver : Newtonsoft.Json.Serialization.DefaultContractResolver
    {
        public PropertyNameFormat PropertyNameFormat { get; set; } = PropertyNameFormat.Original;

        protected override string ResolvePropertyName(string propertyName)
        {
            var name = base.ResolvePropertyName(propertyName);
            switch (PropertyNameFormat)
            {
                case PropertyNameFormat.Original:
                    return name;
                case PropertyNameFormat.LowerCase:
                    return name.ToLower();
                case PropertyNameFormat.UpperCase:
                    return name.ToUpper();
                case PropertyNameFormat.CamleCase:
                    return name.Substring(0, 1).ToLower() + name.Substring(1);
                default:
                    break;
            }
            return name;
        }
    }
}
