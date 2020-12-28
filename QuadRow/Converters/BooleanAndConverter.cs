using System;
using System.Globalization;
using System.Linq;
using System.Windows.Data;

namespace QuadRow.Converters {
	/// <summary>
	/// converter to take multiple values and return whether *all* match the expected value
	/// </summary>
	public class BooleanAndConverter : IMultiValueConverter {
		/// <summary>
		/// read multiple values, return single value indicating *all* match expected
		/// </summary>
		/// <param name="values">list of values</param>
		/// <param name="targetType">boolean</param>
		/// <param name="parameter">value to match (true/false)</param>
		/// <param name="culture"></param>
		/// <returns>true if all values match expected parameter; false otherwise</returns>
		public object Convert(object[] values, Type targetType, object parameter, CultureInfo culture) {
			bool expected = parameter == null || (bool)parameter;

			return values.OfType<bool>().All(value => value == expected);
		}

		public object[] ConvertBack(object value, Type[] targetTypes, object parameter, CultureInfo culture) {
			throw new NotImplementedException();
		}
	}
}
