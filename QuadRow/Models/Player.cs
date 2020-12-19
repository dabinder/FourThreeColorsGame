using QuadRow.Framework;
using System.Collections.Generic;
using System.Collections.ObjectModel;

namespace QuadRow.Models {
	public class Player : ObservableObject {
		private string _name;
		/// <summary>
		/// name of player
		/// </summary>
		public string Name {
			get  => _name;
			set {
				_name = value;
				NotifyPropertyChanged(nameof(Name));
			}
		}

		/// <summary>
		/// current total of color 1 pieces
		/// </summary>
		public int Color1Count {
			get => inventory[ColorType.Color1].Count;
		}

		/// <summary>
		/// current total of color 2 pieces
		/// </summary>
		public int Color2Count {
			get => inventory[ColorType.Color2].Count;
		}

		/// <summary>
		/// current toal of color 3 pieces
		/// </summary>
		public int Color3Count {
			get => inventory[ColorType.Color3].Count;
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
