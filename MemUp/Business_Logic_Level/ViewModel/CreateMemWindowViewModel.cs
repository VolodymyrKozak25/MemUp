using Business_Logic.Repositories;
using Business_Logic_Level.ViewModel.Commands;
using Database_Access_Level;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Business_Logic_Level.ViewModel
{
    public class CreateMemWindowViewModel : INotifyPropertyChanged
    {
        private UnitOfWork _unitOfWork = new UnitOfWork(new MemUpDBContext());
        private MainWindowViewModel mainVM;

        private string memQuestion;
        private string memAnswer;
        private string memAdditionalInfo;

        public CreateMemWindowViewModel(MainWindowViewModel mv)
        {
            mainVM = mv;

            memQuestion = string.Empty;
            memAnswer = string.Empty;
            memAdditionalInfo = string.Empty;

            CreateNewMemCommand = new CreateNewMemCommand(this);
        }

        public MainWindowViewModel MainVM
        {
            get { return mainVM; }
            set { mainVM = value; }
        }

        public CreateNewMemCommand CreateNewMemCommand { get; set; }

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

        public async void CreateNewMem()
        {
            var newMem = new Mem
            {
                UserId = MainVM.CurrentUser.UserId,
                CollectionId = MainVM.SelectedCollection.CollectionId,
                QuestionText = MemQuestion,
                AnswerText = MemAnswer,
                AdditionalInfo = MemAdditionalInfo
            };

            await _unitOfWork.Mems.AddAsync(newMem);
            await _unitOfWork.SaveAsync();
        }

        public event PropertyChangedEventHandler? PropertyChanged;

        private void OnPropertyChanged(string propertyName)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}
