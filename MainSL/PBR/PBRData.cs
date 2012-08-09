using System;
using System.Net;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Documents;
using System.Windows.Ink;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Animation;
using System.Windows.Shapes;
using System.ComponentModel;
using System.Collections.ObjectModel;
using System.Collections.Generic;

namespace MainSL
{
	public class PBRPoint : INotifyPropertyChanged
	{
		protected PBRData parent;
		public event PropertyChangedEventHandler PropertyChanged;
		protected DelegateCommand commandDown;
		protected DelegateCommand commandUp;
		protected DelegateCommand commandPrev;
		protected DelegateCommand commandNext;

		public PBRPoint(DateTime date, double val, PBRData parent) {
			this.parent = parent;
			this.date = date;
			this.val = val;			
			commandDown = new DelegateCommand(downDataFunc, canExec);
			commandUp = new DelegateCommand(upDataFunc, canExec);
			commandPrev = new DelegateCommand(prevDataFunc, canExec);
			commandNext = new DelegateCommand(nextDataFunc, canExec);
		}

		private void downDataFunc(object param) {
			parent.doDown(this);
		}

		private void upDataFunc(object param) {
			parent.doUp(this);
		}

		private void prevDataFunc(object param) {
			parent.doPrev(this);
		}

		private void nextDataFunc(object param) {
			parent.doNext(this);
		}

		private bool canExec(object parameter) {
			return true;
		}

		public void NotifyChanged(string propName) {
			if (PropertyChanged != null)
				PropertyChanged(this, new PropertyChangedEventArgs(propName));
		}
		protected DateTime date;
		public DateTime Date {
			get {
				return date;
			}
			set {
				date = value;
				NotifyChanged("Date");
			}
		}

		protected double val;
		public double Val {
			get {
				return val;
			}
			set {
				val = value;
				parent.Data[Date] = val;
				NotifyChanged("Val");
			}
		}

		public DelegateCommand doDown {
			get {
				return commandDown;
			}
		}

		public DelegateCommand doUp {
			get {
				return commandUp;
			}
		}

		public DelegateCommand doPrev {
			get {
				return commandPrev;
			}
		}

		public DelegateCommand doNext {
			get {
				return commandNext;
			}
		}
	}

	public class PBRData : INotifyPropertyChanged
	{
		public event PropertyChangedEventHandler PropertyChanged;
		private ObservableCollection<PBRPoint> pbr=new ObservableCollection<PBRPoint>();
		public ObservableCollection<PBRPoint> PBR {
			get {
				return pbr;
			}
			set {
				pbr = PBR;
				NotifyChanged("PBR");
			}
		}

		protected Dictionary<DateTime, double> data;
		public  Dictionary<DateTime, double> Data {
			get { return data; }
			protected set { 
				data = value;
				refreshPBRFromData();
			}
		}


		protected void refreshPBRFromData() {
			pbr.Clear();
			foreach (KeyValuePair<DateTime,double> de in data) {
				pbr.Add(new PBRPoint(de.Key, de.Value, this));
			}
		}
		
		public PBRData(Dictionary<DateTime, double> data) {
			if (data == null)
				data = new Dictionary<DateTime, double>();
			this.data = data;
			refreshPBRFromData();
		}

		public void NotifyChanged(string propName) {
			if (PropertyChanged != null)
				PropertyChanged(this, new PropertyChangedEventArgs(propName));
		}

		public void doDown(PBRPoint point) {
			int index=PBR.IndexOf(point);
			for (int i=index; i < PBR.Count; i++) {
				PBR[i].Val = PBR[index].Val;
			}
		}

		public void doUp(PBRPoint point) {
			int index=PBR.IndexOf(point);
			for (int i=0; i < index; i++) {
				PBR[i].Val = PBR[index].Val;
			}
		}

		public void doNext(PBRPoint point) {
			int index=PBR.IndexOf(point);
			if (index < PBR.Count) {
				PBR[index].Val = PBR[index + 1].Val;
			}
		}

		public void doPrev(PBRPoint point) {
			int index=PBR.IndexOf(point);
			if (index > 0) {
				PBR[index].Val = PBR[index - 1].Val;
			}
		}

		protected  bool modeHalfHours=true;
		public bool ModeHalfHours {
			get { return modeHalfHours; }
			set { 
				modeHalfHours = value;
				NotifyChanged("ModeHalfHours");
			}
		}

		public void convertToHoursPBR() {
			if (!modeHalfHours)
				return;
			DateTime[] dates=new DateTime[data.Count];
			data.Keys.CopyTo(dates,0);
			DateTime firstDate=dates[0];
			if (dates[0].Minute==30) {
				double y=data[firstDate];
				double y2=data[firstDate.AddMinutes(30)];
				double y1=y2-2*y;
				data.Remove(firstDate);
				data.Add(firstDate.AddMinutes(-30), y1);
			}
			foreach (DateTime date in dates) {
				if (date.Minute == 30) {
					data.Remove(date);
				}
			}
			ModeHalfHours = false;
			refreshPBRFromData();
		}

		public void convertToHalfHoursPBR() {
			if (modeHalfHours)
				return;
			DateTime[] dates=new DateTime[data.Count];
			data.Keys.CopyTo(dates, 0);
			DateTime firstDate=dates[0];
			DateTime lastDate=dates[dates.Length - 1];
			DateTime date=firstDate.AddMinutes(30);
			while (date < lastDate) {				
				double val = (data[date.AddMinutes(-30)] + data[date.AddMinutes(30)]) / 2;
				data.Add(date, val);
				date = date.AddMinutes(60);
			}

			Dictionary<DateTime,double> newData=new Dictionary<DateTime, double>();
			date=firstDate.AddMinutes(0);
			while (date <= lastDate) {
				newData.Add(date, data[date]);
				date= date.AddMinutes(30);
			}
			Data = newData;
			ModeHalfHours = true;
		}


	}
}
