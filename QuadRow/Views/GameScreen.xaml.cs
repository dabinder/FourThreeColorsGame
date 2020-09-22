using System.Windows;
using System.Windows.Controls;

namespace QuadRow.Views {
	/// <summary>
	/// Interaction logic for GameScreen.xaml
	/// </summary>
	public partial class GameScreen : UserControl {
		/// <summary>
		/// create a new game screen and set players' data context to PlayerViewModels
		/// </summary>
		public GameScreen() {
			InitializeComponent();
			Application currentApp = Application.Current;
			DataContext = currentApp.FindResource("GameViewModel");
			player1Panel.DataContext = currentApp.Resources["Player1ViewModel"];
			player2Panel.DataContext = currentApp.Resources["Player2ViewModel"];
		}
	}
}
