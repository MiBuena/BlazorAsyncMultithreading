using Microsoft.EntityFrameworkCore;
using PopulationCensus.Data.DB;
using PopulationCensus.Data.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;

namespace PopulationCensus.Data.Repositories
{
    public class EfRepository<TEntity> : IAsyncRepository<TEntity>
          where TEntity : class 
    {
        private readonly PopulationContext context;
        private readonly DbSet<TEntity> dbSet;

        public EfRepository(PopulationContext context)
        {
            this.context = context;
            this.dbSet = context.Set<TEntity>();
        }

        protected PopulationContext Context => this.context;

        public virtual async Task<IEnumerable<TEntity>> GetListAsync(
            Expression<Func<TEntity, bool>>? filter = null,
            Func<IQueryable<TEntity>, IOrderedQueryable<TEntity>>? orderBy = null,
            CancellationToken cancellationToken = default, 
            int skip = 0,
            int take = int.MaxValue)
        {
            var query = ApplyCommonManipulations(filter, orderBy, skip);

            return await query
                .Take(take)
                .ToListAsync(cancellationToken);
        }

        public virtual async IAsyncEnumerable<TEntity> GetOneByOneAsync(
            Expression<Func<TEntity, bool>>? filter = null,
            Func<IQueryable<TEntity>, IOrderedQueryable<TEntity>>? orderBy = null,
            [EnumeratorCancellation] CancellationToken cancellationToken = default,
            int skip = 0,
            int take = int.MaxValue)
        {
            var query = ApplyCommonManipulations(filter, orderBy, skip);

            var a = await query
                .Take(take)
                .ToListAsync(cancellationToken);

            foreach (var item in a)
            {
                yield return item;
            }
        }

        public async Task<TEntity> FirstOrDefaultAsync(
            Expression<Func<TEntity, bool>>? filter = null,
            Func<IQueryable<TEntity>, IOrderedQueryable<TEntity>>? orderBy = null,
            CancellationToken cancellationToken = default)
        {
            var query = ApplyCommonManipulations(filter, orderBy);

            return await query.FirstOrDefaultAsync(cancellationToken);
        }

        private IQueryable<TEntity> ApplyCommonManipulations(Expression<Func<TEntity, bool>>? filter = null,
            Func<IQueryable<TEntity>, 
                IOrderedQueryable<TEntity>>? orderBy = null,
            int skip = 0)
        {
            IQueryable<TEntity> query = dbSet;

            if (filter != null)
            {
                query = query.Where(filter);
            }

            if (orderBy != null)
            {
                query = orderBy(query);
            }

            if(skip != 0)
            {
                query = query.Skip(skip);
            }

            return query;
        }

        public void Add(TEntity entity)
        {
            dbSet.Add(entity);
        }

        public void Delete(int id)
        {
            TEntity entityToDelete = dbSet.Find(id);
            Delete(entityToDelete);
        }

        public void Delete(TEntity entityToDelete)
        {
            if (context.Entry(entityToDelete).State == EntityState.Detached)
            {
                dbSet.Attach(entityToDelete);
            }
            dbSet.Remove(entityToDelete);
        }

        public void Update(TEntity entityToUpdate)
        {
            dbSet.Attach(entityToUpdate);
            context.Entry(entityToUpdate).State = EntityState.Modified;
        }

        public void AddRange(IEnumerable<TEntity> entities)
        {
            dbSet.AddRange(entities);
        }
    }
}
