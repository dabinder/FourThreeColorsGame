using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;

/*
 * GroupedObservableCollection class, based on
 * https://github.com/mikegoatly/GroupedObservableCollection,
 * http://blog.nicholasrogoff.com/2011/01/28/creating-an-easy-grouped-observablecollection-for-the-longlistselector/,
 * and https://stackoverflow.com/a/35006856/2136840
 */

namespace FourThreeColorsGame.Collections
{
	public class GroupedObservableCollection<TKey, TValue> : ObservableCollection<Grouping<TKey, TValue>> where TKey : IComparable<TKey> {
		private readonly Func<TValue, TKey> readKey;

		/// <summary>
		/// This is used as an optimisation for when items are likely to be added in key order and there is a good probability
		/// that when an item is added, then next one will be in the same grouping.
		/// </summary>
		private Grouping<TKey, TValue> lastAffectedGroup;

		public GroupedObservableCollection(Func<TValue, TKey> readKey) {
			this.readKey = readKey;
		}

		public GroupedObservableCollection(Func<TValue, TKey> readKey, IEnumerable<TValue> items) : this(readKey) {
			foreach (var item in items) {
				this.Add(item);
			}
		}

		public bool Contains(TValue item) {
			return this.Contains(item, (a, b) => a.Equals(b));
		}

		public bool Contains(TValue item, Func<TValue, TValue, bool> compare) {
			var key = this.readKey(item);
			var group = this.TryFindGroup(key);
			return group != null && group.Any(i => compare(item, i));
		}

		public void Add(TValue item) {
			var key = this.readKey(item);
			this.FindOrCreateGroup(key).Add(item);
		}

		public IEnumerable<TKey> Keys => this.Select(i => i.Key);

		private Grouping<TKey, TValue> TryFindGroup(TKey key) {
			if (this.lastAffectedGroup != null && this.lastAffectedGroup.Key.Equals(key)) {
				return this.lastAffectedGroup;
			}

			var group = this.FirstOrDefault(i => i.Key.Equals(key));

			this.lastAffectedGroup = group;

			return group;
		}

		private Grouping<TKey, TValue> FindOrCreateGroup(TKey key) {
			Grouping<TKey, TValue> group = this.TryFindGroup(key);
			if (group == null) {
				//group doesn't exist
				group = new Grouping<TKey, TValue>(key);
				this.Add(group);

				this.lastAffectedGroup = group;
			}

			return group;
		}
	}
}
