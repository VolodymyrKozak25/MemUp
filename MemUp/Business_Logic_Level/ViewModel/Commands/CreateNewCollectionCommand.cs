using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Input;

namespace Business_Logic_Level.ViewModel.Commands
{
    public class CreateNewCollectionCommand : ICommand
    {
        public CreateCollectionWindowViewModel VM { get; set; }

        public CreateNewCollectionCommand(CreateCollectionWindowViewModel vm)
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
            var senderWindow = parameter as Window;

            VM.CreateNewCollection();
            senderWindow.Close();
        }
    }
}
