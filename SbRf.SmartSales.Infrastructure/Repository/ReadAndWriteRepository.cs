using SbRf.SmartSales.Core.Entities;
using SbRf.SmartSales.Core.Interface.Repository;
using SbRf.SmartSales.Infrastructure.Context;
using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Text;

namespace SbRf.SmartSales.Infrastructure.Repository
{
    public class ReadAndWriteRepository<T, TId> : IReadRepository<T, TId>, IWriteRepository<T, TId> where T : BaseEntity<TId>
    {
        private readonly ReadRepository<T, TId> repositoryRead;
        private readonly WriteRepository<T, TId> repositoryWrite;

        public ReadAndWriteRepository(ApplicationDbContext _dbContext)
        {
            repositoryRead = new ReadRepository<T, TId>(_dbContext ?? throw new ArgumentNullException(nameof(_dbContext)));
            repositoryWrite = new WriteRepository<T, TId>(_dbContext);
        }

        public async Task<TId> AddAsync(T entity)
        {
            return await repositoryWrite.AddAsync(entity);
        }

        public async Task AddAsync(params T[] entity)
        {
            await repositoryWrite.AddAsync(entity);
        }

        public async Task DeleteAsync(T entity)
        {
            await repositoryWrite.DeleteAsync(entity);
        }

        public async Task DeleteAsync(params T[] entity)
        {
            await repositoryWrite.DeleteAsync(entity);
        }

        public async Task<T> FindAsync(params TId[] id)
        {
           return await repositoryRead.FindAsync(id);
        }

        public async Task<T> GetFirstOrDefaultAsync(ISpecificationRepository<T> specification, bool disableTracking = true)
        {
            return await repositoryRead.GetFirstOrDefaultAsync(specification,disableTracking);
        }

        public async Task<PagedResult<TResult>> GetPagedListAsync<TResult>(ISpecificationRepository<T> specification, Expression<Func<T, TResult>> selector = null, int pageIndex = 1, int pageSize = 20, bool disableTracking = true, bool onlyDistinct = false) where TResult : class
        {
            return await repositoryRead.GetPagedListAsync(specification, selector, pageIndex, pageSize, disableTracking,onlyDistinct);
        }

        public async Task UpdateAsync(T entity)
        {
            await repositoryWrite.UpdateAsync(entity);
        }

        public async Task UpdateAsync(params T[] entity)
        {
            await repositoryWrite.UpdateAsync(entity);
        }
    }
}
