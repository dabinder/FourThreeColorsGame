using System;
using System.Windows.Input;

namespace QuadRow.Framework {
	public class RelayCommand : ICommand {
		private readonly Action<object> _execute;
		private readonly Predicate<object> _canExecute;

		/// <summary>
		/// create RelayCommand with an always true predicate
		/// </summary>
		/// <param name="execute"></param>
		public RelayCommand(Action<object> execute) : this(execute, null) { }

		/// <summary>
		/// set action to perform and requirement(s) for performing the action
		/// </summary>
		/// <param name="execute">action to perform</param>
		/// <param name="canExecute">required status to perform the action specified by execute</param>
		public RelayCommand(Action<object> execute, Predicate<object> canExecute) {
			_execute = execute ?? throw new ArgumentNullException("execute");
			_canExecute = canExecute;
		}

		/// <summary>
		/// determine whether this command's action is permitted to be performed
		/// </summary>
		/// <param name="parameter">parameter to pass into predicate</param>
		/// <returns>command can be performed</returns>
		public bool CanExecute(object parameter) =>
			_canExecute == null || _canExecute(parameter);

		/// <summary>
		/// on changing the CanExecute condition, (un)subscribe specified event to force update of can execute status
		/// </summary>
		public event EventHandler CanExecuteChanged {
			add => CommandManager.RequerySuggested += value;
			remove => CommandManager.RequerySuggested -= value;
		}

		/// <summary>
		/// execute this RelayCommand's action
		/// </summary>
		/// <param name="parameter">parameter to pass into action</param>
		public void Execute(object parameter) => _execute(parameter);
	}
}