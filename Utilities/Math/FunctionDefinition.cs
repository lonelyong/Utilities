using System;
using System.Collections.Generic;
using System.Text;

namespace Utilities.Math
{
	public class FunctionDefinition
	{

		public FunctionDefinition(string name, string symbol, bool withbraket, int priorityLevel) {
			Name = name;
			Symbol = symbol;
			Withbracket = withbraket;
			PriorityLevel = priorityLevel;
		}
		public string Name { get; }

		public string Symbol { get;}

		public bool Withbracket { get; }

		public int PriorityLevel { get; } = -1;
	}
}
