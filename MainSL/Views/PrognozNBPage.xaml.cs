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
using System.ServiceModel.DomainServices.Client;

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

			public Dictionary<DateTime, double> PrevData { get; set; }
		}



		PrognozNBContext context;
		public Settings settings;
		public PrognozNBPage() {
			InitializeComponent();
			context=new PrognozNBContext();
			settings = new Settings();
			settings.CountDays = 1;
			settings.UserPBR = new PBRData(null);
			pnlSettings.DataContext = settings;
		}

		public PBREditorWindow pbrEditor;

		// Выполняется, когда пользователь переходит на эту страницу.
		protected override void OnNavigatedTo(NavigationEventArgs e) {
			rightPanel.Visibility = System.Windows.Visibility.Collapsed;
			loadPrognoz(false);
		}

		protected override void OnNavigatedFrom(NavigationEventArgs e) {
			GlobalStatus.Current.StopLoad();
		}

		protected void createPBREditor() {
			pbrEditor = new PBREditorWindow(settings.UserPBR);
			pbrEditor.DataContext = settings.UserPBR;
			pbrEditor.Closed += new EventHandler(pbrEditor_Closed);
		}

		void pbrEditor_Closed(object sender, EventArgs e) {
			if (pbrEditor.DialogResult.Value) {
				loadPrognoz(true);
			} else {
				settings.UserPBR = new PBRData(settings.PrevData);
				createPBREditor();
			}
		}

		protected void loadPrognoz(bool useUserPBR) {
			settings.UserPBR.convertToHalfHoursPBR();
			InvokeOperation currentOper=context.getPrognoz(settings.CountDays, useUserPBR ? settings.UserPBR.Data : null, oper => {
				if (oper.IsCanceled) {
					return;
				}
				try {
					GlobalStatus.Current.StartProcess();
					ChartAnswer answer=oper.Value.Chart;
					chartControl.Create(answer);

					ChartDataSerie serie=answer.Data.Series[answer.Data.SeriesNames["PBR"]];
					Dictionary<DateTime,double> data=new Dictionary<DateTime, double>();
					foreach (ChartDataPoint point in serie.Points) {
						data.Add(point.XVal, point.YVal);
					}
					settings.UserPBR = new PBRData(data);
					createPBREditor();
					rightPanel.Visibility = System.Windows.Visibility.Visible;

					pnlAnswer.DataContext = oper.Value;
				} catch (Exception ex) {
					Logging.Logger.info(ex.ToString());
					GlobalStatus.Current.ErrorLoad("Ошибка");
				} finally {
					GlobalStatus.Current.StopLoad();
				}

			}, null);
			GlobalStatus.Current.StartLoad(currentOper);
		}


		private void btnGetPrognoz_Click(object sender, RoutedEventArgs e) {
			settings.PrevData = new Dictionary<DateTime, double>();
			foreach (KeyValuePair<DateTime,double> de in settings.UserPBR.Data) {
				settings.PrevData.Add(de.Key, de.Value);
			}
			pbrEditor.Show();
		}

		private void btnClearPBR_Click(object sender, RoutedEventArgs e) {
			loadPrognoz(false);
		}

	}
}
