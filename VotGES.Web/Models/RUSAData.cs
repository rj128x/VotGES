using System;
using System.Collections.Generic;
using System.Linq;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ServiceModel.DomainServices.Server;

namespace VotGES.Web.Models
{
	public class GAParams
	{
		int gaNumber;
		public int GaNumber {
			get { return gaNumber; }
			set { gaNumber = value; }
		}

		bool avail;
		public bool Avail {
			get { return avail; }
			set { avail = value; }
		}

		public GAParams() {
		}

		public GAParams(int ga, bool avail) {
			this.gaNumber = ga;
			this.avail = avail;
		}
	}

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

		private Dictionary<int,double> sostav;
		public Dictionary<int, double> Sostav {
			get { return sostav; }
			set { sostav = value; }
		}
	}

	public class RUSAData : INotifyPropertyChanged
	{
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

		private List<GAParams> gaAvail;
		public List<GAParams> GaAvail {
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
			gaAvail = new List<GAParams>();
			for (int ga=1; ga <= 10; ga++) {
				gaAvail.Add(new GAParams(ga,true));
			}
			power = 300;
			napor = 21;
		}

		public List<int> getAvailGenerators() {
			List<int> res=new List<int>();
			foreach (GAParams avail in gaAvail) {
				if (avail.Avail) {
					res.Add(avail.GaNumber);
				}
			}
			return res;
		}

		private List<RUSAResult> eqResult;
		public List<RUSAResult> EqResult {
			get { return eqResult; }
			set { eqResult = value; }
		}

		private List<RUSAResult> diffResult;
		public List<RUSAResult> DiffResult {
			get { return diffResult; }
			set { diffResult = value; }
		}

		
		private List<RUSAResult> result;
		public List<RUSAResult> Result {
			get { return result; }
			set { result = value; }
		}
		
	}
}