using QuadRow.Framework;
using QuadRow.Models;
using QuadRow.Views;
using System.ComponentModel;
using System.Runtime.InteropServices;
using System.Windows;
using System.Windows.Documents;
using System.Windows.Input;

namespace QuadRow.ViewModels {
	/// <summary>
	/// manages Player models and serves as view model for PlayerPanel user control
	/// 
	/// drag-and-drop code borrowed from https://stackoverflow.com/a/27975085/2136840
	/// </summary>
	abstract public class PlayerViewModel : ObservableObject {
		private readonly Player player;

		/// <summary>
		/// indicates whether player has an invalid name entered (name field is blank)
		/// </summary>
		public bool PlayerNameError {
			get => player.Name == "";
		}

		/// <summary>
		/// player's name
		/// </summary>
		public string PlayerName {
			get => player.Name;
			set {
				player.Name = value;
				NotifyPropertyChanged(nameof(PlayerName));
			}
		}

		/// <summary>
		/// current total of color 1 pieces
		/// </summary>
		public int Color1Count {
			get => player.Color1Count;
		}

		/// <summary>
		/// current total of color 2 pieces
		/// </summary>
		public int Color2Count {
			get => player.Color2Count;
		}

		/// <summary>
		/// current total of color 3 pieces
		/// </summary>
		public int Color3Count {
			get => player.Color3Count;
		}
		
		/// <summary>
		/// indicates player has at least one color 1 piece remaining
		/// </summary>
		public bool HasColor1 {
			get => Color1Count > 0;
		}

		/// <summary>
		/// indicates player has at least one color 2 piece remaining
		/// </summary>
		public bool HasColor2 {
			get => Color2Count > 0;
		}

		/// <summary>
		/// indicates player has at least one color 3 piece remaining
		/// </summary>
		public bool HasColor3 {
			get => Color3Count > 0;
		}

		/// <summary>
		/// indicates inventory has no remaining pieces of any color
		/// </summary>
		public bool IsInventoryEmpty {
			get => !HasColor1 && !HasColor2 && !HasColor3;
		}

		private bool _active;
		/// <summary>
		/// player is currently active and permitted to play a piece
		/// </summary>
		public bool Active {
			get => _active;
			set {
				_active = value;
				NotifyPropertyChanged(nameof(Active));
				if (value) IsPiecePlayed = false;
			}
		}

		private bool _isPiecePlayed;
		/// <summary>
		/// indicates player has played a piece this turn
		/// </summary>
		public bool IsPiecePlayed { 
			get => _isPiecePlayed;
			private set {
				_isPiecePlayed = value;
				NotifyPropertyChanged(nameof(IsPiecePlayed));
			}
		}

		/// <summary>
		/// board coordinates of player's most recently played piece
		/// </summary>
		public Coordinates LastPlayedLocation { get; private set; }

		/// <summary>
		/// color of player's most recently played piece
		/// </summary>
		public ColorType LastPlayedColor { get; private set; }

		private DraggableAdorner adorner;
		private AdornerLayer adornerLayer;
		private bool isDragging;

		/// <summary>
		/// identify position of cursor for given point
		/// </summary>
		/// <param name="pt">point of reference</param>
		[DllImport("user32.dll")]
		internal static extern void GetCursorPos(ref Win32Point pt);

		/// <summary>
		/// X, Y coordinates to identify cursor position
		/// </summary>
		[StructLayout(LayoutKind.Sequential)]
		internal struct Win32Point {
			public int X, Y;
		};

		/// <summary>
		/// create new player and listen for property changes
		/// </summary>
		/// <param name="name">initial name of player</param>
		/// <param name="variant">inventory variant for player</param>
		protected PlayerViewModel(string name, InventoryBuilder.InventoryVariant variant) {
			player = new Player(name, variant);
			player.PropertyChanged += PlayerPropertyChanged;
		}

		/// <summary>
		/// listen for changes to player properties:
		/// 
		///		Name: check for valid name entered
		///		Color1/2/3Count: update local count of color and notify listeners
		/// </summary>
		/// <param name="sender">player with property change</param>
		/// <param name="e">details of property change</param>
		private void PlayerPropertyChanged(object sender, PropertyChangedEventArgs e) {
			switch (e.PropertyName) {
				case "Name":
					NotifyPropertyChanged(nameof(PlayerNameError));
					break;

				case "Color1Count":
					NotifyPropertyChanged(nameof(Color1Count));
					NotifyPropertyChanged(nameof(HasColor1));
					break;

				case "Color2Count":
					NotifyPropertyChanged(nameof(Color2Count));
					NotifyPropertyChanged(nameof(HasColor2));
					break;

				case "Color3Count":
					NotifyPropertyChanged(nameof(Color3Count));
					NotifyPropertyChanged(nameof(HasColor3));
					break;
			}
		}

		/// <summary>
		/// action on pressing down the mouse button on a piece
		/// copy visible piece for drag-and-drop effect
		/// </summary>
		/// <param name="sender">visible piece clicked on</param>
		/// <param name="e">mouse button action details</param>
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

		/// <summary>
		/// action on moving the mouse while holding a piece
		/// if currently dragging a piece, moving the mouse will visibly drag the piece across the screen
		/// </summary>
		/// <param name="sender">piece being dragged</param>
		/// <param name="e">mouse move action details</param>
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

		/// <summary>
		/// action on drag-and-drop event while holding a piece
		/// </summary>
		/// <param name="sender">piece being dragged</param>
		/// <param name="e">feedback event details</param>
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

		/// <summary>
		/// play piece from inventory and return it
		/// </summary>
		/// <param name="colorType">color of piece to play</param>
		/// <param name="coordinates">destination on board to place piece</param>
		/// <returns>piece played</returns>
		public Piece PlayPiece(ColorType colorType, Coordinates coordinates) {
			LastPlayedLocation = coordinates;
			LastPlayedColor = colorType;
			IsPiecePlayed = true;
			return player.PlayPiece(colorType);
		}
	}
}
