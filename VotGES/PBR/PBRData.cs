using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using VotGES.Piramida;

namespace PILib.PBR
{
	public class PBRData
	{
		public DateTime DateStart { get; protected set; }
		public DateTime DateEnd { get; protected set; }
		public int GTPIndex { get; protected set; }
		public SortedList<DateTime, double> Data30 { get; protected set; }
		public SortedList<DateTime, double> ProcessData30 { get; protected set; }
		public SortedList<DateTime, double> ProcessData1 { get; protected set; }
		public bool Integrate { get; set; }
		public double Scale { get; set; }

		public PBRData(int GTPIndex,DateTime dateStart, DateTime dateEnd,bool integrate=false, double scale=1,Dictionary<DateTime,double> userPBR=null) {
			this.DateStart = dateStart;
			this.DateEnd = dateEnd;
			this.GTPIndex = GTPIndex;
			this.Integrate = integrate;
			this.Scale = scale;
			if (userPBR == null) {
				readData();
			} else {
				convertUserPBR(userPBR);
			}
			processData();
		}

		protected void convertUserPBR(Dictionary<DateTime, double> userPBR) {
			bool first=true;
			double prevVal=0;
			Data30 = new SortedList<DateTime, double>();
			foreach (KeyValuePair<DateTime,double> de in userPBR) {
				double val= de.Value * Scale;
				Data30.Add(de.Key, de.Value * Scale);
				if (!first) {
					double mid=(val + prevVal) / 2.0;
					Data30.Add(de.Key.AddMinutes(-30), mid);
				}
				prevVal = val;
				first = false;
			}
			longData(DateStart, DateEnd);
		}

		protected void readData() {
			int index=0;
			switch (GTPIndex) {
				case 1:
					index = 2;
					break;
				case 2:
					index = 3;
					break;
				case 0:
					index = 1;
					break;
			}
			Data30 = new SortedList<DateTime, double>();

			Piramida3000Entities model = VotGES.Piramida.PiramidaAccess.getModel();
			IQueryable<DATA> dataArr = from DATA d in model.DATA where
													d.PARNUMBER == 212 && d.DATA_DATE > DateStart && d.DATA_DATE < DateEnd &&
													d.OBJTYPE == 2 && d.OBJECT == 1 && d.ITEM==1
												select d;
			//adapter.FillGTPPBR(table, 1, DateStart, DateEnd);
			foreach (DATA data in dataArr) {
				DateTime date=data.DATA_DATE;

				double val=data.VALUE0.Value;
				val = val * Scale;

				Data30.Add(date, val);				
			}
			longData(DateStart, DateEnd);
		}

		protected void longData(DateTime dateStart, DateTime dateEnd) {
			DateTime date=dateStart;
			while (date <= dateEnd) {
				if (!Data30.Keys.Contains(date)) {
					if (Data30.Keys.Contains(date.AddHours(-24))){
						Data30.Add(date,Data30[date.AddHours(-24)]);
					}else{
						Data30.Add(date, 0);
					}
				}
				date = date.AddMinutes(30);
			}
		}


		protected void processData() {
			ProcessData30 = new SortedList<DateTime, double>();
			ProcessData1 = new SortedList<DateTime, double>();
			DateTime lastDate=Data30.Last().Key;
			double sum=0;
			foreach (KeyValuePair<DateTime,double> de in Data30){
				bool last=de.Key == lastDate;
				DateTime dt=de.Key.AddMinutes(-15);
				double val=de.Value;


				for (int minute=0; minute < 30; minute++) {
					DateTime dtMin=dt.AddMinutes(minute);
					if ((dtMin >= DateStart) && (dtMin <= DateEnd)) {
						sum += val;
						ProcessData1.Add(dtMin, Integrate?sum:val);
					}
				}

				dt=dt<DateStart?DateStart:dt;
				dt = last? DateEnd : dt;
				ProcessData30.Add(dt, val);
			}			

		}

		public double getCurrentVal(DateTime date) {
			KeyValuePair<DateTime,double> first= ProcessData1.First(de => de.Key > date);
			return first.Value;
		}

		public double getAvgVal(DateTime date) {
			double sum=0;
			int count=0;
			foreach (KeyValuePair <DateTime,double> de in ProcessData1) {
				if ((de.Key.Hour == date.Hour)&&(de.Key<=date)) {
					sum += de.Value;
					count++;
				}
			}
			
			return sum/count;
		}

		public double getAvg(DateTime date, int minutes) {
			DateTime ds=date.AddMinutes(minutes);
			DateTime dt=date;
			double sum=0;
			int count=0;
			foreach (KeyValuePair <DateTime,double> de in ProcessData1) {
				if ((de.Key >= ds) && (de.Key <= date) || (de.Key <= ds) && (de.Key >= date)) {
					sum += de.Value;
					count++;
				}
			}
			return sum / count;
		}

	}
}
