using QuadRow.Collections;
using QuadRow.Framework;
using QuadRow.Views;
using System;
using System.ComponentModel;
using System.Diagnostics;
using System.Windows;
using System.Windows.Controls;

namespace QuadRow.ViewModels {
	class GameViewModel : ObservableObject {
		#region intro screen
		private ContentControl _currentScreeen;
		public ContentControl CurrentScreen {
			get {
				return _currentScreeen;
			}
			set {
				_currentScreeen = value;
				NotifyPropertyChanged("CurrentScreen");
			}
		}

		private RelayCommand _closeIntroCommand;
		public RelayCommand CloseIntroCommand {
			get {
				return _closeIntroCommand ?? (
					_closeIntroCommand = new RelayCommand(param => ResetGame())
				);
			}
		}
		#endregion

		#region player info
		private bool _isNameBoxOpen;
		public bool IsNameBoxOpen {
			get {
				return _isNameBoxOpen;
			}
			private set {
				_isNameBoxOpen = value;
				NotifyPropertyChanged(nameof(IsNameBoxOpen));
			}
		}

		private RelayCommand _closeNameBox;
		public RelayCommand CloseNameBox {
			get {
				return _closeNameBox ?? (
					_closeNameBox = new RelayCommand(param => {
						IsNameBoxOpen = false;
						StartGame();
					})
				);
			}
		}

		public PlayerViewModel Player1 {
			get {
				return (PlayerViewModel)Application.Current.FindResource("Player1ViewModel");
			}
		}
		public PlayerViewModel Player2 {
			get {
				return (PlayerViewModel)Application.Current.FindResource("Player2ViewModel");
			}
		}

		private PlayerViewModel _activePlayer;
		public PlayerViewModel ActivePlayer {
			get {
				return _activePlayer;
			}
			private set {
				if (value == Player1) {
					Player1.Active = true;
					Player2.Active = false;
				} else if (value == Player2) {
					Player1.Active = false;
					Player2.Active = true;
				} else if (value == null) { //end of game scenario
					Player1.Active = false;
					Player2.Active = false;
				} else {
					throw new ArgumentException("Unrecognized player " + value);
				}
				_activePlayer = value;
			}
		}
		#endregion

		#region game flow

		private int _turn;
		private int Turn {
			get {
				return _turn;
			}
			set {
				_turn = value;
				ActivePlayer = value % 2 == 1 ?
					Player1 :
					Player2;
			}
		}

		private bool _isTie;
		public bool IsTie {
			get {
				return _isTie;
			}
			private set {
				_isTie = value;
				NotifyPropertyChanged(nameof(IsTie));
			}
		}

		private PlayerViewModel _winner;
		public PlayerViewModel Winner {
			get {
				return _winner;
			}
			set {
				_winner = value;
				NotifyPropertyChanged(nameof(Winner));
				NotifyPropertyChanged(nameof(HasWinner));
			}
		}

		public bool HasWinner {
			get {
				return Winner != null;
			}
		}

		private RelayCommand _restartGame;
		public RelayCommand RestartGame {
			get {
				return _restartGame ?? (
					_restartGame = new RelayCommand(param => ResetGame())
				);
			}
		}
		#endregion

		public GameViewModel() {
			//initialize game on intro screen
			ContentControl introScreen = new IntroScreen {
				DataContext = this
			};
			CurrentScreen = introScreen;
		}

		private void PlayerPropertyChanged(object sender, PropertyChangedEventArgs e) {
			if (e.PropertyName == "IsPiecePlayed" && (bool)sender.GetType().GetProperty(e.PropertyName).GetValue(sender)) {
				if (CheckWinner()) {
					Winner = ActivePlayer;
					EndGame();
				} else {
					Turn++;
					if (CheckTieGame()) {
						IsTie = true;
						EndGame();
					}
				}
			}
		}

		private void StartGame() {
			//start player 1 as active
			Turn = 1;
		}

		private void EndGame() {
			ActivePlayer = null;
		}

		private bool CheckWinner() {
			Coordinates start = ActivePlayer.LastPlayedLocation;
			ColorType color = ActivePlayer.LastPlayedColor;
			int x = start.X,
				y = start.Y;
			ObservableDictionary<Coordinates, SpaceViewModel> board = ((BoardViewModel)Application.Current.Resources["BoardViewModel"]).Board;
			int counter;

			//horizontal
			bool left = true,
				right = true;
			counter = 1;
			for (int i = 1; i < Config.WIN_LENGTH && (left || right); i++) {
				if (left && i <= x && board[new Coordinates(x - i, y)].Occupant?.Color == color) {
					counter++;
				} else {
					left = false;
				}
				if (right && i < (Config.BOARD_SIZE - x) && board[new Coordinates(x + i, y)].Occupant?.Color == color) {
					counter++;
				} else {
					right = false;
				}

				if (counter >= Config.WIN_LENGTH) {
					return true;
				}
			}

			//vertical
			bool up = true,
				down = true;
			counter = 1;
			for (int i = 1; i < Config.WIN_LENGTH && (up || down); i++) {
				if (up && i <= y && board[new Coordinates(x, y - i)].Occupant?.Color == color) {
					counter++;
				} else {
					up = false;
				}
				if (down && i < (Config.BOARD_SIZE - y) && board[new Coordinates(x, y + i)].Occupant?.Color == color) {
					counter++;
				} else {
					down = false;
				}

				if (counter >= Config.WIN_LENGTH) {
					return true;
				}
			}

			//diagonal - NW/SE
			bool nw = true,
				se = true;
			counter = 1;
			for (int i = 1; i < Config.WIN_LENGTH && (nw || se); i++) {
				if (nw && i <= x && i <= y && board[new Coordinates(x - i, y - i)].Occupant?.Color == color) {
					//check nw
					counter++;
				} else {
					nw = false;
				}
				if (se && i < (Config.BOARD_SIZE - x) && i < (Config.BOARD_SIZE - y) && board[new Coordinates(x + i, y + i)].Occupant?.Color == color) {
					//check se
					counter++;
				} else {
					se = false;
				}

				if (counter >= Config.WIN_LENGTH) {
					return true;
				}
			}

			//diagonal - NE/SW
			bool ne = true,
				sw = true;
			counter = 1;
			for (int i = 1; i < Config.WIN_LENGTH && (ne || sw); i++) {
				if (ne && i < (Config.BOARD_SIZE - x) && i <= y && board[new Coordinates(x + i, y - i)].Occupant?.Color == color) {
					//check ne
					counter++;
				} else {
					ne = false;
				}
				if (sw && i < x && i < (Config.BOARD_SIZE - y) && board[new Coordinates(x - i, y + i)].Occupant?.Color == color) {
					//check sw
					counter++;
				} else {
					sw = false;
				}

				if (counter >= Config.WIN_LENGTH) {
					return true;
				}
			}

			//if we got all the way here, no winner
			return false;
		}

		private bool CheckTieGame() {
			//if current player's inventory is empty or board is full, mark game as tie
			return (Turn > Config.BOARD_SIZE * Config.BOARD_SIZE ||
				(Player1.IsInventoryEmpty && ActivePlayer == Player1) ||
				(Player2.IsInventoryEmpty && ActivePlayer == Player2)
			);
		}

		private void ResetGame() {
			Application currentApp = Application.Current;
			currentApp.Resources["Player1ViewModel"] = new Player1ViewModel();
			currentApp.Resources["Player2ViewModel"] = new Player2ViewModel();
			currentApp.Resources["BoardViewModel"] = new BoardViewModel();
			NotifyPropertyChanged(nameof(Player1));
			NotifyPropertyChanged(nameof(Player2));
			Player1.PropertyChanged += PlayerPropertyChanged;
			Player2.PropertyChanged += PlayerPropertyChanged;
			CurrentScreen = new GameScreen();
			IsNameBoxOpen = true;
			IsTie = false;
			Winner = null;
		}
	}
}
