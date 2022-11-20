using Business_Logic.Repositories;
using Business_Logic_Level.ViewModel.Commands;
using Database_Access_Level;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace Business_Logic_Level.ViewModel
{
    public class CreateCollectionWindowViewModel: INotifyPropertyChanged
    {
        private UnitOfWork _unitOfWork = new UnitOfWork(new MemUpDBContext());
        private MainWindowViewModel mainVM;

        private string collectionName;
        private int collectionDailyLimit;

        public CreateCollectionWindowViewModel(MainWindowViewModel mv)
        {
            mainVM = mv;

            collectionDailyLimit = 0;
            collectionName = string.Empty;

            CreateNewCollectionCommand = new CreateNewCollectionCommand(this);
        }

        public MainWindowViewModel MainVM 
        { 
            get { return mainVM; } 
            set { mainVM = value; } 
        }

        public CreateNewCollectionCommand CreateNewCollectionCommand { get; set; }

        public string CollectionName 
        { 
            get { return collectionName; }
            set
            {
                collectionName = value;
                OnPropertyChanged("CollectionName");
            } 
        }

        public int CollectionDailyLimit
        {
            get { return collectionDailyLimit; }
            set
            {
                collectionDailyLimit = value;
                OnPropertyChanged("CollectionDailyLimit");
            }
        }

        public async void CreateNewCollection()
        {
            var newCollection = new Collection
            {
                UserId = MainVM.CurrentUser.UserId,
                CollectionName = CollectionName,
                DailyLimit = CollectionDailyLimit
            };

            await _unitOfWork.Collections.AddAsync(newCollection);
            await _unitOfWork.SaveAsync();

            MainVM.FindAllUserCollections(null);
        }

        public event PropertyChangedEventHandler? PropertyChanged;

        private void OnPropertyChanged(string propertyName)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}
