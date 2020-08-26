using QuadRow.Framework;
using QuadRow.Models;

namespace QuadRow.ViewModels
{
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
			}
		}

		public bool IsOccupied {
			get {
				return Occupant != null;
			}
		}
	}
}
