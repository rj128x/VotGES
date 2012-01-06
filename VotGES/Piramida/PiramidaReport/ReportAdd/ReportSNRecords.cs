using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace VotGES.Piramida.PiramidaReport
{
	class ReportSNRecords
	{
		public static RecordTypeCalc P_SN_1N;
		public static RecordTypeCalc P_SN_2N;
		public static RecordTypeCalc P_SN_3N;
		public static RecordTypeCalc P_SN_7N;
		public static RecordTypeCalc P_SN_8N;
		public static RecordTypeCalc P_SN_9N;
		public static RecordTypeCalc P_SN_10N;
		public static RecordTypeCalc P_SN_36N;
		public static RecordTypeCalc P_SN_Nasos;

		public static void CreateSNP() {
			P_SN_1N = new RecordTypeCalc("P_SN_1N", "СН 1Н", new RecordCalcDelegate((report, date) => {
				return report[date,PiramidaRecords.P_SN_21T_Priem.Key] + report[date,PiramidaRecords.P_SN_22T_Priem.Key];
			}));

			P_SN_2N = new RecordTypeCalc("P_SN_2N", "СН 2Н", new RecordCalcDelegate((report, date) => {
				return report[date,PiramidaRecords.P_SN_23T_Priem.Key] + report[date,PiramidaRecords.P_SN_24T_Priem.Key];
			}));

			P_SN_3N = new RecordTypeCalc("P_SN_3N", "СН 3Н", new RecordCalcDelegate((report, date) => {
				return report[date,PiramidaRecords.P_SN_27T_Priem.Key] + report[date,PiramidaRecords.P_SN_28T_Priem.Key];
			}));

			P_SN_7N = new RecordTypeCalc("P_SN_7N", "СН 7Н", new RecordCalcDelegate((report, date) => {
				return report[date,PiramidaRecords.P_1KU_31T_Priem.Key] + report[date,PiramidaRecords.P_SN_32T_Priem.Key];
			}));

			P_SN_8N = new RecordTypeCalc("P_SN_8N", "СН 8Н", new RecordCalcDelegate((report, date) => {
				return report[date,PiramidaRecords.P_SN_25T_37T_Priem.Key] + report[date,PiramidaRecords.P_SN_38T_Priem.Key];
			}));

			P_SN_9N = new RecordTypeCalc("P_SN_9N", "СН 9Н", new RecordCalcDelegate((report, date) => {
				return report[date,PiramidaRecords.P_SN_29T_Priem.Key] + report[date,PiramidaRecords.P_SN_30T_31T_Priem.Key];
			}));

			P_SN_10N = new RecordTypeCalc("P_SN_10N", "СН 10Н", new RecordCalcDelegate((report, date) => {
				return report[date,PiramidaRecords.P_SN_33T_Priem.Key] + report[date,PiramidaRecords.P_SN_34T_Priem.Key];
			}));

			P_SN_36N = new RecordTypeCalc("P_SN_36N", "СН 36Н", new RecordCalcDelegate((report, date) => {
				return report[date,PiramidaRecords.P_SN_35T_Priem.Key] + report[date,PiramidaRecords.P_SN_36T_Priem.Key];
			}));

			P_SN_Nasos = new RecordTypeCalc("P_SN_Nasos", "СН Насосы", new RecordCalcDelegate((report, date) => {
				return report[date,PiramidaRecords.P_SN_20NDS1_Priem.Key] + report[date,PiramidaRecords.P_SN_20NDS2_Priem.Key];
			}));
		}

		

		public static void AddPRecordsSN(Report report, int parNumber, double scaleMult, double scaleDiv, bool visible, bool toChart) {
			report.AddRecordType(new RecordTypeDB(PiramidaRecords.P_SN_20NDS1_Priem, parNumber, visible: visible, toChart: toChart, divParam: scaleDiv, multParam: scaleMult));
			report.AddRecordType(new RecordTypeDB(PiramidaRecords.P_SN_20NDS2_Priem, parNumber, visible: visible, toChart: toChart, divParam: scaleDiv, multParam: scaleMult));

			report.AddRecordType(new RecordTypeDB(PiramidaRecords.P_SN_21T_Priem, parNumber, visible: visible, toChart: toChart, divParam: scaleDiv, multParam: scaleMult));
			report.AddRecordType(new RecordTypeDB(PiramidaRecords.P_SN_21T_Priem, parNumber, visible: visible, toChart: toChart, divParam: scaleDiv, multParam: scaleMult));

			report.AddRecordType(new RecordTypeDB(PiramidaRecords.P_SN_21T_Priem, parNumber, visible: visible, toChart: toChart, divParam: scaleDiv, multParam: scaleMult));
			report.AddRecordType(new RecordTypeDB(PiramidaRecords.P_SN_22T_Priem, parNumber, visible: visible, toChart: toChart, divParam: scaleDiv, multParam: scaleMult));
			report.AddRecordType(new RecordTypeDB(PiramidaRecords.P_SN_23T_Priem, parNumber, visible: visible, toChart: toChart, divParam: scaleDiv, multParam: scaleMult));
			report.AddRecordType(new RecordTypeDB(PiramidaRecords.P_SN_24T_Priem, parNumber, visible: visible, toChart: toChart, divParam: scaleDiv, multParam: scaleMult));
			report.AddRecordType(new RecordTypeDB(PiramidaRecords.P_SN_25T_37T_Priem, parNumber, visible: visible, toChart: toChart, divParam: scaleDiv, multParam: scaleMult));
			report.AddRecordType(new RecordTypeDB(PiramidaRecords.P_SN_27T_Priem, parNumber, visible: visible, toChart: toChart, divParam: scaleDiv, multParam: scaleMult));
			report.AddRecordType(new RecordTypeDB(PiramidaRecords.P_SN_28T_Priem, parNumber, visible: visible, toChart: toChart, divParam: scaleDiv, multParam: scaleMult));
			report.AddRecordType(new RecordTypeDB(PiramidaRecords.P_SN_29T_Priem, parNumber, visible: visible, toChart: toChart, divParam: scaleDiv, multParam: scaleMult));
			report.AddRecordType(new RecordTypeDB(PiramidaRecords.P_SN_30T_31T_Priem, parNumber, visible: visible, toChart: toChart, divParam: scaleDiv, multParam: scaleMult));
			report.AddRecordType(new RecordTypeDB(PiramidaRecords.P_SN_32T_Priem, parNumber, visible: visible, toChart: toChart, divParam: scaleDiv, multParam: scaleMult));
			report.AddRecordType(new RecordTypeDB(PiramidaRecords.P_SN_33T_Priem, parNumber, visible: visible, toChart: toChart, divParam: scaleDiv, multParam: scaleMult));
			report.AddRecordType(new RecordTypeDB(PiramidaRecords.P_SN_34T_Priem, parNumber, visible: visible, toChart: toChart, divParam: scaleDiv, multParam: scaleMult));
			report.AddRecordType(new RecordTypeDB(PiramidaRecords.P_SN_35T_Priem, parNumber, visible: visible, toChart: toChart, divParam: scaleDiv, multParam: scaleMult));
			report.AddRecordType(new RecordTypeDB(PiramidaRecords.P_SN_36T_Priem, parNumber, visible: visible, toChart: toChart, divParam: scaleDiv, multParam: scaleMult));
			report.AddRecordType(new RecordTypeDB(PiramidaRecords.P_SN_38T_Priem, parNumber, visible: visible, toChart: toChart, divParam: scaleDiv, multParam: scaleMult));
			report.AddRecordType(new RecordTypeDB(PiramidaRecords.P_SN_TVI_Priem, parNumber, visible: visible, toChart: toChart, divParam: scaleDiv, multParam: scaleMult));
		}


	}
}
