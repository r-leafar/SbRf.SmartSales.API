using System;
using System.Collections.Generic;
using System.Text;

namespace SbRf.SmartSales.Core.Entity.Products
{
    public class ProductParameter
    {
        public int ProductId { get; set; }
        public Product Product { get; set; }
        public EnumProductParameter ProductParameterType { get; set; }
        public bool Value { get; set; }
    }
}
