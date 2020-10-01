using QuadRow.Collections;
using QuadRow.Framework;

namespace QuadRow.ViewModels {
	class BoardViewModel : ObservableObject {
		/// <summary>
		/// active game board
		/// </summary>
		public ObservableDictionary<Coordinates, SpaceViewModel> Board { get; }

		/// <summary>
		/// create and display new game board
		/// </summary>
		public BoardViewModel() {
			Board = new ObservableDictionary<Coordinates, SpaceViewModel>();

			//build spaces
			Board.Clear();
			for (int y = 0; y < Config.BOARD_SIZE; y++) {
				for (int x = 0; x < Config.BOARD_SIZE; x++) {
					Coordinates coords = new Coordinates(x, y);
					Board.Add(coords, new SpaceViewModel(coords));
				}
			}
			NotifyPropertyChanged(nameof(Board));
		}
	}
}
