using FourThreeColorsGame.Framework;
using FourThreeColorsGame.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FourThreeColorsGame.ViewModels {
	class SpaceViewModel : ObservableObject {
		private bool _occupied;
		public bool Occupied {
			get {
				return _occupied;
			}
			private set {
				_occupied = value;
				NotifyPropertyChanged(nameof(Occupied));
			}
		}

		private Piece _occupant;
		public Piece Occupant {
			get {
				return _occupant;
			}
			set {
				_occupant = value;
				Occupied = (value != null);
				NotifyPropertyChanged(nameof(Occupant));
			}
		}

		private readonly RelayCommand _selectSpace;
		public RelayCommand SelectSpace {
			get {
				return _selectSpace;
			}
		}

		private readonly RelayCommand _selectPiece;
		public RelayCommand SelectPiece {
			get {
				return _selectPiece;
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

		private Player _currentPlayer;
		public Player CurrentPlayer {
			get {
				return _currentPlayer;
			}
			set {
				_currentPlayer = value;
				NotifyPropertyChanged(nameof(CurrentPlayer));
				_selectPiece.NotifyCanExecuteChanged();
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
				_selectSpace.NotifyCanExecuteChanged();
			}
		}

		public int X { get; }
		public int Y { get; }

		public SpaceViewModel(int x, int y) {
			_selectSpace = new RelayCommand(ClickGameSpace, CanClickGameSpace);
			_selectPiece = new RelayCommand(PlayPiece, CanPlayPiece);
			X = x;
			Y = y;
		}

		private bool CanClickGameSpace(object parameter) {
			return !Occupied && BoardActive;
		}

		private void ClickGameSpace(object parameter) {
			Active = !Active;
		}

		private bool CanPlayPiece(object parameter) {
			if (CurrentPlayer == null) {
				return false;
			}
			return CurrentPlayer.Inventory[(int)parameter].Count > 0;
		}

		private void PlayPiece(object parameter) {
			//update inventory and record space occupied
			var group = CurrentPlayer.Inventory[(int)parameter];
			Piece piece = group[group.Count - 1];
			group.RemoveAt(group.Count - 1);
			Occupant = piece;

			//remove piece selection
			Active = false;
		}
	}
}
