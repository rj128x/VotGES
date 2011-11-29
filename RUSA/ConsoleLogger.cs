using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using VotGES;

namespace RUSA
{
	class ConsoleLogger:Logger
	{
		protected override void debug(string str, LoggerSource source = LoggerSource.server) {
			Console.WriteLine(createMessage(str, source));
		}

		protected override void info(string str, LoggerSource source = LoggerSource.server) {
			Console.WriteLine(createMessage(str, source));
		}

		protected override void error(string str, LoggerSource source = LoggerSource.server) {
			Console.WriteLine(createMessage(str, source));
		}

		protected override string createMessage(string message, LoggerSource source = LoggerSource.server, string user = "", string ip = "") {
			return message;
		}
	}
}
