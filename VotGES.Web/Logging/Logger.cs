using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using VotGES;


namespace VotGES.Web.Logging
{
	public class Logger:VotGES.Logger
	{
		protected override string createMessage(string message, LoggerSource source = LoggerSource.server, string user = "", string ip = "") {
			try {
				user=HttpContext.Current.User.Identity.IsAuthenticated ? HttpContext.Current.User.Identity.Name : "Anonimous";
				ip= HttpContext.Current.Request.UserHostAddress;
			}catch{
			}
			return base.createMessage(message, source, user, ip);
		}
	}
}