using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Xml.Serialization;
using System.IO;
using System.Runtime.Serialization;

namespace VotGES.Chart
{
	public enum ChartSerieType { line, bar, pie, column, stepLine }
	public enum XAxisTypeEnum { numeric, datetime }	

	[Serializable]
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
	

	public class ChartAxisProperties
	{
		public int Index { get; set; }
		public bool Auto { get; set; }
		public double Min { get; set; }
		public double Max { get; set; }
		public double Interval { get; set; }
	}

	
	public class ChartProperties
	{
		[DataMember]
		public List<ChartSerieProperties> Series { get; set; }
		public XAxisTypeEnum XAxisType{get;set;}
		public List<ChartAxisProperties> Axes { get; set; }
		public Dictionary<string, int> SeriesNames { get; set; }
		public Dictionary<int, int> AxesNumbers { get; set; }
		public string XValueFormatString {get;set;}

		public ChartProperties() {
			Axes = new List<ChartAxisProperties>();
			Series = new List<ChartSerieProperties>();
			SeriesNames = new Dictionary<string, int>();
			AxesNumbers = new Dictionary<int, int>();
			XValueFormatString = "dd.MM HH:mm";
		}

		public static ChartProperties fromXML(string fileName) {
			try {
				XmlSerializer mySerializer = new XmlSerializer(typeof(ChartProperties));
				// To read the file, create a FileStream.
				FileStream myFileStream = new FileStream(fileName, FileMode.Open, FileAccess.Read, FileShare.Read); ;
				// Call the Deserialize method and cast to the object type.
				ChartProperties data = (ChartProperties)mySerializer.Deserialize(myFileStream);
				myFileStream.Close();

				if (data.Axes.Count == 0) {
					data.Axes = new List<ChartAxisProperties>();

					ChartAxisProperties main=new ChartAxisProperties();
					main.Auto = true;
					main.Index = 0;

					ChartAxisProperties sec=new ChartAxisProperties();
					sec.Auto = true;
					sec.Index = 1;

					data.Axes.Add(main);
					data.Axes.Add(sec);
				}
				return data;
			} catch (Exception e) {
				Logger.Error(e.ToString());
				return null;
			}
		}

		public void toXML(string fileName) {
			XmlSerializer mySerializer = new XmlSerializer(typeof(ChartProperties));
			// To write to a file, create a StreamWriter object.
			StreamWriter myWriter = new StreamWriter(fileName);
			mySerializer.Serialize(myWriter, this);
			myWriter.Close();
		}

		public void addSerie(ChartSerieProperties serie) {
			if (!SeriesNames.Keys.Contains(serie.TagName)) {
				Series.Add(serie);
				SeriesNames.Add(serie.TagName, Series.IndexOf(serie));
			}
		}

		public void removeSerie(string name) {
			if (SeriesNames.Keys.Contains(name)) {
				Series.RemoveAt(SeriesNames[name]);
				//SeriesNames.Remove(name);
				SeriesNames = new Dictionary<string, int>();
				foreach (ChartSerieProperties serie in Series) {
					SeriesNames.Add(serie.TagName, Series.IndexOf(serie));
				}
			}
		}

		public void addAxis(ChartAxisProperties ax) {
			if (!AxesNumbers.Keys.Contains(ax.Index)) {
				Axes.Add(ax);
				AxesNumbers.Add(ax.Index, Axes.IndexOf(ax));
			}
		}

	}
}
