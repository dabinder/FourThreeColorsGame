using FourThreeColorsGame.Framework;
using FourThreeColorsGame.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Windows.UI;
using Windows.UI.Xaml.Data;
using System.Collections.Specialized;
using System.Windows.Input;
using Windows.UI.Xaml.Controls;
using System.ComponentModel;
using System.Collections.ObjectModel;

namespace FourThreeColorsGame.ViewModels {
	class GameViewModel : ObservableObject {
		const int BOARD_SIZE = 9;
		const int WIN_LENGTH = 4;

		#region piece counts
		public int Color1TotalPieces {
			get {
				return Player1.Inventory[0].Count + Player2.Inventory[0].Count;
			}
		}
		public int Color2TotalPieces {
			get {
				return Player1.Inventory[1].Count + Player2.Inventory[1].Count;
			}
		}
		public int Color3TotalPieces {
			get {
				return Player1.Inventory[2].Count + Player2.Inventory[2].Count;
			}
		}
		public int GrandTotalPieces {
			get {
				return Player1.Inventory.TotalCount + Player2.Inventory.TotalCount;
			}
		}
		#endregion

		#region turns
		private int _turn;
		private int Turn {
			get {
				return _turn;
			}
			set {
				_turn = value;
				CurrentPlayer = value % 2 == 1 ?
					Player1 :
					Player2;
			}
		}

		private Player _currentPlayer;
		public Player CurrentPlayer {
			get {
				return _currentPlayer;
			}
			private set {
				_currentPlayer = value;
				NotifyPropertyChanged(nameof(CurrentPlayer));
			}
		}

		private Player _winner;
		public Player Winner {
			get {
				return _winner;
			}
			private set {
				_winner = value;
				NotifyPropertyChanged(nameof(Winner));
				NotifyPropertyChanged(nameof(HasWinner));
				if (value != null) {
					GameOver = true;
				}
			}
		}

		public bool HasWinner {
			get {
				return Winner != null;
			}
		}

		private bool _gameOver;
		public bool GameOver {
			get {
				return _gameOver;
			}
			private set {
				_gameOver = value;
				NotifyPropertyChanged(nameof(GameOver));
				if (value) {
					BoardActive = false;
				}
			}
		}
		#endregion

		#region player info
		private readonly Player _player1;
		public Player Player1 {
			get {
				return _player1;
			}
		}
		private readonly Player _player2;
		public Player Player2 {
			get {
				return _player2;
			}
		}

		private bool _playerNameBoxVisible;
		public bool PlayerNameBoxVisible {
			get {
				return _playerNameBoxVisible;
			}
			set {
				_playerNameBoxVisible = value;
				NotifyPropertyChanged(nameof(PlayerNameBoxVisible));
			}
		}

		private readonly RelayCommand _hidePlayerNameBox;
		public RelayCommand HidePlayerNameBox {
			get {
				return _hidePlayerNameBox;
			}
		}

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
		#endregion

		#region game board actions
		private SpaceViewModel _currentGameSpace;
		public SpaceViewModel CurrentGameSpace {
			get {
				return _currentGameSpace;
			}
			set {
				_currentGameSpace = value;
				NotifyPropertyChanged(nameof(CurrentGameSpace));
			}
		}

		private bool _boardActive;
		public bool BoardActive {
			get {
				return _boardActive;
			}
			set {
				_boardActive = value;
				NotifyPropertyChanged(nameof(BoardActive));
				//propagate info to spaces
				foreach (SpaceViewModel space in GameBoard.Values) {
					space.BoardActive = value;
				}
			}
		}

		public ObservableDictionary<string, SpaceViewModel> GameBoard { get; }
		#endregion

		public GameViewModel() {
			//setup players
			_player1 = new Player("Player 1", InventoryVariant.Variant1);
			NotifyPropertyChanged(nameof(Player1));
			Player1.PropertyChanged += PlayerNameChanged;
			_player2 = new Player("Player 2", InventoryVariant.Variant2);
			NotifyPropertyChanged(nameof(Player2));
			Player2.PropertyChanged += PlayerNameChanged;
			_playerNameBoxVisible = true;
			_hidePlayerNameBox = new RelayCommand(param => {
				PlayerNameBoxVisible = false;
				BoardActive = true;
			});

			//setup board
			GameBoard = new ObservableDictionary<string, SpaceViewModel>();
			for (int x = 0; x < BOARD_SIZE; x++) {
				for (int y = 0; y < BOARD_SIZE; y++) {
					SpaceViewModel vm = new SpaceViewModel(x, y);
					vm.PropertyChanged += SpaceViewPropertyChanged;
					GameBoard.Add(GetCoordinateString(x, y), vm);
				}
			}
			NotifyPropertyChanged(nameof(GameBoard));

			//record total change on inventory change, set value initially
			NotifyPropertyChanged(nameof(GrandTotalPieces));
			Player1.Inventory.CollectionChanged += OnInventoryChanged;
			foreach (ObservableCollection<Piece> c in Player1.Inventory) {
				c.CollectionChanged += OnInventoryChanged;
			}
			Player2.Inventory.CollectionChanged += OnInventoryChanged;
			foreach (ObservableCollection<Piece> c in Player2.Inventory) {
				c.CollectionChanged += OnInventoryChanged;
			}

			//start first turn
			Turn = 1;
		}

		private void SpaceViewPropertyChanged(object sender, PropertyChangedEventArgs e) {
			switch (e.PropertyName) {
				case "Active":
					//update current space
					SpaceViewModel space = sender as SpaceViewModel;
					if (CurrentGameSpace != space) {
						if (CurrentGameSpace != null) {
							CurrentGameSpace.Active = false;
						}
						CurrentGameSpace = space;
						CurrentGameSpace.CurrentPlayer = CurrentPlayer;
					}
					break;

				case "Occupied":
					//if newly occupied space, check if player is winner, then advance turn marker
					if (CheckWinCondition()) {
						Winner = CurrentPlayer;
					} else if ((Player1.Inventory.TotalCount == 0 && Player2.Inventory.TotalCount == 0) ||
						Turn >= BOARD_SIZE * BOARD_SIZE
						) {
						//if inventories are empty or board is full, mark game as a draw
						GameOver = true;
					} else {
						Turn++;
					}
					break;

				default:
					//ignore
					break;
			}
		}

		private string GetCoordinateString(int x, int y) {
			return IntAlphaConverter.IntToAlpha(x).ToString() + y;
		}

		private bool CheckWinCondition() {
			int x = CurrentGameSpace.X;
			int y = CurrentGameSpace.Y;
			ColorType color = CurrentGameSpace.Occupant.Color;
			int counter;

			//horizontal
			bool left = true,
				right = true;
			counter = 1;
			for (int i = 1; i < WIN_LENGTH && (left || right); i++) {
				if (left && i <= x && GameBoard[GetCoordinateString(x - i, y)].Occupant?.Color == color) {
					//check left
					counter++;
				} else {
					left = false;
				}
				if (right && i < (BOARD_SIZE - x) && GameBoard[GetCoordinateString(x + i, y)].Occupant?.Color == color) {
					//check right
					counter++;
				} else {
					right = false;
				}

				if (counter >= WIN_LENGTH) {
					return true;
				}
			}

			//vertical
			bool up = true,
				down = true;
			counter = 1;
			for (int i = 1; i < WIN_LENGTH && (up || down); i++) {
				if (up && i <= y && GameBoard[GetCoordinateString(x, y - i)].Occupant?.Color == color) {
					//check up
					counter++;
				} else {
					up = false;
				}
				if (down && i < (BOARD_SIZE - y) && GameBoard[GetCoordinateString(x, y + i)].Occupant?.Color == color) {
					//check down
					counter++;
				} else {
					down = false;
				}

				if (counter >= WIN_LENGTH) {
					return true;
				}
			}

			//diagonal - NW/SE
			bool nw = true,
				se = true;
			counter = 1;
			for (int i = 1; i < WIN_LENGTH && (nw || se); i++) {
				if (nw && i <= x && i <= y && GameBoard[GetCoordinateString(x - i, y - i)].Occupant?.Color == color) {
					//check nw
					counter++;
				} else {
					nw = false;
				}
				if (se && i < (BOARD_SIZE - x) && i < (BOARD_SIZE - y) && GameBoard[GetCoordinateString(x + i, y + i)].Occupant?.Color == color) {
					//check se
					counter++;
				} else {
					se = false;
				}

				if (counter >= WIN_LENGTH) {
					return true;
				}
			}

			//diagonal - NE/SW
			bool ne = true,
				sw = true;
			counter = 1;
			for (int i = 1; i < WIN_LENGTH && (ne || sw); i++) {
				if (ne && i < (BOARD_SIZE - x) && i <= y && GameBoard[GetCoordinateString(x + i, y - i)].Occupant?.Color == color) {
					//check ne
					counter++;
				} else {
					ne = false;
				}
				if (sw && i < x && i < (BOARD_SIZE - y) && GameBoard[GetCoordinateString(x - i, y + i)].Occupant?.Color == color) {
					//check sw
					counter++;
				} else {
					sw = false;
				}

				if (counter >= WIN_LENGTH) {
					return true;
				}
			}

			//if we got all the way here, no winner
			return false;
		}

		/// <summary>
		/// make sure player names are valid
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		private void PlayerNameChanged(object sender, PropertyChangedEventArgs e) {
			if (e.PropertyName == "Name") {
				PlayerNameError = (Player1.Name == "" || Player2.Name == "");
			}
		}

		private void OnInventoryChanged(object sender, NotifyCollectionChangedEventArgs e) {
			NotifyPropertyChanged(nameof(GrandTotalPieces));
			NotifyPropertyChanged(nameof(Color1TotalPieces));
			NotifyPropertyChanged(nameof(Color2TotalPieces));
			NotifyPropertyChanged(nameof(Color3TotalPieces));
		}
	}
}
