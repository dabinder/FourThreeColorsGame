﻿using FourThreeColorsGame.Framework;
using FourThreeColorsGame.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Windows.UI;
using Windows.UI.Xaml.Data;
using System.Collections.Specialized;

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
		}

		private void OnInventoryChanged(object sender, NotifyCollectionChangedEventArgs e) {
			OnPropertyChanged(nameof(GrandTotalPieces));
			OnPropertyChanged(nameof(Color1TotalPieces));
			OnPropertyChanged(nameof(Color2TotalPieces));
			OnPropertyChanged(nameof(Color3TotalPieces));
		}
	}
}
