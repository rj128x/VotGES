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
			for (double napor=13; napor <= 23; napor++) {
				napors.Add(napor);
			}
			//calc(powers,napors,true);
			calc(napors, powers, false);
		}

		protected static void calc(List<double>X, List<double> Y, bool isFirstPower) {
			double maxRashod=0;
			double minRashod=0;
			double max=0;
			double min=0;
			double minByX;
			double maxByX;
			double koef;
			double power;
			double napor;

			SortedList<double,double> koefsByX=new SortedList<double, double>();
			foreach (double x in X) {
				minByX = 0;
				maxByX = 0;
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
					koef=min / max;

					Console.WriteLine(String.Format("max:{0:00.00}   min:{1:00.00}   koef:{2:00.00}", max, min, koef));

					minByX += min;
					maxByX += max;

					minRashod += min;
					maxRashod += max;
				}
				koef = minByX / maxByX;
				koefsByX.Add(x, koef);
			}

			string res="";
			foreach (KeyValuePair<double,double> de in koefsByX){
				res += String.Format("<tr><td>{0:00.00}</td><td>{1:00.000000}</td></tr>", de.Key, de.Value);
			}
			res = String.Format("<table>{0}</table>", res);
			System.IO.File.WriteAllText(@"d:\RUSA.html", res);
			Console.WriteLine(minRashod);
			Console.WriteLine(maxRashod);
			Console.WriteLine(minRashod / maxRashod);
		}
	}

}
