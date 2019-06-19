using System;
using System.Collections.Generic;
using System.Text;

namespace Utilities.Configuration
{
	[AttributeUsage(AttributeTargets.Class, AllowMultiple = false, Inherited = false)]
	public sealed class ConfigAttribute : Attribute
	{
		public string ConfigName { get; }

		public string AbsoluteDirectory { get; }

		public string Describution { get; set; }

		public ConfigAttribute(string configName, string absolutePath) {
			ConfigName = configName;
			AbsoluteDirectory = absolutePath;
		}
	}
}
