using QuadRow.Framework;
using QuadRow.Models;
using QuadRow.Views;
using System;
using System.ComponentModel;
using System.Runtime.InteropServices;
using System.Windows;
using System.Windows.Documents;
using System.Windows.Input;

namespace QuadRow.ViewModels {
	public class PlayerViewModel : ObservableObject {
		private readonly Player player;

		private bool _playerNameError;
		public bool PlayerNameError {
			get {
				return _playerNameError;
			}
			private set {
				_playerNameError = value;
				NotifyPropertyChanged(nameof(PlayerNameError));
			}
		}

		public string PlayerName {
			get {
				return player.Name;
			}
			set {
				player.Name = value;
				NotifyPropertyChanged(nameof(PlayerName));
			}
		}

		public int Color1Count {
			get {
				return player.Color1Count;
			}
		}
		public int Color2Count {
			get {
				return player.Color2Count;
			}
		}
		public int Color3Count {
			get {
				return player.Color3Count;
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

		private DraggableAdorner adorner;
		private AdornerLayer adornerLayer;
		private bool isDragging;

		[DllImport("user32.dll")]
		internal static extern void GetCursorPos(ref Win32Point pt);

		[StructLayout(LayoutKind.Sequential)]
		internal struct Win32Point {
			public int X;
			public int Y;
		};

		protected PlayerViewModel(string name, InventoryBuilder.InventoryVariant variant) {
			player = new Player(name, variant);
			player.PropertyChanged += PlayerPropertyChanged;
		}

		private void PlayerPropertyChanged(object sender, PropertyChangedEventArgs e) {
			switch (e.PropertyName) {
				case "Name":
					PlayerNameError = player.Name == "";
					break;

				case "Color1Count":
					NotifyPropertyChanged(nameof(Color1Count));
					break;

				case "Color2Count":
					NotifyPropertyChanged(nameof(Color2Count));
					break;

				case "Color3Count":
					NotifyPropertyChanged(nameof(Color3Count));
					break;
			}
		}

		public void PieceMouseDown(object sender, MouseButtonEventArgs e) {
			if (Active && e.LeftButton == MouseButtonState.Pressed) {
				VisiblePiece piece = (VisiblePiece)sender;
				if (player.CanPlayPiece(piece.ColorType)) {
					adornerLayer = AdornerLayer.GetAdornerLayer(piece);
					adorner = new DraggableAdorner(piece);
					adornerLayer.Add(adorner);
					isDragging = true;
				}
			}
		}

		public void PieceMouseMove(object sender, MouseEventArgs e) {
			if (isDragging) {
				VisiblePiece piece = (VisiblePiece)sender;
				DataObject data = new DataObject();
				data.SetData(typeof(ColorType), piece.ColorType);

				DragDrop.DoDragDrop(piece, data, DragDropEffects.Copy);
				adornerLayer.Remove(adorner);
				isDragging = false;
			}
		}

		public void PieceGiveFeedback(object sender, GiveFeedbackEventArgs e) {
			if (isDragging) {
				VisiblePiece piece = (VisiblePiece)sender;
				Mouse.SetCursor(Cursors.None);
				e.Handled = true;

				Win32Point win32Point = new Win32Point();
				GetCursorPos(ref win32Point);
				Point relPos = piece.PointFromScreen(new Point(win32Point.X, win32Point.Y));
				adorner.Arrange(new Rect(relPos, adorner.DesiredSize));
			}
		}

		public Piece PlayPiece(ColorType colorType) {
			return player.PlayPiece(colorType);
		}

		private void InventoryChanged(object sender, CollectionChangeEventArgs e) {
			NotifyPropertyChanged(nameof(Color1Count));
			NotifyPropertyChanged(nameof(Color2Count));
			NotifyPropertyChanged(nameof(Color3Count));
		}
	}
}
