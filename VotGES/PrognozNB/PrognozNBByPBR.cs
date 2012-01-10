using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using VotGES.Piramida;
using VotGES.Chart;

namespace VotGES.PrognozNB
{
	public class PrognozValue
	{
		public DateTime Date { get; set; }
		public double QAvg { get; set; }
		public double Vyrab { get; set; }
		public double NBAvg { get; set; }
		public double NBMin { get; set; }
		public double NBMax { get; set; }
	}
	public class PrognozNBByPBRAnswer
	{

		public ChartAnswer Chart { get; set; }
		public double VyrabFakt { get; set; }
		public double QFakt { get; set; }
		public double NBAvg { get; set; }
		public double NBMin { get; set; }
		public double NBMax { get; set; }
		public List<PrognozValue> PrognozValues{get;set;}		
	}

	public class PrognozNBByPBR : PrognozNBFunc
	{
		

		protected SortedList<DateTime,double> userPBR;
		public SortedList<DateTime, double> UserPBR {
			get { return userPBR; }
			set { userPBR = value; }
		}

		protected SortedList<DateTime,double> m53500;
		public SortedList<DateTime, double> M53500 {
			get {
				if (m53500 == null) {
					m53500 = new SortedList<DateTime, double>();
					readM53500();
				}
				return m53500;
			}
			set { m53500 = value; }
		}

		protected SortedList<DateTime,double> pbrPrevSutki;
		public SortedList<DateTime, double> PBRPrevSutki {
			get {
				if (pbrPrevSutki == null) {
					pbrPrevSutki = new SortedList<DateTime, double>();
					readPBRPrevSutki();
				}
				return pbrPrevSutki;
			}
			set { pbrPrevSutki = value; }
		}

		protected SortedList<DateTime,double> pbrFull;
		public SortedList<DateTime, double> PBRFull {
			get { return pbrFull; }
			set { pbrFull = value; }
		}

		protected PrognozNBByPBRAnswer prognozAnswer;
		public PrognozNBByPBRAnswer PrognozAnswer {
			get { return prognozAnswer; }
			set { prognozAnswer = value; }
		}


		public PrognozNBByPBR(DateTime dateStart, int daysCount, DateTime datePrognozStart, SortedList<DateTime, double> userPBR)
			: base(dateStart, daysCount) {
			DateStart = dateStart.Date;
			DaysCount = daysCount;
			DateEnd = DateStart.AddDays(daysCount);

			DatePrognozStart = datePrognozStart;
			int hour=DatePrognozStart.Hour;
			int min=DatePrognozStart.Minute;
			min = min < 30 ? 0 : min;
			min = min > 30 ? 30 : min;
			DatePrognozStart = DatePrognozStart.Date.AddHours(hour).AddMinutes(min);
			UserPBR = userPBR;


		}

		public void readM53500() {

			List<PiramidaEnrty> dataArr=PiramidaAccess.GetDataFromDB(DateStart, DateEnd, 53500, 2, 212, (new int[] { 3 }).ToList<int>(), true, true);

			foreach (PiramidaEnrty data in dataArr) {
				if (!m53500.Keys.Contains(data.Date)) {
					m53500.Add(data.Date, data.Value0);
				}
			}
		}


		public void readPBRPrevSutki() {
			DateTime ds=DatePrognozStart;
			DateTime de=DatePrognozStart.AddDays(-1);
			List<PiramidaEnrty> dataArr=PiramidaAccess.GetDataFromDB(DateStart, DateEnd, 0, 2, 212, (new int[] { 1 }).ToList<int>(), true, true);
			foreach (PiramidaEnrty data in dataArr) {
				if (!pbrPrevSutki.Keys.Contains(data.Date)) {
					pbrPrevSutki.Add(data.Date, data.Value0 / 1000);
				}
			}
		}

		public void preparePArr() {
			pbrFull = new SortedList<DateTime, double>();
			DateTime date=datePrognozStart.AddMinutes(0);
			while (date <= dateEnd) {
				if (userPBR != null && userPBR.Keys.Contains(date)) {
					pbrFull.Add(date, userPBR[date]);
				} else if (pbr.Keys.Contains(date)) {
					pbrFull.Add(date, pbr[date]);
				} else if (M53500.Keys.Contains(date)) {
					pbrFull.Add(date, M53500[date]);
				} else if (pbrFull.Keys.Contains(date.AddHours(-24))) {
					pbrFull.Add(date, pbrFull[date.AddHours(-24)]);
				} else if (PBRPrevSutki.Keys.Contains(date.AddHours(-24))) {
					pbrFull.Add(date, PBRPrevSutki[date.AddHours(-24)]);
				} else if (pbrFull.Keys.Contains(date.AddMinutes(-30))) {
					pbrFull.Add(date, pbrFull[date.AddMinutes(-30)]);
				} else {
					pbrFull.Add(date, 0);
				}
				date = date.AddMinutes(30);
			}

		}

		public override SortedList<DateTime, PrognozNBFirstData> readFirstData(DateTime date) {
			date = DatePrognozStart.AddMinutes(0);
			int[] items=new int[] { 354, 276, 373, 275, 274 };
			List<PiramidaEnrty> dataArr=null;
			List<PiramidaEnrty> dataArrP,dataArrW=null;
			List<int> il=items.ToList();
			double cnt=0;
			int index=0;
			while (cnt < 30 && index <= 10) {
				DateTime ds=date.AddHours(-2);
				DateTime de=date.AddHours(0);
				dataArrW=PiramidaAccess.GetDataFromDB(ds, de, 1, 2, 12, il, true, true);
				dataArrP = PiramidaAccess.GetDataFromDB(ds, de, 0, 2, 12, (new int[] { 1 }).ToList<int>(), true, true);
				dataArr = new List<PiramidaEnrty>();
				foreach (PiramidaEnrty entry in dataArrW) {
					dataArr.Add(entry);
				}
				foreach (PiramidaEnrty entry in dataArrP) {
					dataArr.Add(entry);
				}
				cnt = dataArr.Count();
				date = date.AddMinutes(-30);
				index++;
			}
			DatePrognozStart = date.AddMinutes(30);
			return processFirstData(dataArr);
		}

		public override void writeFaktData(Chart.ChartData data) {
			base.writeFaktData(data);

			ChartDataSerie serie=data.Series[data.SeriesNames["PBR"]];
			serie.Points.Clear();
			foreach (KeyValuePair<DateTime,double> de in PBRFull) {
				serie.Points.Add(new ChartDataPoint(de.Key, de.Value));
			}
		}

		public void processAnswer() {
			PrognozNBByPBRAnswer answer=new PrognozNBByPBRAnswer();
			foreach (DateTime date in PFakt.Keys) {
				if (date > DateStart && date <= DatePrognozStart) {
					answer.VyrabFakt += PFakt[date] / 2;
				}
			}

			int countQ=0;
			foreach (DateTime date in QFakt.Keys) {
				if (date > DateStart && date <= DatePrognozStart) {
					answer.QFakt += QFakt[date];
					countQ++;
				}
			}

			answer.NBMin = 100;
			answer.NBMax = 0;
			int countNB=0;
			foreach (DateTime date in NBFakt.Keys) {
				if (date > DateStart && date <= DatePrognozStart) {
					double nb=NBFakt[date];
					answer.NBAvg += nb;
					answer.NBMin = answer.NBMin < nb ? answer.NBMin : nb;
					answer.NBMax = answer.NBMax > nb ? answer.NBMax : nb;
					countNB++;
				}
			}
			
			SortedList<DateTime,PrognozValue> values=new SortedList<DateTime, PrognozValue>();
			DateTime currentDate=DatePrognozStart.AddMinutes(30);
			while (currentDate <= DateEnd) {
				DateTime dt=currentDate.AddMinutes(-30).Date;
				if (!values.Keys.Contains(dt)) {
					values.Add(dt, new PrognozValue());
					values[dt].NBMin = 100;
					values[dt].NBMax = 0;
					values[dt].Date = currentDate;
				}
				double nb= Prognoz.Prognoz[currentDate];
				values[dt].Vyrab += Prognoz.PArr[currentDate]/2;
				values[dt].QAvg += Prognoz.Rashods[currentDate];
				values[dt].NBAvg += nb;
				values[dt].NBMin = values[dt].NBMin < nb ? values[dt].NBMin : nb;
				values[dt].NBMax = values[dt].NBMax > nb ? values[dt].NBMax : nb;
								
				currentDate = currentDate.AddMinutes(30);
			}

			DateTime datePrognoz=DateStart.AddMinutes(30).Date;
			values[datePrognoz].QAvg += answer.QFakt;
			values[datePrognoz].NBAvg += answer.NBAvg;
			values[datePrognoz].Vyrab += answer.VyrabFakt;
			values[datePrognoz].NBMin = answer.NBMin < values[datePrognoz].NBMin ? answer.NBMin : values[datePrognoz].NBMin;
			values[datePrognoz].NBMax = answer.NBMax > values[datePrognoz].NBMax ? answer.NBMax : values[datePrognoz].NBMax;
			
			answer.QFakt /= countQ;
			answer.NBAvg /= countNB;

			foreach (DateTime date in values.Keys) {
				values[date].QAvg /= 48;
				values[date].NBAvg /= 48;
			}

			answer.PrognozValues = values.Values.ToList();
			PrognozAnswer = answer;
		}

		public void startPrognoz() {
			prognoz = new PrognozNB();

			prognoz.FirstData = readFirstData(DatePrognozStart);
			readP();
			readPBR();
			readWater();
			preparePArr();
			checkData(DateStart, DatePrognozStart);

			prognoz.DatePrognozStart = DatePrognozStart;
			prognoz.DatePrognozEnd = DateEnd;
			prognoz.T = TSum/TCount;
			prognoz.PArr = new SortedList<DateTime, double>();
			prognoz.IsQFakt = false;
			bool isFirst=true;
			double prev=0;
			foreach (KeyValuePair<DateTime,double> de in PBRFull) {
				if (!isFirst) {
					Prognoz.PArr.Add(de.Key, (de.Value + prev) / 2);
				}
				prev = de.Value;
				isFirst = false;
			}
			prognoz.calcPrognoz();
			processAnswer();
			

			prognoz.Prognoz.Add(datePrognozStart, prognoz.FirstData.Last().Value.NB);
			prognoz.Rashods.Add(datePrognozStart, prognoz.FirstData.Last().Value.Q);
			prognoz.Napors.Add(datePrognozStart, prognoz.FirstData.Last().Value.VB - prognoz.FirstData.Last().Value.NB);
			PrognozAnswer.Chart = getChart();
		}
	}
}
