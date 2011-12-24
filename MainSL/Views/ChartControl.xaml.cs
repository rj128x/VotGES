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
using Visiblox.Charts.Primitives;
using Visiblox.Charts;
using System.ComponentModel;
using VotGES.Chart;

namespace MainSL.Views
{
	public partial class ChartControl : UserControl
	{
		public event PropertyChangedEventHandler PropertyChanged;
		public ChartAnswer Answer { get; set; }
		public List<VisibloxChartSerie> ChartSeries;
		public Dictionary<int,LinearAxis> Axes;
		public TrackballBehaviour TrackBehaviour { get; protected set; }

		public ChartControl() {
			InitializeComponent();
		}

		protected void processChartAnswer(ChartAnswer chartAnswer) {
			Answer = chartAnswer;
			ChartData data=Answer.Data;
			ChartProperties prop=Answer.Properties;

			CreateChart();
			LinearAxis hiddenYAxis=new LinearAxis();
			hiddenYAxis.Visibility = Visibility.Collapsed;
			CurrentChart.AdditionalSecondaryYAxes.Add(hiddenYAxis);
			Axes = new Dictionary<int, LinearAxis>();
			foreach (ChartAxisProperties ax in chartAnswer.Properties.Axes) {
				LinearAxis axis=new LinearAxis();
				axis.AutoScaleToVisibleData = true;
				axis.LabelFormatString = "### ### ##0.##";
				axis.ShowGridlines = ax.Index == 0;
				if (ax.Interval != 0) {
					axis.MajorTickInterval = ax.Interval;
				}
				if (!ax.Auto) {
					axis.Range = new DoubleRange(ax.Min, ax.Max);
				}
				if (ax.Index > 1) {
					CurrentChart.AdditionalSecondaryYAxes.Add(axis);
				}
				Axes.Add(ax.Index, axis);
			}
			CurrentChart.YAxis = Axes[0];
			CurrentChart.SecondaryYAxis = Axes[1];

			ChartSeries = new List<VisibloxChartSerie>();
			foreach (ChartSerieProperties serieProp in prop.Series) {
				foreach (ChartDataSerie serieData in data.Series) {
					if (serieData.Name == serieProp.TagName) {
						VisibloxChartSerie chartSerie=new VisibloxChartSerie(this);
						chartSerie.init(serieData, serieProp);
						ChartSeries.Add(chartSerie);
					}
				}
			}
			LegendGrid.ItemsSource = ChartSeries;

		}


		private void CreateChart() {
			//CurrentChart = new Chart();

			BehaviourManager manager=new BehaviourManager();
			manager.AllowMultipleEnabled = true;
			manager.IsEnabled = true;

			TrackballBehaviour track=new TrackballBehaviour();
			manager.Behaviours.Add(track);
			track.IsEnabled = true;
			track.TrackingMode = TrackingPointPattern.LineOnX;

			ZoomBehaviour zoom=new ZoomBehaviour();
			manager.Behaviours.Add(zoom);
			zoom.IsEnabled = true;

			TrackBehaviour = track;

			CurrentChart.LegendVisibility = Visibility.Collapsed;

			CurrentChart.Behaviour = manager;			
		}

		public void NotifyChanged(string propName) {
			if (PropertyChanged != null)
				PropertyChanged(this, new PropertyChangedEventArgs(propName));
		}

		public void Create(ChartAnswer answer) {
			processChartAnswer(answer);
		}

		public void RefreshData(ChartAnswer answer) {
			Answer = answer;
			foreach (ChartDataSerie serieData in answer.Data.Series) {
				foreach (VisibloxChartSerie serie in ChartSeries) {
					if (serie.TagName == serieData.Name) {
						serie.refresh(serieData);
					}
				}
			}
		}


		private void ToggleSettingsBtn_Click(object sender, RoutedEventArgs e) {
			LegendGrid.Visibility = LegendGrid.Visibility == System.Windows.Visibility.Collapsed ? 
				System.Windows.Visibility.Visible : System.Windows.Visibility.Collapsed;

		}
	}
}
