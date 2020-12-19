using QuadRow.Collections;
using QuadRow.Framework;

namespace QuadRow.ViewModels {
	class BoardViewModel : ObservableObject {
		public ObservableDictionary<Coordinates, SpaceViewModel> Board { get; }

		public BoardViewModel() {
			Board = new ObservableDictionary<Coordinates, SpaceViewModel>();
			DrawGameBoard();
		}

		private void DrawGameBoard() {
			//build spaces
			Board.Clear();
			for (int y = 0; y < Config.BOARD_SIZE; y++) {
				for (int x = 0; x < Config.BOARD_SIZE; x++) {
					Coordinates coords = new Coordinates(x, y);
					Board.Add(coords, new SpaceViewModel() {
						TestOrigin = coords.ToString()
					});
				}
			}
			NotifyPropertyChanged(nameof(Board));
		}
	}
}
