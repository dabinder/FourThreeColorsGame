using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using QuadRow.Framework;

namespace QuadRow.ViewModels {
	public class Player1ViewModel : PlayerViewModel {
		public Player1ViewModel() : base("Player 1", InventoryBuilder.InventoryVariant.Variant1) {}
	}
}
