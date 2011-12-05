using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace VotGES.Rashod
{
	public class RUSADiffPowerNaporsPowers
	{
		public List<int> availGenerators;

		public SortedList<int,double> currentSostav;
		public SortedList<double,SortedList<double,SortedList<int,double>>> minSostav;
		public SortedList<double,SortedList<double,double>> minRashod;

		public List<double> needPowers;
		public List<double> napors;
		public int step=0;

		public RUSADiffPowerNaporsPowers() {
			availGenerators=new List<int>();
			
		}

		public void getMinRashod(List<int> avail, List<double> napors, List<double> powers) {
			this.needPowers = powers;
			this.napors = napors;
			minRashod = new SortedList<double, SortedList<double, double>>();
			minSostav = new SortedList<double,SortedList<double,SortedList<int,double>>>();
			currentSostav = new SortedList<int, double>();

			foreach (double power in needPowers) {
				minRashod.Add(power, new SortedList<double, double>());
				minSostav.Add(power, new SortedList<double, SortedList<int, double>>());

				foreach (double napor in napors) {
					minRashod[power].Add(napor, 10e8);
					minSostav[power].Add(napor, new SortedList<int, double>());
				}
			}
			
			
			availGenerators = avail;
						

			foreach (int ga in avail) {
				foreach (double power in needPowers) {
					foreach (double napor in napors) {
						minSostav[power][napor].Add(ga, 0);
						
					}					
				}
				currentSostav.Add(ga, 0);
			}

			

			calc(0);
			//Logger.Info(String.Join("~", minSostav.Values));
			//return minRashod;
		}

		public void calc(int gaIndex) {			
			int gaNumber=availGenerators[gaIndex];
			double sumPower=0;
			double newPower;
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
			if (gaIndex < availGenerators.Count) {
				for (int power=0; power <= 100; power+=10) {
					if ((power != 0 && power < 35))
						continue;					
						if (step == 100000) {
							Logger.Info(String.Join("~", currentSostav.Values));
							step = 0;
						}
						step++;
						currentSostav[gaNumber] = power;
						newPower = sumPower + power;

						bool exist=true;
						try {
							needPowers.First(p => p == newPower);
						}catch {
							exist=false;
						}
												
						if (exist) {
							foreach (double napor in napors) {
								gaRashod = RashodTable.getRashod(gaNumber, power, napor);								
								if (sumRashod[napor] + gaRashod < minRashod[newPower][napor]) {
									minRashod[newPower][napor] = sumRashod[napor] + gaRashod;
									foreach (int ga in availGenerators) {
										minSostav[newPower][napor][ga] = currentSostav[ga];
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
