using QuadRow.Framework;
using QuadRow.Models;
using QuadRow.ViewModels;
using System.Windows.Controls;

namespace QuadRow.Views
{
	/// <summary>
	/// Interaction logic for GameScreen.xaml
	/// </summary>
	public partial class GameScreen : UserControl {
		public GameScreen(PlayerViewModel player1Model, PlayerViewModel player2Model) {
			InitializeComponent();
			player1Panel.DataContext = player1Model;
			player2Panel.DataContext = player2Model;
		}
	}
}
