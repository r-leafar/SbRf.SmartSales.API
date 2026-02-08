using SbRf.SmartSales.Application.Dtos.Requests;
using SbRf.SmartSales.Application.Dtos.Responses;
using SbRf.SmartSales.Core.Entity.Products;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Text;

namespace SbRf.SmartSales.Application.Mappings
{
    public static class ProductRequestExtensions
    {
        public static ICollection<ProductCost>  ToEntity(this ICollection<CreateProductCostRequest> request)
        {
            if (request is null || request.Count == 0) { return new Collection<ProductCost>(); }

            return request.Select(item => new ProductCost(
                            item.ProductCostType,
                            item.Value,
                            DateTime.UtcNow,
                            null))
                        .ToList();
        }
        public static ProductResponse ToResponse(this CreateProductRequest request, int IdProduto)
        {
            return new ProductResponse(IdProduto);
        }
    }
}
