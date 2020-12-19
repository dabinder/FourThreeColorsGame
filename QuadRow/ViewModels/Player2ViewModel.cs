using QuadRow.Framework;

namespace QuadRow.ViewModels {
	public sealed class Player2ViewModel : PlayerViewModel {
		/// <summary>
		/// create PlayerViewModel with inventory variant 2
		/// </summary>
		public Player2ViewModel() : base("Player 2", InventoryBuilder.InventoryVariant.Variant2) { }
	}
}
