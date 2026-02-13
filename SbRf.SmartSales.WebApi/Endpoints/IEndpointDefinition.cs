namespace SbRf.SmartSales.WebApi.Endpoints
{
    public interface IEndpointDefinition
    {
        string BaseRoute { get; }
        string Tag { get; }
        void DefineEndpoints(IEndpointRouteBuilder app);
    }
}
