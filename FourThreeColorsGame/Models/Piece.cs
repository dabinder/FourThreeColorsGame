using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FourThreeColorsGame.Framework;

namespace FourThreeColorsGame.Models {
	class Piece {
		public ColorType Color { get; }
		
		public Piece(ColorType color) {
			Color = color;
		}
	}
}
