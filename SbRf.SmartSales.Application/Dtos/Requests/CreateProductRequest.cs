using SbRf.SmartSales.Core.Entity.Products;
using System;
using System.Collections.Generic;
using System.Text;

namespace SbRf.SmartSales.Application.Dtos.Requests
{
    public record CreateProductRequest    
        (
            string Name,
            string Description,
            int? ProductBrandId,
            string? ProductClassificationId,
            EnumUnitOfMeasure UnitOfMeasureType,
            ICollection <CreateProductCostRequest> ProductCostList,
            ICollection<CreateProductParameterRequest> ProductParameterList,
            string? Location,
            string? AdditionalInformation
        );

    public record CreateProductCostRequest
        (
            EnumProductCost ProductCostType,
            decimal Value
        );
    public record CreateProductParameterRequest
        (
            EnumProductParameter ProductParameterType,
            bool Value
        );
}
