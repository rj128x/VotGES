using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using PILib;
using System.Xml.Serialization;
using System.IO;
using System.Runtime.Serialization;


namespace VotGES.Chart
{
	public class ChartDataSerie
	{
		public List<ChartDataPoint> Points { get; set; }
		public string Name { get; set; }

		public ChartDataSerie() {
			Points = new List<ChartDataPoint>();
		}

	}

	public class ChartDataPoint
	{
		public double YVal { get; set; }
		public DateTime XVal { get; set; }
		public double XValDouble { get; set; }

		public ChartDataPoint() {
		}

		public ChartDataPoint(DateTime XVal, double YVal) {
			this.XVal = XVal;
			this.YVal = YVal;
		}

		public ChartDataPoint(double XVal, double YVal) {
			this.XValDouble = XVal;
			this.YVal = YVal;
		}
	}

	public class ChartData
	{

		protected List<ChartDataSerie> series=null;

		public List<ChartDataSerie> Series {
			get {				
				return series;
			}
			set {
				this.series = value;
			}
		}

		

		public Dictionary<string, int> SeriesNames { get; set; }

		public ChartData() {
			Series = new List<ChartDataSerie>();
			SeriesNames = new Dictionary<string, int>();			
		}

		public void addSerie(ChartDataSerie serie) {
			if (!SeriesNames.Keys.Contains(serie.Name)){
				Series.Add(serie);
				SeriesNames.Add(serie.Name, Series.IndexOf(serie));
			}
		}

		public void removeSerie(string name) {
			if (SeriesNames.Keys.Contains(name)) {
				Series.RemoveAt(SeriesNames[name]);
				SeriesNames = new Dictionary<string, int>();
				foreach (ChartDataSerie serie in Series) {
					SeriesNames.Add(serie.Name, Series.IndexOf(serie));
				}
			}
		}

		public ChartDataSerie this[string name]{
			get {
				foreach (ChartDataSerie serie in series) {
					if (serie.Name == name) return serie;
				}
				return null;
			}
		}
		
		public ChartDataSerie getSerie(string name) {			
			foreach (ChartDataSerie serie in series) {
				if (serie.Name == name) return serie;
			}
			return null;
		}
	}
}
