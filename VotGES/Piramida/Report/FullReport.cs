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
			ReportGARecords.CreateGAP();
			ReportGARecords.CreateGAQ();
			ReportGARecords.AddCalcRecords(this, false, false, result);

			ReportLinesRecords.AddLineRecordsP(this, parNumber, 1, scaleDiv, false, false, oper, result);
			ReportLinesRecords.CreateLinesP();
			ReportLinesRecords.AddCalcRecords(this, false, false, result);

			ReportGlTransformRecords.AddGLTransformRecordsP(this, parNumber, 1, scaleDiv, false, false, oper, result);
			ReportGlTransformRecords.AddPRecordsForNebalans(this, parNumber, 1, scaleDiv, false, false, oper, result);
			ReportGlTransformRecords.CreateGlTransformP();
			ReportGlTransformRecords.AddCalcRecords(this, false, false, result);

			ReportSNRecords.AddPRecordsSN(this, parNumber, 1, scaleDiv, false, false, oper, result);
			ReportSNRecords.CreateSNP();
			ReportSNRecords.AddCalcRecords(this, false, false, result);
		}

		public void InitNeedData(List<String> selected) {
			NeedRecords.Clear();
			foreach (String key in selected) {
				NeedRecords.Add(key);
			}

			foreach (RecordTypeBase recordType in RecordTypes.Values) {
				if (recordType is RecordTypeCalc && NeedRecords.Contains(recordType.ID)) {
					RecordTypeCalc rtc=recordType as RecordTypeCalc;					
					double d=rtc.CalcFunction(this, null);
				}
			}

			List<String> keys=RecordTypes.Keys.ToList<string>();
			foreach (string key in keys) {
				if (!NeedRecords.Contains(key)) {
					RecordTypes.Remove(key);
				} else {
					if (selected.Contains(key)){
						RecordTypes[key].Visible = true;
						RecordTypes[key].ToChart = true;
					}
				}
			}
		}

	}
}
