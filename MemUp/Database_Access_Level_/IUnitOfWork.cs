using Database_Access_Level.IRepositories;
using System;
using System.Threading.Tasks;

namespace Database_Access_Level
{
    public interface IUnitOfWork: IDisposable
    {
        Task<int> SaveAsync();
        ICollectionRepository Collections { get; }
        IUserRepository Users { get; }
        IMemRepository Mems { get; }
    }
}
