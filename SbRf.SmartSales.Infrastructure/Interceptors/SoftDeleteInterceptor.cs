using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Diagnostics;
using SbRf.SmartSales.Core.Entities;
using SbRf.SmartSales.Core.Entity.Products;
using SbRf.SmartSales.Core.Exceptions;
using System;
using System.Collections.Generic;
using System.Text;

namespace SbRf.SmartSales.Infrastructure.Interceptors
{
    public sealed class SoftDeleteInterceptor
   : SaveChangesInterceptor
    {
        public override InterceptionResult<int> SavingChanges(
            DbContextEventData eventData,
            InterceptionResult<int> result)
        {
            var context = eventData.Context;

            if (context is null)
                return result;

            var softDelereEntries = context.ChangeTracker
                .Entries<ISoftDeletable>()
                .Where(e => e.State == EntityState.Deleted);

          foreach(var entry in softDelereEntries)
            {
                entry.State = EntityState.Modified;
                entry.CurrentValues["DeletedAt"] = DateTime.UtcNow;
            }

            return result;
        }
    }
}
