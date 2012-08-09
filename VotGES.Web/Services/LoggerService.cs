
namespace VotGES.Web.Services
{
	using System;
	using System.Collections.Generic;
	using System.ComponentModel;
	using System.ComponentModel.DataAnnotations;
	using System.Linq;
	using System.ServiceModel.DomainServices.Hosting;
	using System.ServiceModel.DomainServices.Server;
	using VotGES.Web.Logging;


	// TODO: Create methods containing your application logic.
	[EnableClientAccess()]
	public class LoggerService : DomainService
	{
		public void info(string message) {
			WebLogger.Info(String.Format("{0}", message), Logger.LoggerSource.client);
		}

		public void error(string message) {
			WebLogger.Error(String.Format("{0}", message), Logger.LoggerSource.client);
		}

		public void debug(string message) {
			WebLogger.Debug(String.Format("{0}", message), Logger.LoggerSource.client);
		}

		
	}
}


