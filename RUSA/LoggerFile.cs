using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using VotGES;

namespace RUSA
{
	class LoggerFile:Logger
	{


		protected override string createMessage(string message, LoggerSource source = LoggerSource.server, string user = "", string ip = "") {
			return DateTime.Now.ToString() + "_" + message;
		}
	}
}
