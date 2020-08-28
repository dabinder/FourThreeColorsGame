using QuadRow.Framework;
using System;
using System.Globalization;
using System.Windows.Data;
using System.Windows.Media;

namespace QuadRow.Converters {
	public class ColorTypeToColorConverter : IValueConverter {
		/// <summary>
		/// convert ColorType enum to visible color
		/// </summary>
		/// <param name="value">enum value</param>
		/// <param name="targetType">Brush or Color</param>
		/// <param name="parameter"></param>
		/// <param name="language"></param>
		/// <returns>Color corresponding to given enum value</returns>
		public object Convert(object value, Type targetType, object parameter, CultureInfo language) {
			Color color = DisplayedColors.TranslateDisplayedColor((ColorType)value);

			if (targetType == typeof(Color)) {
				return color;
			} else if (targetType == typeof(Brush)) {
				return new SolidColorBrush(color);
			} else {
				throw new InvalidOperationException("Target must be of Color or Brush type");
			}
		}

		public object ConvertBack(object value, Type targetType, object parameter, CultureInfo language) {
			throw new NotImplementedException();
		}
	}
}
