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
			data.eqResult = new List<RUSAData.RUSAResult>();
			RUSAData.RUSAResult result=new RUSAData.RUSAResult();
			result.Rashod = rashod;
			result.sostav = new Dictionary<int, double>();
			foreach (int ga in sostav) {
				result.sostav.Add(ga, data.Power / sostav.Count);
			}
			result.KPD = RashodTable.KPD(data.Power, data.Napor, rashod);
			data.eqResult.Add(result);
		}

		public static void processDiffData(RUSAData data) {
			List<RUSADiffPower.RusaChoice> choices=RUSADiffPower.getChoices(data.getAvailGenerators(), data.Napor, data.Power);
			foreach (RUSADiffPower.RusaChoice choice in choices) {
				double rashod=choice.rashod;
				data.diffResult = new List<RUSAData.RUSAResult>();
				RUSAData.RUSAResult result=new RUSAData.RUSAResult();
				result.Rashod = rashod;
				result.sostav = new Dictionary<int, double>();
				foreach (KeyValuePair<int, double> de in choice.sostav) {
					if (de.Value > 0) {
						result.sostav.Add(de.Key, de.Value);
					}
				}
				result.KPD = RashodTable.KPD(data.Power, data.Napor, rashod);
				data.eqResult.Add(result);
			}			
		}


	}
}