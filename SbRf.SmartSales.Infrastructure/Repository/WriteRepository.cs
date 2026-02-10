using Microsoft.EntityFrameworkCore;
using SbRf.SmartSales.Core.Entities;
using SbRf.SmartSales.Core.Interface.Repository;
using SbRf.SmartSales.Infrastructure.Context;
using SbRf.SmartSales.Infrastructure.Exceptions;
using System;
using System.Collections.Generic;
using System.Text;

namespace SbRf.SmartSales.Infrastructure.Repository
{
    public class WriteRepository<T, TId> : IWriteRepository<T, TId> where T : BaseEntity<TId>
    {
        protected readonly ApplicationDbContext dbContext;
        protected readonly DbSet<T> dbSet;
        public WriteRepository(ApplicationDbContext _dbContext)
        {
            dbContext = _dbContext ?? throw new ArgumentNullException(nameof(_dbContext));
            dbSet = dbContext.Set<T>();
        }

        public async Task<TId> AddAsync(T entity)
        {
            try
            {
                await dbSet.AddAsync(entity);
                await dbContext.SaveChangesAsync();
                return entity.Id;
            }
            catch (Exception ex)
            {
                throw new InfraestructureException("Error creating record.", ex.Message);
            }
        }

        public async Task AddAsync(params T[] entity)
        {
            try
            {
                await dbSet.AddRangeAsync(entity);
                await dbContext.SaveChangesAsync();
            }
            catch (Exception ex)
            {
                throw new InfraestructureException("Error creating record.", ex.Message);
            }
        }

        public async Task DeleteAsync(T entity)
        {
            try
            {
                dbSet.Remove(entity);
                await dbContext.SaveChangesAsync();
            }
            catch (Exception ex)
            {
                throw new InfraestructureException("Delete operation failed.", ex.Message);
            }
        }

        public async Task DeleteAsync(params T[] entity)
        {
            try
            {
                dbSet.RemoveRange(entity);
                await dbContext.SaveChangesAsync();
            }
            catch (Exception ex)
            {
                throw new InfraestructureException("Delete operation failed.", ex.Message);
            }
        }

        public async Task UpdateAsync(T entity)
        {
            try
            {
                dbSet.Update(entity);
                await dbContext.SaveChangesAsync();
            }
            catch (Exception ex)
            {
                throw new InfraestructureException("Update failed.", ex.Message);
            }
        }

        public async Task UpdateAsync(params T[] entity)
        {
            try
            {
                dbSet.UpdateRange(entity);
                await dbContext.SaveChangesAsync();
            }
            catch (Exception ex)
            {
                throw new InfraestructureException("Update failed.", ex.Message);
            }
        }
    }
}
