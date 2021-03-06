﻿using System;
using System.Net;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Documents;
using System.Windows.Ink;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Animation;
using System.Windows.Shapes;
using System.ComponentModel;
using System.Collections.Generic;
using Visiblox.Charts;
using VotGES.Chart;
using MainSL.Views;
using System.Windows.Markup;

namespace MainSL
{
	public class VisibloxChartSerie : INotifyPropertyChanged
	{
		protected static string TemplateStr=@"<ControlTemplate  
				xmlns=""http://schemas.microsoft.com/client/2007"">         
				<Border Style=""{StaticResource borderGray}"" Background=""LightGray"" Opacity=""0.7"">
					<StackPanel Orientation=""Horizontal"">
						<TextBlock Text=""~serieName~: ["" FontWeight=""Bold"" /> 
						<TextBlock Text=""{Binding X, StringFormat='~xFormat~'}"" FontWeight=""Bold"" /> 
						<TextBlock Text=""]-["" FontWeight=""Bold"" /> 
						<TextBlock Text=""{Binding Y, StringFormat='~yFormat~'}"" FontWeight=""Bold"" />
						<TextBlock Text=""]"" FontWeight=""Bold"" /> 
					</StackPanel>
				</Border>
            </ControlTemplate>";
		public ChartControl silverChartControl { get; protected set; }
		public event PropertyChangedEventHandler PropertyChanged;
		public string TagName { get; protected set; }
		public ChartSeriesBase Serie { get; protected set; }
		public object SeriesData;
		

		public int SerieIndex { get; protected set; }

		protected int yAxisIndex;
		public int YAxisIndex {
			get { return yAxisIndex; }
			set {
				if (yAxisIndex != value) {
					try {
						silverChartControl.AxesVisible[yAxisIndex].Remove(Name);
					} catch { }
					yAxisIndex = value;
					try {
						silverChartControl.AxesVisible[yAxisIndex].Add(Name, Enabled);
					} catch { }
					Serie.YAxis = silverChartControl.Axes[value];
					silverChartControl.checkVisibleAxes(YAxisIndex);
				}
				NotifyChanged("YAxisIndex");

			}
		}

		private string currentPointX;
		public string CurrentPointX {
			get { return currentPointX; }
			set { 
				currentPointX = value;
				NotifyChanged("CurrentPointX"); 
			}
		}

		private string currentPointY;
		public string CurrentPointY {
			get { return currentPointY; }
			set {
				currentPointY = value;
				NotifyChanged("CurrentPointY");
			}
		}

		protected IDataPoint currentPoint;
		public IDataPoint CurrentPoint {
			get {
				return currentPoint;
			}
			set {
				currentPoint = value;
				CurrentPointX = String.Format("{0:" + silverChartControl.XAxesForamtString + "}", currentPoint.X);
				CurrentPointY = String.Format("{0:#,0.##}", currentPoint.Y);
				NotifyChanged("CurrentPoint"); 
			}
		}

		protected bool enabled;
		public bool Enabled {
			get { return enabled; }
			set {
				if (!silverChartControl.AxesVisible[YAxisIndex].ContainsKey(TagName)) {
					silverChartControl.AxesVisible[YAxisIndex].Add(TagName, false);
				}
				enabled = value;
				silverChartControl.AxesVisible[YAxisIndex][TagName] = enabled;
				if (enabled) {
					switch (silverChartControl.XAxesType) {
						case XAxisTypeEnum.datetime:
							Serie.DataSeries = SeriesData as DataSeries<DateTime, double>;
							break;
						case XAxisTypeEnum.numeric:
							Serie.DataSeries = SeriesData as DataSeries<double, double>;
							break;
					}
					
				} else {
					Serie.DataSeries = null;
					CurrentPointX = "-";
					CurrentPointY = "-";
				}
				silverChartControl.checkVisibleAxes(YAxisIndex);
				NotifyChanged("Enabled");  
			}
		}

		protected string name;
		public string Name {
			get { return name; }
			set { 
				name = value;
				NotifyChanged("Name"); 
			}
		}		
		
		protected ChartSerieType serieType;
		public ChartSerieType SerieType {
			get { return serieType; }
			set {			
				serieType=value;
				NotifyChanged("SerieType");				
			}
		}

		protected Brush lineStroke;
		public Brush LineStroke {
			get { return lineStroke; }
			set {
				lineStroke = value;
				NotifyChanged("LineStroke");
			}
		}

		protected bool selected;
		public bool Selected {
			get { return selected; }
			set {
					selected = value;
					NotifyChanged("Selected");
			}
		}

		protected bool showToolTip;
		public bool ShowToolTip {
			get { return showToolTip; }
			set {
				showToolTip = value;
				switch (SerieType) {
					case ChartSerieType.line:
						LineSeries line=Serie as LineSeries;
						line.ShowPoints = value;
						line.ToolTipEnabled = value;
						break;
					case ChartSerieType.stepLine:
					   StaircaseSeries stair=Serie as StaircaseSeries;
						stair.ShowPoints = value;
						stair.ToolTipEnabled = value;
						break;
				}
			}
		}

		public VisibloxChartSerie(ChartControl silverChartControl) {
			this.silverChartControl = silverChartControl;
		}

		public void NotifyChanged(string propName) {
			if (PropertyChanged != null)
				PropertyChanged(this, new PropertyChangedEventArgs(propName));
		}

		
		public void init(ChartDataSerie serieData, ChartSerieProperties serieProp) {
			SerieIndex = silverChartControl.ChartSeries.Count;
			TagName = serieData.Name;
			Name = serieProp.Title;
			SerieType = serieProp.SerieType;
			Brush tr=new SolidColorBrush(Colors.Transparent);
	
			Serie=null;
			Brush br=new SolidColorBrush(Color.FromArgb(255,0,0,0));
			if (serieProp.Color != null) {
				string[] colors=serieProp.Color.Split('-');
				byte r=Byte.Parse(colors[0]);
				byte g=Byte.Parse(colors[1]);
				byte b=Byte.Parse(colors[2]);
				br = new SolidColorBrush(Color.FromArgb(255, r, g, b));

			} else {
				//br = ChartActions.Actions().getNextColor();
			}
			switch (serieProp.SerieType){
				case ChartSerieType.line:
					LineSeries lineSerie=new LineSeries();
					lineSerie.LineStrokeThickness=serieProp.LineWidth+1;
					lineSerie.LineStroke = br;
					LineStroke = lineSerie.LineStroke;
					lineSerie.HighlightingEnabled=true;
					lineSerie.PointSize = 10;
					lineSerie.PointFill = tr;
					lineSerie.PointStroke = tr;					
					Serie = lineSerie;
					break;
				case ChartSerieType.stepLine:
					StaircaseSeries stairSerie=new StaircaseSeries();
					stairSerie.LineStrokeThickness=serieProp.LineWidth+1;
					stairSerie.LineStroke = br;
					stairSerie.HighlightingEnabled = true;					
					LineStroke = stairSerie.LineStroke;
					Serie = stairSerie;
					stairSerie.PointSize = 10;
					stairSerie.PointFill = tr;
					stairSerie.PointStroke = tr;					
					break;		
				case ChartSerieType.column:
					ColumnSeries columnSerie=new ColumnSeries();
					columnSerie.HighlightingEnabled = true;
					columnSerie.PointFill = br;
					columnSerie.PointStroke = br;
					LineStroke = br;
					Serie = columnSerie;
					columnSerie.ToolTipEnabled = true;
					break;
			}
			Serie.ToolTipTemplate = XamlReader.Load
				(TemplateStr.Replace("~serieName~", this.Name).
					Replace("~xFormat~",silverChartControl.XAxesForamtString).
					Replace("~yFormat~", "#,0.##")) as ControlTemplate;

			Serie.PropertyChanged += new PropertyChangedEventHandler(Serie_PropertyChanged);
			silverChartControl.CurrentChart.Series.Add(Serie);
			
			YAxisIndex = serieProp.YAxisIndex;			
			
			silverChartControl.TrackBehaviour.PropertyChanged += new PropertyChangedEventHandler(TrackBehaviour_PropertyChanged);
			refresh(serieData);
			Enabled = serieProp.Enabled;
		}

		void Serie_PropertyChanged(object sender, PropertyChangedEventArgs e) {
			if (e.PropertyName == "IsHighlighted") {
				try {
					Selected = (Serie as ISelectableChartSeries).IsHighlighted;
				}catch{}
			}
		}
		

		void TrackBehaviour_PropertyChanged(object sender, PropertyChangedEventArgs e) {
			try {
				if (Enabled) {
					CurrentPoint = silverChartControl.TrackBehaviour.CurrentPoints[SerieIndex];
				} else {
					CurrentPointX = "-";
					CurrentPointY = "-";
				}
			} catch {
				CurrentPointX = "-";
				CurrentPointY = "-";
			}
		}

		public void refresh(ChartDataSerie serieData) {
			int count=0;
			switch (silverChartControl.XAxesType) {
				case XAxisTypeEnum.numeric:
					DataSeries<double,double> dataSeries=new DataSeries<double, double> { Title = Name };
					foreach (ChartDataPoint point in serieData.Points) {				
						dataSeries.Add(new DataPoint<double, double>(point.XValDouble, point.YVal));
						count++;
					}
					if (Enabled) {
						Serie.DataSeries = dataSeries;
					}
					SeriesData = dataSeries;
					break;
				case XAxisTypeEnum.datetime:
					DataSeries<DateTime,double> dataSeriesDate=new DataSeries<DateTime, double> { Title = Name };
					foreach (ChartDataPoint point in serieData.Points) {
						dataSeriesDate.Add(new DataPoint<DateTime, double>(point.XVal, point.YVal));
						count++;
					}
					if (Enabled) {
						Serie.DataSeries = dataSeriesDate;
					}
					SeriesData = dataSeriesDate;
					break;
			}
			ShowToolTip=count<100;
			
		}
		
	}
}
