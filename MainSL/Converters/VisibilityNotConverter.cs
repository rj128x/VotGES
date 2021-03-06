﻿using System;
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

namespace MainSL.Converters
{
	public class VisibilityNotConverter : IValueConverter
	{
		#region IValueConverter Members
		public object Convert(object value, Type targetType, object parameter, System.Globalization.CultureInfo culture) {
			try {
				bool val=(Boolean)value;
				return !val ? System.Windows.Visibility.Visible : System.Windows.Visibility.Collapsed;
			} catch {
				return System.Windows.Visibility.Visible;
			}
		}

		public object ConvertBack(object value, Type targetType, object parameter, System.Globalization.CultureInfo culture) {
			try {
				System.Windows.Visibility val=(System.Windows.Visibility)value;
				return val == System.Windows.Visibility.Collapsed;
			} catch {
				return false;
			}
		}

		#endregion
	}
	
}
