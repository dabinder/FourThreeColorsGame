using QuadRow.Framework;
using QuadRow.ViewModels;
using System.Windows.Controls;

namespace QuadRow.Views
{
	/// <summary>
	/// Interaction logic for GameScreen.xaml
	/// </summary>
	public partial class GameScreen : UserControl
	{
		public GameScreen() {
			InitializeComponent();
			player1Panel.DataContext = new PlayerViewModel("Player 1", InventoryBuilder.InventoryVariant.Variant1);
			player2Panel.DataContext = new PlayerViewModel("Player 2", InventoryBuilder.InventoryVariant.Variant2);
		}
	}
}
