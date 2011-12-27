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
using System.ComponentModel;

namespace MainSL.Views
{

	public partial class PrognozNBPage : Page
	{
		public class Settings : INotifyPropertyChanged
		{
			public event PropertyChangedEventHandler PropertyChanged;

			public void NotifyChanged(string propName) {
				if (PropertyChanged != null)
					PropertyChanged(this, new PropertyChangedEventArgs(propName));
			}
			
			private int countDays;
			public int CountDays {
				get { return countDays; }
				set { 
					countDays = value;
					countDays = countDays < 1 ? 1 : countDays;
					countDays = countDays > 10 ? 10 : countDays;
					NotifyChanged("CountDays");
				}
			}

			private PBRData userPBR;
			public PBRData UserPBR {
				get { return userPBR; }
				set { 
					userPBR = value;
					NotifyChanged("UserPBR");
				}
			}
		}



		ChartContext chartContext;
		public Settings settings;
		public PrognozNBPage() {
			InitializeComponent();
			chartContext = new ChartContext();
			settings = new Settings();
			settings.CountDays = 1;
			settings.UserPBR = new PBRData(null);
			pnlSettings.DataContext = settings;
		}

		public PBREditorWindow pbrEditor;

		// Выполняется, когда пользователь переходит на эту страницу.
		protected override void OnNavigatedTo(NavigationEventArgs e) {
			btnGenPBR.Visibility = System.Windows.Visibility.Collapsed;
			btnClearPBR.Visibility = System.Windows.Visibility.Collapsed;

		}


		protected void loadPrognoz(bool useUserPBR) {
			settings.UserPBR.convertToHalfHoursPBR();
			chartContext.getPrognoz(settings.CountDays, useUserPBR?settings.UserPBR.Data:null, oper => {
				try {
					ChartAnswer answer=oper.Value;
					chartControl.Create(answer);

					foreach (ChartDataSerie serie in answer.Data.Series) {
						if (serie.Name == "PBR") {
							Dictionary<DateTime,double> data=new Dictionary<DateTime, double>();
							foreach (ChartDataPoint point in serie.Points) {
								data.Add(point.XVal, point.YVal);
							}
							settings.UserPBR = new PBRData(data);
							pbrEditor = new PBREditorWindow(settings.UserPBR);
							pbrEditor.DataContext = settings.UserPBR;
							btnGenPBR.Visibility = System.Windows.Visibility.Visible;
							btnClearPBR.Visibility = System.Windows.Visibility.Visible;
						}
					}
				} catch (Exception ex) {
					Logging.Logger.info(ex.ToString());
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

		private void btnGetPrognoz_Click(object sender, RoutedEventArgs e) {
			loadPrognoz(true);
		}

		private void btnClearPBR_Click(object sender, RoutedEventArgs e) {
			loadPrognoz(false);
		}

	}
}
