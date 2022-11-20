using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace Business_Logic_Level.ViewModel.Commands
{
    public class AnswerMemCommand : ICommand
    {
        public MainWindowViewModel VM { get; set; }

        public AnswerMemCommand(MainWindowViewModel vm)
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
            if (VM.SelectedCollection.CollectionName == null)
            {
                return false;
            }
            return true;
        }

        public void Execute(object? parameter)
        {
            var nextTimeIdentifier = parameter as string;

            VM.AnswerButtonPressed(nextTimeIdentifier);
        }
    }
}
