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
			Assert.AreEqual(Colors.Crimson, converter.Convert(ColorType.Color1, typeof(Color), null, CultureInfo.CurrentCulture));
			Assert.AreEqual(Colors.SpringGreen, converter.Convert(ColorType.Color2, typeof(Color), null, CultureInfo.CurrentCulture));
			Assert.AreEqual(Colors.SkyBlue, converter.Convert(ColorType.Color3, typeof(Color), null, CultureInfo.CurrentCulture));
		}

		[TestMethod]
		public void ConvertTest_ReturnsBrush() {
			Assert.IsTrue(BrushCompare(new SolidColorBrush(Colors.Crimson), (SolidColorBrush) converter.Convert(ColorType.Color1, typeof(Brush), null, CultureInfo.CurrentCulture)));
			Assert.IsTrue(BrushCompare(new SolidColorBrush(Colors.SpringGreen), (SolidColorBrush) converter.Convert(ColorType.Color2, typeof(Brush), null, CultureInfo.CurrentCulture)));
			Assert.IsTrue(BrushCompare(new SolidColorBrush(Colors.SkyBlue), (SolidColorBrush) converter.Convert(ColorType.Color3, typeof(Brush), null, CultureInfo.CurrentCulture)));
		}
	}
}