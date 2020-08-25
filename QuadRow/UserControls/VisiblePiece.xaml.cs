using QuadRow.Framework;
using System.Runtime.InteropServices;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;

namespace QuadRow.UserControls
{
	/// <summary>
	/// Interaction logic for Piece.xaml
	/// code borrowed from:
	///		https://social.msdn.microsoft.com/Forums/en-US/81eca7d5-88d7-477a-8cdb-cfb9e8b75379/how-to-add-controls-to-adorner?forum=wpf,
	///		https://www.wundervisionenvisionthefuture.com/post/wpf-c-drag-and-drop-icon-adorner
	///		https://stackoverflow.com/a/27975085/2136840
	/// </summary>
	public partial class VisiblePiece : UserControl {
		public Brush Fill {
			get {
				return (Brush)GetValue(FillProperty);
			}
			set {
				SetValue(FillProperty, value);
			}
		}

		public static readonly DependencyProperty FillProperty =
			DependencyProperty.Register("Fill", typeof(Brush), typeof(VisiblePiece), new PropertyMetadata(null));

		private readonly bool isDraggable = true;
		DraggableAdorner adorner;

		[DllImport("user32.dll")]
		internal static extern void GetCursorPos(ref Win32Point pt);

		[StructLayout(LayoutKind.Sequential)]
		internal struct Win32Point {
			public int X;
			public int Y;
		};

		public VisiblePiece() {
			InitializeComponent();
		}

		public VisiblePiece(VisiblePiece piece) : this() {
			this.visiblePiece.Height = piece.visiblePiece.Height;
			this.visiblePiece.Width = piece.visiblePiece.Width;
			this.visiblePiece.Fill = piece.visiblePiece.Fill;
			isDraggable = false;
		}

		protected override void OnMouseMove(MouseEventArgs e) {
			base.OnMouseMove(e);
			if (isDraggable && e.LeftButton == MouseButtonState.Pressed) {
				DataObject data = new DataObject();
				data.SetData(DataFormats.StringFormat, visiblePiece.Fill.ToString());
				data.SetData("Double", visiblePiece.Height);
				data.SetData("Object", this);

				var adornerLayer = AdornerLayer.GetAdornerLayer(this);
				adorner = new DraggableAdorner(this);
				adornerLayer.Add(adorner);

				DragDrop.DoDragDrop(this, data, DragDropEffects.Copy);
				adornerLayer.Remove(adorner);
			}
		}

		protected override void OnGiveFeedback(GiveFeedbackEventArgs e) {
			Mouse.SetCursor(Cursors.None);
			e.Handled = true;

			Win32Point win32Point = new Win32Point();
			GetCursorPos(ref win32Point);
			Point relPos = PointFromScreen(new Point(win32Point.X, win32Point.Y));
			adorner.Arrange(new Rect(relPos, adorner.DesiredSize));
		}
	}
}
