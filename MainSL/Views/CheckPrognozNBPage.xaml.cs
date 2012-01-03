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
using System.ComponentModel;
using System.ServiceModel.DomainServices.Client;

namespace MainSL.Views
{
	public partial class CheckPrognozNBPage : Page
	{
		public class Settings : SettingsBase
		{		

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

			protected override void checkDate() {
				if (date.Date >= DateTime.Now.AddHours(-2).Date)
					date = DateTime.Now.AddHours(-2).Date.AddDays(-1);
			}

			private bool isQFakt;
			public bool IsQFakt {
				get { return isQFakt; }
				set {
					isQFakt = value;
					NotifyChanged("IsQFakt");
				}
			}

			private int hourStart;
			public int HourStart {
				get { return hourStart; }
				set {
					hourStart = value;
					hourStart = hourStart < 0 ? 0 : hourStart;
					hourStart = hourStart > 23 ? 23 : hourStart;
					NotifyChanged("HourStart");
				}
			}

			private int minStart;
			public int MinStart {
				get { return minStart; }
				set {
					minStart = value;
					minStart = minStart < 30 ? 0 : 30;
					NotifyChanged("MinStart");
				}
			}
		}

		public Settings settings;
		PrognozNBContext context;
		public CheckPrognozNBPage() {
			InitializeComponent();
			context = new PrognozNBContext();
			settings = new Settings();
			settings.CountDays = 1;
			settings.Date = DateTime.Now.Date.AddDays(-1);
			settings.HourStart = 0;
			settings.MinStart = 0;
			pnlSettings.DataContext = settings;
		}

		// Выполняется, когда пользователь переходит на эту страницу.
		protected override void OnNavigatedTo(NavigationEventArgs e) {
		}

		protected override void OnNavigatedFrom(NavigationEventArgs e) {
			GlobalStatus.Current.StopLoad();
		}

		private void btnGetPrognoz_Click(object sender, RoutedEventArgs e) {
			InvokeOperation currentOper=context.checkPrognozNB(settings.Date, settings.CountDays, settings.IsQFakt, settings.HourStart, settings.MinStart, 
				oper => {
					if (oper.IsCanceled) {
						return;
					}
					GlobalStatus.Current.StartProcess();
					try {
						ChartAnswer answer=oper.Value;
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
