﻿using QuadRow.Framework;
using System.Windows.Controls;
using QuadRow.Views;
using System;
using System.Windows;

namespace QuadRow.ViewModels
{
	class GameViewModel : ObservableObject
	{
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
					_closeIntroCommand = new RelayCommand(param => {
						CurrentScreen = new GameScreen();
						IsNameBoxOpen = true;
					})
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
				return (PlayerViewModel) Application.Current.FindResource("Player1ViewModel");
			}
		}
		public PlayerViewModel Player2 {
			get {
				return (PlayerViewModel) Application.Current.FindResource("Player2ViewModel");
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
				} else {
					throw new ArgumentException("Unrecognized player " + value);
				}
				_activePlayer = value;
				NotifyPropertyChanged(nameof(ActivePlayer));
			}
		}

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

		#endregion

		#region piece counts
		public int Color1TotalPieces {
			get {
				return Player1.Color1Count + Player2.Color1Count;
			}
		}
		public int Color2TotalPieces {
			get {
				return Player1.Color2Count + Player2.Color2Count;
			}
		}
		public int Color3TotalPieces {
			get {
				return Player1.Color3Count + Player2.Color3Count;
			}
		}
		public int GrandTotalPieces {
			get {
				return Player1.TotalCount + Player2.TotalCount;
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

		private void StartGame() {
			//start player 1 as active
			Turn = 1;
		}
	}
}
