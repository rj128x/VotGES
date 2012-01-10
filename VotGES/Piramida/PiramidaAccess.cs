using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data.Common;
using System.Data.SqlClient;

namespace VotGES.Piramida
{
	public class PiramidaEnrty
	{
		public DateTime Date { get; set; }
		public int ParNumber{get;set;}
		public int Object { get; set; }
		public int ObjType { get; set; }
		public double Value0 { get; set; }
		public int Item { get; set; }
	}

	public class PiramidaAccess
	{
		public static string conString=@"metadata=res://*/Piramida.Piramida3000.csdl|res://*/Piramida.Piramida3000.ssdl|res://*/Piramida.Piramida3000.msl;provider=System.Data.SqlClient;provider connection string='Data Source=sr-votges-int\sqlexpress;Initial Catalog=Piramida3000;Integrated Security=True;'";
		public static string pureConString=@"Data Source=192.168.140.240;Initial Catalog=Piramida3000;Persist Security Info=True;User ID=sa;Password=pswd";
		public static SqlConnection Connection { get; set; }
		static PiramidaAccess() {
			Connection = new SqlConnection(pureConString);

		}

		public static List<PiramidaEnrty> GetDataFromDB(DateTime dateStart, DateTime dateEnd, int obj, int objType, int parNumber, List<int> items, bool includeFirst = false, bool includeLast = true) {
			
			List<PiramidaEnrty> result=new List<PiramidaEnrty>();
			SqlConnection connection=PiramidaAccess.getConnection();
			SqlDataReader reader=null;
			SqlCommand command=null;
			connection.Open();
			try {
				command= connection.CreateCommand();
				command.Parameters.AddWithValue("@dateStart", dateStart);
				command.Parameters.AddWithValue("@dateEnd", dateEnd);

				string itemsStr=String.Join(",", items);

				string dateStartCond=includeFirst ? ">=" : ">";
				string dateEndCond=includeLast ? "<=" : "<";

				string valueParams=String.Format(" ( d.[DATA_DATE]{0}@dateStart and d.[DATA_DATE]{1}@dateEnd and d.[PARNUMBER]={2} and d.[OBJTYPE]={3} and d.[OBJECT]={4} and d.[ITEM] in ({5}) ) ",
					dateStartCond, dateEndCond, parNumber, objType, obj, itemsStr);


				command.CommandText = String.Format("SELECT d.[DATA_DATE], d.[OBJECT], d.[OBJTYPE], d.[ITEM], d.[PARNUMBER], d.[VALUE0] from DATA as d  WHERE {0}", valueParams);

				reader=command.ExecuteReader();
				
				while (reader.Read()) {
					PiramidaEnrty entry=new PiramidaEnrty();
					entry.Date = reader.GetDateTime(0);
					entry.Object = reader.GetInt32(1);
					entry.ObjType = reader.GetInt16(2);
					entry.Item = reader.GetInt32(3);
					entry.ParNumber = reader.GetInt32(4);
					entry.Value0 = reader.GetDouble(5);
					result.Add(entry);
				}		
				
			}finally{
				try { reader.Close(); }catch{}
				try { command.Dispose(); } catch { }
				try { connection.Close(); } catch { }
			}
			
			return result;
		}


		public static Piramida3000Entities getModel() {
			return new Piramida3000Entities(conString);
		}

		public static SqlConnection getConnection() {
			SqlConnection con=new SqlConnection(pureConString);			
			return con;
		}



		
	}
}
