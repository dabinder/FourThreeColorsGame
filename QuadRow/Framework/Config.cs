using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Media;

namespace QuadRow.Framework
{
	static class Config {
		//general config settings for game
		public const int BOARD_SIZE = 9;
		public const int WIN_LENGTH = 4;

		//inventory sizes
		internal const int PLAYER1_COLOR1 = 19;
		internal const int PLAYER1_COLOR2 = 13;
		internal const int PLAYER1_COLOR3 = 8;

		internal const int PLAYER2_COLOR1 = 8;
		internal const int PLAYER2_COLOR2 = 13;
		internal const int PLAYER2_COLOR3 = 19;

		internal static readonly Color color1, color2, color3;
		static Config() {
			color1 = Colors.Crimson;
			color2 = Colors.SpringGreen;
			color3 = Colors.SkyBlue;
		}
	}
}
