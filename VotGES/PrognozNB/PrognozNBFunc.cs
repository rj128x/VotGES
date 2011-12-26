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


		public PrognozNBFunc(DateTime dateStart, int daysCount) {
			DateStart = dateStart.Date;
			DaysCount = daysCount;
			DateEnd = DateStart.AddDays(daysCount);
			pbr = new SortedList<DateTime, double>();
			nbFakt = new SortedList<DateTime, double>();
			vbFakt = new SortedList<DateTime, double>();
			qFakt = new SortedList<DateTime, double>();
			pFakt = new SortedList<DateTime, double>();
			naporFakt = new SortedList<DateTime, double>();
		}



		public void readPBR() {
			Piramida3000Entities model=PiramidaAccess.getModel();
			IQueryable<DATA> dataArr=from DATA d in model.DATA where
													 d.PARNUMBER == 212 && d.DATA_DATE >= DateStart && d.DATA_DATE <= dateEnd &&
													 d.OBJTYPE == 2 && d.OBJECT == 0 && d.ITEM == 1 select d;
			foreach (DATA data in dataArr) {
				pbr.Add(data.DATA_DATE, data.VALUE0.Value/1000);
			}
		}

		public void readP() {
			Piramida3000Entities model=PiramidaAccess.getModel();
			IQueryable<DATA> dataArr=from DATA d in model.DATA where
													 d.PARNUMBER == 12 && d.DATA_DATE > DateStart && d.DATA_DATE <= dateEnd &&
													 d.OBJTYPE == 2 && d.OBJECT == 0 && d.ITEM == 1 select d;
			foreach (DATA data in dataArr) {
				pFakt.Add(data.DATA_DATE, data.VALUE0.Value/1000);
			}
		}

		public void readWater() {
			Piramida3000Entities model=PiramidaAccess.getModel();
			int[] items=new int[] { 354, 276,373,275,274 };
			List<int> il=items.ToList();			
			IQueryable<DATA> dataArr=from DATA d in model.DATA where
													 d.PARNUMBER == 12 && d.DATA_DATE > DateStart && d.DATA_DATE <= dateEnd &&
													 d.OBJTYPE == 2 && d.OBJECT == 1 && il.Contains(d.ITEM) select d;
			foreach (DATA data in dataArr) {
				switch (data.ITEM) {
					case 354:
						qFakt.Add(data.DATA_DATE, data.VALUE0.Value);
						break;
					case 276:
						naporFakt.Add(data.DATA_DATE, data.VALUE0.Value);
						break;
					case 275:
						nbFakt.Add(data.DATA_DATE, data.VALUE0.Value);
						break;
					case 274:
						vbFakt.Add(data.DATA_DATE, data.VALUE0.Value);
						break;
					case 373:
						T = data.VALUE0.Value;
						break;
				}
			}
		}

		public virtual  SortedList<DateTime,PrognozNBFirstData> readFirstData(DateTime date) {
			
			Piramida3000Entities model=PiramidaAccess.getModel();
			int[] items=new int[] { 354, 276, 373, 275, 274 };
			List<int> il=items.ToList();
			DateTime ds=date.AddHours(-2);
			DateTime de=date.AddHours(0);
			IQueryable<DATA> dataArr=from DATA d in model.DATA where
													 d.PARNUMBER == 12 && d.DATA_DATE >= ds && d.DATA_DATE <= de &&
													 d.OBJTYPE == 2 && (d.OBJECT == 1 && il.Contains(d.ITEM)|| d.OBJECT==0 && d.ITEM==1) select d;
			return processFirstData(dataArr);
		}

		protected SortedList<DateTime, PrognozNBFirstData> processFirstData(IQueryable<DATA> dataArr) {
			SortedList<DateTime,PrognozNBFirstData> firstData=new SortedList<DateTime, PrognozNBFirstData>();
			foreach (DATA data in dataArr) {
				if (!firstData.Keys.Contains(data.DATA_DATE)) {
					PrognozNBFirstData newData=new PrognozNBFirstData();
					newData.Date = data.DATA_DATE;
					firstData.Add(data.DATA_DATE, newData);
				}

				switch (data.ITEM) {
					case 1:
						firstData[data.DATA_DATE].P = data.VALUE0.Value/1000;
						break;
					case 354:
						firstData[data.DATA_DATE].Q = data.VALUE0.Value;
						break;
					case 275:
						firstData[data.DATA_DATE].NB = data.VALUE0.Value;
						break;
					case 274:
						firstData[data.DATA_DATE].VB = data.VALUE0.Value;
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
			data.Series.Add(nbFaktSerie);

			ChartDataSerie pFaktSerie=new ChartDataSerie();
			pFaktSerie.Name = "PFakt";
			foreach (KeyValuePair<DateTime,double> de in PFakt) {
				pFaktSerie.Points.Add(new ChartDataPoint(de.Key, de.Value));
			}
			data.Series.Add(pFaktSerie);

			ChartDataSerie pbrSerie=new ChartDataSerie();
			pbrSerie.Name = "PBR";
			foreach (KeyValuePair<DateTime,double> de in PBR) {
				pbrSerie.Points.Add(new ChartDataPoint(de.Key, de.Value));
			}
			data.Series.Add(pbrSerie);

			ChartDataSerie qFaktSerie=new ChartDataSerie();
			qFaktSerie.Name = "QFakt";
			foreach (KeyValuePair<DateTime,double> de in QFakt) {
				qFaktSerie.Points.Add(new ChartDataPoint(de.Key, de.Value));
			}
			data.Series.Add(qFaktSerie);

			ChartDataSerie naporFaktSerie=new ChartDataSerie();
			naporFaktSerie.Name = "Napor";
			foreach (KeyValuePair<DateTime,double> de in NaporFakt) {
				naporFaktSerie.Points.Add(new ChartDataPoint(de.Key, de.Value));
			}
			data.Series.Add(naporFaktSerie);

			ChartDataSerie vbFaktSerie=new ChartDataSerie();
			vbFaktSerie.Name = "VB";
			foreach (KeyValuePair<DateTime,double> de in VBFakt) {
				vbFaktSerie.Points.Add(new ChartDataPoint(de.Key, de.Value));
			}
			data.Series.Add(vbFaktSerie);

			

			

			
		}

		public ChartAnswer getChart() {
			ChartAnswer answer=new ChartAnswer();
			answer.Data = new ChartData();
			writeFaktData(answer.Data);
			Prognoz.AddChartData(answer.Data);
			answer.Properties = Prognoz.createChartProperties();
			return answer;
		}
	}
}
