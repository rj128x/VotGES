using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using VotGES;

namespace RUSA
{
	class Program
	{
		static void Main(string[] args) {
			Logger.init(new ConsoleLogger());
			//Console.WriteLine(RashodTable.getStationRashod(349, 13, RashodCalcMode.min));

			List<double>powers=new List<double>();
			for (double power=35; power <= 1020; power++) {
				powers.Add(power);
			}

			List<double> napors=new List<double>();
			for (double napor=13; napor <= 23; napor+=1) {
				napors.Add(napor);
			}
			calc(powers, napors, true, @"d:\RUSA_BY_POWER.html");
			calc(napors, powers, false, @"d:\RUSA_BY_NAPOR.html");
		}

		protected static void calc(List<double>X, List<double> Y, bool isFirstPower, string fn) {
			double maxRashod=0;
			double minRashod=0;
			double idealRashod=0;
			double max=0;
			double min=0;
			double ideal=0;
			double minByX;
			double maxByX;
			double idealByX;
			double koef;
			double power;
			double napor;

			string res="";
			foreach (double x in X) {
				minByX = 0;
				maxByX = 0;
				idealByX = 0;
				foreach (double y in Y) 	{
					if (isFirstPower) {
						power = x;
						napor = y;
					} else {
						power = y;
						napor = x;
					}
					Console.Write(String.Format("{0:00.00} - {1:00} === ", napor, power));
					min = RashodTable.getStationRashod(power, napor, RashodCalcMode.min);
					max = RashodTable.getStationRashod(power, napor, RashodCalcMode.max);
					ideal=1000*power/(9.81*napor);
					koef=min / max;

					Console.WriteLine(String.Format("max:{0:00.00}   min:{1:00.00}   koef:{2:00.00}", max, min, koef));

					minByX += min;
					maxByX += max;
					idealByX += ideal;

					minRashod += min;
					maxRashod += max;
					idealRashod += ideal;
				}
				koef = minByX / maxByX;
				res += String.Format("<tr><td>{0:00.00}</td><td>{1:00.000000}</td><td>{1:00.000000}</td></tr>", x, minByX / idealByX, maxByX / idealByX);
			}

			res = String.Format("<table>{0}</table>", res);
			System.IO.File.WriteAllText(fn, res);
			Console.WriteLine(minRashod);
			Console.WriteLine(maxRashod);
			Console.WriteLine(minRashod / maxRashod);
		}
	}

}
