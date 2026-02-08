using Microsoft.EntityFrameworkCore;
using SbRf.SmartSales.Core.Entities;
using SbRf.SmartSales.Core.Interface.Repository;
using SbRf.SmartSales.Infrastructure.Context;
using SbRf.SmartSales.Infrastructure.Exceptions;
using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Text;

namespace SbRf.SmartSales.Infrastructure.Repository
{
    public class ReadRepository<T, TId> : IReadRepository<T, TId> where T : BaseEntity<TId>
    {
        protected readonly ApplicationDbContext dbContext;
        protected readonly DbSet<T> dbSet;
        public ReadRepository(ApplicationDbContext _dbContext)
        {
            dbContext = _dbContext ?? throw new InfraestructureException(nameof(_dbContext));
            dbSet = dbContext.Set<T>();
        }

        public async Task<T> FindAsync(params TId[] id)
        {
            var entity = await dbSet.FindAsync(id);
            return entity!;
        }

        public async Task<T> GetFirstOrDefaultAsync(ISpecificationRepository<T> specification, bool disableTracking = true)
        {

            IQueryable<T> query = dbSet;

            if (disableTracking)
                query = query.AsNoTracking();

            // Apply criteria
            if (specification.Criteria is not null)
                query = query.Where(specification.Criteria);

            // Apply includes
            if (specification.Includes is not null)
            {
                foreach (var include in specification.Includes)
                    query = query.Include(include);
            }

            // Apply ordering
            if (specification.OrderBy is not null)
                query = specification.OrderBy(query);

            return await query.FirstOrDefaultAsync();
        }

        public async Task<PagedResult<TResult>> GetPagedListAsync<TResult>(ISpecificationRepository<T> specification, Expression<Func<T, TResult>> selector = null, int pageIndex = 1, int pageSize = 20, bool disableTracking = true, bool onlyDistinct = false) where TResult : class
        {
            IQueryable<T> query = dbSet;

            if (disableTracking)
                query = query.AsNoTracking();

            if (specification.Criteria is not null)
                query = query.Where(specification.Criteria);

            if (specification.Includes is not null)
            {
                foreach (var include in specification.Includes)
                    query = query.Include(include);
            }

            if (specification.OrderBy is not null)
                query = specification.OrderBy(query);

            if (onlyDistinct)
                query = query.Distinct();

            var totalCount = await query.CountAsync();

            var itemsQuery = query
                .Skip((pageIndex - 1) * pageSize)
                .Take(pageSize);

            List<TResult> items = selector is null
                ? await itemsQuery.Cast<TResult>().ToListAsync()
                : await itemsQuery.Select(selector).ToListAsync();

            return new PagedResult<TResult>
            {
                PageIndex = pageIndex,
                PageSize = pageSize,
                TotalCount = totalCount,
                Items = items
            };
        }
    }
}
