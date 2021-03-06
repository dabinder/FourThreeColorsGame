﻿using QuadRow.Framework;

namespace QuadRow.ViewModels {
	/// <summary>
	/// manages player with inventory variant 1
	/// </summary>
	public sealed class Player1ViewModel : PlayerViewModel {
		/// <summary>
		/// create PlayerViewModel with inventory variant 1
		/// </summary>
		public Player1ViewModel() : base("Player 1", InventoryBuilder.InventoryVariant.Variant1) { }
	}
}
