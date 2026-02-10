using SbRf.SmartSales.Infrastructure;
using SbRf.SmartSales.Infrastructure.Options;
using SbRf.SmartSales.WebApi.Endpoints;
using SbRf.SmartSales.WebApi.Extensions;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
// Learn more about configuring OpenAPI at https://aka.ms/aspnet/openapi
builder.Services.AddOpenApi();

builder.Services.Configure<DatabaseOptions>(builder.Configuration.GetSection("Database"));

builder.Services.ConfigureJsonSerializer();

builder.Services.AddInfrastructureDI();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.MapOpenApi();
}

app.MapGroup("/api/v1/products").MapProductEndpoints();

app.Run();
