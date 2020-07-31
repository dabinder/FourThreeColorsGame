using QuadRow.Framework;
using QuadRow.Models;
using System.ComponentModel;

namespace QuadRow.ViewModels
{
	public class PlayerViewModel : ObservableObject {
		public Player Player { get; }

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

		public PlayerViewModel(string name, InventoryBuilder.InventoryVariant variant) {
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
