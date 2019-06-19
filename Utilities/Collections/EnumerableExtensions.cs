using System;
using System.Linq;
using System.Collections.Generic;
using System.Text;

namespace Utilities.Collections
{
	public static class EnumerableExtensions
	{
		public static Dictionary<TKey, TValue> AddOrReplace<TKey, TValue>(this Dictionary<TKey, TValue> dic, TKey key, TValue value)
		{
			if (dic.ContainsKey(key))
			{
				dic[key] = value;
			}
			else
			{
				dic.Add(key, value);
			}
			return dic;
		}

		public static bool IsNullOrEmpty<T>(this IEnumerable<T> ts)
		{
			return ts == null || !ts.Any();
		}

		public static string ToString<TKey, TValue>(this Dictionary<TKey, TValue> dic, char separoter, char separator1)
		{
			var sb = new StringBuilder();
			foreach (var item in dic)
			{
				sb.Append(item.Key).Append(separoter).Append(item.Value).Append(separator1);
			}
			sb = sb.Remove(sb.Length - 1, 1);
			return sb.ToString();
		}

		public static string ToString<TKey, TValue>(this Dictionary<TKey, TValue> dic, string separoter, string separator1)
		{
			var sb = new StringBuilder();
			foreach (var item in dic)
			{
				sb.Append(item.Key).Append(separoter).Append(item.Value).Append(separator1);
			}
			sb = sb.Remove(sb.Length - 1, 1);
			return sb.ToString();
		}

		public static IEnumerable<T> Distinct<T>(this IEnumerable<T> collection, Func<T, T, bool> comparator)
		{
			if(comparator == null)
			{
				return collection.Distinct();
			}
			var list = new List<T>();
			foreach (var item in collection)
			{
				if (list.Any(t => comparator(item, t)))
					continue;
				list.Add(item);
			}
			return list;
		}

	}
}
