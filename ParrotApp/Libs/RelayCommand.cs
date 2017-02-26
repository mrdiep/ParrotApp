using System;
using System.Windows.Input;

namespace ParrotApp.Libs
{
    public class RelayCommand : ICommand
    {
        public event EventHandler CanExecuteChanged;

        private readonly Action _execute;

        private readonly Func<bool> _canExecute;

        public void RaiseCanExecuteChanged()
        {
            CanExecuteChanged?.Invoke(this, EventArgs.Empty);
        }

        public RelayCommand(Action execute)
        : this(execute, null)
        {
        }

        public RelayCommand(Action execute, Func<bool> canExecute)
        {
            if (execute == null)
            {
                throw new ArgumentNullException("execute");
            }

            _execute = new Action(execute);

            if (canExecute != null)
            {
                _canExecute = new Func<bool>(canExecute);
            }
        }

        public bool CanExecute(object parameter)
        {
            return _canExecute == null || _canExecute.Invoke();
        }

        public void Execute(object parameter)
        {
            if (CanExecute(parameter) && _execute != null)
            {
                _execute.Invoke();
            }
        }
    }
}