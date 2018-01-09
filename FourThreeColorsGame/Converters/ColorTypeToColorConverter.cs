using FourThreeColorsGame.Framework;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Windows.UI;
using Windows.UI.Xaml.Data;
using Windows.UI.Xaml.Media;

namespace FourThreeColorsGame.Converters {
	class ColorTypeToColorConverter : IValueConverter {
		public object Convert(object value, Type targetType, object parameter, string language) {
			Color color;
			switch ((ColorType)value) {
				case ColorType.Color1:
					color = Colors.Crimson;
					break;

				case ColorType.Color2:
					color = Colors.SpringGreen;
					break;

				case ColorType.Color3:
					color = Colors.SkyBlue;
					break;

				default:
					throw new ArgumentOutOfRangeException($"ColorType {value} not among enumerated types");
			}

			if (targetType == typeof(Color)) {
				return color;
			} else if (targetType == typeof(Brush)) {
				return new SolidColorBrush(color);
			} else {
				throw new InvalidOperationException("Target must be of Color or Brush type");
			}
		}

		public object ConvertBack(object value, Type targetType, object parameter, string language) {
			throw new NotImplementedException();
		}
	}
}
