using BE;
using PL.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace PL.Commands
{
    public class AddFallCommand : ICommand
    {
        public event EventHandler CanExecuteChanged
        {
            add { CommandManager.RequerySuggested += value; }

            remove { CommandManager.RequerySuggested -= value; }
        }
        private IFallsVM CurrentVM;

        public AddFallCommand(IFallsVM currentVM)
        {
            CurrentVM = currentVM;
        }

        public bool CanExecute(object parameter)
        {
            bool result = false;

            if (!(parameter is null))
            {
                var p = parameter.ToString();
                if (p.Length > 2) result = true;
            }
            return result;
        }

        public void Execute(object parameter)
        {
            if (!(parameter is null))
            {
                Fall p = (Fall)parameter;
                CurrentVM.AddFall(p);
            }
        }
    }
}

