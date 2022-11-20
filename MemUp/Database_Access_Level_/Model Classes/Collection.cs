using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;

namespace Database_Access_Level
{
    public partial class Collection
    {
        public Collection()
        {
            Mems = new ObservableCollection<Mem>();
        }

        public int CollectionId { get; set; }
        public int UserId { get; set; }
        public string CollectionName { get; set; } = null!;
        public int DailyLimit { get; set; }
        public int StudyQueue { get; set; }
        public int ReviewQueue { get; set; }

        public virtual User User { get; set; } = null!;
        public virtual ObservableCollection<Mem> Mems { get; set; }
    }
}
