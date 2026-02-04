using SbRf.SmartSales.Core.Entities;
using SbRf.SmartSales.Core.Exceptions;
using SbRf.SmartSales.Core.Share.Ensure;
using System;
using System.Collections.Generic;
using System.Text;

namespace SbRf.SmartSales.Core.Entity.Products
{
    public class Product : BaseEntity<int>
    {

        private Product() { }

        public Product(string name, string description, ICollection<ProductCost> productCostList)
        {
            Ensure.NotNullOrWhiteSpace(name);
            Ensure.NotNullOrWhiteSpace(description);
            ValidateProductCost(productCostList);

            Name = name;
            Description = description;
        }
        public string Name { get; set; }
        public string Description { get; set; }
        public ICollection<ProductCost> ProductCostList { get; set; }
        public ICollection<ProductParameter> ProductParameterList { get; set; }
        public int ProductBrandId { get; set; }
        public ProductBrand? ProductBrand { get; set; }
        public string ProductClassificationId { get; set; }
        public ProductClassification? ProductClassification { get; set; }
        public EnumUnitOfMeasure UnitOfMeasureType { get; set; }
        public string? Location { get; set; }
        public string? AdditionalInformation { get; set; }

        private void ValidateProductCost(ICollection<ProductCost> list)
        {
            Ensure.HasItens(list);

            ValidateRequiredProductCostTypes(list);

            ValidateNoDuplicateProductCostTypes(list);
        }

        private void ValidateRequiredProductCostTypes(ICollection<ProductCost> list)
        {
            var requiredTypes = new[] { EnumProductCost.Unitary, EnumProductCost.Final_Cost };

            bool hasAllRequiredTypes = requiredTypes
                    .All(t => list.Any(c => c.ProductCostType == t));

            if (!hasAllRequiredTypes)
            {
                throw new DomainException(
                    $"The list must contain ProductCostType {nameof(EnumProductCost.Unitary)} and {nameof(EnumProductCost.Final_Cost)}.");
            }
        }

        private void ValidateNoDuplicateProductCostTypes(ICollection<ProductCost> list)
        {
            if (list.GroupBy(e => e.ProductCostType).Any(g => g.Count() > 1))
            {
                throw new DomainException("Duplicate ProductCostType found.");
            }
        }
    }
}
