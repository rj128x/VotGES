using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using VotGES.Piramida;
using VotGES.Chart;

namespace VotGES.PrognozNB
{
	public class PrognozNBFunc
	{
		protected DateTime dateStart;
		public DateTime DateStart {
			get { return dateStart; }
			set { dateStart = value; }
		}

		protected DateTime dateEnd;
		public DateTime DateEnd {
			get { return dateEnd; }
			set { dateEnd = value; }
		}

		protected DateTime datePrognozStart;
		public DateTime DatePrognozStart {
			get { return datePrognozStart; }
			set { datePrognozStart = value; }
		}

		protected int daysCount;
		public int DaysCount {
			get { return daysCount; }
			set { daysCount = value; }
		}

		protected SortedList<DateTime,double> pbr;
		public SortedList<DateTime, double> PBR {
			get { return pbr; }
			set { pbr = value; }
		}

		protected SortedList<DateTime,double> nbFakt;
		public SortedList<DateTime, double> NBFakt {
			get { return nbFakt; }
			set { nbFakt = value; }
		}

		protected SortedList<DateTime,double> naporFakt;
		public SortedList<DateTime, double> NaporFakt {
			get { return naporFakt; }
			set { naporFakt = value; }
		}

		protected SortedList<DateTime,double> vbFakt;
		public SortedList<DateTime, double> VBFakt {
			get { return vbFakt; }
			set { vbFakt = value; }
		}

		protected SortedList<DateTime,double> qFakt;
		public SortedList<DateTime, double> QFakt {
			get { return qFakt; }
			set { qFakt = value; }
		}

		protected SortedList<DateTime,double> tFakt;
		public SortedList<DateTime, double> TFakt {
			get { return tFakt; }
			set { tFakt = value; }
		}

		protected SortedList<DateTime,double> pFakt;
		public SortedList<DateTime, double> PFakt {
			get { return pFakt; }
			set { pFakt = value; }
		}

		protected double t;
		public double T {
			get { return t; }
			set { t = value; }
		}

		protected PrognozNB prognoz;
		public PrognozNB Prognoz {
			get { return prognoz; }
			set { prognoz = value; }
		}


		protected double TSum { get; set; }
		protected double TCount { get; set; }

		public PrognozNBFunc(DateTime dateStart, int daysCount) {
			DateStart = dateStart.Date;
			DaysCount = daysCount;
			DateEnd = DateStart.AddDays(daysCount);
			pbr = new SortedList<DateTime, double>();
			nbFakt = new SortedList<DateTime, double>();
			vbFakt = new SortedList<DateTime, double>();
			qFakt = new SortedList<DateTime, double>();
			pFakt = new SortedList<DateTime, double>();
			tFakt = new SortedList<DateTime, double>();
			naporFakt = new SortedList<DateTime, double>();
			TSum = 0;
			TCount = 0;
		}



		public void readPBR() {
			/*IQueryable<DATA> dataArr=from DATA d in model.DATA where
													 d.PARNUMBER == 212 && d.DATA_DATE >= DateStart && d.DATA_DATE <= dateEnd &&
													 d.OBJTYPE == 2 && d.OBJECT == 0 && d.ITEM == 1 select d;*/
			List<PiramidaEnrty> dataArr=PiramidaAccess.GetDataFromDB(DateStart, DateEnd, 0, 2, 212, (new int[] { 1 }).ToList<int>(), false, true);
			foreach (PiramidaEnrty data in dataArr) {
				if (!pbr.Keys.Contains(data.Date)) {
					pbr.Add(data.Date, data.Value0 / 1000);
				}
			}
		}

		public void readP() {
			/*IQueryable<DATA> dataArr=from DATA d in model.DATA where
													 d.PARNUMBER == 12 && d.DATA_DATE > DateStart && d.DATA_DATE <= dateEnd &&
													 d.OBJTYPE == 2 && d.OBJECT == 0 && d.ITEM == 1 select d;*/
			List<PiramidaEnrty> dataArr=PiramidaAccess.GetDataFromDB(DateStart, DateEnd, 0, 2, 12, (new int[] { 1 }).ToList<int>(), false, true);
			foreach (PiramidaEnrty data in dataArr) {
				if (!pFakt.Keys.Contains(data.Date)) {
					pFakt.Add(data.Date, data.Value0/ 1000);
				}
			}
		}

		public void readWater() {
			int[] items=new int[] { 354, 276,373,275,274 };
			List<int> il=items.ToList();			
			/*IQueryable<DATA> dataArr=from DATA d in model.DATA where
													 d.PARNUMBER == 12 && d.DATA_DATE > DateStart && d.DATA_DATE <= dateEnd &&
													 d.OBJTYPE == 2 && d.OBJECT == 1 && il.Contains(d.ITEM) select d;*/
			List<PiramidaEnrty> dataArr=PiramidaAccess.GetDataFromDB(DateStart, DateEnd, 1, 2, 12, il, false, true);

			foreach (PiramidaEnrty data in dataArr) {
				switch (data.Item) {
					case 354:
						if (!qFakt.Keys.Contains(data.Date)) {
							qFakt.Add(data.Date, data.Value0);
						}
						break;
					case 276:
						if (!naporFakt.Keys.Contains(data.Date)) {
							naporFakt.Add(data.Date, data.Value0);
						}
						break;
					case 275:
						if (!nbFakt.Keys.Contains(data.Date)) {
							nbFakt.Add(data.Date, data.Value0);
						}
						break;
					case 274:
						if (!vbFakt.Keys.Contains(data.Date)) {
							vbFakt.Add(data.Date, data.Value0);
						}
						break;
					case 373:
						if (!tFakt.Keys.Contains(data.Date)) {
							tFakt.Add(data.Date, data.Value0);
							TSum += data.Value0;
							TCount++;
						}
						break;
				}
			}
		}

		public void checkData(DateTime dateStart, DateTime dateEnd) {
			DateTime date=dateStart.AddMinutes(30);
			while (date <= dateEnd) {
				if (!nbFakt.Keys.Contains(date)||nbFakt[date]==0) {
					if (nbFakt.Keys.Contains(date))
						nbFakt.Remove(date);
					if (nbFakt.Keys.Contains(date.AddMinutes(-30)))
						nbFakt.Add(date,nbFakt[date.AddMinutes(-30)]);
					else nbFakt.Add(date,66);
				}
				if (!vbFakt.Keys.Contains(date) || vbFakt[date] == 0) {
					if (vbFakt.Keys.Contains(date))
						vbFakt.Remove(date);
					if (vbFakt.Keys.Contains(date.AddMinutes(-30)))
						vbFakt.Add(date, nbFakt[date.AddMinutes(-30)]);
					else vbFakt.Add(date, 87);
				}
				if (!tFakt.Keys.Contains(date) || tFakt[date] == 0) {
					if (tFakt.Keys.Contains(date))
						tFakt.Remove(date);
					if (tFakt.Keys.Contains(date.AddMinutes(-30)))
						tFakt.Add(date, tFakt[date.AddMinutes(-30)]);
					else tFakt.Add(date, 0);
				}
				if (!naporFakt.Keys.Contains(date) || naporFakt[date] == 0) {
					if (naporFakt.Keys.Contains(date))
						naporFakt.Remove(date);
					naporFakt.Add(date, vbFakt[date] - nbFakt[date]);
				}
				if (!pFakt.Keys.Contains(date)) {
					if (pFakt.Keys.Contains(date.AddMinutes(-30)))
						pFakt.Add(date, pFakt[date.AddMinutes(-30)]);
					else pFakt.Add(date, 100);
				}
				if (!qFakt.Keys.Contains(date) || (qFakt[date] == 0 && pFakt[date]>0)) {
					if (qFakt.Keys.Contains(date))
						qFakt.Remove(date);
					qFakt.Add(date,RashodTable.getStationRashod(pFakt[date],naporFakt[date],RashodCalcMode.avg));
				}
				date = date.AddMinutes(30);
			}
		}

		public virtual  SortedList<DateTime,PrognozNBFirstData> readFirstData(DateTime date) {
			
			int[] items=new int[] { 354, 276, 373, 275, 274 };
			List<int> il=items.ToList();
			DateTime ds=date.AddHours(-2);
			DateTime de=date.AddHours(0);
			/*IQueryable<DATA> dataArr=from DATA d in model.DATA where
													 d.PARNUMBER == 12 && d.DATA_DATE >= ds && d.DATA_DATE <= de &&
													 d.OBJTYPE == 2 && (d.OBJECT == 1 && il.Contains(d.ITEM)|| d.OBJECT==0 && d.ITEM==1) select d;*/
			List<PiramidaEnrty> dataArrW = PiramidaAccess.GetDataFromDB(ds, de, 1, 2, 12, il, true, true);
			List<PiramidaEnrty> dataArrP = PiramidaAccess.GetDataFromDB(ds, de, 0, 2, 12, (new int[] { 1 }).ToList<int>(), true, true);
			List<PiramidaEnrty> dataArr = new List<PiramidaEnrty>();
			foreach (PiramidaEnrty entry in dataArrW) {
				dataArr.Add(entry);
			}
			foreach (PiramidaEnrty entry in dataArrP) {
				dataArr.Add(entry);
			}
			return processFirstData(dataArr);
		}

		protected SortedList<DateTime, PrognozNBFirstData> processFirstData(List<PiramidaEnrty> dataArr) {
			SortedList<DateTime,PrognozNBFirstData> firstData=new SortedList<DateTime, PrognozNBFirstData>();
			foreach (PiramidaEnrty data in dataArr) {
				if (!firstData.Keys.Contains(data.Date)) {
					PrognozNBFirstData newData=new PrognozNBFirstData();
					newData.Date = data.Date;
					firstData.Add(data.Date, newData);
				}

				switch (data.Item) {
					case 1:						
						firstData[data.Date].P = data.Value0/1000;
						break;
					case 354:
						firstData[data.Date].Q = data.Value0;
						break;
					case 275:
						firstData[data.Date].NB = data.Value0;
						break;
					case 274:
						firstData[data.Date].VB = data.Value0;
						break;
					case 373:
						firstData[data.Date].T = data.Value0;
						if (data.Date <= DateStart) {
							TSum += data.Value0;
							TCount++;
						}
						break;
				}
			}
			return firstData;
		}

		

		public virtual void writeFaktData(ChartData data) {
			ChartDataSerie nbFaktSerie=new ChartDataSerie();
			nbFaktSerie.Name = "NBFakt";
			foreach (KeyValuePair<DateTime,double> de in NBFakt){
				nbFaktSerie.Points.Add(new ChartDataPoint(de.Key, de.Value));
			}
			data.addSerie(nbFaktSerie);

			ChartDataSerie pFaktSerie=new ChartDataSerie();
			pFaktSerie.Name = "PFakt";
			foreach (KeyValuePair<DateTime,double> de in PFakt) {
				pFaktSerie.Points.Add(new ChartDataPoint(de.Key, de.Value));
			}
			data.addSerie(pFaktSerie);

			ChartDataSerie pbrSerie=new ChartDataSerie();
			pbrSerie.Name = "PBR";
			foreach (KeyValuePair<DateTime,double> de in PBR) {
				pbrSerie.Points.Add(new ChartDataPoint(de.Key, de.Value));
			}
			data.addSerie(pbrSerie);

			ChartDataSerie qFaktSerie=new ChartDataSerie();
			qFaktSerie.Name = "QFakt";
			foreach (KeyValuePair<DateTime,double> de in QFakt) {
				qFaktSerie.Points.Add(new ChartDataPoint(de.Key, de.Value));
			}
			data.addSerie(qFaktSerie);

			ChartDataSerie naporFaktSerie=new ChartDataSerie();
			naporFaktSerie.Name = "Napor";
			foreach (KeyValuePair<DateTime,double> de in NaporFakt) {
				naporFaktSerie.Points.Add(new ChartDataPoint(de.Key, de.Value));
			}
			data.addSerie(naporFaktSerie);

			ChartDataSerie vbFaktSerie=new ChartDataSerie();
			vbFaktSerie.Name = "VB";
			foreach (KeyValuePair<DateTime,double> de in VBFakt) {
				vbFaktSerie.Points.Add(new ChartDataPoint(de.Key, de.Value));
			}
			data.addSerie(vbFaktSerie);

			ChartDataSerie tFaktSerie=new ChartDataSerie();
			tFaktSerie.Name = "T";
			foreach (KeyValuePair<DateTime,double> de in TFakt) {
				tFaktSerie.Points.Add(new ChartDataPoint(de.Key, de.Value));
			}
			data.addSerie(tFaktSerie);

			

			
		}

		public virtual ChartAnswer getChart() {
			ChartAnswer answer=new ChartAnswer();
			answer.Data = new ChartData();
			writeFaktData(answer.Data);
			Prognoz.AddChartData(answer.Data);
			answer.Properties = Prognoz.createChartProperties();
			return answer;
		}
	}
}
