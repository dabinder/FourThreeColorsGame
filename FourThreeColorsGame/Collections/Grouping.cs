using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;

/*
 * Grouping class, taken from https://github.com/mikegoatly/GroupedObservableCollection
 */

namespace FourThreeColorsGame.Collections {
	public class Grouping<TKey, TValue> : ObservableCollection<TValue>, IGrouping<TKey, TValue> {
		public TKey Key { get; }

		public Grouping(TKey key) {
			this.Key = key;
		}

		public Grouping(TKey key, IEnumerable<TValue> items) : this(key) {
			foreach (var item in items) {
				this.Add(item);
			}
		}
	}
}
