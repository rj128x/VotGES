using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data.Objects;
using System.Data.Common;
using System.Data.SqlClient;
using VotGES.Chart;

namespace VotGES.Piramida.PiramidaReport
{
	public enum IntervalReportEnum { minute, halfHour, hour, day, month, year }
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
			bool toChart=false, bool visible=false, string formatDouble="### ### ### ##0.##") {
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
			bool toChart = false, bool visible = false, string formatDouble = "### ### ### ##0.##") {
			ID = id;
			Title = title;
			CalcFunction = calcFunction;
			Visible = visible;
			ToChart = toChart;
			FormatDouble = formatDouble;
		}

		public RecordTypeCalc(RecordTypeCalc baseRecord, bool toChart = false, bool visible = false, string formatDouble = "### ### ### ##0.##") {
			ID = baseRecord.ID;
			Title = baseRecord.Title;
			CalcFunction = baseRecord.CalcFunction;
			Visible = visible;
			ToChart = toChart;
			FormatDouble = formatDouble;
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
		public Dictionary<String, String> DataStr { get; set; }		
	}

	public class ReportAnswer
	{
		public ChartAnswer Chart { get; set; }
		public List<ReportAnswerRecord> Data{get;set;}
		public Dictionary<string, string> Columns { get; set; }
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
		public List<DateTime> Dates { get; set; }
		public ReportAnswer Answer { get; set; }
		protected SqlConnection connection;


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

		public double this[DateTime? date, string key] {
			get {
				if (date!=null && date.HasValue) {
					return Data[date.Value][key];
				} else {
					if (!NeedRecords.Contains(key)) {
						NeedRecords.Add(key);
					}
					return 0;
				}
			}
		}

		public Report(DateTime dateStart, DateTime dateEnd, IntervalReportEnum interval) {
			DateStart = dateStart;
			DateEnd = dateEnd;
			RealDateEnd = dateEnd;
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

		public virtual  void ReadData() {
			foreach (RecordTypeBase recordType in RecordTypes.Values) {
				if (recordType is RecordTypeCalc) {
					RecordTypeCalc rtc=recordType as RecordTypeCalc;
					double d=rtc.CalcFunction(this, null);
				}
			}


			connection = PiramidaAccess.getConnection();
			connection.Open();
			foreach (RecordTypeBase recordType in RecordTypes.Values) {
				if (recordType is RecordTypeDB) {
					ReadDBData(recordType as RecordTypeDB);
				}
			}
			connection.Close();
			checkDBData();
			foreach (RecordTypeBase recordType in RecordTypes.Values) {
				if (recordType is RecordTypeCalc) {
					RecordTypeCalc rCalc=recordType as RecordTypeCalc;
					foreach (DateTime date in Dates) {
						double val=rCalc.CalcFunction(this,date);
						if (!Data[date].Keys.Contains(rCalc.ID)) {
							Data[date].Add(rCalc.ID, -1);
						}
						Data[date][rCalc.ID] = val;
					}
				}
			}
			calcResult();
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
				} else {
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

		protected void ReadDBData(RecordTypeDB recordType) {
			if (!(recordType.Visible || recordType.ToChart || NeedRecords.Contains(recordType.ID)))
				return;
			SqlCommand command= connection.CreateCommand();
			command.Parameters.AddWithValue("@dateStart", DateStart);
			command.Parameters.AddWithValue("@dateEnd", DateEnd);

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
					commandText = String.Format("SELECT {0}, {1} from DATA  WHERE {2} GROUP BY {0}",
						dateParam, valueOper, valueParams);
					command.CommandText = commandText;
					break;
				case IntervalReportEnum.halfHour:
					dateParam =
						String.Format(
						"datepart(year,{0}), datepart(month,{0}), datepart(day,{0}), datepart(hour,{0}), datepart(minute,{0})",
						"DATA_DATE");
					commandText = String.Format("SELECT {0}, {1} from [dbo].DATA  WHERE {2} GROUP BY {0}",
						dateParam, valueOper, valueParams, valueParams);
					break;
				case IntervalReportEnum.hour:
					dateParam =
						String.Format(
						"datepart(year,{0}), datepart(month,{0}), datepart(day,{0}), datepart(hour,{0})",
						dt30);
					commandText = String.Format("SELECT {0}, {1} from DATA  WHERE {2} GROUP BY {0}",
						dateParam, valueOper, valueParams, valueParams);
					break;
				case IntervalReportEnum.day:
					dateParam =
						String.Format(
						"datepart(year,{0}), datepart(month,{0}), datepart(day,{0})",
						dt30);
					commandText = String.Format("SELECT {0}, {1} from DATA  WHERE {2} GROUP BY {0}",
						dateParam, valueOper, valueParams, valueParams);
					break;
				case IntervalReportEnum.month:
					dateParam =
						String.Format(
						"datepart(year,{0}), datepart(month,{0})",
						dt30);
					commandText = String.Format("SELECT {0}, {1} from DATA d WHERE {2} GROUP BY {0}",
						dateParam, valueOper, valueParams, valueParams);
					break;
				case IntervalReportEnum.year:
					dateParam =
						String.Format(
						"datepart(year,{0})",
						dt30);
					commandText = String.Format("SELECT {0}, {1} from DATA  WHERE {2} GROUP BY {0}",
						dateParam, valueOper, valueParams, valueParams);
					break;
			}

			command.CommandText = commandText;
			SqlDataReader reader=command.ExecuteReader();
			DateTime lastDate=DateEnd;

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
					lastDate = date;
					
					if (Data.Keys.Contains(date)) {
						if (!Data[date].Keys.Contains(recordType.ID)) {
							Data[date].Add(recordType.ID, -1);
						}
						Data[date][recordType.ID] = val * recordType.MultParam / recordType.DivParam;
					}
				}
			}
			RealDateEnd = lastDate < RealDateEnd ? lastDate : RealDateEnd;
			reader.Close();
		}

		public virtual void CreateAnswerData(bool createResult=true) {
			Answer.Data = new List<ReportAnswerRecord>();
			Answer.Columns = new Dictionary<string, string>();

			if (createResult) {
				ReportAnswerRecord recordResult=new ReportAnswerRecord();
				recordResult.Header = "Итог";
				recordResult.DataStr = new Dictionary<string, string>();
				foreach (RecordTypeBase recordType in RecordTypes.Values) {
					if (recordType.Visible) {
						recordResult.DataStr.Add(recordType.ID, ResultData[recordType.ID].ToString(recordType.FormatDouble));
					}
				}
				Answer.Data.Add(recordResult);
			}

			foreach (RecordTypeBase recordType in RecordTypes.Values) {
				if (recordType.Visible) {
					if (!Answer.Columns.Keys.Contains(recordType.ID)) {
						Answer.Columns.Add(recordType.ID, recordType.Title);
					}
				}
			}

			foreach (DateTime date in Dates) {
				ReportAnswerRecord record=new ReportAnswerRecord();
				record.Header = GetCorrectedDate(date).ToString(getDateFormat());
				record.DataStr = new Dictionary<string, string>();				
				foreach (RecordTypeBase recordType in RecordTypes.Values) {
					if (recordType.Visible) {
						record.DataStr.Add(recordType.ID, Data[date][recordType.ID].ToString(recordType.FormatDouble));
					}
				}
				Answer.Data.Add(record);				
			}
		}

		public virtual void CreateAnswerDataHorizontal(bool createResult = true) {
			Answer.Data = new List<ReportAnswerRecord>();
			Answer.Columns = new Dictionary<string, string>();
			
			

			if (createResult) {
				Answer.Columns.Add("result", "Итог");				
			}

			foreach (DateTime date in Dates) {
				string dStr=GetCorrectedDate(date).ToString(getDateFormat());
				if (!Answer.Columns.Keys.Contains(dStr)) {
					Answer.Columns.Add(dStr, dStr);
				}
			}


			foreach (RecordTypeBase recordType in RecordTypes.Values) {
				if (recordType.Visible) {
					
					ReportAnswerRecord record=new ReportAnswerRecord();
					record.Header = recordType.Title;
					record.DataStr = new Dictionary<string, string>();

					if (createResult) {
						record.DataStr.Add("result", ResultData[recordType.ID].ToString(recordType.FormatDouble));
					}
					foreach (DateTime date in Dates) {
						string dStr=GetCorrectedDate(date).ToString(getDateFormat());
						record.DataStr.Add(dStr, Data[date][recordType.ID].ToString(recordType.FormatDouble));
					}
					Answer.Data.Add(record);
				}
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

			Answer.Chart.Properties.XAxisType = XAxisTypeEnum.datetime;

			Answer.Chart.Properties.XValueFormatString = getDateFormat();

			Random r=new Random();

			foreach (RecordTypeBase recordType in RecordTypes.Values) {
				if (recordType.ToChart) {
					ChartSerieProperties props=new ChartSerieProperties();
					props.Title = recordType.Title;
					props.TagName = recordType.ID;
					props.LineWidth = 2;
					props.Color = ChartColor.NextColor();
					props.SerieType=ChartSerieType.stepLine;
					props.YAxisIndex = 0;
					Answer.Chart.Properties.addSerie(props);

					ChartDataSerie data=new ChartDataSerie();
					data.Name = recordType.ID;
					foreach (DateTime date in Dates) {
						DateTime dt=GetCorrectedDate(date);
						data.Points.Add(new ChartDataPoint(dt,Data[date][recordType.ID]));
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
					return "MMMM yy";
				case IntervalReportEnum.year:
					return "yyyy";
			}
			return "dd.MM.yy HH:mm";
		}

		protected DateTime GetCorrectedDate(DateTime date) {
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
