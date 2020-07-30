using QuadRow.Framework;
using System.Windows.Controls;
using QuadRow.Views;

namespace QuadRow.ViewModels
{
	class GameViewModel : ObservableObject
	{
		#region intro screen
		private ContentControl _currentScreeen;
		public ContentControl CurrentScreen {
			get {
				return _currentScreeen;
			}
			set {
				_currentScreeen = value;
				NotifyPropertyChanged("CurrentScreen");
			}
		}

		private RelayCommand _closeIntroCommand;
		public RelayCommand CloseIntroCommand {
			get {
				return _closeIntroCommand ?? (
					_closeIntroCommand = new RelayCommand(param => {
						GameScreen gameScreen = new GameScreen {
							DataContext = this
						};
						CurrentScreen = gameScreen;
						IsNameBoxOpen = true;
					})
				);
			}
		}
		#endregion

		#region player info
		private bool _isNameBoxOpen;
		public bool IsNameBoxOpen {
			get {
				return _isNameBoxOpen;
			}
			private set {
				_isNameBoxOpen = value;
				NotifyPropertyChanged(nameof(IsNameBoxOpen));
			}
		}

		private RelayCommand _clickNameBoxClose;
		public RelayCommand ClickNameBoxClose {
			get {
				return _clickNameBoxClose ?? (
					_clickNameBoxClose = new RelayCommand(param => {
						IsNameBoxOpen = false;
					})
				);
			}
		}
		#endregion

		public GameViewModel() {
			
			//initialize game on intro screen
			ContentControl introScreen = new IntroScreen {
				DataContext = this
			};
			CurrentScreen = introScreen;
		}
	}
}
