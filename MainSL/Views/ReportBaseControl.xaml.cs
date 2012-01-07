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
using VotGES.Piramida.Report;
using MainSL.Converters;
using System.Windows.Markup;

namespace MainSL.Views
{
	public partial class ReportBaseControl : UserControl
	{
		public ReportAnswer Answer;
		public ReportDataConverter converter;
		public DataTemplate headerTemplate;
		public string columnTemplateStr;
		public ReportBaseControl() {
			InitializeComponent();
			converter = new ReportDataConverter();
			string str= @"<DataTemplate  
				xmlns=""http://schemas.microsoft.com/client/2007"">         
				<TextBlock Text=""{Binding Header}"" FontWeight=""Bold"" /> 
            </DataTemplate>";
			headerTemplate = XamlReader.Load(str) as DataTemplate;

			str= @"<DataTemplate  
				xmlns=""http://schemas.microsoft.com/client/2007"">         
				<TextBlock Text=""{Binding DataStr, Converter={StaticResource reportDataConverter}, ConverterParameter='~param~'}"" /> 
            </DataTemplate>";

			columnTemplateStr = str;

		}


		public void Create(ReportAnswer answer) {
			Answer = answer;
			dataReport.AutoGenerateColumns = false;
			dataReport.CanUserResizeColumns = false;
			dataReport.CanUserSortColumns = false;
			dataReport.CanUserReorderColumns = false;
			dataReport.Columns.Clear();


			DataGridTemplateColumn columnHeader=new DataGridTemplateColumn();
			columnHeader.Header = "Параметр";
			DataTemplate temlate=headerTemplate;
			columnHeader.CellTemplate = temlate;
			columnHeader.ClipboardContentBinding = new System.Windows.Data.Binding();
			columnHeader.ClipboardContentBinding.Mode = System.Windows.Data.BindingMode.OneTime;
			columnHeader.ClipboardContentBinding.Path = new PropertyPath("Header");

			dataReport.Columns.Add(columnHeader);

			foreach (KeyValuePair<string,string> de in answer.Columns) {
				DataGridTemplateColumn column=new DataGridTemplateColumn();
				column.Header = de.Value;
				column.CellTemplate = XamlReader.Load(columnTemplateStr.Replace("~param~",de.Key)) as DataTemplate;
				column.Width = new DataGridLength(1, DataGridLengthUnitType.SizeToCells);
				column.ClipboardContentBinding = new System.Windows.Data.Binding();
				column.ClipboardContentBinding.Path = new PropertyPath("DataStr");
				column.ClipboardContentBinding.Converter = converter;
				column.ClipboardContentBinding.ConverterParameter = de.Key;
				column.MinWidth = 50;
				dataReport.Columns.Add(column);
			}



			/*DataGridTextColumn columnHeader=new DataGridTextColumn();
			columnHeader.Header = "Параметр";
			columnHeader.IsReadOnly = true;
			columnHeader.Binding = new System.Windows.Data.Binding();
			columnHeader.Binding.Mode = System.Windows.Data.BindingMode.OneTime;
			columnHeader.Binding.Path = new PropertyPath("Header");
			dataReport.Columns.Add(columnHeader);


			foreach (KeyValuePair<string,string> de in answer.Columns){
				DataGridTextColumn column=new DataGridTextColumn();
				column.Header = de.Value;				
				column.Binding = new System.Windows.Data.Binding();
				column.Binding.Path = new PropertyPath("DataStr");
				column.Binding.Converter = converter;
				column.Binding.ConverterParameter = de.Key;
				column.Width = new DataGridLength(1, DataGridLengthUnitType.SizeToCells);
				column.MinWidth = 50;
				dataReport.Columns.Add(column);
			}*/

			dataReport.ItemsSource = answer.Data;

			chartControl.Create(answer.Chart);
			
		}
	}
}
