using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace VotGES.Rashod
{
	public class RUSADiffPower
	{
		public List<int> availGenerators;

		public SortedList<int,double> currentSostav= new SortedList<int, double>();
		public SortedList<int,double> minSostav=new SortedList<int, double>();
		public double minRashod;

		public double needPower;
		public double napor;

		public RUSADiffPower() {
			availGenerators=new List<int>();
			
		}

		public double getMinRashod(List<int> avail,double power, double napor) {
			this.needPower = power;
			this.napor = napor;
			minRashod = 10e8;
			availGenerators = avail;
			minSostav.Clear();
			currentSostav.Clear();

			int index=0;
			foreach (int ga in avail) {
				minSostav.Add(ga, 0);
				currentSostav.Add(ga, 0);
				index++;
			}


			calc(0);
			//Logger.Info(String.Join("~", minSostav.Values));
			return minRashod;
		}

		public void calc(int gaIndex) {			
			int gaNumber=availGenerators[gaIndex];
			int nextGACount=availGenerators.Count - (gaIndex + 1);			
			double sumPower=0;
			double rashod=0;
			double gaRashod;

			for (int i=0; i < gaIndex; i++) {
				int ga=availGenerators[i];
				sumPower += currentSostav[ga];
				rashod += currentSostav[ga] > 0 ? RashodTable.getRashod(ga, currentSostav[ga], napor) : 0;
			}
			if (sumPower + (nextGACount+1) * 100 < needPower)
				return;
			if (gaIndex < availGenerators.Count) {
				for (int power=0; power <= 100; power+=5) {
					if (sumPower + power > needPower)
						break;
					if ((sumPower+power + 100 * nextGACount < needPower)|| (power != 0 && power < 35))
						continue;
					gaRashod = RashodTable.getRashod(gaNumber, power, napor);
					currentSostav[gaNumber] = power;					
					//Logger.Info(String.Join("~", currentSostav.Values));
					if ((sumPower + power == needPower)) {						
						if (rashod + gaRashod < minRashod) {
							minRashod = rashod+gaRashod;
							foreach (int ga in availGenerators) {
								minSostav[ga] = currentSostav[ga];
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
