using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using VotGES.Piramida;

namespace VotGES.PBR
{
	class PBRData
	{
		public DateTime DateStart { get; protected set; }
		public DateTime DateEnd { get; protected set; }
		public DateTime Date { get; protected set; }

		public int GTPIndex { get; protected set; }

		public SortedList<DateTime, double> RealPBR { get; protected set; }
		public SortedList<DateTime, double> SteppedPBR { get; protected set; }
		public SortedList<DateTime, double> MinutesPBR { get; protected set; }
		public SortedList<DateTime, double> RealP { get; protected set; }

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

			List<int> items=(new int[] {item}).ToList<int>();
			List<PiramidaEnrty> dataPBR=PiramidaAccess.GetDataFromDB(DateStart, DateEnd, 0, 2, 212, items, true, true);
			foreach (PiramidaEnrty data in dataPBR) {
				try {
					DateTime date=data.Date;
					double val=data.Value0 / 1000;
					if (date.Minute == 0) {
						RealPBR.Add(date, val);
					}						
				} catch (Exception e) {
					Logger.Info("Ошибка при чтении ПБР " + e.ToString());
				}
			}



			List<PiramidaEnrty> dataFakt=PiramidaAccess.GetDataFromDB(DateStart, DateEnd, 0, 2, 4, items, true, true,true);
			foreach (PiramidaEnrty data in dataFakt) {
				try {
					DateTime date=data.Date;
					double val=data.Value0 / 1000;
					if (date > DateStart) {
						RealP.Add(date, val);
						Date = date;
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
				if (de.Key == LastPBRDate) {
					SteppedPBR.Add(DateEnd, de.Value);
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
				//RealP[date.AddMinutes(1)] = val + r.Next(-3, 3);
				date = date.AddMinutes(1);
			}
		}

		public void createIntegratedValues() {
			double sum=0;
			foreach (KeyValuePair<DateTime,double> de in MinutesPBR) {
				sum += de.Value;
				IntegratedPBR.Add(de.Key, sum / 60);
			}

			sum = 0;
			foreach (KeyValuePair<DateTime,double> de in RealP) {
				sum += de.Value;
				IntegratedP.Add(de.Key, sum / 60);
			}
		}

		public double getDiff(DateTime date) {
			return RealP[date] - MinutesPBR[date];
		}

		public static double getDiffProc(double fakt, double plan) {
			if (plan > 0) {
				return (fakt - plan) / plan * 100;
			} else {
				if (fakt == 0)
					return 0;
				else
					return 100;
			}
		}

		public double getDiffProc(DateTime date) {
			return getDiffProc(MinutesPBR[date], RealP[date]);
		}

		public static double getAvgHour(DateTime date, SortedList<DateTime, double> data) {
			date = date.AddMinutes(-1);
			DateTime ds=date.Date.AddHours(date.Hour);
			DateTime de=date.Date.AddHours(date.Hour + 1);
			DateTime dt=ds.AddMinutes(1);

			de = de > date ? date : de;

			double sum=0;
			double count=0;
			while (dt <= de) {
				sum += data[dt];
				count++;
				dt = dt.AddMinutes(1);
			}
			return sum / count;
		}

		public SortedList<string, double> getHourVals(DateTime date) {
			SortedList<string,double> result=new SortedList<string, double>();
			double fakt=getAvgHour(date, RealP);
			double plan=getAvgHour(date, MinutesPBR);
			double diff=fakt - plan;
			double diffProc=getDiffProc(fakt, plan);

			DateTime dt=date.AddMinutes(1);
			double sum=0;
			int count=0;
			while (dt.Hour == date.Hour) {
				sum += MinutesPBR[dt];
				count++;
				dt = dt.AddMinutes(1);
			}

			double recP=(sum - diff * date.Minute) / count;

			result.Add("fakt", fakt);
			result.Add("plan", plan);
			result.Add("diff", diff);
			result.Add("diffProc", diffProc);
			result.Add("recP", recP);
			return result;

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

