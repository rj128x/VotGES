using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace VotGES.PBR
{
	class PBR
	{
		private List<PBRCondition> conditions;
		public List<PBRCondition> Conditions {
			get { return conditions; }
			set { conditions = value; }
		}

		protected DateTime date;
		public DateTime Date {
			get { return date; }
			set { date = value; }
		}

		protected SortedList<DateTime,double> data;
		public SortedList<DateTime, double> Data {
			get { return data; }
			set { data = value; }
		}

		protected double vyrab;
		public double Vyrab {
			get { return vyrab; }
			set { vyrab = value; }
		}


		public PBR(DateTime date) {
			Date = date.Date;
			Data=new SortedList<DateTime,double>();
			conditions = new List<PBRCondition>();
			for (int hour=0; hour <= 24; hour++) {
				Data.Add(Date.AddHours(hour), 0);
			}
			Conditions = new List<PBRCondition>();
		}

		public bool checkCondition(PBRCondition newCondition) {
			return false;

		}

		




	}
}
