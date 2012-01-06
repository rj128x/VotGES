using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace VotGES.Piramida.PiramidaReport
{
	public class SNReport : Report
	{
		public SNReport(DateTime dateStart, DateTime dateEnd, IntervalReportEnum interval) :
			base(dateStart, dateEnd, interval) {
			AddRecordType(new RecordTypeDB(PiramidaRecords.P_GES, 12, visible: true));

			AddRecordType(new RecordTypeDB(PiramidaRecords.P_Vozb_GA1_Priem, 12, visible: false));
			AddRecordType(new RecordTypeDB(PiramidaRecords.P_Vozb_GA2_Priem, 12, visible: false));
			AddRecordType(new RecordTypeDB(PiramidaRecords.P_Vozb_GA3_Priem, 12, visible: false));
			AddRecordType(new RecordTypeDB(PiramidaRecords.P_Vozb_GA4_Priem, 12, visible: false));
			AddRecordType(new RecordTypeDB(PiramidaRecords.P_Vozb_GA5_Priem, 12, visible: false));
			AddRecordType(new RecordTypeDB(PiramidaRecords.P_Vozb_GA6_Priem, 12, visible: false));
			AddRecordType(new RecordTypeDB(PiramidaRecords.P_Vozb_GA7_Priem, 12, visible: false));
			AddRecordType(new RecordTypeDB(PiramidaRecords.P_Vozb_GA8_Priem, 12, visible: false));
			AddRecordType(new RecordTypeDB(PiramidaRecords.P_Vozb_GA9_Priem, 12, visible: false));
			AddRecordType(new RecordTypeDB(PiramidaRecords.P_Vozb_GA10_Priem, 12, visible: false));

			

			AddRecordType(new RecordTypeDB(PiramidaRecords.P_GA1_Otd, 12, visible: false));
			AddRecordType(new RecordTypeDB(PiramidaRecords.P_GA2_Otd, 12, visible: false));
			AddRecordType(new RecordTypeDB(PiramidaRecords.P_GA3_Otd, 12, visible: false));
			AddRecordType(new RecordTypeDB(PiramidaRecords.P_GA4_Otd, 12, visible: false));
			AddRecordType(new RecordTypeDB(PiramidaRecords.P_GA5_Otd, 12, visible: false));
			AddRecordType(new RecordTypeDB(PiramidaRecords.P_GA6_Otd, 12, visible: false));
			AddRecordType(new RecordTypeDB(PiramidaRecords.P_GA7_Otd, 12, visible: false));
			AddRecordType(new RecordTypeDB(PiramidaRecords.P_GA8_Otd, 12, visible: false));
			AddRecordType(new RecordTypeDB(PiramidaRecords.P_GA9_Otd, 12, visible: false));
			AddRecordType(new RecordTypeDB(PiramidaRecords.P_GA10_Otd, 12, visible: false));


			AddRecordType(new RecordTypeDB(PiramidaRecords.P_GA1_Priem, 12, visible: false));
			AddRecordType(new RecordTypeDB(PiramidaRecords.P_GA2_Priem, 12, visible: false));
			AddRecordType(new RecordTypeDB(PiramidaRecords.P_GA3_Priem, 12, visible: false));
			AddRecordType(new RecordTypeDB(PiramidaRecords.P_GA4_Priem, 12, visible: false));
			AddRecordType(new RecordTypeDB(PiramidaRecords.P_GA5_Priem, 12, visible: false));
			AddRecordType(new RecordTypeDB(PiramidaRecords.P_GA6_Priem, 12, visible: false));
			AddRecordType(new RecordTypeDB(PiramidaRecords.P_GA7_Priem, 12, visible: false));
			AddRecordType(new RecordTypeDB(PiramidaRecords.P_GA8_Priem, 12, visible: false));
			AddRecordType(new RecordTypeDB(PiramidaRecords.P_GA9_Priem, 12, visible: false));
			AddRecordType(new RecordTypeDB(PiramidaRecords.P_GA10_Priem, 12, visible: false));		

		}
	}
}

