using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Windows.UI.Xaml.Data;

namespace FourThreeColorsGame.Converters {
	class InverseBooleanConverter : IValueConverter {
		public object Convert(object value, Type targetType, object parameter, string language) {
			if (!(value is bool))
				return null;
			return !((bool)value);
		}

		public object ConvertBack(object value, Type targetType, object parameter, string language) {
			throw new NotImplementedException();
		}
	}
}
