using QuadRow.ViewModels;
using System.Windows.Controls;

namespace QuadRow.Views
{
	/// <summary>
	/// Interaction logic for GameSpace.xaml
	/// </summary>
	public partial class GameSpace : UserControl {
		public GameSpace() {
			InitializeComponent();
			DataContext = new SpaceViewModel();
		}
	}
}
