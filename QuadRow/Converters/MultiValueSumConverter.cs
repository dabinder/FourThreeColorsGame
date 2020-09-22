using System;
using System.Globalization;
using System.Windows.Data;

namespace QuadRow.Converters {
	class MultiValueSumConverter : IMultiValueConverter {
		public object Convert(object[] values, Type targetType, object parameter, CultureInfo culture) {
			int sum = 0;

			foreach (var value in values) {
				if (!(value is int)) {
					throw new InvalidOperationException("Values must be integers only");
				} else {
					sum += (int)value;
				}
			}

			return sum.ToString();
		}

		public object[] ConvertBack(object value, Type[] targetTypes, object parameter, CultureInfo culture) {
			throw new NotImplementedException();
		}
	}
}
