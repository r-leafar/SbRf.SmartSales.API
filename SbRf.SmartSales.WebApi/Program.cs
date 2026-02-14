using SbRf.SmartSales.Infrastructure;
using SbRf.SmartSales.Infrastructure.Options;
using SbRf.SmartSales.WebApi.Endpoints;
using SbRf.SmartSales.WebApi.Exceptions;
using SbRf.SmartSales.WebApi.Extensions;
using Serilog;
using Serilog.Sinks.Grafana.Loki;

void showInfos(WebApplicationBuilder builder)
{
    var loggerOptions = builder.Configuration.GetSection("Logger").Get<LoggerOptions>();
    var databaseOptions = builder.Configuration.GetSection("Database").Get<DatabaseOptions>();

    Console.WriteLine($"Logger: {loggerOptions.URI}");
    Console.WriteLine($"Database: {databaseOptions.URL}");
}

void setupLogger(WebApplicationBuilder builder)
{
    var loggerOptions = builder.Configuration.GetSection("Logger").Get<LoggerOptions>();

    Log.Logger = new LoggerConfiguration()
        .Enrich.FromLogContext()
        .Enrich.WithProperty("app","sbrf-smartsales-webapi")
        .WriteTo.Console()
        .WriteTo.GrafanaLoki(loggerOptions.URI, new[]
        { 
            new LokiLabel { Key = "app", Value = "sbrf-smartsales-webapi" }
        })
        .CreateLogger();

    Serilog.Debugging.SelfLog.Enable(msg => Console.WriteLine($"[SERILOG ERROR] {msg}"));

    builder.Host.UseSerilog();
}

var builder = WebApplication.CreateBuilder(args);

builder.Services.Configure<DatabaseOptions>(builder.Configuration.GetSection("Database"));
builder.Services.Configure<LoggerOptions>(builder.Configuration.GetSection("Logger"));

showInfos(builder);

setupLogger(builder);

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
