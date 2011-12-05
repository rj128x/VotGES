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
			string fn="logs";
			Console.WriteLine(String.Join("-", args));
			try {
				powerStart=Int32.Parse(args[0]);
				powerStop=Int32.Parse(args[1]);
				powerStep=Int32.Parse(args[2]);
			} catch {

			}
			Logger.init(new ConsoleLogger());
			//Logger.init(LoggerFile.createFileLogger(fn,String.Join("_", args),new LoggerFile()));
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


				calcFull(powers, napors, "RUSA_FULL.html");
			
			//calcFullNew(powers, napors, "RUSA_FULL.html");
			/*calc(powersAll, naporsAll, true, @"d:\RUSA_BY_POWER.html", powers);
			calc(naporsAll, powersAll, false, @"d:\RUSA_BY_NAPOR.html", napors);*/

			int[] avail= { 1, 2, 3, 4, 5, 6, 7, 8, 9, 10 };

			/*RUSADiffPowerNapors rusa=new RUSADiffPowerNapors();
			rusa.getMinRashod(avail.ToList(),napors,800);*/

			

			


		}

		protected static void calcFullNew(List<double> powers, List<double> napors,string fn) {
			List<int> allGa=new List<int>();
			for (int ga=1; ga <= 10; ga++)
				allGa.Add(ga);
			List<int> sostav=new List<int>();
			double eq=0;
			double diff=0;
			double ideal=0;
			double koef;

			int[] avail= { 1, 2, 3, 4, 5, 6, 7, 8, 9, 10 };
			RUSADiffPowerNaporsPowers rusa=new RUSADiffPowerNaporsPowers();
			rusa.getMinRashod(avail.ToList(), napors, powers);

			string res=String.Format("<tr><th>h</th><th>p</th><th>eq</th><th>diff</th><th>kpdEq</th><th>kpdDiff</th><th>sostavEq</th><th>pEq</th><th>sostavDiff</th></tr>");

			foreach (double power in powers) {
				foreach (double napor in napors) {
					ideal = 1000 * power / (9.81 * napor);
					Console.Write(String.Format("{0,-3} {1,-3}", napor, power));
					eq = VotGES.Rashod.RUSA.getOptimRashod(power, napor, true, sostav);
					Console.Write(String.Format(" e={0:00000.00} k={1:0.00}", eq, ideal / eq));
					diff = rusa.minRashod[power][napor];

					koef = diff / eq;

					Console.WriteLine(String.Format(" d={0:00000.00} k={1:0.00} ({2}={3})", diff, ideal / diff, String.Join("-", sostav), String.Join("-", rusa.minSostav[power][napor].Values)));

					res += String.Format("<tr><th>{0}</th><th>{1}</th><th>{2}</th><th>{3}</th><th>{4}</th><th>{5}</th><th>{6}</th><th>{7}</th><th>{8}</th></tr>",
						napor, power, eq, diff, ideal / eq, ideal / diff, String.Join("-", sostav), power / sostav.Count, String.Join("-", rusa.minSostav[power][napor].Values));
				}
			}
			res = String.Format("<table>{0}</table>", res);
			System.IO.File.WriteAllText(fn, res);
		}


		protected static void calcFull(List<double> powers, List<double> napors, string fn) {
			List<int> allGa=new List<int>();
			for (int ga=1; ga <= 10; ga++)
				allGa.Add(ga);
			List<int> sostav=new List<int>();
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

			string res=String.Format("<tr><th>h</th><th>p</th><th>eq</th><th>diff</th><th>kpdEq</th><th>kpdDiff</th><th>sostavEq</th><th>pEq</th><th>sostavDiff</th></tr>");
			foreach (double power in powers) {
				eqByX = 0;
				diffByX = 0;
				idealByX = 0;


				

				foreach (double napor in napors){
					RUSADiffPower rusa=new RUSADiffPower();
					
					ideal = 1000 * power / (9.81 * napor);
					Console.Write(String.Format("{0,-3} {1,-3}", napor, power));
					eq = VotGES.Rashod.RUSA.getOptimRashod(power, napor, true,sostav);
					rusa.getMinRashod(sostav, power, napor);
					Console.Write(String.Format(" e={0:00000.00} k={1:0.00}", eq, ideal / eq));
					diff = rusa.minRashod;

					koef = diff / eq;

					Console.WriteLine(String.Format(" d={0:00000.00} k={1:0.00} ({2}={3})", diff, ideal / diff, String.Join("-", sostav), String.Join("-", rusa.minSostav.Values)));

					eqByX += eq;
					diffByX += diff;
					idealByX += ideal;

					eqRashod += eq;
					diffRashod += diff;
					idealRashod += ideal;
					res += String.Format("<tr><th>{0}</th><th>{1}</th><th>{2}</th><th>{3}</th><th>{4}</th><th>{5}</th><th>{6}</th><th>{7}</th><th>{8}</th></tr>",
						napor, power, eq, diff, ideal / eq, ideal / diff, String.Join("-", sostav), power / sostav.Count, String.Join("-", rusa.minSostav.Values));
				}
			}
			res = String.Format("<table>{0}</table>", res);
			System.IO.File.WriteAllText(fn, res);
			
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
