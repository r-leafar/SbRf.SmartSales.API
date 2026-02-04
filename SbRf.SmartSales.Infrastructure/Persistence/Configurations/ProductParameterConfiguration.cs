using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using SbRf.SmartSales.Core.Entity.Products;
using System;
using System.Collections.Generic;
using System.Text;

namespace SbRf.SmartSales.Infrastructure.Persistence.Configurations
{

    public class ProductParameterConfiguration : IEntityTypeConfiguration<ProductParameter>
    {
        public void Configure(EntityTypeBuilder<ProductParameter> builder)
        {
            builder.HasKey(pp => new { pp.ProductId, pp.ProductParameterType });
        }
    }
}
