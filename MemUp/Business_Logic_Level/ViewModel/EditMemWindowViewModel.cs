using Business_Logic.Repositories;
using Business_Logic_Level.ViewModel.Commands;
using Database_Access_Level;
using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Business_Logic_Level.ViewModel
{
    public class EditMemWindowViewModel : INotifyPropertyChanged
    {
        private UnitOfWork _unitOfWork = new UnitOfWork(new MemUpDBContext());
        private MainWindowViewModel mainVM;

        private string memQuestion;
        private string memAnswer;
        private string memAdditionalInfo;

        public EditMemWindowViewModel(MainWindowViewModel mv)
        {
            mainVM = mv;

            memQuestion = MainVM.CurrentMem.QuestionText;
            memAnswer = MainVM.CurrentMem.AnswerText;
            memAdditionalInfo = MainVM.CurrentMem.AdditionalInfo;

            EditMemCommand = new EditMemCommand(this);
        }

        public MainWindowViewModel MainVM
        {
            get { return mainVM; }
            set { mainVM = value; }
        }

        public EditMemCommand EditMemCommand { get; set; }

        public string MemQuestion
        {
            get { return memQuestion; }
            set
            {
                memQuestion = value;
                OnPropertyChanged("MemQuestion");
            }
        }

        public string MemAnswer
        {
            get { return memAnswer; }
            set
            {
                memAnswer = value;
                OnPropertyChanged("MemAnswer");
            }
        }

        public string MemAdditionalInfo
        {
            get { return memAdditionalInfo; }
            set
            {
                memAdditionalInfo = value;
                OnPropertyChanged("MemAdditionalInfo");
            }
        }

        public async void EditMem()
        {
            var updatedMem = await _unitOfWork.Mems.GetByIdAsync(MainVM.CurrentMem.MemId);

            updatedMem.QuestionText = MemQuestion;
            updatedMem.AnswerText = MemAnswer;
            updatedMem.AdditionalInfo = MemAdditionalInfo;

            await _unitOfWork.SaveAsync();
        }

        public event PropertyChangedEventHandler? PropertyChanged;

        private void OnPropertyChanged(string propertyName)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}
