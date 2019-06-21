using System;
using System.Collections.Generic;
using System.Text;

namespace Utilities.Compoent
{
    public class PropertyChangingEventArgs : EventArgs
    {
        public object OldValue { get; }

        public object NewValue { get; }

        public string ConfigName { get; }

        public bool Cancel { get; set; }

        public PropertyChangingEventArgs(string configName, object newValue, object oldValue)
        {
            OldValue = oldValue;
            NewValue = newValue;
            ConfigName = configName;
        }
    }

    public delegate void PropertyChangingEventHandler(object sender, PropertyChangingEventArgs e);
}
