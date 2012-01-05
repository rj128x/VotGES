using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data.Objects;
using System.Data.Common;

namespace VotGES.Piramida.PiramidaReport
{
	public enum IntervalReportEnum { minute, halfHour, hour, day, month, year }
	public enum ResultTypeEnum { min, max, avg, sum }
	public enum DBOperEnum { min, max, avg, sum }

	public abstract class RecordTypeBase
	{
		public string ID { get; set; }
		public ResultTypeEnum ResultType { get; set; }
		public Report Report { get; set; }
		public String Title { get; set; }
	}


	public class RecordTypeDB : RecordTypeBase
	{
		public double DefaultValue { get; set; }
		public double MinValue { get; set; }
		public double MaxValue { get; set; }
		public double DivParam { get; set; }
		public double MultParam { get; set; }
		public PiramidaRecord DBRecord { get; set; }
		public DBOperEnum DBOper { get; set; }
		public int ParNumber { get; set; }

		public RecordTypeDB
			(Report report, PiramidaRecord dbRecord, int parNumber,
			double minValue = -10e100, double maxValue = 10e100, double divParam = 1, double multParam = 1,
			string id = null, string title = null, ResultTypeEnum resultType = ResultTypeEnum.avg, DBOperEnum dbOper = DBOperEnum.avg) {
			Report = report;
			DBRecord = dbRecord;
			ParNumber = parNumber;
			MinValue = minValue;
			MaxValue = maxValue;
			DivParam = divParam;
			MultParam = multParam;
			ID = id == null ? dbRecord.Key : id;
			Title = title == null ? dbRecord.Title : title;
			ResultType = resultType;
			DBOper = dbOper;
		}
	}

	public delegate double RecordCalcDelegate(DateTime date);


	public class RecortTypeCalc : RecordTypeBase
	{
		public RecordCalcDelegate CalcFunction { get; set; }

		public RecortTypeCalc(Report report, string id, string title, RecordCalcDelegate calcFunction) {
			ID = id;
			Title = title;
			CalcFunction = calcFunction;
		}
	}

	public class PiramidaReportResultRecord
	{
		public Dictionary<ResultTypeEnum, double> Results { get; set; }
		public double ResultValue { get; set; }
	}

	public class Report
	{
		public DateTime DateStart { get; set; }
		public DateTime DateEnd { get; set; }
		public IntervalReportEnum Interval { get; set; }
		public Dictionary<string, RecordTypeBase> RecordTypes { get; set; }
		public SortedList<DateTime, Dictionary<string, double>> Data { get; set; }
		public List<DateTime> Dates { get; set; }


		public DateTime NextDate(DateTime Date) {
			switch (Interval) {
				case IntervalReportEnum.minute:
					return Date.AddMinutes(1);
				case IntervalReportEnum.halfHour:
					return Date.AddMinutes(30);
				case IntervalReportEnum.hour:
					return Date.AddHours(1);
				case IntervalReportEnum.day:
					return Date.AddDays(1);
				case IntervalReportEnum.month:
					return Date.AddMonths(1);
				case IntervalReportEnum.year:
					return Date.AddYears(1);
			}
			return Date;
		}


		public Report(DateTime dateStart, DateTime dateEnd, IntervalReportEnum interval) {
			DateStart = dateStart;
			DateEnd = dateEnd;
			Interval = interval;
			RecordTypes = new Dictionary<string, RecordTypeBase>();
			Data = new SortedList<DateTime, Dictionary<string, double>>();
			Dates = new List<DateTime>();

			DateTime date=NextDate(DateStart);
			while (date <= DateEnd) {
				Dates.Add(date);
				Data.Add(date, new Dictionary<string, double>());
				date = NextDate(date);
			}

		}

		public void ReadData() {
			foreach (RecordTypeBase recordType in RecordTypes.Values) {
				if (recordType is RecordTypeDB) {
					ReadDBData(recordType as RecordTypeDB);
				}
			}
			foreach (RecordTypeBase recordType in RecordTypes.Values) {
				if (recordType is RecortTypeCalc) {
					RecortTypeCalc rCalc=recordType as RecortTypeCalc;
					foreach (DateTime date in Dates) {
						double val=rCalc.CalcFunction(date);
						if (!Data[date].Keys.Contains(rCalc.ID)) {
							Data[date].Add(rCalc.ID, -1);
						}
						Data[date][rCalc.ID] = val;
					}
				}
			}
		}


		protected void ReadDBData(RecordTypeDB recordType) {
			Piramida3000Entities model=PiramidaAccess.getModel();
			DbCommand command= model.Connection.CreateCommand();
			DbParameter dateStartParam=command.CreateParameter();
			dateStartParam.ParameterName = "@dateStart";
			dateStartParam.Value = DateStart;
			dateStartParam.DbType = System.Data.DbType.DateTime;

			DbParameter dateEndParam=command.CreateParameter();
			dateEndParam.ParameterName = "@dateEnd";
			dateEndParam.Value = DateStart;
			dateEndParam.DbType = System.Data.DbType.DateTime;

			string valueParams=String.Format(" (DATA_DATE>@dateStart and DATA_DATE<=@dateEnd and ITEM={0} and OBJTYPE={1} and OBJECT={2} and PARNUMBER={3}) ",
				recordType.DBRecord.Item, recordType.DBRecord.ObjType, recordType.DBRecord.Obj, recordType.ParNumber);

			string commandText="";
			string valueOper=String.Format("{0}(Value0)", recordType.DBOper.ToString());
			string dt30="dateadd(minute,30,DATA_DATE)";
			string dateParam=
				String.Format(
				"datepart(year,{0}), datepart(month,{0}), datepart(day,{0}), datepart(hour,{0}), datepart(minute,{0})",
				dt30);

			switch (Interval) {
				case IntervalReportEnum.minute:
					dateParam =
						String.Format(
						"datepart(year,{0}), datepart(month,{0}), datepart(day,{0}), datepart(hour,{0}), datepart(minute,{0})",
						"DATA_DATE");
					commandText = String.Format("SELECT {0}, {1} from DATA WHERE {2} GROUP BY {0}",
						dateParam, valueOper, valueParams);
					command.CommandText = commandText;
					break;
				case IntervalReportEnum.halfHour:
					dateParam =
						String.Format(
						"datepart(year,{0}), datepart(month,{0}), datepart(day,{0}), datepart(hour,{0}), datepart(minute,{0})",
						"DATA_DATE");
					commandText = String.Format("SELECT {0}, {1} from DATA WHERE {2} GROUP BY {0}",
						dateParam, valueOper, valueParams, valueParams);
					break;
				case IntervalReportEnum.hour:
					dateParam =
						String.Format(
						"datepart(year,{0}), datepart(month,{0}), datepart(day,{0}), datepart(hour,{0})",
						dt30);
					dateParam = "dateadd(minute,(60-datepart(minute,DATA_DATE))%60,DATA_DATE)";
					commandText = String.Format("SELECT {0}, {1} from DATA WHERE {2} GROUP BY {0}",
						dateParam, valueOper, valueParams, valueParams);
					break;
				case IntervalReportEnum.day:
					dateParam =
						String.Format(
						"datepart(year,{0}), datepart(month,{0}), datepart(day,{0})",
						dt30);
					commandText = String.Format("SELECT {0}, {1} from DATA WHERE {2} GROUP BY {0}",
						dateParam, valueOper, valueParams, valueParams);
					break;
				case IntervalReportEnum.month:
					dateParam =
						String.Format(
						"datepart(year,{0}), datepart(month,{0})",
						dt30);
					commandText = String.Format("SELECT {0}, {1} from DATA WHERE {2} GROUP BY {0}",
						dateParam, valueOper, valueParams, valueParams);
					break;
				case IntervalReportEnum.year:
					dateParam =
						String.Format(
						"datepart(year,{0})",
						dt30);
					commandText = String.Format("SELECT {0}, {1} from DATA WHERE {2} GROUP BY {0}",
						dateParam, valueOper, valueParams, valueParams);
					break;
			}

			command.CommandText = commandText;
			DbDataReader reader=command.ExecuteReader();

			while (reader.Read()) {
				int year=-1;
				int month=-1;
				int day=-1;
				int hour=-1;
				int min=-1;
				double val=-1;
				DateTime date=DateTime.Now;
				bool ok=false;
				switch (Interval) {
					case IntervalReportEnum.minute:
						year = (int)reader[0];
						month = (int)reader[1];
						day = (int)reader[2];
						hour = (int)reader[3];
						min = (int)reader[4];
						val = (double)reader[5];
						date = new DateTime(year, month, day, hour, min, 0);
						ok = true;
						break;
					case IntervalReportEnum.halfHour:
						year = (int)reader[0];
						month = (int)reader[1];
						day = (int)reader[2];
						hour = (int)reader[3];
						min = (int)reader[4];
						val = (double)reader[5];
						date = new DateTime(year, month, day, hour, min, 0);
						ok = true;
						break;
					case IntervalReportEnum.hour:
						year = (int)reader[0];
						month = (int)reader[1];
						day = (int)reader[2];
						hour = (int)reader[3];
						val = (double)reader[4];
						date = new DateTime(year, month, day, hour, 0, 0);
						ok = true;
						break;
					case IntervalReportEnum.day:
						year = (int)reader[0];
						month = (int)reader[1];
						day = (int)reader[2];
						val = (double)reader[3];
						date = new DateTime(year, month, day, 0, 0, 0);
						ok = true;
						break;
					case IntervalReportEnum.month:
						year = (int)reader[0];
						month = (int)reader[1];
						val = (double)reader[2];
						date = new DateTime(year, month, 1, 0, 0, 0);
						ok = true;
						break;
					case IntervalReportEnum.year:
						year = (int)reader[0];
						val = (double)reader[1];
						date = new DateTime(year, 1, 1, 0, 0, 0);
						ok = true;
						break;
				}
				if (ok) {
					if (Data.Keys.Contains(date)) {
						if (!Data[date].Keys.Contains(recordType.ID)) {
							Data[date].Add(recordType.ID, -1);
						}
						Data[date][recordType.ID] = val * recordType.MultParam / recordType.DivParam;
					}
				}
			}
		}

		public void AddRecordType(RecordTypeBase type) {
			RecordTypes.Add(type.ID, type);
		}		
	}
}
