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

namespace MainSL.Views
{
	public partial class RezhimSKReportPage : Page
	{
		public SettingsBase settings;
		ReportBaseDomainContext context;
		public RezhimSKReportPage() {
			InitializeComponent();
			settings = new SettingsBase();
			settings.Date = DateTime.Now.Date.AddDays(-1);
			pnlSettings.DataContext = settings;
			context = new ReportBaseDomainContext();
		}

		// Выполняется, когда пользователь переходит на эту страницу.
		protected override void OnNavigatedTo(NavigationEventArgs e) {

		}

		protected override void OnNavigatedFrom(NavigationEventArgs e) {
			GlobalStatus.Current.StopLoad();
		}
		
		private void btnGetReport_Click(object sender, RoutedEventArgs e) {
			InvokeOperation currentOper=context.GetRezhimSKReport(settings.Date,
				oper => {
					if (oper.IsCanceled) {
						return;
					}
					GlobalStatus.Current.StartProcess();
					try {
						reportControl.chartControl.Create(oper.Value.Chart);
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
