using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using VotGES.Piramida;

namespace VotGES.Reports
{
	public class RezhimSKReportRecord : ReportRecord
	{
		public double GA1P { get; set; }
		public double GA2P { get; set; }
		public double GA9P { get; set; }
		public double GA10P { get; set; }

		public double GA1Q { get; set; }
		public double GA2Q { get; set; }
		public double GA9Q { get; set; }
		public double GA10Q { get; set; }

		public RezhimSKReportRecord() {
			GA1P = 0; GA2P = 0; GA9P = 0; GA10P = 0;
			GA1Q = 0; GA2Q = 0; GA9Q = 0; GA10Q = 0;
		}
	}
	public class RezhimSKReport : Report
	{
		protected override void ReadRecord(PiramidaRecord record) {
			IQueryable<DATA> dataArr=
				from DATA d in Model.DATA
				where
					d.DATA_DATE > DateStart && d.DATA_DATE <= DateEnd &&
					d.ITEM == record.Item && d.OBJECT == record.Obj && d.OBJTYPE == record.ObjType &&
					d.PARNUMBER == 4 
				select d;
			processEntities(dataArr);
		}

		public override void ReadData() {
			ReadRecord(PiramidaRecords.P_GA1_Otd);
			ReadRecord(PiramidaRecords.P_GA2_Otd);
			ReadRecord(PiramidaRecords.P_GA9_Otd);
			ReadRecord(PiramidaRecords.P_GA10_Otd);
			ReadRecord(PiramidaRecords.P_GA1_Priem);
			ReadRecord(PiramidaRecords.P_GA2_Priem);
			ReadRecord(PiramidaRecords.P_GA9_Priem);
			ReadRecord(PiramidaRecords.P_GA10_Priem);

			ReadRecord(PiramidaRecords.Q_GA1_Otd);
			ReadRecord(PiramidaRecords.Q_GA2_Otd);
			ReadRecord(PiramidaRecords.Q_GA9_Otd);
			ReadRecord(PiramidaRecords.Q_GA10_Otd);
			ReadRecord(PiramidaRecords.Q_GA1_Priem);
			ReadRecord(PiramidaRecords.Q_GA2_Priem);
			ReadRecord(PiramidaRecords.Q_GA9_Priem);
			ReadRecord(PiramidaRecords.Q_GA10_Priem);
		}

		protected override void ProcessPiramidaEntity(DATA data) {
			base.ProcessPiramidaEntity(data);
			RezhimSKReportRecord record=Values[data.DATA_DATE] as RezhimSKReportRecord;
			string key=PiramidaRecord.GetKey(data);
			if (key == PiramidaRecords.P_GA1_Otd.Key) {	record.GA1P += data.VALUE0.Value;}
			if (key == PiramidaRecords.P_GA2_Otd.Key) { record.GA2P += data.VALUE0.Value; }
			if (key == PiramidaRecords.P_GA9_Otd.Key) { record.GA9P += data.VALUE0.Value; }
			if (key == PiramidaRecords.P_GA10_Otd.Key) { record.GA10P += data.VALUE0.Value; }
			if (key == PiramidaRecords.P_GA1_Priem.Key) { record.GA1P -= data.VALUE0.Value; }
			if (key == PiramidaRecords.P_GA2_Priem.Key) { record.GA2P -= data.VALUE0.Value; }
			if (key == PiramidaRecords.P_GA9_Priem.Key) { record.GA9P -= data.VALUE0.Value; }
			if (key == PiramidaRecords.P_GA10_Priem.Key) { record.GA10P -= data.VALUE0.Value; }

			if (key == PiramidaRecords.Q_GA1_Otd.Key) { record.GA1Q += data.VALUE0.Value; }
			if (key == PiramidaRecords.Q_GA2_Otd.Key) { record.GA2Q += data.VALUE0.Value; }
			if (key == PiramidaRecords.Q_GA9_Otd.Key) { record.GA9Q += data.VALUE0.Value; }
			if (key == PiramidaRecords.Q_GA10_Otd.Key) { record.GA10Q += data.VALUE0.Value; }
			if (key == PiramidaRecords.Q_GA1_Priem.Key) { record.GA1Q -= data.VALUE0.Value; }
			if (key == PiramidaRecords.Q_GA2_Priem.Key) { record.GA2Q -= data.VALUE0.Value; }
			if (key == PiramidaRecords.Q_GA9_Priem.Key) { record.GA9Q -= data.VALUE0.Value; }
			if (key == PiramidaRecords.Q_GA10_Priem.Key) { record.GA10Q -= data.VALUE0.Value; }
		}

		protected override ReportRecord createReportRecord(DateTime date) {
			RezhimSKReportRecord record=new RezhimSKReportRecord();
			record.Date = date;
			return record;
		}

	}
}
