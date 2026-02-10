using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using SbRf.SmartSales.Core.Entity.Products;
using System;
using System.Collections.Generic;
using System.Text;

namespace SbRf.SmartSales.Infrastructure.Persistence.Configurations
{
    public class ProductConfiguration : IEntityTypeConfiguration<Product>
    {
        public void Configure(EntityTypeBuilder<Product> builder)
        {
            builder.HasKey(x => x.Id);

            builder.Property(e => e.Name)
                .IsRequired();

            builder.Property(e => e.Description)
                .IsRequired();

            builder.HasMany(s => s.ProductCostList)
                   .WithOne( c => c.Product)
                   .HasForeignKey(c => c.ProductId)
                   .OnDelete(DeleteBehavior.Cascade);

            builder.HasMany(s => s.ProductParameterList)
                   .WithOne(c => c.Product)
                   .HasForeignKey(c => c.ProductId)
                   .OnDelete(DeleteBehavior.Cascade);

            builder.HasOne(e => e.ProductBrand)
                   .WithMany()
                   .HasForeignKey(e => e.ProductBrandId)
                   .OnDelete(DeleteBehavior.Restrict);

            builder.HasOne(e => e.ProductClassification)
                .WithMany(p => p.ProductList)
                .HasForeignKey(e => e.ProductClassificationId)
                .OnDelete(DeleteBehavior.Restrict);

            builder.HasQueryFilter( p => p.DeletedAt == null);
        }
    }
}
