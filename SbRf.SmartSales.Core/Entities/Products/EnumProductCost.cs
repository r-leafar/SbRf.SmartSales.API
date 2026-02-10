using System;
using System.Collections.Generic;
using System.Text;

namespace SbRf.SmartSales.Core.Entity.Products
{
    public enum EnumProductCost
    {
       None = 0,
       Unitary = 1,
       Freight = 2,
       Fees = 3,
       Taxes = 4,
       Other = 5,
       FinalCost = 99
    }
}
