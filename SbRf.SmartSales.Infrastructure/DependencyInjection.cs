using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Options;
using SbRf.SmartSales.Infrastructure.Context;
using SbRf.SmartSales.Infrastructure.Options;

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

            //services.AddScoped(typeof(IReadOnlyRepository<,>), typeof(ReadOnlyRepository<,>));
            //services.AddScoped(typeof(IWriteOnlyRepository<,>), typeof(WriteOnlyRepository<,>));
            //services.AddScoped<IStudentRepository, StudentRepository>();

            return services;
        }
        private static string BuildConnectionString(DatabaseOptions opt)
        {
            return opt.URL ?? $"Host={opt.Host};Port={opt.Port};Database={opt.Database};Username={opt.Username};Password={opt.Password};";
        }
    }
}
