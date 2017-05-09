using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace UILayer.Commands
{
    public class AddOrderCommand : ICommand
    {
        private Func<bool> whenExecute;
        private Action whatExecute;

        public event EventHandler CanExecuteChanged;

        public AddOrderCommand(Func<bool> whenExecute, Action whatExecute)
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
