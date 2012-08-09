using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace VotGES.Piramida.Report
{
	class ReportGARecords
	{
		public static RecordTypeCalc P_GA1=new RecordTypeCalc("P_GA1", "ГА-1 P", null);
		public static RecordTypeCalc P_GA2=new RecordTypeCalc("P_GA2", "ГА-2 P", null);
		public static RecordTypeCalc P_GA3=new RecordTypeCalc("P_GA3", "ГА-3 P", null);
		public static RecordTypeCalc P_GA4=new RecordTypeCalc("P_GA4", "ГА-4 P", null);
		public static RecordTypeCalc P_GA5=new RecordTypeCalc("P_GA5", "ГА-5 P", null);
		public static RecordTypeCalc P_GA6=new RecordTypeCalc("P_GA6", "ГА-6 P", null);
		public static RecordTypeCalc P_GA7=new RecordTypeCalc("P_GA7", "ГА-7 P", null);
		public static RecordTypeCalc P_GA8=new RecordTypeCalc("P_GA8", "ГА-8 P", null);
		public static RecordTypeCalc P_GA9=new RecordTypeCalc("P_GA9", "ГА-9 P", null);
		public static RecordTypeCalc P_GA10=new RecordTypeCalc("P_GA10", "ГА-10 P", null);

		public static RecordTypeCalc Q_GA1=new RecordTypeCalc("Q_GA1", "ГА-1 Q", null);
		public static RecordTypeCalc Q_GA2=new RecordTypeCalc("Q_GA2", "ГА-2 Q", null);
		public static RecordTypeCalc Q_GA3=new RecordTypeCalc("Q_GA3", "ГА-3 Q", null);
		public static RecordTypeCalc Q_GA4=new RecordTypeCalc("Q_GA4", "ГА-4 Q", null);
		public static RecordTypeCalc Q_GA5=new RecordTypeCalc("Q_GA5", "ГА-5 Q", null);
		public static RecordTypeCalc Q_GA6=new RecordTypeCalc("Q_GA6", "ГА-6 Q", null);
		public static RecordTypeCalc Q_GA7=new RecordTypeCalc("Q_GA7", "ГА-7 Q", null);
		public static RecordTypeCalc Q_GA8=new RecordTypeCalc("Q_GA8", "ГА-8 Q", null);
		public static RecordTypeCalc Q_GA9=new RecordTypeCalc("Q_GA9", "ГА-9 Q", null);
		public static RecordTypeCalc Q_GA10=new RecordTypeCalc("Q_GA10", "ГА-10 Q", null);

		public static RecordTypeCalc P_SN_GA=new RecordTypeCalc("P_SN_GA", "СН генераторов", null);
		public static RecordTypeCalc P_Vozb=new RecordTypeCalc("P_Vozb", "Возбуждение", null);
		public static RecordTypeCalc P_SK=new RecordTypeCalc("P_SK", "СК", null);

		static ReportGARecords() {
			CreateGAP();
			CreateGAQ();
		}

		public static void CreateGAP() {
			P_GA1.CalcFunction=
				new RecordCalcDelegate((report, date) => {
					return (report[date,PiramidaRecords.P_GA1_Otd.Key] - report[date,PiramidaRecords.P_GA1_Priem.Key]);
				});

			P_GA2.CalcFunction=
				new RecordCalcDelegate((report, date) => {
					return (report[date,PiramidaRecords.P_GA2_Otd.Key] - report[date,PiramidaRecords.P_GA2_Priem.Key]);
				});

			P_GA3 = new RecordTypeCalc("P_GA3", "ГА-3 P",
				new RecordCalcDelegate((report, date) => {
					return (report[date,PiramidaRecords.P_GA3_Otd.Key] - report[date,PiramidaRecords.P_GA3_Priem.Key]);
				}));

			P_GA4.CalcFunction=
				new RecordCalcDelegate((report, date) => {
					return (report[date,PiramidaRecords.P_GA4_Otd.Key] - report[date,PiramidaRecords.P_GA4_Priem.Key]);
				});

			P_GA5.CalcFunction=
				new RecordCalcDelegate((report, date) => {
					return (report[date,PiramidaRecords.P_GA5_Otd.Key] - report[date,PiramidaRecords.P_GA5_Priem.Key]);
				});

			P_GA6.CalcFunction=
				new RecordCalcDelegate((report, date) => {
					return (report[date,PiramidaRecords.P_GA6_Otd.Key] - report[date,PiramidaRecords.P_GA6_Priem.Key]);
				});

			P_GA7.CalcFunction=
				new RecordCalcDelegate((report, date) => {
					return (report[date,PiramidaRecords.P_GA7_Otd.Key] - report[date,PiramidaRecords.P_GA7_Priem.Key]);
				});

			P_GA8.CalcFunction=
				new RecordCalcDelegate((report, date) => {
					return (report[date,PiramidaRecords.P_GA8_Otd.Key] - report[date,PiramidaRecords.P_GA8_Priem.Key]);
				});

			P_GA9.CalcFunction=
				new RecordCalcDelegate((report, date) => {
					return (report[date,PiramidaRecords.P_GA9_Otd.Key] - report[date,PiramidaRecords.P_GA9_Priem.Key]);
				});

			P_GA10.CalcFunction=
				new RecordCalcDelegate((report, date) => {
					return (report[date,PiramidaRecords.P_GA10_Otd.Key] - report[date,PiramidaRecords.P_GA10_Priem.Key]);
				});

			P_Vozb.CalcFunction=
				new RecordCalcDelegate((report, date) => {
					return
						report[date, PiramidaRecords.P_Vozb_GA1_Priem.Key] +
						report[date, PiramidaRecords.P_Vozb_GA2_Priem.Key] +
						report[date, PiramidaRecords.P_Vozb_GA3_Priem.Key] +
						report[date, PiramidaRecords.P_Vozb_GA4_Priem.Key] +
						report[date, PiramidaRecords.P_Vozb_GA5_Priem.Key] +
						report[date, PiramidaRecords.P_Vozb_GA6_Priem.Key] +
						report[date, PiramidaRecords.P_Vozb_GA7_Priem.Key] +
						report[date, PiramidaRecords.P_Vozb_GA8_Priem.Key] +
						report[date, PiramidaRecords.P_Vozb_GA9_Priem.Key] +
						report[date, PiramidaRecords.P_Vozb_GA10_Priem.Key];
				});

			P_SN_GA.CalcFunction=
				new RecordCalcDelegate((report, date) => {
					return
						report[date, PiramidaRecords.P_SN_11T_Priem.Key] +
						report[date, PiramidaRecords.P_SN_12T_Priem.Key] +
						report[date, PiramidaRecords.P_SN_13T_Priem.Key] +
						report[date, PiramidaRecords.P_SN_14T_Priem.Key] +
						report[date, PiramidaRecords.P_SN_15T_Priem.Key] +
						report[date, PiramidaRecords.P_SN_16T_Priem.Key] +
						report[date, PiramidaRecords.P_SN_17T_Priem.Key] +
						report[date, PiramidaRecords.P_SN_18T_Priem.Key] +
						report[date, PiramidaRecords.P_SN_19T_Priem.Key] +
						report[date, PiramidaRecords.P_SN_20T_Priem.Key];
				});

			P_SK.CalcFunction=
				new RecordCalcDelegate((report, date) => {
					return
						report[date, PiramidaRecords.P_GA1_Priem.Key] +
						report[date, PiramidaRecords.P_GA2_Priem.Key] +
						report[date, PiramidaRecords.P_GA9_Priem.Key] +
						report[date, PiramidaRecords.P_GA10_Priem.Key];
				});
		}

		public static void CreateGAQ() {
			Q_GA1.CalcFunction=
				new RecordCalcDelegate((report, date) => {
					return (report[date,PiramidaRecords.Q_GA1_Otd.Key] - report[date,PiramidaRecords.Q_GA1_Priem.Key]);
				});

			Q_GA2.CalcFunction=
				new RecordCalcDelegate((report, date) => {
					return (report[date,PiramidaRecords.Q_GA2_Otd.Key] - report[date,PiramidaRecords.Q_GA2_Priem.Key]);
				});

			Q_GA3.CalcFunction=
				new RecordCalcDelegate((report, date) => {
					return (report[date,PiramidaRecords.Q_GA3_Otd.Key] - report[date,PiramidaRecords.Q_GA3_Priem.Key]);
				});

			Q_GA4.CalcFunction=
				new RecordCalcDelegate((report, date) => {
					return (report[date,PiramidaRecords.Q_GA4_Otd.Key] - report[date,PiramidaRecords.Q_GA4_Priem.Key]);
				});

			Q_GA5.CalcFunction=
				new RecordCalcDelegate((report, date) => {
					return (report[date,PiramidaRecords.Q_GA5_Otd.Key] - report[date,PiramidaRecords.Q_GA5_Priem.Key]);
				});

			Q_GA6.CalcFunction=
				new RecordCalcDelegate((report, date) => {
					return (report[date,PiramidaRecords.Q_GA6_Otd.Key] - report[date,PiramidaRecords.Q_GA6_Priem.Key]);
				});

			Q_GA7.CalcFunction=
				new RecordCalcDelegate((report, date) => {
					return (report[date,PiramidaRecords.Q_GA7_Otd.Key] - report[date,PiramidaRecords.Q_GA7_Priem.Key]);
				});

			Q_GA8.CalcFunction=
				new RecordCalcDelegate((report, date) => {
					return (report[date,PiramidaRecords.Q_GA8_Otd.Key] - report[date,PiramidaRecords.Q_GA8_Priem.Key]);
				});

			Q_GA9.CalcFunction=
				new RecordCalcDelegate((report, date) => {
					return (report[date,PiramidaRecords.Q_GA9_Otd.Key] - report[date,PiramidaRecords.Q_GA9_Priem.Key]);
				});

			Q_GA10.CalcFunction=
				new RecordCalcDelegate((report, date) => {
					return (report[date,PiramidaRecords.Q_GA10_Otd.Key] - report[date,PiramidaRecords.Q_GA10_Priem.Key]);
				});
		}

		public static void AddCalcRecords(Report report, bool visible, bool toChart, ResultTypeEnum oper) {
			report.AddRecordType(new RecordTypeCalc(P_GA1, toChart, visible, oper));
			report.AddRecordType(new RecordTypeCalc(P_GA2, toChart, visible, oper));
			report.AddRecordType(new RecordTypeCalc(P_GA3, toChart, visible, oper));
			report.AddRecordType(new RecordTypeCalc(P_GA4, toChart, visible, oper));
			report.AddRecordType(new RecordTypeCalc(P_GA5, toChart, visible, oper));
			report.AddRecordType(new RecordTypeCalc(P_GA6, toChart, visible, oper));
			report.AddRecordType(new RecordTypeCalc(P_GA7, toChart, visible, oper));
			report.AddRecordType(new RecordTypeCalc(P_GA8, toChart, visible, oper));
			report.AddRecordType(new RecordTypeCalc(P_GA9, toChart, visible, oper));
			report.AddRecordType(new RecordTypeCalc(P_GA10, toChart, visible, oper));

			report.AddRecordType(new RecordTypeCalc(P_SK, toChart, visible, oper));
			report.AddRecordType(new RecordTypeCalc(P_SN_GA, toChart, visible, oper));
			report.AddRecordType(new RecordTypeCalc(P_Vozb, toChart, visible, oper));


			report.AddRecordType(new RecordTypeCalc(Q_GA1, toChart, visible, oper));
			report.AddRecordType(new RecordTypeCalc(Q_GA2, toChart, visible, oper));
			report.AddRecordType(new RecordTypeCalc(Q_GA3, toChart, visible, oper));
			report.AddRecordType(new RecordTypeCalc(Q_GA4, toChart, visible, oper));
			report.AddRecordType(new RecordTypeCalc(Q_GA5, toChart, visible, oper));
			report.AddRecordType(new RecordTypeCalc(Q_GA6, toChart, visible, oper));
			report.AddRecordType(new RecordTypeCalc(Q_GA7, toChart, visible, oper));
			report.AddRecordType(new RecordTypeCalc(Q_GA8, toChart, visible, oper));
			report.AddRecordType(new RecordTypeCalc(Q_GA9, toChart, visible, oper));
			report.AddRecordType(new RecordTypeCalc(Q_GA10, toChart, visible, oper));
		}

		public static void AddPRecordsGAP(Report report, int parNumber, double scaleMult, double scaleDiv, bool visible, bool toChart, DBOperEnum oper, ResultTypeEnum result) {

			report.AddRecordType(new RecordTypeDB(PiramidaRecords.P_GA1_Otd, parNumber, visible: visible, toChart: toChart, divParam: scaleDiv, multParam: scaleMult, resultType:result, dbOper:oper));
			report.AddRecordType(new RecordTypeDB(PiramidaRecords.P_GA2_Otd, parNumber, visible: visible, toChart: toChart, divParam: scaleDiv, multParam: scaleMult, resultType:result, dbOper:oper));
			report.AddRecordType(new RecordTypeDB(PiramidaRecords.P_GA3_Otd, parNumber, visible: visible, toChart: toChart, divParam: scaleDiv, multParam: scaleMult, resultType:result, dbOper:oper));
			report.AddRecordType(new RecordTypeDB(PiramidaRecords.P_GA4_Otd, parNumber, visible: visible, toChart: toChart, divParam: scaleDiv, multParam: scaleMult, resultType:result, dbOper:oper));
			report.AddRecordType(new RecordTypeDB(PiramidaRecords.P_GA5_Otd, parNumber, visible: visible, toChart: toChart, divParam: scaleDiv, multParam: scaleMult, resultType:result, dbOper:oper));
			report.AddRecordType(new RecordTypeDB(PiramidaRecords.P_GA6_Otd, parNumber, visible: visible, toChart: toChart, divParam: scaleDiv, multParam: scaleMult, resultType:result, dbOper:oper));
			report.AddRecordType(new RecordTypeDB(PiramidaRecords.P_GA7_Otd, parNumber, visible: visible, toChart: toChart, divParam: scaleDiv, multParam: scaleMult, resultType:result, dbOper:oper));
			report.AddRecordType(new RecordTypeDB(PiramidaRecords.P_GA8_Otd, parNumber, visible: visible, toChart: toChart, divParam: scaleDiv, multParam: scaleMult, resultType:result, dbOper:oper));
			report.AddRecordType(new RecordTypeDB(PiramidaRecords.P_GA9_Otd, parNumber, visible: visible, toChart: toChart, divParam: scaleDiv, multParam: scaleMult, resultType:result, dbOper:oper));
			report.AddRecordType(new RecordTypeDB(PiramidaRecords.P_GA10_Otd, parNumber, visible: visible, toChart: toChart, divParam: scaleDiv, multParam: scaleMult, resultType:result, dbOper:oper));


			report.AddRecordType(new RecordTypeDB(PiramidaRecords.P_GA1_Priem, parNumber, visible: visible, toChart: toChart, divParam: scaleDiv, multParam: scaleMult, resultType:result, dbOper:oper));
			report.AddRecordType(new RecordTypeDB(PiramidaRecords.P_GA2_Priem, parNumber, visible: visible, toChart: toChart, divParam: scaleDiv, multParam: scaleMult, resultType:result, dbOper:oper));
			report.AddRecordType(new RecordTypeDB(PiramidaRecords.P_GA3_Priem, parNumber, visible: visible, toChart: toChart, divParam: scaleDiv, multParam: scaleMult, resultType:result, dbOper:oper));
			report.AddRecordType(new RecordTypeDB(PiramidaRecords.P_GA4_Priem, parNumber, visible: visible, toChart: toChart, divParam: scaleDiv, multParam: scaleMult, resultType:result, dbOper:oper));
			report.AddRecordType(new RecordTypeDB(PiramidaRecords.P_GA5_Priem, parNumber, visible: visible, toChart: toChart, divParam: scaleDiv, multParam: scaleMult, resultType:result, dbOper:oper));
			report.AddRecordType(new RecordTypeDB(PiramidaRecords.P_GA6_Priem, parNumber, visible: visible, toChart: toChart, divParam: scaleDiv, multParam: scaleMult, resultType:result, dbOper:oper));
			report.AddRecordType(new RecordTypeDB(PiramidaRecords.P_GA7_Priem, parNumber, visible: visible, toChart: toChart, divParam: scaleDiv, multParam: scaleMult, resultType:result, dbOper:oper));
			report.AddRecordType(new RecordTypeDB(PiramidaRecords.P_GA8_Priem, parNumber, visible: visible, toChart: toChart, divParam: scaleDiv, multParam: scaleMult, resultType:result, dbOper:oper));
			report.AddRecordType(new RecordTypeDB(PiramidaRecords.P_GA9_Priem, parNumber, visible: visible, toChart: toChart, divParam: scaleDiv, multParam: scaleMult, resultType:result, dbOper:oper));
			report.AddRecordType(new RecordTypeDB(PiramidaRecords.P_GA10_Priem, parNumber, visible: visible, toChart: toChart, divParam: scaleDiv, multParam: scaleMult, resultType:result, dbOper:oper));
		}

		public static void AddPRecordsGAAdd(Report report, int parNumber, double scaleMult, double scaleDiv, bool visible, bool toChart, DBOperEnum oper, ResultTypeEnum result) {
			report.AddRecordType(new RecordTypeDB(PiramidaRecords.P_Vozb_GA1_Priem, parNumber, visible: visible, toChart: toChart, divParam: scaleDiv, multParam: scaleMult, resultType:result, dbOper:oper));
			report.AddRecordType(new RecordTypeDB(PiramidaRecords.P_Vozb_GA2_Priem, parNumber, visible: visible, toChart: toChart, divParam: scaleDiv, multParam: scaleMult, resultType:result, dbOper:oper));
			report.AddRecordType(new RecordTypeDB(PiramidaRecords.P_Vozb_GA3_Priem, parNumber, visible: visible, toChart: toChart, divParam: scaleDiv, multParam: scaleMult, resultType:result, dbOper:oper));
			report.AddRecordType(new RecordTypeDB(PiramidaRecords.P_Vozb_GA4_Priem, parNumber, visible: visible, toChart: toChart, divParam: scaleDiv, multParam: scaleMult, resultType:result, dbOper:oper));
			report.AddRecordType(new RecordTypeDB(PiramidaRecords.P_Vozb_GA5_Priem, parNumber, visible: visible, toChart: toChart, divParam: scaleDiv, multParam: scaleMult, resultType:result, dbOper:oper));
			report.AddRecordType(new RecordTypeDB(PiramidaRecords.P_Vozb_GA6_Priem, parNumber, visible: visible, toChart: toChart, divParam: scaleDiv, multParam: scaleMult, resultType:result, dbOper:oper));
			report.AddRecordType(new RecordTypeDB(PiramidaRecords.P_Vozb_GA7_Priem, parNumber, visible: visible, toChart: toChart, divParam: scaleDiv, multParam: scaleMult, resultType:result, dbOper:oper));
			report.AddRecordType(new RecordTypeDB(PiramidaRecords.P_Vozb_GA8_Priem, parNumber, visible: visible, toChart: toChart, divParam: scaleDiv, multParam: scaleMult, resultType:result, dbOper:oper));
			report.AddRecordType(new RecordTypeDB(PiramidaRecords.P_Vozb_GA9_Priem, parNumber, visible: visible, toChart: toChart, divParam: scaleDiv, multParam: scaleMult, resultType:result, dbOper:oper));
			report.AddRecordType(new RecordTypeDB(PiramidaRecords.P_Vozb_GA10_Priem, parNumber, visible: visible, toChart: toChart, divParam: scaleDiv, multParam: scaleMult, resultType:result, dbOper:oper));

			report.AddRecordType(new RecordTypeDB(PiramidaRecords.P_SN_11T_Priem, parNumber, visible: visible, toChart: toChart, divParam: scaleDiv, multParam: scaleMult, resultType:result, dbOper:oper));
			report.AddRecordType(new RecordTypeDB(PiramidaRecords.P_SN_12T_Priem, parNumber, visible: visible, toChart: toChart, divParam: scaleDiv, multParam: scaleMult, resultType:result, dbOper:oper));
			report.AddRecordType(new RecordTypeDB(PiramidaRecords.P_SN_13T_Priem, parNumber, visible: visible, toChart: toChart, divParam: scaleDiv, multParam: scaleMult, resultType:result, dbOper:oper));
			report.AddRecordType(new RecordTypeDB(PiramidaRecords.P_SN_14T_Priem, parNumber, visible: visible, toChart: toChart, divParam: scaleDiv, multParam: scaleMult, resultType:result, dbOper:oper));
			report.AddRecordType(new RecordTypeDB(PiramidaRecords.P_SN_15T_Priem, parNumber, visible: visible, toChart: toChart, divParam: scaleDiv, multParam: scaleMult, resultType:result, dbOper:oper));
			report.AddRecordType(new RecordTypeDB(PiramidaRecords.P_SN_16T_Priem, parNumber, visible: visible, toChart: toChart, divParam: scaleDiv, multParam: scaleMult, resultType:result, dbOper:oper));
			report.AddRecordType(new RecordTypeDB(PiramidaRecords.P_SN_17T_Priem, parNumber, visible: visible, toChart: toChart, divParam: scaleDiv, multParam: scaleMult, resultType:result, dbOper:oper));
			report.AddRecordType(new RecordTypeDB(PiramidaRecords.P_SN_18T_Priem, parNumber, visible: visible, toChart: toChart, divParam: scaleDiv, multParam: scaleMult, resultType:result, dbOper:oper));
			report.AddRecordType(new RecordTypeDB(PiramidaRecords.P_SN_19T_Priem, parNumber, visible: visible, toChart: toChart, divParam: scaleDiv, multParam: scaleMult, resultType:result, dbOper:oper));
			report.AddRecordType(new RecordTypeDB(PiramidaRecords.P_SN_20T_Priem, parNumber, visible: visible, toChart: toChart, divParam: scaleDiv, multParam: scaleMult, resultType:result, dbOper:oper));
		}


		public static void AddPRecordsGAQ(Report report, int parNumber, double scaleMult, double scaleDiv, bool visible, bool toChart, DBOperEnum oper, ResultTypeEnum result) {
			report.AddRecordType(new RecordTypeDB(PiramidaRecords.Q_GA1_Otd, parNumber, visible: visible, toChart: toChart, divParam: scaleDiv, multParam: scaleMult, resultType:result, dbOper:oper));
			report.AddRecordType(new RecordTypeDB(PiramidaRecords.Q_GA2_Otd, parNumber, visible: visible, toChart: toChart, divParam: scaleDiv, multParam: scaleMult, resultType:result, dbOper:oper));
			report.AddRecordType(new RecordTypeDB(PiramidaRecords.Q_GA3_Otd, parNumber, visible: visible, toChart: toChart, divParam: scaleDiv, multParam: scaleMult, resultType:result, dbOper:oper));
			report.AddRecordType(new RecordTypeDB(PiramidaRecords.Q_GA4_Otd, parNumber, visible: visible, toChart: toChart, divParam: scaleDiv, multParam: scaleMult, resultType:result, dbOper:oper));
			report.AddRecordType(new RecordTypeDB(PiramidaRecords.Q_GA5_Otd, parNumber, visible: visible, toChart: toChart, divParam: scaleDiv, multParam: scaleMult, resultType:result, dbOper:oper));
			report.AddRecordType(new RecordTypeDB(PiramidaRecords.Q_GA6_Otd, parNumber, visible: visible, toChart: toChart, divParam: scaleDiv, multParam: scaleMult, resultType:result, dbOper:oper));
			report.AddRecordType(new RecordTypeDB(PiramidaRecords.Q_GA7_Otd, parNumber, visible: visible, toChart: toChart, divParam: scaleDiv, multParam: scaleMult, resultType:result, dbOper:oper));
			report.AddRecordType(new RecordTypeDB(PiramidaRecords.Q_GA8_Otd, parNumber, visible: visible, toChart: toChart, divParam: scaleDiv, multParam: scaleMult, resultType:result, dbOper:oper));
			report.AddRecordType(new RecordTypeDB(PiramidaRecords.Q_GA9_Otd, parNumber, visible: visible, toChart: toChart, divParam: scaleDiv, multParam: scaleMult, resultType:result, dbOper:oper));
			report.AddRecordType(new RecordTypeDB(PiramidaRecords.Q_GA10_Otd, parNumber, visible: visible, toChart: toChart, divParam: scaleDiv, multParam: scaleMult, resultType:result, dbOper:oper));


			report.AddRecordType(new RecordTypeDB(PiramidaRecords.Q_GA1_Priem, parNumber, visible: visible, toChart: toChart, divParam: scaleDiv, multParam: scaleMult, resultType:result, dbOper:oper));
			report.AddRecordType(new RecordTypeDB(PiramidaRecords.Q_GA2_Priem, parNumber, visible: visible, toChart: toChart, divParam: scaleDiv, multParam: scaleMult, resultType:result, dbOper:oper));
			report.AddRecordType(new RecordTypeDB(PiramidaRecords.Q_GA3_Priem, parNumber, visible: visible, toChart: toChart, divParam: scaleDiv, multParam: scaleMult, resultType:result, dbOper:oper));
			report.AddRecordType(new RecordTypeDB(PiramidaRecords.Q_GA4_Priem, parNumber, visible: visible, toChart: toChart, divParam: scaleDiv, multParam: scaleMult, resultType:result, dbOper:oper));
			report.AddRecordType(new RecordTypeDB(PiramidaRecords.Q_GA5_Priem, parNumber, visible: visible, toChart: toChart, divParam: scaleDiv, multParam: scaleMult, resultType:result, dbOper:oper));
			report.AddRecordType(new RecordTypeDB(PiramidaRecords.Q_GA6_Priem, parNumber, visible: visible, toChart: toChart, divParam: scaleDiv, multParam: scaleMult, resultType:result, dbOper:oper));
			report.AddRecordType(new RecordTypeDB(PiramidaRecords.Q_GA7_Priem, parNumber, visible: visible, toChart: toChart, divParam: scaleDiv, multParam: scaleMult, resultType:result, dbOper:oper));
			report.AddRecordType(new RecordTypeDB(PiramidaRecords.Q_GA8_Priem, parNumber, visible: visible, toChart: toChart, divParam: scaleDiv, multParam: scaleMult, resultType:result, dbOper:oper));
			report.AddRecordType(new RecordTypeDB(PiramidaRecords.Q_GA9_Priem, parNumber, visible: visible, toChart: toChart, divParam: scaleDiv, multParam: scaleMult, resultType:result, dbOper:oper));
			report.AddRecordType(new RecordTypeDB(PiramidaRecords.Q_GA10_Priem, parNumber, visible: visible, toChart: toChart, divParam: scaleDiv, multParam: scaleMult, resultType:result, dbOper:oper));
		}

	}
}
