using QuadRow.Framework;

namespace QuadRow.Models
{
	class Player : ObservableObject {
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

		public Inventory Inventory { get; }

		/// <summary>
		/// set player name and inventory
		/// </summary>
		/// <param name="name">player name</param>
		/// <param name="variant">inventory variant enum value</param>
		public Player(string name, InventoryBuilder.InventoryVariant variant) {
			Name = name;
			Inventory = new Inventory(variant);
			NotifyPropertyChanged(nameof(Inventory));
		}
	}
}
