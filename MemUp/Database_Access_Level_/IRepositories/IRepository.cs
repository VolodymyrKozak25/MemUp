using Microsoft.EntityFrameworkCore.ChangeTracking;
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
        Task AddRangeAsync(IEnumerable<TEntity> entities);

        //getting data (Read)
        Task<TEntity> GetByIdAsync(int id);
        Task<IEnumerable<TEntity>> GetTableAsync();
        IEnumerable<TEntity> Find(Expression<Func<TEntity, bool>> predicate);

        //removing data (Delete)
        void Remove(TEntity entity);
        void RemoveRange(IEnumerable<TEntity> entities);

        //data binding
        Task LoadAsync();
        public LocalView<TEntity> Local();
    }
}
