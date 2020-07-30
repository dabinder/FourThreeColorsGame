using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

/*
 * ObservableDictionary class, from https://stackoverflow.com/a/14410196/2136840
 */

namespace QuadRow.Collections {
	class ObservableDictionary<TKey, TValue> : Dictionary<TKey, TValue>, INotifyCollectionChanged, INotifyPropertyChanged {

		public ObservableDictionary() : base() { }
		public ObservableDictionary(int capacity) : base(capacity) { }
		public ObservableDictionary(IEqualityComparer<TKey> comparer) : base(comparer) { }
		public ObservableDictionary(IDictionary<TKey, TValue> dictionary) : base(dictionary) { }
		public ObservableDictionary(int capacity, IEqualityComparer<TKey> comparer) : base(capacity, comparer) { }
		public ObservableDictionary(IDictionary<TKey, TValue> dictionary, IEqualityComparer<TKey> comparer) : base(dictionary, comparer) { }

		public new TValue this[TKey key] {
			get {
				return base[key];
			}
			set {
				TValue oldValue;
				bool exist = base.TryGetValue(key, out oldValue);
				var oldItem = new KeyValuePair<TKey, TValue>(key, oldValue);
				base[key] = value;
				var newItem = new KeyValuePair<TKey, TValue>(key, value);
				if (exist) {
					this.OnCollectionChanged(new NotifyCollectionChangedEventArgs(NotifyCollectionChangedAction.Replace, newItem, oldItem, base.Keys.ToList().IndexOf(key)));
				} else {
					this.OnCollectionChanged(new NotifyCollectionChangedEventArgs(NotifyCollectionChangedAction.Add, newItem, base.Keys.ToList().IndexOf(key)));
					this.OnPropertyChanged(nameof(Count));
				}
			}
		}

		public new void Add(TKey key, TValue value) {
			if (!base.ContainsKey(key)) {
				var item = new KeyValuePair<TKey, TValue>(key, value);
				base.Add(key, value);
				this.OnCollectionChanged(new NotifyCollectionChangedEventArgs(NotifyCollectionChangedAction.Add, item, base.Keys.ToList().IndexOf(key)));
				this.OnPropertyChanged(nameof(Count));
			}
		}

		public new bool Remove(TKey key) {
			TValue value;
			if (base.TryGetValue(key, out value)) {
				var item = new KeyValuePair<TKey, TValue>(key, base[key]);
				bool result = base.Remove(key);
				this.OnCollectionChanged(new NotifyCollectionChangedEventArgs(NotifyCollectionChangedAction.Remove, item, base.Keys.ToList().IndexOf(key)));
				this.OnPropertyChanged(nameof(Count));
				return result;
			}
			return false;
		}

		public new void Clear() {
			base.Clear();
			this.OnCollectionChanged(new NotifyCollectionChangedEventArgs(NotifyCollectionChangedAction.Reset));
			this.OnPropertyChanged(nameof(Count));
		}

		public event NotifyCollectionChangedEventHandler CollectionChanged;

		protected void OnCollectionChanged(NotifyCollectionChangedEventArgs e) {
			CollectionChanged?.Invoke(this, e);
		}

		public event PropertyChangedEventHandler PropertyChanged;

		protected void OnPropertyChanged(PropertyChangedEventArgs e) {
			PropertyChanged?.Invoke(this, e);
		}
		protected void OnPropertyChanged(string propertyName) {
			OnPropertyChanged(new PropertyChangedEventArgs(propertyName));
		}
	}
}
