# 1. Base Runtime
FROM mcr.microsoft.com/dotnet/aspnet:10.0 AS base
USER app
WORKDIR /app
EXPOSE 8080
EXPOSE 8081

# 2. Build Stage
FROM mcr.microsoft.com/dotnet/sdk:10.0 AS build
ARG BUILD_CONFIGURATION=Release
WORKDIR /src

COPY ["SbRf.SmartSales.WebApi/SbRf.SmartSales.WebApi.csproj", "SbRf.SmartSales.WebApi/"]
COPY ["SbRf.SmartSales.Application/SbRf.SmartSales.Application.csproj", "SbRf.SmartSales.Application/"]
COPY ["SbRf.SmartSales.Core/SbRf.SmartSales.Core.csproj", "SbRf.SmartSales.Core/"]
COPY ["SbRf.SmartSales.Infrastructure/SbRf.SmartSales.Infrastructure.csproj", "SbRf.SmartSales.Infrastructure/"]

# Restore
RUN dotnet restore "SbRf.SmartSales.WebApi/SbRf.SmartSales.WebApi.csproj"

# copy the remaining files
COPY . .

# Build
WORKDIR "/src/SbRf.SmartSales.WebApi"
RUN dotnet build "SbRf.SmartSales.WebApi.csproj" -c $BUILD_CONFIGURATION -o /app/build

# 3. Publish Stage
FROM build AS publish
ARG BUILD_CONFIGURATION=Release
RUN dotnet publish "SbRf.SmartSales.WebApi.csproj" \
    -c $BUILD_CONFIGURATION \
    -o /app/publish \
    /p:UseAppHost=false \
    /p:PublishReadyToRun=true

# 4. Final Image
FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .

# Environment variables for production
ENV DOTNET_EnableDiagnostics=0 \
    ASPNETCORE_HTTP_PORTS=8080

ENTRYPOINT ["dotnet", "SbRf.SmartSales.WebApi.dll"]