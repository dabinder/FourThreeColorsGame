using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Globalization;

namespace QuadRow.Converters.Tests {
	[TestClass()]
	public class MultiValueSumConverterTests {
		private MultiValueSumConverter converter = new MultiValueSumConverter();

		[DataTestMethod]
		[DataRow(new object[] { 1, 2, 3 }, 6)]
		[DataRow(new object[] { 0, 2, 0 }, 2)]
		public void ConvertTest(object[] values, int result) {
			Assert.AreEqual(result.ToString(), converter.Convert(values, typeof(string), null, CultureInfo.CurrentCulture));
		}
	}
}