using QuadRow.ViewModels;
using System.Windows;
using System.Windows.Controls;

namespace QuadRow.Views {
	/// <summary>
	/// Interaction logic for GameSpace.xaml
	/// </summary>
	public partial class GameSpace : UserControl {
		private bool subscribed;

		/// <summary>
		/// create a new game space and listen for changes to the data context
		/// </summary>
		public GameSpace() {
			InitializeComponent();
			DataContextChanged += GameSpace_DataContextChanged;
		}

		/// <summary>
		/// if this space's data context is changed, subscribe/unsubscribe to events on the corresponding view model for dragging and dropping pieces into the space
		/// </summary>
		/// <param name="sender">source of data context change</param>
		/// <param name="e">property change details</param>
		private void GameSpace_DataContextChanged(object sender, DependencyPropertyChangedEventArgs e) {
			if (e.NewValue is SpaceViewModel && !subscribed) {
				SpaceViewModel model = e.NewValue as SpaceViewModel;
				DragEnter += model.DragPieceIn;
				DragLeave += model.DragPieceOut;
				Drop += model.DropPiece;
				subscribed = true;
			} else if (!(e.NewValue is SpaceViewModel) && subscribed) {
				SpaceViewModel model = e.OldValue as SpaceViewModel;
				DragEnter -= model.DragPieceIn;
				DragLeave -= model.DragPieceOut;
				Drop -= model.DropPiece;
				subscribed = false;
			}
		}
	}
}
