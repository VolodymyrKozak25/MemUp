using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace Business_Logic_Level.ViewModel.Commands
{
    public class LoginUserCommand : ICommand
    {
        public MainWindowViewModel VM { get; set; }

        public LoginUserCommand(MainWindowViewModel vm)
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
            VM.LoginUser();
        }
    }
}
