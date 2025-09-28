using System.Text.Json;
using EnSerioSalesPredictor.Api;
using EnSerioSalesPredictor.Api.Contracts;
using EnSerioSalesPredictor.Api.Dtos;
using EnSerioSalesPredictor.Api.RequestFeautures;
using FluentValidation;
using Microsoft.AspNetCore.Mvc;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddProblemDetails();

builder.Services.AddDependencies();

builder.Services.AddOpenApi();

var app = builder.Build();

app.UseExceptionHandler();

if (app.Environment.IsDevelopment())
{
    app.MapOpenApi();
}

app.UseHttpsRedirection();

app.UseCors("CorsPolicy");

app.MapGet("/shippers", async (IShipperService service) =>
{
    var shippers = await service.GetShipperAsync();
    return Results.Ok(shippers);
});

app.MapGet("/employees", async (IEmployeeService service) =>
{
    var employees = await service.GetEmployeesAsync();
    return Results.Ok(employees);
});

app.MapGet("/products", async (IProductService service) =>
{
    var products = await service.GetProductsAsync();
    return Results.Ok(products);
});

app.MapGet("/salespredictions", async (
    HttpContext context,
    [FromServices] ISalesPredictionService service,
    [FromQuery] int pageNumber = 1,
    [FromQuery] int pageSize = 10,
    [FromQuery] string orderBy = "",
    [FromQuery] string sort = "") =>
{
    var sales = await service.GetSalesPredictionsAsync(
        new RequestParameters
        {
            PageSize = pageSize,
            PageNumber = pageNumber,
            OrderBy = orderBy,
            Sort = sort
        });

    context.Response.Headers.TryAdd("X-Pagination", 
        JsonSerializer.Serialize(sales.metaData));
    
    return Results.Ok(sales.Item1);
});

app.MapGet("/customers/{id}/orders", async (
    HttpContext context,
    [FromServices] IOrderService service,
    [FromRoute] int id,
    [FromQuery] int pageNumber = 1,
    [FromQuery] int pageSize = 10,
    [FromQuery] string orderBy = "",
    [FromQuery] string sort = "") =>
{
    var orders = await service.GetOrdersAsync(
        id,
        new RequestParameters
        {
            PageSize = pageSize,
            PageNumber = pageNumber,
            OrderBy = orderBy,
            Sort = sort
        });

    context.Response.Headers.TryAdd("X-Pagination", 
        JsonSerializer.Serialize(orders.metaData));

    return Results.Ok(orders.Item1);
});

app.MapPost("/customers/{id}/orders", async (
    [FromServices] IOrderService service,
    [FromRoute] int id,
    [FromBody] CreateOrderDto dto,
    IValidator<CreateOrderDto> validator) =>
{
    var validationResult = await validator.ValidateAsync(dto);
    if (!validationResult.IsValid)
    {
        return Results.ValidationProblem(validationResult.ToDictionary());
    }
    
    var result = await service.CreateOrderAsync(id, dto);

    if (result == 0)
        return Results.BadRequest("Order could not be created.");

    return Results.Ok(result);
});

app.Run();