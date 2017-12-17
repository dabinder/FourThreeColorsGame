using FourThreeColorsGame.Framework;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Windows.UI;
using Windows.UI.Xaml.Data;

namespace FourThreeColorsGame.Converters {
	class ColorTypeToColorConverter : IValueConverter {
		public object Convert(object value, Type targetType, object parameter, string language) {
			switch ((ColorType)parameter) {
				case ColorType.Color1:
					return Colors.Crimson;

				case ColorType.Color2:
					return Colors.SpringGreen;

				case ColorType.Color3:
					return Colors.SkyBlue;

				default:
					throw new ArgumentOutOfRangeException($"ColorType {parameter} not among enumerated types");
			}
		}

		public object ConvertBack(object value, Type targetType, object parameter, string language) {
			throw new NotImplementedException();
		}
	}
}
