using QuadRow.Framework;
using System.Windows.Controls;
using QuadRow.Views;

namespace QuadRow.ViewModels
{
	class GameViewModel : ObservableObject
	{
		#region intro screen
		private ContentControl _currentScreeen;
		internal ContentControl CurrentScreen {
			get {
				return _currentScreeen;
			}
			set {
				_currentScreeen = value;
				NotifyPropertyChanged("CurrentScreen");
			}
		}

		private RelayCommand _closeIntroCommand;
		internal RelayCommand CloseIntroCommand {
			get {
				return _closeIntroCommand ?? (
					_closeIntroCommand = new RelayCommand(param => {
						GameScreen gameScreen = new GameScreen(Player1ViewModel, Player2ViewModel) {
							DataContext = this
						};
						CurrentScreen = gameScreen;
						IsNameBoxOpen = true;
					})
				);
			}
		}
		#endregion

		#region player info
		private bool _isNameBoxOpen;
		internal bool IsNameBoxOpen {
			get {
				return _isNameBoxOpen;
			}
			private set {
				_isNameBoxOpen = value;
				NotifyPropertyChanged(nameof(IsNameBoxOpen));
			}
		}

		private RelayCommand _closeNameBox;
		internal RelayCommand CloseNameBox {
			get {
				return _closeNameBox ?? (
					_closeNameBox = new RelayCommand(param => {
						IsNameBoxOpen = false;
					})
				);
			}
		}

		internal PlayerViewModel Player1ViewModel { get; }
		internal PlayerViewModel Player2ViewModel { get; }
		#endregion

		internal GameViewModel() {
			//initialize game on intro screen
			ContentControl introScreen = new IntroScreen {
				DataContext = this
			};
			CurrentScreen = introScreen;

			//create players
			Player1ViewModel = new PlayerViewModel("Player 1", InventoryBuilder.InventoryVariant.Variant1);
			Player2ViewModel = new PlayerViewModel("Player 2", InventoryBuilder.InventoryVariant.Variant2);
		}
	}
}
