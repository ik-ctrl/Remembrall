using System;
using System.Windows.Input;

namespace Remembrall.Source.Infrastructure
{
    internal class RelayCommand:ICommand
    {
        private readonly Func<bool> _canExecute;
        private readonly Action _execute;

        public RelayCommand(Action execute, Func<bool> canExecute = null)
        {
            _execute = execute;
            _canExecute = canExecute;
        }

        public event EventHandler CanExecuteChanged
        {
            add => CommandManager.RequerySuggested += value;
            remove => CommandManager.RequerySuggested -= value;
        }

        public bool CanExecute(object parameter)
        {
            return _canExecute==null||_canExecute();
        }

        public void Execute(object parameter)
        {
            _execute();
        }
    }
}
