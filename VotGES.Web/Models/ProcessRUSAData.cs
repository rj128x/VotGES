using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using VotGES.Rashod;

namespace VotGES.Web.Models
{
	public class ProcessRUSAData
	{
		public static void processEqualData(RUSAData data) {						
			List<int> sostav=new List<int>();
			double rashod=RUSA.getOptimRashod(data.Power, data.Napor, true, sostav,data.getAvailGenerators());
			data.EqResult = new List<RUSAResult>();
			RUSAResult result=new RUSAResult();
			result.Rashod = rashod;
			result.Sostav = new Dictionary<int, double>();
			foreach (int ga in sostav) {
				result.Sostav.Add(ga, data.Power / sostav.Count);
			}
			result.KPD = RashodTable.KPD(data.Power, data.Napor, rashod)*100;
			data.EqResult.Add(result);



		}

		public static void processDiffData(RUSAData data) {
			List<RUSADiffPower.RusaChoice> choices=RUSADiffPower.getChoices(data.getAvailGenerators(), data.Napor, data.Power);
			foreach (RUSADiffPower.RusaChoice choice in choices) {
				double rashod=choice.rashod;
				data.DiffResult = new List<RUSAResult>();
				RUSAResult result=new RUSAResult();
				result.Rashod = rashod;
				result.Sostav = new Dictionary<int, double>();
				foreach (KeyValuePair<int, double> de in choice.sostav) {
					if (de.Value > 0) {
						result.Sostav.Add(de.Key, de.Value);
					}
				}
				result.KPD = RashodTable.KPD(data.Power, data.Napor, rashod)*100;
				data.EqResult.Add(result);
			}			
		}


	}
}