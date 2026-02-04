using SbRf.SmartSales.Core.Entities;
using SbRf.SmartSales.Core.Share.Ensure;
using System;
using System.Collections.Generic;
using System.Text;

namespace SbRf.SmartSales.Core.Entity.Products
{
    public class ProductCost : IHistory
    {
        private ProductCost() { }
        public ProductCost(int productId, EnumProductCost productCostType, decimal value, DateTime startDate, DateTime endDate)
        {
            Ensure.IsGreaterThanZero(productId);
            Ensure.NotNull(startDate);
            Ensure.IsGreaterThanZero(value);

            ProductId = productId;
            ProductCostType = productCostType;
            Value = value;
            StartDate = startDate;
            EndDate = endDate;
        }
        public int ProductId { get; set; }
        public Product Product { get; set; }
        public EnumProductCost ProductCostType { get; set; }
        public decimal Value { get; set; }
        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; }

    }
}
