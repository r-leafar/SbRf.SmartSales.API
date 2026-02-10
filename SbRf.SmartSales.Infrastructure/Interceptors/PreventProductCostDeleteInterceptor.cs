using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Diagnostics;
using SbRf.SmartSales.Core.Entity.Products;
using SbRf.SmartSales.Core.Exceptions;
using System;
using System.Collections.Generic;
using System.Text;

namespace SbRf.SmartSales.Infrastructure.Interceptors
{
    public sealed class PreventProductCostDeleteInterceptor
     : SaveChangesInterceptor
    {
        public override InterceptionResult<int> SavingChanges(
            DbContextEventData eventData,
            InterceptionResult<int> result)
        {
            var context = eventData.Context;

            if (context is null)
                return result;

            var hasDelete = context.ChangeTracker
                .Entries<ProductCost>()
                .Any(e => e.State == EntityState.Deleted);

            if (hasDelete)
                throw new DomainException(
                    "ProductCost cannot be deleted. Use EndDate to close it.");

            return result;
        }
    }
}
