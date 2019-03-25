using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Reflection;
using System.Text;

namespace NgNet.Reflection
{
	public static class RuntimeUtils
	{
		/// <summary>
		/// 获取指定对象的指定名称的私有字段的值
		/// </summary>
		/// <typeparam name="T"></typeparam>
		/// <param name="instance"></param>
		/// <param name="fieldname"></param>
		/// <returns></returns>
		public static T GetPrivateFieldValue<T>(this object instance, string fieldname)
		{
			return (T)instance.GetType().GetField(fieldname, BindingFlags.Instance | BindingFlags.NonPublic).GetValue(instance);
		}
		/// <summary>
		/// 获取调用此方法的方法
		/// </summary>
		/// <returns></returns>
		public static MethodBase GetCaller()
		{
			StackTrace _st = new StackTrace();
			return _st.GetFrame(0).GetMethod();
		}
		/// <summary>
		/// 自下而上获取具有指定标记的方法体
		/// </summary>
		/// <param name="typeOfAttribute"></param>
		/// <returns></returns>
		public static MethodBase GetCaller(Type typeOfAttribute)
		{
			StackTrace _st = new StackTrace();
			foreach (StackFrame item in _st.GetFrames())
			{
				if (item.GetMethod().GetCustomAttribute(typeOfAttribute) == null)
					continue;
				else
					return item.GetMethod();
			}
			return null;
		}
	}
}
