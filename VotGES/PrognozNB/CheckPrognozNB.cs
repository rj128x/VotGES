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
		public int HourStart { get; protected set; }
		public int MinStart { get; protected set; }
		public CheckPrognozNB(DateTime dateStart, int daysCount, bool isQFakt,int hourStart, int minStart):base(dateStart,daysCount) {
			DateStart = dateStart.Date;
			DaysCount = daysCount;
			IsQFakt = isQFakt;
			MinStart = minStart;
			HourStart = hourStart;
			DatePrognozStart = DateStart.AddHours(HourStart).AddMinutes(MinStart);
			DateEnd = DateStart.AddDays(daysCount);
		}

		public void startPrognoz() {
			prognoz = new PrognozNB();

			prognoz.FirstData = readFirstData(DatePrognozStart);
			readP();
			readPBR();
			readWater();
			checkData(DateStart,DateEnd);

			prognoz.DatePrognozStart = DatePrognozStart;
			prognoz.DatePrognozEnd = DateEnd;
			prognoz.T = TSum / TCount;
			prognoz.PArr = new SortedList<DateTime, double>();
			prognoz.IsQFakt = IsQFakt;
			if (IsQFakt) {
				foreach (KeyValuePair<DateTime,double> de in QFakt) {
					if (de.Key > DatePrognozStart) {
						prognoz.PArr.Add(de.Key, de.Value);
					}
				}
			} else {
				foreach (KeyValuePair<DateTime,double> de in PFakt) {
					if (de.Key > DatePrognozStart) {
						prognoz.PArr.Add(de.Key, de.Value);
					}
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
