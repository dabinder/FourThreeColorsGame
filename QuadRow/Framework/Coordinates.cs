namespace QuadRow.Framework {
	public readonly struct Coordinates {
		public int X { get; }
		public int Y { get; }

		public Coordinates(int x, int y) =>
			(X, Y) = (x, y);

		public override int GetHashCode() =>
			(X, Y).GetHashCode();

		public override bool Equals(object obj) =>
			obj is Coordinates c && this == c;

		public static bool operator ==(Coordinates a, Coordinates b) =>
			a.X == b.X && a.Y == b.Y;

		public static bool operator !=(Coordinates a, Coordinates b) =>
			!(a == b);

		public override string ToString() =>
			$"{X},{Y}";
	}
}
