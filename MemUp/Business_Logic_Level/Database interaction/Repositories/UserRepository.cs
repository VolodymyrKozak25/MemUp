using System.Linq;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using Database_Access_Level;
using Database_Access_Level.IRepositories;
using Microsoft.EntityFrameworkCore;

namespace Business_Logic.Repositories
{
    public class UserRepository : Repository<User>, IUserRepository
    {
        public UserRepository(MemUpDBContext context)
            : base(context)
        {
        }

        public async Task<User?> CreateOrLoginUser(string username)
        {
            if (IsUsernameValid(username))
            {
                var currentUser = await context.Set<User>().SingleOrDefaultAsync(u => u.UserName == username);

                if (currentUser == null)
                {
                    await context.Set<User>().AddAsync(new User { UserName = username });
                    await context.SaveChangesAsync();

                    currentUser = await context.Set<User>().SingleAsync(u => u.UserName == username);

                    return currentUser;
                }

                return currentUser;
            }

            return null;
        }

        public bool IsUsernameValid(string username)
        {
            return (Regex.IsMatch(username, @"^[a-zA-Z0-9 ]+$")) & (4 <= username.Length) & (username.Length <= 16);
        }
    }
}
