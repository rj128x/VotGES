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
			SortedList<double,List<int>> sostavs=RUSA.getOptimRashodsFull(data.Power, data.Napor, data.getAvailGenerators());
			int index=0;
			data.EqResult = new List<RUSAResult>();
			foreach (KeyValuePair<double, List<int>> de in sostavs) {
				if (index == 3)
					break;
				index++;
				double rashod=de.Key;
				List<int> sostav=de.Value;				
				RUSAResult result=new RUSAResult();
				result.Rashod = rashod;
				result.Sostav = new Dictionary<int, double>();
				foreach (int ga in sostav) {
					result.Sostav.Add(ga, data.Power / sostav.Count);
				}
				result.KPD = RashodTable.KPD(data.Power, data.Napor, rashod) * 100;
				data.EqResult.Add(result);
			}


		}

		public static void processDiffData(RUSAData data) {
			List<RUSADiffPower.RusaChoice> choices=RUSADiffPower.getChoices(data.getAvailGenerators(), data.Napor, data.Power,3);
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