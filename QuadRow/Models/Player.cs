using QuadRow.Framework;

namespace QuadRow.Models {
	public class Player : ObservableObject {
		private string _name;
		public string Name {
			get {
				return _name;
			}
			set {
				_name = value;
				NotifyPropertyChanged(nameof(Name));
			}
		}

		private readonly Inventory inventory;

		/// <summary>
		/// set player name and inventory
		/// </summary>
		/// <param name="name">player name</param>
		/// <param name="variant">inventory variant enum value</param>
		public Player(string name, InventoryBuilder.InventoryVariant variant) {
			Name = name;
			inventory = new Inventory(variant);
			NotifyPropertyChanged(nameof(Inventory));
		}

		public int GetCount(ColorType colorType) {
			return inventory[colorType].Count;
		}

		public Piece PlayPiece(ColorType colorType) {
			return inventory.RemovePiece(colorType);
		}
	}
}
