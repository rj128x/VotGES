using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace VotGES.Piramida
{
	public class PiramidaRecord
	{
		public int ObjType{get;protected set;}
		public int Obj { get; protected set; }
		public int Item { get; protected set; }
		public string Title { get; protected set; }
		public string Key { get; protected set; }
		public PiramidaRecord(int objType, int obj, int item, string title){
			ObjType = objType;
			Obj = obj;
			Item = item;
			Title = title;
			Key = String.Format("{0}-{1}-{2}", ObjType, Obj, Item);			
		}
		public static string GetKey(DATA data) {			
			string Key = String.Format("{0}-{1}-{2}", data.OBJTYPE,data.OBJECT,data.ITEM);
			return Key;
		}
	}
}
