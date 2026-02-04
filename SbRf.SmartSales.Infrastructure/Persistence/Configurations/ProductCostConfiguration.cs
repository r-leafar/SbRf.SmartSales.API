using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using SbRf.SmartSales.Core.Entity.Products;
using System;
using System.Collections.Generic;
using System.Text;

namespace SbRf.SmartSales.Infrastructure.Persistence.Configurations
{
    public class ProductCostConfiguration : IEntityTypeConfiguration<ProductCost>
    {
        public void Configure(EntityTypeBuilder<ProductCost> builder)
        {
            builder.HasKey(pc => new
            {
                pc.ProductId,
                pc.ProductCostType,
                pc.EndDate
            });
        }
    }
}
