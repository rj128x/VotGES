
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
		public GraphVyrabAnswer getGraphVyrad() {
			DateTime date=new DateTime(2010, 3, 15);
			date = date.AddHours(DateTime.Now.Hour - 2).AddMinutes(DateTime.Now.Minute);
			return GraphVyrab.getAnswer(date);
		}
	}
}


