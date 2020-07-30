using System;
using System.Globalization;
using System.Linq;
using System.Windows.Data;

namespace QuadRow.Converters
{
	public class BooleanAndConverter : IMultiValueConverter {
		public object Convert(object[] values, Type targetType, object parameter, CultureInfo culture) {
			bool expected = parameter == null || (bool)parameter;

			return values.OfType<bool>().All(value => value == expected);
		}

		public object[] ConvertBack(object value, Type[] targetTypes, object parameter, CultureInfo culture) {
			throw new NotImplementedException();
		}
	}
}
