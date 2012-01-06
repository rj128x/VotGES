using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.ComponentModel;

namespace VotGES.Piramida.PiramidaReport
{
	public enum ReportTypeEnum { dayByMinutes, dayByHalfHours, dayByHours, monthByDays, yearByDays, yearByMonths }

	public class ReportSettings: INotifyPropertyChanged
	{
		public event PropertyChangedEventHandler PropertyChanged;
		public void NotifyChanged(string propName) {
			if (PropertyChanged != null)
				PropertyChanged(this, new PropertyChangedEventArgs(propName));
		}
	
		public class DateTimeStartEnd
		{
			public DateTime DateStart { get; set; }
			public DateTime DateEnd { get; set; }

			public static DateTimeStartEnd getFullDay(DateTime date) {
				DateTimeStartEnd result=new DateTimeStartEnd();
				result.DateStart = date.Date;
				result.DateEnd = date.Date.AddDays(1);
				return result;
			}

			public static DateTimeStartEnd getFullMonth(int year, int month) {
				DateTimeStartEnd result=new DateTimeStartEnd();
				result.DateStart = new DateTime(year, month, 1);
				result.DateEnd = result.DateStart.AddMonths(1);
				return result;
			}

			public static DateTimeStartEnd getFullYear(int year) {
				DateTimeStartEnd result=new DateTimeStartEnd();
				result.DateStart = new DateTime(year, 1, 1);
				result.DateEnd = result.DateStart.AddYears(1);
				return result;
			}
		}



		private Dictionary<ReportTypeEnum,string> reportTypeNames;
		public Dictionary<ReportTypeEnum, string> ReportTypeNames {
			get { return reportTypeNames; }
			set { reportTypeNames = value; }
		}

		private Dictionary<int,string> monthNames;
		public Dictionary<int, string> MonthNames {
			get { return monthNames; }
			set { monthNames = value; }
		}


		private ReportTypeEnum reportType;
		public ReportTypeEnum ReportType {
			get { return reportType; }
			set { 
				reportType = value;
				switch (reportType) {
					case ReportTypeEnum.dayByMinutes:
					case ReportTypeEnum.dayByHalfHours:
					case ReportTypeEnum.dayByHours:
						IsVisibleDate = true;
						IsVisibleMonth = false;
						IsVisibleYear = false;
						break;
					case ReportTypeEnum.monthByDays:
						IsVisibleDate = false;
						IsVisibleYear = true;
						IsVisibleMonth = true;
						break;
					case ReportTypeEnum.yearByDays:
					case ReportTypeEnum.yearByMonths:
						IsVisibleDate = false;
						IsVisibleYear = true;
						IsVisibleMonth = false;
						break;
				}
				NotifyChanged("ReportType");
			}
		}

		private int year;
		public int Year {
			get { return year; }
			set { 
				year = value;
				NotifyChanged("Year");
			}
		}

		private int month;
		public int Month {
			get { return month; }
			set { 
				month = value;
				NotifyChanged("Month");
			}
		}

		private DateTime date;
		public DateTime Date {
			get { return date; }
			set { 
				date = value;
				NotifyChanged("Date");
			}
		}

		private bool isVisibleDate;
		public bool IsVisibleDate {
			get { return isVisibleDate; }
			set { 
				isVisibleDate = value;
				NotifyChanged("IsVisibleDate");
			}
		}

		private bool isVisibleMonth;
		public bool IsVisibleMonth {
			get { return isVisibleMonth; }
			set { 
				isVisibleMonth = value;
				NotifyChanged("IsVisibleMonth");
			}
		}

		private bool isVisibleYear;
		public bool IsVisibleYear {
			get { return isVisibleYear; }
			set { 
				isVisibleYear = value;
				NotifyChanged("IsVisibleYear");
			}
		}

		public ReportSettings() {
			ReportTypeNames.Add(ReportTypeEnum.dayByMinutes, "За сутки по минутам");
			ReportTypeNames.Add(ReportTypeEnum.dayByHalfHours, "За сутки по 30 минут");
			ReportTypeNames.Add(ReportTypeEnum.dayByHours, "За сутки по часам");
			ReportTypeNames.Add(ReportTypeEnum.monthByDays, "За месяц по дням");
			ReportTypeNames.Add(ReportTypeEnum.yearByDays, "За год по дням");
			ReportTypeNames.Add(ReportTypeEnum.yearByMonths, "За год по месяцам");

			MonthNames.Add(1, "Январь");
			MonthNames.Add(2, "Февраль");
			MonthNames.Add(3, "Март");
			MonthNames.Add(4, "Апрель");
			MonthNames.Add(5, "Май");
			MonthNames.Add(6, "Июнь");
			MonthNames.Add(7, "Июль");
			MonthNames.Add(8, "Август");
			MonthNames.Add(9, "Сентябрь");
			MonthNames.Add(10, "Октябрь");
			MonthNames.Add(11, "Ноябрь");
			MonthNames.Add(12, "Декабрь");

			Year = DateTime.Now.Year;
			Month = DateTime.Now.Month;
			Date = DateTime.Now.Date;

			ReportType = ReportTypeEnum.dayByHalfHours;

		}

	}
}
