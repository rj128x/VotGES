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
		public static string pureConString2000=@"Data Source=192.168.140.240;Initial Catalog=Piramida2000;Persist Security Info=True;User ID=sa;Password=pswd";
		static PiramidaAccess() {
			


		}

		public static List<PiramidaEnrty> GetDataFromDB(DateTime dateStart, DateTime dateEnd,int obj, int objType,int parNumber, List<int> items, bool includeFirst=false, bool includeLast=true, bool is2000=false){
			
			List<PiramidaEnrty> result=new List<PiramidaEnrty>();
			SqlConnection connection=is2000?PiramidaAccess.getConnection():PiramidaAccess.getConnection2000();
			SqlCommand command= connection.CreateCommand();
			command.Parameters.AddWithValue("@dateStart", dateStart);
			command.Parameters.AddWithValue("@dateEnd", dateEnd);

			string itemsStr=String.Join(",",items);
			string dateStartCond=includeFirst?">=":">";
			string dateEndCond=includeLast?"<=":"<";

			string valueParams=String.Format(" ( DATA_DATE{0}@dateStart and DATA_DATE{1}@dateEnd and PARNUMBER={2} and OBJTYPE={3} and OBJECT={4} and ITEM in ({5}) ) ",
				dateStartCond,dateEndCond,parNumber,objType,obj,itemsStr);

			connection.Open();
			command.CommandText = String.Format("SELECT * from DATA  WHERE {0}",valueParams);
			Logger.Info(command.CommandText.Replace("@dateStart", String.Format("'{0}'", dateStart)).Replace("@dateEnd", String.Format("'{0}'", dateEnd)));
			SqlDataReader reader=command.ExecuteReader();
			while (reader.Read()){
				Logger.Info(reader["OBJECT"].ToString());
				PiramidaEnrty entry=new PiramidaEnrty();
				entry.Date=(DateTime)reader["DATA_DATE"];
				entry.Object=(int)reader["OBJECT"];
				entry.ObjType=((Int16)reader["OBJTYPE"]);
				entry.Item = ((int)reader["ITEM"]);
				entry.ParNumber=(int)reader["PARNUMBER"];
				entry.Value0=(double)reader["VALUE0"];
				result.Add(entry);
			}
			reader.Close();
			connection.Close();
			return result;
		}


		public static Piramida3000Entities getModel() {
			return new Piramida3000Entities(conString);
		}

		public static SqlConnection getConnection() {
			SqlConnection con=new SqlConnection(pureConString);			
			return con;
		}

		public static SqlConnection getConnection2000() {
			SqlConnection con=new SqlConnection(pureConString2000);
			return con;
		}


		
	}
}
