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
		public ObservableCollection<SpaceViewModel> GameBoard { get; }
		//public ObservableDictionary<Coordinates, SpaceViewModel> GameBoard { get; }

		public BoardViewModel() {
			GameBoard = new ObservableCollection<SpaceViewModel>();
			//GameBoard = new ObservableDictionary<Coordinates, SpaceViewModel>();
			DrawGameBoard();
		}

		private void DrawGameBoard() {
			//build spaces
			GameBoard.Clear();
			for (int x = 0; x < Config.BOARD_SIZE; x++) {
				for (int y = 0; y < Config.BOARD_SIZE; y++) {
					//GameBoard.Add(new Coordinates(x, y), model);
					GameBoard.Add(new SpaceViewModel());
				}
			}
			NotifyPropertyChanged(nameof(GameBoard));
		}
	}
}
