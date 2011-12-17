using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace VotGES.Rashod
{
	public static class RUSA
	{
		public static double getOptimRashod(double power, double napor, bool min = true, List<int> sostav =null, List<int> avail=null) {			
			try {
				if (avail == null) {
					avail = new List<int>();
					for (int ga=1; ga <= 10; ga++) {
						avail.Add(ga);
					}
				}
				
				if (power < 35)
					return 0;
				double minRashod=10e6;
				double maxRashod=0;
				for (int count=1; count <= avail.Count; count++) {					
					double divPower=(double)power / (double)count;
					if ((divPower < 35) || (divPower > 110))
						continue;
					SortedList<double,int>rashods=new SortedList<double, int>();
					foreach(int ga in avail) {
						double rashodGA=RashodTable.getRashod(ga, divPower, napor);						
						while (rashods.Keys.Contains(rashodGA)) {
							rashodGA += 10e-5;
						}
						rashods.Add(rashodGA, ga);
					}

					if (min) {
						double fullRashod=0;						
						for (int i=0; i < count; i++) {
							fullRashod += rashods.Keys[i];
						}
						if (minRashod > fullRashod) {
							if (sostav != null)
								sostav.Clear();
							for (int i=0; i < count; i++) {
								if (sostav != null)
									sostav.Add(rashods.Values[i]);
							}
							minRashod = fullRashod;
						}
					} else {
						double fullRashod=0;
						for (int i=0; i < count; i++) {
							fullRashod += rashods.Keys[avail.Count-1 - i];
						}
						if (maxRashod < fullRashod) {
							maxRashod = fullRashod;
							if (sostav != null)
								sostav.Clear();
							for (int i=0; i < count; i++) {
								if (sostav != null)
									sostav.Add(rashods.Values[avail.Count - 1 - i]);
							}
						}
					}
				}
				//Logger.Info(String.Format("Получение оптимального расхода для напора {0} и мощности {1} : {2}",napor,power,minRashod));
				return min ? minRashod : maxRashod;
			} catch (Exception e) {
				Logger.Error(String.Format("Ошибка получения оптимпльного расхода мощность: {0} napor: {1} ({2})", power, napor, e.Message));
				Logger.Error(e.StackTrace);
				return 0;
			}
		}



		
	}
}
