using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace VotGES.Piramida.Report
{
	class ReportGlTransformRecords
	{
		public static RecordTypeCalc P_56AT_220=new RecordTypeCalc("P_56AT_220", "5-6АТ 220 P", null);
		public static RecordTypeCalc P_56AT_110=new RecordTypeCalc("P_56AT_110", "5-6АТ 110 P", null);
		public static RecordTypeCalc P_2AT_220=new RecordTypeCalc("P_2AT_220", "2АТ 220 P", null);
		public static RecordTypeCalc P_2AT_500=new RecordTypeCalc("P_2AT_500", "2АТ 500 P", null);
		public static RecordTypeCalc P_3AT_220=new RecordTypeCalc("P_3AT_220", "3АТ 220 P", null);
		public static RecordTypeCalc P_3AT_500=new RecordTypeCalc("P_3AT_500", "3АТ 500 P", null);
		public static RecordTypeCalc P_1T_110=new RecordTypeCalc("P_1T_110", "1Т 110 P", null);
		public static RecordTypeCalc P_4T_220=new RecordTypeCalc("P_4T_220", "4Т 220 P", null);

		public static RecordTypeCalc P_56AT_Nebalans=new RecordTypeCalc("P_56AT_Nebalans", "5-6АТ Небаланс P", null);
		public static RecordTypeCalc P_2AT_Nebalans=new RecordTypeCalc("P_2AT_Nebalans", "2АТ Небаланс P", null);
		public static RecordTypeCalc P_3AT_Nebalans=new RecordTypeCalc("P_3AT_Nebalans", "3АТ Небаланс P", null);
		public static RecordTypeCalc P_1T_Nebalans=new RecordTypeCalc("P_1T_Nebalans", "1Т Небаланс P", null);
		public static RecordTypeCalc P_4T_Nebalans=new RecordTypeCalc("P_4T_Nebalans", "4Т Небаланс P", null);
		public static RecordTypeCalc P_T_Nebalans=new RecordTypeCalc("P_T_Nebalans", "Трансформаторы Небаланс P", null);

		static ReportGlTransformRecords() {
			CreateGlTransformP();
		}

		public static void CreateGlTransformP() {
			P_1T_Nebalans.CalcFunction=
				new RecordCalcDelegate((report, date) => {
					return
						(report[date,PiramidaRecords.P_GA1_Otd.Key] + report[date,PiramidaRecords.P_GA2_Otd.Key])
						- (report[date,PiramidaRecords.P_GA1_Priem.Key] + report[date,PiramidaRecords.P_GA2_Priem.Key])
						- (report[date,PiramidaRecords.P_Vozb_GA1_Priem.Key] + report[date,PiramidaRecords.P_Vozb_GA1_Priem.Key])
						- (report[date,PiramidaRecords.P_SN_11T_Priem.Key] + report[date,PiramidaRecords.P_SN_12T_Priem.Key])
						- (report[date,PiramidaRecords.P_1T_110_Priem.Key] - report[date,PiramidaRecords.P_1T_110_Otd.Key]);
				});

			P_2AT_Nebalans.CalcFunction=
				new RecordCalcDelegate((report, date) => {
					return
						(report[date,PiramidaRecords.P_GA3_Otd.Key] + report[date,PiramidaRecords.P_GA4_Otd.Key])
						- (report[date,PiramidaRecords.P_GA3_Priem.Key] + report[date,PiramidaRecords.P_GA4_Priem.Key])
						- (report[date,PiramidaRecords.P_Vozb_GA3_Priem.Key] + report[date,PiramidaRecords.P_Vozb_GA4_Priem.Key])
						- (report[date,PiramidaRecords.P_SN_13T_Priem.Key] + report[date,PiramidaRecords.P_SN_14T_Priem.Key])
						- (report[date,PiramidaRecords.P_2AT_220_Priem.Key] - report[date,PiramidaRecords.P_2AT_220_Otd.Key])
						- (report[date,PiramidaRecords.P_2AT_500_Priem.Key] - report[date,PiramidaRecords.P_2AT_500_Otd.Key])
						- report[date,PiramidaRecords.P_SN_7T_Priem.Key];
				});

			P_3AT_Nebalans.CalcFunction=
				new RecordCalcDelegate((report, date) => {
					return
						(report[date,PiramidaRecords.P_GA5_Otd.Key] + report[date,PiramidaRecords.P_GA6_Otd.Key])
						- (report[date,PiramidaRecords.P_GA5_Priem.Key] + report[date,PiramidaRecords.P_GA6_Priem.Key])
						- (report[date,PiramidaRecords.P_Vozb_GA5_Priem.Key] + report[date,PiramidaRecords.P_Vozb_GA6_Priem.Key])
						- (report[date,PiramidaRecords.P_SN_15T_Priem.Key] + report[date,PiramidaRecords.P_SN_16T_Priem.Key])
						- (report[date,PiramidaRecords.P_3AT_220_Priem.Key] - report[date,PiramidaRecords.P_3AT_220_Otd.Key])
						- (report[date,PiramidaRecords.P_3AT_500_Priem.Key] - report[date,PiramidaRecords.P_3AT_500_Otd.Key])
						- report[date,PiramidaRecords.P_SN_8T_Priem.Key];
				});

			P_4T_Nebalans.CalcFunction=
				new RecordCalcDelegate((report, date) => {
					return
						(report[date,PiramidaRecords.P_GA7_Otd.Key] + report[date,PiramidaRecords.P_GA8_Otd.Key])
						- (report[date,PiramidaRecords.P_GA7_Priem.Key] + report[date,PiramidaRecords.P_GA8_Priem.Key])
						- (report[date,PiramidaRecords.P_Vozb_GA7_Priem.Key] + report[date,PiramidaRecords.P_Vozb_GA8_Priem.Key])
						- (report[date,PiramidaRecords.P_SN_17T_Priem.Key] + report[date,PiramidaRecords.P_SN_18T_Priem.Key])
						- (report[date,PiramidaRecords.P_4T_220_Priem.Key] - report[date,PiramidaRecords.P_4T_220_Otd.Key]);
				});

			P_56AT_Nebalans.CalcFunction=
				new RecordCalcDelegate((report, date) => {
					return
						(report[date,PiramidaRecords.P_GA9_Otd.Key] + report[date,PiramidaRecords.P_GA10_Otd.Key])
						- (report[date,PiramidaRecords.P_GA9_Priem.Key] + report[date,PiramidaRecords.P_GA10_Priem.Key])
						- (report[date,PiramidaRecords.P_Vozb_GA9_Priem.Key] + report[date,PiramidaRecords.P_Vozb_GA10_Priem.Key])
						- (report[date,PiramidaRecords.P_SN_19T_Priem.Key] + report[date,PiramidaRecords.P_SN_20T_Priem.Key])
						- (report[date,PiramidaRecords.P_56AT_220_Priem.Key] - report[date,PiramidaRecords.P_56AT_220_Otd.Key])
						- (report[date,PiramidaRecords.P_56AT_110_Priem.Key] - report[date,PiramidaRecords.P_56AT_110_Otd.Key]);
				});

			P_T_Nebalans.CalcFunction=
				new RecordCalcDelegate((report, date) => {
					return
						report[date, ReportGlTransformRecords.P_1T_Nebalans.ID] +
						report[date, ReportGlTransformRecords.P_2AT_Nebalans.ID] +
						report[date, ReportGlTransformRecords.P_3AT_Nebalans.ID] +
						report[date, ReportGlTransformRecords.P_4T_Nebalans.ID] +
						report[date, ReportGlTransformRecords.P_56AT_Nebalans.ID];						
				});

			P_56AT_220.CalcFunction=
				new RecordCalcDelegate((report, date) => {
					return (report[date,PiramidaRecords.P_56AT_220_Priem.Key] - report[date,PiramidaRecords.P_56AT_220_Otd.Key]);
				});

			P_56AT_110.CalcFunction=
				new RecordCalcDelegate((report, date) => {
					return (report[date,PiramidaRecords.P_56AT_110_Priem.Key] - report[date,PiramidaRecords.P_56AT_110_Otd.Key]);
				});

			P_2AT_220.CalcFunction=
				new RecordCalcDelegate((report, date) => {
					return (report[date,PiramidaRecords.P_2AT_220_Priem.Key] - report[date,PiramidaRecords.P_2AT_220_Otd.Key]);
				});

			P_2AT_500.CalcFunction=
				new RecordCalcDelegate((report, date) => {
					return (report[date,PiramidaRecords.P_2AT_500_Priem.Key] - report[date,PiramidaRecords.P_2AT_500_Otd.Key]);
				});

			P_3AT_220.CalcFunction=
				new RecordCalcDelegate((report, date) => {
					return (report[date,PiramidaRecords.P_3AT_220_Priem.Key] - report[date,PiramidaRecords.P_3AT_220_Otd.Key]);
				});

			P_3AT_500.CalcFunction=
				new RecordCalcDelegate((report, date) => {
					return (report[date,PiramidaRecords.P_3AT_500_Priem.Key] - report[date,PiramidaRecords.P_3AT_500_Otd.Key]);
				});


			P_1T_110.CalcFunction=
				new RecordCalcDelegate((report, date) => {
					return (report[date,PiramidaRecords.P_1T_110_Priem.Key] - report[date,PiramidaRecords.P_1T_110_Otd.Key]);
				});

			P_4T_220.CalcFunction=
				new RecordCalcDelegate((report, date) => {
					return (report[date,PiramidaRecords.P_4T_220_Priem.Key] - report[date,PiramidaRecords.P_4T_220_Otd.Key]);
				});
		}

		public static void AddCalcRecords(Report report, bool visible, bool toChart, ResultTypeEnum oper) {
			report.AddRecordType(new RecordTypeCalc(P_1T_110, toChart, visible, oper));
			report.AddRecordType(new RecordTypeCalc(P_1T_Nebalans, toChart, visible, oper));
			report.AddRecordType(new RecordTypeCalc(P_2AT_220, toChart, visible, oper));
			report.AddRecordType(new RecordTypeCalc(P_2AT_500, toChart, visible, oper));
			report.AddRecordType(new RecordTypeCalc(P_2AT_Nebalans, toChart, visible, oper));
			report.AddRecordType(new RecordTypeCalc(P_3AT_220, toChart, visible, oper));
			report.AddRecordType(new RecordTypeCalc(P_3AT_500, toChart, visible, oper));
			report.AddRecordType(new RecordTypeCalc(P_3AT_Nebalans, toChart, visible, oper));
			report.AddRecordType(new RecordTypeCalc(P_4T_220, toChart, visible, oper));
			report.AddRecordType(new RecordTypeCalc(P_4T_Nebalans, toChart, visible, oper));
			report.AddRecordType(new RecordTypeCalc(P_56AT_110, toChart, visible, oper));
			report.AddRecordType(new RecordTypeCalc(P_56AT_220, toChart, visible, oper));
			report.AddRecordType(new RecordTypeCalc(P_56AT_Nebalans, toChart, visible, oper));

			report.AddRecordType(new RecordTypeCalc(P_T_Nebalans, toChart, visible, oper));
		}


		public static void AddGLTransformRecordsP(Report report, int parNumber, double scaleMult, double scaleDiv, bool visible, bool toChart, DBOperEnum oper, ResultTypeEnum result) {
			report.AddRecordType(new RecordTypeDB(PiramidaRecords.P_2AT_220_Otd, parNumber, visible: visible, toChart: toChart, divParam: scaleDiv, multParam: scaleMult, resultType:result, dbOper:oper));
			report.AddRecordType(new RecordTypeDB(PiramidaRecords.P_2AT_220_Priem, parNumber, visible: visible, toChart: toChart, divParam: scaleDiv, multParam: scaleMult, resultType:result, dbOper:oper));
			report.AddRecordType(new RecordTypeDB(PiramidaRecords.P_2AT_500_Otd, parNumber, visible: visible, toChart: toChart, divParam: scaleDiv, multParam: scaleMult, resultType:result, dbOper:oper));
			report.AddRecordType(new RecordTypeDB(PiramidaRecords.P_2AT_500_Priem, parNumber, visible: visible, toChart: toChart, divParam: scaleDiv, multParam: scaleMult, resultType:result, dbOper:oper));

			report.AddRecordType(new RecordTypeDB(PiramidaRecords.P_3AT_220_Otd, parNumber, visible: visible, toChart: toChart, divParam: scaleDiv, multParam: scaleMult, resultType:result, dbOper:oper));
			report.AddRecordType(new RecordTypeDB(PiramidaRecords.P_3AT_220_Priem, parNumber, visible: visible, toChart: toChart, divParam: scaleDiv, multParam: scaleMult, resultType:result, dbOper:oper));
			report.AddRecordType(new RecordTypeDB(PiramidaRecords.P_3AT_500_Otd, parNumber, visible: visible, toChart: toChart, divParam: scaleDiv, multParam: scaleMult, resultType:result, dbOper:oper));
			report.AddRecordType(new RecordTypeDB(PiramidaRecords.P_3AT_500_Priem, parNumber, visible: visible, toChart: toChart, divParam: scaleDiv, multParam: scaleMult, resultType:result, dbOper:oper));

			report.AddRecordType(new RecordTypeDB(PiramidaRecords.P_56AT_110_Otd, parNumber, visible: visible, toChart: toChart, divParam: scaleDiv, multParam: scaleMult, resultType:result, dbOper:oper));
			report.AddRecordType(new RecordTypeDB(PiramidaRecords.P_56AT_110_Priem, parNumber, visible: visible, toChart: toChart, divParam: scaleDiv, multParam: scaleMult, resultType:result, dbOper:oper));
			report.AddRecordType(new RecordTypeDB(PiramidaRecords.P_56AT_220_Otd, parNumber, visible: visible, toChart: toChart, divParam: scaleDiv, multParam: scaleMult, resultType:result, dbOper:oper));
			report.AddRecordType(new RecordTypeDB(PiramidaRecords.P_56AT_220_Priem, parNumber, visible: visible, toChart: toChart, divParam: scaleDiv, multParam: scaleMult, resultType:result, dbOper:oper));

			report.AddRecordType(new RecordTypeDB(PiramidaRecords.P_4T_220_Otd, parNumber, visible: visible, toChart: toChart, divParam: scaleDiv, multParam: scaleMult, resultType:result, dbOper:oper));
			report.AddRecordType(new RecordTypeDB(PiramidaRecords.P_4T_220_Priem, parNumber, visible: visible, toChart: toChart, divParam: scaleDiv, multParam: scaleMult, resultType:result, dbOper:oper));
			report.AddRecordType(new RecordTypeDB(PiramidaRecords.P_1T_110_Otd, parNumber, visible: visible, toChart: toChart, divParam: scaleDiv, multParam: scaleMult, resultType:result, dbOper:oper));
			report.AddRecordType(new RecordTypeDB(PiramidaRecords.P_1T_110_Priem, parNumber, visible: visible, toChart: toChart, divParam: scaleDiv, multParam: scaleMult, resultType:result, dbOper:oper));
						
		}

		public static void AddPRecordsForNebalans(Report report, int parNumber, double scaleMult, double scaleDiv, bool visible, bool toChart, DBOperEnum oper, ResultTypeEnum result) {
			ReportGARecords.AddPRecordsGAAdd(report, parNumber, scaleMult, scaleDiv, visible, toChart, oper, result);
			ReportGARecords.AddPRecordsGAP(report, parNumber, scaleMult, scaleDiv, visible, toChart, oper, result);
			report.AddRecordType(new RecordTypeDB(PiramidaRecords.P_SN_7T_Priem, parNumber, visible: visible, toChart: toChart, divParam: scaleDiv, multParam: scaleMult, resultType:result, dbOper:oper));
			report.AddRecordType(new RecordTypeDB(PiramidaRecords.P_SN_8T_Priem, parNumber, visible: visible, toChart: toChart, divParam: scaleDiv, multParam: scaleMult, resultType:result, dbOper:oper));		
			
		}
	}
}
