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
using VotGES.Web.Services;
using System.ServiceModel.DomainServices.Client;
using VotGES.Chart;
using VotGES.PBR;

namespace MainSL.Views
{
	public partial class GraphVyrabPage : Page
	{
		public GraphVyrabAnswer CurrentAnswer { get; set; }
		public GraphVyrabDomainContext context;

		public GraphVyrabPage() {
			InitializeComponent();
			CurrentAnswer = new GraphVyrabAnswer();
			context = new GraphVyrabDomainContext();
			pnlSettings.DataContext = CurrentAnswer;
		}

		// Выполняется, когда пользователь переходит на эту страницу.
		protected override void OnNavigatedTo(NavigationEventArgs e) {
			refresh();
		}

		protected override void OnNavigatedFrom(NavigationEventArgs e) {
			GlobalStatus.Current.StopLoad();
		}

		private void btnRefresh_Click(object sender, RoutedEventArgs e) {
			refresh();
		}

		private void refresh() {
			if (GlobalStatus.Current.IsBusy)
				return;
			InvokeOperation currentOper=context.getGraphVyrad(
				oper => {
					if (oper.IsCanceled) {
						return;
					}
					GlobalStatus.Current.StartProcess();
					try {
						ChartAnswer answer=oper.Value.Chart;
						txtActualDate.Text = oper.Value.ActualDate.ToString("HH:mm");
						pnlSettings.DataContext = oper.Value;
						chartControl.Create(answer);
					} catch (Exception ex) {
						Logging.Logger.info(ex.ToString());
						MessageBox.Show("Ошибка при обработке ответа от сервера");
					} finally {
						GlobalStatus.Current.StopLoad();
					}
				}, null);
			GlobalStatus.Current.StartLoad(currentOper);
		}

	}
}
