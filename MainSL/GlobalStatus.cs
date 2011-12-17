using System;
using System.Net;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Documents;
using System.Windows.Ink;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Animation;
using System.Windows.Shapes;
using System.ComponentModel;

namespace MainSL
{
	public class GlobalStatus : INotifyPropertyChanged
	{
		public event PropertyChangedEventHandler PropertyChanged;

		public void NotifyChanged(string propName) {
			if (PropertyChanged != null)
				PropertyChanged(this, new PropertyChangedEventArgs(propName));
		}

		public GlobalStatus() {

		}

		public void init() {
			try {
				HomeHeader = "Список заявок";
			} catch (Exception e) {
				//Logging.Logger.logMessage(e.ToString());
			}
		}

		void Current_PropertyChanged(object sender, PropertyChangedEventArgs e) {
			if (e.PropertyName == "LastUpdate") {
			}
		}
			

		protected string status;
		public string Status {
			get {
				return status;
			}
			set {
				status = value;
				NotifyChanged("Status");
			}
		}

		protected bool isBusy;
		public bool IsBusy {
			get {
				return isBusy;
			}
			set {
				isBusy = value;
				
				NotifyChanged("IsBusy");
				CanRefresh = !IsBusy;
			}
		}

		protected bool isError;
		public bool IsError {
			get { return isError; }
			set {
				isError = value;
				NotifyChanged("IsError");
			}
		}

		private bool canRefresh;
		public bool CanRefresh {
			get { return canRefresh; }
			set {
				canRefresh = value;
				NotifyChanged("CanRefresh");
			}
		}

		private string homeHeader;
		public string HomeHeader {
			get { return homeHeader; }
			set {
				homeHeader = value;
				NotifyChanged("HomeHeader");
			}
		}

		public static GlobalStatus Current {
			get {
				return Application.Current.Resources["globalStatus"] as GlobalStatus;
			}
		}
	}
}
