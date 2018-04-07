using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FourThreeColorsGame.Models;
using System.Collections.ObjectModel;

namespace FourThreeColorsGame.Framework {
	static class InventoryBuilder {
		/// <summary>
		/// color sets, workaround for psuedo const array
		/// </summary>
		private static readonly int[] colorSet1 = { 19, 13, 8 };
		private static readonly int[] colorSet2 = { 8, 13, 19 };

		/// <summary>
		/// get contents of inventory for given variant
		/// </summary>
		/// <param name="variant">variant enum value</param>
		/// <returns>enumerable set of pieces</returns>
		public static IEnumerable<Piece> GetInventoryContents(InventoryVariant variant) {
			int[] colors;

			switch (variant) {
				case InventoryVariant.Variant1 :
					colors = colorSet1;
					break;

				case InventoryVariant.Variant2 :
					colors = colorSet2;
					break;

				default :
					throw new ArgumentOutOfRangeException($"InventoryVariant {variant} not among enumerated types");
			}

			//create sets of pieces of corresponding colors
			for (int i = 1; i <= colors[0]; i++) {
				yield return new Piece(ColorType.Color1);
			}
			for (int i = 1; i <= colors[1]; i++) {
				yield return new Piece(ColorType.Color2);
			}
			for (int i = 1; i <= colors[2]; i++) {
				yield return new Piece(ColorType.Color3);
			}
		}
	}
}
