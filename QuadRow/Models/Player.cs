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

		/// <summary>
		/// recalculate counts and notify listeners on inventory changes
		/// </summary>
		/// <param name="colorType">color of piece added to or removed from inventory</param>
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

		/// <summary>
		/// remove piece from inventory and return it
		/// </summary>
		/// <param name="colorType">color of piece to play</param>
		/// <returns>played piece</returns>
		public Piece PlayPiece(ColorType colorType) {
			return inventory.RemovePiece(colorType);
		}

		/// <summary>
		/// determine whether player currently can play a piece
		/// </summary>
		/// <param name="colorType">color of piece to play</param>
		/// <returns>player can play a piece of the specified color</returns>
		public bool CanPlayPiece(ColorType colorType) {
			return inventory[colorType].Count > 0;
		}
	}
}
