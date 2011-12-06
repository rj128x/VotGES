using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace VotGES.Rashod
{	
	public class RUSADiffPowerFull
	{
		public List<int> availGenerators;

		public SortedList<int,double> currentSostav= new SortedList<int, double>();
		public SortedList<int,double> minSostav=new SortedList<int, double>();

		public SortedList<int,double> startSostav=new SortedList<int, double>();
		public SortedList<int,double> stopSostav=new SortedList<int, double>();
		public SortedList<int,SortedList<double,double>> minRashodsFull;
		public SortedList<int,SortedList<double,SortedList<int,double>>> minSostavsFull;

		public double minRashod;

		public double needPower;
		public double napor;
		public double stepPower=1;

		public RUSADiffPowerFull(List<int> avail, double power, double napor) {
			availGenerators = new List<int>();
			currentSostav = new SortedList<int, double>();
			minSostav = new SortedList<int, double>();
			startSostav = new SortedList<int, double>();
			stopSostav=new SortedList<int, double>();

			this.needPower = power;
			this.napor = napor;
			minRashod = 10e8;
			availGenerators = avail;

			foreach (int ga in avail) {
				minSostav.Add(ga, 0);
				currentSostav.Add(ga, 0);
			}

			minRashodsFull = new SortedList<int, SortedList<double, double>>();
			minSostavsFull = new SortedList<int, SortedList<double, SortedList<int, double>>>();
			for (int i=0; i < avail.Count; i++) {
				minRashodsFull.Add(i, new SortedList<double, double>());
				minSostavsFull.Add(i, new SortedList<double, SortedList<int, double>>());
			}
			preCalc(0);
		}

		public void preCalc(int gaIndex) {			
			int gaNumber=availGenerators[gaIndex];
			double sumPower=0;
			double rashod=0;
			double gaRashod=0;

			for (int i=0; i < gaIndex; i++) {
				int ga=availGenerators[i];
				sumPower += currentSostav[ga];
				rashod += currentSostav[ga] > 0 ? RashodTable.getRashod(ga, currentSostav[ga], napor) : 0;
			}
			
			double gaPower=0;
			double newPower=0;
			while (gaPower <= 100) {
				Logger.Info(String.Format("{0}={1}={2}",gaIndex,gaNumber,gaPower));
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
							if (minRashodsFull[gaIndex + 1][nextPower] + gaRashod > minRashodsFull[gaIndex][newPower]) {
								minRashodsFull[gaIndex][newPower] = rashod + gaRashod;
								minSostavsFull[gaIndex][newPower] = new SortedList<int, double>();
								foreach (int ga in minSostavsFull[gaIndex + 1][nextPower].Keys) {
									minSostavsFull[gaIndex][newPower].Add(ga, minSostavsFull[gaIndex + 1][nextPower][ga]);
								}
							}
						}
					}
				}
				gaPower += stepPower;
			}
		}

		public double getMinRashod(double power) {
			double min=minRashodsFull[0][power];
			return min;
		}

		public void calc(int gaIndex) {
			int gaNumber=availGenerators[gaIndex];
			int nextGACount=availGenerators.Count - (gaIndex + 1);
			double sumPower=0;
			double rashod=0;


			for (int i=0; i < gaIndex; i++) {
				int ga=availGenerators[i];
				sumPower += currentSostav[ga];
				rashod += currentSostav[ga] > 0 ? RashodTable.getRashod(ga, currentSostav[ga], napor) : 0;
			}
			if (sumPower + (nextGACount + 1) * 100 < needPower)
				return;
			if (gaIndex < availGenerators.Count) {

				checkSostav(gaNumber, 0, napor, rashod, sumPower);
				if (gaIndex < availGenerators.Count - 1) {
					calc(gaIndex + 1);
				}

				double power=startSostav[gaNumber];
				while (power <= stopSostav[gaNumber]) {
					if (sumPower + power > needPower)
						break;
					if ((sumPower + power + 100 * nextGACount < needPower))
						power = needPower - sumPower - 100 * nextGACount;
					checkSostav(gaNumber, power, napor, rashod, sumPower);
					if (gaIndex < availGenerators.Count - 1) {
						calc(gaIndex + 1);
					}
					power += stepPower;
				}
				currentSostav[gaNumber] = 0;
			}
		}

		protected void checkSostav(int gaNumber, double power, double napor, double rashod, double sumPower) {
			double gaRashod;
			gaRashod = RashodTable.getRashod(gaNumber, power, napor);
			currentSostav[gaNumber] = power;
			if ((sumPower + power == needPower) && (rashod + gaRashod < minRashod)) {
				minRashod = rashod + gaRashod;
				foreach (int ga in availGenerators) {
					minSostav[ga] = currentSostav[ga];
				}
			}
		}


	}
}
