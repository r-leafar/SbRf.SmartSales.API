using SbRf.SmartSales.Core.Entities;
using SbRf.SmartSales.Core.Share.Ensure;
using System;
using System.Collections.Generic;
using System.Text;

namespace SbRf.SmartSales.Core.Entity.Products
{
    public class ProductClassification : BaseEntity<string>
    {
        private ProductClassification() { }
        public ProductClassification(string id, string name)
        {
            Ensure.NotNullOrWhiteSpace(id);
            Ensure.NotNullOrWhiteSpace(Name);

            SetId(id);
            this.Name = Name;
        }
        public string Name { get; set; }
        public ICollection<Product> ProductList { get; set; }
    }
}
