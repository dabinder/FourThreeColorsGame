using Microsoft.VisualStudio.TestTools.UnitTesting;
using QuadRow.Framework;
using System.Globalization;
using System.Windows.Media;

namespace QuadRow.Converters.Tests {
	[TestClass()]
	public class ColorTypeToColorConverterTests {
		private ColorTypeToColorConverter converter = new ColorTypeToColorConverter();

		private bool BrushCompare(SolidColorBrush a, SolidColorBrush b) {
			return a.Color == b.Color && a.Opacity == b.Opacity;
		}

		[TestMethod]
		public void ConvertTest_ReturnsColor() {
			Assert.AreEqual(Config.color1, converter.Convert(ColorType.Color1, typeof(Color), null, CultureInfo.CurrentCulture));
			Assert.AreEqual(Config.color2, converter.Convert(ColorType.Color2, typeof(Color), null, CultureInfo.CurrentCulture));
			Assert.AreEqual(Config.color3, converter.Convert(ColorType.Color3, typeof(Color), null, CultureInfo.CurrentCulture));
		}

		[TestMethod]
		public void ConvertTest_ReturnsBrush() {
			Assert.IsTrue(BrushCompare(new SolidColorBrush(Config.color1), (SolidColorBrush) converter.Convert(ColorType.Color1, typeof(Brush), null, CultureInfo.CurrentCulture)));
			Assert.IsTrue(BrushCompare(new SolidColorBrush(Config.color2), (SolidColorBrush) converter.Convert(ColorType.Color2, typeof(Brush), null, CultureInfo.CurrentCulture)));
			Assert.IsTrue(BrushCompare(new SolidColorBrush(Config.color3), (SolidColorBrush) converter.Convert(ColorType.Color3, typeof(Brush), null, CultureInfo.CurrentCulture)));
		}
	}
}