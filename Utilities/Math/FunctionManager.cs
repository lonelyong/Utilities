using System;
using System.Collections.Generic;
using System.Text;
using System.Reflection;
using System.Linq;

namespace Utilities.Math
{
	public class FunctionManager
	{
		private static readonly Dictionary<Type, FunctionDefinition> _coreFunctions;

		private Dictionary<Type, FunctionDefinition> _funcDefinitions = new Dictionary<Type, FunctionDefinition>();

		public static FunctionManager Default { get; }

		public IReadOnlyDictionary<Type, FunctionDefinition> Functions { get; }

		static FunctionManager()
		{
			var types = Assembly.GetExecutingAssembly().GetTypes().Where(t => !t.IsAbstract && t.IsSubclassOf(typeof(Function)));
			var funcDefinitions = new Dictionary<Type, FunctionDefinition>();
			foreach (var item in types)
			{
				var funcAttr = item.GetCustomAttribute(typeof(FunctionAttribute)) as FunctionAttribute;
				if(funcAttr != null)
				{
					funcDefinitions.Add(item, new FunctionDefinition(funcAttr.Name, funcAttr.Symbol, funcAttr.Withbracket, funcAttr.PriorityLevel));
				}
			}
			_coreFunctions = funcDefinitions;
			Default = new FunctionManager();
		}

		public FunctionManager()
		{
			foreach (var item in _coreFunctions)
			{
				_funcDefinitions.Add(item.Key, item.Value);
			}
		}

		public void RegisterFunction<T>()
		{
			RegisterFunction(typeof(T));
		}

		public void RegisterFunction(Type item)
		{
			if(!item.IsAbstract || !item.IsSubclassOf(typeof(Function)))
			{
				throw new ArgumentException($"{nameof(item)}不是{typeof(Function).FullName}");
			}
			var funcAttr = item.GetCustomAttribute(typeof(FunctionAttribute)) as FunctionAttribute;
			if (funcAttr != null)
			{
				_funcDefinitions.Add(item, new FunctionDefinition(funcAttr.Name, funcAttr.Symbol, funcAttr.Withbracket, funcAttr.PriorityLevel));
			}
			else
			{
				throw new ArgumentException($"{nameof(item)}必须具有{typeof(FunctionAttribute).FullName}特性");
			}
		}
	}
}
