using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Globalization;
using System.Windows;

namespace QuadRow.Converters.Tests {
	[TestClass()]
	public class BooleanToVisibilityConverterTests {
		private BooleanToVisibilityConverter converter = new BooleanToVisibilityConverter();

		[DataTestMethod]
		[DataRow(true, Visibility.Visible)]
		[DataRow(false, Visibility.Hidden)]
		public void ConvertTest(bool value, Visibility result) {
			Assert.AreEqual(result, converter.Convert(value, typeof(Visibility), null, CultureInfo.CurrentCulture));
		}

		[DataTestMethod]
		[DataRow(Visibility.Visible, true)]
		[DataRow(Visibility.Hidden, false)]
		public void ConvertBackTest(Visibility value, bool result) {
			Assert.AreEqual(result, converter.ConvertBack(value, typeof(bool), null, CultureInfo.CurrentCulture));
		}
	}
}