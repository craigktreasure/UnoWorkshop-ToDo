namespace ToDo.ViewModels
{
    using System;
    using System.Windows.Input;

    internal class SimpleCommand : ICommand
    {
        private readonly Action _action;

        public event EventHandler CanExecuteChanged;

        public SimpleCommand(Action action)
        {
            this._action = action;
        }

        public bool CanExecute(object parameter)
        {
            return true;
        }

        public void Execute(object parameter)
        {
            this._action();
        }
    }

    internal class SimpleCommand<T> : ICommand where T : class
    {
        private readonly Action<T> _action;

        public event EventHandler CanExecuteChanged;

        public SimpleCommand(Action<T> action)
        {
            this._action = action;
        }

        public bool CanExecute(object parameter)
        {
            return parameter is T;
        }

        public void Execute(object parameter)
        {
            this._action(parameter as T);
        }
    }
}