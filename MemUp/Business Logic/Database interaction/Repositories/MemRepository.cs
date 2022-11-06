using System.Linq;
using Database_Access_Level.IRepositories;
using Database_Access_Level;

namespace Business_Logic.Repositories
{
    public class MemRepository : Repository<Mem>, IMemRepository
    {
        public MemRepository(MemUpDBContext context)
            : base(context)
        {
        }
    }
}
