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
	public class SettingsBase : INotifyPropertyChanged
	{
		public event PropertyChangedEventHandler PropertyChanged;
		public void NotifyChanged(string propName) {
			if (PropertyChanged != null)
				PropertyChanged(this, new PropertyChangedEventArgs(propName));
		}

		protected DateTime date;
		public DateTime Date {
			get { return date; }
			set {
				date = value;
				checkDate();
				NotifyChanged("Date");
			}
		}

		protected virtual void checkDate(){

		}
	}
	public class GlobalStatus : INotifyPropertyChanged
	{
		public event PropertyChangedEventHandler PropertyChanged;

		private InvokeOperation currentOper;

		public InvokeOperation CurrentOper {
			get { return currentOper; }
			set {
				if (currentOper != null && currentOper.CanCancel) {
					CurrentOper.Cancel();
				}
				currentOper = value; 

			}
		}

		public void NotifyChanged(string propName) {
			if (PropertyChanged != null)
				PropertyChanged(this, new PropertyChangedEventArgs(propName));
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
			}
		}

		protected bool isError;
		public bool IsError {
			get {
				return isError;
			}
			set {
				isError = value;
				NotifyChanged("IsError");
			}
		}

		protected string errorMessage;
		public string ErrorMessage {
			get {
				return errorMessage;
			}
			set {
				errorMessage = value;
				NotifyChanged("ErrorMessage");
			}
		}


		public void StartLoad(InvokeOperation oper, string message = "Загрузка") {
			IsError = false;
			CurrentOper = oper;
			Status = message;
			IsBusy = true;
		}

		public void StartProcess(string message = "Обработка ответа от сервера") {
			Status = message;
		}

		public void ChangeStatus(string message = "Загрузка") {
			Status = message;
		}

		public void StopLoad(string message = "Готово") {
			CurrentOper = null;
			Status = message;
			IsBusy = false;
		}

		public void ErrorLoad(string message = "Ошибка") {
			CurrentOper = null;
			ErrorMessage = message;
			IsError = true;
		}

		public static GlobalStatus Current {
			get {
				return Application.Current.Resources["globalStatus"] as GlobalStatus;
			}
		}
	}
}
