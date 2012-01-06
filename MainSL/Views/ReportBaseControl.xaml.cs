using System;
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
using VotGES.Piramida.PiramidaReport;
using MainSL.Converters;

namespace MainSL.Views
{
	public partial class ReportBaseControl : UserControl
	{
		public ReportAnswer Answer;
		public ReportDataConverter converter;
		public ReportBaseControl() {
			InitializeComponent();
			converter = new ReportDataConverter();
		}


		public void Create(ReportAnswer answer) {
			Answer = answer;
			dataReport.AutoGenerateColumns = false;
			dataReport.CanUserResizeColumns = false;
			dataReport.CanUserSortColumns = false;
			dataReport.CanUserReorderColumns = false;
			dataReport.Columns.Clear();

			DataGridTextColumn columnHeader=new DataGridTextColumn();
			columnHeader.Header = "Параметр";
			columnHeader.IsReadOnly = true;
			columnHeader.Binding = new System.Windows.Data.Binding();
			columnHeader.Binding.Mode = System.Windows.Data.BindingMode.OneTime;
			columnHeader.Binding.Path = new PropertyPath("Header");
			dataReport.Columns.Add(columnHeader);


			foreach (KeyValuePair<string,string> de in answer.Columns){
				DataGridTextColumn column=new DataGridTextColumn();
				column.Header = de.Value;
				dataReport.Columns.Add(column);
				column.Binding = new System.Windows.Data.Binding();
				column.Binding.Path = new PropertyPath("DataStr");
				column.Binding.Converter = converter;
				column.Binding.ConverterParameter = de.Key;
			}

			dataReport.ItemsSource = answer.Data;

			chartControl.Create(answer.Chart);
			
		}
	}
}
