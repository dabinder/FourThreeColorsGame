using QuadRow.Framework;
using QuadRow.Models;
using QuadRow.ViewModels;
using System.Windows;
using System.Windows.Controls;

namespace QuadRow.Views {
	/// <summary>
	/// Interaction logic for GameSpace.xaml
	/// </summary>
	public partial class GameSpace : UserControl {
		private bool subscribed;

		public GameSpace() {
			InitializeComponent();
			DataContextChanged += GameSpace_DataContextChanged;
		}

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
