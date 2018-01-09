using FourThreeColorsGame.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Text.RegularExpressions;
using Windows.UI.Xaml.Data;

namespace FourThreeColorsGame.Converters {
	class PositionToIntConverter : IValueConverter {

		public object Convert(object value, Type targetType, object parameter, string language) {
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

			switch ((string)parameter) {
				case "Column":
					return IntAlphaConverter.AlphaToInt(match.Groups[1].Value[0]);

				case "Row":
					return Int32.Parse(match.Groups[2].Value);

				default:
					throw new ArgumentOutOfRangeException($"invalid parameter {parameter}; expecting Row or Column");
			}
		}

		public object ConvertBack(object value, Type targetType, object parameter, string language) {
			throw new NotImplementedException();
		}
	}
}
