using System;
using System.Windows.Media;

namespace QuadRow.Framework
{
	static class DisplayedColors {
		/// <summary>
		/// translate ColorType into displayable color
		/// </summary>
		/// <param name="colorType">value from ColorType enum</param>
		/// <returns>Color object corresponding to selected ColorType</returns>
		public static Color TranslateDisplayedColor(ColorType colorType) {
			switch (colorType) {
				case ColorType.Color1:
					return Config.color1;

				case ColorType.Color2:
					return Config.color2;

				case ColorType.Color3:
					return Config.color3;

				default:
					throw new ArgumentOutOfRangeException($"ColorType {colorType} not among enumerated types");
			}
		}
	}
}
