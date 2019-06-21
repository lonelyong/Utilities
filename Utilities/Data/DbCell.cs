using System;
using System.Collections.Generic;
using System.Text;

namespace Utilities.Data
{
    public class DbCell
    {
        public DbColumn Column { get; set; }

        public object DbValue { get; set; }

        public object Value { get; set; }
    }
}
