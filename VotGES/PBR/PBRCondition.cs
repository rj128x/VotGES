using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace VotGES.PBR
{
	public enum ConditionMode { min, max, equal }
	class PBRCondition
	{
		private int hourStart;
		public int HourStart {
			get { return hourStart; }
			set { hourStart = value; }
		}
	
		private int hourEnd;
		public int HourEnd {
			get { return hourEnd; }
			set { hourEnd = value; }
		}


		private double value;
		public double Value {
			get { return this.value; }
			set { this.value = value; }
		}


		private PBR parent;
		public PBR Parent {
			get { return parent; }
			set { parent = value; }
		}

		private bool isOk;
		public bool IsOk {
			get { return isOk; }
			set { isOk = value; }
		}

		private ConditionMode mode;
		public ConditionMode Mode {
			get { return mode; }
			set { mode = value; }
		}


	}
}
