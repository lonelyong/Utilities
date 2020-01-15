using System;
using System.Collections.Generic;
using System.Text;

namespace Utilities.Collections
{
    public static class CollectionExtensions
    {
        public static IList<T> AddReturn<T>(this IList<T> list, T item)
        {
            list.Add(item);
            return list;
        }

        public static IList<T> AddRangeReturn<T>(this IList<T> list, IEnumerable<T> ts)
        {
            foreach (var item in ts)
            {
                list.Add(item);
            }
            return list;
        }

        public static IList<T> RemoveAtReturn<T>(this IList<T> list, int index)
        {
            list.RemoveAt(index);
            return list;
        }

        public static IList<T> RemoveReturn<T>(this IList<T> list, T item)
        {
            list.Remove(item);
            return list;
        }
    }
}
