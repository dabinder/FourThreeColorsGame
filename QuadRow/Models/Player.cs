using QuadRow.Framework;
using System.Collections.Generic;
using System.Collections.ObjectModel;

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

		public int Color1Count {
			get {
				return inventory[ColorType.Color1].Count;
			}
		}
		public int Color2Count {
			get {
				return inventory[ColorType.Color2].Count;
			}
		}
		public int Color3Count {
			get {
				return inventory[ColorType.Color3].Count;
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
			foreach (KeyValuePair<ColorType, ObservableCollection<Piece>> pair in inventory) {
				pair.Value.CollectionChanged += (sender, e) => {
					OnInventoryChanged(pair.Key);
				};
			}
		}

		private void OnInventoryChanged(ColorType colorType) {
			switch (colorType) {
				case ColorType.Color1:
					NotifyPropertyChanged(nameof(Color1Count));
					break;

				case ColorType.Color2:
					NotifyPropertyChanged(nameof(Color2Count));
					break;

				case ColorType.Color3:
					NotifyPropertyChanged(nameof(Color3Count));
					break;
			}
		}

		public Piece PlayPiece(ColorType colorType) {
			return inventory.RemovePiece(colorType);
		}
	}
}
