using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
using Windows.Foundation;
using Windows.Foundation.Collections;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Controls.Primitives;
using Windows.UI.Xaml.Data;
using Windows.UI.Xaml.Input;
using Windows.UI.Xaml.Media;
using Windows.UI.Xaml.Navigation;

// The Blank Page item template is documented at http://go.microsoft.com/fwlink/?LinkId=402352&clcid=0x409

namespace FourThreeColorsGame {
	/// <summary>
	/// An empty page that can be used on its own or navigated to within a Frame.
	/// </summary>
	public sealed partial class StartPage : Page {
		/// <summary>
		/// initialize start page
		/// </summary>
		public StartPage() {
			this.InitializeComponent();
		}

		/// <summary>
		/// click new game button on start page to begin a game
		/// </summary>
		/// <param name="sender">source of new game request</param>
		/// <param name="e">request detail</param>
		private void OnNewGameClicked(object sender, RoutedEventArgs e) {
			this.Frame.Navigate(typeof(Views.GamePage));
		}
	}
}
