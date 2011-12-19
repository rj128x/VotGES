
namespace VotGES.Web.Services
{
	using System;
	using System.Collections.Generic;
	using System.ComponentModel;
	using System.ComponentModel.DataAnnotations;
	using System.Linq;
	using System.ServiceModel.DomainServices.Hosting;
	using System.ServiceModel.DomainServices.Server;
	using VotGES.Web.Models;


	// TODO: создайте методы, содержащие собственную логику приложения.
	[EnableClientAccess()]
	public class RUSADomainService : DomainService
	{
		public RUSAData processRUSAData(RUSAData data) {
			Logger.Info("RUSA process", VotGES.Logger.LoggerSource.service);
			data.Result = new List<RUSAResult>();
			ProcessRUSAData.processEqualData(data);
			ProcessRUSAData.processDiffData(data);
			foreach (RUSAResult result in data.EqResult) {
				data.Result.Add(result);
			}
			foreach (RUSAResult result in data.DiffResult) {
				data.Result.Add(result);
			}
			return data;
		}
	}
}


