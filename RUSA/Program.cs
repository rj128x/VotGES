using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using VotGES;
using VotGES.Rashod;

namespace RUSA
{
	class Program
	{
		static void Main(string[] args) {
			Logger.init(new ConsoleLogger());
			//Console.WriteLine(RashodTable.getStationRashod(349, 13, RashodCalcMode.min));

			List<double>powers=new List<double>();
			for (double power=50; power <= 1020; power+=50) {
				powers.Add(power);
			}

			List<double> napors=new List<double>();
			for (double napor=16; napor <= 22; napor+=1) {
				napors.Add(napor);
			}
			calc(powers, napors, true, @"d:\RUSA_BY_POWER.html");
			//calc(napors, powers, false, @"d:\RUSA_BY_NAPOR.html");

			/*int[] avail= { 1, 2, 3, 4, 5, 6, 7, 8, 9, 10 };
			RUSADiffPower rusa=new RUSADiffPower(avail);*/

			/*List<int> sostav=new List<int>();
			double rashodEq = VotGES.Rashod.RUSA.getOptimRashod(250, 16, true,sostav);
			sostav.Sort();
			RUSADiffPower rusa=new RUSADiffPower();
			double rashodDiff=rusa.getMinRashod(sostav,250, 16);
			Console.WriteLine(rashodDiff.ToString());
			Console.WriteLine(rashodEq.ToString());
			Console.WriteLine(String.Join("~", sostav));*/
			
		}

		protected static void calc(List<double>X, List<double> Y, bool isFirstPower, string fn) {
			List<int> sostav=new List<int>();
			RUSADiffPower rusa=new RUSADiffPower();
			double eqRashod=0;
			double diffRashod=0;
			double idealRashod=0;
			double eq=0;
			double diff=0;
			double ideal=0;
			double eqByX;
			double diffByX;
			double idealByX;
			double koef;
			double power;
			double napor;

			string res="";
			foreach (double x in X) {
				eqByX = 0;
				diffByX = 0;
				idealByX = 0;
				foreach (double y in Y) 	{
					if (isFirstPower) {
						power = x;
						napor = y;
					} else {
						power = y;
						napor = x;
					}

					ideal=1000*power/(9.81*napor);
					Console.Write(String.Format("{0,-3} {1,-3}", napor, power));
					eq = VotGES.Rashod.RUSA.getOptimRashod(power, napor, true, sostav);
					Console.Write(String.Format(" e={0:00000.00} k={1:0.00}", eq, ideal/eq));
					diff = rusa.getMinRashod(sostav, power, napor); 					
					koef=diff / eq;

					Console.WriteLine(String.Format(" d={0:00000.00} k={1:0.00} ({2}={3})", diff, ideal/diff, String.Join("-", sostav), String.Join("-", rusa.minSostav.Values)));

					eqByX += eq;
					diffByX += diff;
					idealByX += ideal;

					eqRashod += eq;
					diffRashod += diff;
					idealRashod += ideal;
				}
				koef = diffByX / eqByX;
				res += String.Format("<tr><td>{0:00.00}</td><td>{1:00.000000}</td><td>{2:00.000000}</td></tr>", x, idealByX/diffByX, idealByX/eqByX);
			}

			res = String.Format("<table>{0}</table>", res);
			System.IO.File.WriteAllText(fn, res);
			Console.WriteLine(eqRashod);
			Console.WriteLine(diffRashod);
			Console.WriteLine(eqRashod / diffRashod);
		}
	}

}
