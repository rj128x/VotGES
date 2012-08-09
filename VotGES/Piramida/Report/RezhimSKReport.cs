using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace VotGES.Piramida.Report
{
	public class RezhimSKReport : Report
	{
		public RezhimSKReport(DateTime dateStart, DateTime dateEnd, IntervalReportEnum interval) :
			base(dateStart, dateEnd, interval) {
			int pn=4;
			ReportGARecords.AddPRecordsGAP(this, pn, 1, 1, false, false,DBOperEnum.avg, ResultTypeEnum.sum);
			ReportGARecords.AddPRecordsGAQ(this, pn, 1, 1, false, false, DBOperEnum.avg, ResultTypeEnum.sum);
			ReportGARecords.CreateGAP();
			ReportGARecords.CreateGAQ();

			AddRecordType(new RecordTypeCalc(ReportGARecords.P_GA1, toChart: true, visible: true));
			AddRecordType(new RecordTypeCalc(ReportGARecords.P_GA2, toChart: true, visible: true));
			AddRecordType(new RecordTypeCalc(ReportGARecords.P_GA9, toChart: true, visible: true));
			AddRecordType(new RecordTypeCalc(ReportGARecords.P_GA10, toChart: true, visible: true));

			AddRecordType(new RecordTypeCalc(ReportGARecords.Q_GA1, toChart: true, visible: true));
			AddRecordType(new RecordTypeCalc(ReportGARecords.Q_GA2, toChart: true, visible: true));
			AddRecordType(new RecordTypeCalc(ReportGARecords.Q_GA9, toChart: true, visible: true));
			AddRecordType(new RecordTypeCalc(ReportGARecords.Q_GA10, toChart: true, visible: true));
		}


		public override void ReadData() {
			base.ReadData();
		}

	}
}
