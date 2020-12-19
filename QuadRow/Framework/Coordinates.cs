namespace QuadRow.Framework {
	/// <summary>
	/// X, Y coordinates for each space on the board
	/// </summary>
	public readonly struct Coordinates {
		/// <summary>
		/// x coordinate
		/// </summary>
		public int X { get; }

		/// <summary>
		/// y coordinate
		/// </summary>
		public int Y { get; }

		/// <summary>
		/// create new coordinate pair
		/// </summary>
		/// <param name="x">x coordinate</param>
		/// <param name="y">y coordinate</param>
		public Coordinates(int x, int y) =>
			(X, Y) = (x, y);

		/// <summary>
		/// override gethashcode to return code of tuple of x,y
		/// </summary>
		/// <returns>hashcode of x,y tuple</returns>
		public override int GetHashCode() =>
			(X, Y).GetHashCode();

		/// <summary>
		/// override object equals to check if comparison object is coordinates and has same x,y value
		/// </summary>
		/// <param name="obj">object to compare</param>
		/// <returns>comparison object is a set of coordinates with same x and y value</returns>
		public override bool Equals(object obj) =>
			obj is Coordinates c && this == c;

		/// <summary>
		/// operator override to check if x and y are of same value in compared coordinates
		/// </summary>
		/// <param name="a">first set of coordinates</param>
		/// <param name="b">second set of coordinates</param>
		/// <returns>first and second set have same x and y values</returns>
		public static bool operator ==(Coordinates a, Coordinates b) =>
			a.X == b.X && a.Y == b.Y;

		/// <summary>
		/// operator override to check if either x or y value are different in compared coordinates
		/// </summary>
		/// <param name="a">first set of coordinates</param>
		/// <param name="b">second set of coordinates</param>
		/// <returns>coordinates are not equal (either x or y is not the same in both)</returns>
		public static bool operator !=(Coordinates a, Coordinates b) =>
			!(a == b);

		/// <summary>
		/// return coordinate string as "X,Y"
		/// </summary>
		/// <returns>coordinates as "X,Y"</returns>
		public override string ToString() =>
			$"{X},{Y}";
	}
}
