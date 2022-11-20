using System.Linq;
using System.Threading.Tasks;

namespace Database_Access_Level.IRepositories
{
    public interface IMemRepository: IRepository<Mem>
    {
        Task<int> EvaluateReviewQueue(Collection collection);
        int EvaluateStudyQueue(Collection collection);
    }
}
