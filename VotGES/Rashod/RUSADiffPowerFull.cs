using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace VotGES.Rashod
{	
	public class RUSADiffPowerFull
	{
		protected static SortedList<string, SortedList<double, RUSADiffPowerFull>> cache=new SortedList<string, SortedList<double, RUSADiffPowerFull>>();

		static RUSADiffPowerFull() {
			cache = new SortedList<string, SortedList<double, RUSADiffPowerFull>>();
		}

		protected List<int> availGenerators;

		public static SortedList<int,double> startSostav=new SortedList<int, double>();
		public static SortedList<int,double> stopSostav=new SortedList<int, double>();
		public SortedList<int,SortedList<double,double>> minRashodsFull;
		public SortedList<int,SortedList<double,SortedList<int,double>>> minSostavsFull;

		public double minRashod;

		public double napor;
		public double stepPower=1;

		protected RUSADiffPowerFull(List<int> avail,  double napor) {
			availGenerators = new List<int>();
			startSostav = new SortedList<int, double>();
			stopSostav=new SortedList<int, double>();

			this.napor = napor;
			minRashod = 10e8;
			availGenerators = avail;

			minRashodsFull = new SortedList<int, SortedList<double, double>>();
			minSostavsFull = new SortedList<int, SortedList<double, SortedList<int, double>>>();
			for (int i=0; i < avail.Count; i++) {
				minRashodsFull.Add(i, new SortedList<double, double>());
				minSostavsFull.Add(i, new SortedList<double, SortedList<int, double>>());
			}
			preCalc(0);
		}

		protected void preCalc(int gaIndex) {			
			int gaNumber=availGenerators[gaIndex];
			double gaRashod=0;
			
			double gaPower=0;
			double newPower=0;
			double stop=gaNumber < 3 ? 110 : 100;
			while (gaPower <= stop) {
				//Logger.Info(String.Format("{0}={1}={2}",gaIndex,gaNumber,gaPower));
				if (gaPower > 0 && gaPower < 35) {
					gaPower += stepPower;
					continue;
				}
					
				gaRashod = RashodTable.getRashod(gaNumber, gaPower, napor);

				if (gaIndex == availGenerators.Count - 1) {
					minRashodsFull[gaIndex][gaPower] = gaRashod;
					minSostavsFull[gaIndex][gaPower] = new SortedList<int, double>();
					minSostavsFull[gaIndex][gaPower].Add(gaNumber, gaPower);
				} else {
					if (minRashodsFull[gaIndex+1].Count==0) {
						preCalc(gaIndex + 1);
					}
					foreach (double nextPower in minRashodsFull[gaIndex + 1].Keys) {												
						newPower = gaPower + nextPower;
						if (!minRashodsFull[gaIndex].Keys.Contains(newPower)) {
							minRashodsFull[gaIndex].Add(newPower, 10e8);
							minSostavsFull[gaIndex].Add(newPower, new SortedList<int, double>());
						}

						if (minRashodsFull[gaIndex + 1].Keys.Contains(nextPower)) {
							if (minRashodsFull[gaIndex + 1][nextPower] + gaRashod < minRashodsFull[gaIndex][newPower]) {
								minRashodsFull[gaIndex][newPower] = minRashodsFull[gaIndex + 1][nextPower] + gaRashod;
								minSostavsFull[gaIndex][newPower] = new SortedList<int, double>();
								foreach (int ga in minSostavsFull[gaIndex + 1][nextPower].Keys) {
									minSostavsFull[gaIndex][newPower].Add(ga, minSostavsFull[gaIndex + 1][nextPower][ga]);
								}
								minSostavsFull[gaIndex][newPower].Add(gaNumber, gaPower);
							}
						}
					}
				}
				gaPower += stepPower;
			}
		}

		protected static RUSADiffPowerFull getFromCache(List<int> availGenerators, double napor){
			string str=String.Join("-", availGenerators);
			if (!cache.Keys.Contains(str)) {
				cache.Add(str, new SortedList<double, RUSADiffPowerFull>());								
			}
			/*if (cache[str].Count > 100) {
				cache[str].Clear();
			}*/
			if (!cache[str].Keys.Contains(napor)) {
				RUSADiffPowerFull rusa=new RUSADiffPowerFull(availGenerators, napor);
				cache[str].Add(napor, rusa);
			}
			return cache[str][napor];
		}

		public static double getMinRashod(List<int> availGenerators,double napor, double power) {			
			double min=getFromCache(availGenerators,napor).minRashodsFull[0][power];
			return min;
		}

		public static SortedList<int, double> getMinSostav(List<int> availGenerators, double napor, double power) {
			return getFromCache(availGenerators,napor).minSostavsFull[0][power];
		}
	}
}
