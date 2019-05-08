using System;
using System.Collections.Generic;
using System.Text;

namespace NgNet.Math
{
	[AttributeUsage(AttributeTargets.Class, Inherited = false, AllowMultiple = false)]
	public class FunctionAttribute : Attribute
	{
		public string Name { get; }

		public string Symbol { get; }

		public bool Withbracket { get; }


		public int PriorityLevel { get; set; }

		public FunctionAttribute(string name, string symbol, bool withbraket, int priorityLevel = -1)
		{
			Name = name;
			Symbol = symbol;
			Withbracket = withbraket;
			PriorityLevel = priorityLevel;
		}
	}
}
