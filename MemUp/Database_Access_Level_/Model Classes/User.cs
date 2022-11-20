using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;

namespace Database_Access_Level
{
    public partial class User
    {
        public User()
        {
            Collections = new ObservableCollection<Collection>();
            Mems = new ObservableCollection<Mem>();
        }

        public int UserId { get; set; }
        public string UserName { get; set; } = null!;
        public int MpBalance { get; set; }
        public int DayStreak { get; set; }
        public bool? MessagesStatus { get; set; }
        public DateTime LastLogin { get; set; }

        public virtual ObservableCollection<Collection> Collections { get; set; }
        public virtual ObservableCollection<Mem> Mems { get; set; }
    }
}
