using QuadRow.Collections;
using QuadRow.Framework;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QuadRow.ViewModels
{
	class BoardViewModel : ObservableObject {
		public ObservableDictionary<Coordinates, SpaceViewModel> GameBoard { get; }

		public BoardViewModel() {
			GameBoard = new ObservableDictionary<Coordinates, SpaceViewModel>();
			DrawGameBoard();
		}

		private void DrawGameBoard() {
			//build spaces
			GameBoard.Clear();
			for (int y = 0; y < Config.BOARD_SIZE; y++) {
				for (int x = 0; x < Config.BOARD_SIZE; x++) {
					Coordinates coords = new Coordinates(x, y);
					GameBoard.Add(coords, new SpaceViewModel() {
						TestOrigin = coords.ToString()
					});
				}
			}
			NotifyPropertyChanged(nameof(GameBoard));
		}
	}
}
