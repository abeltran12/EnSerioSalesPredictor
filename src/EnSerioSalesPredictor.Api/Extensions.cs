using EnSerioSalesPredictor.Api.Context;
using EnSerioSalesPredictor.Api.Contracts;
using EnSerioSalesPredictor.Api.Repositories;
using EnSerioSalesPredictor.Api.Services;
using EnSerioSalesPredictor.Api.Validators;
using FluentValidation;

namespace EnSerioSalesPredictor.Api;

public static class Extensions
{
    public static void AddDependencies(this IServiceCollection services)
    {
        services.AddSingleton(service =>
        {
            var configuration = service.GetRequiredService<IConfiguration>();

            var connectionString = configuration.GetConnectionString("Default") ??
                throw new ApplicationException("Connection String is null");

            return new SqlConnectionFactory(connectionString);
        });

        services.AddScoped<DapperContext>();
        services.AddScoped<IShipperRepository, ShipperRepository>();
        services.AddScoped<IShipperService, ShipperService>();

        services.AddScoped<IEmployeeRepository, EmployeeRepository>();
        services.AddScoped<IEmployeeService, EmployeeService>();

        services.AddScoped<IProductRepository, ProductRepository>();
        services.AddScoped<IProductService, ProductService>();

        services.AddScoped<ISalesPredictionRepository, SalesPredictionRepository>();
        services.AddScoped<ISalesPredictionService, SalesPredictionService>();

        services.AddScoped<IOrderRepository, OrderRepository>();
        services.AddScoped<IOrderService, OrderService>();

        services.AddValidatorsFromAssemblyContaining<CreateOrderDtoValidator>();

        services.AddCors(options =>
        {
            options.AddPolicy("CorsPolicy", builder =>
                builder.WithOrigins("http://localhost:4200")
                .AllowAnyMethod()
                .AllowAnyHeader()
                .WithExposedHeaders("X-Pagination")
            );
        });
    }
}