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
		private readonly VisualCollection visualChildren;
		private readonly VisiblePiece draggedPiece;

		/// <summary>
		/// create copy of visible piece from adorned element
		/// original piece remains in its starting location while this one is being dragged
		/// </summary>
		/// <param name="adornedElement">visible piece to copy and drag</param>
		public DraggableAdorner(VisiblePiece adornedElement) : base(adornedElement) {
			visualChildren = new VisualCollection(this);
			draggedPiece = new VisiblePiece(adornedElement);
			visualChildren.Add(draggedPiece);

			IsHitTestVisible = false;
		}

		/// <summary>
		/// determine this adorner's size
		/// </summary>
		/// <param name="finalSize">The final area within the parent that this element should use to arrange itself and its children.</param>
		/// <returns>the size used</returns>
		protected override Size ArrangeOverride(Size finalSize) {
			draggedPiece.Arrange(new Rect(0, 0, finalSize.Width, finalSize.Height));
			return draggedPiece.RenderSize;
		}

		/// <summary>
		/// gets the number of visual child elements contained within this adorner
		/// </summary>
		/// <returns>the number of visual child elements</returns>
		protected override int VisualChildrenCount => visualChildren.Count;

		/// <summary>
		/// gets the specified visual child of this adorner
		/// </summary>
		/// <param name="index">index of the specified child element</param>
		/// <returns>the specified child element</returns>
		protected override Visual GetVisualChild(int index) {
			return visualChildren[index];
		}
	}
}
