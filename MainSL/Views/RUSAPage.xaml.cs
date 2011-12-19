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
using System.ServiceModel.DomainServices.Client;
using VotGES.Web.Services;

namespace MainSL.Views
{
	public partial class RUSAPage : Page
	{
		private RUSAData currentData;
		public RUSAData CurrentData {
			get { return currentData; }
			set { 
				currentData = value;
				pnlData.DataContext = CurrentData;
			}
		}

		RUSADomainContext context;

		public RUSAPage() {
			InitializeComponent();
			context = new RUSADomainContext();
			GlobalStatus.Current.addContext(context);
		}

		

		// Выполняется, когда пользователь переходит на эту страницу.
		protected override void OnNavigatedTo(NavigationEventArgs e) {
			CurrentData=new RUSAData();
			CurrentData.GaAvail = new List<GAParams>();
			for (int ga=1; ga <= 10; ga++) {
				GAParams p=new GAParams();
				p.GaNumber = ga;
				p.Avail = true;
				CurrentData.GaAvail.Add(p);
			}
			CurrentData.Power = 300;
			CurrentData.Napor = 21;
			
		}
				

		private void btnCalcRUSA_Click(object sender, RoutedEventArgs e) {
			context.processRUSAData(CurrentData, oper => {
				GlobalStatus.Current.IsWaiting = false;
				CurrentData = oper.Value;
			},null);
			GlobalStatus.Current.IsWaiting = true;
			
		}

		void processOper_Completed(object sender, EventArgs e) {

		}

	}
}
