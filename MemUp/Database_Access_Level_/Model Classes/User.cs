using System;
using System.Collections.Generic;

namespace Database_Access_Level
{
    public partial class User
    {
        public User()
        {
            Collections = new HashSet<Collection>();
            Mems = new HashSet<Mem>();
        }

        public int UserId { get; set; }
        public string UserName { get; set; } = null!;
        public int MpBalance { get; set; }
        public int DayStreak { get; set; }
        public bool? MessagesStatus { get; set; }

        public virtual ICollection<Collection> Collections { get; set; }
        public virtual ICollection<Mem> Mems { get; set; }
    }
}
