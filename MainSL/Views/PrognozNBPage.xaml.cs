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
using MainSL.PBR;

namespace MainSL.Views
{
	public partial class PrognozNBPage : Page
	{
		ChartContext chartContext;
		public PrognozNBPage() {
			InitializeComponent();
			chartContext = new ChartContext();
			txtCountDays.Text = "1";
		}

		public PBRData CurrentPBRData;
		public PBREditorWindow pbrEditor;

		// Выполняется, когда пользователь переходит на эту страницу.
		protected override void OnNavigatedTo(NavigationEventArgs e) {
			CurrentPBRData = new PBRData();
			btnGenPBR.Visibility = System.Windows.Visibility.Collapsed;
			
		}

		private void btnGetPrognoz_Click(object sender, RoutedEventArgs e) {
			int CountDays=1;
			try{
				CountDays=Int32.Parse(txtCountDays.Text);
			}catch{
				CountDays = 1;
			}
			CountDays = CountDays > 10 ? 10 : CountDays;
			CountDays = CountDays < 1 ? 1 : CountDays;
			txtCountDays.Text = CountDays.ToString();

			chartContext.getPrognoz(CountDays, CurrentPBRData.Data, oper => {
				try {
					ChartAnswer answer=oper.Value;
					chartControl.Create(answer);

					foreach (ChartDataSerie serie in answer.Data.Series) {
						if (serie.Name == "PBR") {
							Dictionary<DateTime,double> data=new Dictionary<DateTime, double>();
							foreach (ChartDataPoint point in serie.Points) {
								data.Add(point.XVal, point.YVal);
							}
							CurrentPBRData = new PBRData();
							CurrentPBRData.Data = data;
							pbrEditor = new PBREditorWindow(CurrentPBRData);
							pbrEditor.DataContext = CurrentPBRData;
							btnGenPBR.Visibility = System.Windows.Visibility.Visible;
						}
					}
				}catch{
					MessageBox.Show("Ошибка при обработке ответа от сервера");
				}
				GlobalStatus.Current.IsWaiting = false;

			}, null);
			GlobalStatus.Current.IsWaiting = true;
		}

		private void btnGenPBR_Click(object sender, RoutedEventArgs e) {
			if (pbrEditor != null) {
				pbrEditor.Show();
			}
		}

	}
}
