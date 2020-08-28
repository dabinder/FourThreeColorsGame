using System;
using System.Globalization;
using System.Windows.Data;

namespace QuadRow.Converters {
	public class InverseBooleanConverter : IValueConverter {
		public object Convert(object value, Type targetType, object parameter, CultureInfo language) {
			if (!(value is bool))
				return null;
			return !((bool)value);
		}

		public object ConvertBack(object value, Type targetType, object parameter, CultureInfo language) {
			throw new NotImplementedException();
		}
	}
}
