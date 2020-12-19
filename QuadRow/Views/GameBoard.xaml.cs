using System.Windows;
using System.Windows.Controls;

namespace QuadRow.Views {
	/// <summary>
	/// Interaction logic for GameBoard.xaml
	/// </summary>
	public partial class GameBoard : UserControl {
		public GameBoard() {
			InitializeComponent();
			DataContext = Application.Current.Resources["BoardViewModel"];
		}
	}
}
