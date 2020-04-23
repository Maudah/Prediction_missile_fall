using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using PL.ViewModels;

namespace PL.Commands
{
    public class OpenImageCommand : ICommand
    {
        public IFallsVM CurrentVM { get; }

        public OpenImageCommand()
        {

        }

        public OpenImageCommand(IFallsVM Vm)
        {
            CurrentVM = Vm;
        }

        public event EventHandler CanExecuteChanged
        {
            add { CommandManager.RequerySuggested += value; }

            remove { CommandManager.RequerySuggested -= value; }
        }

        public bool CanExecute(object parameter)
        {
            return true;
        }
       
        public void Execute(object parameter)
        {
            var path = parameter.ToString();
            Process.Start(path);


        }
    }
}
