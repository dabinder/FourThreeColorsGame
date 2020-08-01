using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace QuadRow.UserControls
{
	/// <summary>
	/// Interaction logic for Piece.xaml
	/// </summary>
	public partial class VisiblePiece : UserControl
	{
		public VisiblePiece() {
			InitializeComponent();
		}

		public VisiblePiece(VisiblePiece piece) {
			InitializeComponent();
			this.visiblePiece.Height = piece.visiblePiece.Height;
			this.visiblePiece.Width = piece.visiblePiece.Width;
			this.visiblePiece.Fill = piece.visiblePiece.Fill;
		}

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
	}
}
