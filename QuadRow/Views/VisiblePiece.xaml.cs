using QuadRow.Framework;
using QuadRow.ViewModels;
using System.Runtime.InteropServices;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;

namespace QuadRow.Views {
	/// <summary>
	/// Interaction logic for Piece
	/// code borrowed from:
	///		https://social.msdn.microsoft.com/Forums/en-US/81eca7d5-88d7-477a-8cdb-cfb9e8b75379/how-to-add-controls-to-adorner?forum=wpf,
	///		https://www.wundervisionenvisionthefuture.com/post/wpf-c-drag-and-drop-icon-adorner
	///		https://stackoverflow.com/a/27975085/2136840
	/// </summary>
	public partial class VisiblePiece : UserControl {
		public Brush Fill {
			get {
				return new SolidColorBrush(DisplayedColors.TranslateDisplayedColor(ColorType));
			}
		}

		public ColorType ColorType {
			get {
				return (ColorType)GetValue(ColorTypeProperty);
			}
			set {
				SetValue(ColorTypeProperty, value);
			}
		}

		public static readonly DependencyProperty ColorTypeProperty = DependencyProperty.Register(
			"ColorType",
			typeof(ColorType),
			typeof(VisiblePiece)
		);

		private DraggableAdorner adorner;
		private AdornerLayer adornerLayer;
		private bool isDragging;

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
			visiblePiece.Height = piece.visiblePiece.Height;
			visiblePiece.Width = piece.visiblePiece.Width;
			visiblePiece.Fill = piece.visiblePiece.Fill;
		}

		protected override void OnMouseDown(MouseButtonEventArgs e) {
			base.OnMouseDown(e);
			if (DataContext is PlayerViewModel) {
				PlayerViewModel model = DataContext as PlayerViewModel;
				if (model.Active && e.LeftButton == MouseButtonState.Pressed) {
					adornerLayer = AdornerLayer.GetAdornerLayer(this);
					adorner = new DraggableAdorner(this);
					adornerLayer.Add(adorner);
					isDragging = true;
				}
			}
		}

		protected override void OnMouseMove(MouseEventArgs e) {
			base.OnMouseMove(e);
			if (isDragging && DataContext is PlayerViewModel) {
				DataObject data = new DataObject();
				data.SetData(typeof(ColorType), ColorType);
				
				DragDrop.DoDragDrop(this, data, DragDropEffects.None);
				adornerLayer.Remove(adorner);
				isDragging = false;
			}
		}

		protected override void OnGiveFeedback(GiveFeedbackEventArgs e) {
			if (isDragging && DataContext is PlayerViewModel) {
				Mouse.SetCursor(Cursors.None);
				e.Handled = true;

				Win32Point win32Point = new Win32Point();
				GetCursorPos(ref win32Point);
				Point relPos = PointFromScreen(new Point(win32Point.X, win32Point.Y));
				adorner.Arrange(new Rect(relPos, adorner.DesiredSize));
			}
		}
	}
}
