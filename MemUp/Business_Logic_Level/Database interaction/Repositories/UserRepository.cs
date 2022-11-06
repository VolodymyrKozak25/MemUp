using System.Linq;
using Database_Access_Level;
using Database_Access_Level.IRepositories;

namespace Business_Logic.Repositories
{
    public class UserRepository: Repository<User>, IUserRepository
    {
        public UserRepository(MemUpDBContext context) 
            : base(context)
        {
        }
    }
}
