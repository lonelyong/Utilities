using System;
using System.Collections.Generic;
using System.Text;
using NgNet.Collections;
namespace NgNet
{
	/// <summary>
	/// 对象映射工具类
	/// </summary>
	 public static class MappingUtils
	{
		/// <summary>
		/// 将源对象的属性值赋给目标对象的同名属性
		/// </summary>
		/// <typeparam name="T">目标对象类型</typeparam>
		/// <param name="sourceObjs">源对象集合</param>
		/// <returns>目标对象类型的新实例</returns>
		public static T MapPropertiesFrom<T>(params object[] sourceObjs) where T : class, new()
		{
			return new T().MapPropertiesFrom<T>(sourceObjs);
		}

		/// <summary>
		/// 将源对象的属性值赋给目标对象的同名属性
		/// </summary>
		/// <typeparam name="T">目标对象类型</typeparam>
		/// <param name="targetObj">目标对象实例</param>
		/// <param name="sourceObjs">源对象集合</param>
		/// <returns>目标对象</returns>
		public static T MapPropertiesFrom<T>(this T targetObj, params object[] sourceObjs)
		{
			if (!sourceObjs.IsNullOrEmpty())
			{
				foreach (var obj in sourceObjs)
				{
					obj.MapPropertiesTo<T>(targetObj);
				}
			}
			return targetObj;
		}

		/// <summary>
		/// 将源对象的字段值赋给目标对象的同名字段
		/// </summary>
		/// <typeparam name="T">目标对象类型</typeparam>
		/// <param name="sourceObjs">源对象集合</param>
		/// <returns>目标对象类型的新实例</returns>
		public static T MapFiledsFrom<T>(params object[] sourceObjs) where T : class, new()
		{
			return new T().MapFieldsFrom<T>(sourceObjs);
		}

		/// <summary>
		/// 将源对象的字段值赋给目标对象的同名字段
		/// </summary>
		/// <typeparam name="T">目标对象类型</typeparam>
		/// <param name="targetObj">目标对象实例</param>
		/// <param name="sourceObjs">源对象集合</param>
		/// <returns>目标对象</returns>
		public static T MapFieldsFrom<T>(this T targetObj, params object[] sourceObjs)
		{
			if (!sourceObjs.IsNullOrEmpty())
			{
				foreach (var obj in sourceObjs)
				{
					obj.MapFieldsTo<T>(targetObj);
				}
			}
			return targetObj;
		}

		/// <summary>
		/// 将源对象的属性和字段值赋给目标对象的同名属性和字段
		/// </summary>
		/// <typeparam name="T">目标对象类型</typeparam>
		/// <param name="sourceObjs">源对象集合</param>
		/// <returns>目标对象类型的新实例</returns>
		public static T MapFrom<T>(params object[] sourceObjs) where T : class, new()
		{
			return new T().MapFrom<T>(sourceObjs);
		}

		/// <summary>
		/// 将源对象的属性和字段值赋给目标对象的同名属性和字段
		/// </summary>
		/// <typeparam name="T">目标对象类型</typeparam>
		/// <param name="targetObj">目标对象实例</param>
		/// <param name="sourceObjs">源对象集合</param>
		/// <returns>目标对象</returns>
		public static T MapFrom<T>(this T targetObj, params object[] sourceObjs)
		{
			targetObj.MapFieldsFrom<T>(sourceObjs);
			targetObj.MapPropertiesFrom<T>(sourceObjs);
			return targetObj;
		}

		/// <summary>
		/// 将源对象的属性值赋给目标对象的同名属性
		/// </summary>
		/// <typeparam name="T">目标类型</typeparam>
		/// <param name="sourceObj">源对象</param>
		/// <returns>目标类型的新实例</returns>
		public static T MapPropertiesTo<T>(this object sourceObj) where T : class, new()
		{
			return MapPropertiesTo<T>(sourceObj, new T());
		}

		/// <summary>
		/// 将源对象的属性值赋给目标对象的同名属性
		/// </summary>
		/// <typeparam name="T">目标类型</typeparam>
		/// <param name="sourceObj">源对象</param>
		/// <param name="targetObj">目标对象</param>
		/// <returns>目标对象</returns>
		public static T MapPropertiesTo<T>(this object sourceObj, T targetObj)
		{
			if (sourceObj == null) return targetObj;
			var t = targetObj;
			foreach (var pi in typeof(T).GetProperties(System.Reflection.BindingFlags.Public | System.Reflection.BindingFlags.Instance))
			{
				if (pi.CanWrite)
				{
					var fromPi = sourceObj.GetType().GetProperty(pi.Name);
					if (fromPi != null && fromPi.CanRead)
					{
						var fromValue = fromPi.GetValue(sourceObj);
						pi.SetValue(t, fromValue);
					}
				}
			}
			return t;
		}

		/// <summary>
		/// 将源对象的字段值赋给目标对象的同名字段
		/// </summary>
		/// <typeparam name="T">目标类型</typeparam>
		/// <param name="sourceObj">源对象</param>
		/// <returns>目标类型的新实例</returns>
		public static T MapFieldsTo<T>(this object sourceObj) where T : class, new()
		{
			return MapFieldsTo<T>(sourceObj, new T());
		}

		/// <summary>
		/// 将源字段的属性值赋给目标对象的同名字段
		/// </summary>
		/// <typeparam name="T">目标类型</typeparam>
		/// <param name="sourceObj">源对象</param>
		/// <param name="targetObj">目标对象</param>
		/// <returns>目标对象</returns>
		public static T MapFieldsTo<T>(this object sourceObj, T targetObj)
		{
			if (sourceObj == null) return targetObj;
			var t = targetObj;
			foreach (var fi in typeof(T).GetFields(System.Reflection.BindingFlags.Public | System.Reflection.BindingFlags.Instance))
			{
				var fromFi = sourceObj.GetType().GetField(fi.Name);
				if (fromFi != null)
				{
					var fromValue = fromFi.GetValue(sourceObj);
					fi.SetValue(t, fromValue);
				}
			}
			return t;
		}

		/// <summary>
		/// 将源对象的属性与字段值赋给目标对象的同名属性与字段
		/// </summary>
		/// <typeparam name="T">目标类型</typeparam>
		/// <param name="sourceObj">源对象</param>
		/// <returns>目标类型的新实例</returns>
		public static T MapTo<T>(this object sourceObj) where T : class, new()
		{
			var t = new T();
			MapPropertiesTo<T>(sourceObj, t);
			MapFieldsTo<T>(sourceObj, t);
			return t;
		}

		/// <summary>
		/// 将源对象的属性与字段值赋给目标对象的同名属性与字段
		/// </summary>
		/// <typeparam name="T">目标类型</typeparam>
		/// <param name="sourceObj">源对象</param>
		/// <param name="targetObj">目标对象</param>
		/// <returns>目标对象</returns>
		public static T MapTo<T>(this object sourceObj, T targetObj)
		{
			var t = targetObj;
			MapPropertiesTo<T>(sourceObj, t);
			MapFieldsTo<T>(sourceObj, t);
			return t;
		}
	}
}
