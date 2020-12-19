using Microsoft.VisualStudio.TestTools.UnitTesting;
using QuadRow.Framework;
using QuadRow.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QuadRow.Models.Tests {
	[TestClass()]
	public class InventoryTests {
		private Inventory inventory = new Inventory(InventoryBuilder.InventoryVariant.Variant1);

		[DataTestMethod]
		[DataRow(ColorType.Color1)]
		[DataRow(ColorType.Color2)]
		[DataRow(ColorType.Color3)]
		public void RemovePieceTest(ColorType color) {
			Piece piece = inventory.RemovePiece(color);
			Assert.AreEqual(color, piece.Color);
		}
	}
}