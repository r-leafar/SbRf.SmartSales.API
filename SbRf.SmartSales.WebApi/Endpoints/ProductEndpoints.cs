using SbRf.SmartSales.Application.Dtos.Requests;
using SbRf.SmartSales.Application.Dtos.Responses;
using SbRf.SmartSales.Application.UseCases.Request;
using SbRf.SmartSales.Core.Interfaces.Repository;
using SbRf.SmartSales.Application.Mappings;

namespace SbRf.SmartSales.WebApi.Endpoints
{
    public static class ProductEndpoints
    {
        public static RouteGroupBuilder MapProductEndpoints(this RouteGroupBuilder group)
        {
            group.MapPost("/", CreateProduct)
                 .WithName("CreateProduct")
                 .Produces<ProductResponse>(StatusCodes.Status201Created)
                 .Produces(StatusCodes.Status400BadRequest);

            return group;
        }
        private static async Task<IResult> CreateProduct(CreateProductRequest product, IProductRepository repository)
        {
            var command = new CreateProduct(repository);
            var idProduct = await command.Handle(product);
            return Results.Created($"/products/{idProduct}", product.ToResponse(idProduct));
        }

    }
}
