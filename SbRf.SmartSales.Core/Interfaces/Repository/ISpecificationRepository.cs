using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Text;

namespace SbRf.SmartSales.Core.Interface.Repository
{
    public interface ISpecificationRepository<TEntity>
    {
        Expression<Func<TEntity, bool>> Criteria { get; }
        Func<IQueryable<TEntity>, IOrderedQueryable<TEntity>> OrderBy { get; }
        IReadOnlyCollection<Expression<Func<TEntity, object>>> Includes { get; }
    }
}
