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
        public ProductCost(EnumProductCost productCostType, decimal value, DateTime startDate, DateTime? endDate)
        {
            Ensure.NotNull(startDate);
            Ensure.IsGreaterThanZero(value);

            ProductCostType = productCostType;
            Value = value;
            StartDate = startDate;
            EndDate = endDate;
        }

        public ProductCost(int productId, EnumProductCost productCostType, decimal value,DateTime startDate, DateTime? endDate)
            : this(productCostType, value, startDate, endDate)
        {
            Ensure.IsGreaterThanZero(productId);
            ProductId = productId;
        }

        public int ProductId { get; set; }
        public Product Product { get; set; }
        public EnumProductCost ProductCostType { get; set; }
        public decimal Value { get; set; }
        public DateTime StartDate { get; set; }
        public DateTime? EndDate { get; set; }

    }
}
