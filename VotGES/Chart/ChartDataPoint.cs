using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Runtime.Serialization;

namespace VotGES.Chart
{
	public class ChartDataPoint
	{
		public double YVal{get; set;}

		public DateTime XVal { get; set; }

		public ChartDataPoint() {
		}

		public ChartDataPoint(DateTime XVal, double YVal) {
			this.XVal = XVal;
			this.YVal = YVal;
			//this.Index = Index;
		}
	}
}
