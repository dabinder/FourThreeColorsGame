using QuadRow.Framework;
using System;
using System.Globalization;
using System.Text.RegularExpressions;
using System.Windows.Data;

namespace QuadRow.Converters
{
	public class PositionToIntConverter : IValueConverter {

		/// <summary>
		/// convert game space position (A1 format) to integer value for row or column
		/// </summary>
		/// <param name="value">input coordinates</param>
		/// <param name="targetType">int</param>
		/// <param name="parameter">row or column</param>
		/// <param name="language"></param>
		/// <returns>numeric value for row or column from coordinate</returns>
		public object Convert(object value, Type targetType, object parameter, CultureInfo language) {
			if (targetType != typeof(int)) {
				throw new InvalidOperationException("Target must be an integer");
			}
			if (!(value is string)) {
				return null;
			}

			string valueStr = ((string)value).Trim();
			Match match = Regex.Match(valueStr, "^([A-Z])(\\d+)$");
			if (!match.Groups[0].Success) {
				throw new ArgumentException("Input value must be board position in format [A-Z]#");
			}

			//return numeric value for row or column
			switch ((string)parameter) {
				case "Column":
					return IntAlphaConverter.AlphaToInt(match.Groups[1].Value[0]);

				case "Row":
					return int.Parse(match.Groups[2].Value);

				default:
					throw new ArgumentOutOfRangeException($"invalid parameter {parameter}; expecting Row or Column");
			}
		}

		public object ConvertBack(object value, Type targetType, object parameter, CultureInfo language) {
			throw new NotImplementedException();
		}
	}
}
