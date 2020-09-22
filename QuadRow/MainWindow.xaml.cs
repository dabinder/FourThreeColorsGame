using System.Windows;

namespace QuadRow {
	/// <summary>
	/// Interaction logic for MainWindow.xaml
	/// </summary>
	public partial class MainWindow : Window {
		/// <summary>
		/// create new main application window and set its data context to GameViewModel
		/// </summary>
		public MainWindow() {
			InitializeComponent();
			DataContext = Application.Current.FindResource("GameViewModel");
		}
	}
}
