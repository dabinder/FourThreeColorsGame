using QuadRow.Framework;
using QuadRow.Models;
using QuadRow.Views;
using System.ComponentModel;
using System.Runtime.InteropServices;
using System.Windows;
using System.Windows.Documents;
using System.Windows.Input;

namespace QuadRow.ViewModels {
	public class PlayerViewModel : ObservableObject {
		private Player Player { get; }

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

		public string PlayerName {
			get {
				return Player.Name;
			}
			set {
				Player.Name = value;
				NotifyPropertyChanged(nameof(PlayerName));
			}
		}

		public int Color1Count {
			get {
				return Player.GetCount(ColorType.Color1);
			}
		}
		public int Color2Count {
			get {
				return Player.GetCount(ColorType.Color2);
			}
		}
		public int Color3Count {
			get {
				return Player.GetCount(ColorType.Color3);
			}
		}
		public int TotalCount {
			get {
				return Color1Count + Color2Count + Color3Count;
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
			Player = new Player(name, variant);
			Player.PropertyChanged += PlayerNameChanged;
			NotifyPropertyChanged(nameof(Player));
		}

		private void PlayerNameChanged(object sender, PropertyChangedEventArgs e) {
			if (e.PropertyName == "Name") {
				PlayerNameError = Player.Name == "";
			}
		}

		public void PieceMouseDown(object sender, MouseButtonEventArgs e) {
			if (Active && e.LeftButton == MouseButtonState.Pressed) {
				VisiblePiece piece = (VisiblePiece)sender;
				adornerLayer = AdornerLayer.GetAdornerLayer(piece);
				adorner = new DraggableAdorner(piece);
				adornerLayer.Add(adorner);
				isDragging = true;
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
			return Player.PlayPiece(colorType);
		}
	}
}
