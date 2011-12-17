using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Animation;
using System.Windows.Shapes;
using System.Windows.Navigation;
using VotGES.Web.Models;
using MainSL.Contexts;
using System.ServiceModel.DomainServices.Client;

namespace MainSL.Views
{
	public partial class RUSAPage : Page
	{
		private RUSAData currentData;
		public RUSAData CurrentData {
			get { return currentData; }
			set { currentData = value; }
		}
		


		public RUSAPage() {
			InitializeComponent();			
		}

		

		// Выполняется, когда пользователь переходит на эту страницу.
		protected override void OnNavigatedTo(NavigationEventArgs e) {
			CurrentData=new RUSAData();
			CurrentData.GaAvail = new Dictionary<int, bool>();
			for (int ga=1; ga <= 10; ga++) {
				CurrentData.GaAvail.Add(ga, true);
			}
			CurrentData.Power = 300;
			CurrentData.Napor = 21;
			pnlData.DataContext = CurrentData;
		}

		void oper_Completed(object sender, EventArgs e) {
			InvokeOperation<int> oper= sender as InvokeOperation<int>;
			if (!oper.HasError) {
				GlobalStatus.Current.IsError = false;

				MessageBox.Show(oper.Value.ToString());
			} else {
				GlobalStatus.Current.IsError = true;
				oper.MarkErrorAsHandled();
			}
		}

		

		private void btnCalcRUSA_Click(object sender, RoutedEventArgs e) {
			InvokeOperation<RUSAData> processOper=RUSAClientContext.Current.Context.processRUSAData(CurrentData);
			processOper.Completed += new EventHandler(processOper_Completed);
		}

		void processOper_Completed(object sender, EventArgs e) {
			throw new NotImplementedException();
		}

	}
}
