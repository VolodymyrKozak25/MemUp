using System.Linq;
using Database_Access_Level;
using Database_Access_Level.IRepositories;
using Microsoft.EntityFrameworkCore;

namespace Business_Logic.Repositories
{
    public class CollectionRepository : Repository<Collection>, ICollectionRepository
    {
        public CollectionRepository(MemUpDBContext context) 
            : base(context)
        {
        }

        //ICollection methods
        //
    }
}
