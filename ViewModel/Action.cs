using System;
using System.Windows.Input;

namespace ViewModel
{
    internal class Action : ICommand
    {
        public event EventHandler CanExecuteChanged;
        private readonly System.Action execute;         //Action to be executed
        private readonly Func<bool> canExecute;  //Checking if we can execute the action

        public Action(System.Action execute, Func<bool> canExecute = null)
        {
            this.execute = execute;
            this.canExecute = canExecute;
        }
        public bool CanExecute(object parameter) =>
            canExecute == null || canExecute();
        public void Execute(object parameter) => execute();

        internal void OnCanExecuteChanged()
        {
            CanExecuteChanged?.Invoke(this, EventArgs.Empty);
        }

    }

}