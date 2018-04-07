using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FourThreeColorsGame.Framework {
	class IntAlphaConverter {
		private const string ALPHABET = "ABCDEFGHIJKLMNOPQRSTUVWXYZ";

		/// <summary>
		/// get letter at given numeric position in alphabet
		/// </summary>
		/// <param name="index">numeric position</param>
		/// <returns>letter</returns>
		public static char IntToAlpha(int index) {
			if (index < 0) {
				throw new IndexOutOfRangeException("index must be a positive number");
			}
			if (index >= ALPHABET.Length) {
				throw new IndexOutOfRangeException("index cannot be greater than 25");
			}

			return ALPHABET[index];
		}

		/// <summary>
		/// get numeric position of given letter in alphabet
		/// </summary>
		/// <param name="value"letter></param>
		/// <returns>numeric position</returns>
		public static int AlphaToInt(char value) {
			int index = ALPHABET.IndexOf(value);

			if (index == -1) {
				throw new IndexOutOfRangeException($"invalid character {value} given; expecting A-Z");
			}

			return index;
		}
	}
}
