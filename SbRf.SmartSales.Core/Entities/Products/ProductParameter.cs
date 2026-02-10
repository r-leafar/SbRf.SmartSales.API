using System;
using System.Collections.Generic;
using System.Text;

namespace SbRf.SmartSales.Core.Entity.Products
{
    public class ProductParameter
    {

        private ProductParameter() { }
        public ProductParameter(EnumProductParameter productParameterType, bool value)
        {
            ProductParameterType = productParameterType;
            Value = value;
        }
        public int ProductId { get; set; }
        public Product Product { get; set; }
        public EnumProductParameter ProductParameterType { get; set; }
        public bool Value { get; set; }
    }
}
