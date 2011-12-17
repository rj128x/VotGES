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
using VotGES.Web.Models;
using VotGES.Web.Services;
using System.ServiceModel.DomainServices.Client;
using MainSL.Logging;

namespace MainSL.Contexts
{
	public class RUSAClientContext : INotifyPropertyChanged
	{
		public event PropertyChangedEventHandler PropertyChanged;

		public void NotifyChanged(string propName) {
			if (PropertyChanged != null)
				PropertyChanged(this, new PropertyChangedEventArgs(propName));
		}


		private static RUSAClientContext current;
		public static RUSAClientContext Current {
			get { return current; }
		}

		private RUSAContext context;
		public RUSAContext Context {
			get { return context; }
		}

		static RUSAClientContext() {
			current = new RUSAClientContext();			
		}

		private RUSAClientContext() {
			context = new RUSAContext();			
		}

		public void SubmitChangesCallbackError() {
			context.SubmitChanges(submit, null);
		}

		protected void submit(SubmitOperation oper) {
			if (oper.HasError) {
				GlobalStatus.Current.Status = "Ошибка при выполнении операции на сервере: " + oper.Error.Message;
				MessageBox.Show(oper.Error.Message, "Ошибка при выполнении операции на сервере", MessageBoxButton.OK);
				Logger.info(oper.Error.ToString());
				oper.MarkErrorAsHandled();
			} else {
				GlobalStatus.Current.Status = "Готово";
			}
		}

	}
}
