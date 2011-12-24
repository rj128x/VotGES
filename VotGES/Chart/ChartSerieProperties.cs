using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace VotGES.Chart
{
	public enum ChartSerieType { line, bar, pie, column, stepLine}
	public class ChartSerieProperties
	{
		public string TagName { get; set; }
		public string Title { get; set; }
		public bool Enabled { get; set; }
		public ChartSerieType SerieType { get; set; }
		public int LineWidth { get; set; }
		public string Color { get; set; }
		public bool Marker { get; set; }
		public int YAxisIndex { get; set; }
		

		public ChartSerieProperties() {
			Enabled = true;
			LineWidth = 2;
			Color = null;
			Marker = true;
			YAxisIndex = 0;
		}

		
	}
}
