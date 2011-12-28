
namespace VotGES.Web.Services
{
	using System;
	using System.Collections.Generic;
	using System.ComponentModel;
	using System.ComponentModel.DataAnnotations;
	using System.Data;
	using System.Linq;
	using System.ServiceModel.DomainServices.EntityFramework;
	using System.ServiceModel.DomainServices.Hosting;
	using System.ServiceModel.DomainServices.Server;
	using VotGES.Piramida;
	using VotGES.Chart;
	using VotGES.PrognozNB;


	// Реализует логику приложения с использованием контекста Piramida3000Entities.
	// TODO: добавьте свою прикладную логику в эти или другие методы.
	// TODO: включите проверку подлинности (Windows/ASP.NET Forms) и раскомментируйте следующие строки, чтобы запретить анонимный доступ
	// Кроме того, рассмотрите возможность добавления ролей для соответствующего ограничения доступа.
	// [RequiresAuthentication]
	[EnableClientAccess()]
	public class ChartService : DomainService
	{
		public ChartAnswer processChart() {
			Logger.Info("getChart");
			try {
				ChartAnswer answer=new ChartAnswer();
				ChartProperties props=new ChartProperties();
				props.XAxisType = XAxisTypeEnum.auto;

				ChartAxisProperties ax=new ChartAxisProperties();
				ax.Index = 0;
				ax.Auto = true;
				props.Axes.Add(ax);

				ax = new ChartAxisProperties();
				ax.Index = 1;
				ax.Auto = true;
				props.Axes.Add(ax);


				ChartSerieProperties serie=new ChartSerieProperties();
				serie.Color = "255-0-0";
				serie.Enabled = true;
				serie.LineWidth = 2;
				serie.Marker = true;
				serie.SerieType = ChartSerieType.line;
				serie.Title = "Парабола";
				serie.TagName = "s1";
				serie.YAxisIndex = 0;
				props.Series.Add(serie);
				

				serie = new ChartSerieProperties();
				serie.Color = "0-255-0";
				serie.Enabled = true;
				serie.LineWidth = 2;
				serie.Marker = true;
				serie.SerieType = ChartSerieType.line;
				serie.Title = "Парабола2";
				serie.TagName = "s2";
				serie.YAxisIndex = 1;
				props.Series.Add(serie);

				

				answer.Properties = props;

				ChartData data=new ChartData();

				ChartDataSerie serieData=new ChartDataSerie();
				serieData.Name = "s1";

				ChartDataSerie serieData2=new ChartDataSerie();
				serieData2.Name = "s2";

				for (int i=-10; i <= 10; i++) {
					serieData.Points.Add(new ChartDataPoint(new DateTime(2010,1,20+i), i * i));
					serieData2.Points.Add(new ChartDataPoint(new DateTime(2010, 1, 20+i), i * i * i));
				}

				data.Series.Add(serieData);
				data.Series.Add(serieData2);

				answer.Data = data;
				Logger.Info(answer.ToString());
				return answer;
			} catch (Exception e) {
				Logger.Error(e.ToString());
				return null;
			}
		}

		public ChartAnswer checkPrognozNB(DateTime date,int countDays) {
			Logger.Info(String.Format("Получение прогноза (факт) {0} - {1}",date,countDays));
			try {
				if (date.AddHours(-2).Date >= DateTime.Now.Date)
					date = DateTime.Now.AddHours(-2).Date.AddHours(-24);
				CheckPrognozNB prognoz=new CheckPrognozNB(date.Date,countDays);
				//PrognozNBByPBR prognoz=new PrognozNBByPBR(date.Date, 1, date.Date.AddHours(8).AddMinutes(15),null);
				prognoz.startPrognoz();
				return prognoz.getChart();
			} catch (Exception e) {
				Logger.Error(e.ToString());
				return null;
			}
		}

		public ChartAnswer getPrognoz( int countDays, SortedList<DateTime,double> pbr) {
			Logger.Info(String.Format("Получение прогноза (прогноз) {0} [{1}]",countDays, pbr==null?"":String.Join(" ",pbr)));
			try {
				DateTime date= DateTime.Now.AddHours(-2);
				//DateTime date=new DateTime(2010, 03, 15);
				//date = date.AddHours(15).AddMinutes(15);
				PrognozNBByPBR prognoz=new PrognozNBByPBR(date.Date,countDays,date,pbr);
				prognoz.startPrognoz();
				return prognoz.getChart();
			} catch (Exception e) {
				Logger.Error(e.ToString());
				return null;
			}
		}
	}
}


