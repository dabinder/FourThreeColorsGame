using Microsoft.VisualStudio.TestTools.UnitTesting;
using QuadRow.Converters;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QuadRow.Converters.Tests {
	[TestClass()]
	public class BooleanAndConverterTests {
		private BooleanAndConverter converter = new BooleanAndConverter();

		[DataTestMethod]
		[DataRow(new object[] { true, true, true }, true, true)]
		[DataRow(new object[] { false, false, false }, false, true)]
		[DataRow(new object[] { true, false, true }, true, false)]
		[DataRow(new object[] { true, false, true }, false, false)]
		public void ConvertTest(object[] values, bool match, bool result) {
			Assert.AreEqual(result, converter.Convert(values, typeof(bool), match, CultureInfo.CurrentCulture));
		}
	}
}