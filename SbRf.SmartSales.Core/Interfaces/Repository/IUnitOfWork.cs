using System;
using System.Collections.Generic;
using System.Text;

namespace SbRf.SmartSales.Core.Interface.Repository
{
    public interface IUnitOfWork
    {
        Task<int> SaveChangesAsync(params IUnitOfWork[] unitOfWork);

        string GetSQLConnectionString();
    }
}
