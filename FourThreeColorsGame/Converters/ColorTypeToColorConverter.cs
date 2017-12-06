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
			return DisplayedColors.TranslateDisplayedColor((ColorType)parameter);
		}

		public object ConvertBack(object value, Type targetType, object parameter, string language) {
			throw new NotImplementedException();
		}
	}
}
