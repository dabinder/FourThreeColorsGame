using QuadRow.Framework;
using QuadRow.Models;
using System.Windows;

namespace QuadRow.ViewModels {
	class SpaceViewModel : ObservableObject {
		public string TestOrigin { get; set; }

		private bool _isHovered;
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

		public bool IsOccupied {
			get {
				return Occupant != null;
			}
		}

		public void DragPieceIn(object sender, DragEventArgs e) {
			if (!IsOccupied && e.Data.GetDataPresent(typeof(ColorType))) {
				IsHovered = true;
			}
		}

		public void DragPieceOut(object sender, DragEventArgs e) {
			if (e.Data.GetDataPresent(typeof(ColorType))) {
				IsHovered = false;
			}
		}

		public void DropPiece(object sender, DragEventArgs e) {
			if (!IsOccupied && e.Data.GetDataPresent(typeof(ColorType)) && !e.Handled) {
				//Piece piece = (Piece)e.Data.GetData(typeof(Piece));
				//Occupant = piece;
				GameViewModel game = (GameViewModel)Application.Current.FindResource("GameViewModel");
				PlayerViewModel player = game.ActivePlayer;
				ColorType colorType = (ColorType)e.Data.GetData(typeof(ColorType));
				Occupant = player.PlayPiece(colorType);

				//clear decoration
				IsHovered = false;
				e.Handled = true;
			}
		}

	}
}
