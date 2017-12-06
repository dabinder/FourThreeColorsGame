using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Windows.UI;

namespace FourThreeColorsGame.Framework {
	static class DisplayedColors {
		/// <summary>
		/// translate ColorType into displayable color
		/// </summary>
		/// <param name="color">value from ColorType enum</param>
		/// <returns>Color object corresponding to selected ColorType</returns>
		public static Color TranslateDisplayedColor(ColorType color) {
			switch (color) {
				case ColorType.Color1:
					return Colors.Crimson;

				case ColorType.Color2:
					return Colors.SpringGreen;

				case ColorType.Color3:
					return Colors.SkyBlue;

				default:
					throw new ArgumentOutOfRangeException($"ColorType {color} not among enumerated types");
			}
		}
	}
}
