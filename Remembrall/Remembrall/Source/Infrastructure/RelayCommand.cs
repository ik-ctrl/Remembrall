using System;
using System.Net.Http.Headers;
using System.Windows.Input;
using Remembrall.Annotations;

namespace Remembrall.Source.Infrastructure
{
    public class RelayCommand : ICommand
    {
        private readonly Action<object> _execute;
        private readonly Func<object, bool> _canExecute;

        public RelayCommand([CanBeNull] Action<object> execute, Func<object, bool> canExecute=null)
        {
            _execute = execute;
            _canExecute = canExecute;
        }


        public bool CanExecute(object parameter)
        {
            return _canExecute == null || _canExecute(parameter);
        }

        public void Execute(object parameter)
        {
            _execute(parameter);
        }

        public event EventHandler CanExecuteChanged
        {
            add => CommandManager.RequerySuggested += value;
            remove => CommandManager.RequerySuggested -= value;
        }
    }
}
