
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
	using VotGES.Web.Logging;


	// Реализует логику приложения с использованием контекста Piramida3000Entities.
	// TODO: добавьте свою прикладную логику в эти или другие методы.
	// TODO: включите проверку подлинности (Windows/ASP.NET Forms) и раскомментируйте следующие строки, чтобы запретить анонимный доступ
	// Кроме того, рассмотрите возможность добавления ролей для соответствующего ограничения доступа.
	// [RequiresAuthentication]
	[EnableClientAccess()]
	public class ChartService : DomainService
	{
		
		public ChartAnswer checkPrognozNB(DateTime date,int countDays) {
			WebLogger.Info(String.Format("Получение прогноза (факт) {0} - {1}", date, countDays));
			try {
				if (date.AddHours(-2).Date >= DateTime.Now.Date)
					date = DateTime.Now.AddHours(-2).Date.AddHours(-24);
				CheckPrognozNB prognoz=new CheckPrognozNB(date.Date,countDays);
				//PrognozNBByPBR prognoz=new PrognozNBByPBR(date.Date, 1, date.Date.AddHours(8).AddMinutes(15),null);
				prognoz.startPrognoz();
				return prognoz.getChart();
			} catch (Exception e) {
				WebLogger.Error(e.ToString());
				return null;
			}
		}

		public PrognozNBByPBRAnswer getPrognoz( int countDays, SortedList<DateTime,double> pbr) {
			WebLogger.Info(String.Format("Получение прогноза (прогноз) {0} [{1}]", countDays, pbr == null ? "" : String.Join(" ", pbr)));
			try {
				//DateTime date= DateTime.Now.AddHours(-2);
				DateTime date=new DateTime(2010, 03, 14);
				date = date.AddHours(15).AddMinutes(15);
				PrognozNBByPBR prognoz=new PrognozNBByPBR(date.Date,countDays,date,pbr);
				prognoz.startPrognoz();
				return prognoz.PrognozAnswer;
			} catch (Exception e) {
				WebLogger.Error(e.ToString());
				return null;
			}
		}
	}
}


