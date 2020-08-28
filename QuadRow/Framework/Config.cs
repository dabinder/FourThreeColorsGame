using System.Windows.Media;

namespace QuadRow.Framework {
	static class Config {
		//general config settings for game
		public const int BOARD_SIZE = 9;
		public const int WIN_LENGTH = 4;

		//inventory sizes
		public const int PLAYER1_COLOR1 = 19;
		public const int PLAYER1_COLOR2 = 13;
		public const int PLAYER1_COLOR3 = 8;

		public const int PLAYER2_COLOR1 = 8;
		public const int PLAYER2_COLOR2 = 13;
		public const int PLAYER2_COLOR3 = 19;

		public static readonly Color color1 = Colors.Crimson,
			color2 = Colors.SpringGreen,
			color3 = Colors.SkyBlue;
	}
}
