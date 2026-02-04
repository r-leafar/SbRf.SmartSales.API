using SbRf.SmartSales.Core.Entities;
using System.Linq.Expressions;

namespace SbRf.SmartSales.Core.Interface.Repository
{
    public interface IReadRepository<TEntity, TId> where TEntity : BaseEntity<TId>
    {
        public Task<TEntity> FindAsync(params TId[] id);

        public Task<TEntity> GetFirstOrDefaultAsync(
                ISpecificationRepository<TEntity> specification,
                bool disableTracking = true);
        public Task<PagedResult<TResult>> GetPagedListAsync<TResult>(
            ISpecificationRepository<TEntity> specification,
            Expression<Func<TEntity, TResult>> selector = null,
            int pageIndex = 1,
            int pageSize = 20,
            bool disableTracking = true,
            bool onlyDistinct = false) where TResult : class;
    }

}
