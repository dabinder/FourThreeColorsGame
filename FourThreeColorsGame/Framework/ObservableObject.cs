using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FourThreeColorsGame.Framework {
	public abstract class ObservableObject : INotifyPropertyChanged {
		/// <summary>
		/// handler for global property changes
		/// </summary>
		public event PropertyChangedEventHandler PropertyChanged;

		/// <summary>
		/// send notification of property changed to listeners
		/// </summary>
		/// <param name="propertyName">name of property changed</param>
		protected void NotifyPropertyChanged(string propertyName) {
			PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
		}
	}
}
