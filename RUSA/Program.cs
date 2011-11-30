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
			int powerStart=50;
			int powerStop=1000;
			int powerStep=50;
			Console.WriteLine(String.Join("-", args));
			try {
				powerStart=Int32.Parse(args[0]);
				powerStop=Int32.Parse(args[1]);
				powerStep=Int32.Parse(args[2]);
			} catch {

			}
			Logger.init(new ConsoleLogger());
			//Console.WriteLine(RashodTable.getStationRashod(349, 13, RashodCalcMode.min));

			List<double>powers=new List<double>();
			for (double power=powerStart; power <= powerStop; power+=powerStep) {
				powers.Add(power);
			}

			List<double>powersAll=new List<double>();
			for (double power=1; power <= 1020; power++) {
				powersAll.Add(power);
			}

			List<double> napors=new List<double>();
			for (double napor=13; napor <= 23; napor+=1) {
				napors.Add(napor);
			}

			List<double> naporsAll=new List<double>();
			for (double napor=13; napor <= 23; napor += 0.1) {
				naporsAll.Add(napor);
			}

			//calcFull(powers, napors);
			calc(powersAll, naporsAll, true, @"d:\RUSA_BY_POWER.html", powers);
			calc(naporsAll, powersAll, false, @"d:\RUSA_BY_NAPOR.html", napors);

			/*int[] avail= { 1, 2, 3, 4, 5, 6, 7, 8, 9, 10 };
			RUSADiffPower rusa=new RUSADiffPower(avail);*/

			/*List<int> sostav=new List<int>();
			double rashodEq = VotGES.Rashod.RUSA.getOptimRashod(300, 16, true,sostav);
			sostav.Sort();
			RUSADiffPower rusa=new RUSADiffPower();
			double rashodDiff=rusa.getMinRashod(sostav,300, 16);
			Console.WriteLine(rashodDiff.ToString());
			Console.WriteLine(rashodEq.ToString());
			Console.WriteLine(String.Join("~", sostav));*/
			
		}

		protected static void calcFull(List<double> powers, List<double> napors) {
			List<int> allGa=new List<int>();
			for (int ga=1; ga <= 10; ga++)
				allGa.Add(ga);
			List<int> sostav=new List<int>();
			RUSADiffPowerNapors rusa=new RUSADiffPowerNapors();
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


			foreach (double power in powers) {
				eqByX = 0;
				diffByX = 0;
				idealByX = 0;

				rusa.getMinRashod(allGa, napors, power);

				foreach (double napor in napors){
					ideal = 1000 * power / (9.81 * napor);
					Console.Write(String.Format("{0,-3} {1,-3}", napor, power));
					eq = VotGES.Rashod.RUSA.getOptimRashod(power, napor, true, sostav);
					Console.Write(String.Format(" e={0:00000.00} k={1:0.00}", eq, ideal / eq));
					diff = rusa.minRashod[napor];

					koef = diff / eq;

					Console.WriteLine(String.Format(" d={0:00000.00} k={1:0.00} ({2}={3})", diff, ideal / diff, String.Join("-", sostav), String.Join("-", rusa.minSostav[napor].Values)));

					eqByX += eq;
					diffByX += diff;
					idealByX += ideal;

					eqRashod += eq;
					diffRashod += diff;
					idealRashod += ideal;
				}
			}
		}


		protected static void calc(List<double>X, List<double> Y, bool isFirstPower, string fn, List<double> XReport) {
			double minX=XReport.First();
			SortedList<double,SortedList<int,double>> result=new SortedList<double, SortedList<int, double>>();
			foreach (double x in XReport) {
				result.Add(x, new SortedList<int, double>());
				for (int ga=1;ga<=10;ga++){
					result[x].Add(ga,0);
				}
			}

			double power=0;
			double napor=0;
			double rashod;

			SortedList<int,int>countByGA=new SortedList<int,int>();
			for (int ga=1; ga <= 10; ga++) {
				countByGA.Add(ga, 0);
			}

			SortedList<double,int>countByX=new SortedList<double, int>();
			foreach (double x in XReport) {
				countByX.Add(x, 0);
			}

			double allCount=0;
			List<int> sostav=new List<int>();

			string res="";
			double xRep;

			foreach (double x in X) {
				xRep = x < minX ? minX : XReport.Last(i => i <= x);

				foreach (double y in Y) 	{
					if (isFirstPower) {
						power = x;
						napor = y;
					} else {
						power = y;
						napor = x;
					}

					Console.Write(String.Format("{2} === {0:0.00} {1:0.00}", napor, power, xRep));
					rashod = VotGES.Rashod.RUSA.getOptimRashod(power, napor, true, sostav);
					Console.WriteLine(String.Format(" e={0:00000.00}, {1}", rashod, String.Join("-", sostav)));

					foreach (int ga in sostav){
						result[xRep][ga]+=1;
						countByX[xRep] += 1;
						allCount++;
						countByGA[ga]++;
					}
				}
				
			}


			res="<tr><th>X</th>";
			for (int ga=1;ga<=10;ga++){
				res+=String.Format("<th>GA-{0}</th>",ga);
			}
			res+="</tr>";

			foreach (double x in XReport) {
				res+=String.Format("<tr><th>{0}</th>",x);
				for (int ga=1;ga<=10;ga++){
					res+=String.Format("<td>{0}</td>",result[x][ga]/(double)countByX[x]*100.0);
				}
				res+="</tr>";
			}
			res += String.Format("<tr><th>Sum</th>");
			for (int ga=1; ga <= 10; ga++) {
				res += String.Format("<td>{0}</td>", (double)countByGA[ga] / (double)allCount*100.0);
			}
			res += "</tr>";

			res = String.Format("<table>{0}</table>", res);
			System.IO.File.WriteAllText(fn, res);
		}
	}

}
