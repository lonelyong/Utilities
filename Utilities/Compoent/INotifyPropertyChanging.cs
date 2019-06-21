using System;
using System.Collections.Generic;
using System.Text;

namespace Utilities.Compoent
{
    interface INotifyPropertyChanging
    {
        event PropertyChangingEventHandler PropertyChanging;
    }
}
