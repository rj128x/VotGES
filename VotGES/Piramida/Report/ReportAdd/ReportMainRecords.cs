using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace VotGES.Piramida.Report
{
	class ReportMainRecords
	{
		public static RecordTypeCalc P_Nebalans=new RecordTypeCalc("P_Nebalans", "Небаланс P", null);
		public static RecordTypeCalc P_SP=new RecordTypeCalc("P_SP", "Собственное потребление", null);

		static ReportMainRecords() {
			Create();
		}

		public static void Create() {
			P_SP.CalcFunction= new RecordCalcDelegate((report, date) => {
				return
					report[date, ReportSNRecords.P_SN.ID] +
					report[date, ReportGARecords.P_Vozb.ID] +
					report[date, ReportGARecords.P_SN_GA.ID] +
					report[date, ReportGlTransformRecords.P_T_Nebalans.ID] +
					report[date, ReportLinesRecords.P_VL_Nebalans.ID];					
			});

			P_Nebalans.CalcFunction=new RecordCalcDelegate((report, date) => {
				return
					report[date, ReportGlTransformRecords.P_T_Nebalans.ID] +
					report[date, ReportLinesRecords.P_VL_Nebalans.ID];
			});

		}

		

		public static void AddCalcRecords(Report report, bool visible, bool toChart, ResultTypeEnum oper) {
			report.AddRecordType(new RecordTypeCalc(P_SP, toChart, visible, oper));
			report.AddRecordType(new RecordTypeCalc(P_Nebalans, toChart, visible, oper));
		}

		public static void AddPRecords(Report report, int parNumber, double scaleMult, double scaleDiv, bool visible, bool toChart, DBOperEnum oper, ResultTypeEnum result) {

			report.AddRecordType(new RecordTypeDB(PiramidaRecords.P_GES, parNumber, visible: visible, toChart: toChart, divParam: scaleDiv, multParam: scaleMult, resultType:result, dbOper:oper));
			report.AddRecordType(new RecordTypeDB(PiramidaRecords.P_GTP1, parNumber, visible: visible, toChart: toChart, divParam: scaleDiv, multParam: scaleMult, resultType:result, dbOper:oper));
			report.AddRecordType(new RecordTypeDB(PiramidaRecords.P_GTP2, parNumber, visible: visible, toChart: toChart, divParam: scaleDiv, multParam: scaleMult, resultType:result, dbOper:oper));			
		}

	}
}
