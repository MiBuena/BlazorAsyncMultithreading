using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace PopulationCensus.Data.Interfaces
{
    public interface IAsyncRepository<TEntity> where TEntity : class
    {
        Task<IEnumerable<TEntity>> GetListAsync(
            Expression<Func<TEntity, bool>>? filter = null,
            Func<IQueryable<TEntity>, IOrderedQueryable<TEntity>>? orderBy = null,
            CancellationToken cancellationToken = default,
            int skip = 0,
            int take = int.MaxValue);

        Task<TEntity> FirstOrDefaultAsync(
            Expression<Func<TEntity, bool>>? filter = null,
            Func<IQueryable<TEntity>, IOrderedQueryable<TEntity>>? orderBy = null,
            CancellationToken cancellationToken = default);

        void Add(TEntity entity);
        void AddRange(IEnumerable<TEntity> entities);

        void Update(TEntity entity);

        void Delete(int id);

        void Delete(TEntity entity);
    }
}
