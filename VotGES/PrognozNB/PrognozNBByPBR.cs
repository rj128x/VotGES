using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using VotGES.Piramida;
using VotGES.Chart;

namespace VotGES.PrognozNB
{
	public class PrognozNBByPBR:PrognozNBFunc
	{
		protected DateTime datePrognozStart;
		public DateTime DatePrognozStart {
			get { return datePrognozStart; }
			set { datePrognozStart = value; }
		}

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

		public PrognozNBByPBR(DateTime dateStart, int daysCount, DateTime datePrognozStart, SortedList<DateTime,double> userPBR)
			: base(dateStart, daysCount) {
			DateStart = dateStart.Date;
			DaysCount = daysCount;
			DateEnd = DateStart.AddDays(daysCount);

			DatePrognozStart = datePrognozStart;
			int hour=DatePrognozStart.Hour;
			int min=DatePrognozStart.Minute;
			min=min<30?0:min;
			min=min>30?30:min;
			DatePrognozStart = DatePrognozStart.Date.AddHours(hour).AddMinutes(min);
			UserPBR = userPBR;
			
			
		}

		public void readM53500() {
			Piramida3000Entities model=PiramidaAccess.getModel();
			IQueryable<DATA> dataArr=from DATA d in model.DATA where
													 d.PARNUMBER == 212 && d.DATA_DATE > DateStart && d.DATA_DATE <= dateEnd &&
													 d.OBJTYPE == 2 && d.OBJECT == 53500 && d.ITEM == 1 select d;
			foreach (DATA data in dataArr) {
				pbr.Add(data.DATA_DATE, data.VALUE0.Value / 1000);
			}
		}


		public void readPBRPrevSutki() {
			Piramida3000Entities model=PiramidaAccess.getModel();
			DateTime ds=DatePrognozStart;
			DateTime de=DatePrognozStart.AddDays(-1);
			IQueryable<DATA> dataArr=from DATA d in model.DATA where
													 d.PARNUMBER == 212 && d.DATA_DATE > DateStart && d.DATA_DATE <= dateEnd &&
													 d.OBJTYPE == 2 && d.OBJECT == 0 && d.ITEM == 1 select d;
			foreach (DATA data in dataArr) {
				pbrPrevSutki.Add(data.DATA_DATE, data.VALUE0.Value / 1000);
			}
		}

		public void preparePArr() {
			pbrFull=new SortedList<DateTime,double>();
			DateTime date=datePrognozStart.AddMinutes(0);
			while (date <= dateEnd) {
				if (userPBR != null && userPBR.Keys.Contains(date)) {
					pbrFull.Add(date,userPBR[date]);
				}else{
					if (pbr.Keys.Contains(date)){
						pbrFull.Add(date,pbr[date]);
					}else if (M53500.Keys.Contains(date)){
						pbrFull.Add(date,M53500[date]);
					} else if (pbrFull.Keys.Contains(date.AddHours(-24))) {
						pbrFull.Add(date,pbrFull[date.AddHours(-24)]);
					}else if (PBRPrevSutki.Keys.Contains(date.AddHours(-24))){
						pbrFull.Add(date,PBRPrevSutki[date.AddHours(-24)]);
					}else if (pbrFull.Keys.Contains(date.AddMinutes(-30))){
						pbrFull.Add(date,pbrFull[date.AddMinutes(-30)]);
					}else{
						pbrFull.Add(date,0);
					}
				}
				date = date.AddMinutes(30);
			}
		}

		public override SortedList<DateTime, PrognozNBFirstData> readFirstData(DateTime date) {
			date = DatePrognozStart.AddMinutes(0);
			int[] items=new int[] { 354, 276, 373, 275, 274 };
			Piramida3000Entities model=PiramidaAccess.getModel();
			IQueryable<DATA> dataArr=null;
			List<int> il=items.ToList();
			double cnt=0;
			int index=0;
			while (cnt < 30 && index<=10) {
				DateTime ds=date.AddHours(-2);
				DateTime de=date.AddHours(0);
				dataArr=from DATA d in model.DATA where
														 d.PARNUMBER == 12 && d.DATA_DATE >= ds && d.DATA_DATE <= de &&
														 d.OBJTYPE == 2 && (d.OBJECT == 1 && il.Contains(d.ITEM) || d.OBJECT == 0 && d.ITEM == 1) select d;
				cnt = dataArr.Count();
				Logger.Info(cnt.ToString());
				date = date.AddMinutes(-30);
				index++;
			}
			DatePrognozStart = date.AddMinutes(30);
			return processFirstData(dataArr);
		}

		public override void writeFaktData(Chart.ChartData data) {
			base.writeFaktData(data);

			foreach (ChartDataSerie serie in data.Series) {
				if (serie.Name == "PBR") {
					serie.Points.Clear();
					foreach (KeyValuePair<DateTime,double> de in PBR) {
						serie.Points.Add(new ChartDataPoint(de.Key, de.Value));
					}
				}
			}
		}

		public void startPrognoz() {
			prognoz = new PrognozNB();

			prognoz.FirstData = readFirstData(DatePrognozStart);
			readP();
			readPBR();
			readWater();
			preparePArr();

			prognoz.DatePrognozStart = DatePrognozStart;
			prognoz.DatePrognozEnd = DateEnd;
			prognoz.T = T;
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
		}
	}
}
