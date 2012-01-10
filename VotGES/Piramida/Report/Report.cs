using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data.Objects;
using System.Data.Common;
using System.Data.SqlClient;
using VotGES.Chart;

namespace VotGES.Piramida.Report
{
	public enum ReportTypeEnum { dayByMinutes, dayByHalfHours, dayByHours, monthByDays, monthByHalfHours, monthByHours, quarterByDays, yearByDays, yearByMonths, yearByQarters }
	public enum IntervalReportEnum { minute, halfHour, hour, day, month, quarter, year }
	public enum ResultTypeEnum { min, max, avg, sum }
	public enum DBOperEnum { min, max, avg, sum }

	public abstract class RecordTypeBase
	{
		public string ID { get; set; }
		public ResultTypeEnum ResultType { get; set; }
		public Report Parent { get; set; }
		public String Title { get; set; }
		public bool Visible { get; set; }
		public bool ToChart { get; set; }
		public string FormatDouble { get; set; }
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
			(PiramidaRecord dbRecord, int parNumber,
			double minValue = -10e100, double maxValue = 10e100, double divParam = 1, double multParam = 1,
			string id = null, string title = null, ResultTypeEnum resultType = ResultTypeEnum.sum, DBOperEnum dbOper = DBOperEnum.sum,
			bool toChart = false, bool visible = false, string formatDouble = "#,0.##") {
			DBRecord = dbRecord;
			ParNumber = parNumber;
			MinValue = minValue;
			MaxValue = maxValue;
			DivParam = divParam;
			MultParam = multParam;
			Visible = visible;
			ToChart = toChart;
			ID = id == null ? dbRecord.Key : id;
			Title = title == null ? dbRecord.Title : title;
			ResultType = resultType;
			DBOper = dbOper;
			FormatDouble = formatDouble;
		}
	}

	public delegate double RecordCalcDelegate(Report report, DateTime? date);


	public class RecordTypeCalc : RecordTypeBase
	{
		public RecordCalcDelegate CalcFunction { get; set; }

		public RecordTypeCalc(string id, string title, RecordCalcDelegate calcFunction,
			bool toChart = false, bool visible = false, ResultTypeEnum resultType = ResultTypeEnum.sum, string formatDouble = "#,0.##") {
			ID = id;
			Title = title;
			CalcFunction = calcFunction;
			Visible = visible;
			ToChart = toChart;
			FormatDouble = formatDouble;
			ResultType = resultType;
		}

		public RecordTypeCalc(RecordTypeCalc baseRecord, bool toChart = false, bool visible = false, ResultTypeEnum resultType = ResultTypeEnum.sum, string formatDouble = "#,0.##") {
			ID = baseRecord.ID;
			Title = baseRecord.Title;
			CalcFunction = baseRecord.CalcFunction;
			Visible = visible;
			ToChart = toChart;
			FormatDouble = formatDouble;
			ResultType = resultType;
		}
	}

	public class PiramidaReportResultRecord
	{
		public Dictionary<ResultTypeEnum, double> Results { get; set; }
		public double ResultValue { get; set; }
	}


	public class ReportAnswerRecord
	{
		public String Header { get; set; }
		public Dictionary<String, double> DataStr { get; set; }
	}

	public class ReportAnswer
	{
		public ChartAnswer Chart { get; set; }
		public List<ReportAnswerRecord> Data { get; set; }
		public Dictionary<string, string> Columns { get; set; }
		public Dictionary<string, string> Formats { get; set; }
	}



	public class Report
	{
		public DateTime DateStart { get; set; }
		public DateTime DateEnd { get; set; }
		public DateTime RealDateEnd { get; set; }
		public IntervalReportEnum Interval { get; set; }
		public Dictionary<string, RecordTypeBase> RecordTypes { get; set; }
		protected SortedList<DateTime, Dictionary<string, double>> Data { get; set; }
		public Dictionary<string, double> ResultData { get; set; }
		public SortedList<ResultTypeEnum, Dictionary<string, double>> ResultDataFull { get; set; }
		public List<string> NeedRecords { get; set; }
		public List<string> CurrentNeedRecords { get; set; }
		public List<DateTime> Dates { get; set; }
		public ReportAnswer Answer { get; set; }
		protected SqlConnection connection;

		public static IntervalReportEnum GetInterval(ReportTypeEnum ReportType) {
			switch (ReportType) {
				case ReportTypeEnum.dayByMinutes:
					return IntervalReportEnum.minute;
				case ReportTypeEnum.dayByHalfHours:
					return IntervalReportEnum.halfHour;
				case ReportTypeEnum.dayByHours:
					return IntervalReportEnum.hour;
				case ReportTypeEnum.monthByDays:
					return IntervalReportEnum.day;
				case ReportTypeEnum.monthByHalfHours:
					return IntervalReportEnum.halfHour;
				case ReportTypeEnum.monthByHours:
					return IntervalReportEnum.hour;
				case ReportTypeEnum.quarterByDays:
					return IntervalReportEnum.day;
				case ReportTypeEnum.yearByDays:
					return IntervalReportEnum.day;
				case ReportTypeEnum.yearByMonths:
					return IntervalReportEnum.month;
				case ReportTypeEnum.yearByQarters:
					return IntervalReportEnum.quarter;
			}
			return IntervalReportEnum.halfHour;
		}


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
				case IntervalReportEnum.quarter:
					return Date.AddMonths(3);
				case IntervalReportEnum.year:
					return Date.AddYears(1);
			}
			return Date;
		}

		public double this[DateTime? date, string key] {
			get {
				if (date != null && date.HasValue) {
					if (RecordTypes.Keys.Contains(key)) {
						return Data[date.Value][key];
					} else {
						Logger.Info("Ошибка в отчете. Ключ не найден " + key);
						return 0;
					}

				} else {
					if (!CurrentNeedRecords.Contains(key)) {
						CurrentNeedRecords.Add(key);
					}
					return 0;
				}
			}
		}

		public Report(DateTime dateStart, DateTime dateEnd, IntervalReportEnum interval) {
			DateStart = dateStart;
			DateEnd = dateEnd;
			RealDateEnd = dateStart;
			Interval = interval;
			RecordTypes = new Dictionary<string, RecordTypeBase>();
			Data = new SortedList<DateTime, Dictionary<string, double>>();
			NeedRecords = new List<string>();

			ResultData = new Dictionary<string, double>();
			ResultDataFull = new SortedList<ResultTypeEnum, Dictionary<string, double>>();
			ResultDataFull.Add(ResultTypeEnum.avg, new Dictionary<string, double>());
			ResultDataFull.Add(ResultTypeEnum.max, new Dictionary<string, double>());
			ResultDataFull.Add(ResultTypeEnum.min, new Dictionary<string, double>());
			ResultDataFull.Add(ResultTypeEnum.sum, new Dictionary<string, double>());
			Dates = new List<DateTime>();


			DateTime date=NextDate(DateStart);
			while (date <= DateEnd) {
				Dates.Add(date);
				Data.Add(date, new Dictionary<string, double>());
				date = NextDate(date);
			}



			Answer = new ReportAnswer();
		}

		protected void processNeedData(RecordTypeCalc rCalc) {
			CurrentNeedRecords = new List<string>();
			double d=rCalc.CalcFunction(this, null);
			List<string> current=CurrentNeedRecords.ToList();
			foreach (String key in current) {
				if (!NeedRecords.Contains(key)) {
					NeedRecords.Add(key);
				}
				RecordTypeBase recordType=RecordTypes[key];
				if (recordType is RecordTypeCalc) {
					RecordTypeCalc child=recordType as RecordTypeCalc;
					processNeedData(child);
				}
			}
		}

		public void InitNeedData() {
			NeedRecords.Clear();
			CurrentNeedRecords = new List<string>();
			foreach (RecordTypeBase recordType in RecordTypes.Values) {
				if (recordType.Visible || recordType.ToChart) {
					NeedRecords.Add(recordType.ID);
				}
			}

			foreach (RecordTypeBase recordType in RecordTypes.Values) {
				if (recordType is RecordTypeCalc && NeedRecords.Contains(recordType.ID)) {
					RecordTypeCalc rtc=recordType as RecordTypeCalc;
					processNeedData(rtc);
				}
			}

			List<String> keys=RecordTypes.Keys.ToList<string>();
			foreach (string key in keys) {
				if (!NeedRecords.Contains(key)) {
					RecordTypes.Remove(key);
				}
			}
		}

		public Dictionary<string, Dictionary<string, RecordTypeDB>> processDBData() {
			Dictionary<string, Dictionary<string,RecordTypeDB>> result=new Dictionary<string, Dictionary<string, RecordTypeDB>>();
			foreach (RecordTypeBase recordType in RecordTypes.Values) {
				if (recordType is RecordTypeDB) {
					RecordTypeDB rdb=recordType as RecordTypeDB;
					string key=String.Format("{0}-{1}-{2}-{3}", rdb.DBOper.ToString(), rdb.ParNumber, rdb.DBRecord.ObjType, rdb.DBRecord.Obj);
					if (!result.Keys.Contains(key)) {
						result.Add(key, new Dictionary<string, RecordTypeDB>());
					}
					result[key].Add(rdb.ID, rdb);
				}
			}
			return result;
		}


		public virtual void ReadData() {
			InitNeedData();

			Dictionary<string, Dictionary<string,RecordTypeDB>> DBRecords=processDBData();

			foreach (KeyValuePair<string,Dictionary<string,RecordTypeDB>> de in DBRecords) {
				ReadDBData(de.Key, de.Value);
			}
			checkDBData();
			calculateData();
			calcResult();
		}


		protected void calculateData() {
			Dictionary<string,List<string>> NeedDataForRecords=new Dictionary<string, List<string>>();
			Dictionary<string,bool> calculatedData=new Dictionary<string, bool>();
			foreach (RecordTypeBase recordType in RecordTypes.Values) {
				calculatedData.Add(recordType.ID, false);
				if (recordType is RecordTypeCalc) {
					CurrentNeedRecords = new List<string>();
					RecordTypeCalc rCalc=recordType as RecordTypeCalc;
					double d=rCalc.CalcFunction(this, null);
					NeedDataForRecords.Add(rCalc.ID, CurrentNeedRecords);
				}
				if (recordType is RecordTypeDB) {
					calculatedData[recordType.ID] = true;
				}
			}

			bool finish=true;
			do {
				finish = true;
				foreach (RecordTypeBase recordType in RecordTypes.Values) {
					if (calculatedData[recordType.ID])
						continue;
					if (recordType is RecordTypeCalc) {
						RecordTypeCalc rCalc=recordType as RecordTypeCalc;
						bool allReady=true;
						foreach (string key in NeedDataForRecords[rCalc.ID]) {
							allReady = allReady && calculatedData[key];
						}
						if (allReady) {
							foreach (DateTime date in Dates) {
								double val=rCalc.CalcFunction(this, date);
								if (!Data[date].Keys.Contains(rCalc.ID)) {
									Data[date].Add(rCalc.ID, -1);
								}
								Data[date][rCalc.ID] = val;
							}
							calculatedData[rCalc.ID] = true;
						} else {
							finish = false;
						}
					}
				}
			} while (!finish);

		}


		protected virtual void calcResult() {
			foreach (RecordTypeBase recordType in RecordTypes.Values) {
				double min=10e100;
				double max=-10e100;
				double sum=0;
				double count=0;
				foreach (DateTime date in Dates) {
					double val=Data[date][recordType.ID];
					min = min < val ? min : val;
					max = max > val ? max : val;
					sum += val;
					count++;
				}
				double avg=count > 0 ? sum / count : 0;
				ResultDataFull[ResultTypeEnum.avg].Add(recordType.ID, avg);
				ResultDataFull[ResultTypeEnum.max].Add(recordType.ID, max);
				ResultDataFull[ResultTypeEnum.min].Add(recordType.ID, min);
				ResultDataFull[ResultTypeEnum.sum].Add(recordType.ID, sum);
				ResultData.Add(recordType.ID, ResultDataFull[recordType.ResultType][recordType.ID]);
			}
		}


		protected void checkDBData() {
			List<DateTime> forRemove=new List<DateTime>();
			foreach (DateTime date in Dates) {
				if (date > RealDateEnd) {
					forRemove.Add(date);
				}  else{
					foreach (RecordTypeBase recordType in RecordTypes.Values) {
						if (recordType is RecordTypeDB) {
							RecordTypeDB rdb=recordType as RecordTypeDB;
							if (!Data[date].Keys.Contains(rdb.ID)) {
								Data[date].Add(rdb.ID, rdb.DefaultValue);
							}
						}
					}
				}
			}
			foreach (DateTime date in forRemove) {
				Dates.Remove(date);
				Data.Remove(date);
			}
		}

		protected void ReadDBData(string paramsKey, Dictionary<string, RecordTypeDB> records) {
			string[]paramsArr=paramsKey.Split('-');
			string dbOper=paramsArr[0];
			string parNumber=paramsArr[1];
			string objType=paramsArr[2];
			string obj=paramsArr[3];

			connection = PiramidaAccess.getConnection();
			connection.Open();
			SqlDataReader reader=null; SqlCommand command=null;
			try {
				List<int> items=new List<int>();
				Dictionary<int,RecordTypeDB> recordsDict=new Dictionary<int, RecordTypeDB>();
				foreach (RecordTypeDB rdb in records.Values) {
					items.Add(rdb.DBRecord.Item);
					recordsDict.Add(rdb.DBRecord.Item, rdb);
				}
				string itemsStr=String.Join(",", items);

				command = connection.CreateCommand();
				command.Parameters.AddWithValue("@dateStart", DateStart);
				command.Parameters.AddWithValue("@dateEnd", DateEnd);

				string valueParams=String.Format(" (DATA_DATE>@dateStart and DATA_DATE<=@dateEnd and PARNUMBER={3} and OBJTYPE={1} and OBJECT={2} and ITEM in ({0})) ",
					itemsStr, objType, obj, parNumber);

				string commandText="";
				string valueOper=String.Format("{0}(Value0)", dbOper);
				string dt30="dateadd(minute,-30,DATA_DATE)";
				string dateParam=
				String.Format(
					"ITEM, datepart(year,{0}), datepart(month,{0}), datepart(day,{0}), datepart(hour,{0}), datepart(minute,{0})",
					dt30);

				switch (Interval) {
					case IntervalReportEnum.minute:
						dateParam =
							String.Format(
							"ITEM, datepart(year,{0}), datepart(month,{0}), datepart(day,{0}), datepart(hour,{0}), datepart(minute,{0})",
							"DATA_DATE");
						commandText = String.Format("SELECT  {0}, {1} from DATA  WHERE {2} GROUP BY {0}",
							dateParam, valueOper, valueParams);
						command.CommandText = commandText;
						break;
					case IntervalReportEnum.halfHour:
						dateParam =
							String.Format(
							"ITEM, datepart(year,{0}), datepart(month,{0}), datepart(day,{0}), datepart(hour,{0}), datepart(minute,{0})",
							"DATA_DATE");
						commandText = String.Format("SELECT {0}, {1} from [dbo].DATA  WHERE {2} GROUP BY {0}",
							dateParam, valueOper, valueParams, valueParams);
						break;
					case IntervalReportEnum.hour:
						dateParam =
							String.Format(
							"ITEM, datepart(year,{0}), datepart(month,{0}), datepart(day,{0}), datepart(hour,{0})",
							dt30);
						commandText = String.Format("SELECT {0}, {1} from DATA  WHERE {2} GROUP BY {0}",
							dateParam, valueOper, valueParams, valueParams);
						break;
					case IntervalReportEnum.day:
						dateParam =
							String.Format(
							"ITEM, datepart(year,{0}), datepart(month,{0}), datepart(day,{0})",
							dt30);
						commandText = String.Format("SELECT  {0}, {1} from DATA  WHERE {2} GROUP BY {0}",
							dateParam, valueOper, valueParams, valueParams);
						break;
					case IntervalReportEnum.quarter:
						dateParam =
							String.Format(
							"ITEM, datepart(year,{0}), datepart(quarter,{0})",
							dt30);
						commandText = String.Format("SELECT {0}, {1} from DATA d WHERE {2} GROUP BY {0}",
							dateParam, valueOper, valueParams, valueParams);
						break;
					case IntervalReportEnum.month:
						dateParam =
							String.Format(
							"ITEM, datepart(year,{0}), datepart(month,{0})",
							dt30);
						commandText = String.Format("SELECT {0}, {1} from DATA d WHERE {2} GROUP BY {0}",
							dateParam, valueOper, valueParams, valueParams);
						break;
					case IntervalReportEnum.year:
						dateParam =
							String.Format(
							"ITEM, datepart(year,{0})",
							dt30);
						commandText = String.Format("SELECT {0}, {1} from DATA  WHERE {2} GROUP BY {0}",
							dateParam, valueOper, valueParams, valueParams);
						break;
				}

				command.CommandText = commandText;
				//Logger.Info(commandText.Replace("@dateStart", String.Format("'{0}'", DateStart)).Replace("@dateEnd", String.Format("'{0}'", DateEnd)));

				reader = command.ExecuteReader();
				DateTime lastDate=DateEnd;

				while (reader.Read()) {
					int year=-1;
					int month=-1;
					int day=-1;
					int hour=-1;
					int min=-1;
					int quarter=-1;
					double val=-1;
					DateTime date=DateTime.Now;
					bool ok=false;
					int item=(int)reader[0];
					RecordTypeDB record=recordsDict[item];

					switch (Interval) {
						case IntervalReportEnum.minute:
							year = (int)reader[1];
							month = (int)reader[2];
							day = (int)reader[3];
							hour = (int)reader[4];
							min = (int)reader[5];
							val = (double)reader[6];
							date = new DateTime(year, month, day, hour, min, 0);
							ok = true;
							break;
						case IntervalReportEnum.halfHour:
							year = (int)reader[1];
							month = (int)reader[2];
							day = (int)reader[3];
							hour = (int)reader[4];
							min = (int)reader[5];
							val = (double)reader[6];
							date = new DateTime(year, month, day, hour, min, 0);
							ok = true;
							break;
						case IntervalReportEnum.hour:
							year = (int)reader[1];
							month = (int)reader[2];
							day = (int)reader[3];
							hour = (int)reader[4];
							val = (double)reader[5];
							date = new DateTime(year, month, day, hour, 0, 0).AddHours(1); ;
							ok = true;
							break;
						case IntervalReportEnum.day:
							year = (int)reader[1];
							month = (int)reader[2];
							day = (int)reader[3];
							val = (double)reader[4];
							date = new DateTime(year, month, day, 0, 0, 0).AddDays(1);
							ok = true;
							break;
						case IntervalReportEnum.month:
							year = (int)reader[1];
							month = (int)reader[2];
							val = (double)reader[3];
							date = new DateTime(year, month, 1, 0, 0, 0).AddMonths(1);
							ok = true;
							break;
						case IntervalReportEnum.quarter:
							year = (int)reader[1];
							quarter = (int)reader[2];
							val = (double)reader[3];
							date = new DateTime(year, 3 * (quarter - 1) + 1, 1, 0, 0, 0).AddMonths(3);
							ok = true;
							break;
						case IntervalReportEnum.year:
							year = (int)reader[1];
							val = (double)reader[2];
							date = new DateTime(year, 1, 1, 0, 0, 0).AddYears(1);
							ok = true;
							break;

					}
					if (ok) {
						lastDate = date;

						if (Data.Keys.Contains(date)) {
							if (!Data[date].Keys.Contains(record.ID)) {
								Data[date].Add(record.ID, -1);
							}
							Data[date][record.ID] = val * record.MultParam / record.DivParam;
						}
						RealDateEnd = lastDate > RealDateEnd ? lastDate : RealDateEnd;
					}
				}
			} finally {
				try { reader.Close(); } catch { }
				try { command.Dispose(); } catch { }
				try { connection.Close(); } catch { }
			}
		}


		public virtual void CreateAnswerData(bool createResult = true) {
			Answer.Data = new List<ReportAnswerRecord>();
			Answer.Columns = new Dictionary<string, string>();
			Answer.Formats = new Dictionary<string, string>();

			if (createResult) {
				ReportAnswerRecord recordResult=new ReportAnswerRecord();
				recordResult.Header = "Итог";
				recordResult.DataStr = new Dictionary<string, double>();
				foreach (RecordTypeBase recordType in RecordTypes.Values) {
					if (recordType.Visible) {
						recordResult.DataStr.Add(recordType.ID, ResultData[recordType.ID]);
					}
				}
				Answer.Data.Add(recordResult);
			}

			foreach (RecordTypeBase recordType in RecordTypes.Values) {
				if (recordType.Visible) {
					if (!Answer.Columns.Keys.Contains(recordType.ID)) {
						Answer.Columns.Add(recordType.ID, recordType.Title);
						Answer.Formats.Add(recordType.ID, recordType.FormatDouble);
					}
				}
			}

			foreach (DateTime date in Dates) {
				ReportAnswerRecord record=new ReportAnswerRecord();
				record.Header = GetCorrectedDateForTable(date).ToString(getDateFormat());
				record.DataStr = new Dictionary<string, double>();
				foreach (RecordTypeBase recordType in RecordTypes.Values) {
					if (recordType.Visible) {
						record.DataStr.Add(recordType.ID, Data[date][recordType.ID]);
					}
				}
				Answer.Data.Add(record);
			}
		}


		public virtual void CreateChart() {
			Answer.Chart = new ChartAnswer();
			Answer.Chart.Properties = new ChartProperties();
			Answer.Chart.Data = new ChartData();

			ChartAxisProperties ax=new ChartAxisProperties();
			ax.Auto = true;
			ax.Index = 0;

			ChartAxisProperties ax1=new ChartAxisProperties();
			ax1.Auto = true;
			ax1.Index = 1;

			Answer.Chart.Properties.addAxis(ax);
			Answer.Chart.Properties.addAxis(ax1);

			ChartSerieType type=ChartSerieType.stepLine;
			if (Interval == IntervalReportEnum.quarter || Interval == IntervalReportEnum.month) {
				type = ChartSerieType.column;
			}

			Answer.Chart.Properties.XAxisType = XAxisTypeEnum.datetime;

			Answer.Chart.Properties.XValueFormatString = getDateFormat();

			Random r=new Random();
			int indexColor=0;
			foreach (RecordTypeBase recordType in RecordTypes.Values) {
				if (recordType.ToChart) {
					ChartSerieProperties props=new ChartSerieProperties();
					props.Title = recordType.Title;
					props.TagName = recordType.ID;
					props.LineWidth = 2;
					props.Color = ChartColor.GetColorStr(indexColor++);
					props.SerieType = type;
					props.YAxisIndex = 0;
					Answer.Chart.Properties.addSerie(props);

					ChartDataSerie data=new ChartDataSerie();
					data.Name = recordType.ID;
					foreach (DateTime date in Dates) {
						DateTime dt=GetCorrectedDateForChart(date);
						data.Points.Add(new ChartDataPoint(dt, Data[date][recordType.ID]));
					}
					Answer.Chart.Data.addSerie(data);
				}
			}
		}

		protected String getDateFormat() {
			switch (Interval) {
				case IntervalReportEnum.minute:
					return "HH:mm";
				case IntervalReportEnum.halfHour:
				case IntervalReportEnum.hour:
					return "dd.MM HH:mm";
				case IntervalReportEnum.day:
					return "dd.MM";
				case IntervalReportEnum.month:
				case IntervalReportEnum.quarter:
					return "MMMM yy";
				case IntervalReportEnum.year:
					return "yyyy";
			}
			return "dd.MM.yy HH:mm";
		}

		protected DateTime GetCorrectedDateForTable(DateTime date) {
			DateTime dt=date;
			switch (Interval) {
				case IntervalReportEnum.minute:
					dt = date.AddMinutes(0);
					break;
				case IntervalReportEnum.halfHour:
					dt = date.AddMinutes(0);
					break;
				case IntervalReportEnum.hour:
					dt = date.AddHours(0);
					break;
				case IntervalReportEnum.day:
					dt = date.AddDays(-1);
					break;
				case IntervalReportEnum.month:
					dt = date.AddMonths(-1);
					break;
				case IntervalReportEnum.quarter:
					dt = date.AddMonths(-3);
					break;
				case IntervalReportEnum.year:
					dt = date.AddYears(-1);
					break;
			}
			return dt;
		}

		protected DateTime GetCorrectedDateForChart(DateTime date) {
			DateTime dt=date;
			switch (Interval) {
				case IntervalReportEnum.minute:
					dt = date.AddMinutes(-1);
					break;
				case IntervalReportEnum.halfHour:
					dt = date.AddMinutes(-30);
					break;
				case IntervalReportEnum.hour:
					dt = date.AddHours(-1);
					break;
				case IntervalReportEnum.day:
					dt = date.AddDays(-1);
					break;
				case IntervalReportEnum.month:
					dt = date.AddMonths(-1);
					break;
				case IntervalReportEnum.quarter:
					dt = date.AddMonths(-3);
					break;
				case IntervalReportEnum.year:
					dt = date.AddYears(-1);
					break;
			}
			return dt;
		}


		public void AddRecordType(RecordTypeBase type) {
			if (!RecordTypes.Keys.Contains(type.ID)) {
				RecordTypes.Add(type.ID, type);
			}
		}

	}
}
