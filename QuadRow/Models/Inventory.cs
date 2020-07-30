using QuadRow.Collections;
using QuadRow.Framework;
using System;
using System.Collections.ObjectModel;
using System.ComponentModel;

namespace QuadRow.Models
{
	class Inventory : ObservableDictionary<ColorType, ObservableCollection<Piece>> {
		public int TotalCount {
			get {
				int count = 0;
				foreach (ObservableCollection<Piece> group in Values) {
					count += group.Count;
				}
				return count;
			}
		}

		/// <summary>
		/// build inventory for given variant
		/// </summary>
		/// <param name="variant">variant enum value</param>
		public Inventory(InventoryBuilder.InventoryVariant variant) : base() {
			foreach (Piece piece in InventoryBuilder.GetInventoryContents(variant)) {
				ObservableCollection<Piece> collection;
				if (!ContainsKey(piece.Color)) {
					collection = new ObservableCollection<Piece>();
					Add(piece.Color, collection);
				} else {
					collection = this[piece.Color];
				}
				collection.Add(piece);
			}

			//trigger update of total
			foreach (ObservableCollection<Piece> c in Values) {
				c.CollectionChanged += (sender, e) => {
					OnPropertyChanged(new PropertyChangedEventArgs(nameof(TotalCount)));
				};
			}
		}
	}
}
