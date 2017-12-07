using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;

/*
 * grouping, based on https://montemagno.com/enhancing-xamarin-forms-listview-with-grouping/
 */

namespace FourThreeColorsGame.Collections {
	public class Grouping<TKey, TValue> : ObservableCollection<TValue>, IGrouping<TKey, TValue> {
		public TKey Key { get; private set; }

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
