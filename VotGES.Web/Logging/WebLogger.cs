using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using VotGES;


namespace VotGES.Web.Logging
{
	public class WebLogger:VotGES.Logger
	{
		protected override string createMessage(string message, LoggerSource source = LoggerSource.server) {
			try {
				string user = HttpContext.Current.User.Identity.IsAuthenticated ? HttpContext.Current.User.Identity.Name : "Anonimous";
				string ip = HttpContext.Current.Request.UserHostAddress;
				return String.Format("{0,-30} {1,-15} {2,-20} {3}", user, ip, source.ToString(), message);
			} catch {
				return String.Format("{0,-30} {1,-15} {2,-20} {3}", "", "", source.ToString(), message); ;
			}
			
		}
	}
}