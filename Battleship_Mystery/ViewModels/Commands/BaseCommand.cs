using System;
using System.Collections.Generic;
using System.Text;
using System.Windows.Input;

namespace Battleship_Mystery.ViewModels.Commands
{
    public class BaseCommand : ICommand
    {
        private Action<object> _excute;
        public event EventHandler CanExecuteChanged;

        public BaseCommand(Action<object> execute)
        {
            _excute = execute;
        }

        public bool CanExecute(object parameter)
        {
            return true;
        }

        public void Execute(object parameter)
        {
            _excute.Invoke(parameter);
        }
    }
}
