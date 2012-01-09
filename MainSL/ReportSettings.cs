using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.ComponentModel;

namespace VotGES.Piramida.Report
{
	

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

			public static DateTimeStartEnd getFullQuarter(int year, int quarter) {
				DateTimeStartEnd result=new DateTimeStartEnd();
				result.DateStart = new DateTime(year, (quarter-1)*3+1, 1);
				result.DateEnd = result.DateStart.AddMonths(3);
				return result;
			}

			public static DateTimeStartEnd getFullYear(int year) {
				DateTimeStartEnd result=new DateTimeStartEnd();
				result.DateStart = new DateTime(year, 1, 1);
				result.DateEnd = result.DateStart.AddYears(1);
				return result;
			}

			public static DateTimeStartEnd getBySettings(ReportSettings settings) {
				switch (settings.reportType) {
					case ReportTypeEnum.dayByHalfHours:
					case ReportTypeEnum.dayByHours:
					case ReportTypeEnum.dayByMinutes:
						return getFullDay(settings.Date);
					case ReportTypeEnum.monthByDays:
					case ReportTypeEnum.monthByHours:
					case ReportTypeEnum.monthByHalfHours:
						return getFullMonth(settings.Year, settings.Month);
					case ReportTypeEnum.quarterByDays:
						return getFullQuarter(settings.Year, settings.Quarter);
					case ReportTypeEnum.yearByDays:
					case ReportTypeEnum.yearByMonths:
					case ReportTypeEnum.yearByQarters:
						return getFullYear(settings.Year);
				}
				return null;
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

		private Dictionary<int,string> quarterNames;
		public Dictionary<int, string> QuarterNames {
			get { return quarterNames; }
			set { quarterNames = value; }
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
						IsVisibleQuarter = false;
						IsVisibleYear = false;
						break;
					case ReportTypeEnum.monthByDays:
					case ReportTypeEnum.monthByHalfHours:
					case ReportTypeEnum.monthByHours:
						IsVisibleDate = false;
						IsVisibleYear = true;
						IsVisibleMonth = true;
						IsVisibleQuarter = false;
						break;
					case ReportTypeEnum.quarterByDays:
						IsVisibleDate = false;
						IsVisibleYear = true;
						IsVisibleMonth = false;
						IsVisibleQuarter = true;
						break;
					case ReportTypeEnum.yearByDays:
					case ReportTypeEnum.yearByMonths:
					case ReportTypeEnum.yearByQarters:
						IsVisibleDate = false;
						IsVisibleYear = true;
						IsVisibleMonth = false;
						IsVisibleQuarter = false;
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
				year=year<1960?DateTime.Now.Year:year;
				year = year > DateTime.Now.Year ? DateTime.Now.Year : year;

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

		private int quarter;
		public int Quarter {
			get { return quarter; }
			set {
				quarter = value;
				NotifyChanged("Quarter");
			}
		}

		private DateTime date;
		public DateTime Date {
			get { return date; }
			set { 
				date = value;
				Year = Date.Year;
				Month = Date.Month;
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

		private bool isVisibleQuarter;
		public bool IsVisibleQuarter {
			get { return isVisibleQuarter; }
			set {
				isVisibleQuarter = value;
				NotifyChanged("IsVisibleQuarter");
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
			ReportTypeNames = new Dictionary<ReportTypeEnum, string>();
			MonthNames = new Dictionary<int, string>();
			QuarterNames = new Dictionary<int, string>();

			ReportTypeNames.Add(ReportTypeEnum.dayByMinutes, "За сутки по минутам");
			ReportTypeNames.Add(ReportTypeEnum.dayByHalfHours, "За сутки по 30 минут");
			ReportTypeNames.Add(ReportTypeEnum.dayByHours, "За сутки по часам");
			ReportTypeNames.Add(ReportTypeEnum.monthByHalfHours, "За месяц по 30 минут");
			ReportTypeNames.Add(ReportTypeEnum.monthByHours, "За месяц по часам");
			ReportTypeNames.Add(ReportTypeEnum.monthByDays, "За месяц по дням");			
			ReportTypeNames.Add(ReportTypeEnum.quarterByDays, "За квартал по дням");
			ReportTypeNames.Add(ReportTypeEnum.yearByDays, "За год по дням");
			ReportTypeNames.Add(ReportTypeEnum.yearByMonths, "За год по месяцам");
			ReportTypeNames.Add(ReportTypeEnum.yearByQarters, "За год по кварталам");

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

			QuarterNames.Add(1, "1 Квартал");
			QuarterNames.Add(2, "2 Квартал");
			QuarterNames.Add(3, "3 Квартал");
			QuarterNames.Add(4, "4 Квартал");

			Year = DateTime.Now.Year;
			Month = DateTime.Now.Month;
			Date = DateTime.Now.Date;
			Quarter = 1;

			ReportType = ReportTypeEnum.dayByHalfHours;

		}

	}
}
