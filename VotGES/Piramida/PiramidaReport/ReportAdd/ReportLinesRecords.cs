using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace VotGES.Piramida.PiramidaReport
{
	class ReportLinesRecords
	{
		public static RecordTypeCalc P_VL110_Otd;
		public static RecordTypeCalc P_VL110_Priem;
		public static RecordTypeCalc P_VL220_Otd;
		public static RecordTypeCalc P_VL220_Priem;
		public static RecordTypeCalc P_VL500_Otd;
		public static RecordTypeCalc P_VL500_Priem;
		public static RecordTypeCalc P_VL110_Saldo;
		public static RecordTypeCalc P_VL220_Saldo;
		public static RecordTypeCalc P_VL500_Saldo;
		public static RecordTypeCalc P_VL110_Nebalans;
		public static RecordTypeCalc P_VL220_Nebalans;
		public static RecordTypeCalc P_VL500_Nebalans;

		public static void CreateLinesP() {
			P_VL110_Otd = new RecordTypeCalc("P_VL110_Otd", "ВЛ 110кВ отдача P", new RecordCalcDelegate((report, date) => {
				return
					report[date,PiramidaRecords.P_VL110_Berezovka_Otd.Key] +
					report[date,PiramidaRecords.P_VL110_Dubovaya_Otd.Key] +
					report[date,PiramidaRecords.P_VL110_Ivanovka_Otd.Key] +
					report[date,PiramidaRecords.P_VL110_Kauchuk_Otd.Key] +
					report[date,PiramidaRecords.P_VL110_KSHT1_Otd.Key] +
					report[date,PiramidaRecords.P_VL110_KSHT2_Otd.Key] +
					report[date,PiramidaRecords.P_VL110_Svetlaya_Otd.Key] +
					report[date,PiramidaRecords.P_VL110_TEC_Otd.Key] +
					report[date,PiramidaRecords.P_VL110_Vodozabor1_Otd.Key] +
					report[date,PiramidaRecords.P_VL110_Vodozabor2_Otd.Key];
			}));

			P_VL110_Priem = new RecordTypeCalc("P_VL110_Priem", "ВЛ 110кВ прием P", new RecordCalcDelegate((report, date) => {
				return
					report[date,PiramidaRecords.P_VL110_Berezovka_Priem.Key] +
					report[date,PiramidaRecords.P_VL110_Dubovaya_Priem.Key] +
					report[date,PiramidaRecords.P_VL110_Ivanovka_Priem.Key] +
					report[date,PiramidaRecords.P_VL110_Kauchuk_Priem.Key] +
					report[date,PiramidaRecords.P_VL110_KSHT1_Priem.Key] +
					report[date,PiramidaRecords.P_VL110_KSHT2_Priem.Key] +
					report[date,PiramidaRecords.P_VL110_Svetlaya_Priem.Key] +
					report[date,PiramidaRecords.P_VL110_TEC_Priem.Key] +
					report[date,PiramidaRecords.P_VL110_Vodozabor1_Priem.Key] +
					report[date,PiramidaRecords.P_VL110_Vodozabor2_Priem.Key];
			}));

			P_VL220_Otd = new RecordTypeCalc("P_VL220_Otd", "ВЛ 220кВ отдача P", new RecordCalcDelegate((report, date) => {
				return
					report[date,PiramidaRecords.P_VL220_Izhevsk1_Otd.Key] +
					report[date,PiramidaRecords.P_VL220_Izhevsk2_Otd.Key] +
					report[date,PiramidaRecords.P_VL220_Kauchuk1_Otd.Key] +
					report[date,PiramidaRecords.P_VL220_Kauchuk2_Otd.Key] +
					report[date,PiramidaRecords.P_VL220_Svetlaya_Otd.Key];
			}));

			P_VL220_Priem = new RecordTypeCalc("P_VL220_Priem", "ВЛ 220кВ прием P", new RecordCalcDelegate((report, date) => {
				return
					report[date,PiramidaRecords.P_VL220_Izhevsk1_Priem.Key] +
					report[date,PiramidaRecords.P_VL220_Izhevsk2_Priem.Key] +
					report[date,PiramidaRecords.P_VL220_Kauchuk1_Priem.Key] +
					report[date,PiramidaRecords.P_VL220_Kauchuk2_Priem.Key] +
					report[date,PiramidaRecords.P_VL220_Svetlaya_Priem.Key];
			}));

			P_VL500_Otd= new RecordTypeCalc("P_VL500_Otd", "ВЛ 500кВ отдача P", new RecordCalcDelegate((report, date) => {
				return
					report[date,PiramidaRecords.P_VL500_Emelino_Otd.Key] +
					report[date,PiramidaRecords.P_VL500_Karmanovo_Otd.Key] +
					report[date,PiramidaRecords.P_VL500_Vyatka_Otd.Key];
			}));

			P_VL500_Priem = new RecordTypeCalc("P_VL500_Priem", "ВЛ 500кВ прием P", new RecordCalcDelegate((report, date) => {
				return
					report[date,PiramidaRecords.P_VL500_Emelino_Priem.Key] +
					report[date,PiramidaRecords.P_VL500_Karmanovo_Priem.Key] +
					report[date,PiramidaRecords.P_VL500_Vytka_Priem.Key];
			}));

			P_VL110_Saldo = new RecordTypeCalc("P_VL110_Saldo", "ВЛ 110 кВ сальдо P", new RecordCalcDelegate((report, date) => {
				return
					report[date,ReportLinesRecords.P_VL110_Otd.ID] - report[date,ReportLinesRecords.P_VL110_Priem.ID];
			}));

			P_VL220_Saldo = new RecordTypeCalc("P_VL220_Saldo", "ВЛ 220 кВ сальдо P", new RecordCalcDelegate((report, date) => {
				return
					report[date,ReportLinesRecords.P_VL220_Otd.ID] - report[date,ReportLinesRecords.P_VL220_Priem.ID];
			}));

			P_VL500_Saldo = new RecordTypeCalc("P_VL500_Saldo", "ВЛ 500 кВ сальдо P", new RecordCalcDelegate((report, date) => {
				return
					report[date,ReportLinesRecords.P_VL500_Otd.ID] - report[date,ReportLinesRecords.P_VL500_Priem.ID];
			}));

			P_VL110_Nebalans = new RecordTypeCalc("P_VL110_Nebalans", "ВЛ 110 кВ небаланс P", new RecordCalcDelegate((report, date) => {
				return
					report[date,ReportLinesRecords.P_VL110_Saldo.ID] - 
					report[date,ReportGlTransformRecords.P_56AT_110.ID]-
					report[date,ReportGlTransformRecords.P_1T_110.ID];
			}));

			P_VL220_Nebalans = new RecordTypeCalc("P_VL220_Nebalans", "ВЛ 220 кВ небаланс P", new RecordCalcDelegate((report, date) => {
				return
					report[date,ReportLinesRecords.P_VL220_Saldo.ID] -
					report[date,ReportGlTransformRecords.P_56AT_220.ID] -
					report[date,ReportGlTransformRecords.P_4T_220.ID] -
					report[date,ReportGlTransformRecords.P_2AT_220.ID] -
					report[date,ReportGlTransformRecords.P_3AT_220.ID];
			}));

			P_VL500_Nebalans = new RecordTypeCalc("P_VL500_Nebalans", "ВЛ 500 кВ небаланс P", new RecordCalcDelegate((report, date) => {
				return
					report[date,ReportLinesRecords.P_VL500_Saldo.ID] -
					report[date,ReportGlTransformRecords.P_3AT_500.ID] -
					report[date,ReportGlTransformRecords.P_3AT_500.ID];
			}));
		}

		public static void AddLineRecordsP(Report report, int parNumber, double scaleMult, double scaleDiv, bool visible, bool toChart) {
			report.AddRecordType(new RecordTypeDB(PiramidaRecords.P_VL110_Berezovka_Otd, parNumber, visible: visible, toChart:toChart, divParam:scaleDiv, multParam:scaleMult));
			report.AddRecordType(new RecordTypeDB(PiramidaRecords.P_VL110_Berezovka_Priem, parNumber, visible: visible, toChart: toChart, divParam: scaleDiv, multParam: scaleMult));
			report.AddRecordType(new RecordTypeDB(PiramidaRecords.P_VL110_Dubovaya_Otd, parNumber, visible: visible, toChart: toChart, divParam: scaleDiv, multParam: scaleMult));
			report.AddRecordType(new RecordTypeDB(PiramidaRecords.P_VL110_Dubovaya_Priem, parNumber, visible: visible, toChart: toChart, divParam: scaleDiv, multParam: scaleMult));
			report.AddRecordType(new RecordTypeDB(PiramidaRecords.P_VL110_Ivanovka_Otd, parNumber, visible: visible, toChart: toChart, divParam: scaleDiv, multParam: scaleMult));
			report.AddRecordType(new RecordTypeDB(PiramidaRecords.P_VL110_Ivanovka_Priem, parNumber, visible: visible, toChart: toChart, divParam: scaleDiv, multParam: scaleMult));
			report.AddRecordType(new RecordTypeDB(PiramidaRecords.P_VL110_Kauchuk_Otd, parNumber, visible: visible, toChart: toChart, divParam: scaleDiv, multParam: scaleMult));
			report.AddRecordType(new RecordTypeDB(PiramidaRecords.P_VL110_Kauchuk_Priem, parNumber, visible: visible, toChart: toChart, divParam: scaleDiv, multParam: scaleMult));
			report.AddRecordType(new RecordTypeDB(PiramidaRecords.P_VL110_KSHT1_Otd, parNumber, visible: visible, toChart: toChart, divParam: scaleDiv, multParam: scaleMult));
			report.AddRecordType(new RecordTypeDB(PiramidaRecords.P_VL110_KSHT1_Priem, parNumber, visible: visible, toChart: toChart, divParam: scaleDiv, multParam: scaleMult));
			report.AddRecordType(new RecordTypeDB(PiramidaRecords.P_VL110_KSHT2_Otd, parNumber, visible: visible, toChart: toChart, divParam: scaleDiv, multParam: scaleMult));
			report.AddRecordType(new RecordTypeDB(PiramidaRecords.P_VL110_KSHT2_Priem, parNumber, visible: visible, toChart: toChart, divParam: scaleDiv, multParam: scaleMult));
			report.AddRecordType(new RecordTypeDB(PiramidaRecords.P_VL110_Svetlaya_Otd, parNumber, visible: visible, toChart: toChart, divParam: scaleDiv, multParam: scaleMult));
			report.AddRecordType(new RecordTypeDB(PiramidaRecords.P_VL110_Svetlaya_Priem, parNumber, visible: visible, toChart: toChart, divParam: scaleDiv, multParam: scaleMult));
			report.AddRecordType(new RecordTypeDB(PiramidaRecords.P_VL110_TEC_Otd, parNumber, visible: visible, toChart: toChart, divParam: scaleDiv, multParam: scaleMult));
			report.AddRecordType(new RecordTypeDB(PiramidaRecords.P_VL110_TEC_Priem, parNumber, visible: visible, toChart: toChart, divParam: scaleDiv, multParam: scaleMult));
			report.AddRecordType(new RecordTypeDB(PiramidaRecords.P_VL110_Vodozabor1_Otd, parNumber, visible: visible, toChart: toChart, divParam: scaleDiv, multParam: scaleMult));
			report.AddRecordType(new RecordTypeDB(PiramidaRecords.P_VL110_Vodozabor1_Priem, parNumber, visible: visible, toChart: toChart, divParam: scaleDiv, multParam: scaleMult));
			report.AddRecordType(new RecordTypeDB(PiramidaRecords.P_VL110_Vodozabor2_Otd, parNumber, visible: visible, toChart: toChart, divParam: scaleDiv, multParam: scaleMult));
			report.AddRecordType(new RecordTypeDB(PiramidaRecords.P_VL110_Vodozabor2_Priem, parNumber, visible: visible, toChart: toChart, divParam: scaleDiv, multParam: scaleMult));

			report.AddRecordType(new RecordTypeDB(PiramidaRecords.P_VL220_Izhevsk1_Otd, parNumber, visible: visible, toChart: toChart, divParam: scaleDiv, multParam: scaleMult));
			report.AddRecordType(new RecordTypeDB(PiramidaRecords.P_VL220_Izhevsk1_Priem, parNumber, visible: visible, toChart: toChart, divParam: scaleDiv, multParam: scaleMult));
			report.AddRecordType(new RecordTypeDB(PiramidaRecords.P_VL220_Izhevsk2_Otd, parNumber, visible: visible, toChart: toChart, divParam: scaleDiv, multParam: scaleMult));
			report.AddRecordType(new RecordTypeDB(PiramidaRecords.P_VL220_Izhevsk2_Priem, parNumber, visible: visible, toChart: toChart, divParam: scaleDiv, multParam: scaleMult));
			report.AddRecordType(new RecordTypeDB(PiramidaRecords.P_VL220_Kauchuk1_Otd, parNumber, visible: visible, toChart: toChart, divParam: scaleDiv, multParam: scaleMult));
			report.AddRecordType(new RecordTypeDB(PiramidaRecords.P_VL220_Kauchuk1_Priem, parNumber, visible: visible, toChart: toChart, divParam: scaleDiv, multParam: scaleMult));
			report.AddRecordType(new RecordTypeDB(PiramidaRecords.P_VL220_Kauchuk2_Otd, parNumber, visible: visible, toChart: toChart, divParam: scaleDiv, multParam: scaleMult));
			report.AddRecordType(new RecordTypeDB(PiramidaRecords.P_VL220_Kauchuk2_Priem, parNumber, visible: visible, toChart: toChart, divParam: scaleDiv, multParam: scaleMult));
			report.AddRecordType(new RecordTypeDB(PiramidaRecords.P_VL220_Svetlaya_Otd, parNumber, visible: visible, toChart: toChart, divParam: scaleDiv, multParam: scaleMult));
			report.AddRecordType(new RecordTypeDB(PiramidaRecords.P_VL220_Svetlaya_Priem, parNumber, visible: visible, toChart: toChart, divParam: scaleDiv, multParam: scaleMult));

			report.AddRecordType(new RecordTypeDB(PiramidaRecords.P_VL500_Emelino_Otd, parNumber, visible: visible, toChart: toChart, divParam: scaleDiv, multParam: scaleMult));
			report.AddRecordType(new RecordTypeDB(PiramidaRecords.P_VL500_Emelino_Priem, parNumber, visible: visible, toChart: toChart, divParam: scaleDiv, multParam: scaleMult));
			report.AddRecordType(new RecordTypeDB(PiramidaRecords.P_VL500_Karmanovo_Otd, parNumber, visible: visible, toChart: toChart, divParam: scaleDiv, multParam: scaleMult));
			report.AddRecordType(new RecordTypeDB(PiramidaRecords.P_VL500_Karmanovo_Priem, parNumber, visible: visible, toChart: toChart, divParam: scaleDiv, multParam: scaleMult));
			report.AddRecordType(new RecordTypeDB(PiramidaRecords.P_VL500_Vyatka_Otd, parNumber, visible: visible, toChart: toChart, divParam: scaleDiv, multParam: scaleMult));
			report.AddRecordType(new RecordTypeDB(PiramidaRecords.P_VL500_Vytka_Priem, parNumber, visible: visible, toChart: toChart, divParam: scaleDiv, multParam: scaleMult));
		}	


	}
}
