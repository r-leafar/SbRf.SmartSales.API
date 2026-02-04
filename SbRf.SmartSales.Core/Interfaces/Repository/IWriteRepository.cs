using SbRf.SmartSales.Core.Entities;
using System;
using System.Collections.Generic;
using System.Security.Cryptography;
using System.Text;

namespace SbRf.SmartSales.Core.Interface.Repository
{
    public interface IWriteRepository<TEntity, TId> where TEntity : BaseEntity<TId>
    {
        public Task<TId> AddAsync(TEntity entity);

        public Task<TId[]> AddAsync(params TEntity[] entity);

        public Task UpdateAsync(TEntity entity);

        public Task UpdateAsync(params TEntity[] entity);

        public Task DeleteAsync(TEntity entity);

        public Task DeleteAsync(params TEntity[] entity);
    }
}
