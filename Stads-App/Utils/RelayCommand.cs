using System;
using System.Windows.Input;

namespace Stads_App.Utils
{
    public class RelayCommand : ICommand
    {
        public event EventHandler CanExecuteChanged;

        public void RaiseCanExecuteChanged()
        {
            CanExecuteChanged?.Invoke(this, EventArgs.Empty);
        }

        private readonly Action<object> _execute;
        private readonly Func<object, bool> _canExecute = (_) => true;

        public RelayCommand(Action<object> action)
        {
            _execute = action;
        }

        public RelayCommand(Action<object> action, Func<object, bool> test)
        {
            _execute = action;
            _canExecute = test;
        }


        public bool CanExecute(object parameter)
        {
            return _canExecute(parameter);
        }

        public void Execute(object parameter)
        {
            _execute(parameter);
        }
    }
}