using System;
using System.Collections.Generic;

namespace Database_Access_Level
{
    public partial class Collection
    {
        public Collection()
        {
            Mems = new HashSet<Mem>();
        }

        public int CollectionId { get; set; }
        public int UserId { get; set; }
        public string CollectionName { get; set; } = null!;
        public int DailyLimit { get; set; }

        public virtual User User { get; set; } = null!;
        public virtual ICollection<Mem> Mems { get; set; }
    }
}
