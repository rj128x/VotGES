﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace VotGES.PrognozNB
{
	class PrognozNBFirstData
	{
		public DateTime Date { get; set; }
		public double Q { get; set; }
		public double NB { get; set; }
		public double VB { get; set; }
	}
	
	class PrognozNB
	{
		SortedList<DateTime,PrognozNBFirstData> FirstData;
		double T;

		protected static NNET.NNET nnet;

		static PrognozNB() {
			nnet = NNET.NNET.getNNET(NNET.NNETMODEL.vges_nb);
		}

		protected SortedList<DateTime, double> getPrognoz(DateTime datePrognozStart, SortedList<DateTime, double> pArr,
			SortedList<DateTime, double> prevPrognoz, out SortedList<DateTime, double> rashods, bool isQFakt = false) {
			
			SortedList<int,double>prevDataRashodArray=new SortedList<int, double>();
			SortedList<int,double>prevDataNBArray=new SortedList<int, double>();
			SortedList<int,double>prevDataVBArray=new SortedList<int, double>();
			SortedList<DateTime,double>prognoz=new SortedList<DateTime, double>();

			int index=0;
			foreach (DateTime date in FirstData.Keys) {
				prevDataRashodArray.Add(index, FirstData[date].Q);
				prevDataNBArray.Add(index, FirstData[date].NB);
				prevDataVBArray.Add(index, FirstData[date].VB);
				index++;
			}


			SortedList<DateTime,double>naporArray=new SortedList<DateTime, double>();
			rashods = new SortedList<DateTime, double>();


			if (prevPrognoz == null) {
				double napor=prevDataVBArray.Last().Value - prevDataNBArray.Last().Value;
				foreach (KeyValuePair<DateTime,double>de in pArr) {
					naporArray.Add(de.Key, napor);
				}
			} else {
				double vb=prevDataVBArray.Last().Value;
				foreach (KeyValuePair<DateTime,double>de in pArr) {
					naporArray.Add(de.Key, vb - prevPrognoz.First(pr => pr.Key >= de.Key).Value);
				}
			}

			foreach (KeyValuePair<DateTime,double> de in pArr) {
				double rashod=isQFakt ? de.Value : RashodTable.getStationRashod(de.Value, naporArray[de.Key],RashodCalcMode.avg);
				rashods.Add(de.Key, rashod);
				prognoz.Add(de.Key, 0);
			}
			prognoz.Add(rashods.First().Key.AddMinutes(-30), prevDataNBArray[4]);

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
						inputVector[0] = datePrognozStart.Year;
						inputVector[1] = datePrognozStart.DayOfYear;
						inputVector[2] = T;

						inputVector[3] = prevDataVBArray[0];
						inputVector[4] = prevDataVBArray[1];
						inputVector[5] = prevDataVBArray[2];
						inputVector[6] = prevDataVBArray[3];

						inputVector[7] = prevDataRashodArray[0];
						inputVector[8] = prevDataRashodArray[1];
						inputVector[9] = prevDataRashodArray[2];
						inputVector[10] = prevDataRashodArray[3];
						inputVector[11] = prevDataRashodArray[4];

						for (int i=0; i < 24; i++) {
							double rashod=0;
							if (!isQFakt) {
								rashod = RashodTable.getStationRashod(pArr[dataForPrognoz.Keys[i]], naporsForPrognoz[dataForPrognoz.Keys[i]], RashodCalcMode.avg);
							} else {
								rashod = rashods[dataForPrognoz.Keys[i]];
							}

							rashods[dataForPrognoz.Keys[i]] = rashod;
							inputVector[i + 12] = rashod;
						}

						inputVector[35] = prevDataNBArray[0];
						inputVector[36] = prevDataNBArray[1];
						inputVector[37] = prevDataNBArray[2];
						inputVector[38] = prevDataNBArray[3];

						outputVector = nnet.calc(inputVector);

						for (int i=0; i < outputVector.Count; i++) {
							prognoz[dataForPrognoz.Keys[i]] = outputVector[i];
						}

						for (int i=0; i < 24; i++) {
							naporsForPrognoz[dataForPrognoz.Keys[i]] = prevDataVBArray[4] - prognoz[dataForPrognoz.Keys[i]];
						}
					}

					for (int i=0; i <= 4; i++) {
						prevDataNBArray[i] = prognoz[dataForPrognoz.Keys[18 + i]];
						prevDataRashodArray[i] = rashods[dataForPrognoz.Keys[18 + i]];
					}

					dataForPrognoz.Clear();
				}
			}
			return prognoz;
		}
	}
}
