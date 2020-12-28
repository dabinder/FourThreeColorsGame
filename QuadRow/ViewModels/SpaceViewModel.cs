using QuadRow.Framework;
using QuadRow.Models;
using System.Windows;

namespace QuadRow.ViewModels {
	/// <summary>
	/// manages individual spaces on board including receiving drag-and-drop pieces
	/// </summary>
	class SpaceViewModel : ObservableObject {

		private readonly Coordinates coordinates;

		private bool _isHovered;
		/// <summary>
		/// indicates a dragged piece is currently over this space
		/// </summary>
		public bool IsHovered {
			get {
				return _isHovered;
			}
			set {
				_isHovered = value;
				NotifyPropertyChanged(nameof(IsHovered));
			}
		}

		private Piece _occupant;
		/// <summary>
		/// current piece occupying this space
		/// </summary>
		public Piece Occupant {
			get {
				return _occupant;
			}
			set {
				_occupant = value;
				NotifyPropertyChanged(nameof(Occupant));
				NotifyPropertyChanged(nameof(IsOccupied));
			}
		}

		/// <summary>
		/// indicates this space is occupied by a piece
		/// </summary>
		public bool IsOccupied {
			get {
				return Occupant != null;
			}
		}

		/// <summary>
		/// set this space's coordinates
		/// </summary>
		/// <param name="coordinates"></param>
		public SpaceViewModel(Coordinates coordinates) {
			this.coordinates = coordinates;
		}

		/// <summary>
		/// handle dragging a piece over a space
		/// highlight the space on drag in
		/// </summary>
		/// <param name="sender">space dragged over</param>
		/// <param name="e">drag event details</param>
		public void DragPieceIn(object sender, DragEventArgs e) {
			if (!IsOccupied && e.Data.GetDataPresent(typeof(ColorType))) {
				IsHovered = true;
			}
		}

		/// <summary>
		/// handle dragging a piece out from a space
		/// remove highlight on drag out
		/// </summary>
		/// <param name="sender">space being dragged over</param>
		/// <param name="e">drag event details</param>
		public void DragPieceOut(object sender, DragEventArgs e) {
			if (e.Data.GetDataPresent(typeof(ColorType))) {
				IsHovered = false;
			}
		}

		/// <summary>
		/// handle dropping a piece into a space
		/// if the space is available, play the piece from the current player's inventory and draw the played piece on the board
		/// </summary>
		/// <param name="sender">space where piece is dropped</param>
		/// <param name="e">drag event details</param>
		public void DropPiece(object sender, DragEventArgs e) {
			if (!IsOccupied && e.Data.GetDataPresent(typeof(ColorType)) && !e.Handled) {
				GameViewModel game = (GameViewModel)Application.Current.FindResource("GameViewModel");
				PlayerViewModel player = game.ActivePlayer;
				ColorType colorType = (ColorType)e.Data.GetData(typeof(ColorType));
				Occupant = player.PlayPiece(colorType, coordinates);
				
				//clear decoration
				IsHovered = false;
				e.Handled = true;
			}
		}
	}
}
