using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace VotGES.Piramida.Report
{
	public class FullReport:Report
	{
		public FullReport(DateTime dateStart, DateTime dateEnd, IntervalReportEnum interval):
			base(dateStart,dateEnd,interval)
		{
			int parNumber=12;
			int scaleDiv=2;
			ResultTypeEnum result=ResultTypeEnum.sum;
			DBOperEnum oper=DBOperEnum.sum;
			if (interval == IntervalReportEnum.minute) {
				parNumber = 4;
				oper = DBOperEnum.avg;
				result = ResultTypeEnum.sum;
				scaleDiv = 1;
			}

			ReportGARecords.AddPRecordsGAP(this, parNumber, 1, scaleDiv, false, false,oper,result);
			ReportGARecords.AddPRecordsGAQ(this, parNumber, 1, scaleDiv, false, false,oper,result);
			ReportGARecords.AddPRecordsGAAdd(this, parNumber, 1, scaleDiv, false, false, oper,result);
			ReportGARecords.AddCalcRecords(this, false, false, result);

			ReportLinesRecords.AddLineRecordsP(this, parNumber, 1, scaleDiv, false, false, oper, result);
			ReportLinesRecords.AddCalcRecords(this, false, false, result);

			ReportGlTransformRecords.AddGLTransformRecordsP(this, parNumber, 1, scaleDiv, false, false, oper, result);
			ReportGlTransformRecords.AddPRecordsForNebalans(this, parNumber, 1, scaleDiv, false, false, oper, result);
			ReportGlTransformRecords.AddCalcRecords(this, false, false, result);

			ReportSNRecords.AddPRecordsSN(this, parNumber, 1, scaleDiv, false, false, oper, result);
			ReportSNRecords.AddCalcRecords(this, false, false, result);

			ReportWaterRecords.AddPRecordsWater(this, parNumber, 1, 1, false, false, DBOperEnum.avg, ResultTypeEnum.avg);

			ReportMainRecords.AddPRecords(this, parNumber, 1, scaleDiv, false, false, oper, result);
			ReportMainRecords.AddCalcRecords(this, false, false, result);

		}

		public void InitNeedData(List<String> selected) {
			
			foreach (String key in selected) {
				RecordTypes[key].Visible = true;
				RecordTypes[key].ToChart = true;
			}
		}

	}
}
