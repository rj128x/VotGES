﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace VotGES.Piramida
{
	public class PiramidaAccess
	{
		public static string conString=@"metadata=res://*/Piramida.Piramida3000.csdl|res://*/Piramida.Piramida3000.ssdl|res://*/Piramida.Piramida3000.msl;provider=System.Data.SqlClient;provider connection string='Data Source=.\sqlexpress;Initial Catalog=Piramida3000;Integrated Security=True'";
		static PiramidaAccess() {
			


		}
		public static Piramida3000Entities getModel() {
			return new Piramida3000Entities(conString);
		}
	}
}
