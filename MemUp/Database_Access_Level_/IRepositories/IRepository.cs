using Microsoft.EntityFrameworkCore.ChangeTracking;
using Microsoft.EntityFrameworkCore.Query;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace Database_Access_Level.IRepositories
{
    public interface IRepository<TEntity> where TEntity : class
    {
        //adding data (Create)
        Task AddAsync(TEntity entity);

        //getting data (Read)
        Task<TEntity?> GetByIdAsync(int id);

        IEnumerable<TEntity> Find(Expression<Func<TEntity, bool>> predicate);

        //removing data (Delete)
        void Remove(TEntity entity);
    }
}
