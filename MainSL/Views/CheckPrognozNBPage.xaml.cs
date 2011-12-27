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
using VotGES.Chart;

namespace MainSL.Views
{
	public partial class CheckPrognozNBPage : Page
	{
		ChartContext chartContext;
		public CheckPrognozNBPage() {
			InitializeComponent();
			chartContext = new ChartContext();
			txtCountDays.Text = "1";
			clndDate.SelectedDate = DateTime.Now.Date.AddDays(-1);
		}

		// Выполняется, когда пользователь переходит на эту страницу.
		protected override void OnNavigatedTo(NavigationEventArgs e) {
		}

		private void btnGetPrognoz_Click(object sender, RoutedEventArgs e) {
			DateTime date=clndDate.SelectedDate.Value;
			int CountDays=1;
			try{
				CountDays=Int32.Parse(txtCountDays.Text);
			}catch{
				CountDays = 1;
			}
			CountDays = CountDays > 10 ? 10 : CountDays;
			CountDays = CountDays < 1 ? 1 : CountDays;
			txtCountDays.Text = CountDays.ToString();

			chartContext.checkPrognozNB(date, CountDays, oper => {
				try {
					ChartAnswer answer=oper.Value;
					clndDate.SelectedDate = answer.Data.Series[0].Points[0].XVal;
					chartControl.Create(answer);
				} catch (Exception) {
					Logging.Logger.info(e.ToString());
					MessageBox.Show("Ошибка при обработке ответа от сервера");
				}
				GlobalStatus.Current.IsWaiting = false;
			}, null);
			GlobalStatus.Current.IsWaiting = true;
		}

	}
}
