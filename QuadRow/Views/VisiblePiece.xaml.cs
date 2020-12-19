﻿using QuadRow.Framework;
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

		/// <summary>
		/// create new visible piece
		/// </summary>
		public VisiblePiece() {
			InitializeComponent();
		}

		/// <summary>
		/// create new visible piece as a copy of an existing piece
		/// </summary>
		/// <param name="piece">piece to copy</param>
		public VisiblePiece(VisiblePiece piece) : this() {
			visiblePiece.Height = piece.visiblePiece.Height;
			visiblePiece.Width = piece.visiblePiece.Width;
			visiblePiece.Fill = piece.visiblePiece.Fill;
		}
	}
}
