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

namespace FourThreeColorsGame.ViewModels {
	class GameViewModel : ObservableObject {
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
		private int turnCount;
		public Player CurrentPlayer {
			get {
				return turnCount % 2 == 1 ?
					Player1 :
					Player2;
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
		#endregion

		#region game board actions
		private readonly RelayCommand _gameSpaceClick;
		public RelayCommand GameSpaceClick {
			get {
				return _gameSpaceClick;
			}
		}

		private bool _pieceSelectionVisible = false;
		public bool PieceSelectionVisible {
			get {
				return _pieceSelectionVisible;
			}
			set {
				_pieceSelectionVisible = value;
				OnPropertyChanged(nameof(PieceSelectionVisible));
			}
		}
		#endregion

		public GameViewModel() {
			//setup players
			_player1 = new Player("Player 1", InventoryVariant.Variant1);
			OnPropertyChanged(nameof(Player1));
			_player2 = new Player("Player 2", InventoryVariant.Variant2);
			OnPropertyChanged(nameof(Player2));

			//record total change on inventory change, set value initially
			OnPropertyChanged(nameof(GrandTotalPieces));
			Player1.Inventory.CollectionChanged += OnInventoryChanged;
			Player2.Inventory.CollectionChanged += OnInventoryChanged;

			//start first turn
			turnCount = 1;
			OnPropertyChanged(nameof(CurrentPlayer));

			//initialize click listener
			_gameSpaceClick = new RelayCommand(param => this.SelectPiece());
		}

		private void SelectPiece() {
			PieceSelectionVisible = true;
		}

		private void OnInventoryChanged(object sender, NotifyCollectionChangedEventArgs e) {
			OnPropertyChanged(nameof(GrandTotalPieces));
			OnPropertyChanged(nameof(Color1TotalPieces));
			OnPropertyChanged(nameof(Color2TotalPieces));
			OnPropertyChanged(nameof(Color3TotalPieces));
		}
	}
}
