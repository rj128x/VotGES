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
			int powerStart=200;
			int powerStop=1000;
			int powerStep=50;
			string fn="logs";
			Console.WriteLine(String.Join("-", args));
			try {
				powerStart = Int32.Parse(args[0]);
				powerStop = Int32.Parse(args[1]);
				powerStep = Int32.Parse(args[2]);
			} catch {

			}
			Logger.init(new ConsoleLogger());
			//Logger.init(LoggerFile.createFileLogger(fn,String.Join("_", args),new LoggerFile()));
			//Console.WriteLine(RashodTable.getStationRashod(349, 13, RashodCalcMode.min));

			List<double>powers=new List<double>();
			for (double power=powerStart; power <= powerStop; power += powerStep) {
				powers.Add(power);
			}

			List<double>powersAll=new List<double>();
			for (double power=1; power <= 1020; power++) {
				powersAll.Add(power);
			}

			List<double> napors=new List<double>();
			for (double napor=18; napor <= 18; napor += 1) {
				napors.Add(napor);
			}

			double[]naporsMisc=new double[] { 17, 18, 19, 20, 21, 22, 16, 15, 14, 13, 23 };


			List<double> naporsAll=new List<double>();
			for (double napor=13; napor <= 23; napor += 0.1) {
				naporsAll.Add(napor);
			}


			calcFull(powers, naporsMisc.ToList(), "RUSA_FULL.html");
			//createMaxKPD(napors, "KPD_MAX.html");
			//createKPD(napors,powersAll, "KPD");

			//calcFullNew(powers, napors, "RUSA_FULL.html");

			/*calc(powersAll, naporsAll, true, @"d:\RUSA_BY_POWER.html", powers);
			calc(naporsAll, powersAll, false, @"d:\RUSA_BY_NAPOR.html", napors);*/

			int[] avail= { 1, 2, 3, 4, 5, 6, 7, 8, 9, 10 };

			/*RUSADiffPowerNapors rusa=new RUSADiffPowerNapors();
			rusa.getMinRashod(avail.ToList(),napors,800);*/






		}

		protected static string ArrToStr(SortedList<int, double> arr) {
			List<string> arrStr=new List<string>();
			foreach (KeyValuePair<int,double> de in arr) {
				if (de.Value > 0) {
					arrStr.Add(String.Format("{0}={1:0}", de.Key, de.Value));
				}
			}
			return String.Join(" ", arrStr);

		}

		protected static string ArrToStr(Dictionary<int, double> arr) {
			List<string> arrStr=new List<string>();
			foreach (KeyValuePair<int,double> de in arr) {
				if (de.Value > 0) {
					arrStr.Add(String.Format("{0}={1:0}", de.Key, de.Value));
				}
			}
			return String.Join(" ", arrStr);

		}

		protected static void checkAleks(Dictionary<int, double> sostav, double power, double napor, RUSADiffPower rusa) {
			int count=sostav.Count;
			int[] keys=sostav.Keys.ToArray();
			List<int> avail=new List<int>();
			for (int i=0; i < count - 2; i++) {
				int ga=keys[i];
				avail.Add(ga);
				rusa.startSostav.Add(ga, sostav[ga]);
				rusa.stopSostav.Add(ga, sostav[ga]);
			}
			for (int i=1; i <= 2; i++) {
				avail.Add(keys[count - i]);
				rusa.startSostav.Add(keys[count - i], 35);
				rusa.stopSostav.Add(keys[count - i], 100);
			}
			rusa.getMinRashod(avail, power, napor);
			double rashod=rusa.minRashod;
			int index=3;
			while (rashod == 10e8) {
				rusa.startSostav[keys[count - index]] = 35;
				rusa.stopSostav[keys[count - index]] = 100;
				Console.Write("==" + index + "==");
				rusa.getMinRashod(avail, power, napor);
				index++;
				rashod = rusa.minRashod;
			}

		}

		public static void createMaxKPD(List<double> napors, string fn) {
			string res="";
			res += "<tr><th>h</th>";
			for (int ga=1; ga <= 10; ga++) {
				res += String.Format("<th>GA-{0}</th>", ga);
			}
			res += "</tr>";
			foreach (double napor in napors) {
				res += String.Format("<tr><th>{0}</th>", napor);
				Dictionary<int,double> kpdArr=RashodTable.KPDArr(napor);
				for (int ga=1; ga <= 10; ga++) {
					res += String.Format("<td>{0:0.00} ({1:0.00})</td>", kpdArr[ga], RashodTable.KPD(kpdArr[ga], napor, RashodTable.getRashod(ga, kpdArr[ga], napor)) * 100);
				}
				res += "</tr>";
			}
			res = String.Format("<table border='1'>{0}</table>", res);
			System.IO.File.WriteAllText(fn, res);
		}

		public static void createKPD(List<double> napors, List<double> powers, string fn) {
			for (int ga=1; ga <= 10; ga++) {
				string res="";
				res += "<tr><th>h</th>";

				foreach (double napor in napors) {
					res += String.Format("<th>{0}m</th>", napor);
				}
				res += "</tr>";
				for (double power=1; power <= 110; power++) {
					res += String.Format("<tr><th>{0}</th>", power);
					foreach (double napor in napors) {
						res += String.Format("<td>{0:0.00}", RashodTable.KPD(power, napor, RashodTable.getRashod(ga, power, napor)) * 100);
					}
					res += "</tr>";
				}
				res = String.Format("<table border='1'>{0}</table>", res);
				System.IO.File.WriteAllText(fn + "_" + ga + ".html", res);
			}
		}

		protected static void calcFull(List<double> powers, List<double> napors, string fn) {
			List<int> sostav=new List<int>();

			double eq=0;
			double diff=0;
			double diff10=0;
			double aleks=0;
			double ideal=0;
			int[] allGAArr=new int[] { 1, 2, 3, 4, 5, 6, 7, 8, 9, 10 };

			SortedList<double,Dictionary<int,double>> kpdArrs=new SortedList<double, Dictionary<int, double>>();
			foreach (double napor in napors) {
				kpdArrs.Add(napor, RashodTable.KPDArr(napor));
			}

			string res=String.Format("<table border='1'><tr><th>h</th><th>p</th><th>eq</th><th>Alekseev</th><th>diff</th><th>kpdEq</th><th>kpdAl</th><th>kpdDiff</th><th>sostavEq</th><th>pEq</th><th>sostavAl</th><th>sostavDiff</th></tr>");
			System.IO.File.WriteAllText(fn, res);
			string str="";
			foreach (double napor in napors) {				

				foreach (double power in powers) {
					RUSADiffPower rusa=new RUSADiffPower();
					RUSADiffPower rusaAl=new RUSADiffPower();

					ideal = 1000 * power / (9.81 * napor);
					Console.Write(String.Format("{0,-3} {1,-3}", napor, power));
					eq = VotGES.Rashod.RUSA.getOptimRashod(power, napor, true, sostav);
					Console.Write(String.Format(" e={0:0.00} k={1:0.00} [{2}]", eq, ideal / eq * 100, String.Join("-", sostav)));

					Dictionary<int,double> kpdArr=kpdArrs[napor];
					double p=0;
					Dictionary<int,double> sostavAleks=new Dictionary<int, double>();
					foreach (KeyValuePair<int,double> de in kpdArr) {
						p += de.Value;
						sostavAleks.Add(de.Key, de.Value);
						if (p >= power) {
							break;
						}
					}
					checkAleks(sostavAleks, power, napor, rusaAl);
					aleks = rusaAl.minRashod;

					Console.Write(String.Format(" al={0:0.00} k={1:0.00} [{2}]", aleks, ideal / aleks * 100, ArrToStr(rusaAl.minSostav)));

					rusa.stepPower = 10;
					foreach (int ga in allGAArr) {
						rusa.startSostav.Add(ga, 40);
						rusa.stopSostav.Add(ga, 100);
					}
					diff10 = rusa.getMinRashod(allGAArr.ToList(), power, napor);
					Console.Write(String.Format(" [{0}]", ArrToStr(rusa.minSostav)));

					RUSADiffPower rusaNew=new RUSADiffPower();
					rusaNew.stepPower = 1;
					List<int>avail=new List<int>();
					foreach (int ga in allGAArr) {
						double min=rusa.minSostav[ga];
						if (min > 0) {
							avail.Add(ga);
							rusaNew.startSostav.Add(ga, min - 10 >= 35 ? min - 10 : 35);
							rusaNew.stopSostav.Add(ga, min + 10 <= 100 ? min + 10 : 100);
						}
					}
					rusaNew.getMinRashod(avail, power, napor);
					diff = rusaNew.minRashod;

					Console.WriteLine(String.Format(" d={0:0.00} k={1:0.00} [{2}]", diff, ideal / diff * 100, ArrToStr(rusaNew.minSostav)));

					str = String.Format("<tr><td>{0}</td><td>{1}</td><td>{2:0.00}</td><td>{3:0.00}</td><td>{4:0.00}</td><td>{5:0.0000}</td><td>{6:0.0000}</td><td>{7:0.0000}</td><td>{8}</td><td>{9:0.00}</td><td>{10}</td><td>{11}</td></tr>",
						napor, power, eq, aleks, diff, ideal / eq * 100, ideal / aleks * 100, ideal / diff * 100, String.Join(" ", sostav), power / sostav.Count, ArrToStr(rusaAl.minSostav), ArrToStr(rusaNew.minSostav));


					System.IO.File.AppendAllText(fn, str);
				}
			}
			System.IO.File.AppendAllText(fn, "</table>");
		}



		protected static void calc(List<double> X, List<double> Y, bool isFirstPower, string fn, List<double> XReport) {
			double minX=XReport.First();
			SortedList<double,SortedList<int,double>> result=new SortedList<double, SortedList<int, double>>();
			foreach (double x in XReport) {
				result.Add(x, new SortedList<int, double>());
				for (int ga=1; ga <= 10; ga++) {
					result[x].Add(ga, 0);
				}
			}

			double power=0;
			double napor=0;
			double rashod;

			SortedList<int,int>countByGA=new SortedList<int, int>();
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

				foreach (double y in Y) {
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

					foreach (int ga in sostav) {
						result[xRep][ga] += 1;
						countByX[xRep] += 1;
						allCount++;
						countByGA[ga]++;
					}
				}

			}


			res = "<tr><th>X</th>";
			for (int ga=1; ga <= 10; ga++) {
				res += String.Format("<th>GA-{0}</th>", ga);
			}
			res += "</tr>";

			foreach (double x in XReport) {
				res += String.Format("<tr><th>{0}</th>", x);
				for (int ga=1; ga <= 10; ga++) {
					res += String.Format("<td>{0}</td>", result[x][ga] / (double)countByX[x] * 100.0);
				}
				res += "</tr>";
			}
			res += String.Format("<tr><th>Sum</th>");
			for (int ga=1; ga <= 10; ga++) {
				res += String.Format("<td>{0}</td>", (double)countByGA[ga] / (double)allCount * 100.0);
			}
			res += "</tr>";

			res = String.Format("<table>{0}</table>", res);
			System.IO.File.WriteAllText(fn, res);
		}
	}

}
