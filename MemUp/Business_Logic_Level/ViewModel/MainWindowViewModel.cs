using Business_Logic.Repositories;
using Business_Logic_Level.ViewModel.Commands;
using Database_Access_Level;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;

namespace Business_Logic_Level.ViewModel
{
    public class MainWindowViewModel : INotifyPropertyChanged
    {
        private static UnitOfWork _unitOfWork = new UnitOfWork(new MemUpDBContext());

        private string collectionQuery;

        private string username;
        private User currentUser;
        private bool isUserSigned;
        private Visibility guestVisibility;
        private Visibility signedInVisibility;

        private Collection selectedCollection;
        private Mem currentMem;


        public MainWindowViewModel()
        {
            username = "Enter username";
            currentUser = new User();
            selectedCollection = new Collection();
            currentMem = new Mem();
            isUserSigned = false;
            guestVisibility = Visibility.Visible;
            signedInVisibility = Visibility.Collapsed;
            collectionQuery = "Search collection";

            UserCollections = new ObservableCollection<Collection>();
            SelectedCollectionMems = new ObservableCollection<Mem>();

            SearchColletionCommand = new SearchCollectionCommand(this);
            LoginUserCommand = new LoginUserCommand(this);
            DeleteCollectionCommand = new DeleteCollectionCommand(this);
            LoadMemListCommand = new LoadMemListCommand(this);
            DeleteMemCommand = new DeleteMemCommand(this);
            AnswerMemCommand = new AnswerMemCommand(this);
        }


        public ObservableCollection<Collection> UserCollections { get; set; }
        public ObservableCollection<Mem> SelectedCollectionMems { get; set; }

        public SearchCollectionCommand SearchColletionCommand { get; set; }
        public LoginUserCommand LoginUserCommand { get; set; }
        public DeleteCollectionCommand DeleteCollectionCommand { get; set; }
        public LoadMemListCommand LoadMemListCommand { get; set; }
        public DeleteMemCommand DeleteMemCommand { get; set; }
        public AnswerMemCommand AnswerMemCommand { get; set; }

        public bool IsUserSigned
        {
            get { return isUserSigned; }
            set
            {
                isUserSigned = value;
                GuestVisibility = Visibility.Collapsed;
                OnPropertyChanged("IsUserSigned");
            }
        }

        public Visibility GuestVisibility
        {
            get { return guestVisibility; }
            set
            {
                guestVisibility = value;
                OnPropertyChanged("GuestVisibility");
            }
        }

        public Visibility SignedInVisibility
        {
            get { return signedInVisibility; }
            set
            {
                signedInVisibility = value;
                OnPropertyChanged("SignedInVisibility");
            }
        }

        public string Username
        {
            get { return username; }
            set
            {
                username = value;
                OnPropertyChanged("Username");
            }
        }

        public User CurrentUser
        {
            get { return currentUser; }
            set
            {
                currentUser = value;
                OnPropertyChanged("CurrentUser");
            }
        }

        public Collection SelectedCollection
        {
            get { return selectedCollection; }
            set
            {
                selectedCollection = value;
                OnPropertyChanged("SelectedCollection");
            }
        }

        public Mem CurrentMem
        {
            get { return currentMem; }
            set
            {
                currentMem = value;
                OnPropertyChanged("CurrentMem");
            }
        }

        public string CollectionQuery
        {
            get { return collectionQuery; }
            set
            {
                collectionQuery = value;

                if (!string.IsNullOrWhiteSpace(collectionQuery) & collectionQuery != "Search collection")
                {

                    UserCollections.Select(u => u.CollectionName.ToLower().Contains(collectionQuery.ToLower()));
                    OnPropertyChanged("CollectionQuery");
                }
            }
        }



        public void SearchColletionsInList()
        {
            if (!string.IsNullOrWhiteSpace(collectionQuery) & collectionQuery != "Search collection")
            {
                FindAllUserCollections(CollectionQuery);
            }
            else
            {
                FindAllUserCollections(null);
            }
        }

        public async void LoginUser()
        {
            if (Username != "Enter username")
            {
                var user = await _unitOfWork.Users.CreateOrLoginUser(Username);

                if (user == null)
                {
                    //show message that username is incorrect
                    MessageBox.Show("Incorrect username." +
                                    "\nUsername can only contain latin alphabet and numbers." +
                                    "\n(minimum length is 4 and maximum is 16 characters)",
                                    "Incorrect Username",
                                    MessageBoxButton.OK,
                                    MessageBoxImage.Warning);
                    return;
                }

                Username = user.UserName;
                CurrentUser = user;

                IsUserSigned = true;
                GuestVisibility = Visibility.Collapsed;
                SignedInVisibility = Visibility.Visible;

                FindAllUserCollections(null);

                UpdateUserLastLogin();
            }
        }

        public async void UpdateUserLastLogin()
        {
            using (_unitOfWork = new UnitOfWork(new MemUpDBContext()))
            {
                var user = await _unitOfWork.Users.GetByIdAsync(CurrentUser.UserId);

                if (user != null)
                {
                    user.LastLogin = DateTime.UtcNow;
                    await _unitOfWork.SaveAsync();
                }

            }
        }

        public async void FindAllUserCollections(string? query)
        {
            UserCollections.Clear();

            using (_unitOfWork = new UnitOfWork(new MemUpDBContext()))
            {
                var userCollections = await _unitOfWork.Collections.GetCollectionForUser(currentUser.UserId, query);
                foreach (var collection in userCollections)
                {
                    UserCollections.Add(collection);
                }
            }
        }

        public async void DeleteCollection()
        {
            using (_unitOfWork = new UnitOfWork(new MemUpDBContext()))
            {
                var collection = await _unitOfWork.Collections.GetByIdAsync(selectedCollection.CollectionId);

                if (collection != null)
                {
                    _unitOfWork.Collections.Remove(collection);
                    await _unitOfWork.SaveAsync();
                }
            }

            FindAllUserCollections(null);
        }

        public void LoadCollectionMems()
        {
            SelectedCollectionMems.Clear();

            using (_unitOfWork = new UnitOfWork(new MemUpDBContext()))
            {
                var reviewMems = _unitOfWork.Mems
                .Find(m => m.CollectionId == SelectedCollection.CollectionId
                           & m.Status == "review")
                .OrderBy(m => m.ReviewTime);
                foreach (var mem in reviewMems)
                {
                    SelectedCollectionMems.Add(mem);
                }

                var studyMems = _unitOfWork.Mems
                    .Find(m => m.CollectionId == SelectedCollection.CollectionId
                               & m.Status == "study")
                    .OrderBy(m => m.ReviewTime);
                foreach (var mem in studyMems)
                {
                    SelectedCollectionMems.Add(mem);
                }
            }
        }

        public void SelectNextMem()
        {
            var mem = SelectedCollectionMems.FirstOrDefault();
            if (mem != null)
            {
                CurrentMem = mem;
                return;
            }
            CurrentMem = new Mem();
        }

        public async void DeleteCurrentMem()
        {
            using (_unitOfWork = new UnitOfWork(new MemUpDBContext()))
            {
                if (CurrentMem.QuestionText == null)
                {
                    MessageBox.Show("Cannot delete empty mem.",
                        "Deleting empty mem",
                        MessageBoxButton.OK,
                        MessageBoxImage.Information);
                    return;
                }

                var previousMem = currentMem;
                var nextMem = SelectedCollectionMems.Skip(1).FirstOrDefault();
                if (nextMem != null)
                {
                    CurrentMem = nextMem;
                }
                CurrentMem = new Mem();

                _unitOfWork.Mems.Remove(previousMem);

                await _unitOfWork.SaveAsync();
            }
        }

        public async void AnswerButtonPressed(string timeIdentifier)
        {
            using (_unitOfWork = new UnitOfWork(new MemUpDBContext()))
            {
                var thisMem = await _unitOfWork.Mems.GetByIdAsync(currentMem.MemId);

                if (thisMem != null)
                {
                    if (thisMem.Status == "study")
                    {
                        thisMem.Status = "review";
                    }

                    switch (timeIdentifier)
                    {
                        case "Forgot":
                            thisMem.ReviewTime = DateTime.UtcNow.AddMinutes(5);
                            SelectedCollectionMems.Remove(thisMem);
                            SelectNextMem();
                            break;

                        case "Hard":
                            thisMem.ReviewTime = DateTime.UtcNow.AddMinutes(15);
                            SelectedCollectionMems.Remove(thisMem);
                            SelectNextMem();
                            break;

                        case "Good":
                            thisMem.ReviewTime = DateTime.UtcNow.AddHours(3);
                            SelectedCollectionMems.Remove(thisMem);
                            SelectNextMem();
                            break;

                        case "Easy":
                            thisMem.ReviewTime = DateTime.UtcNow.AddHours(24);
                            SelectedCollectionMems.Remove(thisMem);
                            SelectNextMem();
                            break;

                        default:
                            break;
                    }

                    await _unitOfWork.SaveAsync();
                }
            }
        }

        public event PropertyChangedEventHandler? PropertyChanged;

        private void OnPropertyChanged(string propertyName)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}
