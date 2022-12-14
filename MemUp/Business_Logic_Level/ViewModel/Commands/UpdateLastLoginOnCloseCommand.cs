using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Input;

namespace Business_Logic_Level.ViewModel.Commands
{
    public class UpdateLastLoginOnCloseCommand : ICommand
    {
        public MainWindowViewModel VM { get; set; }

        public UpdateLastLoginOnCloseCommand(MainWindowViewModel vm)
        {
            VM = vm;
        }

        public event EventHandler? CanExecuteChanged
        {
            add { CommandManager.RequerySuggested += value; }
            remove { CommandManager.RequerySuggested -= value; }
        }

        public bool CanExecute(object? parameter)
        {
            return true;
        }

        public void Execute(object? parameter)
        {
            var sender = parameter as Window;

            VM.UpdateUserLastLogin();

            if (sender != null)
            {
                sender.Close();
            }
        }
    }
}
