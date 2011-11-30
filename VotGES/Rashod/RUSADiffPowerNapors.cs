using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace VotGES.Rashod
{
	public class RUSADiffPowerNapors
	{
		public List<int> availGenerators;

		public SortedList<int,double> currentSostav;
		public SortedList<double,SortedList<int,double>> minSostav;
		public SortedList<double,double> minRashod;

		public double needPower;
		public List<double> napors;

		public RUSADiffPowerNapors() {
			availGenerators=new List<int>();
			
		}

		public void getMinRashod(List<int> avail, List<double> napors,double power) {
			this.needPower = power;
			this.napors = napors;
			minRashod = new SortedList<double, double>();
			minSostav = new SortedList<double, SortedList<int, double>>();
			currentSostav = new SortedList<int, double>();
			foreach (double napor in napors) {
				minRashod.Add(napor, 10e8);
				minSostav.Add(napor, new SortedList<int, double>());				
			}
			availGenerators = avail;
			currentSostav.Clear();
			foreach (double napor in napors) {
				minSostav[napor].Clear();
			}

			int index=0;
			foreach (int ga in avail) {
				foreach (double napor in napors) {
					minSostav[napor].Add(ga, 0);
				}
				currentSostav.Add(ga, 0);
				index++;
			}

			

			calc(0);
			//Logger.Info(String.Join("~", minSostav.Values));
			//return minRashod;
		}

		public void calc(int gaIndex) {			
			int gaNumber=availGenerators[gaIndex];
			int nextGACount=availGenerators.Count - (gaIndex + 1);			
			double sumPower=0;
			SortedList<double,double> sumRashod=new SortedList<double, double>();
			foreach (double napor in napors) {
				sumRashod[napor] = 0;
			}
			double gaRashod;

			for (int i=0; i < gaIndex; i++) {
				int ga=availGenerators[i];
				sumPower += currentSostav[ga];
				foreach (double napor in napors) {
					sumRashod[napor] += currentSostav[ga] > 0 ? RashodTable.getRashod(ga, currentSostav[ga], napor) : 0;
				}
			}
			if (sumPower + (nextGACount+1) * 100 < needPower)
				return;
			if (gaIndex < availGenerators.Count) {
				for (int power=0; power <= 100; power+=10) {
					if (sumPower + power > needPower)
						break;
					if ((sumPower+power + 100 * nextGACount < needPower)|| (power != 0 && power < 35))
						continue;
					foreach (double napor in napors) {
						gaRashod = RashodTable.getRashod(gaNumber, power, napor);
						currentSostav[gaNumber] = power;
						//Logger.Info(String.Join("~", currentSostav.Values));
						if ((sumPower + power == needPower)) {
							if (sumRashod[napor] + gaRashod < minRashod[napor]) {
								minRashod[napor] = sumRashod[napor] + gaRashod;
								foreach (int ga in availGenerators) {
									minSostav[napor][ga] = currentSostav[ga];
								}
							}
						}
					}
					if (gaIndex < availGenerators.Count - 1) {
						calc(gaIndex + 1);
					}
				}
				currentSostav[gaNumber] = 0;
			}
		}	

		
	}
}
