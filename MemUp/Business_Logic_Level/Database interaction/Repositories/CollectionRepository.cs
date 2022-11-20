using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Threading.Tasks;
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

        public async Task<List<Collection>> GetCollectionForUser(int userID, string? query)
        {
            var collections = new List<Collection>();

            if (string.IsNullOrEmpty(query))
            {
                collections = await context.Set<Collection>()
                    .Where(c => c.UserId == userID)
                    .ToListAsync();
            }
            else
            {
                collections = await context.Set<Collection>()
                    .Where(c => c.UserId == userID & c.CollectionName.ToLower().Contains(query.ToLower()))
                    .ToListAsync();
            }

            return collections;
        }
    }
}
