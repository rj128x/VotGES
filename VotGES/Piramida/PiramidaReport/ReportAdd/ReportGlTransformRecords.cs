using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace VotGES.Piramida.PiramidaReport
{
	class ReportGlTransformRecords
	{
		public static RecordTypeCalc P_56AT_220;
		public static RecordTypeCalc P_56AT_110;
		public static RecordTypeCalc P_2AT_220;
		public static RecordTypeCalc P_2AT_500;
		public static RecordTypeCalc P_3AT_220;
		public static RecordTypeCalc P_3AT_500;
		public static RecordTypeCalc P_1T_110;
		public static RecordTypeCalc P_4T_220;

		public static RecordTypeCalc P_56AT_Nebalans;		
		public static RecordTypeCalc P_2AT_Nebalans;
		public static RecordTypeCalc P_3AT_Nebalans;
		public static RecordTypeCalc P_1T_Nebalans;
		public static RecordTypeCalc P_4T_Nebalans;

		public static void CreateGlTransformP() {
			P_1T_Nebalans = new RecordTypeCalc("P_1T_Nebalans", "1Т небаланс P",
				new RecordCalcDelegate((report, date) => {
					return
						(report[date,PiramidaRecords.P_GA1_Otd.Key] + report[date,PiramidaRecords.P_GA2_Otd.Key])
						- (report[date,PiramidaRecords.P_GA1_Priem.Key] + report[date,PiramidaRecords.P_GA2_Priem.Key])
						- (report[date,PiramidaRecords.P_Vozb_GA1_Priem.Key] + report[date,PiramidaRecords.P_Vozb_GA1_Priem.Key])
						- (report[date,PiramidaRecords.P_SN_11T_Priem.Key] + report[date,PiramidaRecords.P_SN_12T_Priem.Key])
						- (report[date,PiramidaRecords.P_1T_110_Priem.Key] - report[date,PiramidaRecords.P_1T_110_Otd.Key]);
				}));

			P_2AT_Nebalans = new RecordTypeCalc("P_2AT_Nebalans", "2AТ небаланс P",
				new RecordCalcDelegate((report, date) => {
					return
						(report[date,PiramidaRecords.P_GA3_Otd.Key] + report[date,PiramidaRecords.P_GA4_Otd.Key])
						- (report[date,PiramidaRecords.P_GA3_Priem.Key] + report[date,PiramidaRecords.P_GA4_Priem.Key])
						- (report[date,PiramidaRecords.P_Vozb_GA3_Priem.Key] + report[date,PiramidaRecords.P_Vozb_GA4_Priem.Key])
						- (report[date,PiramidaRecords.P_SN_13T_Priem.Key] + report[date,PiramidaRecords.P_SN_14T_Priem.Key])
						- (report[date,PiramidaRecords.P_2AT_220_Priem.Key] - report[date,PiramidaRecords.P_2AT_220_Otd.Key])
						- (report[date,PiramidaRecords.P_2AT_500_Priem.Key] - report[date,PiramidaRecords.P_2AT_500_Otd.Key])
						- report[date,PiramidaRecords.P_SN_7T_Priem.Key];
				}));

			P_3AT_Nebalans = new RecordTypeCalc("P_3AT_Nebalans", "3AТ небаланс P",
				new RecordCalcDelegate((report, date) => {
					return
						(report[date,PiramidaRecords.P_GA5_Otd.Key] + report[date,PiramidaRecords.P_GA6_Otd.Key])
						- (report[date,PiramidaRecords.P_GA5_Priem.Key] + report[date,PiramidaRecords.P_GA6_Priem.Key])
						- (report[date,PiramidaRecords.P_Vozb_GA5_Priem.Key] + report[date,PiramidaRecords.P_Vozb_GA6_Priem.Key])
						- (report[date,PiramidaRecords.P_SN_15T_Priem.Key] + report[date,PiramidaRecords.P_SN_16T_Priem.Key])
						- (report[date,PiramidaRecords.P_3AT_220_Priem.Key] - report[date,PiramidaRecords.P_3AT_220_Otd.Key])
						- (report[date,PiramidaRecords.P_3AT_500_Priem.Key] - report[date,PiramidaRecords.P_3AT_500_Otd.Key])
						- report[date,PiramidaRecords.P_SN_8T_Priem.Key];
				}));

			P_4T_Nebalans = new RecordTypeCalc("P_4T_Nebalans", "4Т небаланс P",
				new RecordCalcDelegate((report, date) => {
					return
						(report[date,PiramidaRecords.P_GA7_Otd.Key] + report[date,PiramidaRecords.P_GA8_Otd.Key])
						- (report[date,PiramidaRecords.P_GA7_Priem.Key] + report[date,PiramidaRecords.P_GA8_Priem.Key])
						- (report[date,PiramidaRecords.P_Vozb_GA7_Priem.Key] + report[date,PiramidaRecords.P_Vozb_GA8_Priem.Key])
						- (report[date,PiramidaRecords.P_SN_17T_Priem.Key] + report[date,PiramidaRecords.P_SN_18T_Priem.Key])
						- (report[date,PiramidaRecords.P_4T_220_Priem.Key] - report[date,PiramidaRecords.P_4T_220_Otd.Key]);
				}));

			P_56AT_Nebalans = new RecordTypeCalc("P_56AT_Nebalans", "5-6AТ небаланс P",
				new RecordCalcDelegate((report, date) => {
					return
						(report[date,PiramidaRecords.P_GA9_Otd.Key] + report[date,PiramidaRecords.P_GA10_Otd.Key])
						- (report[date,PiramidaRecords.P_GA9_Priem.Key] + report[date,PiramidaRecords.P_GA10_Priem.Key])
						- (report[date,PiramidaRecords.P_Vozb_GA9_Priem.Key] + report[date,PiramidaRecords.P_Vozb_GA10_Priem.Key])
						- (report[date,PiramidaRecords.P_SN_19T_Priem.Key] + report[date,PiramidaRecords.P_SN_20T_Priem.Key])
						- (report[date,PiramidaRecords.P_56AT_220_Priem.Key] - report[date,PiramidaRecords.P_56AT_220_Otd.Key])
						- (report[date,PiramidaRecords.P_56AT_110_Priem.Key] - report[date,PiramidaRecords.P_56AT_110_Otd.Key]);
				}));

			P_56AT_220 = new RecordTypeCalc("P_56AT_220", "5-6AТ P 220",
				new RecordCalcDelegate((report, date) => {
					return (report[date,PiramidaRecords.P_56AT_220_Priem.Key] - report[date,PiramidaRecords.P_56AT_220_Otd.Key]);
				}));

			P_56AT_110 = new RecordTypeCalc("P_56AT_110", "5-6AТ P 110",
				new RecordCalcDelegate((report, date) => {
					return (report[date,PiramidaRecords.P_56AT_110_Priem.Key] - report[date,PiramidaRecords.P_56AT_110_Otd.Key]);
				}));

			P_2AT_220 = new RecordTypeCalc("P_2AT_220", "2AТ P 220",
				new RecordCalcDelegate((report, date) => {
					return (report[date,PiramidaRecords.P_2AT_220_Priem.Key] - report[date,PiramidaRecords.P_2AT_220_Otd.Key]);
				}));

			P_2AT_500 = new RecordTypeCalc("P_2AT_500", "2AТ P 500",
				new RecordCalcDelegate((report, date) => {
					return (report[date,PiramidaRecords.P_2AT_500_Priem.Key] - report[date,PiramidaRecords.P_2AT_500_Otd.Key]);
				}));

			P_3AT_220 = new RecordTypeCalc("P_3AT_220", "3AТ P 220",
				new RecordCalcDelegate((report, date) => {
					return (report[date,PiramidaRecords.P_3AT_220_Priem.Key] - report[date,PiramidaRecords.P_3AT_220_Otd.Key]);
				}));

			P_3AT_500 = new RecordTypeCalc("P_3AT_500", "3AТ P 500",
				new RecordCalcDelegate((report, date) => {
					return (report[date,PiramidaRecords.P_3AT_500_Priem.Key] - report[date,PiramidaRecords.P_3AT_500_Otd.Key]);
				}));


			P_1T_110 = new RecordTypeCalc("P_1T_110", "1Т P 110",
				new RecordCalcDelegate((report, date) => {
					return (report[date,PiramidaRecords.P_1T_110_Priem.Key] - report[date,PiramidaRecords.P_1T_110_Otd.Key]);
				}));

			P_4T_220 = new RecordTypeCalc("P_4T_220", "4Т P 220",
				new RecordCalcDelegate((report, date) => {
					return (report[date,PiramidaRecords.P_4T_220_Priem.Key] - report[date,PiramidaRecords.P_4T_220_Otd.Key]);
				}));
		}


		public static void AddGLTransformRecordsP(Report report, int parNumber, double scaleMult, double scaleDiv, bool visible, bool toChart) {
			report.AddRecordType(new RecordTypeDB(PiramidaRecords.P_2AT_220_Otd, parNumber, visible: visible, toChart: toChart, divParam: scaleDiv, multParam: scaleMult));
			report.AddRecordType(new RecordTypeDB(PiramidaRecords.P_2AT_220_Priem, parNumber, visible: visible, toChart: toChart, divParam: scaleDiv, multParam: scaleMult));
			report.AddRecordType(new RecordTypeDB(PiramidaRecords.P_2AT_500_Otd, parNumber, visible: visible, toChart: toChart, divParam: scaleDiv, multParam: scaleMult));
			report.AddRecordType(new RecordTypeDB(PiramidaRecords.P_2AT_500_Priem, parNumber, visible: visible, toChart: toChart, divParam: scaleDiv, multParam: scaleMult));

			report.AddRecordType(new RecordTypeDB(PiramidaRecords.P_3AT_220_Otd, parNumber, visible: visible, toChart: toChart, divParam: scaleDiv, multParam: scaleMult));
			report.AddRecordType(new RecordTypeDB(PiramidaRecords.P_3AT_220_Priem, parNumber, visible: visible, toChart: toChart, divParam: scaleDiv, multParam: scaleMult));
			report.AddRecordType(new RecordTypeDB(PiramidaRecords.P_3AT_500_Otd, parNumber, visible: visible, toChart: toChart, divParam: scaleDiv, multParam: scaleMult));
			report.AddRecordType(new RecordTypeDB(PiramidaRecords.P_3AT_500_Priem, parNumber, visible: visible, toChart: toChart, divParam: scaleDiv, multParam: scaleMult));

			report.AddRecordType(new RecordTypeDB(PiramidaRecords.P_56AT_110_Otd, parNumber, visible: visible, toChart: toChart, divParam: scaleDiv, multParam: scaleMult));
			report.AddRecordType(new RecordTypeDB(PiramidaRecords.P_56AT_110_Priem, parNumber, visible: visible, toChart: toChart, divParam: scaleDiv, multParam: scaleMult));
			report.AddRecordType(new RecordTypeDB(PiramidaRecords.P_56AT_220_Otd, parNumber, visible: visible, toChart: toChart, divParam: scaleDiv, multParam: scaleMult));
			report.AddRecordType(new RecordTypeDB(PiramidaRecords.P_56AT_220_Priem, parNumber, visible: visible, toChart: toChart, divParam: scaleDiv, multParam: scaleMult));

			report.AddRecordType(new RecordTypeDB(PiramidaRecords.P_4T_220_Otd, parNumber, visible: visible, toChart: toChart, divParam: scaleDiv, multParam: scaleMult));
			report.AddRecordType(new RecordTypeDB(PiramidaRecords.P_4T_220_Priem, parNumber, visible: visible, toChart: toChart, divParam: scaleDiv, multParam: scaleMult));
			report.AddRecordType(new RecordTypeDB(PiramidaRecords.P_1T_110_Otd, parNumber, visible: visible, toChart: toChart, divParam: scaleDiv, multParam: scaleMult));
			report.AddRecordType(new RecordTypeDB(PiramidaRecords.P_1T_110_Priem, parNumber, visible: visible, toChart: toChart, divParam: scaleDiv, multParam: scaleMult));
						
		}

		public static void AddPRecordsForNebalans(Report report, int parNumber, double scaleMult, double scaleDiv, bool visible, bool toChart) {
			ReportGARecords.AddPRecordsGAAdd(report, parNumber, scaleMult, scaleDiv, visible, toChart);
			ReportGARecords.AddPRecordsGAP(report, parNumber, scaleMult, scaleDiv, visible, toChart);
			report.AddRecordType(new RecordTypeDB(PiramidaRecords.P_SN_7T_Priem, parNumber, visible: visible, toChart: toChart, divParam: scaleDiv, multParam: scaleMult));
			report.AddRecordType(new RecordTypeDB(PiramidaRecords.P_SN_8T_Priem, parNumber, visible: visible, toChart: toChart, divParam: scaleDiv, multParam: scaleMult));		
			
		}
	}
}
