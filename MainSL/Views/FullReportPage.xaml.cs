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
using VotGES.Piramida.Report;
using VotGES.Web.Services;
using System.ServiceModel.DomainServices.Client;

namespace MainSL.Views
{
	public partial class FullReportPage : Page
	{
		FullReportRoot Root { get; set; }
		ReportBaseDomainContext Context { get; set; }
		List<String> SelectedValues { get; set; }

		public FullReportPage() {
			InitializeComponent();
			Context = new ReportBaseDomainContext();
			SelectedValues = new List<string>();
		}

		// Выполняется, когда пользователь переходит на эту страницу.
		protected override void OnNavigatedTo(NavigationEventArgs e) {
			if (Root == null) {
				loadRoot();
			}			
		}

		protected override void OnNavigatedFrom(NavigationEventArgs e) {
			GlobalStatus.Current.StopLoad();
		}

		protected void loadRoot() {
			InvokeOperation currentOper=Context.GetFullReportRoot(oper => {
				if (oper.IsCanceled) {
					return;
				}
				try {
					GlobalStatus.Current.StartProcess();
					Root = oper.Value;
					TreeMainRecords.ItemsSource = Root.RootMain.Children;
					TreeLinesRecords.ItemsSource = Root.RootLines.Children;
					TreeSNRecords.ItemsSource = Root.RootSN.Children;
					
				} catch (Exception ex) {
					Logging.Logger.info(ex.ToString());
					GlobalStatus.Current.ErrorLoad("Ошибка при получении дерева");
				} finally {
					GlobalStatus.Current.StopLoad();
				}

			}, null);
			GlobalStatus.Current.StartLoad(currentOper);
		}

		protected void RefreshSelectedValues() {
			SelectedValues.Clear();
			createSelectedList(Root.RootMain);
			createSelectedList(Root.RootLines);
			createSelectedList(Root.RootSN);
		}

		protected void createSelectedList(FullReportRecord record) {			
			foreach (FullReportRecord child in record.Children) {
				if (child.Selected) {
					SelectedValues.Add(child.Key);
				}
				if (child.IsGroup) {
					createSelectedList(child);
				}
			}
		}

		private void btnGetReport_Click(object sender, RoutedEventArgs e) {
			RefreshSelectedValues();
			ReportSettings.DateTimeStartEnd des=ReportSettings.DateTimeStartEnd.getBySettings(SettingsControl.Settings);
			InvokeOperation currentOper=Context.GetFullReport(SelectedValues,des.DateStart,des.DateEnd,SettingsControl.Settings.ReportType, 
				oper => {
				if (oper.IsCanceled) {
					return;
				}
				try {
					GlobalStatus.Current.StartProcess();
					ResultControl.Create(oper.Value);
					tabResult.IsSelected = true;
				} catch (Exception ex) {
					Logging.Logger.info(ex.ToString());
					GlobalStatus.Current.ErrorLoad("Ошибка при получении данных");
				} finally {
					GlobalStatus.Current.StopLoad();
				}

			}, null);
			GlobalStatus.Current.StartLoad(currentOper);
		}

		private void btnGetChart_Click(object sender, RoutedEventArgs e) {

		}
	}
}
