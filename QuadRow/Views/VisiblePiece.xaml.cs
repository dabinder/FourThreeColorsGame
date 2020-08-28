using QuadRow.Framework;
using System.Windows;
using System.Windows.Controls;
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

		public VisiblePiece() {
			InitializeComponent();
		}

		public VisiblePiece(VisiblePiece piece) : this() {
			visiblePiece.Height = piece.visiblePiece.Height;
			visiblePiece.Width = piece.visiblePiece.Width;
			visiblePiece.Fill = piece.visiblePiece.Fill;
		}
	}
}
