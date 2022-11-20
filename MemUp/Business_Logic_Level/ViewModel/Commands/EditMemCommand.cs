using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using System.Windows;

namespace Business_Logic_Level.ViewModel.Commands
{
    public class EditMemCommand : ICommand
    {
        public EditMemWindowViewModel VM { get; set; }

        public EditMemCommand(EditMemWindowViewModel vm)
        {
            VM = vm;
        }

        public event EventHandler? CanExecuteChanged;

        public bool CanExecute(object? parameter)
        {
            return true;
        }

        public void Execute(object? parameter)
        {
            var senderWindow = parameter as Window;

            VM.EditMem();
            senderWindow.Close();
        }
    }
}
