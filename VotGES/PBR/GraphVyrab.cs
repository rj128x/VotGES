using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using VotGES.Chart;

namespace VotGES.PBR
{
	public class GraphVyrabAnswer
	{
		public ChartAnswer Chart { get; set; }
		public DateTime ActualDate { get; set; }		
	}

	public class GraphVyrab
	{
		public static GraphVyrabAnswer getAnswer(DateTime date) {
			DateTime dateStart=date.Date;
			DateTime dateEnd=date.Date.AddHours(24);

			GraphVyrabAnswer answer=new GraphVyrabAnswer();
			
			PBRData ges=new PBRData(dateStart, dateEnd, date, 0);
			PBRData gtp1=new PBRData(dateStart, dateEnd, date, 1);
			PBRData gtp2=new PBRData(dateStart, dateEnd, date, 2);

			answer.ActualDate = ges.Date;
			answer.Chart = new ChartAnswer();
			answer.Chart.Properties = getChartProperties();
			answer.Chart.Data = new ChartData();

			gtp1.InitData();
			gtp2.InitData();
			ges.InitData();

			answer.Chart.Data.addSerie(getDataSerie("gtp1Fakt",gtp1.RealP,-1));
			answer.Chart.Data.addSerie(getDataSerie("gtp2Fakt", gtp2.RealP,-1));
			answer.Chart.Data.addSerie(getDataSerie("gesFakt", ges.RealP,-1));
			answer.Chart.Data.addSerie(getDataSerie("gtp1Plan", gtp1.SteppedPBR,0));
			answer.Chart.Data.addSerie(getDataSerie("gtp2Plan", gtp2.SteppedPBR,0));
			answer.Chart.Data.addSerie(getDataSerie("gesPlan", ges.SteppedPBR,0));
			
			answer.Chart.Data.addSerie(getDataSerie("vyrabPlan", ges.IntegratedPBR,-1));
			answer.Chart.Data.addSerie(getDataSerie("vyrabFakt", ges.IntegratedP,-1));

			return answer;
		}

		public static ChartDataSerie getDataSerie(string serieName, SortedList<DateTime, double> data, int correctTime) {
			ChartDataSerie serie=new ChartDataSerie();
			serie.Name = serieName;
			foreach (KeyValuePair<DateTime,double> de in data) {
				serie.Points.Add(new ChartDataPoint(de.Key.AddMinutes(correctTime), de.Value));
			}
			return serie;
		}

		public static ChartProperties getChartProperties() {
			ChartProperties props=new ChartProperties();
			props.XAxisType = XAxisTypeEnum.datetime;
			props.XValueFormatString = "dd.MM HH:mm";

			ChartAxisProperties pAx=new ChartAxisProperties();
			pAx.Auto = true;
			pAx.Min = 0;
			pAx.Max = 1050;
			pAx.Interval = 50;
			pAx.Index = 0;

			ChartAxisProperties vAx=new ChartAxisProperties();
			vAx.Auto = true;
			vAx.Index = 1;

			props.addAxis(pAx);
			props.addAxis(vAx);

			ChartSerieProperties gtp1FaktSerie=new ChartSerieProperties();
			gtp1FaktSerie.Color="0-255-0";
			gtp1FaktSerie.Title = "ГТП-1 факт";
			gtp1FaktSerie.TagName = "gtp1Fakt";
			gtp1FaktSerie.LineWidth = 2;
			gtp1FaktSerie.SerieType = ChartSerieType.stepLine;
			gtp1FaktSerie.YAxisIndex = 0;
			gtp1FaktSerie.Enabled = true;

			ChartSerieProperties gtp2FaktSerie=new ChartSerieProperties();
			gtp2FaktSerie.Color = "0-0-255";
			gtp2FaktSerie.Title = "ГТП-2 факт";
			gtp2FaktSerie.TagName = "gtp2Fakt";
			gtp2FaktSerie.LineWidth = 2;
			gtp2FaktSerie.SerieType = ChartSerieType.stepLine;
			gtp2FaktSerie.YAxisIndex = 0;
			gtp2FaktSerie.Enabled = true;

			ChartSerieProperties gesFaktSerie=new ChartSerieProperties();
			gesFaktSerie.Color = "0-0-0";
			gesFaktSerie.Title = "ГЭС факт";
			gesFaktSerie.TagName = "gesFakt";
			gesFaktSerie.LineWidth = 2;
			gesFaktSerie.SerieType = ChartSerieType.stepLine;
			gesFaktSerie.YAxisIndex = 0;
			gesFaktSerie.Enabled = false;

			ChartSerieProperties gtp1PlanSerie=new ChartSerieProperties();
			gtp1PlanSerie.Color = "0-255-0";
			gtp1PlanSerie.Title = "ГТП-1 план";
			gtp1PlanSerie.TagName = "gtp1Plan";
			gtp1PlanSerie.LineWidth = 1;
			gtp1PlanSerie.SerieType = ChartSerieType.stepLine;
			gtp1PlanSerie.YAxisIndex = 0;
			gtp1PlanSerie.Enabled = true;

			ChartSerieProperties gtp2PlanSerie=new ChartSerieProperties();
			gtp2PlanSerie.Color = "0-0-255";
			gtp2PlanSerie.Title = "ГТП-2 план";
			gtp2PlanSerie.TagName = "gtp2Plan";
			gtp2PlanSerie.LineWidth = 1;
			gtp2PlanSerie.SerieType = ChartSerieType.stepLine;
			gtp2PlanSerie.YAxisIndex = 0;
			gtp2PlanSerie.Enabled = true;

			ChartSerieProperties gesPlanSerie=new ChartSerieProperties();
			gesPlanSerie.Color = "0-0-0";
			gesPlanSerie.Title = "ГЭС план";
			gesPlanSerie.TagName = "gesPlan";
			gesPlanSerie.LineWidth = 1;
			gesPlanSerie.SerieType = ChartSerieType.stepLine;
			gesPlanSerie.YAxisIndex = 0;
			gesPlanSerie.Enabled = false;

			ChartSerieProperties vyrabFaktSerie=new ChartSerieProperties();
			vyrabFaktSerie.Color = "255-0-0";
			vyrabFaktSerie.Title = "Выработка факт";
			vyrabFaktSerie.TagName = "vyrabFakt";
			vyrabFaktSerie.LineWidth = 2;
			vyrabFaktSerie.SerieType = ChartSerieType.stepLine;
			vyrabFaktSerie.YAxisIndex = 1;
			vyrabFaktSerie.Enabled = true;

			ChartSerieProperties vyrabPlanSerie=new ChartSerieProperties();
			vyrabPlanSerie.Color = "255-0-0";
			vyrabPlanSerie.Title = "Выработка план";
			vyrabPlanSerie.TagName = "vyrabPlan";
			vyrabPlanSerie.LineWidth = 1;
			vyrabPlanSerie.SerieType = ChartSerieType.stepLine;
			vyrabPlanSerie.YAxisIndex = 1;
			vyrabPlanSerie.Enabled = true;

			props.addSerie(gtp1FaktSerie);
			props.addSerie(gtp2FaktSerie);
			props.addSerie(gesFaktSerie);
			props.addSerie(gtp1PlanSerie);
			props.addSerie(gtp2PlanSerie);
			props.addSerie(gesPlanSerie);
			props.addSerie(vyrabPlanSerie);
			props.addSerie(vyrabFaktSerie);
			

			return props;
		}
	}
}
