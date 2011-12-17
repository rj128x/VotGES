using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.ServiceModel.DomainServices.Hosting;
using System.ServiceModel.DomainServices.Server;
using System.ServiceModel.DomainServices.Server.ApplicationServices;
using VotGES.Web.Models;

namespace VotGES.Web.Services
{
	[EnableClientAccess]
	public class RUSAService : DomainService
	{
		public RUSAData processRUSAData(RUSAData data) {
			Logger.Info("RUSA process", VotGES.Logger.LoggerSource.service);
			ProcessRUSAData.processEqualData(data);
			ProcessRUSAData.processDiffData(data);
			return data;
		}

		public int initRUSAData(RUSAData data) {
			return 10;
		}
	}

}