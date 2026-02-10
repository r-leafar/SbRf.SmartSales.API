using System.Text.Json.Serialization;

namespace SbRf.SmartSales.WebApi.Extensions
{
    public static class JsonSerializerExtension
    {
        public static IServiceCollection ConfigureJsonSerializer(this IServiceCollection services)
        {
            services.Configure<Microsoft.AspNetCore.Http.Json.JsonOptions>(options =>
            {
                options.SerializerOptions.Converters.Add( new JsonStringEnumConverter());
            });

            return services;
        }
    }
}
