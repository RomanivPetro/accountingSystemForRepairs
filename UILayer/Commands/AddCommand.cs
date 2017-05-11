using System;
using System.Windows.Input;

namespace UILayer.Commands
{
    public class AddCommand : ICommand
    {
        private Func<bool> whenExecute;
        private Action whatExecute;

        public event EventHandler CanExecuteChanged;

        public AddCommand(Func<bool> whenExecute, Action whatExecute)
        {
            this.whenExecute = whenExecute;
            this.whatExecute = whatExecute;
        }

        public bool CanExecute(object parameter)
        {
            return whenExecute();
        }

        public void Execute(object parameter)
        {
            whatExecute();
        }
    }
}
