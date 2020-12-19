using QuadRow.ViewModels;
using System.Windows;
using System.Windows.Controls;

namespace QuadRow.Views {
	/// <summary>
	/// Interaction logic for PlayerPanel.xaml
	/// </summary>
	public partial class PlayerPanel : UserControl {
		private bool subscribed;

		/// <summary>
		/// create new player panel and listen for changes to the data context
		/// </summary>
		public PlayerPanel() {
			InitializeComponent();
			DataContextChanged += PlayerPanel_DataContextChanged;
		}

		/// <summary>
		/// if this player panel's data context is changed, subscribe/unsubscribe to piece events on the corresponding view model for dragging visible pieces from the panel to the board
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
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
