using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;
using VotGES.Rashod;
using System.Globalization;

namespace VotGES
{
	public enum RashodCalcMode{avg,min,max};
	
	public class RashodTable
	{
		

		protected double[] napors;
		protected double[] powers;
		protected double[][] rashods;
		protected SortedList<double,SortedList<double,double>> rashodsByNapor;
		protected double minPower;
		protected double maxPower;
		protected double minNapor;
		protected double maxNapor;		
		protected double stepNapor;
		protected double stepPower;

		protected static RashodTable[] rashodTables;
		public static string debugId=Guid.NewGuid().ToString();

		static RashodTable() {
			rashodTables = new RashodTable[13];
		}

		protected RashodTable(int ga) {
			NumberFormatInfo nfi = new CultureInfo("ru-RU", false).NumberFormat;
			nfi.NumberDecimalSeparator = ".";
			string str=null;
			int len=111;
			switch (ga) {
				case 12:
					str = VotGES.rashods.opt;
					len = 1035;
					break;
				case 11:
					str = VotGES.rashods.avg;
					len = 1035;
					break;
				case 1:
					str = VotGES.rashods._1;
					break;
				case 2:
					str = VotGES.rashods._2;
					break;
				case 3:
					str = VotGES.rashods._3;
					break;
				case 4:
					str = VotGES.rashods._4;
					break;
				case 5:
					str = VotGES.rashods._5;
					break;
				case 6:
					str = VotGES.rashods._6;
					break;
				case 7:
					str = VotGES.rashods._7;
					break;
				case 8:
					str = VotGES.rashods._8;
					break;
				case 9:
					str = VotGES.rashods._9;
					break;
				case 10:
					str = VotGES.rashods._10;
					break;
			}
			string[]rows=str.Split('\n');
			int Row = 0;
			foreach (string strLine in rows) {
				try {
					string[] Line = strLine.Replace(",",".").Split(';');
					if (Row == 0) {
						//strArray = new string[Line.Length, 111];
						napors = new double[Line.Length - 1];
						rashods = new double[len][];
						powers = new double[len];
						for (int column=1; column < Line.Length; column++) {
							napors[column - 1] = Double.Parse(Line[column], nfi);
						}
					} else {
						rashods[Row-1]=new double[Line.Length - 1];
						powers[Row - 1] = Double.Parse(Line[0], nfi);
						for (int column=1; column < Line.Length; column++) {
							rashods[Row - 1][column - 1] = Double.Parse(Line[column], nfi);
						}
					}
					Row++;
				} catch { }
			}
			minPower = powers[0];
			maxPower = powers[powers.Length - 1];
			minNapor = napors[0];
			maxNapor = napors[napors.Length - 1];
			stepPower = powers[1] - powers[0];
			stepNapor = napors[1] - napors[0];

			rashodsByNapor = new SortedList<double, SortedList<double, double>>();

			for (int naporIndex=0; naporIndex < napors.Length; naporIndex++) {
				double napor=napors[naporIndex];
				rashodsByNapor.Add(napor, new SortedList<double, double>());
				rashodsByNapor[napor].Add(0, 0);
				for (int powerIndex=0; powerIndex < powers.Length; powerIndex++) {
					try {
						double rashod=rashods[powerIndex][naporIndex];
						double power=powers[powerIndex];
						rashodsByNapor[napor].Add(power, rashod);
					}catch{}
				}
			}

		}

		public static RashodTable getRashodTable(int ga) {
			if (rashodTables[ga] == null) {				
				rashodTables[ga] = new RashodTable(ga);				
			}
			return rashodTables[ga];
		}

		protected double getRashod(double power, double napor) {
			try {				
				KeyValuePair<double,SortedList<double,double>> naporRashod1=rashodsByNapor.Last(de => de.Key <= napor);
				KeyValuePair<double,SortedList<double,double>> naporRashod2=rashodsByNapor.First(de => de.Key >= napor);
				KeyValuePair<double,double>powerRashod11=naporRashod1.Value.Last(de => de.Key <= power);
				KeyValuePair<double,double>powerRashod12=naporRashod1.Value.First(de => de.Key >= power);
				KeyValuePair<double,double>powerRashod21=naporRashod2.Value.Last(de => de.Key <= power);
				KeyValuePair<double,double>powerRashod22=naporRashod2.Value.First(de => de.Key >= power);

				double naporK=(napor - naporRashod1.Key) / (naporRashod2.Key - naporRashod1.Key);
				naporK = Double.IsNaN(naporK) ? 0 : naporK;
				double powerK=(power - powerRashod11.Key) / (powerRashod12.Key - powerRashod11.Key);
				powerK = Double.IsNaN(powerK) ? 0 : powerK;

				double rashod1=powerRashod11.Value + (powerRashod12.Value - powerRashod11.Value) * powerK;
				double rashod2=powerRashod21.Value + (powerRashod22.Value - powerRashod21.Value) * powerK;
				double rashod=rashod1 + (rashod2 - rashod1) * naporK;
				//Logger.debug(String.Format("Расход мощность: {0} napor: {1} ({2})", power, napor, rashod));
				return rashod;
			} catch (Exception e) {
				Logger.Error(String.Format("Ошибка получения расхода мощность: {0} napor: {1} ({2})",power,napor,e.Message));
				return 0;
			}
		}

		

		protected double getPower(double rashod, double napor) {
			try {
				KeyValuePair<double,SortedList<double,double>> naporRashod1=rashodsByNapor.Last(de => de.Key <= napor);
				KeyValuePair<double,SortedList<double,double>> naporRashod2=rashodsByNapor.First(de => de.Key >= napor);
				KeyValuePair<double,double>powerRashod11=naporRashod1.Value.Last(de => de.Value <= rashod);
				KeyValuePair<double,double>powerRashod12=naporRashod1.Value.First(de => de.Value >= rashod);
				KeyValuePair<double,double>powerRashod21=naporRashod2.Value.Last(de => de.Value <= rashod);
				KeyValuePair<double,double>powerRashod22=naporRashod2.Value.First(de => de.Value >= rashod);

				double naporK=(napor - naporRashod1.Key) / (naporRashod2.Key - naporRashod1.Key);
				naporK = Double.IsNaN(naporK) ? 0 : naporK;
				double rashod1K=(rashod - powerRashod11.Value) / (powerRashod12.Value - powerRashod11.Value);
				rashod1K = Double.IsNaN(rashod1K) ? 0 : rashod1K;
				double rashod2K=(rashod - powerRashod21.Value) / (powerRashod22.Value - powerRashod21.Value);
				rashod2K = Double.IsNaN(rashod2K) ? 0 : rashod2K;

				double power1=powerRashod11.Key + (powerRashod12.Key - powerRashod11.Key) * rashod1K;
				double power2=powerRashod21.Key + (powerRashod22.Key - powerRashod21.Key) * rashod2K;
				double power=power1 + (power2 - power1) * naporK;

				//Logger.debug(String.Format("Мощность расход: {0} napor: {1} ({2})", rashod, napor, power));
				return rashod;
			} catch (Exception e) {
				Logger.Error(String.Format("Ошибка получения мощности расход: {0} napor: {1} ({2})", rashod, napor, e.Message));
				return 0;
			}
		}

		protected double getMaxKPDPower(double napor) {
			double maxKPD=-1;
			double maxKPDPower=-1;
			double kpd=0;
			for (double power=minPower; power <= maxPower; power += stepPower) {
				kpd = KPD(power, napor, getRashod(power, napor));
				if (kpd > maxKPD) {
					maxKPD = kpd;
					maxKPDPower = power;
				}
			}
			return maxKPDPower;
		}

		public static Dictionary<int,double> KPDArr(double napor) {
			SortedList<double,int> kpdArr=new SortedList<double,int>();
			SortedList<int,double> powerArr=new SortedList<int,double>();
			for (int ga=1;ga<=10;ga++){
				double maxKPDPower=getRashodTable(ga).getMaxKPDPower(napor);
				double kpd=KPD(maxKPDPower,napor,getRashodTable(ga).getRashod(maxKPDPower,napor));
				kpdArr.Add(kpd,ga);
				powerArr.Add(ga,maxKPDPower);
			}
			Dictionary<int,double> result=new Dictionary<int,double>();
			for (int index=1;index<=10;index++){
				int ga=kpdArr.Values[10-index];
				result.Add(ga,powerArr[ga]);
			}
			return result;
		}

		public static double getRashod(int ga, double power, double napor) {
			if (power == 0) return 0;
			return getRashodTable(ga).getRashod(power, napor);
		}

		public static double KPD(double power, double napor, double rashod) {
			return 1000 * power / (9.81 * napor) / rashod;
		}

		public static double getStationRashod(double power, double napor, RashodCalcMode mode) {
			/*if (napor < 16)
				mode = RashodCalcMode.min;*/
			if (power == 0)
				return 0;
			switch (mode) {
				case RashodCalcMode.avg:
					return getRashodTable(11).getRashod(power, napor);
				case RashodCalcMode.min:
					return RUSA.getOptimRashod(power, napor, true);
				case RashodCalcMode.max:
					return RUSA.getOptimRashod(power, napor, false);
				default:
					return getRashodTable(12).getRashod(power, napor);
			}
		}

		
	}
}
