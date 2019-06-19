using System;
using System.Text.RegularExpressions;
using System.Collections.Generic;
using System.Text;

namespace Utilities.Math
{
	[Function("Sqrt", "√", true, -1)]
	public class SqrtFunction : WithbracketFunction
	{
		const string REGEX_PATTERN = @"^ *√ *(?<param>\d+(.\d+)?) *$";

		public double Param { get; set; }

		public SqrtFunction(string expr) : base(expr)
		{

		}

		public override double Eval()
		{
			throw new NotImplementedException();
		}

		protected override bool Parse(string expr)
		{
			var match = Regex.Match(expr, REGEX_PATTERN);
			if (match.Success)
			{
				Param = double.Parse(match.Groups["param"].Value);
			}
			return match.Success;
		}
	}
}
