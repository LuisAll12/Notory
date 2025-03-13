using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace Notory.Helpers
{
    internal class RelayCommand : ICommand
    {
        private readonly Action<object> _executeWithParam;
        private readonly Action _executeNoParam;
        private readonly Predicate<object> _canExecute;

        public event EventHandler CanExecuteChanged
        {
            add { CommandManager.RequerySuggested += value; }
            remove { CommandManager.RequerySuggested -= value; }
        }

        public RelayCommand(Action<object> execute, Predicate<object> canExecute = null)
        {
            _executeWithParam = execute ?? throw new ArgumentNullException(nameof(execute));
            _canExecute = canExecute;
        }

        public RelayCommand(Action execute, Func<bool> canExecute = null)
        {
            _executeNoParam = execute ?? throw new ArgumentNullException(nameof(execute));

            // Avoid using nullable types or null assignment for canExecute
            if (canExecute != null)
            {
                _canExecute = _ => canExecute();
            }
            else
            {
                _canExecute = null;
            }
        }

        public bool CanExecute(object parameter)
        {
            return _canExecute == null || _canExecute(parameter);
        }

        public void Execute(object parameter)
        {
            if (_executeWithParam != null)
                _executeWithParam(parameter);
            else
                _executeNoParam?.Invoke();
        }
    }
}
