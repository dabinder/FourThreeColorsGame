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
			private set {
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
					char colName = IntAlphaConverter.IntToAlpha(x);
					SpaceViewModel vm = new SpaceViewModel();
					vm.PropertyChanged += SpaceViewPropertyChanged;
					GameBoard.Add(colName.ToString() + y, vm);
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
					//if newly occupied space, advance turn marker
					Turn++;
					break;

				default:
					//ignore
					break;
			}
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
