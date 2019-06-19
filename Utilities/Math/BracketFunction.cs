using System;
using System.Collections.Generic;
using System.Text;
using System.Text.RegularExpressions;

namespace Utilities.Math
{
	public class BracketFunction : Function
	{

		const string REGEX_PATTERN = @"^ *(?<param>\([\w+-/%() ]+\)) *$";

		public string Param { get; set; }

		public BracketFunction(string expr) : base(expr)
		{
			ParseChild(Param);
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
				Param = match.Groups["param"].Value;
			}
			return match.Success;
		}
	}
}
