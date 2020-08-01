using QuadRow.ViewModels;
using System.Windows.Controls;

namespace QuadRow.Views
{
	/// <summary>
	/// Interaction logic for GameBoard.xaml
	/// </summary>
	public partial class GameBoard : UserControl
	{
		public GameBoard() {
			InitializeComponent();
			DataContext = new BoardViewModel();
		}
	}
}
