﻿using System;
using System.Globalization;
using System.Windows;
using System.Windows.Data;

namespace QuadRow.Converters {
	/// <summary>
	/// converter to take a boolean and return a Visibility (visible/hidden) value
	/// </summary>
	public class BooleanToVisibilityConverter : IValueConverter {
		/// <summary>
		/// convert boolean to Visibility value
		/// </summary>
		/// <param name="value">boolean value</param>
		/// <param name="targetType">Visiblity</param>
		/// <param name="parameter"></param>
		/// <param name="language"></param>
		/// <returns>true = Visible, false = Hidden</returns>
		public object Convert(object value, Type targetType, object parameter, CultureInfo language) {
			if (targetType != typeof(Visibility)) throw new InvalidOperationException("Target must be of Visibility type");
			if (!(value is bool)) return null;
			return (bool)value ? Visibility.Visible : Visibility.Hidden;
		}

		/// <summary>
		/// convert Visibility to boolean
		/// </summary>
		/// <param name="value">Visibility value</param>
		/// <param name="targetType">boolean</param>
		/// <param name="parameter"></param>
		/// <param name="language"></param>
		/// <returns>Visible = true, Hidden = false</returns>
		public object ConvertBack(object value, Type targetType, object parameter, CultureInfo language) {
			if (!(value is Visibility)) throw new InvalidOperationException("Value must be of Visibility type");
			if (Equals(value, Visibility.Visible)) return true;
			if (Equals(value, Visibility.Hidden)) return false;
			return null;
		}
	}
}
