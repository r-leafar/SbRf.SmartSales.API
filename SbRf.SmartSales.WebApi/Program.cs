using OpenTelemetry.Exporter;
using OpenTelemetry.Metrics;
using OpenTelemetry.Resources;
using OpenTelemetry.Trace;
using SbRf.SmartSales.Infrastructure;
using SbRf.SmartSales.Infrastructure.Options;
using SbRf.SmartSales.WebApi.Endpoints;
using SbRf.SmartSales.WebApi.Exceptions;
using SbRf.SmartSales.WebApi.Extensions;
using Serilog;
using Serilog.Sinks.OpenTelemetry;

void showInfos(WebApplicationBuilder builder)
{
    var loggerOptions = builder.Configuration.GetSection("Logger").Get<LoggerOptions>();
    var databaseOptions = builder.Configuration.GetSection("Database").Get<DatabaseOptions>();
    Console.WriteLine();
    Console.WriteLine($"Logger: {loggerOptions.URI}");
    Console.WriteLine($"Database: {databaseOptions.ConnectionString}");
    Console.WriteLine();
}

void setupOpenTelemetry(WebApplicationBuilder builder)
{
    var loggerOptions = builder.Configuration.GetSection("Logger").Get<LoggerOptions>();

    builder.Services.AddOpenTelemetry()
        .ConfigureResource(r => r.AddService("sbrf-smartsales-webapi"))
        .WithTracing(tracing => tracing
            .AddAspNetCoreInstrumentation()
            .AddHttpClientInstrumentation()
            .AddOtlpExporter(o =>
            {
                o.Endpoint = new Uri(loggerOptions.URI);
                o.Protocol = OtlpExportProtocol.Grpc;
            }))
        .WithMetrics(metrics => metrics
            .AddAspNetCoreInstrumentation()
            .AddHttpClientInstrumentation()
            .AddRuntimeInstrumentation()
            .AddOtlpExporter(o =>
            {
                o.Endpoint = new Uri(loggerOptions.URI);
                o.Protocol = OtlpExportProtocol.Grpc;
            }));
}

void setupLogger(WebApplicationBuilder builder)
{
    var loggerOptions = builder.Configuration.GetSection("Logger").Get<LoggerOptions>();

    Log.Logger = new LoggerConfiguration()
        .ReadFrom.Configuration(builder.Configuration)
        .Enrich.FromLogContext()
        .Enrich.WithProperty("app","sbrf-smartsales-webapi")
        .WriteTo.Console()
        .WriteTo.OpenTelemetry(o =>
        {
            o.Endpoint = loggerOptions.URI;
            o.Protocol = OtlpProtocol.Grpc;
            o.ResourceAttributes = new Dictionary<string, object>
            {
                ["service.name"] = "sbrf-smartsales-webapi"
            };
        }).CreateLogger();

    builder.Host.UseSerilog();
}

var builder = WebApplication.CreateBuilder(args);

builder.Services.Configure<DatabaseOptions>(builder.Configuration.GetSection("Database"));
builder.Services.Configure<LoggerOptions>(builder.Configuration.GetSection("Logger"));

showInfos(builder);

setupLogger(builder);

setupOpenTelemetry(builder);

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
