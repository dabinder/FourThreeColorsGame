using FourThreeColorsGame.Collections;
using FourThreeColorsGame.Framework;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Collections.Specialized;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Windows.UI.Xaml.Data;

namespace FourThreeColorsGame.Models {
	class Inventory : GroupedObservableCollection<int, Piece> {
		internal const int SIZE = 40;

		public int TotalCount {
			get {
				int count = 0;
				foreach (Grouping<int, Piece> group in this) {
					count += group.Count;
				}
				return count;
			}
		}

		public Inventory(InventoryVariant variant) : base(piece => Convert.ToInt32(piece.Color)) {
			foreach (Piece piece in InventoryBuilder.GetInventoryContents(variant)) {
				this.Add(piece);
			}
			
			if (TotalCount != SIZE) {
				throw new ArithmeticException("Total number of pieces must add up to exactly " + SIZE);
			}

			//trigger update of total
			foreach (ObservableCollection<Piece> c in this) {
				c.CollectionChanged += (sender, e) => {
					OnPropertyChanged(new PropertyChangedEventArgs(nameof(TotalCount)));
				};
			}
		}
	}
}
