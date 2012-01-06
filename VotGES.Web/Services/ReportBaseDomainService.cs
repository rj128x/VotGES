
namespace VotGES.Web.Services
{
	using System;
	using System.Collections.Generic;
	using System.ComponentModel;
	using System.ComponentModel.DataAnnotations;
	using System.Linq;
	using System.ServiceModel.DomainServices.Hosting;
	using System.ServiceModel.DomainServices.Server;
using VotGES.Piramida.PiramidaReport;


	// TODO: создайте методы, содержащие собственную логику приложения.
	[EnableClientAccess()]
	public class ReportBaseDomainService : DomainService
	{
		public ReportAnswer GetRezhimSKReport(DateTime date) {
			try {
				RezhimSKReport report=new RezhimSKReport(date.Date, date.Date.AddDays(1), IntervalReportEnum.halfHour);
				report.ReadData();
				report.CreateAnswerData();
				report.CreateChart();
				Logger.Info(String.Join("-", report.Data));
				return report.Answer;
			} catch (Exception e) {
				Logger.Error("Ошибка при получении режима СК " + e.ToString());
				return null;
			}
		}

	}
}


