using System;
using System.Globalization;
using System.Windows.Data;

namespace QuadRow.Converters {
	/// <summary>
	/// converter to take multiple integer values and return their sum
	/// </summary>
	public class MultiValueSumConverter : IMultiValueConverter {
		/// <summary>
		/// get sum of integer values in list
		/// </summary>
		/// <param name="values">list of integer values to sum</param>
		/// <param name="targetType"></param>
		/// <param name="parameter"></param>
		/// <param name="culture"></param>
		/// <returns></returns>
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
