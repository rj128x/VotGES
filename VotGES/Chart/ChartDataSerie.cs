using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Xml.Serialization;
using System.Runtime.Serialization;

namespace VotGES.Chart
{
	public class ChartDataSerie
	{
		public List<ChartDataPoint> Points { get; set; }
		public string Name{get; set;}		

		public ChartDataSerie() {
			Points = new List<ChartDataPoint>();
		}

	}
}
