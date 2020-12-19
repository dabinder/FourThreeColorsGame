using QuadRow.Collections;
using QuadRow.Framework;
using QuadRow.Views;
using System;
using System.ComponentModel;
using System.Windows;
using System.Windows.Controls;

namespace QuadRow.ViewModels {
	class GameViewModel : ObservableObject {
		#region intro screen
		private ContentControl _currentScreeen;
		/// <summary>
		/// currently displayed screen
		/// </summary>
		public ContentControl CurrentScreen {
			get => _currentScreeen;
			set {
				_currentScreeen = value;
				NotifyPropertyChanged("CurrentScreen");
			}
		}

		private RelayCommand _closeIntroCommand;
		/// <summary>
		/// command to close the intro screen and prepare a new game
		/// </summary>
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
		/// <summary>
		/// indicates whether the player name entry box is currently displayed on screen
		/// </summary>
		public bool IsNameBoxOpen {
			get => _isNameBoxOpen;
			private set {
				_isNameBoxOpen = value;
				NotifyPropertyChanged(nameof(IsNameBoxOpen));
			}
		}

		private RelayCommand _closeNameBox;
		/// <summary>
		/// command to close the player name entry box and start the game
		/// </summary>
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

		/// <summary>
		/// game player 1; uses globally shared resource for the player 1 view model
		/// </summary>
		public PlayerViewModel Player1 {
			get => (PlayerViewModel)Application.Current.FindResource("Player1ViewModel");
		}

		/// <summary>
		/// game player 2; uses globally shared resource for the player 2 view model
		/// </summary>
		public PlayerViewModel Player2 {
			get => (PlayerViewModel)Application.Current.FindResource("Player2ViewModel");
		}

		private PlayerViewModel _activePlayer;
		/// <summary>
		/// current player
		/// this will alternate back and forth each turn
		/// </summary>
		public PlayerViewModel ActivePlayer {
			get => _activePlayer;
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
		/// <summary>
		/// current game turn
		/// the active player will be player 1 for odd numbered turns; 2 for even-numbered turns
		/// </summary>
		private int Turn {
			get => _turn;
			set {
				_turn = value;
				ActivePlayer = value % 2 == 1 ?
					Player1 :
					Player2;
			}
		}

		private bool _isTie;
		/// <summary>
		/// indicates game has ended in a tie
		/// </summary>
		public bool IsTie {
			get => _isTie;
			private set {
				_isTie = value;
				NotifyPropertyChanged(nameof(IsTie));
			}
		}

		private PlayerViewModel _winner;
		/// <summary>
		/// indicates which player has won the game
		/// </summary>
		public PlayerViewModel Winner {
			get => _winner;
			set {
				_winner = value;
				NotifyPropertyChanged(nameof(Winner));
				NotifyPropertyChanged(nameof(HasWinner));
			}
		}

		/// <summary>
		/// indicates whether the game has a winner
		/// </summary>
		public bool HasWinner {
			get => Winner != null;
		}

		private PlayerViewModel _previousPlayer;

		private bool _isRestartBoxOpen;
		/// <summary>
		/// indicates whether confirmation popup to restart game is currently displayed on the screen
		/// </summary>
		public bool IsRestartBoxOpen {
			get => _isRestartBoxOpen;
			private set {
				_isRestartBoxOpen = value;
				if (value) _previousPlayer = _activePlayer;
				ActivePlayer = null;
				NotifyPropertyChanged(nameof(IsRestartBoxOpen));
			}
		}

		private RelayCommand _confirmRestart;
		/// <summary>
		/// confirms player wishes to restart game
		/// </summary>
		public RelayCommand ConfirmRestart {
			get {
				return _confirmRestart ?? (
					_confirmRestart = new RelayCommand(param => IsRestartBoxOpen = true)
				);
			}
		}

		private RelayCommand _cancelRestart;
		/// <summary>
		/// cancels request to restart game
		/// </summary>
		public RelayCommand CancelRestart {
			get {
				return _cancelRestart ?? (
					_cancelRestart = new RelayCommand(param => {
						IsRestartBoxOpen = false;
						ActivePlayer = _previousPlayer;
					})
				);
			}
		}

		private RelayCommand _restartGame;
		/// <summary>
		/// command to setup a new game
		/// </summary>
		public RelayCommand RestartGame {
			get {
				return _restartGame ?? (
					_restartGame = new RelayCommand(param => ResetGame())
				);
			}
		}
		#endregion

		/// <summary>
		/// initialize game on intro screen
		/// </summary>
		public GameViewModel() {
			ContentControl introScreen = new IntroScreen {
				DataContext = this
			};
			CurrentScreen = introScreen;
		}

		/// <summary>
		/// listen for changes to IsPieceChanged player property
		/// check for winner or tie with each played piece
		/// </summary>
		/// <param name="sender">player with changed property</param>
		/// <param name="e">details of changed property</param>
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

		/// <summary>
		/// start player 1 as active
		/// </summary>
		private void StartGame() => Turn = 1;

		/// <summary>
		/// set active player to no one
		/// </summary>
		private void EndGame() => ActivePlayer = null;

		/// <summary>
		/// check if the current player has won the game with the latest played piece
		/// a win is declared with 4 in a row either horizontally, vertically, or diagonally
		/// </summary>
		/// <returns>current player has won</returns>
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

		/// <summary>
		/// if current player's inventory is empty or board is full, mark game as tie
		/// </summary>
		/// <returns>game has ended in a tie</returns>
		private bool CheckTieGame() {
			return (Turn > Config.BOARD_SIZE * Config.BOARD_SIZE ||
				(Player1.IsInventoryEmpty && ActivePlayer == Player1) ||
				(Player2.IsInventoryEmpty && ActivePlayer == Player2)
			);
		}

		/// <summary>
		/// reset board and player parameters to prepare for a new game
		/// </summary>
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
			IsRestartBoxOpen = false;
			IsNameBoxOpen = true;
			IsTie = false;
			Winner = null;
		}
	}
}
