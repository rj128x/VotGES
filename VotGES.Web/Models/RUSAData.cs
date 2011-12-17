using System;
using System.Collections.Generic;
using System.Linq;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace VotGES.Web.Models
{
	public class RUSAData : INotifyPropertyChanged
	{
		public class RUSAResult
		{
			private double rashod;
			public double Rashod {
				get { return rashod; }
				set { rashod = value; }
			}

			private double kpd;
			public double KPD {
				get { return kpd; }
				set { kpd = value; }
			}

			public Dictionary<int,double> sostav;
		}


		public event PropertyChangedEventHandler PropertyChanged;
		public void NotifyChanged(string propName) {
			if (PropertyChanged != null)
				PropertyChanged(this, new PropertyChangedEventArgs(propName));
		}


		private Guid id;
		public Guid Id {
			get { return id; }
			set { id = value; }
		}

		private Dictionary<int,bool> gaAvail;
		public Dictionary<int, bool> GaAvail {
			get { return gaAvail; }
			set { gaAvail = value; }
		}

		private double napor;
		public double Napor {
			get { return napor; }
			set { napor = value; }
		}

		private double power;
		public double Power {
			get { return power; }
			set { power = value; }
		}
								
		public RUSAData() {
			gaAvail = new Dictionary<int, bool>();
			for (int ga=1; ga <= 10; ga++) {
				gaAvail.Add(ga, false);
			}
			power = 300;
			napor = 21;
		}

		public List<int> getAvailGenerators() {
			List<int> avail=new List<int>();
			for (int ga=1; ga <= 10; ga++) {
				if (gaAvail[ga])
					avail.Add(ga);
			}
			return avail;
		}

		public List<RUSAResult> eqResult;
		public List<RUSAResult> diffResult;

		
	}
}