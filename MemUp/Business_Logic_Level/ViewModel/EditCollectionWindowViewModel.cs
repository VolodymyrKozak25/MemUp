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
    public class EditCollectionWindowViewModel : INotifyPropertyChanged
    {
        private UnitOfWork _unitOfWork = new UnitOfWork(new MemUpDBContext());
        private MainWindowViewModel mainVM;

        private string collectionName;
        private int collectionDailyLimit;

        public EditCollectionWindowViewModel(MainWindowViewModel mv)
        {
            mainVM = mv;

            collectionDailyLimit = MainVM.SelectedCollection.DailyLimit;
            collectionName = MainVM.SelectedCollection.CollectionName;

            EditCollectionCommand = new EditCollectionCommand(this);
        }

        public MainWindowViewModel MainVM
        {
            get { return mainVM; }
            set { mainVM = value; }
        }

        public EditCollectionCommand EditCollectionCommand { get; set; }

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

        public async void EditCollection()
        {
            var updatedCollection = await _unitOfWork.Collections.GetByIdAsync(MainVM.SelectedCollection.CollectionId);

            updatedCollection.CollectionName = CollectionName;
            updatedCollection.DailyLimit = CollectionDailyLimit;

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
