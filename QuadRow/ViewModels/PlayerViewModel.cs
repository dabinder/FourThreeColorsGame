using QuadRow.Framework;
using QuadRow.Models;
using System.ComponentModel;

namespace QuadRow.ViewModels {
	public class PlayerViewModel : ObservableObject {
		private Player Player { get; }

		private bool _playerNameError;
		public bool PlayerNameError {
			get {
				return _playerNameError;
			}
			set {
				_playerNameError = value;
				NotifyPropertyChanged(nameof(PlayerNameError));
			}
		}

		public string PlayerName {
			get {
				return Player.Name;
			}
			set {
				Player.Name = value;
				NotifyPropertyChanged(nameof(PlayerName));
			}
		}

		public Inventory Inventory {
			get {
				return Player.Inventory;
			}
		}

		public int Color1Count {
			get {
				return Player.GetCount(ColorType.Color1);
			}
		}
		public int Color2Count {
			get {
				return Player.GetCount(ColorType.Color2);
			}
		}
		public int Color3Count {
			get {
				return Player.GetCount(ColorType.Color3);
			}
		}
		public int TotalCount {
			get {
				return Color1Count + Color2Count + Color3Count;
			}
		}

		private bool _active;
		public bool Active {
			get {
				return _active;
			}
			set {
				_active = value;
				NotifyPropertyChanged(nameof(Active));
			}
		}

		protected PlayerViewModel(string name, InventoryBuilder.InventoryVariant variant) {
			Player = new Player(name, variant);
			Player.PropertyChanged += PlayerNameChanged;
			NotifyPropertyChanged(nameof(Player));
		}

		private void PlayerNameChanged(object sender, PropertyChangedEventArgs e) {
			if (e.PropertyName == "Name") {
				PlayerNameError = Player.Name == "";
			}
		}
	}
}
