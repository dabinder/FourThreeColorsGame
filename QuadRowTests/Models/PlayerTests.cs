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
	public class PlayerTests {
		[DataTestMethod]
		[DataRow(ColorType.Color1)]
		[DataRow(ColorType.Color2)]
		[DataRow(ColorType.Color3)]
		public void PlayPieceTest(ColorType color) {
			Player player = new Player("TestPlayer", Framework.InventoryBuilder.InventoryVariant.Variant1);
			Piece piece = player.PlayPiece(color);
			Assert.AreEqual(color, piece.Color);
		}

		[DataTestMethod]
		[DataRow(ColorType.Color1)]
		[DataRow(ColorType.Color2)]
		[DataRow(ColorType.Color3)]
		public void CanPlayPieceTest_Start_ReturnsTrue(ColorType color) {
			Player player = new Player("TestPlayer", Framework.InventoryBuilder.InventoryVariant.Variant1);
			Assert.IsTrue(player.CanPlayPiece(color));
		}

		[TestMethod]
		public void CanPlayPieceTest_LastPieceOfType_ReturnsTrue() {
			Player player = new Player("TestPlayer", Framework.InventoryBuilder.InventoryVariant.Variant1);
			while(player.Color1Count > 1) {
				player.PlayPiece(ColorType.Color1);
			}
			Assert.IsTrue(player.CanPlayPiece(ColorType.Color1));
		}

		[TestMethod]
		public void CanPlayPieceTest_NoPiecesOfType_ReturnsFalse() {
			Player player = new Player("TestPlayer", Framework.InventoryBuilder.InventoryVariant.Variant1);
			while(player.Color1Count > 0) {
				player.PlayPiece(ColorType.Color1);
			}
			Assert.IsFalse(player.CanPlayPiece(ColorType.Color1));
		}
	}
}