using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using VotGES.Piramida;
using VotGES.Rashod;
using VotGES;
namespace RUSA
{
	class RUSARecord
	{
		public DateTime date;

		public double p;
		public double napor;

		public SortedList<int,double> realQ, equalQ, diffQ;

		public RUSARecord() {
			realQ = new SortedList<int, double>();
			equalQ = new SortedList<int, double>();
			diffQ = new SortedList<int, double>();
			for (int ga=1; ga <= 10; ga++) {
				realQ.Add(ga, 0);
				equalQ.Add(ga, 0);
				diffQ.Add(ga, 0);
			}
		}
		public bool hasQ=false;

	}
	static class CheckRusa
	{
		public static DateTime dateStart=new DateTime(2010, 1, 1);
		public static DateTime dateEnd=new DateTime(2010, 2, 1);

		public static string fn;

		public static SortedList<DateTime,RUSARecord> result=new SortedList<DateTime, RUSARecord>();
		public static SortedList<int,int> realCount,equalCount,diffCount;
		public static SortedList<int,double> realQ,equalQ,diffQ;

		public static double Qreal, Qdiff, Qeq;

		public static SortedList<double,List<DateTime>> napors=new SortedList<double, List<DateTime>>();


		public static void processGAQ(DateTime ds, DateTime de, bool processSum) {
			int[] items=new int[] { 104, 129, 154, 179, 204, 229, 254, 279, 304, 329, 276 };
			List<int> il=items.ToList();
			Piramida3000Entities model = VotGES.Piramida.PiramidaAccess.getModel();
			IQueryable<DATA> dataArr = from DATA d in model.DATA where
													d.PARNUMBER == 12 && d.DATA_DATE > ds && d.DATA_DATE < de &&
													d.OBJTYPE == 2 && d.OBJECT == 1 && il.Contains(d.ITEM)
												select d;
			DateTime date;
			foreach (DATA data in dataArr) {
				date = data.DATA_DATE;
				if (result.Keys.Contains(date)) {
					if (data.VALUE0.Value > 0) {
						int ga=-1;
						switch (data.ITEM) {
							case 276:
								double napor=Math.Round(data.VALUE0.Value * 10) / 10;
								napor = napor < 16 ? 16 : napor;
								napor = napor > 23 ? 23 : napor;
								result[date].napor = napor;
								if (!napors.Keys.Contains(napor)) {
									napors.Add(napor, new List<DateTime>());
								}
								napors[napor].Add(date);
								break;
							case 104:
								ga = 1;
								break;
							case 129:
								ga = 2;
								break;
							case 154:
								ga = 3;
								break;
							case 179:
								ga = 4;
								break;
							case 204:
								ga = 5;
								break;
							case 229:
								ga = 6;
								break;
							case 254:
								ga = 7;
								break;
							case 279:
								ga = 8;
								break;
							case 304:
								ga = 9;
								break;
							case 329:
								ga = 10;
								break;
						}
						if (processSum) {
							if (ga != -1) {
								result[date].realQ[ga] = data.VALUE0.Value;
								realCount[ga]++;
								realQ[ga] += data.VALUE0.Value;
								Qreal += data.VALUE0.Value;
								result[date].hasQ = true;
							}
						}
					}
				}
			}
		}

		
		public static void calc() {
			realCount = new SortedList<int, int>();
			realQ = new SortedList<int, double>();
			equalCount = new SortedList<int, int>();
			equalQ = new SortedList<int, double>();
			diffCount = new SortedList<int, int>();
			diffQ = new SortedList<int, double>();
			Qreal = 0; Qdiff = 0; Qeq = 0;

			for (int ga=1; ga <= 10; ga++) {
				realCount.Add(ga, 0);
				realQ.Add(ga, 0);
				equalCount.Add(ga, 0);
				equalQ.Add(ga, 0);
				diffCount.Add(ga, 0);
				diffQ.Add(ga, 0);
			}

			DateTime ds;
			DateTime de;

			ds = dateStart.Date;
			de = ds.Date;

			while (de < dateEnd) {
				de = ds.AddDays(10);
				de = de > dateEnd ? dateEnd : de;
				Console.WriteLine(ds);
				Piramida3000Entities model=VotGES.Piramida.PiramidaAccess.getModel();

				IQueryable<DATA> dataArr=from DATA d in model.DATA where
													 d.PARNUMBER == 12 && d.DATA_DATE > ds && d.DATA_DATE < de &&
													 d.OBJTYPE == 2 && d.OBJECT == 0 && d.ITEM == 1 && d.VALUE0.Value < 800000 select d;
				DateTime date;
				foreach (DATA data in dataArr) {
					date = data.DATA_DATE;

					if (!result.Keys.Contains(date)) {
						result.Add(date, new RUSARecord());
						result[date].date = data.DATA_DATE;
						result[date].p = Math.Round(data.VALUE0.Value / 1000.0);
						result[date].p = result[date].p < 35 ? 0 : result[date].p;
						result[date].p = result[date].p > 1020 ? 1020 : result[date].p;
					}
				}

				processGAQ(ds, de,true);

				ds = de;
			}



			double diff=0;
			double equal=0;
			SortedList<int,double> sostavDiff;
			int[]avail=new int[] { 1,2,3, 4, 5, 6, 7, 8, 9, 10 };
			List<int> sostavEq=new List<int>();
			List<int>availList=avail.ToList();
			foreach (double napor in napors.Keys) {
				foreach (DateTime date in napors[napor]) {
					RUSARecord rusa=result[date];
					if (rusa.hasQ) {
						Console.WriteLine(String.Format("{0}: {1} {2}", rusa.date, napor, rusa.p));
						diff = RUSADiffPower.getMinRashod(availList, napor, rusa.p);
						sostavDiff = RUSADiffPower.getMinSostav(availList, rusa.napor, rusa.p);
						equal = VotGES.Rashod.RUSA.getOptimRashod(rusa.p, rusa.napor, true, sostavEq, availList);

						foreach (int ga in sostavDiff.Keys) {
							if (sostavDiff[ga] > 0) {
								rusa.diffQ[ga] = RashodTable.getRashod(ga, sostavDiff[ga], rusa.napor);
								diffCount[ga]++;
								diffQ[ga] += rusa.diffQ[ga];
								Qdiff += rusa.diffQ[ga];
							}
						}

						foreach (int ga in sostavEq) {
							rusa.equalQ[ga] = RashodTable.getRashod(ga, rusa.p / sostavEq.Count, rusa.napor);
							equalCount[ga]++;
							equalQ[ga] += rusa.equalQ[ga];
							Qeq += rusa.equalQ[ga];
						}
					}
				}
			}

			double hours=result.Count;
			string str="<table><tr><th>GA</th><th>Qreal</th><th>Real%</th><th>Treal</th><th>Treal%</th><th>Qeq</th><th>Eq%</th><th>Teq</th><th>Teq%</th><th>Qdiff</th><th>Diff%</th><th>Tdiff</th><th>Tdiff%</th></tr>";
			for (int ga=1; ga <= 10; ga++) {
				string s=String.Format("<tr><th>GA{0}</th><td>{1:0.00}</td><td>{2:0.00}</td><td>{3:0.00}</td><td>{4:0.00}</td><td>{5:0.00}</td><td>{6:0.00}</td><td>{7:0.00}</td><td>{8:0.00}</td><td>{9:0.00}</td><td>{10:0.00}</td><td>{11:0.00}</td><td>{12:0.00}</td></tr>",
					ga, realQ[ga], realQ[ga] / Qreal * 100, realCount[ga] / 2, realCount[ga] / hours * 100,
					equalQ[ga], equalQ[ga] / Qeq * 100, equalCount[ga] / 2, equalCount[ga] / hours * 100,
					diffQ[ga], diffQ[ga] / Qdiff * 100, diffCount[ga] / 2, diffCount[ga] / hours * 100);
				str += s;
			}
			str += "</table>";
			System.IO.File.WriteAllText(fn, str);


		}
	}
}
