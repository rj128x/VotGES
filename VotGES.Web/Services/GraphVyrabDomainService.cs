
namespace VotGES.Web.Services
{
	using System;
	using System.Collections.Generic;
	using System.ComponentModel;
	using System.ComponentModel.DataAnnotations;
	using System.Linq;
	using System.ServiceModel.DomainServices.Hosting;
	using System.ServiceModel.DomainServices.Server;
	using VotGES.PBR;


	// TODO: создайте методы, содержащие собственную логику приложения.
	[EnableClientAccess()]
	public class GraphVyrabDomainService : DomainService
	{
		public GraphVyrabAnswer getGraphVyrab() {
			try {
				DateTime date=new DateTime(2010, 3, 15);
				date = date.AddHours(DateTime.Now.Hour - 2).AddMinutes(DateTime.Now.Minute);
				return GraphVyrab.getAnswer(date, true);
			} catch (Exception e) {
				Logger.Error("Ошибка при получении графика нагрузки " + e);
				return null;
			}
		}

		public GraphVyrabAnswer getGraphVyrabMin(DateTime date) {
			try {
				date = date.Date.AddHours(23).AddMinutes(59);
				return GraphVyrab.getAnswer(date, false);
			} catch (Exception e) {
				Logger.Error("Ошибка при получении факта нагрузки " + e);
				return null;
			}
		}

		public CheckGraphVyrabAnswer getGraphVyrabHH(DateTime date) {
			try {
				date = date.Date;
				return GraphVyrab.getAnswerHH(date);
			} catch (Exception e) {
				Logger.Error("Ошибка при получении факта нагрузки " + e);
				return null;
			}
		}
	}
}


