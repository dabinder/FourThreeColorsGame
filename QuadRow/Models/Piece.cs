using QuadRow.Framework;

namespace QuadRow.Models {
	public class Piece {
		/// <summary>
		/// this piece's color
		/// </summary>
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
