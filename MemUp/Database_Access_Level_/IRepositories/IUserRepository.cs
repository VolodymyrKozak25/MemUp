using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Database_Access_Level.IRepositories
{
    public interface IUserRepository: IRepository<User>
    {
        Task<User?> CreateOrLoginUser(string username);
        bool IsUsernameValid(string username);
    }
}
