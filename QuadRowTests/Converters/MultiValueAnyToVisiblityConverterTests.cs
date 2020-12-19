using Microsoft.VisualStudio.TestTools.UnitTesting;
using QuadRow.Converters;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace QuadRow.Converters.Tests {
	[TestClass()]
	public class MultiValueAnyToVisiblityConverterTests {
		private MultiValueAnyToVisiblityConverter converter = new MultiValueAnyToVisiblityConverter();

		[DataTestMethod]
		[DataRow(new object[] { true, true, true }, true, Visibility.Visible)]
		[DataRow(new object[] { false, false, false }, false, Visibility.Visible)]
		[DataRow(new object[] { true, false, true }, true, Visibility.Visible)]
		[DataRow(new object[] { true, false, true }, false, Visibility.Visible)]
		[DataRow(new object[] { true, true, true }, false, Visibility.Hidden)]
		[DataRow(new object[] { false, false, false }, true, Visibility.Hidden)]
		public void ConvertTest(object[] values, bool match, Visibility result) {
			Assert.AreEqual(result, converter.Convert(values, typeof(Visibility), match, CultureInfo.CurrentCulture));
		}
	}
}