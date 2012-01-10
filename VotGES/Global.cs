using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Globalization;

namespace VotGES
{
	public class GlobalVotGES
	{
		static GlobalVotGES() {			
			NFIPoint = new CultureInfo("ru-RU").NumberFormat;
			NFIPoint.NumberDecimalSeparator = ".";
		}
		public static NumberFormatInfo NFIPoint;
		public static void setCulture() {
			System.Globalization.CultureInfo ci = new	System.Globalization.CultureInfo("en-GB");
			/*ci.NumberFormat.NumberDecimalSeparator = ".";
			ci.NumberFormat.NumberGroupSeparator = " ";
			ci.NumberFormat.NaNSymbol = "-";
			ci.NumberFormat.PositiveInfinitySymbol = "-";
			ci.NumberFormat.NegativeInfinitySymbol = "-";*/
			System.Threading.Thread.CurrentThread.CurrentCulture = ci;
			System.Threading.Thread.CurrentThread.CurrentUICulture = ci;			
		}
	}
}
