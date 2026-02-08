using Microsoft.EntityFrameworkCore;
using SbRf.SmartSales.Core.Entity.Products;
using SbRf.SmartSales.Core.Interfaces.Repository;
using SbRf.SmartSales.Infrastructure.Context;
using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Text;

namespace SbRf.SmartSales.Infrastructure.Repository
{
    public class ProductRepository : ReadAndWriteRepository<Product, int>, IProductRepository
    {
        private readonly ApplicationDbContext dbContext;
        public ProductRepository(ApplicationDbContext _dbContext) : base(_dbContext)
        {
            this.dbContext = _dbContext ?? throw new ArgumentNullException(nameof(_dbContext));
        }
    }
}
