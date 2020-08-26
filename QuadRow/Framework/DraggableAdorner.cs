using QuadRow.Views;
using System.Windows;
using System.Windows.Documents;
using System.Windows.Media;

namespace QuadRow.Framework {
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
