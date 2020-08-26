using QuadRow.Framework;
using QuadRow.Models;
using QuadRow.ViewModels;
using System.Windows;
using System.Windows.Controls;

namespace QuadRow.Views {
	/// <summary>
	/// Interaction logic for GameScreen.xaml
	/// </summary>
	public partial class GameScreen : UserControl {
		public GameScreen() {
			InitializeComponent();
			Application currentApp = Application.Current;
			DataContext = currentApp.FindResource("GameViewModel");
			player1Panel.DataContext = currentApp.FindResource("Player1ViewModel");
			player2Panel.DataContext = currentApp.FindResource("Player2ViewModel");
		}
	}
}
