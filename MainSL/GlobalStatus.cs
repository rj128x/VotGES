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
using System.ServiceModel.DomainServices.Client;
using System.Collections.Generic;

namespace MainSL
{
	public class GlobalStatus : INotifyPropertyChanged
	{
		public event PropertyChangedEventHandler PropertyChanged;
		protected List<DomainContext> contexts;

		public void NotifyChanged(string propName) {
			if (PropertyChanged != null)
				PropertyChanged(this, new PropertyChangedEventArgs(propName));
		}

		public void addContext(DomainContext context) {
			context.PropertyChanged += new PropertyChangedEventHandler(context_PropertyChanged);
			contexts.Add(context);
			IsBusy = false;
		}

		void context_PropertyChanged(object sender, PropertyChangedEventArgs e) {
			checkBusy();
		}

		protected void checkBusy() {
			bool busy=false;
			foreach (DomainContext context in contexts) {
				busy = busy || context.IsLoading || context.IsSubmitting;
			}
			IsBusy = busy || IsWaiting;
		}

		public GlobalStatus() {
			contexts = new List<DomainContext>();
			LastUpdate = DateTime.Now;	
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
				Status = IsBusy ? "Загрузка" : "Готово";
				NotifyChanged("IsBusy");
				CanRefresh = !IsBusy;
			}
		}

		private bool isWaiting;
		public bool IsWaiting {
			get { return isWaiting; }
			set { 
				isWaiting = value;
				checkBusy();
				NotifyChanged("IsWaiting");
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

		private bool needRefresh;
		public bool NeedRefresh {
			get { return needRefresh; }
			set {
				needRefresh = value;
				NotifyChanged("NeedRefresh");
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

		private DateTime lastUpdate;
		public DateTime LastUpdate {
			get {
				return lastUpdate;
			}
			protected set {
				lastUpdate = value;
				NotifyChanged("LastUpdate");
				NeedRefresh = false;
			}
		}

		public static GlobalStatus Current {
			get {
				return Application.Current.Resources["globalStatus"] as GlobalStatus;
			}
		}
	}
}
