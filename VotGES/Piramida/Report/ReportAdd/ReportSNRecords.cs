using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace VotGES.Piramida.Report
{
	class ReportSNRecords
	{
		public static RecordTypeCalc P_SN_1N=new RecordTypeCalc("P_SN_1N", "СН 1Н", null);
		public static RecordTypeCalc P_SN_2N=new RecordTypeCalc("P_SN_2N", "СН 2Н", null);
		public static RecordTypeCalc P_SN_3N=new RecordTypeCalc("P_SN_3N", "СН 3Н", null);
		public static RecordTypeCalc P_SN_7N=new RecordTypeCalc("P_SN_7N", "СН 7Н", null);
		public static RecordTypeCalc P_SN_8N=new RecordTypeCalc("P_SN_8N", "СН 8Н", null);
		public static RecordTypeCalc P_SN_9N=new RecordTypeCalc("P_SN_9N", "СН 9Н", null);
		public static RecordTypeCalc P_SN_10N=new RecordTypeCalc("P_SN_10N", "СН 10Н", null);
		public static RecordTypeCalc P_SN_36N=new RecordTypeCalc("P_SN_36N", "СН 36Н", null);
		public static RecordTypeCalc P_SN_Nasos=new RecordTypeCalc("P_SN_Nasos", "СН Насосы", null);
		public static RecordTypeCalc P_SN=new RecordTypeCalc("P_SN", "СН", null);

		static ReportSNRecords() {
			CreateSNP();
		}
		

		public static void CreateSNP() {
			P_SN_1N.CalcFunction=new RecordCalcDelegate((report, date) => {
				return report[date,PiramidaRecords.P_SN_21T_Priem.Key] + report[date,PiramidaRecords.P_SN_22T_Priem.Key];
			});

			P_SN_2N.CalcFunction=new RecordCalcDelegate((report, date) => {
				return report[date,PiramidaRecords.P_SN_23T_Priem.Key] + report[date,PiramidaRecords.P_SN_24T_Priem.Key];
			});

			P_SN_3N.CalcFunction=new RecordCalcDelegate((report, date) => {
				return report[date,PiramidaRecords.P_SN_27T_Priem.Key] + report[date,PiramidaRecords.P_SN_28T_Priem.Key];
			});

			P_SN_7N.CalcFunction=new RecordCalcDelegate((report, date) => {
				return report[date,PiramidaRecords.P_1KU_31T_Priem.Key] + report[date,PiramidaRecords.P_SN_32T_Priem.Key];
			});

			P_SN_8N.CalcFunction=new RecordCalcDelegate((report, date) => {
				return report[date,PiramidaRecords.P_SN_25T_37T_Priem.Key] + report[date,PiramidaRecords.P_SN_38T_Priem.Key];
			});

			P_SN_9N.CalcFunction=new RecordCalcDelegate((report, date) => {
				return report[date,PiramidaRecords.P_SN_29T_Priem.Key] + report[date,PiramidaRecords.P_SN_30T_31T_Priem.Key];
			});

			P_SN_10N.CalcFunction= new RecordCalcDelegate((report, date) => {
				return report[date,PiramidaRecords.P_SN_33T_Priem.Key] + report[date,PiramidaRecords.P_SN_34T_Priem.Key];
			});

			P_SN_36N.CalcFunction=new RecordCalcDelegate((report, date) => {
				return report[date,PiramidaRecords.P_SN_35T_Priem.Key] + report[date,PiramidaRecords.P_SN_36T_Priem.Key];
			});

			P_SN_Nasos.CalcFunction=new RecordCalcDelegate((report, date) => {
				return report[date,PiramidaRecords.P_SN_20NDS1_Priem.Key] + report[date,PiramidaRecords.P_SN_20NDS2_Priem.Key];
			});

			P_SN.CalcFunction=new RecordCalcDelegate((report, date) => {
				return
					report[date, ReportSNRecords.P_SN_10N.ID] +
					report[date, ReportSNRecords.P_SN_1N.ID] +
					report[date, ReportSNRecords.P_SN_2N.ID] +
					report[date, ReportSNRecords.P_SN_36N.ID] +
					report[date, ReportSNRecords.P_SN_3N.ID] +
					report[date, ReportSNRecords.P_SN_7N.ID] +
					report[date, ReportSNRecords.P_SN_8N.ID] +
					report[date, ReportSNRecords.P_SN_9N.ID] +
					report[date, ReportSNRecords.P_SN_Nasos.ID];
			});

			
		}

		public static void AddCalcRecords(Report report, bool visible, bool toChart, ResultTypeEnum oper) {
			report.AddRecordType(new RecordTypeCalc(P_SN_10N, toChart, visible,oper));
			report.AddRecordType(new RecordTypeCalc(P_SN_1N, toChart, visible, oper));
			report.AddRecordType(new RecordTypeCalc(P_SN_2N, toChart, visible, oper));
			report.AddRecordType(new RecordTypeCalc(P_SN_36N, toChart, visible, oper));
			report.AddRecordType(new RecordTypeCalc(P_SN_3N, toChart, visible, oper));
			report.AddRecordType(new RecordTypeCalc(P_SN_7N, toChart, visible, oper));
			report.AddRecordType(new RecordTypeCalc(P_SN_8N, toChart, visible, oper));
			report.AddRecordType(new RecordTypeCalc(P_SN_9N, toChart, visible, oper));
			report.AddRecordType(new RecordTypeCalc(P_SN_Nasos, toChart, visible, oper));

			report.AddRecordType(new RecordTypeCalc(P_SN, toChart, visible, oper));
		}
		

		public static void AddPRecordsSN(Report report, int parNumber, double scaleMult, double scaleDiv, bool visible, bool toChart, DBOperEnum oper, ResultTypeEnum result) {
			report.AddRecordType(new RecordTypeDB(PiramidaRecords.P_SN_20NDS1_Priem, parNumber, visible: visible, toChart: toChart, divParam: scaleDiv, multParam: scaleMult, resultType:result, dbOper:oper));
			report.AddRecordType(new RecordTypeDB(PiramidaRecords.P_SN_20NDS2_Priem, parNumber, visible: visible, toChart: toChart, divParam: scaleDiv, multParam: scaleMult, resultType:result, dbOper:oper));

			report.AddRecordType(new RecordTypeDB(PiramidaRecords.P_SN_21T_Priem, parNumber, visible: visible, toChart: toChart, divParam: scaleDiv, multParam: scaleMult, resultType:result, dbOper:oper));
			report.AddRecordType(new RecordTypeDB(PiramidaRecords.P_SN_21T_Priem, parNumber, visible: visible, toChart: toChart, divParam: scaleDiv, multParam: scaleMult, resultType:result, dbOper:oper));

			report.AddRecordType(new RecordTypeDB(PiramidaRecords.P_SN_21T_Priem, parNumber, visible: visible, toChart: toChart, divParam: scaleDiv, multParam: scaleMult, resultType:result, dbOper:oper));
			report.AddRecordType(new RecordTypeDB(PiramidaRecords.P_SN_22T_Priem, parNumber, visible: visible, toChart: toChart, divParam: scaleDiv, multParam: scaleMult, resultType:result, dbOper:oper));
			report.AddRecordType(new RecordTypeDB(PiramidaRecords.P_SN_23T_Priem, parNumber, visible: visible, toChart: toChart, divParam: scaleDiv, multParam: scaleMult, resultType:result, dbOper:oper));
			report.AddRecordType(new RecordTypeDB(PiramidaRecords.P_SN_24T_Priem, parNumber, visible: visible, toChart: toChart, divParam: scaleDiv, multParam: scaleMult, resultType:result, dbOper:oper));
			report.AddRecordType(new RecordTypeDB(PiramidaRecords.P_SN_25T_37T_Priem, parNumber, visible: visible, toChart: toChart, divParam: scaleDiv, multParam: scaleMult, resultType:result, dbOper:oper));
			report.AddRecordType(new RecordTypeDB(PiramidaRecords.P_SN_27T_Priem, parNumber, visible: visible, toChart: toChart, divParam: scaleDiv, multParam: scaleMult, resultType:result, dbOper:oper));
			report.AddRecordType(new RecordTypeDB(PiramidaRecords.P_SN_28T_Priem, parNumber, visible: visible, toChart: toChart, divParam: scaleDiv, multParam: scaleMult, resultType:result, dbOper:oper));
			report.AddRecordType(new RecordTypeDB(PiramidaRecords.P_SN_29T_Priem, parNumber, visible: visible, toChart: toChart, divParam: scaleDiv, multParam: scaleMult, resultType:result, dbOper:oper));
			report.AddRecordType(new RecordTypeDB(PiramidaRecords.P_SN_30T_31T_Priem, parNumber, visible: visible, toChart: toChart, divParam: scaleDiv, multParam: scaleMult, resultType:result, dbOper:oper));
			report.AddRecordType(new RecordTypeDB(PiramidaRecords.P_SN_32T_Priem, parNumber, visible: visible, toChart: toChart, divParam: scaleDiv, multParam: scaleMult, resultType:result, dbOper:oper));
			report.AddRecordType(new RecordTypeDB(PiramidaRecords.P_SN_33T_Priem, parNumber, visible: visible, toChart: toChart, divParam: scaleDiv, multParam: scaleMult, resultType:result, dbOper:oper));
			report.AddRecordType(new RecordTypeDB(PiramidaRecords.P_SN_34T_Priem, parNumber, visible: visible, toChart: toChart, divParam: scaleDiv, multParam: scaleMult, resultType:result, dbOper:oper));
			report.AddRecordType(new RecordTypeDB(PiramidaRecords.P_SN_35T_Priem, parNumber, visible: visible, toChart: toChart, divParam: scaleDiv, multParam: scaleMult, resultType:result, dbOper:oper));
			report.AddRecordType(new RecordTypeDB(PiramidaRecords.P_SN_36T_Priem, parNumber, visible: visible, toChart: toChart, divParam: scaleDiv, multParam: scaleMult, resultType:result, dbOper:oper));
			report.AddRecordType(new RecordTypeDB(PiramidaRecords.P_SN_38T_Priem, parNumber, visible: visible, toChart: toChart, divParam: scaleDiv, multParam: scaleMult, resultType:result, dbOper:oper));
			report.AddRecordType(new RecordTypeDB(PiramidaRecords.P_SN_TVI_Priem, parNumber, visible: visible, toChart: toChart, divParam: scaleDiv, multParam: scaleMult, resultType:result, dbOper:oper));
		}


	}
}
