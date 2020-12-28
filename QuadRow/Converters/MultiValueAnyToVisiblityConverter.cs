using System;
using System.Globalization;
using System.Windows;
using System.Windows.Data;

namespace QuadRow.Converters {
	/// <summary>
	/// converter to take multiple values and return a Visibility (visible/hidden) value
	/// </summary>
	public class MultiValueAnyToVisiblityConverter : IMultiValueConverter {
		/// <summary>
		/// set object to visible if any values in list match expected (true/false)
		/// set to hidden otherwise
		/// </summary>
		/// <param name="values">list of values</param>
		/// <param name="targetType">Visibility</param>
		/// <param name="parameter">value to match (true/false)</param>
		/// <param name="culture"></param>
		/// <returns>if all values match parameter: true; otherwise false</returns>
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
