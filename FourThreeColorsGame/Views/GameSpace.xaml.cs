using FourThreeColorsGame.Models;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
using System.Windows.Input;
using Windows.Foundation;
using Windows.Foundation.Collections;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Controls.Primitives;
using Windows.UI.Xaml.Data;
using Windows.UI.Xaml.Input;
using Windows.UI.Xaml.Media;
using Windows.UI.Xaml.Navigation;

// The User Control item template is documented at http://go.microsoft.com/fwlink/?LinkId=234236
namespace FourThreeColorsGame.Views {
	public sealed partial class GameSpace : UserControl {
		public static readonly DependencyProperty CommandProperty = DependencyProperty.Register(
			"Command",
			typeof(ICommand),
			typeof(GameSpace),
			new PropertyMetadata(null)
		);

		public ICommand Command {
			get {
				return (ICommand)GetValue(CommandProperty);
			}
			set {
				SetValue(CommandProperty, value);
			}
		}

		public GameSpace() {
			this.InitializeComponent();
		}
	}
}
