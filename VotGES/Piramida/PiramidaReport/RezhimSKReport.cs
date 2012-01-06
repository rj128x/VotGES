using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace VotGES.Piramida.PiramidaReport
{
	public class RezhimSKReport : Report
	{
		public RezhimSKReport(DateTime dateStart, DateTime dateEnd, IntervalReportEnum interval) :
			base(dateStart, dateEnd, interval) {
				int pn=12;
				AddRecordType(new RecordTypeDB(PiramidaRecords.P_GA1_Otd, pn, id: "p_ga1_Otd", visible: false, toChart: false));
				AddRecordType(new RecordTypeDB(PiramidaRecords.P_GA2_Otd, pn, id: "p_ga2_Otd", visible: false, toChart: false));
				AddRecordType(new RecordTypeDB(PiramidaRecords.P_GA9_Otd, pn, id: "p_ga9_Otd", visible: false, toChart: false));
				AddRecordType(new RecordTypeDB(PiramidaRecords.P_GA10_Otd, pn, id: "p_ga10_Otd", visible: false, toChart: false));
				AddRecordType(new RecordTypeDB(PiramidaRecords.Q_GA1_Otd, pn, id: "q_ga1_Otd", visible: false, toChart: false));
				AddRecordType(new RecordTypeDB(PiramidaRecords.Q_GA2_Otd, pn, id: "q_ga2_Otd", visible: false, toChart: false));
				AddRecordType(new RecordTypeDB(PiramidaRecords.Q_GA9_Otd, pn, id: "q_ga9_Otd", visible: false, toChart: false));
				AddRecordType(new RecordTypeDB(PiramidaRecords.Q_GA10_Otd, pn, id: "q_ga10_Otd", visible: false, toChart: false));

				AddRecordType(new RecordTypeDB(PiramidaRecords.P_GA1_Priem, pn, id: "p_ga1_Priem", visible: false, toChart: false));
				AddRecordType(new RecordTypeDB(PiramidaRecords.P_GA2_Priem, pn, id: "p_ga2_Priem", visible: false, toChart: false));
				AddRecordType(new RecordTypeDB(PiramidaRecords.P_GA9_Priem, pn, id: "p_ga9_Priem", visible: false, toChart: false));
				AddRecordType(new RecordTypeDB(PiramidaRecords.P_GA10_Priem, pn, id: "p_ga10_Priem", visible: false, toChart: false));
				AddRecordType(new RecordTypeDB(PiramidaRecords.Q_GA1_Priem, pn, id: "q_ga1_Priem", visible: false, toChart: false));
				AddRecordType(new RecordTypeDB(PiramidaRecords.Q_GA2_Priem, pn, id: "q_ga2_Priem", visible: false, toChart: false));
				AddRecordType(new RecordTypeDB(PiramidaRecords.Q_GA9_Priem, pn, id: "q_ga9_Priem", visible: false, toChart: false));
				AddRecordType(new RecordTypeDB(PiramidaRecords.Q_GA10_Priem, pn, id: "q_ga10_Priem", visible: false, toChart: false));


			AddRecordType(new RecordTypeCalc("p_ga1", "ГА-1 P", visible:true, toChart:true, 
					calcFunction: new RecordCalcDelegate((report,date) => {
						return Data[date]["p_ga1_Otd"] - Data[date]["p_ga1_Priem"];
					})
				));

			AddRecordType(new RecordTypeCalc("p_ga2", "ГА-2 P", visible: true, toChart: true,
					calcFunction: new RecordCalcDelegate((report, date) => {
						return Data[date]["p_ga2_Otd"] - Data[date]["p_ga2_Priem"];
					})
				));

			AddRecordType(new RecordTypeCalc("p_ga9", "ГА-9 P", visible: true, toChart: true,
					calcFunction: new RecordCalcDelegate((report, date) => {
						return Data[date]["p_ga9_Otd"] - Data[date]["p_ga9_Priem"];
					})
				));

			AddRecordType(new RecordTypeCalc("p_ga10", "ГА-10 P", visible: true, toChart: true,
					calcFunction: new RecordCalcDelegate((report, date) => {
						return Data[date]["p_ga10_Otd"] - Data[date]["p_ga10_Priem"];
					})
				));

			AddRecordType(new RecordTypeCalc("q_ga1", "ГА-1 Q", visible: true, toChart: true,
					calcFunction: new RecordCalcDelegate((report, date) => {
						return Data[date]["q_ga1_Otd"] - Data[date]["q_ga1_Priem"];
					})
				));

			AddRecordType(new RecordTypeCalc("q_ga2", "ГА-2 Q", visible: true, toChart: true,
					calcFunction: new RecordCalcDelegate((report, date) => {
						return Data[date]["q_ga2_Otd"] - Data[date]["q_ga2_Priem"];
					})
				));

			AddRecordType(new RecordTypeCalc("q_ga9", "ГА-9 Q", visible: true, toChart: true,
					calcFunction: new RecordCalcDelegate((report, date) => {
						return Data[date]["q_ga9_Otd"] - Data[date]["q_ga9_Priem"];
					})
				));

			AddRecordType(new RecordTypeCalc("q_ga10", "ГА-10 Q", visible: true, toChart: true,
					calcFunction: new RecordCalcDelegate((report, date) => {
						return Data[date]["q_ga10_Otd"] - Data[date]["q_ga10_Priem"];
					})
				));

		}

		public override void ReadData() {
			base.ReadData();
		}

	}
}
