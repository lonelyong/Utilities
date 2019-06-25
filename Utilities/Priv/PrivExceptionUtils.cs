using System;
using System.Collections.Generic;
using System.Text;

namespace Utilities.Priv
{
    public static class PrivExceptionUtils
    {
        internal static void ThrowIfGenericParamIsInvalid<TSpecified, TRequired>()
        {
            if(typeof(TSpecified).IsSubclassOf(typeof(TRequired)) || typeof(TSpecified) == typeof(TRequired) || typeof(TRequired).IsAssignableFrom(typeof(TSpecified)))
            {

            }
            else
            {
                throw new ArgumentException($"无法将类型{typeof(TSpecified).FullName}转换为{typeof(TRequired).FullName}");
            }
        }

        internal static void ThrowIfGenericParamIsInvalid<TSpecified>(Predicate<Type> predicate)
        {
            if (!predicate.Invoke(typeof(TSpecified)))
            {
                throw new ArgumentException($"{typeof(TSpecified).FullName}不是所需的类型");
            }
        }
    }
}
