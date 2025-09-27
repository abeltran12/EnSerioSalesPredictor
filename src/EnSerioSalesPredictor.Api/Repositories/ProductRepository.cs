using Dapper;
using EnSerioSalesPredictor.Api.Context;
using EnSerioSalesPredictor.Api.Contracts;
using EnSerioSalesPredictor.Api.Models;

namespace EnSerioSalesPredictor.Api.Repositories;

public class ProductRepository(DapperContext context) : IProductRepository
{
    private readonly DapperContext _context = context;

    public async Task<List<Product>> GetProductsAsync()
    {
        using var connection = _context.CreateConnection();

        var shippers = await connection.QueryAsync<Product>(
            "SP_GET_PRODUCTS", 
            commandType: System.Data.CommandType.StoredProcedure
        );

        return shippers.ToList();
    }
}