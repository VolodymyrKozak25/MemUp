using System.Linq;
using Database_Access_Level.IRepositories;
using Database_Access_Level;
using System.Threading.Tasks;
using System;
using Microsoft.EntityFrameworkCore;

namespace Business_Logic.Repositories
{
    public class MemRepository : Repository<Mem>, IMemRepository
    {
        public MemRepository(MemUpDBContext context)
            : base(context)
        {
        }

        public async Task<int> EvaluateReviewQueue(Collection collection)
        {
            var memsToReview = await context.Set<Mem>()
                .CountAsync(m => m.CollectionId == collection.CollectionId &
                            m.ReviewTime < DateTime.UtcNow &
                            m.Status == "review");

            return memsToReview;
        }

        public int EvaluateStudyQueue(Collection collection)
        {
            collection.StudyQueue = collection.StudyQueue + 
                                    (DateTime.Now - collection.User.LastLogin.Date).Days *
                                    collection.DailyLimit;

            return collection.StudyQueue;
        }
    }
}
