using System;
using System.Reflection;
using System.Collections.Generic;
using NgNet.Collections;
using System.Text;

namespace NgNet.Math
{
	public abstract class Function
	{
		const string REGEX_CORE_EXPRESSION_PATTERN = @"^([+-]\d+(.\d+)?)+$";

		private static readonly Dictionary<Type, FunctionDefinition> _definitions;

		public string ExpressionString { get; }

		public Function[] Children { get; private set; }

		public FunctionDefinition Definition {
			get
			{
				if (!_definitions.ContainsKey(GetType()))
				{
					var funcAttr = GetType().GetCustomAttribute(typeof(FunctionAttribute)) as FunctionAttribute;
					if (funcAttr == null)
					{
						throw new Exception($"{nameof(Function)}必须有{nameof(FunctionAttribute)}");
					}
					_definitions.AddOrReplace(GetType(), new FunctionDefinition(funcAttr.Name, funcAttr.Symbol, funcAttr.Withbracket, funcAttr.PriorityLevel));
				}
				return _definitions[GetType()];
			}
		}

		protected Function(string expr)
		{
			FunctionManager = FunctionManager.Default;
			ExpressionString = expr;
			if (!Parse(expr))
			{
				if (!ParseChild(expr))
				{
					throw new ArgumentException($"expr不是合法的表达式");
				}
			}
		}

		public abstract double Eval();

		public FunctionManager FunctionManager {
			get;set;
		}

		protected abstract bool Parse(string expr);

		protected bool ParseChild(string expr)
		{

			return true;
		}

	}
}
