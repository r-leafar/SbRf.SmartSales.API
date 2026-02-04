using SbRf.SmartSales.Core.Entities;
using SbRf.SmartSales.Core.Share.Ensure;
using System;
using System.Collections.Generic;
using System.Text;

namespace SbRf.SmartSales.Core.Entity.Products
{
    public class ProductBrand : BaseEntity<int>
    {
        private ProductBrand(){}

        public ProductBrand(int id, string Name)
        {
            Ensure.IsGreaterThanZero(id);
            Ensure.NotNullOrWhiteSpace(Name);

            SetId(id);
            this.Name = Name;
        }
        public string Name { get; set; }
    }
}
