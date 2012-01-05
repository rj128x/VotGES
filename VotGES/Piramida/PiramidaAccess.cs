using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data.Common;
using System.Data.SqlClient;

namespace VotGES.Piramida
{
	public class PiramidaAccess
	{
		public static string conString=@"metadata=res://*/Piramida.Piramida3000.csdl|res://*/Piramida.Piramida3000.ssdl|res://*/Piramida.Piramida3000.msl;provider=System.Data.SqlClient;provider connection string='Data Source=.\sqlexpress;Initial Catalog=Piramida3000;Integrated Security=True'";
		public static string pureConString=@"Data Source=.\sqlexpress;Initial Catalog=Piramida3000;Integrated Security=True";
		public static string conStringP2000=@"metadata=res://*/Piramida.Piramida3000.csdl|res://*/Piramida.Piramida3000.ssdl|res://*/Piramida.Piramida3000.msl;provider=System.Data.SqlClient;provider connection string='Data Source=.\sqlexpress;Initial Catalog=Piramida2000;Integrated Security=True'";
		static PiramidaAccess() {
			


		}



		public static Piramida3000Entities getModel() {
			return new Piramida3000Entities(conString);
		}

		public static SqlConnection getConnection() {
			SqlConnection con=new SqlConnection(pureConString);
			return con;
		}

		public static Piramida3000Entities getModelP2000() {
			return new Piramida3000Entities(conStringP2000);
		}
	}
}
