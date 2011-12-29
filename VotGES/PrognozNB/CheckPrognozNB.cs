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

		public CheckPrognozNB(DateTime dateStart, int daysCount):base(dateStart,daysCount) {
			DateStart = dateStart.Date;
			DaysCount = daysCount;
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
			prognoz.IsQFakt = true;
			foreach (KeyValuePair<DateTime,double> de in QFakt) {
				prognoz.PArr.Add(de.Key, de.Value);
			}
			prognoz.calcPrognoz();
		}
	}
}
