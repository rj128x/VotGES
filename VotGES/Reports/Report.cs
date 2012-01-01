using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using VotGES.Piramida;

namespace VotGES.Reports
{
	public class ReportRecord
	{
		public DateTime Date { get; set; }
	}

	public class Report
	{
		public DateTime DateStart { get;  set; }
		public DateTime DateEnd { get; set; }
		public Piramida3000Entities Model { get; protected set; }

		public SortedList<DateTime, ReportRecord> Values { get; protected set; }

		public Report(){
			Values=new SortedList<DateTime,ReportRecord>();
			Model = PiramidaAccess.getModel();
		}

		public Report(DateTime dateStart, DateTime dateEnd):base() {
			DateStart = dateStart;
			DateEnd = dateEnd;
		}

		public virtual void ReadData(){
		}


		protected virtual void ReadRecord(PiramidaRecord  record) {
		}

		protected virtual void processEntities(IQueryable<DATA> dataArr) {
			foreach (DATA data in dataArr) {
				ProcessPiramidaEntity(data);
			}
		}

		protected virtual void ProcessPiramidaEntity(DATA data) {
			DateTime date=data.DATA_DATE;
			if (date < DateStart || date > DateEnd)
				return;
			if (!Values.Keys.Contains(date)) {
				Values.Add(date, createReportRecord(date));
			}
			ReportRecord record=Values[date];
		}

		protected virtual ReportRecord createReportRecord(DateTime date) {
			ReportRecord record=new ReportRecord();
			record.Date = date;
			return record;
		}

		public List<ReportRecord> getReportData() {
			return Values.Values.ToList<ReportRecord>();
		}

	}

}
