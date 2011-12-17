using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace VotGES.Rashod
{
	public class RUSADiffPower
	{
		public class RusaChoice
		{
			public double rashod;
			public SortedList<int,double> sostav;

			public RusaChoice(double rashod, SortedList<int, double> newSostav) {
				this.rashod = rashod;
				sostav = new SortedList<int, double>();
				if (newSostav != null && newSostav.Count>0) {
					foreach (KeyValuePair<int,double> de in newSostav) {
						sostav.Add(de.Key, de.Value);
					}
				}
			}
		}

		protected class RusaRecord
		{
			public double power;
			public int count;
			public SortedList<double,RusaChoice> choices;

			public RusaRecord(int count) {
				choices = new SortedList<double, RusaChoice>();
				this.count = count;
			}

			public double maxRashod=10e8;
			public double minRashod=10e8;
			public bool empty=true;

			public void checkSostav(double rashod, SortedList<int, double> sostav) {
				empty = false;
				RusaChoice newChoice=new RusaChoice(rashod, sostav);
				while (choices.Keys.Contains(rashod)) {
					rashod += 10e-5;
				}
				choices.Add(rashod, newChoice);
				if (choices.Count > count) {
					choices.Remove(choices.Keys.Last());
				}
				minRashod = choices.Keys.First();
				maxRashod = choices.Keys.Last();
			}

		}
		protected static SortedList<string, SortedList<double, RUSADiffPower>> cache=new SortedList<string, SortedList<double, RUSADiffPower>>();

		static RUSADiffPower() {
			cache = new SortedList<string, SortedList<double, RUSADiffPower>>();
		}


		protected List<int> availGenerators;

		protected SortedList<int,SortedList<double,RusaRecord>> RUSARecords;

		public double minRashod;

		public double napor;
		public double stepPower=1;
		public int countChoices=10;

		protected RUSADiffPower(List<int> avail, double napor) {
			availGenerators = new List<int>();

			this.napor = napor;
			availGenerators = avail;

			RUSARecords = new SortedList<int, SortedList<double, RusaRecord>>();
			for (int index=0; index < availGenerators.Count; index++) {
				RUSARecords.Add(index, new SortedList<double, RusaRecord>());
			}

			preCalc(0);
			//Logger.Info(String.Join("-", RUSARecords[0].Keys));
			for (int index=1; index < availGenerators.Count;index++ ) {
				RUSARecords.RemoveAt(RUSARecords.Count - 1);
			}
		}

		protected void preCalc(int gaIndex) {			
			int gaNumber=availGenerators[gaIndex];
			double gaRashod=0;
			SortedList<int,double>currentSostav=new SortedList<int, double>();
			foreach (int ga in availGenerators) {
				currentSostav.Add(ga, 0);
			}


			double gaPower=0;
			double newPower=0;
			double stop=gaNumber < 3 ? 110 : 100;
			while (gaPower <= stop) {
				//Logger.Info(String.Format("{0}={1}={2}", gaIndex, gaNumber, gaPower));
				if (gaPower > 0 && gaPower < 35) {
					gaPower += stepPower;
					continue;
				}

				gaRashod = RashodTable.getRashod(gaNumber, gaPower, napor);

				if (gaIndex == availGenerators.Count - 1) {
					foreach (int ga in availGenerators) {
						currentSostav[ga] = 0;
					}
					currentSostav[gaNumber] = gaPower;
					RUSARecords[gaIndex].Add(gaPower, new RusaRecord(countChoices));
					RUSARecords[gaIndex][gaPower].checkSostav(gaRashod, currentSostav);
				} else {
					if (RUSARecords[gaIndex+1].Count==0) {
						preCalc(gaIndex + 1);
					}					
					foreach (double nextPower in RUSARecords[gaIndex + 1].Keys) {
						newPower = gaPower + nextPower;
						if (!RUSARecords[gaIndex].Keys.Contains(newPower)) {
							RUSARecords[gaIndex].Add(newPower, new RusaRecord(countChoices));
						}

						if (RUSARecords[gaIndex + 1].Keys.Contains(nextPower)) {
							foreach (KeyValuePair<double, RusaChoice> de in RUSARecords[gaIndex + 1][nextPower].choices) {
								if (de.Key + gaRashod < RUSARecords[gaIndex][newPower].maxRashod || RUSARecords[gaIndex][newPower].choices.Count<countChoices) {
									foreach (int ga in availGenerators) {
										currentSostav[ga] = de.Value.sostav[ga];
									}
									currentSostav[gaNumber] = gaPower;
									RUSARecords[gaIndex][newPower].checkSostav(de.Key + gaRashod, currentSostav);
								}
							}
						}
					}
				}
				gaPower += stepPower;
			}
		}


		public static RUSADiffPower getFromCache(List<int> availGenerators, double napor, int countChoices = 1) {
			string str=String.Join("-", availGenerators);
			if (!cache.Keys.Contains(str)) {
				cache.Add(str, new SortedList<double, RUSADiffPower>());
			}
			if (cache[str].Keys.Count > 100) {
				cache[str] = new SortedList<double, RUSADiffPower>();
			}
			if (!cache[str].Keys.Contains(napor)) {
				RUSADiffPower rusa=new RUSADiffPower(availGenerators, napor);
				cache[str].Add(napor, rusa);
			}
			return cache[str][napor];
		}

		public static double getMinRashod(List<int> availGenerators, double napor, double power) {
			double min=getFromCache(availGenerators, napor).RUSARecords[0][power].choices[8].rashod;
			return min;
		}

		public static SortedList<int, double> getMinSostav(List<int> availGenerators, double napor, double power) {
			RusaRecord rec= getFromCache(availGenerators, napor).RUSARecords[0][power];
			return rec.choices.First().Value.sostav;
		}

		public static List<RusaChoice> getChoices(List<int> availGenerators, double napor, double power) {
			return getFromCache(availGenerators, napor).RUSARecords[0][power].choices.Values.ToList();
		}

	}
}
