using SbRf.SmartSales.Core.Entities;
using SbRf.SmartSales.Core.Exceptions;
using SbRf.SmartSales.Core.Share.Ensure;

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

        public void Close(DateTime endDate)
        {
            if (EndDate.HasValue)
                throw new DomainException("ProductCost is already closed.");

            if (endDate <= StartDate)
                throw new DomainException("EndDate must be greater than StartDate.");

            EndDate = endDate;
        }
        public static void ValidateRequiredProductCostTypes(ICollection<ProductCost> list)
        {
            var requiredTypes = new[] { EnumProductCost.Unitary, EnumProductCost.FinalCost };

            bool hasAllRequiredTypes = requiredTypes
                    .All(t => list.Any(c => c.ProductCostType == t));

            if (!hasAllRequiredTypes)
            {
                throw new DomainException(
                    $"The list must contain ProductCostType {nameof(EnumProductCost.Unitary)} and {nameof(EnumProductCost.FinalCost)}.");
            }
        }
    }
}
