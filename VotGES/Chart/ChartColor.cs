using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Drawing;


namespace VotGES.Chart
{
	class ChartColor
	{
		protected static int current;
		protected static Dictionary<int,string> Colors;

		static ChartColor() {
			current = 0;
			Colors = new Dictionary<int, string>();
			Colors.Add(0, GetColorStr(Color.Black));
			Colors.Add(1, GetColorStr(Color.Red));
			Colors.Add(2, GetColorStr(Color.Green));
			Colors.Add(3, GetColorStr(Color.Blue));
			Colors.Add(4, GetColorStr(Color.Gray));
			Colors.Add(5, GetColorStr(Color.Pink));
			Colors.Add(6, GetColorStr(Color.Orange));
			Colors.Add(7, GetColorStr(Color.Purple));
			Colors.Add(8, GetColorStr(Color.Brown));
			Colors.Add(9, GetColorStr(Color.LightBlue));
			Colors.Add(10, GetColorStr(Color.LightGreen));			
		}

		public static string GetColorStr(Color color) {
			return String.Format("{0}-{1}-{2}", color.R, color.G, color.B);
		}

		public static string GetColorStr(int index) {
			if (index > Colors.Last().Key) {
				index = Colors.First().Key;
			}
			return Colors[index];
		}

		public static string NextColor() {
			current++;
			if (current > Colors.Last().Key) {
				current = Colors.First().Key;
			}
			return Colors[current];
		}

	}
}
