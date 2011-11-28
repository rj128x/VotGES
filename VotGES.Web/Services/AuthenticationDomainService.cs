using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.ServiceModel.DomainServices.Hosting;
using System.ServiceModel.DomainServices.Server;
using System.ServiceModel.DomainServices.Server.ApplicationServices;

namespace VotGES.Web.Services
{
	[EnableClientAccess]
	public class AuthenticationDomainService : AuthenticationBase<User>
	{
		protected override User GetAuthenticatedUser(System.Security.Principal.IPrincipal principal) {
			Logger.Info(String.Format("Пользователь пытается авторизоваться в системе"), Logger.LoggerSource.server);
			User user=base.GetAuthenticatedUser(principal);
			Logger.Info(String.Format("Пользователь авторизовался в системе: {0}", user), Logger.LoggerSource.server);

			return user;
		}
	}

	public class User : UserBase
	{
		// NOTE: Profile properties can be added here 
		// To enable profiles, edit the appropriate section of web.config file.

		// public string MyProfileProperty { get; set; }


	}
}