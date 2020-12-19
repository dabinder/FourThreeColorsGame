using QuadRow.Framework;
using QuadRow.ViewModels;
using System.ComponentModel;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;

namespace QuadRow.Views {
	/// <summary>
	/// Interaction logic for Piece
	/// </summary>
	public partial class VisiblePiece : UserControl, INotifyPropertyChanged {

		public ColorType ColorType {
			get {
				return (ColorType)GetValue(ColorTypeProperty);
			}
			set {
				SetValue(ColorTypeProperty, value);
				PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(nameof(ColorType)));
			}
		}

		public static readonly DependencyProperty ColorTypeProperty = DependencyProperty.Register(
			"ColorType",
			typeof(ColorType),
			typeof(VisiblePiece)
		);

		public event PropertyChangedEventHandler PropertyChanged;

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
