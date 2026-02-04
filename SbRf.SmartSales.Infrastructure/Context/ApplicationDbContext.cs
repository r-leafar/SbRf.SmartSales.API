using Microsoft.EntityFrameworkCore;
using SbRf.SmartSales.Core.Entity.Products;
using SbRf.SmartSales.Infrastructure.Persistence.Configurations;
using System;
using System.Collections.Generic;
using System.Text;

namespace SbRf.SmartSales.Infrastructure.Context;

public class ApplicationDbContext : DbContext
{
    public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options)
    {
    }
    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.ApplyConfiguration(new ProductConfiguration());
        modelBuilder.ApplyConfiguration(new ProductParameterConfiguration());
        modelBuilder.ApplyConfiguration(new ProductCostConfiguration());
        modelBuilder.ApplyConfiguration(new ProductClassificationConfiguration());
    }

    public DbSet<Product> Product { get; set; }
}
