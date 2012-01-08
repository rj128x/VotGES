
namespace VotGES.Web.Services
{
	using System;
	using System.Collections.Generic;
	using System.ComponentModel;
	using System.ComponentModel.DataAnnotations;
	using System.Linq;
	using System.ServiceModel.DomainServices.Hosting;
	using System.ServiceModel.DomainServices.Server;
	using VotGES.Piramida.Report;


	// TODO: создайте методы, содержащие собственную логику приложения.
	[EnableClientAccess()]
	public class ReportBaseDomainService : DomainService
	{
		public FullReportRecord GetFullReportRoot() {
			try {
				Logger.Info("Получение данных для полного отчета");
				FullReportInitData report=new FullReportInitData();
				return report.Root;
			} catch (Exception e) {
				Logger.Error("Ошибка при получении данных для полного отчета "+e.ToString());
				return null;

			}
		}

		public ReportAnswer GetFullReport(List<string> selectedData, DateTime dateStart, DateTime dateEnd, ReportTypeEnum ReportType) {
			try {
				Logger.Info(String.Format("Получение отчета {0} - {1} [{2}]",dateStart,dateEnd,ReportType));
				FullReport report=new FullReport(dateStart, dateEnd, Report.GetInterval(ReportType));
				report.InitNeedData(selectedData);
				report.ReadData();
				report.CreateAnswerData();
				report.CreateChart();
				return report.Answer;
			} catch (Exception e) {
				Logger.Error("Ошибка при получении отчета " + e.ToString());
				return null;
			}
		}

		public ReportAnswer GetRezhimSKReport(DateTime date) {
			try {
				Logger.Info("Получение отчета режим СК "+date);
				RezhimSKReport report=new RezhimSKReport(date.Date, date.Date.AddDays(1), IntervalReportEnum.halfHour);
				report.ReadData();
				report.CreateAnswerData();
				report.CreateChart();
				return report.Answer;
			} catch (Exception e) {
				Logger.Error("Ошибка при получении режима СК " + e.ToString());
				return null;
			}
		}

	}
}


