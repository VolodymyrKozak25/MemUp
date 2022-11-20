using System.Collections;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Threading.Tasks;

namespace Database_Access_Level.IRepositories
{
    public interface ICollectionRepository: IRepository<Collection>
    {
        Task<List<Collection>> GetCollectionForUser(int userID, string? query);
    }
}
