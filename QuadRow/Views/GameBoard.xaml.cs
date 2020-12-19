using System.Windows;
using System.Windows.Controls;

namespace QuadRow.Views {
	/// <summary>
	/// Interaction logic for GameBoard.xaml
	/// </summary>
	public partial class GameBoard : UserControl {
		/// <summary>
		/// create a new game board and set the data context to the BoardViewModel
		/// </summary>
		public GameBoard() {
			InitializeComponent();
			DataContext = Application.Current.Resources["BoardViewModel"];
		}
	}
}
