using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace VotGES.Rashod
{
	public static class RUSA
	{
		public static double getOptimRashod(double power, double napor, bool min = true) {			
			try {
				if (power < 35)
					return 0;
				double minRashod=10e6;
				double maxRashod=0;
				for (int count=1; count <= 10; count++) {					
					double divPower=(double)power / (double)count;
					if ((divPower < 35) || (divPower > 110))
						continue;
					SortedList<double,int>rashods=new SortedList<double, int>();
					for (int ga=1; ga <= 10; ga++) {
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
						if (minRashod > fullRashod)
							minRashod = fullRashod;
					} else {
						double fullRashod=0;
						for (int i=0; i < count; i++) {
							fullRashod += rashods.Keys[9 - i];
						}
						if (maxRashod < fullRashod)
							maxRashod = fullRashod;
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
