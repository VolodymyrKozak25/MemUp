using Database_Access_Level;
using Database_Access_Level.IRepositories;
using System.Threading.Tasks;

namespace Business_Logic.Repositories
{
    public class UnitOfWork : IUnitOfWork
    {
        private readonly MemUpDBContext _context;

        public IMemRepository Mems { get; private set; }
        public ICollectionRepository Collections { get; private set; }
        public IUserRepository Users { get; private set; }

        public UnitOfWork(MemUpDBContext context)
        {
            _context = context;
            Mems = new MemRepository(_context);
            Users = new UserRepository(_context);
            Collections = new CollectionRepository(_context);
        }

        public async Task<int> SaveAsync()
        {
            return await _context.SaveChangesAsync();
        }
        public void Dispose()
        {
            _context.Dispose();
        }
    }
}
