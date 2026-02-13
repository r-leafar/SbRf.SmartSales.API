using SbRf.SmartSales.Infrastructure;
using SbRf.SmartSales.Infrastructure.Options;
using SbRf.SmartSales.WebApi.Endpoints;
using SbRf.SmartSales.WebApi.Exceptions;
using SbRf.SmartSales.WebApi.Extensions;
using Serilog;
using Serilog.Sinks.Grafana.Loki;

var builder = WebApplication.CreateBuilder(args);

Log.Logger = new LoggerConfiguration()
    .Enrich.FromLogContext()
    .WriteTo.Console()
    .WriteTo.GrafanaLoki("http://localhost:3100")
    .CreateLogger();

builder.Host.UseSerilog();

// Add services to the container.
// Learn more about configuring OpenAPI at https://aka.ms/aspnet/openapi
builder.Services.AddOpenApi();

builder.Services.AddProblemDetails (c =>{
    c.CustomizeProblemDetails = context =>
    {
     context.ProblemDetails.Extensions.TryAdd("requestId", context.HttpContext.TraceIdentifier);
    };
});

builder.Services.AddExceptionHandler<GlobalExceptionHandler>();

builder.Services.Configure<DatabaseOptions>(builder.Configuration.GetSection("Database"));

builder.Services.ConfigureJsonSerializer();

builder.Services.AddInfrastructureDI();

builder.Services.AddScoped<ProductEndpoints>();

builder.Services.AddSmartSalesEndpoints();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.MapOpenApi();
}

app.MapSmartSalesEndpoints();

app.UseExceptionHandler();

app.Run();
