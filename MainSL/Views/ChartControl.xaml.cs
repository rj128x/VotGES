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
		public XAxisTypeEnum XAxesType { get; set; }
		public string XAxesForamtString { get; set; }
		public ChartControl() {
			InitializeComponent();
		}

		protected void processChartAnswer(ChartAnswer chartAnswer) {
			Answer = chartAnswer;
			ChartData data=Answer.Data;
			ChartProperties prop=Answer.Properties;

			CreateChart();
			switch (XAxesType) {
				case XAxisTypeEnum.numeric:
					CurrentChart.XAxis = new LinearAxis();
					break;
				case XAxisTypeEnum.datetime:
					CurrentChart.XAxis = new DateTimeAxis();
					break;
			}
			CurrentChart.XAxis.LabelFormatString = XAxesForamtString;
			
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
				try {
					ChartDataSerie serieData=chartAnswer.Data.Series[chartAnswer.Data.SeriesNames[serieProp.TagName]];
					VisibloxChartSerie chartSerie=new VisibloxChartSerie(this);
					chartSerie.init(serieData, serieProp);
					ChartSeries.Add(chartSerie);
				} catch {
				}
			}
			LegendGrid.ItemsSource = ChartSeries;

		}


		private void CreateChart() {

			chartPanel.Children.Remove(CurrentChart);
			CurrentChart = new Chart();
			chartPanel.Children.Add(CurrentChart);
			CurrentChart.VerticalAlignment = System.Windows.VerticalAlignment.Stretch;
			CurrentChart.HorizontalAlignment = System.Windows.HorizontalAlignment.Stretch;
			CurrentChart.LegendVisibility = System.Windows.Visibility.Collapsed;
			XAxesForamtString = Answer.Properties.XValueFormatString;
			XAxesType = Answer.Properties.XAxisType;

			chartPanel.Children.Remove(SettingsPanel);
			chartPanel.Children.Add(SettingsPanel);

			BehaviourManager manager=new BehaviourManager();
			manager.AllowMultipleEnabled = true;
			manager.IsEnabled = true;

			TrackballBehaviour track=new TrackballBehaviour();
			manager.Behaviours.Add(track);
			track.IsEnabled = true;
			track.TrackingMode = TrackingPointPattern.LineOnX;
			track.HideTrackballsOnMouseLeave = true;


			ZoomBehaviour zoom=new ZoomBehaviour();
			zoom.AnimationEnabled = false;
			zoom.ZoomMode = ZoomMode.MouseDrag;
			zoom.DisableAxisRendering = true;
			manager.Behaviours.Add(zoom);
			zoom.IsEnabled = true;

			TrackBehaviour = track;

			CurrentChart.LegendVisibility = Visibility.Collapsed;

			CurrentChart.Behaviour = manager;

		}

		private bool isMovingPanel=false;



		public void NotifyChanged(string propName) {
			if (PropertyChanged != null)
				PropertyChanged(this, new PropertyChangedEventArgs(propName));
		}

		public void Create(ChartAnswer answer) {
			processChartAnswer(answer);
		}

		public void RefreshData(ChartAnswer answer) {
			Answer = answer;
			foreach (VisibloxChartSerie serie in ChartSeries) {
				try {
					ChartDataSerie serieData=answer.Data.Series[answer.Data.SeriesNames[serie.TagName]];
					serie.refresh(serieData);
				} catch { }
			}
		}

		private void chartPanel_MouseLeftButtonUp(object sender, MouseButtonEventArgs e) {
			if (isMovingPanel) {
				isMovingPanel = false;
				borderMove.Background = new SolidColorBrush(Colors.LightGray);
			}
		}

		private void chartPanel_MouseMove(object sender, MouseEventArgs e) {
			if (isMovingPanel) {
				try {
					SettingsPanel.Margin = new Thickness(e.GetPosition(chartPanel).X +1, e.GetPosition(chartPanel).Y + 1, 0, 0);
				} catch { }
			}
		}

		private void borderMove_MouseLeftButtonUp(object sender, MouseButtonEventArgs e) {
			isMovingPanel = false;
			borderMove.Background = new SolidColorBrush(Colors.LightGray);
			LegendGrid.Visibility = LegendGrid.Visibility == System.Windows.Visibility.Collapsed ?
				System.Windows.Visibility.Visible : System.Windows.Visibility.Collapsed;
		}

		private void borderMove_MouseLeftButtonDown(object sender, MouseButtonEventArgs e) {
			isMovingPanel = true;
			borderMove.Background = new SolidColorBrush(Colors.Blue);
		}


	}
}
