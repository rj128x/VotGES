using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using VotGES.Piramida;

namespace VotGES.PBR
{
	class PBRDataHH
	{
		public DateTime DateStart{get;protected set;}
		public DateTime DateEnd{get;protected set;}

		public int GTPIndex { get; protected set; }

		public SortedList<DateTime, double> RealPBR{get;protected set;}
		public SortedList<DateTime, double> HalfHoursP { get; protected set; }
		public SortedList<DateTime, double> HoursP { get; protected set; }

		public SortedList<DateTime, double> HoursPBR { get; protected set; }
		public SortedList<DateTime, double> HalfHoursPBR { get; protected set; }




		public PBRDataHH(DateTime dateStart, DateTime dateEnd, int gtpIndex) {
			DateStart = dateStart.Date.AddHours(dateStart.Hour);
			DateEnd = dateEnd.Date.AddHours(dateEnd.Hour);
			GTPIndex = gtpIndex;
			RealPBR = new SortedList<DateTime, double>();
			HalfHoursP = new SortedList<DateTime, double>();
			HoursP = new SortedList<DateTime, double>();

			HalfHoursPBR = new SortedList<DateTime, double>();
			HoursPBR = new SortedList<DateTime, double>();
		}

		public void readData() {
			int item=GTPIndex + 1;
			Piramida3000Entities model=PiramidaAccess.getModel();


			IQueryable<DATA> dataArr=from DATA d in model.DATA
											 where
												d.DATA_DATE >= DateStart && d.DATA_DATE <= DateEnd &&
												d.ITEM == item && d.OBJECT == 0 && d.OBJTYPE == 2 &&
												(d.PARNUMBER == 12 || d.PARNUMBER == 212) select d;
			foreach (DATA data in dataArr) {
				try {
					DateTime date=data.DATA_DATE;
					double val=data.VALUE0.Value / 1000;
					switch (data.PARNUMBER) {
						case 12:
							if (date > DateStart) {
								HalfHoursP.Add(date, val);
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
			DateTime date=DateStart.AddMinutes(30);
			while (date <= DateEnd) {
				if (!HalfHoursP.Keys.Contains(date)) {
					if (HalfHoursP.Keys.Contains(date.AddMinutes(-30))) {
						HalfHoursP.Add(date, HalfHoursP[date.AddMinutes(-30)]);
					} else {
						HalfHoursP.Add(date, -1);
						Logger.Info("Не записана мощность " + date.ToString());
						//RealP.Add(date, GTPIndex * 100 + 100 + r.Next(-10, 10));
					}
				}
				date = date.AddMinutes(30);
			}

			date = DateStart.AddMinutes(0);
			while (date <= DateEnd) {
				if (!RealPBR.Keys.Contains(date)) {
					if (RealPBR.Keys.Contains(date.AddMinutes(-60))) {
						RealPBR.Add(date, RealPBR[date.AddMinutes(-60)]);
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

			date = DateStart.AddMinutes(30);
			while (date <= DateEnd) {
				DateTime prevDate=date.AddMinutes(-30);
				if (RealPBR.Keys.Contains(prevDate) ) {
					HalfHoursPBR.Add(date, (RealPBR[prevDate] + RealPBR[date]) / 2);
				} else {
					HalfHoursPBR.Add(date, -1);
					Logger.Info("Ошибка при формировании получасовок");
				}
				date = date.AddMinutes(30);
			}

			date = DateStart.AddMinutes(60);
			while (date <= DateEnd) {
				DateTime prevDate=date.AddMinutes(-30);
				if (HalfHoursPBR.Keys.Contains(prevDate)) {
					HoursPBR.Add(date, (HalfHoursPBR[prevDate] + HalfHoursPBR[date]) / 2);
				} else {
					HoursPBR.Add(date, -1);
					Logger.Info("Ошибка при формировании получасовок");
				}

				if (HalfHoursP.Keys.Contains(prevDate)) {
					HoursP.Add(date, (HalfHoursP[prevDate] + HalfHoursP[date]) / 2);
				} else {
					HoursP.Add(date, -1);
					Logger.Info("Ошибка при формировании получасовок");
				}
				date = date.AddMinutes(60);
			}
		}

			

		public void InitData() {
			readData();
			checkData();
		}
				
	}
}

