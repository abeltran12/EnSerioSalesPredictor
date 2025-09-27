using Dapper;
using EnSerioSalesPredictor.Api.Context;
using EnSerioSalesPredictor.Api.Contracts;
using EnSerioSalesPredictor.Api.Models;

namespace EnSerioSalesPredictor.Api.Repositories;

public class ShipperRepository(DapperContext context) : IShipperRepository
{
    private readonly DapperContext _context = context;

    public async Task<List<Shipper>> GetShipperAsync()
    {
        using var connection = _context.CreateConnection();

        var shippers = await connection.QueryAsync<Shipper>(
            "SP_GET_SHIPPERS", 
            commandType: System.Data.CommandType.StoredProcedure
        );

        return shippers.ToList();
    }
}