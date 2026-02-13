using SbRf.SmartSales.Application.Dtos.Requests;
using SbRf.SmartSales.Application.Dtos.Responses;
using SbRf.SmartSales.Application.UseCases.Request;
using SbRf.SmartSales.Core.Interfaces.Repository;
using SbRf.SmartSales.Application.Mappings;

namespace SbRf.SmartSales.WebApi.Endpoints
{
    public  class ProductEndpoints(ILogger<ProductEndpoints> _logger) : IEndpointDefinition
    {
        public string BaseRoute => "/api/v1/products";
        public string Tag => "Products";
        
        public void DefineEndpoints(IEndpointRouteBuilder app)
        {
            var group = app.MapGroup(BaseRoute)
                       .WithTags(Tag);

            group.MapPost("/", CreateProduct)
                 .WithName("CreateProduct")
                 .Produces<ProductResponse>(StatusCodes.Status201Created)
                 .Produces(StatusCodes.Status400BadRequest);        
        }
        private async Task<IResult> CreateProduct(CreateProductRequest product, IProductRepository repository)
        {
            _logger.LogInformation("Creating product: {ProductName}", product.Name);

            var command = new CreateProduct(repository);
            var idProduct = await command.Handle(product);
            return Results.Created($"/products/{idProduct}", product.ToResponse(idProduct));
        }

    }
}
