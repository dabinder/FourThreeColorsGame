using FourThreeColorsGame.Models;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FourThreeColorsGame.Framework;

namespace FourThreeColorsGame.Models {
	class Player : ObservableObject {
		private string _name;
		public string Name {
			get {
				return _name;
			}
			set {
				_name = value;
				NotifyPropertyChanged(nameof(Name));
			}
		}

		private Inventory _inventory;
		public Inventory Inventory {
			get {
				return _inventory;
			}
		}

		public Player(string name, InventoryVariant variant) {
			Name = name;
			_inventory = new Inventory(variant);
			NotifyPropertyChanged(nameof(Inventory));
		}
	}
}
