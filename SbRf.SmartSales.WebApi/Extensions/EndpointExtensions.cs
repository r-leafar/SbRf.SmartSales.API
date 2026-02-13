using SbRf.SmartSales.WebApi.Endpoints;

namespace SbRf.SmartSales.WebApi.Extensions
{
    public static class EndpointExtensions
    {
        private static List<Type>? _endpointTypes;
        public static IEndpointRouteBuilder MapSmartSalesEndpoints(this IEndpointRouteBuilder app)
        {
            if (_endpointTypes == null)
                throw new InvalidOperationException("Deve chamar AddSmartSalesEndpoints antes de MapSmartSalesEndpoints.");

            using var scope = app.ServiceProvider.CreateScope();

            foreach (var type in _endpointTypes)
            {
                var instance = scope.ServiceProvider.GetRequiredService(type) as IEndpointDefinition;
                instance?.DefineEndpoints(app);
            }

            return app;
        }
        public static IServiceCollection AddSmartSalesEndpoints(this IServiceCollection services)
        {
            _endpointTypes = typeof(IEndpointDefinition).Assembly.GetTypes()
             .Where(t => t.IsClass && !t.IsAbstract && typeof(IEndpointDefinition).IsAssignableFrom(t))
             .ToList();

            foreach (var type in _endpointTypes)
            {
                services.AddScoped(type);
            }

            return services;
        }
    }
}
