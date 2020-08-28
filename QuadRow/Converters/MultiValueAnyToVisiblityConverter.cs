using System;
using System.Globalization;
using System.Windows;
using System.Windows.Data;

namespace QuadRow.Converters {
	public class MultiValueAnyToVisiblityConverter : IMultiValueConverter {
		/// <summary>
		/// set object to visible if any values in list match expected (true/false)
		/// </summary>
		/// <param name="values"></param>
		/// <param name="targetType"></param>
		/// <param name="parameter"></param>
		/// <param name="culture"></param>
		/// <returns></returns>
		public object Convert(object[] values, Type targetType, object parameter, CultureInfo culture) {
			bool expected = parameter == null || (bool)parameter;

			foreach (var value in values) {
				if (!(value is bool)) {
					return Visibility.Hidden;
				}
				if ((bool)value == expected) {
					return Visibility.Visible;
				}
			}
			return Visibility.Hidden;
		}

		public object[] ConvertBack(object value, Type[] targetTypes, object parameter, CultureInfo culture) {
			throw new NotImplementedException();
		}
	}
}
