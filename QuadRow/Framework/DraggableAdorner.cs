using QuadRow.Views;
using System.Windows;
using System.Windows.Documents;
using System.Windows.Media;

namespace QuadRow.Framework {
	/// <summary>
	/// display visible piece while dragging
	/// 
	/// code borrowed from:
	///		https://social.msdn.microsoft.com/Forums/en-US/81eca7d5-88d7-477a-8cdb-cfb9e8b75379/how-to-add-controls-to-adorner?forum=wpf,
	///		https://www.wundervisionenvisionthefuture.com/post/wpf-c-drag-and-drop-icon-adorner
	/// </summary>
	class DraggableAdorner : Adorner {
		private VisualCollection visualChildren;
		private VisiblePiece draggedPiece;

		public Point CenterOffset { get; private set; }

		public DraggableAdorner(VisiblePiece adornedElement) : base(adornedElement) {
			visualChildren = new VisualCollection(this);
			draggedPiece = new VisiblePiece(adornedElement);
			visualChildren.Add(draggedPiece);

			IsHitTestVisible = false;
			Rect renderRect = new Rect(adornedElement.RenderSize);
			CenterOffset = new Point(-renderRect.Width / 2, -renderRect.Height / 2);
		}

		protected override Size ArrangeOverride(Size finalSize) {
			draggedPiece.Arrange(new Rect(0, 0, finalSize.Width, finalSize.Height));
			return draggedPiece.RenderSize;
		}

		protected override int VisualChildrenCount => visualChildren.Count;

		protected override Visual GetVisualChild(int index) {
			return visualChildren[index];
		}
	}
}
