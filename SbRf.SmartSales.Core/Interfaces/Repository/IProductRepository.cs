using SbRf.SmartSales.Core.Entity.Products;
using SbRf.SmartSales.Core.Interface.Repository;
using System;
using System.Collections.Generic;
using System.Text;

namespace SbRf.SmartSales.Core.Interfaces.Repository
{
    public interface IProductRepository  : IWriteRepository<Product, int>, IReadRepository<Product, int>
    {
    }
}
