using System;
using System.Collections.Generic;
using System.Text;
using System.IO;
using System.Reflection;
using System.Xml;
using Utilities.IO;
using System.ComponentModel;

namespace Utilities.Configuration
{
	public abstract class ConfigBase : ICloneable, INotifyPropertyChanged
	{
		#region fields & properties
		private ConfigBase _oldConfig;

		private object _lockObj { get; } = new object();

		public bool InUpdating { get; private set; }

		public ConfigAttribute ConfigAttribute { get; }
		#endregion

		#region events
		public event EventHandler ConfigChanged;

        public event PropertyChangingEventHandler PropertyChanging;

        public event PropertyChangedEventHandler PropertyChanged;
        #endregion

        #region constructor
        public ConfigBase()
        {
            ConfigAttribute = this.GetType().GetCustomAttribute(typeof(ConfigAttribute), false) as ConfigAttribute;
            if (ConfigAttribute == null)
            {
                throw new Exception($"配置类必须具有{nameof(ConfigAttribute)}特性");
            }
        }
        #endregion

        #region methods
        protected void OnPropertyChanging(PropertyChangingEventArgs e) {
            PropertyChanging?.Invoke(this, e);
		}

        protected virtual void OnPropertyChanged(PropertyChangedEventArgs e)
        {
            PropertyChanged?.Invoke(this, e);
        }

        protected virtual void OnConfigChanged(EventArgs e)
        {
            ConfigChanged?.Invoke(this, e);
        }

		protected virtual bool IsValueChanged(object newValue, object oldValue) {
			return newValue == oldValue;
		}

		public abstract object Clone();

		public void BeginUpdate() {
			if (InUpdating)
			{
				return;
			}
			InUpdating = true;
			_oldConfig = (ConfigBase)Clone();
		}

		public void EndUpdate() {
			if (!InUpdating) {
				return;
			}
			InUpdating = false;
            OnConfigChanged(new EventArgs());
		}

		public string ToJson()
		{
			return Newtonsoft.Json.JsonConvert.SerializeObject(this);
		}

		public string ToJson(Newtonsoft.Json.JsonSerializerSettings jsonSerializerSettings) {
			return Newtonsoft.Json.JsonConvert.SerializeObject(this, jsonSerializerSettings);
		}

		void Write(DirectoryInfo directory, string content, Encoding encoding, string ext)
		{
			if (!directory.Exists)
			{
				directory.Create();
			}
			directory = new DirectoryInfo(directory.FullName + ConfigAttribute.AbsoluteDirectory);
			if (!directory.Exists)
			{
				directory.Create();
			}
			var fileInfo = new FileInfo(directory.FullName + ConfigAttribute.ConfigName + ext);
			File.WriteAllText(fileInfo.FullName, ToJson(), encoding);
		}

		public void WriteJson(string directory)
		{
			WriteJson(directory, Encoding.UTF8);
		}

		public void WriteJson(string directory, Encoding encoding)
		{
			 WriteJson(new DirectoryInfo(directory), encoding);
		}

		public void WriteJson(DirectoryInfo directory, Encoding encoding)
		{
			Write(directory, ToJson(), encoding, ExtensionConsts.JSON);
		}

		public string ToXml()
		{
			return Xml.XmlConvert.SerializeObject(this);
		}

		public void WriteXml(string directory)
		{
			WriteXml(directory, Encoding.UTF8);
		}

		public void WriteXml(string directory, Encoding encoding)
		{
			WriteXml(new DirectoryInfo(directory), encoding);
		}

		public void WriteXml(DirectoryInfo directory, Encoding encoding)
		{
			Write(directory, ToXml(), encoding, ExtensionConsts.XML);
		}

		void InnerFrom(ConfigBase config)
		{
			BeginUpdate();
			From(config);
			EndUpdate();
		}

		public abstract void From(ConfigBase other);

		public void FromJson(string json)
		{
			var config = (ConfigBase)Newtonsoft.Json.JsonConvert.DeserializeObject(json, this.GetType());
			InnerFrom(config);
		}

		public void FromXml(string xml)
		{
			var config = (ConfigBase)Xml.XmlConvert.DeserializeObject(xml, this.GetType());
			InnerFrom(config);
		}

		public void ReadJson(string directory) {
			ReadJson(directory, Encoding.UTF8);
		}

		public void ReadJson(string directory, Encoding encoding)
		{
			ReadJson(new DirectoryInfo(directory), encoding);
		}

		public void ReadJson(DirectoryInfo directory, Encoding encoding)
		{
			var fileInfo = new FileInfo(Path.Combine(directory.FullName, ConfigAttribute.AbsoluteDirectory, ConfigAttribute.ConfigName + ExtensionConsts.JSON));
			FromJson(File.ReadAllText(fileInfo.FullName, encoding));
		}

		public void ReadXml(string directory)
		{
			ReadXml(directory, Encoding.UTF8);
		}

		public void ReadXml(string directory, Encoding encoding)
		{
			ReadXml(new DirectoryInfo(directory), encoding);
		}

		public void ReadXml(DirectoryInfo directory, Encoding encoding)
		{
			var fileInfo = new FileInfo(Path.Combine(directory.FullName, ConfigAttribute.AbsoluteDirectory, ConfigAttribute.ConfigName + ExtensionConsts.XML));
			FromXml(File.ReadAllText(fileInfo.FullName, encoding));
		}

		public static TConfig ParseJson<TConfig>(string josn) where TConfig : ConfigBase, new()
		{
			return Newtonsoft.Json.JsonConvert.DeserializeObject<TConfig>(josn);
		}

		public static TConfig ParseJson<TConfig>(string josn, Newtonsoft.Json.JsonSerializerSettings jsonSerializerSettings) where TConfig : ConfigBase, new()
		{
			return Newtonsoft.Json.JsonConvert.DeserializeObject<TConfig>(josn, jsonSerializerSettings);
		}

		public static bool TryParseJson<TConfig>(string josn, out TConfig config) where TConfig : ConfigBase, new()
		{
			try
			{
				config = ParseJson<TConfig>(josn);
				return true;
			}
			catch
			{

				throw;
			}
		}

		public static bool TryParseJson<TConfig>(string josn, Newtonsoft.Json.JsonSerializerSettings jsonSerializerSettings, out TConfig config) where TConfig : ConfigBase, new()
		{
			try
			{
				config = ParseJson<TConfig>(josn, jsonSerializerSettings);
				return true;
			}
			catch
			{

				throw;
			}
		}

		public static TConfig ParseXml<TConfig>(string xml) where TConfig : ConfigBase, new()
		{
			return Xml.XmlConvert.DeserializeObject<TConfig>(xml);
		}

		public static bool TryParseXml<TConfig>(string xml, out TConfig config) where TConfig : ConfigBase, new()
		{
			try
			{
				config = ParseXml<TConfig>(xml);
				return true;
			}
			catch
			{

				throw;
			}
		}
		#endregion
	}
}
