using SbRf.SmartSales.Application.Dtos.Requests;
using SbRf.SmartSales.Application.Mappings;
using SbRf.SmartSales.Core.Entity.Products;
using SbRf.SmartSales.Core.Interfaces.Repository;
using System;

namespace SbRf.SmartSales.Application.UseCases.Request
{
    public class CreateProduct
    {
        private readonly IProductRepository _repository;


        public CreateProduct(IProductRepository repository)
        {
            _repository = repository;
        }

        public async Task<int> Handle(CreateProductRequest dto)
        {

            if (!Enum.IsDefined(typeof(EnumUnitOfMeasure), dto.UnitOfMeasureType)|| dto.UnitOfMeasureType == EnumUnitOfMeasure.None)
                throw new ApplicationException("Invalid unitOfMeasureType.");

            var product = new Product(
               name: dto.Name,
               description: dto.Description,
               unitOfMeasureType: dto.UnitOfMeasureType,
               productCostList: dto.ProductCostList.ToEntity(),
               productParameterList: dto.ProductParameterList.ToEntity()

           );

            product.ProductBrandId = dto.ProductBrandId;
            product.ProductClassificationId = dto.ProductClassificationId;
            product.UnitOfMeasureType = dto.UnitOfMeasureType;
            product.AdditionalInformation = dto.AdditionalInformation;
            product.Location = dto.Location;

            await _repository.AddAsync(product);

            return product.Id;
        }
    }
}
