using FourThreeColorsGame.ViewModels;
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

// The Blank Page item template is documented at http://go.microsoft.com/fwlink/?LinkId=234238

namespace FourThreeColorsGame.Views {
	/// <summary>
	/// An empty page that can be used on its own or navigated to within a Frame.
	/// </summary>
	sealed partial class GamePage : Page {
		public GamePage() {
			this.InitializeComponent();
		}

		private void OnPlayerNameCloseButtonClicked(object sender, RoutedEventArgs e) {
			GameDisplay.Children.Remove(PlayerNames);
		}

		private void OnPlayerNameChanged(object sender, TextChangedEventArgs e) {
			bool enabled = Player1Name.Text != "" && Player2Name.Text != "";

			PlayerNameCloseButton.IsEnabled = enabled;
			PlayerNamesError.Visibility = enabled ? Visibility.Collapsed : Visibility.Visible;
		}
	}
}
