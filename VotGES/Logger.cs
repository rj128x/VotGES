using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using log4net;
using log4net.Appender;
using log4net.Layout;
using log4net.Config;
using System.Web;

namespace VotGES
{
	public class Logger
	{
		public enum LoggerSource { server, client, ordersContext, objectsContext, usersContext, service }
		public log4net.ILog logger;

		protected static Logger context;

		protected Logger() {
			
		}		


		public static void init(string path, string name) {
			context = new Logger();
			string fileName=String.Format("{0}/{1}_{2}.txt", path, name, DateTime.Now.ToShortDateString().Replace(":", "_").Replace("/", "_").Replace(".", "_"));
			PatternLayout layout = new PatternLayout(@"[%d] %-10p %m%n");
			FileAppender appender=new FileAppender();
			appender.Layout = layout;
			appender.File = fileName;
			appender.AppendToFile = true;
			BasicConfigurator.Configure(appender);
			appender.ActivateOptions();
			context.logger = LogManager.GetLogger(name);
		}

		protected virtual  string createMessage(string message, LoggerSource source=LoggerSource.server,string user="", string ip="") {
			try {
				return String.Format("{0,-30} {1,-15} {2,-20} {3}", user, ip, source.ToString(), message);
			} catch {
				return String.Format("{0,-30} {1,-15} {2,-20} {3}", "", "", source.ToString(), message); ;
			}
		}

		protected void info(string str, LoggerSource source = LoggerSource.server) {
			logger.Info(createMessage(str, source));
		}

		protected void error(string str, LoggerSource source = LoggerSource.server) {
			logger.Error(createMessage(str, source));
		}

		protected void debug(string str, LoggerSource source = LoggerSource.server) {
			logger.Debug(createMessage(str, source));
		}


		public static void Info(string str, LoggerSource source = LoggerSource.server) {
			context.info(str, source);
		}

		public static void Error(string str, LoggerSource source = LoggerSource.server) {
			context.info(str, source);
		}

		public static void Debug(string str, LoggerSource source = LoggerSource.server) {
			context.info(str, source);
		}

	}
}
