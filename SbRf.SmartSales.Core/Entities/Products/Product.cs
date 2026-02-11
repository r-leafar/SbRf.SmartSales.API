using SbRf.SmartSales.Core.Entities;
using SbRf.SmartSales.Core.Exceptions;
using SbRf.SmartSales.Core.Share.Ensure;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Text;

namespace SbRf.SmartSales.Core.Entity.Products
{
    public class Product : BaseEntity<int>,ISoftDeletable
    {

        private Product() { }

        public Product(string name, string description, EnumUnitOfMeasure unitOfMeasureType, ICollection<ProductCost> productCostList, ICollection<ProductParameter> productParameterList)
        {
            Ensure.NotNullOrWhiteSpace(name);
            Ensure.NotNullOrWhiteSpace(description);
            Ensure.NoDuplicateValues(productParameterList, i => i.ProductParameterType);
            ValidateProductCost(productCostList);

            Name = name;
            Description = description;
            UnitOfMeasureType = unitOfMeasureType;
            ProductCostList = productCostList;
            ProductParameterList = productParameterList;
        }
        public string Name { get; set; }
        public string Description { get; set; }
        public ICollection<ProductCost> ProductCostList { get; set; }
        public ICollection<ProductParameter> ProductParameterList { get; set; }
        public int? ProductBrandId { get; set; }
        public ProductBrand? ProductBrand { get; set; }
        public string? ProductClassificationId { get; set; }
        public ProductClassification? ProductClassification { get; set; }
        public EnumUnitOfMeasure UnitOfMeasureType { get; set; }
        public string? Location { get; set; }
        public string? AdditionalInformation { get; set; }
        public DateTime? DeletedAt { get; }

        private void ValidateProductCost(ICollection<ProductCost> productCostList)
        {
            Ensure.NoDuplicateValues(productCostList, e => e.ProductCostType);
            ProductCost.ValidateRequiredProductCostTypes(productCostList);
        }
    }
}
