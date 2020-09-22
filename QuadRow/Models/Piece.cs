using QuadRow.Framework;

namespace QuadRow.Models {
	public class Piece {
		public ColorType Color { get; }

		/// <summary>
		/// set this piece's color
		/// </summary>
		/// <param name="color">piece color</param>
		public Piece(ColorType color) {
			Color = color;
		}
	}
}
