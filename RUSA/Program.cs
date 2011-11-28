using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using VotGES;

namespace RUSA
{
	class Program
	{
		static void Main(string[] args) {
			Logger.init(new ConsoleLogger());
			Console.WriteLine(RashodTable.getStationRashod(349, 13, RashodCalcMode.min));
		}

		protected void calc() {
			double maxRashod=0;
			double minRashod=0;
			double max=0;
			double min=0;
			for (int napor=13; napor <= 23; napor++) {
				for (int power=35; power <= 1020; power++) {
					Console.Write(String.Format("{0:00} - {1:00} === ", napor, power));
					min = RashodTable.getStationRashod(power, napor, RashodCalcMode.min);
					max = RashodTable.getStationRashod(power, napor, RashodCalcMode.max);
					Console.WriteLine(String.Format("max:{0:00}   min:{1:00}   koef:{2:00}", max, min, min / max));

					minRashod += min;
					maxRashod += max;
				}
			}
			Console.WriteLine(minRashod);
			Console.WriteLine(maxRashod);
			Console.WriteLine(minRashod / maxRashod);
		}
	}

}
