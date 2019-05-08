using System;
using System.Collections.Generic;
using System.Text;
using System.Text.RegularExpressions;

namespace NgNet.Math
{
	[Function("Addition", "+", false, 4)]
	public class AdditionFunction : NonbracketFunction
	{
		const string REGEX_PARTTERN = @"^ *(?<left>\d+(\.\d+)?) *\+ *(?<right>\d+(\.\d+)?) *$";

		public AdditionFunction(string expr) : base(expr)
		{

		}

		public double ParamLeft { get; set; }

		public double ParamRight { get; set; }

		public override double Eval()
		{
			return ParamLeft + ParamRight;
		}

		protected override bool Parse(string expr)
		{
			var match = Regex.Match(expr, REGEX_PARTTERN);
			if (match.Success)
			{
				ParamLeft = double.Parse(match.Groups["left"].Value);
				ParamRight = double.Parse(match.Groups["right"].Value);
			}
			return match.Success;
		}
	}
}
