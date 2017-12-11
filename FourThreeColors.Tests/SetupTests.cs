using System;
using Microsoft.VisualStudio.TestPlatform.UnitTestFramework;
using FourThreeColorsGame.Framework;
using FourThreeColorsGame.Models;

namespace FourThreeColors.Tests {
	[TestClass]
	public class SetupTests {
		[TestMethod]
		public void InventoryVariantsProduceCorrectSize() {
			var variants = Enum.GetValues(typeof(InventoryVariant));
			foreach (InventoryVariant variant in variants) {
				Inventory inventory = new Inventory(variant);
				Assert.AreEqual(inventory.TotalCount, Inventory.SIZE);
			}
		}

		//[TestMethod]
		//public void 
	}
}
