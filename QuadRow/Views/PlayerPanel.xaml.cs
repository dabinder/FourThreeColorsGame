using QuadRow.ViewModels;
using System.Windows;
using System.Windows.Controls;

namespace QuadRow.Views {
	/// <summary>
	/// Interaction logic for PlayerPanel.xaml
	/// </summary>
	public partial class PlayerPanel : UserControl {
		private bool subscribed;

		public PlayerPanel() {
			InitializeComponent();
			DataContextChanged += PlayerPanel_DataContextChanged;
		}

		private void PlayerPanel_DataContextChanged(object sender, DependencyPropertyChangedEventArgs e) {
			if (e.NewValue is PlayerViewModel && !subscribed) {
				PlayerViewModel model = e.NewValue as PlayerViewModel;
				foreach (VisiblePiece piece in new VisiblePiece[] { pieceType1, pieceType2, pieceType3 }) {
					piece.MouseDown += model.PieceMouseDown;
					piece.MouseMove += model.PieceMouseMove;
					piece.GiveFeedback += model.PieceGiveFeedback;
				}
				subscribed = true;
			} else if (!(e.NewValue is PlayerViewModel) && subscribed) {
				PlayerViewModel model = e.OldValue as PlayerViewModel;
				foreach (VisiblePiece piece in new VisiblePiece[] { pieceType1, pieceType2, pieceType3 }) {
					piece.MouseDown -= model.PieceMouseDown;
					piece.MouseMove -= model.PieceMouseMove;
					piece.GiveFeedback -= model.PieceGiveFeedback;
				}
				subscribed = false;
			}
		}
	}
}
