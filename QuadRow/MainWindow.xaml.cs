using System.Windows;

namespace QuadRow {
	/// <summary>
	/// Interaction logic for MainWindow.xaml
	/// </summary>
	public partial class MainWindow : Window {
		public MainWindow() {
			InitializeComponent();
			DataContext = Application.Current.FindResource("GameViewModel");
		}
	}
}
