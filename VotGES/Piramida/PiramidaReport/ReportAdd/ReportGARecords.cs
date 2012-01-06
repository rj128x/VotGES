using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace VotGES.Piramida.PiramidaReport
{
	class ReportGARecords
	{
		public static RecordTypeCalc P_GA1;
		public static RecordTypeCalc P_GA2;
		public static RecordTypeCalc P_GA3;
		public static RecordTypeCalc P_GA4;
		public static RecordTypeCalc P_GA5;
		public static RecordTypeCalc P_GA6;
		public static RecordTypeCalc P_GA7;
		public static RecordTypeCalc P_GA8;
		public static RecordTypeCalc P_GA9;
		public static RecordTypeCalc P_GA10;

		public static RecordTypeCalc Q_GA1;
		public static RecordTypeCalc Q_GA2;
		public static RecordTypeCalc Q_GA3;
		public static RecordTypeCalc Q_GA4;
		public static RecordTypeCalc Q_GA5;
		public static RecordTypeCalc Q_GA6;
		public static RecordTypeCalc Q_GA7;
		public static RecordTypeCalc Q_GA8;
		public static RecordTypeCalc Q_GA9;
		public static RecordTypeCalc Q_GA10;

		public static void CreateGAP() {
			P_GA1 = new RecordTypeCalc("P_GA1", "ГА-1 P",
				new RecordCalcDelegate((report, date) => {
					return (report.Data[date][PiramidaRecords.P_GA1_Otd.Key] - report.Data[date][PiramidaRecords.P_GA1_Priem.Key]);
				}));

			P_GA2 = new RecordTypeCalc("P_GA2", "ГА-2 P",
				new RecordCalcDelegate((report, date) => {
					return (report.Data[date][PiramidaRecords.P_GA2_Otd.Key] - report.Data[date][PiramidaRecords.P_GA2_Priem.Key]);
				}));

			P_GA3 = new RecordTypeCalc("P_GA3", "ГА-3 P",
				new RecordCalcDelegate((report, date) => {
					return (report.Data[date][PiramidaRecords.P_GA3_Otd.Key] - report.Data[date][PiramidaRecords.P_GA3_Priem.Key]);
				}));

			P_GA4 = new RecordTypeCalc("P_GA4", "ГА-4 P",
				new RecordCalcDelegate((report, date) => {
					return (report.Data[date][PiramidaRecords.P_GA4_Otd.Key] - report.Data[date][PiramidaRecords.P_GA4_Priem.Key]);
				}));

			P_GA5 = new RecordTypeCalc("P_GA5", "ГА-5 P",
				new RecordCalcDelegate((report, date) => {
					return (report.Data[date][PiramidaRecords.P_GA5_Otd.Key] - report.Data[date][PiramidaRecords.P_GA5_Priem.Key]);
				}));

			P_GA6 = new RecordTypeCalc("P_GA6", "ГА-6 P",
				new RecordCalcDelegate((report, date) => {
					return (report.Data[date][PiramidaRecords.P_GA6_Otd.Key] - report.Data[date][PiramidaRecords.P_GA6_Priem.Key]);
				}));

			P_GA7 = new RecordTypeCalc("P_GA7", "ГА-7 P",
				new RecordCalcDelegate((report, date) => {
					return (report.Data[date][PiramidaRecords.P_GA7_Otd.Key] - report.Data[date][PiramidaRecords.P_GA7_Priem.Key]);
				}));

			P_GA8 = new RecordTypeCalc("P_GA8", "ГА-8 P",
				new RecordCalcDelegate((report, date) => {
					return (report.Data[date][PiramidaRecords.P_GA8_Otd.Key] - report.Data[date][PiramidaRecords.P_GA8_Priem.Key]);
				}));

			P_GA9 = new RecordTypeCalc("P_GA9", "ГА-9 P",
				new RecordCalcDelegate((report, date) => {
					return (report.Data[date][PiramidaRecords.P_GA9_Otd.Key] - report.Data[date][PiramidaRecords.P_GA9_Priem.Key]);
				}));

			P_GA10 = new RecordTypeCalc("P_GA10", "ГА-10 P",
				new RecordCalcDelegate((report, date) => {
					return (report.Data[date][PiramidaRecords.P_GA10_Otd.Key] - report.Data[date][PiramidaRecords.P_GA10_Priem.Key]);
				}));
		}

		public static void CreateGAQ() {
			Q_GA1 = new RecordTypeCalc("Q_GA1", "ГА-1 Q",
				new RecordCalcDelegate((report, date) => {
					return (report.Data[date][PiramidaRecords.Q_GA1_Otd.Key] - report.Data[date][PiramidaRecords.Q_GA1_Priem.Key]);
				}));

			Q_GA2 = new RecordTypeCalc("Q_GA2", "ГА-2 Q",
				new RecordCalcDelegate((report, date) => {
					return (report.Data[date][PiramidaRecords.Q_GA2_Otd.Key] - report.Data[date][PiramidaRecords.Q_GA2_Priem.Key]);
				}));

			Q_GA3 = new RecordTypeCalc("Q_GA3", "ГА-3 Q",
				new RecordCalcDelegate((report, date) => {
					return (report.Data[date][PiramidaRecords.Q_GA3_Otd.Key] - report.Data[date][PiramidaRecords.Q_GA3_Priem.Key]);
				}));

			Q_GA4 = new RecordTypeCalc("Q_GA4", "ГА-4 Q",
				new RecordCalcDelegate((report, date) => {
					return (report.Data[date][PiramidaRecords.Q_GA4_Otd.Key] - report.Data[date][PiramidaRecords.Q_GA4_Priem.Key]);
				}));

			Q_GA5 = new RecordTypeCalc("Q_GA5", "ГА-5 Q",
				new RecordCalcDelegate((report, date) => {
					return (report.Data[date][PiramidaRecords.Q_GA5_Otd.Key] - report.Data[date][PiramidaRecords.Q_GA5_Priem.Key]);
				}));

			Q_GA6 = new RecordTypeCalc("Q_GA6", "ГА-6 Q",
				new RecordCalcDelegate((report, date) => {
					return (report.Data[date][PiramidaRecords.Q_GA6_Otd.Key] - report.Data[date][PiramidaRecords.Q_GA6_Priem.Key]);
				}));

			Q_GA7 = new RecordTypeCalc("Q_GA7", "ГА-7 Q",
				new RecordCalcDelegate((report, date) => {
					return (report.Data[date][PiramidaRecords.Q_GA7_Otd.Key] - report.Data[date][PiramidaRecords.Q_GA7_Priem.Key]);
				}));

			Q_GA8 = new RecordTypeCalc("Q_GA8", "ГА-8 Q",
				new RecordCalcDelegate((report, date) => {
					return (report.Data[date][PiramidaRecords.Q_GA8_Otd.Key] - report.Data[date][PiramidaRecords.Q_GA8_Priem.Key]);
				}));

			Q_GA9 = new RecordTypeCalc("Q_GA9", "ГА-9 Q",
				new RecordCalcDelegate((report, date) => {
					return (report.Data[date][PiramidaRecords.Q_GA9_Otd.Key] - report.Data[date][PiramidaRecords.Q_GA9_Priem.Key]);
				}));

			Q_GA10 = new RecordTypeCalc("Q_GA10", "ГА-10 Q",
				new RecordCalcDelegate((report, date) => {
					return (report.Data[date][PiramidaRecords.Q_GA10_Otd.Key] - report.Data[date][PiramidaRecords.Q_GA10_Priem.Key]);
				}));
		}

		public static void AddPRecordsGAP(Report report, int parNumber, double scaleMult, double scaleDiv, bool visible, bool toChart) {

			report.AddRecordType(new RecordTypeDB(PiramidaRecords.P_GA1_Otd, parNumber, visible: visible, toChart: toChart, divParam: scaleDiv, multParam: scaleMult));
			report.AddRecordType(new RecordTypeDB(PiramidaRecords.P_GA2_Otd, parNumber, visible: visible, toChart: toChart, divParam: scaleDiv, multParam: scaleMult));
			report.AddRecordType(new RecordTypeDB(PiramidaRecords.P_GA3_Otd, parNumber, visible: visible, toChart: toChart, divParam: scaleDiv, multParam: scaleMult));
			report.AddRecordType(new RecordTypeDB(PiramidaRecords.P_GA4_Otd, parNumber, visible: visible, toChart: toChart, divParam: scaleDiv, multParam: scaleMult));
			report.AddRecordType(new RecordTypeDB(PiramidaRecords.P_GA5_Otd, parNumber, visible: visible, toChart: toChart, divParam: scaleDiv, multParam: scaleMult));
			report.AddRecordType(new RecordTypeDB(PiramidaRecords.P_GA6_Otd, parNumber, visible: visible, toChart: toChart, divParam: scaleDiv, multParam: scaleMult));
			report.AddRecordType(new RecordTypeDB(PiramidaRecords.P_GA7_Otd, parNumber, visible: visible, toChart: toChart, divParam: scaleDiv, multParam: scaleMult));
			report.AddRecordType(new RecordTypeDB(PiramidaRecords.P_GA8_Otd, parNumber, visible: visible, toChart: toChart, divParam: scaleDiv, multParam: scaleMult));
			report.AddRecordType(new RecordTypeDB(PiramidaRecords.P_GA9_Otd, parNumber, visible: visible, toChart: toChart, divParam: scaleDiv, multParam: scaleMult));
			report.AddRecordType(new RecordTypeDB(PiramidaRecords.P_GA10_Otd, parNumber, visible: visible, toChart: toChart, divParam: scaleDiv, multParam: scaleMult));


			report.AddRecordType(new RecordTypeDB(PiramidaRecords.P_GA1_Priem, parNumber, visible: visible, toChart: toChart, divParam: scaleDiv, multParam: scaleMult));
			report.AddRecordType(new RecordTypeDB(PiramidaRecords.P_GA2_Priem, parNumber, visible: visible, toChart: toChart, divParam: scaleDiv, multParam: scaleMult));
			report.AddRecordType(new RecordTypeDB(PiramidaRecords.P_GA3_Priem, parNumber, visible: visible, toChart: toChart, divParam: scaleDiv, multParam: scaleMult));
			report.AddRecordType(new RecordTypeDB(PiramidaRecords.P_GA4_Priem, parNumber, visible: visible, toChart: toChart, divParam: scaleDiv, multParam: scaleMult));
			report.AddRecordType(new RecordTypeDB(PiramidaRecords.P_GA5_Priem, parNumber, visible: visible, toChart: toChart, divParam: scaleDiv, multParam: scaleMult));
			report.AddRecordType(new RecordTypeDB(PiramidaRecords.P_GA6_Priem, parNumber, visible: visible, toChart: toChart, divParam: scaleDiv, multParam: scaleMult));
			report.AddRecordType(new RecordTypeDB(PiramidaRecords.P_GA7_Priem, parNumber, visible: visible, toChart: toChart, divParam: scaleDiv, multParam: scaleMult));
			report.AddRecordType(new RecordTypeDB(PiramidaRecords.P_GA8_Priem, parNumber, visible: visible, toChart: toChart, divParam: scaleDiv, multParam: scaleMult));
			report.AddRecordType(new RecordTypeDB(PiramidaRecords.P_GA9_Priem, parNumber, visible: visible, toChart: toChart, divParam: scaleDiv, multParam: scaleMult));
			report.AddRecordType(new RecordTypeDB(PiramidaRecords.P_GA10_Priem, parNumber, visible: visible, toChart: toChart, divParam: scaleDiv, multParam: scaleMult));
		}

		public static void AddPRecordsGAAdd(Report report, int parNumber, double scaleMult, double scaleDiv, bool visible, bool toChart) {
			report.AddRecordType(new RecordTypeDB(PiramidaRecords.P_Vozb_GA1_Priem, parNumber, visible: visible, toChart: toChart, divParam: scaleDiv, multParam: scaleMult));
			report.AddRecordType(new RecordTypeDB(PiramidaRecords.P_Vozb_GA2_Priem, parNumber, visible: visible, toChart: toChart, divParam: scaleDiv, multParam: scaleMult));
			report.AddRecordType(new RecordTypeDB(PiramidaRecords.P_Vozb_GA3_Priem, parNumber, visible: visible, toChart: toChart, divParam: scaleDiv, multParam: scaleMult));
			report.AddRecordType(new RecordTypeDB(PiramidaRecords.P_Vozb_GA4_Priem, parNumber, visible: visible, toChart: toChart, divParam: scaleDiv, multParam: scaleMult));
			report.AddRecordType(new RecordTypeDB(PiramidaRecords.P_Vozb_GA5_Priem, parNumber, visible: visible, toChart: toChart, divParam: scaleDiv, multParam: scaleMult));
			report.AddRecordType(new RecordTypeDB(PiramidaRecords.P_Vozb_GA6_Priem, parNumber, visible: visible, toChart: toChart, divParam: scaleDiv, multParam: scaleMult));
			report.AddRecordType(new RecordTypeDB(PiramidaRecords.P_Vozb_GA7_Priem, parNumber, visible: visible, toChart: toChart, divParam: scaleDiv, multParam: scaleMult));
			report.AddRecordType(new RecordTypeDB(PiramidaRecords.P_Vozb_GA8_Priem, parNumber, visible: visible, toChart: toChart, divParam: scaleDiv, multParam: scaleMult));
			report.AddRecordType(new RecordTypeDB(PiramidaRecords.P_Vozb_GA9_Priem, parNumber, visible: visible, toChart: toChart, divParam: scaleDiv, multParam: scaleMult));
			report.AddRecordType(new RecordTypeDB(PiramidaRecords.P_Vozb_GA10_Priem, parNumber, visible: visible, toChart: toChart, divParam: scaleDiv, multParam: scaleMult));

			report.AddRecordType(new RecordTypeDB(PiramidaRecords.P_SN_11T_Priem, parNumber, visible: visible, toChart: toChart, divParam: scaleDiv, multParam: scaleMult));
			report.AddRecordType(new RecordTypeDB(PiramidaRecords.P_SN_12T_Priem, parNumber, visible: visible, toChart: toChart, divParam: scaleDiv, multParam: scaleMult));
			report.AddRecordType(new RecordTypeDB(PiramidaRecords.P_SN_13T_Priem, parNumber, visible: visible, toChart: toChart, divParam: scaleDiv, multParam: scaleMult));
			report.AddRecordType(new RecordTypeDB(PiramidaRecords.P_SN_14T_Priem, parNumber, visible: visible, toChart: toChart, divParam: scaleDiv, multParam: scaleMult));
			report.AddRecordType(new RecordTypeDB(PiramidaRecords.P_SN_15T_Priem, parNumber, visible: visible, toChart: toChart, divParam: scaleDiv, multParam: scaleMult));
			report.AddRecordType(new RecordTypeDB(PiramidaRecords.P_SN_16T_Priem, parNumber, visible: visible, toChart: toChart, divParam: scaleDiv, multParam: scaleMult));
			report.AddRecordType(new RecordTypeDB(PiramidaRecords.P_SN_17T_Priem, parNumber, visible: visible, toChart: toChart, divParam: scaleDiv, multParam: scaleMult));
			report.AddRecordType(new RecordTypeDB(PiramidaRecords.P_SN_18T_Priem, parNumber, visible: visible, toChart: toChart, divParam: scaleDiv, multParam: scaleMult));
			report.AddRecordType(new RecordTypeDB(PiramidaRecords.P_SN_19T_Priem, parNumber, visible: visible, toChart: toChart, divParam: scaleDiv, multParam: scaleMult));
			report.AddRecordType(new RecordTypeDB(PiramidaRecords.P_SN_20T_Priem, parNumber, visible: visible, toChart: toChart, divParam: scaleDiv, multParam: scaleMult));
		}


		public static void AddPRecordsGAQ(Report report, int parNumber, double scaleMult, double scaleDiv, bool visible, bool toChart) {
			report.AddRecordType(new RecordTypeDB(PiramidaRecords.Q_GA1_Otd, parNumber, visible: visible, toChart: toChart, divParam: scaleDiv, multParam: scaleMult));
			report.AddRecordType(new RecordTypeDB(PiramidaRecords.Q_GA2_Otd, parNumber, visible: visible, toChart: toChart, divParam: scaleDiv, multParam: scaleMult));
			report.AddRecordType(new RecordTypeDB(PiramidaRecords.Q_GA3_Otd, parNumber, visible: visible, toChart: toChart, divParam: scaleDiv, multParam: scaleMult));
			report.AddRecordType(new RecordTypeDB(PiramidaRecords.Q_GA4_Otd, parNumber, visible: visible, toChart: toChart, divParam: scaleDiv, multParam: scaleMult));
			report.AddRecordType(new RecordTypeDB(PiramidaRecords.Q_GA5_Otd, parNumber, visible: visible, toChart: toChart, divParam: scaleDiv, multParam: scaleMult));
			report.AddRecordType(new RecordTypeDB(PiramidaRecords.Q_GA6_Otd, parNumber, visible: visible, toChart: toChart, divParam: scaleDiv, multParam: scaleMult));
			report.AddRecordType(new RecordTypeDB(PiramidaRecords.Q_GA7_Otd, parNumber, visible: visible, toChart: toChart, divParam: scaleDiv, multParam: scaleMult));
			report.AddRecordType(new RecordTypeDB(PiramidaRecords.Q_GA8_Otd, parNumber, visible: visible, toChart: toChart, divParam: scaleDiv, multParam: scaleMult));
			report.AddRecordType(new RecordTypeDB(PiramidaRecords.Q_GA9_Otd, parNumber, visible: visible, toChart: toChart, divParam: scaleDiv, multParam: scaleMult));
			report.AddRecordType(new RecordTypeDB(PiramidaRecords.Q_GA10_Otd, parNumber, visible: visible, toChart: toChart, divParam: scaleDiv, multParam: scaleMult));


			report.AddRecordType(new RecordTypeDB(PiramidaRecords.Q_GA1_Priem, parNumber, visible: visible, toChart: toChart, divParam: scaleDiv, multParam: scaleMult));
			report.AddRecordType(new RecordTypeDB(PiramidaRecords.Q_GA2_Priem, parNumber, visible: visible, toChart: toChart, divParam: scaleDiv, multParam: scaleMult));
			report.AddRecordType(new RecordTypeDB(PiramidaRecords.Q_GA3_Priem, parNumber, visible: visible, toChart: toChart, divParam: scaleDiv, multParam: scaleMult));
			report.AddRecordType(new RecordTypeDB(PiramidaRecords.Q_GA4_Priem, parNumber, visible: visible, toChart: toChart, divParam: scaleDiv, multParam: scaleMult));
			report.AddRecordType(new RecordTypeDB(PiramidaRecords.Q_GA5_Priem, parNumber, visible: visible, toChart: toChart, divParam: scaleDiv, multParam: scaleMult));
			report.AddRecordType(new RecordTypeDB(PiramidaRecords.Q_GA6_Priem, parNumber, visible: visible, toChart: toChart, divParam: scaleDiv, multParam: scaleMult));
			report.AddRecordType(new RecordTypeDB(PiramidaRecords.Q_GA7_Priem, parNumber, visible: visible, toChart: toChart, divParam: scaleDiv, multParam: scaleMult));
			report.AddRecordType(new RecordTypeDB(PiramidaRecords.Q_GA8_Priem, parNumber, visible: visible, toChart: toChart, divParam: scaleDiv, multParam: scaleMult));
			report.AddRecordType(new RecordTypeDB(PiramidaRecords.Q_GA9_Priem, parNumber, visible: visible, toChart: toChart, divParam: scaleDiv, multParam: scaleMult));
			report.AddRecordType(new RecordTypeDB(PiramidaRecords.Q_GA10_Priem, parNumber, visible: visible, toChart: toChart, divParam: scaleDiv, multParam: scaleMult));
		}

	}
}
