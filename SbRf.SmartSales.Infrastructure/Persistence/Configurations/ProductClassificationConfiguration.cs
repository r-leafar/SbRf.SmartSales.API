using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using SbRf.SmartSales.Core.Entity.Products;
using System;
using System.Collections.Generic;
using System.Text;

namespace SbRf.SmartSales.Infrastructure.Persistence.Configurations
{
    public class ProductClassificationConfiguration : IEntityTypeConfiguration<ProductClassification>
    {
        public void Configure(EntityTypeBuilder<ProductClassification> builder)
        {
            builder.HasKey(pc => pc.Id);

            builder.Property(e => e.Id)
                .HasColumnType("ltree");
        }
    }
}
