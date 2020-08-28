using QuadRow.ViewModels;
using System.Windows;
using System.Windows.Controls;

namespace QuadRow.Views {
	/// <summary>
	/// Interaction logic for PlayerPanel.xaml
	/// </summary>
	public partial class PlayerPanel : UserControl {
		public PlayerPanel() {
			InitializeComponent();
			DataContextChanged += PlayerPanel_DataContextChanged;
		}

		private void PlayerPanel_DataContextChanged(object sender, DependencyPropertyChangedEventArgs e) {
			if (DataContext is PlayerViewModel) {
				PlayerViewModel model = DataContext as PlayerViewModel;
				foreach (VisiblePiece piece in new VisiblePiece[] { pieceType1, pieceType2, pieceType3 }) {
					piece.MouseDown += model.PieceMouseDown;
					piece.MouseMove += model.PieceMouseMove;
					piece.GiveFeedback += model.PieceGiveFeedback;
				}
			}
		}
	}
}
