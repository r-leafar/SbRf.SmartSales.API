using SbRf.SmartSales.Core.Entities;
using System;
using System.Collections.Generic;
using System.Text;

namespace SbRf.SmartSales.Core.Entity.Products
{
    public class ProductSale : IHistory
    {
        public int ProductId { get; set; }
        public Product Produto { get; set; }
        public float MarginPercentage { get; set; }
        public float NetMarginPercentage { get; set; }
        public  decimal SalePrice { get; set; }
        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; } 
    }
}
