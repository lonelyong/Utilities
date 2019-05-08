using System;
using System.Collections.Generic;
using System.Text;
using System.Text.RegularExpressions;

namespace NgNet.Math
{
	public class Expression
	{
		const string REGEX_PATTERN_ROOT = @"^ *\(";

		public BracketFunction RootFunction { get; private set; }

		public string ExpressionString { get; }

		public Expression(string expr)
		{
			ExpressionString = expr;
			Parse(expr);
		}

		public void Parse(string expr)
		{
			if (!Regex.IsMatch(expr, REGEX_PATTERN_ROOT))
			{
				expr = "(" + expr + ")";
			}
			RootFunction = new BracketFunction(expr);
		}

		
	}
}
