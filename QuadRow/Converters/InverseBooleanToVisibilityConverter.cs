using System;
using System.Globalization;
using System.Windows;
using System.Windows.Data;

namespace QuadRow.Converters
{
	class InverseBooleanToVisibilityConverter : IValueConverter {
		public object Convert(object value, Type targetType, object parameter, CultureInfo language) {
			if (targetType != typeof(Visibility))
				throw new InvalidOperationException("Target must be of Visibility type");
			if (!(value is bool))
				return null;
			return (bool)value ? Visibility.Collapsed : Visibility.Visible;
		}

		public object ConvertBack(object value, Type targetType, object parameter, CultureInfo language) {
			if (!(value is Visibility))
				throw new InvalidOperationException("Value must be of Visibility type");
			if (Equals(value, Visibility.Visible))
				return false;
			if (Equals(value, Visibility.Collapsed))
				return true;
			return null;
		}
	}
}
