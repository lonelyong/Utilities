using System;
using System.Collections.Generic;
using System.Text;

namespace Utilities.Configuration
{
	public class ConfigChangedEventArgs : EventArgs
	{
		public ConfigBase NewConfig { get; }

		public ConfigBase OldConfig { get; }

		public ConfigChangedEventArgs(ConfigBase newConfig, ConfigBase oldConfig) {
			NewConfig = newConfig;
			OldConfig = oldConfig;
		}
	}

	public delegate void ConfigChangedEventHandler(object sender, ConfigChangedEventArgs e);
}
