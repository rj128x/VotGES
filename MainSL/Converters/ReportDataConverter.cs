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
using System.Windows.Data;
using MainSL.Logging;
using System.Collections.Generic;

namespace MainSL.Converters
{
	public class ReportDataConverter : IValueConverter
	{
		/// <summary>
		/// Convert a Date into DD/MMM/YYYY HH:mm format
		/// </summary>
		/// <param name="value">object: value</param>
		/// <param name="targetType">Type: targetType</param>
		/// <param name="parameter">object: parameter</param>
		/// <param name="culture">System.Globalization.CultureInfo: culture</param>
		/// <returns>System.Object</returns>
		public object Convert(object value, Type targetType, object parameter, System.Globalization.CultureInfo culture) {
			try {
				Dictionary<string,double> sostav=value as Dictionary<string, double>;
				string key=parameter.ToString();
				return sostav[key];
			} catch {
				return "-";
			}
		}

		public object ConvertBack(object value, Type targetType, object parameter, System.Globalization.CultureInfo culture) {
			return null;
		}

	}
}
