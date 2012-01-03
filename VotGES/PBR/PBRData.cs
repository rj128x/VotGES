using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using VotGES.Piramida;

namespace VotGES.PBR
{
	class PBRData
	{
		public DateTime DateStart{get;protected set;}
		public DateTime DateEnd{get;protected set;}
		public DateTime Date { get; protected set; }

		public int GTPIndex { get; protected set; }

		public SortedList<DateTime, double> RealPBR{get;protected set;}
		public SortedList<DateTime, double> SteppedPBR{get;protected set;}
		public SortedList<DateTime, double> MinutesPBR{get;protected set;}
		public SortedList<DateTime,double> RealP{get;protected set;}

		public SortedList<DateTime, double> IntegratedP { get; protected set; }
		public SortedList<DateTime, double> IntegratedPBR { get; protected set; }



		public PBRData(DateTime dateStart, DateTime dateEnd, DateTime date, int gtpIndex) {
			DateStart = dateStart.Date.AddHours(dateStart.Hour);
			DateEnd = dateEnd.Date.AddHours(dateEnd.Hour);
			Date = date;
			GTPIndex = gtpIndex;
			RealPBR = new SortedList<DateTime, double>();
			SteppedPBR = new SortedList<DateTime, double>();
			MinutesPBR = new SortedList<DateTime, double>();
			RealP = new SortedList<DateTime, double>();
			IntegratedP = new SortedList<DateTime, double>();
			IntegratedPBR = new SortedList<DateTime, double>();
		}

		public void readData() {
			int item=GTPIndex + 1;
			Piramida3000Entities model=PiramidaAccess.getModel();


			IQueryable<DATA> dataArr=from DATA d in model.DATA
											 where
												d.DATA_DATE >= DateStart && d.DATA_DATE <= DateEnd &&
												d.ITEM == item && d.OBJECT == 0 && d.OBJTYPE == 2 &&
												(d.PARNUMBER == 4 || d.PARNUMBER == 212) select d;
			foreach (DATA data in dataArr) {
				try {
					DateTime date=data.DATA_DATE;
					double val=data.VALUE0.Value / 1000;
					switch (data.PARNUMBER) {
						case 4:
							if (date > DateStart) {
								RealP.Add(date, val);
								Date = date;
							}
							break;
						case 212:
							if (date.Minute == 0) {
								RealPBR.Add(date, val);
							}
							break;
					}
				} catch (Exception e) {
					Logger.Info("Ошибка при чтении ПБР " + e.ToString());
				}
			}						
			
		}


		public void checkData() {
			DateTime date=DateStart.AddMinutes(1);
			Random r=new Random();
			while (date <= Date) {
				if (!RealP.Keys.Contains(date)) {
					if (RealP.Keys.Contains(date.AddMinutes(-1))) {
						RealP.Add(date, RealP[date.AddMinutes(-1)]);
					} else {
						RealP.Add(date, -1);
						//Logger.Info("Не записана мощность " + date.ToString());
						//RealP.Add(date, GTPIndex * 100 + 100 + r.Next(-10, 10));
					}
				}
				date = date.AddMinutes(1);
			}

			date = DateStart.AddMinutes(0);
			while (date <= DateEnd) {
				if (!RealPBR.Keys.Contains(date)) {
					if (RealPBR.Keys.Contains(date.AddMinutes(-60))) {
						RealP.Add(date, RealPBR[date.AddMinutes(-60)]);
					} else {
						RealPBR.Add(date, -1);
						Logger.Info("Не записан ПБР " + date.ToString());
					}
				}
				date = date.AddMinutes(60);
			}

			date = DateStart.AddMinutes(30);
			while (date <= DateEnd) {
				DateTime prevDate=date.AddMinutes(-30);
				DateTime nextDate=date.AddMinutes(30);
				if (RealPBR.Keys.Contains(prevDate) && (RealPBR.Keys.Contains(nextDate))) {
					RealPBR.Add(date, (RealPBR[prevDate] + RealPBR[nextDate]) / 2);
				} else {
					RealPBR.Add(date, -1);
					Logger.Info("Ошибка при формировании получасовок");
				}
				date = date.AddMinutes(60);
			}
			
		}

		public void createSteppedPBR() {
			DateTime LastPBRDate=RealPBR.Last().Key;
			DateTime FirstPBRDate=RealPBR.First().Key;
			foreach (KeyValuePair<DateTime, double> de in RealPBR) {
				DateTime date=de.Key.AddMinutes(-15);
				if (de.Key == FirstPBRDate)
					date = DateStart;
				
				SteppedPBR.Add(date, de.Value);
				if (de.Key==LastPBRDate){
					SteppedPBR.Add(DateEnd,de.Value);
				}
			}
			
		}

		public void createMinutesPBR() {
			DateTime date=DateStart;
			double val=0;
			Random r=new Random();
			while (date < DateEnd) {
				if (SteppedPBR.Keys.Contains(date)) {
					val = SteppedPBR[date];				
				}				
				MinutesPBR.Add(date.AddMinutes(1), val);
				RealP[date.AddMinutes(1)] = val + r.Next(-3, 3);
				date = date.AddMinutes(1);
			}
		}

		public void createIntegratedValues() {
			double sum=0;
			foreach (KeyValuePair<DateTime,double> de in MinutesPBR) {
				sum += de.Value;
				IntegratedPBR.Add(de.Key, sum / 60);
			}

			sum=0;
			foreach (KeyValuePair<DateTime,double> de in RealP) {
				sum += de.Value;
				IntegratedP.Add(de.Key, sum / 60);
			}
		}

		public void InitData() {
			readData();
			checkData();
			createSteppedPBR();
			createMinutesPBR();
			createIntegratedValues();
		}
				
	}
}

