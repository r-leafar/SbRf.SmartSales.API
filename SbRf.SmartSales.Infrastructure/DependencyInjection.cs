using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Options;
using SbRf.SmartSales.Core.Interface.Repository;
using SbRf.SmartSales.Core.Interfaces.Repository;
using SbRf.SmartSales.Infrastructure.Context;
using SbRf.SmartSales.Infrastructure.Interceptors;
using SbRf.SmartSales.Infrastructure.Options;
using SbRf.SmartSales.Infrastructure.Repository;

namespace SbRf.SmartSales.Infrastructure
{
    public static class DependencyInjection
    {
        public static IServiceCollection AddInfrastructureDI(
        this IServiceCollection services)
        {
            services.AddDbContext<ApplicationDbContext>((sp, options) =>
            {
                var dbOptions = sp.GetRequiredService<IOptions<DatabaseOptions>>().Value;

                options.UseNpgsql(
                    BuildConnectionString(dbOptions),
                    npgsql => npgsql.MigrationsAssembly("SbRf.SmartSales.Infrastructure")
                )
                .UseSnakeCaseNamingConvention();
            });

            RegisterInterceptors(services);

            services.AddScoped(typeof(IReadRepository<,>), typeof(ReadRepository<,>));
            services.AddScoped(typeof(IWriteRepository<,>), typeof(WriteRepository<,>));
            services.AddScoped<IProductRepository, ProductRepository>();

            return services;
        }

        private  static void RegisterInterceptors(IServiceCollection services)
        {
            services.AddDbContext<ApplicationDbContext>(options =>
            {
                options.AddInterceptors(new PreventProductCostDeleteInterceptor());
                options.AddInterceptors(new SoftDeleteInterceptor());
            });

        }
        private static string BuildConnectionString(DatabaseOptions opt)
        {
            return opt.URL ?? $"Host={opt.Host};Port={opt.Port};Database={opt.Database};Username={opt.Username};Password={opt.Password};";
        }
    }
}
