using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using VotGES.Chart;

namespace VotGES.PrognozNB
{
	public class PrognozNBFirstData
	{
		public DateTime Date { get; set; }
		public double Q { get; set; }
		public double P { get; set; }
		public double T { get; set; }
		public double NB { get; set; }
		public double VB { get; set; }
	}
	
	public class PrognozNB
	{
		protected SortedList<DateTime,PrognozNBFirstData> firstData;
		public SortedList<DateTime, PrognozNBFirstData> FirstData {
			get { return firstData; }
			set { firstData = value; }
		}

		double t=0;
		public double T {
			get { return t; }
			set { t = value; }
		}
		
		protected DateTime datePrognozStart;
		public DateTime DatePrognozStart {
			get { return datePrognozStart; }
			set { datePrognozStart = value; }
		}

		protected DateTime datePrognozEnd;
		public DateTime DatePrognozEnd {
			get { return datePrognozEnd; }
			set { datePrognozEnd = value; }
		}

		protected SortedList<DateTime, double> rashods;
		public SortedList<DateTime, double> Rashods {
			get { return rashods; }
			protected set { rashods = value; }
		}

		protected SortedList<DateTime, double> pArr;
		public SortedList<DateTime, double> PArr {
			get { return pArr; }
			set { pArr = value; }
		}

		protected  SortedList<DateTime, double> prognoz;
		public SortedList<DateTime, double> Prognoz
		{
			get { return prognoz; }
			set { prognoz = value; }
		}

		private bool isQFakt;

		public bool IsQFakt {
			get { return isQFakt; }
			set { isQFakt = value; }
		}

		protected static NNET.NNET nnet;

		static PrognozNB() {
			nnet = NNET.NNET.getNNET(NNET.NNETMODEL.vges_nb);
		}

		public void calcPrognoz() {
			if (PArr.Count % 24 != 0) {
				int addCnt=24 - PArr.Count % 24;
				for (int i=0; i < addCnt; i++) {
					DateTime last=PArr.Last().Key;
					PArr.Add(last.AddMinutes(30), PArr[last]);
				}
			}


			
			SortedList<int,double>prevDataRashodArray=new SortedList<int, double>();
			SortedList<int,double>prevDataNBArray=new SortedList<int, double>();
			SortedList<int,double>prevDataVBArray=new SortedList<int, double>();
			prognoz=new SortedList<DateTime, double>();

			int index=0;
			double k=0;
			foreach (DateTime date in FirstData.Keys) {
				prevDataRashodArray.Add(index, FirstData[date].Q);
				prevDataNBArray.Add(index, FirstData[date].NB);
				prevDataVBArray.Add(index, FirstData[date].VB);
				k = FirstData[date].Q / RashodTable.getStationRashod(FirstData[date].P, FirstData[date].VB - FirstData[date].NB, RashodCalcMode.avg);
				index++;
			}


			SortedList<DateTime,double>naporArray=new SortedList<DateTime, double>();
			rashods = new SortedList<DateTime, double>();


			double napor=prevDataVBArray.Last().Value - prevDataNBArray.Last().Value;
			foreach (KeyValuePair<DateTime,double>de in pArr) {
				naporArray.Add(de.Key, napor);
			}
		
			foreach (KeyValuePair<DateTime,double> de in PArr) {
				double rashod=IsQFakt ? de.Value : RashodTable.getStationRashod(de.Value, naporArray[de.Key],RashodCalcMode.avg)*k;
				rashods.Add(de.Key, rashod);
				prognoz.Add(de.Key, 0);
			}
			//prognoz.Add(rashods.First().Key.AddMinutes(-30), prevDataNBArray[4]);

			double currentNapor=naporArray.First().Value;
			SortedList<DateTime,double> dataForPrognoz=new SortedList<DateTime, double>();
			SortedList<DateTime,double> naporsForPrognoz=new SortedList<DateTime, double>();
			for (int indexPoint=0; indexPoint < pArr.Keys.Count; indexPoint++) {
				DateTime Key=pArr.Keys[indexPoint];
				dataForPrognoz.Add(Key, pArr[Key]);
				naporsForPrognoz.Add(Key, naporArray[Key]);
				if (dataForPrognoz.Count == 24) {
					SortedList<int,double> outputVector=new SortedList<int, double>();
					for (int step=0; step <= 3; step++) {
						SortedList<int, double> inputVector=new SortedList<int, double>();
						inputVector[0] = DatePrognozStart.Year;
						inputVector[1] = DatePrognozStart.DayOfYear;
						inputVector[2] = T;

						inputVector[3] = prevDataVBArray[1];
						inputVector[4] = prevDataVBArray[2];
						inputVector[5] = prevDataVBArray[3];
						inputVector[6] = prevDataVBArray[4];

						inputVector[7] = prevDataRashodArray[1];
						inputVector[8] = prevDataRashodArray[2];
						inputVector[9] = prevDataRashodArray[3];
						inputVector[10] = prevDataRashodArray[4];

						for (int i=0; i < 24; i++) {
							double rashod=0;
							if (!IsQFakt) {
								rashod = RashodTable.getStationRashod(pArr[dataForPrognoz.Keys[i]], naporsForPrognoz[dataForPrognoz.Keys[i]], RashodCalcMode.avg)*k;
							} else {
								rashod = rashods[dataForPrognoz.Keys[i]];
							}

							rashods[dataForPrognoz.Keys[i]] = rashod;
							inputVector[i + 11] = rashod;
						}

						inputVector[35] = prevDataNBArray[1];
						inputVector[36] = prevDataNBArray[2];
						inputVector[37] = prevDataNBArray[3];
						inputVector[38] = prevDataNBArray[4];

						outputVector = nnet.calc(inputVector);

						for (int i=0; i < outputVector.Count; i++) {
							prognoz[dataForPrognoz.Keys[i]] = outputVector[i];
						}

						for (int i=0; i < 24; i++) {
							naporsForPrognoz[dataForPrognoz.Keys[i]] = prevDataVBArray[4] - prognoz[dataForPrognoz.Keys[i]];
						}
					}

					for (int i=0; i <= 4; i++) {
						prevDataNBArray[i] = prognoz[dataForPrognoz.Keys[19 + i]];
						prevDataRashodArray[i] = rashods[dataForPrognoz.Keys[19 + i]];
					}

					dataForPrognoz.Clear();
				}
			}


			while (prognoz.Last().Key > DatePrognozEnd) {
				prognoz.Remove(prognoz.Last().Key);
			}
			while (rashods.Last().Key > DatePrognozEnd) {
				rashods.Remove(rashods.Last().Key);
			}
		}

		public void AddChartData(ChartData data) {
			ChartDataSerie prognozNBSerie=new ChartDataSerie();
			foreach (KeyValuePair<DateTime,double> de in Prognoz) {
				prognozNBSerie.Points.Add(new ChartDataPoint(de.Key,de.Value));
			}

			ChartDataSerie prognozQSerie=new ChartDataSerie();
			foreach (KeyValuePair<DateTime,double> de in Rashods) {
				prognozQSerie.Points.Add(new ChartDataPoint(de.Key, de.Value));
			}

			prognozNBSerie.Name = "NBPrognoz";
			prognozQSerie.Name = "QPrognoz";

			data.Series.Add(prognozNBSerie);
			data.Series.Add(prognozQSerie);
		}


		public ChartProperties createChartProperties() {
			ChartProperties props=new ChartProperties();

			ChartAxisProperties pAx=new ChartAxisProperties();
			pAx.Min = 0;
			pAx.Max = 1020;
			pAx.Auto = false;
			pAx.Interval = 100;
			pAx.Index = 1;

			ChartAxisProperties nbAx=new ChartAxisProperties();
			nbAx.Auto = true;
			nbAx.Interval = 0.1;
			nbAx.Index = 0;

			ChartAxisProperties qAx=new ChartAxisProperties();
			qAx.Auto = false;
			qAx.Min = 0;
			qAx.Max = 8000;
			qAx.Index = 2;

			ChartAxisProperties vbAx=new ChartAxisProperties();
			vbAx.Auto = true;
			vbAx.Index = 3;

			ChartAxisProperties naporAx=new ChartAxisProperties();
			naporAx.Auto = true;
			naporAx.Index = 4;

			ChartSerieProperties pSerie=new ChartSerieProperties();
			pSerie.Color = "0-0-255";
			pSerie.LineWidth = 2;
			pSerie.SerieType = ChartSerieType.line;
			pSerie.Title = "P факт";
			pSerie.TagName = "PFakt";
			pSerie.YAxisIndex = 1;

			ChartSerieProperties pbrSerie=new ChartSerieProperties();
			pbrSerie.Color = "0-0-255";
			pbrSerie.LineWidth = 1;
			pbrSerie.SerieType = ChartSerieType.line;
			pbrSerie.Title = "ПБР";
			pbrSerie.TagName = "PBR";
			pbrSerie.YAxisIndex = 1;

			ChartSerieProperties nbFaktSerie=new ChartSerieProperties();
			nbFaktSerie.Color = "255-0-0";
			nbFaktSerie.LineWidth = 2;
			nbFaktSerie.SerieType = ChartSerieType.line;
			nbFaktSerie.Title = "НБ факт";
			nbFaktSerie.TagName = "NBFakt";
			nbFaktSerie.YAxisIndex = 0;

			ChartSerieProperties nbPrognozSerie=new ChartSerieProperties();
			nbPrognozSerie.Color = "255-0-0";
			nbPrognozSerie.LineWidth = 1;
			nbPrognozSerie.SerieType = ChartSerieType.line;
			nbPrognozSerie.Title = "НБ прогноз";
			nbPrognozSerie.TagName = "NBPrognoz";
			nbPrognozSerie.YAxisIndex = 0;

			ChartSerieProperties qFaktSerie=new ChartSerieProperties();
			qFaktSerie.Color = "0-255-0";
			qFaktSerie.LineWidth = 2;
			qFaktSerie.SerieType = ChartSerieType.line;
			qFaktSerie.Title = "Q факт";
			qFaktSerie.TagName = "QFakt";
			qFaktSerie.YAxisIndex = 2;

			ChartSerieProperties qPrognozSerie=new ChartSerieProperties();
			qPrognozSerie.Color = "0-255-0";
			qPrognozSerie.LineWidth = 1;
			qPrognozSerie.SerieType = ChartSerieType.line;
			qPrognozSerie.Title = "Q прогноз";
			qPrognozSerie.TagName = "QPrognoz";
			qPrognozSerie.Enabled = false;
			qPrognozSerie.YAxisIndex = 2;

			ChartSerieProperties vbSerie=new ChartSerieProperties();
			vbSerie.Color = "0-255-255";
			vbSerie.LineWidth = 2;
			vbSerie.SerieType = ChartSerieType.line;
			vbSerie.Title = "ВБ";
			vbSerie.TagName = "VB";
			vbSerie.Enabled = false;
			vbSerie.YAxisIndex = 3;

			ChartSerieProperties naporSerie=new ChartSerieProperties();
			naporSerie.Color = "255-255-0";
			naporSerie.LineWidth = 2;
			naporSerie.SerieType = ChartSerieType.line;
			naporSerie.Title = "Напор";
			naporSerie.TagName = "Napor";
			naporSerie.Enabled = false;
			naporSerie.YAxisIndex = 4;
			

			props.Axes.Add(pAx);
			props.Axes.Add(nbAx);
			props.Axes.Add(qAx);
			props.Axes.Add(vbAx);
			props.Axes.Add(naporAx);

			props.Series.Add(pSerie);
			props.Series.Add(pbrSerie);
			props.Series.Add(nbFaktSerie);
			props.Series.Add(nbPrognozSerie);
			props.Series.Add(qFaktSerie);
			props.Series.Add(qPrognozSerie);
			props.Series.Add(vbSerie);
			props.Series.Add(naporSerie);

			props.XAxisType = XAxisTypeEnum.datetime;

			return props;
		}
	}
}
