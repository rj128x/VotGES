﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Animation;
using System.Windows.Shapes;
using System.Windows.Navigation;
using VotGES.Web.Services;
using VotGES.Chart;
using System.ComponentModel;

namespace MainSL.Views
{
	public partial class CheckPrognozNBPage : Page
	{
		public class Settings : INotifyPropertyChanged
		{
			public event PropertyChangedEventHandler PropertyChanged;

			public void NotifyChanged(string propName) {
				if (PropertyChanged != null)
					PropertyChanged(this, new PropertyChangedEventArgs(propName));
			}

			private int countDays;
			public int CountDays {
				get { return countDays; }
				set {
					countDays = value;
					countDays = countDays < 1 ? 1 : countDays;
					countDays = countDays > 10 ? 10 : countDays;
					NotifyChanged("CountDays");
				}
			}

			private DateTime date;
			public DateTime Date {
				get { return date; }
				set { 
					date = value;
					if (date.Date >= DateTime.Now.AddHours(-2).Date)
						date = DateTime.Now.AddHours(-2).Date.AddDays(-1);
					NotifyChanged("Date");					
				}
			}
		}

		public Settings settings;
		ChartContext chartContext;
		public CheckPrognozNBPage() {
			InitializeComponent();
			chartContext = new ChartContext();
			settings = new Settings();
			settings.CountDays = 1;
			settings.Date = DateTime.Now.Date.AddDays(-1);
			pnlSettings.DataContext = settings;
		}

		// Выполняется, когда пользователь переходит на эту страницу.
		protected override void OnNavigatedTo(NavigationEventArgs e) {
		}

		private void btnGetPrognoz_Click(object sender, RoutedEventArgs e) {
			chartContext.checkPrognozNB(settings.Date, settings.CountDays, oper => {
				try {
					ChartAnswer answer=oper.Value;
					chartControl.Create(answer);
				} catch (Exception ex) {
					Logging.Logger.info(ex.ToString());
					MessageBox.Show("Ошибка при обработке ответа от сервера");
				}
				GlobalStatus.Current.IsWaiting = false;
			}, null);
			GlobalStatus.Current.IsWaiting = true;
		}

	}
}
