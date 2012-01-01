
namespace VotGES.Web.Services
{
	using System;
	using System.Collections.Generic;
	using System.ComponentModel;
	using System.ComponentModel.DataAnnotations;
	using System.Linq;
	using System.ServiceModel.DomainServices.Hosting;
	using System.ServiceModel.DomainServices.Server;
using VotGES.Reports;


	// TODO: создайте методы, содержащие собственную логику приложения.
	[EnableClientAccess()]
	public class RezhimSKDomainService : DomainService
	{
		public List<ReportRecord> getReznimSKReport(DateTime date) {
			RezhimSKReport report=new RezhimSKReport();
			report.DateStart = date.Date;
			report.DateEnd = date.Date.AddHours(24);
			report.ReadData();
			return report.getReportData();
		}
	}
}


