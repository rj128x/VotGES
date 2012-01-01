using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using VotGES.Piramida;
using VotGES.Chart;

namespace VotGES.PrognozNB
{
	public class CheckPrognozNB:PrognozNBFunc
	{
		public bool IsQFakt { get; protected set; }
		public CheckPrognozNB(DateTime dateStart, int daysCount, bool isQFakt):base(dateStart,daysCount) {
			DateStart = dateStart.Date;
			DaysCount = daysCount;
			IsQFakt = isQFakt;
			DateEnd = DateStart.AddDays(daysCount);
		}

		public void startPrognoz() {
			prognoz = new PrognozNB();

			prognoz.FirstData = readFirstData(DateStart);
			readP();
			readPBR();
			readWater();
			checkData(DateStart,DateEnd);

			prognoz.DatePrognozStart = DateStart;
			prognoz.DatePrognozEnd = DateEnd;
			prognoz.T = T;
			prognoz.PArr = new SortedList<DateTime, double>();
			prognoz.IsQFakt = IsQFakt;
			if (IsQFakt) {
				foreach (KeyValuePair<DateTime,double> de in QFakt) {
					prognoz.PArr.Add(de.Key, de.Value);
				}
			} else {
				foreach (KeyValuePair<DateTime,double> de in PFakt) {
					prognoz.PArr.Add(de.Key, de.Value);
				}
			}
			prognoz.calcPrognoz();
		}

		public override ChartAnswer getChart() {
			ChartAnswer chart= base.getChart();
			chart.Properties.removeSerie("PBR");
			chart.Data.removeSerie("PBR");

			if (IsQFakt) {
				chart.Properties.removeSerie("QPrognoz");
				chart.Data.removeSerie("QPrognoz");
				chart.Properties.removeSerie("NaporPrognoz");
				chart.Data.removeSerie("NaporPrognoz");
			}

			chart.Properties.Series[chart.Properties.SeriesNames["PFakt"]].Enabled = false;
			chart.Properties.Series[chart.Properties.SeriesNames["QFakt"]].Enabled = false;

			return chart;
		}
	}
}
